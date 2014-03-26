﻿namespace SS.Ynote.Classic.UI
{
    partial class ConsoleUI
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbcommand = new System.Windows.Forms.TextBox();
            this.completemenu = new AutocompleteMenuNS.AutocompleteMenu();
            this.SuspendLayout();
            // 
            // tbcommand
            // 
            this.completemenu.SetAutocompleteMenu(this.tbcommand, this.completemenu);
            this.tbcommand.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tbcommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbcommand.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.tbcommand.Location = new System.Drawing.Point(1, 0);
            this.tbcommand.Name = "tbcommand";
            this.tbcommand.Size = new System.Drawing.Size(502, 29);
            this.tbcommand.TabIndex = 0;
            this.tbcommand.Click += new System.EventHandler(this.textBox1_Click);
            this.tbcommand.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // completemenu
            // 
            this.completemenu.AppearInterval = 20;
            this.completemenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.completemenu.ImageList = null;
            this.completemenu.Items = new string[0];
            this.completemenu.LeftPadding = 10;
            this.completemenu.MaximumSize = new System.Drawing.Size(503, 200);
            this.completemenu.SearchPattern = ".*";
            this.completemenu.TargetControlWrapper = null;
            // 
            // ConsoleUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 29);
            this.Controls.Add(this.tbcommand);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ConsoleUI";
            this.Text = "Console";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbcommand;
        private AutocompleteMenuNS.AutocompleteMenu completemenu;
    }
}