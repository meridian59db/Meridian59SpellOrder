using System;
using System.Windows.Forms;
using System.Linq;
using System.Diagnostics;
using System;
using System.IO;
using System.Collections.Generic;
using WinFormsApp1; 

namespace WinFormsApp1
{
    public partial class Setup : Form
    {
        private ComboBox comboBoxSpells_F1;
        private ComboBox comboBoxSpells_F2;
        private ComboBox comboBoxSpells_F3;
        private ComboBox comboBoxSpells_F4;
        private ComboBox comboBoxSpells_F5;
        private ComboBox comboBoxSpells_F6;
        private ComboBox comboBoxSpells_F7;
        private ComboBox comboBoxSpells_F8;
        private ComboBox comboBoxSpells_F9;
        private ComboBox comboBoxSpells_F10;
        private ComboBox comboBoxSpells_F11;
        private ComboBox comboBoxSpells_F12;

        public List<Spell> SelectedSpells { get; set; }

        private Button buttonSave;
        public event EventHandler WindowClosing;

        private static readonly Spell[] arrayOfSpells = new Spell[]
        {
            new Spell(0, School.KRANAAN, "create_weapon", "Create Weapon"),
            new Spell(1, School.KRANAAN, "create_food", "Create Food"),
            new Spell(2, School.KRANAAN, "bless", "Bless"),
            new Spell(3, School.KRANAAN, "super_strength", "Super Strength"),
            new Spell(4, School.KRANAAN, "blink", "Blink"),
            new Spell(5, School.KRANAAN, "deflect", "Deflect"),
            new Spell(6, School.KRANAAN, "detect_invisible", "Detect Invisible"),
            new Spell(7, School.KRANAAN, "discordance", "Discordance"),
            new Spell(8, School.KRANAAN, "dispell_illusion", "Dispel Illusion"),
            new Spell(9, School.KRANAAN, "eagle_eyes", "Eagle Eyes"),
            new Spell(10, School.KRANAAN, "enchant_weapon", "Enchant Weapon"),
            new Spell(11, School.KRANAAN, "free_action", "Free Action"),
            new Spell(12, School.KRANAAN, "glow", "Glow"),
            new Spell(13, School.KRANAAN, "haste", "Haste"),
            new Spell(14, School.KRANAAN, "magic_shield", "Magic Shield"),
            new Spell(15, School.KRANAAN, "mana_bomb", "Mana Bomb"),
            new Spell(16, School.KRANAAN, "martyrs_battleground", "Martyr's Battleground"),
            new Spell(17, School.KRANAAN, "mend", "Mend"),
            new Spell(18, School.KRANAAN, "night_vision", "Night Vision"),
            new Spell(19, School.KRANAAN, "relay", "Relay"),
            new Spell(20, School.KRANAAN, "resist_poison", "Resist Poison"),
            new Spell(21, School.KRANAAN, "shroud", "Shroud"),
        };

        /// <summary>
        /// Gets specific skill by an Alias
        /// </summary>
        /// <param name="bind">The binding of the skill.</param>
        /// <param name="selectingVariable">The Spell we are selecting on the Combo Box.</param>
        /// <param name="y">The y positioning of the Combo Box.</param>
        private static Spell getSpellByAlias(string SkillName)
        {
            Spell targetSpell = arrayOfSpells.FirstOrDefault(spell => spell.Alias == SkillName);

            if (targetSpell != null)
            {
                // Found the spell, do something with it
                return targetSpell;
                Console.WriteLine($"Spell found: {targetSpell.Name}");
            }
            else
            {
                // Spell not found
                return null;
                Console.WriteLine("Spell not found.");
            }
        }

        /// <summary>
        /// Gets specific skill by a Name
        /// </summary>
        /// <param name="SkillName">The name of the skill.</param>
        private static Spell getSpellByName(string SkillName)
        {
            Spell targetSpell = arrayOfSpells.FirstOrDefault(spell => spell.Name == SkillName);

            if (targetSpell != null)
            {
                // Found the spell, do something with it
                return targetSpell;
                Console.WriteLine($"Spell found: {targetSpell.Name}");
            }
            else
            {
                // Spell not found
                return null;
                Console.WriteLine("Spell not found.");
            }
        }

        public Setup()
        {
            SelectedSpells = new List<Spell>();

            LoadData();
            InitializeComponent();

            // Subscribe Setup_FormClosing to the FormClosing event
            this.FormClosing += Setup_FormClosing;
        }

        private Spell LoadDataByKey(string keyToLoad)
        {
            string filePath = Path.Combine(Application.StartupPath, "", "data.ini"); ;
            Dictionary<string, string> iniData = IniFileReader.LoadIniFile(filePath);


            // Access the data loaded from the INI file
            foreach (KeyValuePair<string, string> entry in iniData)
            {
                
                if (entry.Key == keyToLoad)
                {
                    Spell theSpell = getSpellByName(entry.Value);

                    if (theSpell != null)
                    {
                        return theSpell;
                    }
                }
            }
            return null;
        }

        public static List<Spell> LoadData()
        {
            string filePath = Path.Combine(Application.StartupPath, "", "data.ini");
            Dictionary<string, string> iniData = IniFileReader.LoadIniFile(filePath);

            List<Spell> spells = new List<Spell>();

            // Iterar pelos valores do dicionário e criar instâncias de Spell
            foreach (string spellName in iniData.Values)
            {
                Spell spell = getSpellByName(spellName);
                if (spell != null)
                {
                    spells.Add(spell);
                }
            }

            return spells;
        }

        /// <summary>
        /// Initalizes the Setup.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Creating a Panel to contain the ComboBox and Label
            /*Panel panel = new Panel();
            panel.Location = new Point(50, 50);
            panel.Size = new Size(200, 70);
            this.Controls.Add(panel);*/

            // the combo boxes
            comboBoxSpells_F1 = addComboBox("F1", 0);
            comboBoxSpells_F2 = addComboBox("F2", 30);
            comboBoxSpells_F3 = addComboBox("F3", 60);
            comboBoxSpells_F4 = addComboBox("F4", 90);
            comboBoxSpells_F5 = addComboBox("F5", 120);
            comboBoxSpells_F6 = addComboBox("F6", 150);
            comboBoxSpells_F7 = addComboBox("F7", 180);
            comboBoxSpells_F8 = addComboBox("F8", 210);
            comboBoxSpells_F9 = addComboBox("F9", 240);
            comboBoxSpells_F10 = addComboBox("F10", 270);
            comboBoxSpells_F11 = addComboBox("F11", 300);
            comboBoxSpells_F12 = addComboBox("F12", 330);

            // buttonSave
            buttonSave = new Button();
            buttonSave.Location = new System.Drawing.Point(50, 360);
            buttonSave.Size = new System.Drawing.Size(150, 30);
            buttonSave.Text = "Save";
            // Subscribe to the Click event of the button
            buttonSave.Click += ButtonSave_Click;
            this.Controls.Add(buttonSave);

            this.ClientSize = new System.Drawing.Size(250, 390);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SecondaryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setup";
            this.ResumeLayout(false);
        }

        /// <summary>
        /// Adds a Combo Box.
        /// </summary>
        /// <param name="bind">The binding of the skill.</param>
        /// <param name="panel">The panel holding the Combo Box.</param>
        /// <param name="y">The y positioning of the Combo Box.</param>
        private ComboBox addComboBox(string bind, int y)
        {
            ComboBox comboBox = createComboBox(bind, y);
            this.Controls.Add(comboBox);
            return comboBox;
        }

        /// <summary>
        /// Builds the structure of the Combo Box.
        /// </summary>
        /// <param name="bind">The binding of the skill.</param>
        /// <param name="y">The y positioning of the Combo Box.</param>
        private ComboBox createComboBox(string bind, int y)
        {
            ComboBox newComboBox = new ComboBox();
            newComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            newComboBox.FormattingEnabled = true;

            //newComboBox.Items.Add(bind+": Select a Spell");
          
            // Add all the spells
            foreach (Spell spell in arrayOfSpells)
            {
                newComboBox.Items.Add(spell.Alias);
            }

            // Load the persisted data
            if (LoadDataByKey(bind) == null)
            {
                newComboBox.SelectedIndex = 0;
            } else
            {
                Spell loadedSpell = LoadDataByKey(bind);
                newComboBox.SelectedItem = loadedSpell.Alias;
                SelectedSpells.Add(loadedSpell);
            }

            // Assign the event handler using a lambda expression
            /*newComboBox.SelectedIndexChanged += (sender, e) =>
            {

                // Check if the selected index is 0 (label) and handle accordingly
                if (newComboBox.SelectedIndex == 0)
                {
                    // For example, display a message or disable functionality
                    newComboBox.SelectedIndex = 0;
                    MessageBox.Show("Please select a spell.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Handle the case where a spell is selected
                    // For example, enable functionality related to the selected spell
                    ComboBox comboBox = (ComboBox)sender;
                    string selectedValue = comboBox.SelectedItem?.ToString();
                    Spell spell = getSpellByAlias(selectedValue);

                    if (spell != null)
                    {
                        MessageBox.Show("selected " + spell.Name);
                        
                    }
                }
            };*/


            newComboBox.Location = new System.Drawing.Point(30, y);

            // Add label
            Label theLabel = new Label();
            theLabel.Text = bind;
            theLabel.Location = new System.Drawing.Point(0, y+3);
            theLabel.AutoSize = false;
            theLabel.Size = new Size(30, 20);
            this.Controls.Add(theLabel);

            newComboBox.Size = new System.Drawing.Size(200, 21);
            return newComboBox;
        }

        private void Setup_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Raise the event when the form is closing
            WindowClosing?.Invoke(this, EventArgs.Empty);
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            // Save data to .ini file
            SaveDataToIniFile();
        }

        /// <summary>
        /// Saves the data do ini file
        /// </summary>
        private void SaveDataToIniFile()
        {
            string fileName = "data.ini";

            Dictionary<string, string> dataToSave = new Dictionary<string, string>();

            // Save the bindings data
            string[] binds = new string[] { "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12" };
            ComboBox[] comboBoxes = new ComboBox[] { comboBoxSpells_F1, comboBoxSpells_F2, comboBoxSpells_F3, comboBoxSpells_F4, comboBoxSpells_F5, comboBoxSpells_F6, comboBoxSpells_F7, comboBoxSpells_F8, comboBoxSpells_F9, comboBoxSpells_F10, comboBoxSpells_F11, comboBoxSpells_F12 };

            // Parses the config
            for (int i = 0; i < comboBoxes.Length; i++)
            {
                ParseConfig(dataToSave, comboBoxes[i], binds[i]);
            }

            try
            {
                // Write data to .ini file
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    foreach (KeyValuePair<string, string> config in dataToSave)
                    {
                        writer.WriteLine($"{config.Key}={config.Value}");
                    }
                }

                // Show a message to indicate that data has been saved
                MessageBox.Show("Data has been saved to data.ini", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., show an error message)
                MessageBox.Show($"An error occurred while saving data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }

        /// <summary>
        /// Represents the Schools
        /// </summary>
        private void ParseConfig(Dictionary<string, string> dataToSave, ComboBox comboBox, string bind)
        {
            if (comboBox.SelectedItem != null)
            {
                Spell spellToGet = getSpellByAlias(comboBox.SelectedItem.ToString());
                if (spellToGet != null)
                {
                    dataToSave.Add(bind, spellToGet.Name);
                }
            }
        }

        /// <summary>
        /// Logs a message to a file .txt
        /// </summary>
        /// <param name="message">The Logging message</param>
        private void LogMessageToFile(string message)
        {
            string logFilePath = Path.Combine(Application.StartupPath, "", "log.txt");
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }

        /// <summary>
        /// Reads the .INI file
        /// </summary>
        public static class IniFileReader
        {
            public static Dictionary<string, string> LoadIniFile(string filePath)
            {
                Dictionary<string, string> iniData = new Dictionary<string, string>();

                try
                {
                    // Read all lines from the INI file
                    string[] lines = File.ReadAllLines(filePath);

                    // Parse each line to extract key-value pairs
                    foreach (string line in lines)
                    {
                        // Split each line into key and value parts
                        string[] parts = line.Split('=');

                        // Ensure that the line contains a valid key-value pair
                        if (parts.Length == 2)
                        {
                            // Add the key-value pair to the dictionary
                            string key = parts[0].Trim();
                            string value = parts[1].Trim();
                            iniData[key] = value;
                        }
                    }
                }
                catch (IOException ex)
                {
                    // Handle file reading errors
                    Console.WriteLine($"Error reading INI file: {ex.Message}");
                }

                return iniData;
            }
        }
    }


    /// <summary>
    /// Represents the Schools
    /// </summary>
    public enum School
    {
        KRANAAN,
        QOR,
        SHALILLE,
        FAREN,
        RIIJA
    }

    /// <summary>
    /// Represents the Spell
    /// </summary>
    public class Spell
    {
        public int Id { get; }
        public string Name { get; }
        public string Alias { get; }
        public School School { get; }

        public Spell(int id, School school, string name, string alias)
        {
            Id = id;
            School = school;
            Name = name;
            Alias = alias;
        }
    }
}
