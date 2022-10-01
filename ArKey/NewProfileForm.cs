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
    public partial class NewProfileForm : Form
    {
        public string ProfilePath { get; set; }
        public Form1 parForm { get; set; }
        public NewProfileForm(string profilePath, Form1 form1, int ButtonsAdd)
        {
            InitializeComponent();

            ProfilePath = profilePath;
            parForm = form1;

            //LoadProfile(ProfilePath);

            data = new IniData();


            for (int i = 1; i <= ButtonsAdd; i++)
            {
                data.Sections.AddSection("btn" + i);
                ButtonEdit buttonEdit = new ButtonEdit();
                buttonEdit.ButtonName = "btn" + i;
                flowLayoutPanel1.Controls.Add(buttonEdit);
            }
        }

        FileIniDataParser parser = new FileIniDataParser();
        IniData data;

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
            parser.WriteFile(parForm.ProfilePath+textBox1.Text+".ini", data);

            parForm.ReadProfiles();

            parForm.comboBox2.Text = textBox1.Text + ".ini";

            parForm.LoadProfile(parForm.ProfilePath +textBox1.Text+".ini");

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
