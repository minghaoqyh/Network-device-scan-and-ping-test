namespace service_tool
{
    partial class AdvancedForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedForm));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbDicom = new System.Windows.Forms.CheckBox();
            this.cbAdvancedImage = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cbAdvancedDelay = new System.Windows.Forms.CheckBox();
            this.cbLight = new System.Windows.Forms.CheckBox();
            this.cbCross = new System.Windows.Forms.CheckBox();
            this.cbCaries = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnGet = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnUpdate);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.groupBox6);
            this.groupBox2.Controls.Add(this.btnGet);
            this.groupBox2.Location = new System.Drawing.Point(27, 21);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(373, 358);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "License Feature List";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(260, 318);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(77, 25);
            this.btnUpdate.TabIndex = 20;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbDicom);
            this.groupBox3.Controls.Add(this.cbAdvancedImage);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(19, 200);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(335, 95);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "SW Features";
            // 
            // cbDicom
            // 
            this.cbDicom.AutoSize = true;
            this.cbDicom.Enabled = false;
            this.cbDicom.Location = new System.Drawing.Point(263, 64);
            this.cbDicom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbDicom.Name = "cbDicom";
            this.cbDicom.Size = new System.Drawing.Size(18, 17);
            this.cbDicom.TabIndex = 15;
            this.cbDicom.UseVisualStyleBackColor = true;
            // 
            // cbAdvancedImage
            // 
            this.cbAdvancedImage.AutoSize = true;
            this.cbAdvancedImage.Enabled = false;
            this.cbAdvancedImage.Location = new System.Drawing.Point(263, 30);
            this.cbAdvancedImage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbAdvancedImage.Name = "cbAdvancedImage";
            this.cbAdvancedImage.Size = new System.Drawing.Size(18, 17);
            this.cbAdvancedImage.TabIndex = 14;
            this.cbAdvancedImage.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Advanced Image Editing";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 64);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "DICOM Connectivity";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.cbAdvancedDelay);
            this.groupBox6.Controls.Add(this.cbLight);
            this.groupBox6.Controls.Add(this.cbCross);
            this.groupBox6.Controls.Add(this.cbCaries);
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Controls.Add(this.label4);
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.label6);
            this.groupBox6.Location = new System.Drawing.Point(19, 31);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox6.Size = new System.Drawing.Size(336, 158);
            this.groupBox6.TabIndex = 18;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "HW Features";
            // 
            // cbAdvancedDelay
            // 
            this.cbAdvancedDelay.AutoSize = true;
            this.cbAdvancedDelay.Location = new System.Drawing.Point(264, 128);
            this.cbAdvancedDelay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbAdvancedDelay.Name = "cbAdvancedDelay";
            this.cbAdvancedDelay.Size = new System.Drawing.Size(18, 17);
            this.cbAdvancedDelay.TabIndex = 15;
            this.cbAdvancedDelay.UseVisualStyleBackColor = true;
            // 
            // cbLight
            // 
            this.cbLight.AutoSize = true;
            this.cbLight.Location = new System.Drawing.Point(264, 94);
            this.cbLight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbLight.Name = "cbLight";
            this.cbLight.Size = new System.Drawing.Size(18, 17);
            this.cbLight.TabIndex = 14;
            this.cbLight.UseVisualStyleBackColor = true;
            // 
            // cbCross
            // 
            this.cbCross.AutoSize = true;
            this.cbCross.Location = new System.Drawing.Point(264, 61);
            this.cbCross.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbCross.Name = "cbCross";
            this.cbCross.Size = new System.Drawing.Size(18, 17);
            this.cbCross.TabIndex = 13;
            this.cbCross.UseVisualStyleBackColor = true;
            // 
            // cbCaries
            // 
            this.cbCaries.AutoSize = true;
            this.cbCaries.Location = new System.Drawing.Point(264, 29);
            this.cbCaries.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbCaries.Name = "cbCaries";
            this.cbCaries.Size = new System.Drawing.Size(18, 17);
            this.cbCaries.TabIndex = 12;
            this.cbCaries.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(175, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Caries Detection Mode";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 62);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(191, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "Cross-Polarization Mode";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 95);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 15);
            this.label5.TabIndex = 6;
            this.label5.Text = "Light Boost";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 128);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(215, 15);
            this.label6.TabIndex = 7;
            this.label6.Text = "Advanced Delay Curing mode";
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(173, 318);
            this.btnGet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(77, 25);
            this.btnGet.TabIndex = 3;
            this.btnGet.Text = "Get";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // AdvancedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 405);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "AdvancedForm";
            this.Text = "AdvancedForm";
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.CheckBox cbDicom;
        private System.Windows.Forms.CheckBox cbAdvancedImage;
        private System.Windows.Forms.CheckBox cbAdvancedDelay;
        private System.Windows.Forms.CheckBox cbLight;
        private System.Windows.Forms.CheckBox cbCross;
        private System.Windows.Forms.CheckBox cbCaries;
    }
}