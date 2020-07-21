namespace service_tool
{
    partial class OperLogDefineForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OperLogDefineForm));
            this.listViewOperLogDefine = new System.Windows.Forms.ListView();
            this.OperLogDate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.OperLogTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOperNext = new System.Windows.Forms.Button();
            this.btnOperPrevious = new System.Windows.Forms.Button();
            this.textBoxOperInfo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listViewOperLogDefine
            // 
            this.listViewOperLogDefine.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.listViewOperLogDefine.HideSelection = false;
            this.listViewOperLogDefine.Location = new System.Drawing.Point(13, 15);
            this.listViewOperLogDefine.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.listViewOperLogDefine.Name = "listViewOperLogDefine";
            this.listViewOperLogDefine.Size = new System.Drawing.Size(673, 318);
            this.listViewOperLogDefine.TabIndex = 1;
            this.listViewOperLogDefine.UseCompatibleStateImageBehavior = false;
            this.listViewOperLogDefine.View = System.Windows.Forms.View.Details;
            this.listViewOperLogDefine.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.listViewOperLogDefine_ColumnWidthChanging);
            // 
            // OperLogDate
            // 
            this.OperLogDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.OperLogDate.Location = new System.Drawing.Point(72, 351);
            this.OperLogDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.OperLogDate.Name = "OperLogDate";
            this.OperLogDate.Size = new System.Drawing.Size(132, 21);
            this.OperLogDate.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label1.Location = new System.Drawing.Point(16, 355);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Date:";
            // 
            // OperLogTime
            // 
            this.OperLogTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.OperLogTime.Location = new System.Drawing.Point(291, 351);
            this.OperLogTime.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.OperLogTime.Name = "OperLogTime";
            this.OperLogTime.Size = new System.Drawing.Size(132, 21);
            this.OperLogTime.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label2.Location = new System.Drawing.Point(232, 355);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Time:";
            // 
            // btnOperNext
            // 
            this.btnOperNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnOperNext.Location = new System.Drawing.Point(588, 351);
            this.btnOperNext.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnOperNext.Name = "btnOperNext";
            this.btnOperNext.Size = new System.Drawing.Size(100, 27);
            this.btnOperNext.TabIndex = 11;
            this.btnOperNext.Text = "Next";
            this.btnOperNext.UseVisualStyleBackColor = true;
            this.btnOperNext.Click += new System.EventHandler(this.btnOperNext_Click);
            // 
            // btnOperPrevious
            // 
            this.btnOperPrevious.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnOperPrevious.Location = new System.Drawing.Point(480, 351);
            this.btnOperPrevious.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnOperPrevious.Name = "btnOperPrevious";
            this.btnOperPrevious.Size = new System.Drawing.Size(100, 27);
            this.btnOperPrevious.TabIndex = 10;
            this.btnOperPrevious.Text = "Previous";
            this.btnOperPrevious.UseVisualStyleBackColor = true;
            this.btnOperPrevious.Click += new System.EventHandler(this.btnOperPrevious_Click);
            // 
            // textBoxOperInfo
            // 
            this.textBoxOperInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.textBoxOperInfo.Location = new System.Drawing.Point(81, 405);
            this.textBoxOperInfo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxOperInfo.Name = "textBoxOperInfo";
            this.textBoxOperInfo.ReadOnly = true;
            this.textBoxOperInfo.Size = new System.Drawing.Size(605, 21);
            this.textBoxOperInfo.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label3.Location = new System.Drawing.Point(16, 405);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 16);
            this.label3.TabIndex = 12;
            this.label3.Text = "Info:";
            // 
            // OperLogDefineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 443);
            this.Controls.Add(this.textBoxOperInfo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnOperNext);
            this.Controls.Add(this.btnOperPrevious);
            this.Controls.Add(this.OperLogTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OperLogDate);
            this.Controls.Add(this.listViewOperLogDefine);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "OperLogDefineForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Content of Operation Log Entry";
            this.Load += new System.EventHandler(this.OperLogDefineForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewOperLogDefine;
        private System.Windows.Forms.TextBox OperLogDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox OperLogTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOperNext;
        private System.Windows.Forms.Button btnOperPrevious;
        private System.Windows.Forms.TextBox textBoxOperInfo;
        private System.Windows.Forms.Label label3;
    }
}