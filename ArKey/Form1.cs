using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using WindowsInput;
using System.IO;
using IniParser;
using IniParser.Model;
using IWshRuntimeLibrary;
using File = System.IO.File;
using Tulpep.NotificationWindow;

namespace ArKey
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            // Get a list of serial port names.
            string[] ports = SerialPort.GetPortNames();

            // Display each port name to the console.
            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
            }

            Timer timer = new Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 5000;
            timer.Start();

            ReadProfiles();

            LoadSettings(Application.StartupPath + "//Settings.ini");
        }

        private void LoadSettings(string v)
        {
            if (File.Exists(v))
            {
                var parser = new FileIniDataParser();
                IniData data = parser.ReadFile(v);

                string ArPort = data["Settings"]["Port"].ToString();
                string ProfileSel = data["Settings"]["Profile"].ToString();

                AutoLaunchCheck.Checked = bool.Parse(data["Settings"]["AutoStart"]);
                AutoConnectCheck.Checked = bool.Parse(data["Settings"]["AutoConnect"]);


                if (comboBox1.Items.Contains(ArPort))
                {
                    comboBox1.Text = ArPort;
                }
                else
                {
                    AutoConnectCheck.Checked = false;
                    MessageBox.Show("Arduino is Not Configured, Maybe not plugged in or has changed Port ID", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (comboBox2.Items.Contains(ProfileSel))
                {
                    comboBox2.Text = ProfileSel;
                }
                else
                {
                    MessageBox.Show("Selected Profile is not Avaiable, Could be Deleted, or something Else is Wrong", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }



                LoadProfile(ProfilePath + comboBox2.Text);
            }
            else
            {
                if (!File.Exists(v))
                {
                    string[] Defaults =
                    {
                    "[Settings]",
                    "Port=",
                    "Profile=",
                    "NewProfileBTNS=12",
                    "AutoStart=False",
                    "AutoConnect=False"

                };

                    File.WriteAllLines(v, Defaults);
                }

                LoadSettings(v);
            }
        }

        public string ProfilePath = Application.StartupPath + "//Profiles//";

        public void ReadProfiles()
        {
            comboBox2.Items.Clear();
            foreach(var File in Directory.GetFiles(ProfilePath))
            {
                comboBox2.Items.Add(Path.GetFileName(File));
            }

            comboBox2.SelectedIndex = 0;

            LoadProfile(ProfilePath+comboBox2.Text);
        }

        FileIniDataParser parser = new FileIniDataParser();
        public void LoadProfile(string iniPath)
        {
            Console.WriteLine("Loading File: " + iniPath);

            arkeyButtons.Clear();

            IniData data = parser.ReadFile(iniPath);

            foreach (var Section in data.Sections)
            {

                int ButtonId = Convert.ToInt32(Section.SectionName.Replace("btn",""));
                string btnAction = Section.Keys["action"].ToString();
                bool btnSingle = bool.Parse(Section.Keys["single"].ToString());
                bool typeMode = bool.Parse(Section.Keys["type_mode"].ToString());
                bool multiKey = bool.Parse(Section.Keys["multiKey"].ToString());

                arkeyButtons.Add(new ArkeyButtonConfig(ButtonId,btnAction, btnSingle, typeMode, multiKey));

                Console.WriteLine(Section.SectionName+" Added Successfully!");
            }
        }

        private List<ArkeyButtonConfig> arkeyButtons = new List<ArkeyButtonConfig>();

        private SerialPort port;

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (port == null || !port.IsOpen)
            {
                if (button1.InvokeRequired)
                {
                    Invoke(new Action(() =>
                    {
                        button1.Text = "Connect";
                        ArkeyIcon.Text = "Arkey - Disconnected";
                        ConnectBTNContext.Text = "Connect";
                    }));
                }
                else
                {
                    button1.Text = "Connect";
                    ArkeyIcon.Text = "Arkey - Disconnected";
                    ConnectBTNContext.Text = "Connect";
                }
            }
            else
            {
                if (button1.InvokeRequired)
                {
                    Invoke(new Action(() =>
                    {
                        button1.Text = "Disconnect";
                        ArkeyIcon.Text = "Arkey - Connected";
                        ConnectBTNContext.Text = "Disconnect";
                    }));
                }
                else
                {
                    button1.Text = "Disconnect";
                    ArkeyIcon.Text = "Arkey - Connected";
                    ConnectBTNContext.Text = "Disconnect";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Connect")
            {
                if (comboBox1.Text != "")
                {
                    if (port != null)
                    {
                        port.Close();
                        port = null;
                        GC.Collect();
                    }

                    port = new SerialPort(comboBox1.Text, 9600);


                    port.DataReceived += Port_DataReceived;

                    port.Open();
                }
            }
            else
            {
                port.Close();
                port = null;
                GC.Collect();
            }
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            InputCapture(port.ReadLine());
        }

        private void InputCapture(string v)
        {
            Console.WriteLine(v);
            try
            {

                //int btnId = Convert.ToInt32(v.Substring(3, 2));
                string[] btnInfo = v.Split('_');
                int btnId = Convert.ToInt32(btnInfo[0].Remove(0, 3));
                var GetButtonId = arkeyButtons.FirstOrDefault(a => a.ButtonID == btnId);

                //var keys = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetButtonId.ButtonAction);

                if (GetButtonId != null)
                {
                    if (!GetButtonId.ButtonAction.Contains("App:"))
                    {

                        if (GetButtonId.ButtonSingle && !v.Contains("up") && !GetButtonId.Pressed && !GetButtonId.TypeMode && !GetButtonId.Multikey)
                        {
                            var keys = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetButtonId.ButtonAction);
                            new InputSimulator().Keyboard.KeyPress(keys);

                            GetButtonId.Pressed = true;
                        }

                        else if (GetButtonId.TypeMode && !GetButtonId.Pressed && !v.Contains("up"))
                        {

                            new InputSimulator().Keyboard.TextEntry(GetButtonId.ButtonAction);

                            GetButtonId.Pressed = true;
                        }

                        else if (GetButtonId.Multikey && !GetButtonId.Pressed && !v.Contains("up"))
                        {
                            List<VirtualKeyCode> virtualKeys = new List<VirtualKeyCode>();
                            List<VirtualKeyCode> virtualModiferKeys = new List<VirtualKeyCode>();

                            foreach (var key in GetButtonId.ButtonAction.Split(','))
                            {
                                if (key.Contains("CONTROL") || key.Contains("MENU") || key.Contains("SHIFT") || key.Contains("LWIN") || key.Contains("RWIN"))
                                {
                                    virtualModiferKeys.Add((VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), key));
                                }
                                else
                                {
                                    virtualKeys.Add((VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), key));
                                }
                            }

                            new InputSimulator().Keyboard.ModifiedKeyStroke(virtualModiferKeys, virtualKeys);

                            GC.Collect();
                            GetButtonId.Pressed = true;
                        }

                        else if (v.Contains("down") && !GetButtonId.ButtonSingle && !GetButtonId.TypeMode && !GetButtonId.Multikey)
                        {
                            var keys = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetButtonId.ButtonAction);
                            new InputSimulator().Keyboard.KeyDown(keys);
                        }
                        else
                        {
                            if (v.Contains("up") && !GetButtonId.ButtonSingle && !GetButtonId.TypeMode && !GetButtonId.Multikey)
                            {
                                var keys = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetButtonId.ButtonAction);
                                new InputSimulator().Keyboard.KeyUp(keys);
                                GetButtonId.Pressed = false;
                            }
                            else if (v.Contains("up"))
                            {
                                GetButtonId.Pressed = false;
                            }
                        }
                    }

                    else
                    {

                        if (GetButtonId.ButtonAction.Contains("Add") && !profileChange && !v.Contains("up"))
                        {
                            if (comboBox2.InvokeRequired)
                            {
                                Invoke(new Action(() =>
                                {
                                    int selected_index = comboBox2.SelectedIndex;
                                    int next_index = selected_index + 1;
                                    if (next_index < comboBox2.Items.Count)
                                    {
                                        comboBox2.SelectedIndex = next_index;
                                    }
                                    else
                                    {
                                        comboBox2.SelectedIndex = 0;
                                    }

                                    PopupNotificaiton(comboBox2.Text.Replace(".ini", ""));



                                    profileChange = true;
                                }));

                            }
                        }

                        if (GetButtonId.ButtonAction.Contains("Prev") && !profileChange && !v.Contains("up"))
                        {
                            if (comboBox2.InvokeRequired)
                            {
                                Invoke(new Action(() =>
                                {
                                    int selected_index = comboBox2.SelectedIndex;
                                    int next_index = selected_index - 1;
                                    if (next_index <= -1)
                                    {
                                        comboBox2.SelectedIndex = comboBox2.Items.Count - 1;
                                    }
                                    else
                                    {
                                        comboBox2.SelectedIndex = next_index;
                                    }

                                    PopupNotificaiton(comboBox2.Text.Replace(".ini", ""));



                                    profileChange = true;
                                }));
                            }
                        }
                        if (GetButtonId.ButtonAction.Contains("Launch") && !profileChange && !v.Contains("up"))
                        {
                            string[] actionSeperator = GetButtonId.ButtonAction.Split(',');

                            profileChange = true;

                            System.Diagnostics.Process.Start(actionSeperator[1]);
                        }


                        if (v.Contains("up"))
                        {
                            profileChange = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            GC.Collect();
        }

        private void PopupNotificaiton(string v)
        {
            PopupNotifier popup = new PopupNotifier();
            popup.BodyColor = Color.FromArgb(0, 150, 136);
            popup.TitleText = "ArKey: Changed Profile.";
            popup.TitleColor = Color.White;
            popup.TitleFont = new Font("Century Gothic", 15, FontStyle.Bold);
            popup.ShowGrip = false;

            popup.Image = Properties.Resources.Keyboard_256;
            popup.ImagePadding = new Padding(5, 5, 5, 5);

            popup.ContentText = "Profile Swapped to: " + v;
            popup.ContentColor = Color.White;
            popup.ContentFont = new Font("Century Gothic", 12);
            popup.AnimationDuration = 100;
            popup.Delay = 3000;
            popup.Popup();
        }

        bool profileChange = false;

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Text = "";

            GC.Collect();

            // Get a list of serial port names.
            string[] ports = SerialPort.GetPortNames();

            // Display each port name to the console.
            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.SelectedIndex > -1)
            {
                LoadProfile(ProfilePath + comboBox2.Text);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var EditForm = new EditProfileForm(ProfilePath + comboBox2.Text, this);
            EditForm.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings(Application.StartupPath+"//Settings.ini");
        }

        private void SaveSettings(string v)
        {
            if(!File.Exists(v))
            {
                string[] Defaults =
                {
                    "[Settings]",
                    "Port=",
                    "Profile=",
                    "NewProfileBTNS=12",
                    "AutoStart=",
                    "AutoConnect"

                };

                File.WriteAllLines(v, Defaults);
            }

            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(v);

            data["Settings"]["Port"] = comboBox1.Text;
            data["Settings"]["Profile"] = comboBox2.Text;
            data["Settings"]["AutoStart"] = AutoLaunchCheck.Checked.ToString();
            data["Settings"]["AutoConnect"] = AutoConnectCheck.Checked.ToString();

            parser.WriteFile(v, data);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();

            ArkeyIcon.Visible = true;
        }

        private void ArkeyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                this.Show();
                ArkeyIcon.Visible = false;
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            ArkeyIcon.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(Application.StartupPath + "//Settings.ini");
            NewProfileForm newProfileForm = new NewProfileForm("", this, Convert.ToInt32(data["Settings"]["NewProfileBTNS"]));
            newProfileForm.ShowDialog();
        }

        private void AutoLaunchCheck_CheckedChanged(object sender, EventArgs e)
        {
            if(AutoLaunchCheck.Checked)
            {
                WshShell wshShell = new WshShell();
                IWshRuntimeLibrary.IWshShortcut shortcut;
                string startUpFolderPath =
                  Environment.GetFolderPath(Environment.SpecialFolder.Startup);

                // Create the shortcut
                shortcut =
                  (IWshRuntimeLibrary.IWshShortcut)wshShell.CreateShortcut(
                    startUpFolderPath + "\\" +
                    Application.ProductName + ".lnk");

                shortcut.TargetPath = Application.ExecutablePath;
                shortcut.WorkingDirectory = Application.StartupPath;
                shortcut.Description = "Launch My Application";
                shortcut.IconLocation = Application.StartupPath + @"\ArKey.exe";
                shortcut.Save();
            }
            else
            {
                string startUpFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
                File.Delete(startUpFolderPath + "\\" + Application.ProductName + ".lnk");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if(AutoConnectCheck.Checked)
            {
                button1.PerformClick();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            File.Delete(ProfilePath + "\\" + comboBox2.Text);
            ReadProfiles();
        }
    }
}
