using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArKey
{
    public partial class ButtonEdit : UserControl
    {
        public string ButtonName { 
            get { return label1.Text; }
            set { label1.Text = value; } 
        }

        public bool MultiBtnChecked
        {
            get { return MultiKeyBox.Checked; }
            set { MultiKeyBox.Checked = value;}
        }

        public bool SingleBtnChecked
        {
            get { return SingleClickBox.Checked; }
            set { SingleClickBox.Checked = value;}  
        }

        public bool TypeBtnChecked
        {
            get { return TypeModeBox.Checked; }
            set { TypeModeBox.Checked = value; }
        }

        public string ButtonAction
        {
            get { return buttonConfigText.Text; }
            set { buttonConfigText.Text = value; }  
        }

        public ButtonEdit()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (this.ButtonClick != null)
                this.ButtonClick(this, e);
        }

        [Browsable(true)]
        [Category("Action")]
        [Description("Delete Button Clicked")]
        [DisplayName("Delete Button")]
        public event EventHandler ButtonClick;
    }
}
