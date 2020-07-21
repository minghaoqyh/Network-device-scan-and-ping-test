namespace service_tool
{
    partial class Password
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Password));
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PasswordOK = new System.Windows.Forms.Button();
            this.PasswordCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(135, 47);
            this.textBoxPassword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(173, 25);
            this.textBoxPassword.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label1.Location = new System.Drawing.Point(43, 50);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Password";
            // 
            // PasswordOK
            // 
            this.PasswordOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.PasswordOK.Location = new System.Drawing.Point(135, 95);
            this.PasswordOK.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PasswordOK.Name = "PasswordOK";
            this.PasswordOK.Size = new System.Drawing.Size(85, 27);
            this.PasswordOK.TabIndex = 1;
            this.PasswordOK.Text = "OK";
            this.PasswordOK.UseVisualStyleBackColor = true;
            this.PasswordOK.Click += new System.EventHandler(this.PasswordOK_Click);
            // 
            // PasswordCancel
            // 
            this.PasswordCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.PasswordCancel.Location = new System.Drawing.Point(224, 95);
            this.PasswordCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PasswordCancel.Name = "PasswordCancel";
            this.PasswordCancel.Size = new System.Drawing.Size(85, 27);
            this.PasswordCancel.TabIndex = 3;
            this.PasswordCancel.Text = "Cancel";
            this.PasswordCancel.UseVisualStyleBackColor = true;
            this.PasswordCancel.Click += new System.EventHandler(this.PasswordCancel_Click);
            // 
            // Password
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 150);
            this.Controls.Add(this.PasswordCancel);
            this.Controls.Add(this.PasswordOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPassword);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Password";
            this.Text = "Password";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button PasswordOK;
        private System.Windows.Forms.Button PasswordCancel;
    }
}