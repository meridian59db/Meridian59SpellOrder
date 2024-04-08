using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Timer = System.Windows.Forms.Timer;
using WinFormsApp1;

namespace WinFormsApp1
{
    public partial class Program : Form
    {
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int WS_EX_LAYERED = 0x80000;
        private const int GWL_EXSTYLE = -20;
        private const int SW_HIDE = 0;

        private IntPtr gameHandle;
        private IntPtr handle;
        private NotifyIcon notifyIcon;
        private ContextMenuStrip trayMenu;
        private Timer timer;
        private bool meridianWindowFocused = false;

        public static bool isSystemTrayMenuOpen = false;

        private bool hiding = false;

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowRect(IntPtr hWnd, out Rectangle lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        static extern bool UpdateLayeredWindow(IntPtr hWnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pprSrc, int crKey, ref BLENDFUNCTION pblend, int dwFlags);

        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        // Import necessary Win32 API functions
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;


        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Program());
        }

        public Program()
        {

            
            
            InitializeForm();
            InitializeSystemTray();

            // Initialize and start the timer
            timer = new Timer();
            timer.Interval = 100; // Set the interval (in milliseconds) as needed
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!isSystemTrayMenuOpen)
            {
                IntPtr foregroundWindowHandle = GetForegroundWindow();
                Process[] processes = Process.GetProcessesByName("Meridian");


                foreach (Process process in processes)
                {
                    if (foregroundWindowHandle == process.MainWindowHandle)
                    {
                        meridianWindowFocused = true;
                        break;
                    }
                }

                if (meridianWindowFocused)
                {
                    ShowOverlay();
                }
                else
                {
                    HideOverlay();
                }
            }
        }


        private void InitializeSystemTray()
        {
            trayMenu = new ContextMenuStrip();

            // Adding title label to the context menu
            ToolStripLabel titleLabel = new ToolStripLabel(Application.ProductName);
            titleLabel.Font = new Font(titleLabel.Font, FontStyle.Bold);
            trayMenu.Items.Add(titleLabel);

            // Adicionando item "Exit" ao menu de contexto
            ToolStripMenuItem setupItem = new ToolStripMenuItem("Setup");
            ToolStripMenuItem exitMenuItem = new ToolStripMenuItem("Exit");

            setupItem.Click += OnSetup;
            trayMenu.Items.Add(setupItem);

            exitMenuItem.Click += OnExit;
            trayMenu.Items.Add(exitMenuItem);


            // Subscribe to the ContextMenuClosed event
            trayMenu.Closed += trayMenu_Closed;

            notifyIcon = new NotifyIcon();
            notifyIcon.Text = "MeridianSpellOrders";
            
            notifyIcon.MouseClick += NotifyIcon_MouseClick;

            string imagePath = Path.Combine(Application.StartupPath, "", "icon.ico");

            using (Image image = Image.FromFile(imagePath))
            {
                if (image != null)
                {
                    notifyIcon.Icon = new Icon(imagePath);
                }

            }

            // Definir o menu de contexto correto
            notifyIcon.ContextMenuStrip = trayMenu;

            notifyIcon.Visible = true;
        }



        private void InitializeForm()
        {
            this.BackColor = Color.Black; // Change the background color to a key color like Magenta
            this.TransparencyKey = Color.Black;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false; // esconder dps
            this.TopMost = true;
            this.DoubleBuffered = true; // Enable double buffering to reduce flickering
            this.handle = this.Handle;
            this.Size = new Size(550, 90); // Set the size of the overlay window
            this.Load += OverlayForm_Load;
        }

        private void trayMenu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            // Reset the flag when the system tray menu is closed without selection
            isSystemTrayMenuOpen = false;
        }

        private void OverlayForm_Load(object sender, EventArgs e)
        {
            // Find the game process by its name
            Process[] processes = Process.GetProcessesByName("Meridian");
            if (processes.Length > 0)
            {
                gameHandle = processes[0].MainWindowHandle;
                SetWindowLong(this.Handle, GWL_EXSTYLE, GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_TRANSPARENT);
                ShowOverlay();
                //MessageBox.Show("Found");
            }
            else
            {
                //MessageBox.Show("Game not found.");
                this.Close();
            }
        }

        private void ShowOverlay()
        {
           
                if (gameHandle != IntPtr.Zero)
                {
                    Rectangle rect;
                    if (GetWindowRect(gameHandle, out rect))
                    {

                    // Adjust the position of the overlay
                    int offsetX = 50; // Adjust as needed
                    int offsetY = 105; // Adjust as needed
                    int overlayWidth = this.Width;
                    int overlayHeight = this.Height;

                    int overlayX = rect.Left + offsetX;
                    int overlayY = rect.Top + offsetY;

                    // Ensure the overlay stays within the game window bounds
                    if (overlayX + overlayWidth > rect.Right)
                    {
                        overlayX = rect.Right - overlayWidth;
                    }
                    if (overlayY + overlayHeight > rect.Bottom)
                    {
                        overlayY = rect.Bottom - overlayHeight;
                    }

                    // Set the position of the overlay
                    this.Location = new Point(overlayX, overlayY);

                    ShowWindow(this.handle, SW_HIDE); // Hide the window frame
                    this.Show();
                }
                    else
                    {
                        //MessageBox.Show("Failed to get game window position.");
                        this.Close();
                    }
                }
                else
                {
                    //MessageBox.Show("Game window handle not found.");
                    this.Close();
                }
            
        }

        private void HideOverlay()
        {
            if (this.Visible)
            {
                this.Hide();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Draw text
            base.OnPaint(e);

            /*using (Pen pen = new Pen(Color.White, 1))
            {
                e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, Width - 1, Height - 1));
            }*/

            using (Font font = new Font("Arial", 12))
            {

                int amount = 40;
                int pastF10Offset = 3;
                Setup programInstance = new Setup();
                for (var i = 0; i < programInstance.SelectedSpells.Count; i++) 
                {

                    e.Graphics.DrawString("F" + (i + 1) + " ", font, Brushes.White, new Point(((i + (amount - (i >= 9 ? 5 : 2)))), 25));

                    // Load and draw the image from a file
                    string spellName = programInstance.SelectedSpells[i].Name;
                    string imagePath = Path.Combine(Application.StartupPath, "Assets", spellName +".png");

                    if (File.Exists(imagePath))
                    {
                        using (Image image = Image.FromFile(imagePath))
                        {
                            Rectangle imageRect = new Rectangle((i + amount), 50, image.Width, image.Height);

                            e.Graphics.DrawImage(image, new Point((i + amount), 50));

                            using (Pen pen = new Pen(Color.White, 1))
                            {
                                e.Graphics.DrawRectangle(pen, imageRect);
                            }
                            
                        }
                    }

                    amount += 40;
                }
 
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Do not paint background to make it transparent
        }

        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;

                // Reset the flag when left-clicking on the notifyIcon
                isSystemTrayMenuOpen = false;
            }
            else if (e.Button == MouseButtons.Right)
            {
                isSystemTrayMenuOpen = true; // System tray menu is open
            }
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnSetup(object sender, EventArgs e)
        {
            System.Console.WriteLine("Setup");
            Setup secondaryForm = new Setup();
            isSystemTrayMenuOpen = true;
            secondaryForm.WindowClosing += (sender, args) => { isSystemTrayMenuOpen = false; };
            secondaryForm.Show();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;  // Cancel the default action
                this.Hide();      // Hide the form instead of closing
            }
            base.OnFormClosing(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                notifyIcon.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
                return createParams;
            }
        }

        protected override void SetVisibleCore(bool value)
        {
            // Ensure that the window is always visible
            base.SetVisibleCore(true);
        }

        private void Program_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);

            // Allow dragging when the left mouse button is pressed down on the form
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;

            public BLENDFUNCTION(byte op, byte flags, byte alpha, byte format)
            {
                BlendOp = op;
                BlendFlags = flags;
                SourceConstantAlpha = alpha;
                AlphaFormat = format;
            }
        }
    }

}
