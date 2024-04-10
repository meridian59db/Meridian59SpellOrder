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
        private ComboBox? comboBoxSpells_F1;
        private ComboBox? comboBoxSpells_F2;
        private ComboBox? comboBoxSpells_F3;
        private ComboBox? comboBoxSpells_F4;
        private ComboBox? comboBoxSpells_F5;
        private ComboBox? comboBoxSpells_F6;
        private ComboBox? comboBoxSpells_F7;
        private ComboBox? comboBoxSpells_F8;
        private ComboBox? comboBoxSpells_F9;
        private ComboBox? comboBoxSpells_F10;
        private ComboBox? comboBoxSpells_F11;
        private ComboBox? comboBoxSpells_F12;

        public List<Spell> SelectedSpells { get; set; }

        private Button? buttonSave;
        public event EventHandler? WindowClosing;

        private static readonly Spell[] arrayOfSpells =
        [
            new Spell(School.KRANAAN, "empty", "Empty"),
            // Kranaan
            new Spell(School.KRANAAN, "", GetSeparator("Kranaan")),
            new Spell(School.KRANAAN, "anti_magic_aura", "Anti-Magic Aura"),
            new Spell(School.KRANAAN, "armor_of_gort", "Armor of Gort"),
            new Spell(School.KRANAAN, "bless", "Bless"),
            new Spell(School.KRANAAN, "blink", "Blink"),
            new Spell(School.KRANAAN, "create_food", "Create Food"),
            new Spell(School.KRANAAN, "create_weapon", "Create Weapon"),
            new Spell(School.KRANAAN, "deflect", "Deflect"),
            new Spell(School.KRANAAN, "detect_invisible", "Detect Invisible"),
            new Spell(School.KRANAAN, "discordance", "Discordance"),
            new Spell(School.KRANAAN, "dispell_illusion", "Dispel Illusion"),
            new Spell(School.KRANAAN, "eagle_eyes", "Eagle Eyes"),
            new Spell(School.KRANAAN, "enchant_weapon", "Enchant Weapon"),
            new Spell(School.KRANAAN, "free_action", "Free Action"),
            new Spell(School.KRANAAN, "glow", "Glow"),
            new Spell(School.KRANAAN, "haste", "Haste"),
            new Spell(School.KRANAAN, "hunt", "Hunt"),
            new Spell(School.KRANAAN, "killing_fields", "Killing Fields"),
            new Spell(School.KRANAAN, "magic_shield", "Magic Shield"),
            new Spell(School.KRANAAN, "mana_bomb", "Mana Bomb"),
            new Spell(School.KRANAAN, "martyrs_battleground", "Martyr's Battleground"),
            new Spell(School.KRANAAN, "mend", "Mend"),
            new Spell(School.KRANAAN, "night_vision", "Night Vision"),
            new Spell(School.KRANAAN, "relay", "Relay"),
            new Spell(School.KRANAAN, "resist_magic", "Resist Magic"),
            new Spell(School.KRANAAN, "resist_poison", "Resist Poison"),
            new Spell(School.KRANAAN, "shatterlock", "Shatterlock"),
            new Spell(School.KRANAAN, "shroud", "Shroud"),
            new Spell(School.KRANAAN, "super_strength", "Super Strength"),
            // Jala
            new Spell(School.QOR, "", GetSeparator("Jala")),
            new Spell(School.JALA, "civility", "Civility"),
            new Spell(School.JALA, "conciliation", "Conciliation"),
            new Spell(School.JALA, "crystalize_mana", "Crystalize Mana"),
            new Spell(School.JALA, "disharmony", "Disharmony"),
            new Spell(School.JALA, "distill", "Distill"),
            new Spell(School.JALA, "invigorate", "Invigorate"),
            new Spell(School.JALA, "jig", "Jig"),
            new Spell(School.JALA, "mana_convergence", "Mana Convergence"),
            new Spell(School.JALA, "melancholy", "Melancholy"),
            new Spell(School.JALA, "mirth", "Mirth"),
            new Spell(School.JALA, "profane_resonance", "Profane Resonance"),
            new Spell(School.JALA, "rejuvenate", "Rejuvenate"),
            new Spell(School.JALA, "restorate", "Restorate"),
            new Spell(School.JALA, "sacred_resonance", "Sacred Resonance"),
            new Spell(School.JALA, "spellbane", "Spellbane"),
            new Spell(School.JALA, "truth", "Truth"),
            new Spell(School.JALA, "warp_time", "Warp Time"),
            // Qor
            new Spell(School.QOR, "", GetSeparator("Qor")),
            new Spell(School.QOR, "acid_touch", "Acid Touch"),
            new Spell(School.QOR, "animate", "Animate"),
            new Spell(School.QOR, "blind", "Blind"),
            new Spell(School.QOR, "blood_inheritance", "Blood Inheritance"),
            new Spell(School.QOR, "cloak", "Cloak"),
            new Spell(School.QOR, "curse_weapon", "Curse Weapon"),
            new Spell(School.QOR, "darkness", "Darkness"),
            new Spell(School.QOR, "death_link", "Death Link"),
            new Spell(School.QOR, "defile", "Defile"),
            new Spell(School.QOR, "detect_good", "Detect Good"),
            new Spell(School.QOR, "enfeeble", "Enfeeble"),
            new Spell(School.QOR, "fade", "Fade"),
            new Spell(School.QOR, "gaze_of_the_basilisk", "Gaze of the Basilisk"),
            new Spell(School.QOR, "hold", "Hold"),
            new Spell(School.QOR, "invisibility", "Invisibility"),
            new Spell(School.QOR, "karahols_curse", "Karahols Curse"),
            new Spell(School.QOR, "node_burst", "Node Burst"),
            new Spell(School.QOR, "poison_fog", "Poison Fog"),
            new Spell(School.QOR, "shadow_rift", "Shadow Rift"),
            new Spell(School.QOR, "shalille_bane", "Shalille Bane"),
            new Spell(School.QOR, "silence", "Silence"),
            new Spell(School.QOR, "splash_of_acid", "Splash of Acid"),
            new Spell(School.QOR, "swap", "Swap"),
            new Spell(School.QOR, "unholy_resolve", "Unholy Resolve"),
            new Spell(School.QOR, "unholy_weapon", "Unholy Weapon"),
            new Spell(School.QOR, "vampiric_drain", "Vampiric Drain"),

            // Shal'ille
            new Spell(School.SHALILLE, "", GetSeparator("Sha'ille")),
            new Spell(School.SHALILLE, "bond", "Bond"),
            new Spell(School.SHALILLE, "breath_of_life", "Breath of Life"),
            new Spell(School.SHALILLE, "cure_disease", "Cure Disease"),
            new Spell(School.SHALILLE, "cure_poison", "Cure Poison"),
            new Spell(School.SHALILLE, "dazzle", "Dazzle"),
            new Spell(School.SHALILLE, "detect_evil", "Detect Evil"),
            new Spell(School.SHALILLE, "final_rites", "Final Rites"),
            new Spell(School.SHALILLE, "forces_of_light", "Forces of Light"),
            new Spell(School.SHALILLE, "holy_resolve", "Holy Resolve"),
            new Spell(School.SHALILLE, "holy_symbol", "Holy Symbol"),
            new Spell(School.SHALILLE, "holy_touch", "Holy Touch"),
            new Spell(School.SHALILLE, "holy_weapon", "Holy Weapon"),
            new Spell(School.SHALILLE, "hospice", "Hospice"),
            new Spell(School.SHALILLE, "identify", "Identify"),
            new Spell(School.SHALILLE, "major_heal", "Major Heal"),
            new Spell(School.SHALILLE, "mark_of_dishonor", "Mark of Dishonor"),
            new Spell(School.SHALILLE, "minor_heal", "Minor Heal"),
            new Spell(School.SHALILLE, "portal_of_life", "Portal of Life"),
            new Spell(School.SHALILLE, "purge", "Purge"),
            new Spell(School.SHALILLE, "purify", "Purify"),
            new Spell(School.SHALILLE, "qorbane", "Qorbane"),
            new Spell(School.SHALILLE, "remove_curse", "Rescue"),
            new Spell(School.SHALILLE, "resist_acid", "Resist Acid"),
            new Spell(School.SHALILLE, "reveal", "Reveal"),
            new Spell(School.SHALILLE, "seance", "Seance"),
            //Faren
            new Spell(School.FAREN, "", GetSeparator("Faren")),
            new Spell(School.FAREN, "blast_of_fire", "Blast of Fire"),
            new Spell(School.FAREN, "bramble_wall", "Bramble Wall"),
            new Spell(School.FAREN, "brittle", "Brittle"),
            new Spell(School.FAREN, "earthquake", "Earthquake"),
            new Spell(School.FAREN, "explosive_frost", "Explosive Frost"),
            new Spell(School.FAREN, "fireball", "Fireball"),
            new Spell(School.FAREN, "firewall", "Firewall"),
            new Spell(School.FAREN, "fog", "Fog"),
            new Spell(School.FAREN, "heat", "Heat"),
            new Spell(School.FAREN, "icy_fingers", "Icy Fingers"),
            new Spell(School.FAREN, "light", "Light"),
            new Spell(School.FAREN, "lightning_bolt", "Lightning Bolt"),
            new Spell(School.FAREN, "lightning_wall", "Lightning Wall"),
            new Spell(School.FAREN, "mana_focus", "Mana Focus"),
            new Spell(School.FAREN, "mystic_touch", "Mystic Touch"),
            new Spell(School.FAREN, "resist_shock", "Resist Shock"),
            new Spell(School.FAREN, "ring_of_flames", "Ring of Flames"),
            new Spell(School.FAREN, "sand_storm", "Sand Storm"),
            new Spell(School.FAREN, "shatter", "Shatter"),
            new Spell(School.FAREN, "shocking_fury", "Shocking Fury"),
            new Spell(School.FAREN, "spider_web", "Spider Web"),
            new Spell(School.FAREN, "spore_burst", "Spore Burst"),
            new Spell(School.FAREN, "sweep", "Sweep"),
            new Spell(School.FAREN, "touch_of_flame", "Touch of Flame"),
            new Spell(School.FAREN, "winds", "Winds"),
            new Spell(School.FAREN, "withstand_fire", "Withstand Fire"),
            new Spell(School.FAREN, "zap", "Zap")
        ];

         private static string GetSeparator(string text)
        {
            // Calculate the length of the separator based on the width of the ComboBox
            int separatorLength = 32 - 4;

            // Ensure the separator length is at least the length of the text plus padding
            if (separatorLength < text.Length)
            {
                separatorLength = text.Length + 4;
            }

            // Construct the separator with the input text
            return "─" + new string('─', (separatorLength - text.Length) / 2) + "┤" + text+ "├" + new string('─', (separatorLength - text.Length + 1) / 2) + "─";
        }

        /// <summary>
        /// Gets specific skill by an Alias
        /// </summary>
        /// <param name="bind">The binding of the skill.</param>
        /// <param name="selectingVariable">The Spell we are selecting on the Combo Box.</param>
        /// <param name="y">The y positioning of the Combo Box.</param>
        private static Spell? GetSpellByAlias(string SkillName)
        {
            Spell targetSpell = arrayOfSpells.FirstOrDefault(spell => spell.Alias == SkillName);

            if (targetSpell != null)
            {
                // Found the spell, do something with it
                Console.WriteLine($"Spell found: {targetSpell.Name}");
                return targetSpell;
            }
            else
            {
                // Spell not found
                Console.WriteLine("Spell not found.");
                return null;
            }
        }

        /// <summary>
        /// Gets specific skill by a Name
        /// </summary>
        /// <param name="SkillName">The name of the skill.</param>
        private static Spell? GetSpellByName(string SkillName)
        {
            Spell targetSpell = arrayOfSpells.FirstOrDefault(spell => spell.Name == SkillName);

            if (targetSpell != null)
            {
                // Found the spell, do something with it
                Console.WriteLine($"Spell found: {targetSpell.Name}");
                return targetSpell;
            }
            else
            {
                // Spell not found
                Console.WriteLine("Spell not found.");
                return null;
            }
        }

        public Setup()
        {
            SelectedSpells = [];

            LoadData();
            InitializeComponent();

            // Subscribe Setup_FormClosing to the FormClosing event
            this.FormClosing += Setup_FormClosing;
        }

        private static Spell? LoadDataByKey(string keyToLoad)
        {
            string filePath = Path.Combine(Application.StartupPath, "", "data.ini"); ;
            Dictionary<string, string> iniData = IniFileReader.LoadIniFile(filePath);


            // Access the data loaded from the INI file
            foreach (KeyValuePair<string, string> entry in iniData)
            {
                
                if (entry.Key == keyToLoad)
                {
                    Spell theSpell = GetSpellByName(entry.Value);

                    if (theSpell != null)
                    {
                        return theSpell;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Loads the data for the file
        /// </summary>
        public static List<Spell> LoadData()
        {
            string filePath = Path.Combine(Application.StartupPath, "", "data.ini");
            Dictionary<string, string> iniData = IniFileReader.LoadIniFile(filePath);

            List<Spell> spells = new List<Spell>();

            // Iterar pelos valores do dicionário e criar instâncias de Spell
            foreach (string spellName in iniData.Values)
            {
                Spell spell = GetSpellByName(spellName);
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

            // the combo boxes
            comboBoxSpells_F1 = AddComboBox("F1", 0);
            comboBoxSpells_F2 = AddComboBox("F2", 30);
            comboBoxSpells_F3 = AddComboBox("F3", 60);
            comboBoxSpells_F4 = AddComboBox("F4", 90);
            comboBoxSpells_F5 = AddComboBox("F5", 120);
            comboBoxSpells_F6 = AddComboBox("F6", 150);
            comboBoxSpells_F7 = AddComboBox("F7", 180);
            comboBoxSpells_F8 = AddComboBox("F8", 210);
            comboBoxSpells_F9 = AddComboBox("F9", 240);
            comboBoxSpells_F10 = AddComboBox("F10", 270);
            comboBoxSpells_F11 = AddComboBox("F11", 300);
            comboBoxSpells_F12 = AddComboBox("F12", 330);

            // buttonSave
            buttonSave = new Button();
            buttonSave.Location = new System.Drawing.Point(50, 360);
            buttonSave.Size = new System.Drawing.Size(150, 30);
            buttonSave.Text = "Save";
            
            // Subscribe to the Click event of the button
            buttonSave.Click += ButtonSave_Click;
            this.Controls.Add(buttonSave);

            this.ClientSize = new Size(250, 390);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SecondaryForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Setup";
            this.ResumeLayout(false);
        }

        /// <summary>
        /// Adds a Combo Box.
        /// </summary>
        /// <param name="bind">The binding of the skill.</param>
        /// <param name="panel">The panel holding the Combo Box.</param>
        /// <param name="y">The y positioning of the Combo Box.</param>
        private ComboBox AddComboBox(string bind, int y)
        {
            ComboBox comboBox = CreateComboBox(bind, y);
            this.Controls.Add(comboBox);
            return comboBox;
        }

        /// <summary>
        /// Builds the structure of the Combo Box.
        /// </summary>
        /// <param name="bind">The binding of the skill.</param>
        /// <param name="y">The y positioning of the Combo Box.</param>
        private ComboBox CreateComboBox(string bind, int y)
        {
            ComboBox newComboBox = new()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                FormattingEnabled = true
            };

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

            // Subscribe to the SelectedIndexChanged event
            newComboBox.SelectedIndexChanged += (sender, e) =>
            {
                // Add your event handling logic here
                var selectedItem = (string)newComboBox.SelectedItem;

                Spell spell = GetSpellByAlias(selectedItem);
                if (spell != null)
                {
                    // Check if the specific option is selected
                    if (spell.Name == "")
                    {
                        // Revert the selection to the previous item or the first item
                        if (newComboBox.Items.Count > 0)
                        {
                            newComboBox.SelectedIndex = 0;
                        }
                    }
                }
            };

            // Suppress mouse wheel event when over ComboBox
            newComboBox.MouseWheel += (sender, e) =>
            {
                ((HandledMouseEventArgs)e).Handled = true;
            };

            newComboBox.Location = new Point(30, y);

            // Add label
            int offSetBindingLabel = 3;
            Label theLabel = new()
            {
                Text = bind,
                Location = new Point(0, y + offSetBindingLabel),
                AutoSize = false,
                Size = new Size(30, 20)
            };
            this.Controls.Add(theLabel);

            newComboBox.Size = new Size(200, 21);
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
            //string[] binds = ["F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12"];
            ComboBox[] comboBoxes = [comboBoxSpells_F1, comboBoxSpells_F2, comboBoxSpells_F3, comboBoxSpells_F4, comboBoxSpells_F5, comboBoxSpells_F6, comboBoxSpells_F7, comboBoxSpells_F8, comboBoxSpells_F9, comboBoxSpells_F10, comboBoxSpells_F11, comboBoxSpells_F12];

            // Parses the config
            for (int i = 0; i < comboBoxes.Length; i++)
            {
                ParseConfig(dataToSave, comboBoxes[i], "F"+(i+1));
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
                MessageBox.Show("Data has been saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                Spell spellToGet = GetSpellByAlias(comboBox.SelectedItem.ToString());
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
        RIIJA,
        JALA
    }

    /// <summary>
    /// Represents the Spell
    /// </summary>
    public class Spell(School school, string name, string alias)
    {
        public string Name { get; } = name;
        public string Alias { get; } = alias;
        public School School { get; } = school;
    }
}
