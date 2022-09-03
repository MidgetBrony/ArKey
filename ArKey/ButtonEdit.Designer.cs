namespace ArKey
{
    partial class ButtonEdit
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.SingleClickBox = new System.Windows.Forms.CheckBox();
            this.MultiKeyBox = new System.Windows.Forms.CheckBox();
            this.TypeModeBox = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonConfigText = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Button {#}";
            // 
            // SingleClickBox
            // 
            this.SingleClickBox.AutoSize = true;
            this.SingleClickBox.Location = new System.Drawing.Point(11, 90);
            this.SingleClickBox.Name = "SingleClickBox";
            this.SingleClickBox.Size = new System.Drawing.Size(84, 17);
            this.SingleClickBox.TabIndex = 1;
            this.SingleClickBox.Text = "Single Press";
            this.toolTip1.SetToolTip(this.SingleClickBox, "Key is Single Press, AKA Single Click");
            this.SingleClickBox.UseVisualStyleBackColor = true;
            // 
            // MultiKeyBox
            // 
            this.MultiKeyBox.AutoSize = true;
            this.MultiKeyBox.Location = new System.Drawing.Point(101, 90);
            this.MultiKeyBox.Name = "MultiKeyBox";
            this.MultiKeyBox.Size = new System.Drawing.Size(66, 17);
            this.MultiKeyBox.TabIndex = 2;
            this.MultiKeyBox.Text = "MultiKey";
            this.toolTip1.SetToolTip(this.MultiKeyBox, "Key Combo, AKA CTRL+S");
            this.MultiKeyBox.UseVisualStyleBackColor = true;
            // 
            // TypeModeBox
            // 
            this.TypeModeBox.AutoSize = true;
            this.TypeModeBox.Location = new System.Drawing.Point(173, 90);
            this.TypeModeBox.Name = "TypeModeBox";
            this.TypeModeBox.Size = new System.Drawing.Size(80, 17);
            this.TypeModeBox.TabIndex = 3;
            this.TypeModeBox.Text = "Type Mode";
            this.toolTip1.SetToolTip(this.TypeModeBox, "Type the selected Text");
            this.TypeModeBox.UseVisualStyleBackColor = true;
            // 
            // buttonConfigText
            // 
            this.buttonConfigText.Location = new System.Drawing.Point(12, 23);
            this.buttonConfigText.Name = "buttonConfigText";
            this.buttonConfigText.Size = new System.Drawing.Size(278, 61);
            this.buttonConfigText.TabIndex = 4;
            this.buttonConfigText.Text = "";
            this.toolTip1.SetToolTip(this.buttonConfigText, "Type as VirtualKeyCodes, if doing multiKey, please add a comma between each Key. " +
        "(EX:CONTROL,S)");
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ArKey.Properties.Resources.Trash_07_WF_24;
            this.pictureBox1.Location = new System.Drawing.Point(265, 87);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 24);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // ButtonEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonConfigText);
            this.Controls.Add(this.TypeModeBox);
            this.Controls.Add(this.MultiKeyBox);
            this.Controls.Add(this.SingleClickBox);
            this.Controls.Add(this.label1);
            this.Name = "ButtonEdit";
            this.Size = new System.Drawing.Size(307, 117);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox SingleClickBox;
        private System.Windows.Forms.CheckBox MultiKeyBox;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox TypeModeBox;
        private System.Windows.Forms.RichTextBox buttonConfigText;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
