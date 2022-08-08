
namespace can_bus_timer
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkBoxRun = new System.Windows.Forms.CheckBox();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.labelIndicator = new System.Windows.Forms.Label();
            this.checkBoxSynchronous = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBoxRun
            // 
            this.checkBoxRun.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxRun.Location = new System.Drawing.Point(13, 13);
            this.checkBoxRun.Name = "checkBoxRun";
            this.checkBoxRun.Size = new System.Drawing.Size(111, 44);
            this.checkBoxRun.TabIndex = 0;
            this.checkBoxRun.Text = "Run";
            this.checkBoxRun.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxRun.UseVisualStyleBackColor = true;
            this.checkBoxRun.CheckedChanged += new System.EventHandler(this.checkBoxRun_CheckedChanged);
            // 
            // richTextBox
            // 
            this.richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox.Location = new System.Drawing.Point(13, 77);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(700, 1000);
            this.richTextBox.TabIndex = 1;
            this.richTextBox.Text = "";
            // 
            // labelIndicator
            // 
            this.labelIndicator.BackColor = System.Drawing.Color.LightGray;
            this.labelIndicator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelIndicator.Location = new System.Drawing.Point(675, 22);
            this.labelIndicator.Name = "labelIndicator";
            this.labelIndicator.Size = new System.Drawing.Size(38, 38);
            this.labelIndicator.TabIndex = 2;
            // 
            // checkBoxSynchronous
            // 
            this.checkBoxSynchronous.Checked = true;
            this.checkBoxSynchronous.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSynchronous.Location = new System.Drawing.Point(147, 14);
            this.checkBoxSynchronous.Name = "checkBoxSynchronous";
            this.checkBoxSynchronous.Size = new System.Drawing.Size(162, 44);
            this.checkBoxSynchronous.TabIndex = 0;
            this.checkBoxSynchronous.Text = "Synchronous";
            this.checkBoxSynchronous.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxSynchronous.UseVisualStyleBackColor = false;
            this.checkBoxSynchronous.CheckedChanged += new System.EventHandler(this.checkBoxRun_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 1144);
            this.Controls.Add(this.labelIndicator);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.checkBoxSynchronous);
            this.Controls.Add(this.checkBoxRun);
            this.Name = "MainForm";
            this.Text = "Main Form";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxRun;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.Label labelIndicator;
        private System.Windows.Forms.CheckBox checkBoxSynchronous;
    }
}

