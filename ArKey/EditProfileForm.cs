using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArKey
{
    public partial class EditProfileForm : Form
    {
        public string ProfilePath { get; set; }
        public Form1 parForm { get; set; }
        public EditProfileForm(string profilePath, Form1 form1)
        {
            InitializeComponent();

            ProfilePath = profilePath;
            parForm = form1;

            LoadProfile(ProfilePath);
        }

        FileIniDataParser parser = new FileIniDataParser();
        IniData data;
        private void LoadProfile(string profilePath)
        {
            data = parser.ReadFile(profilePath);
            foreach (var Section in data.Sections)
            {
                ButtonEdit buttonEdit = new ButtonEdit();
                buttonEdit.ButtonName = Section.SectionName.ToString();
                buttonEdit.SingleBtnChecked = bool.Parse(Section.Keys["single"].ToString());
                buttonEdit.MultiBtnChecked = bool.Parse(Section.Keys["multiKey"].ToString());
                buttonEdit.TypeBtnChecked=  bool.Parse(Section.Keys["type_mode"].ToString());
                buttonEdit.ButtonAction = Section.Keys["action"].ToString();
                buttonEdit.ButtonClick += ButtonEdit_ButtonClick;
                flowLayoutPanel1.Controls.Add(buttonEdit);
            }

            this.Text = "Edit Profile: " + Path.GetFileName(profilePath);
        }

        private void ButtonEdit_ButtonClick(object sender, EventArgs e)
        {
            var Current_Control = (ButtonEdit)sender;
            data.Sections.RemoveSection(Current_Control.ButtonName);
            flowLayoutPanel1.Controls.Remove(Current_Control);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (ButtonEdit btnEdit in this.flowLayoutPanel1.Controls)
            {
                    data[btnEdit.ButtonName]["single"] = btnEdit.SingleBtnChecked.ToString();
                    data[btnEdit.ButtonName]["multiKey"] = btnEdit.MultiBtnChecked.ToString();
                    data[btnEdit.ButtonName]["type_mode"] = btnEdit.TypeBtnChecked.ToString();
                    data[btnEdit.ButtonName]["action"] = btnEdit.ButtonAction;
            }
            parser.WriteFile(ProfilePath, data);

            parForm.LoadProfile(ProfilePath);

            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            data.Sections.AddSection("btn" + numericUpDown1.Value.ToString());
            ButtonEdit buttonEdit = new ButtonEdit();
            buttonEdit.ButtonName = "btn" + numericUpDown1.Value.ToString();
            flowLayoutPanel1.Controls.Add(buttonEdit); 
        }
    }
}
