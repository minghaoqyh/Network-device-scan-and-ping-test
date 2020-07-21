namespace service_tool
{
    partial class LogFiles
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
        public void InitializeComponent()
        {
            this.label16 = new System.Windows.Forms.Label();
            this.groupBoxErr = new System.Windows.Forms.GroupBox();
            this.btnErrImport = new System.Windows.Forms.Button();
            this.btnErrLast = new System.Windows.Forms.Button();
            this.btnErrExport = new System.Windows.Forms.Button();
            this.btnErrFirst = new System.Windows.Forms.Button();
            this.textBoxErrCount = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.btnErrClean = new System.Windows.Forms.Button();
            this.listViewErr = new System.Windows.Forms.ListView();
            this.btnErrForward = new System.Windows.Forms.Button();
            this.btnErrBack = new System.Windows.Forms.Button();
            this.textBoxLogInfo = new System.Windows.Forms.TextBox();
            this.groupBoxOper = new System.Windows.Forms.GroupBox();
            this.btnOperImport = new System.Windows.Forms.Button();
            this.btnOperExport = new System.Windows.Forms.Button();
            this.btnOperLast = new System.Windows.Forms.Button();
            this.btnOperFirst = new System.Windows.Forms.Button();
            this.textBoxOperCount = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.btnOperClean = new System.Windows.Forms.Button();
            this.listViewOper = new System.Windows.Forms.ListView();
            this.btnOperForward = new System.Windows.Forms.Button();
            this.btnOperBack = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.license_log= new service_tool.License();
            this.groupBoxErr.SuspendLayout();
            this.groupBoxOper.SuspendLayout();
            this.SuspendLayout();
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label16.Location = new System.Drawing.Point(26, 449);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(30, 15);
            this.label16.TabIndex = 21;
            this.label16.Text = "Info:";
            // 
            // groupBoxErr
            // 
            this.groupBoxErr.Controls.Add(this.btnErrImport);
            this.groupBoxErr.Controls.Add(this.btnErrLast);
            this.groupBoxErr.Controls.Add(this.btnErrExport);
            this.groupBoxErr.Controls.Add(this.btnErrFirst);
            this.groupBoxErr.Controls.Add(this.textBoxErrCount);
            this.groupBoxErr.Controls.Add(this.label18);
            this.groupBoxErr.Controls.Add(this.btnErrClean);
            this.groupBoxErr.Controls.Add(this.listViewErr);
            this.groupBoxErr.Controls.Add(this.btnErrForward);
            this.groupBoxErr.Controls.Add(this.btnErrBack);
            this.groupBoxErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.groupBoxErr.Location = new System.Drawing.Point(444, 7);
            this.groupBoxErr.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxErr.Name = "groupBoxErr";
            this.groupBoxErr.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxErr.Size = new System.Drawing.Size(429, 420);
            this.groupBoxErr.TabIndex = 18;
            this.groupBoxErr.TabStop = false;
            this.groupBoxErr.Text = "Error Log";
            // 
            // btnErrImport
            // 
            this.btnErrImport.Location = new System.Drawing.Point(352, 54);
            this.btnErrImport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnErrImport.Name = "btnErrImport";
            this.btnErrImport.Size = new System.Drawing.Size(70, 29);
            this.btnErrImport.TabIndex = 22;
            this.btnErrImport.Text = "Import";
            this.btnErrImport.UseVisualStyleBackColor = true;
            this.btnErrImport.Click += new System.EventHandler(this.btnErrImport_Click);
            // 
            // btnErrLast
            // 
            this.btnErrLast.Location = new System.Drawing.Point(256, 375);
            this.btnErrLast.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnErrLast.Name = "btnErrLast";
            this.btnErrLast.Size = new System.Drawing.Size(70, 29);
            this.btnErrLast.TabIndex = 23;
            this.btnErrLast.Text = "Last";
            this.btnErrLast.UseVisualStyleBackColor = true;
            this.btnErrLast.Click += new System.EventHandler(this.btnErrLast_Click);
            // 
            // btnErrExport
            // 
            this.btnErrExport.Location = new System.Drawing.Point(352, 19);
            this.btnErrExport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnErrExport.Name = "btnErrExport";
            this.btnErrExport.Size = new System.Drawing.Size(70, 29);
            this.btnErrExport.TabIndex = 22;
            this.btnErrExport.Text = "Export";
            this.btnErrExport.UseVisualStyleBackColor = true;
            this.btnErrExport.Click += new System.EventHandler(this.btnErrExport_Click);
            // 
            // btnErrFirst
            // 
            this.btnErrFirst.Location = new System.Drawing.Point(21, 375);
            this.btnErrFirst.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnErrFirst.Name = "btnErrFirst";
            this.btnErrFirst.Size = new System.Drawing.Size(70, 29);
            this.btnErrFirst.TabIndex = 22;
            this.btnErrFirst.Text = "First";
            this.btnErrFirst.UseVisualStyleBackColor = true;
            this.btnErrFirst.Click += new System.EventHandler(this.btnErrFirst_Click);
            // 
            // textBoxErrCount
            // 
            this.textBoxErrCount.Location = new System.Drawing.Point(220, 35);
            this.textBoxErrCount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxErrCount.Name = "textBoxErrCount";
            this.textBoxErrCount.ReadOnly = true;
            this.textBoxErrCount.Size = new System.Drawing.Size(68, 21);
            this.textBoxErrCount.TabIndex = 21;
            this.textBoxErrCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label18.Location = new System.Drawing.Point(9, 37);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(255, 22);
            this.label18.TabIndex = 20;
            this.label18.Text = "Number of Error Log Entries:";
            // 
            // btnErrClean
            // 
            this.btnErrClean.Location = new System.Drawing.Point(338, 375);
            this.btnErrClean.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnErrClean.Name = "btnErrClean";
            this.btnErrClean.Size = new System.Drawing.Size(70, 29);
            this.btnErrClean.TabIndex = 0;
            this.btnErrClean.Text = "Clear";
            this.btnErrClean.UseVisualStyleBackColor = true;
            this.btnErrClean.Click += new System.EventHandler(this.btnErrClean_Click);
            // 
            // listViewErr
            // 
            this.listViewErr.FullRowSelect = true;
            this.listViewErr.Location = new System.Drawing.Point(6, 90);
            this.listViewErr.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listViewErr.Name = "listViewErr";
            this.listViewErr.Size = new System.Drawing.Size(415, 268);
            this.listViewErr.TabIndex = 0;
            this.listViewErr.UseCompatibleStateImageBehavior = false;
            this.listViewErr.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.listViewErr_ColumnWidthChanging);
            this.listViewErr.DoubleClick += new System.EventHandler(this.listViewErr_DoubleClick);
            // 
            // btnErrForward
            // 
            this.btnErrForward.Location = new System.Drawing.Point(100, 375);
            this.btnErrForward.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnErrForward.Name = "btnErrForward";
            this.btnErrForward.Size = new System.Drawing.Size(70, 29);
            this.btnErrForward.TabIndex = 0;
            this.btnErrForward.Text = "<<";
            this.btnErrForward.UseVisualStyleBackColor = true;
            this.btnErrForward.Click += new System.EventHandler(this.btnErrForward_Click);
            // 
            // btnErrBack
            // 
            this.btnErrBack.Location = new System.Drawing.Point(178, 375);
            this.btnErrBack.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnErrBack.Name = "btnErrBack";
            this.btnErrBack.Size = new System.Drawing.Size(70, 29);
            this.btnErrBack.TabIndex = 0;
            this.btnErrBack.Text = ">>";
            this.btnErrBack.UseVisualStyleBackColor = true;
            this.btnErrBack.Click += new System.EventHandler(this.btnErrBack_Click);
            // 
            // textBoxLogInfo
            // 
            this.textBoxLogInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.textBoxLogInfo.Location = new System.Drawing.Point(81, 446);
            this.textBoxLogInfo.Name = "textBoxLogInfo";
            this.textBoxLogInfo.ReadOnly = true;
            this.textBoxLogInfo.Size = new System.Drawing.Size(787, 21);
            this.textBoxLogInfo.TabIndex = 20;
            // 
            // groupBoxOper
            // 
            this.groupBoxOper.Controls.Add(this.btnOperImport);
            this.groupBoxOper.Controls.Add(this.btnOperExport);
            this.groupBoxOper.Controls.Add(this.btnOperLast);
            this.groupBoxOper.Controls.Add(this.btnOperFirst);
            this.groupBoxOper.Controls.Add(this.textBoxOperCount);
            this.groupBoxOper.Controls.Add(this.label17);
            this.groupBoxOper.Controls.Add(this.btnOperClean);
            this.groupBoxOper.Controls.Add(this.listViewOper);
            this.groupBoxOper.Controls.Add(this.btnOperForward);
            this.groupBoxOper.Controls.Add(this.btnOperBack);
            this.groupBoxOper.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.groupBoxOper.Location = new System.Drawing.Point(0, 7);
            this.groupBoxOper.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxOper.Name = "groupBoxOper";
            this.groupBoxOper.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxOper.Size = new System.Drawing.Size(430, 420);
            this.groupBoxOper.TabIndex = 19;
            this.groupBoxOper.TabStop = false;
            this.groupBoxOper.Text = "Operation Log";
            // 
            // btnOperImport
            // 
            this.btnOperImport.Location = new System.Drawing.Point(354, 54);
            this.btnOperImport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOperImport.Name = "btnOperImport";
            this.btnOperImport.Size = new System.Drawing.Size(70, 29);
            this.btnOperImport.TabIndex = 22;
            this.btnOperImport.Text = "Import";
            this.btnOperImport.UseVisualStyleBackColor = true;
            this.btnOperImport.Click += new System.EventHandler(this.btnOperImport_Click);
            // 
            // btnOperExport
            // 
            this.btnOperExport.Location = new System.Drawing.Point(354, 19);
            this.btnOperExport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOperExport.Name = "btnOperExport";
            this.btnOperExport.Size = new System.Drawing.Size(70, 29);
            this.btnOperExport.TabIndex = 22;
            this.btnOperExport.Text = "Export";
            this.btnOperExport.UseVisualStyleBackColor = true;
            this.btnOperExport.Click += new System.EventHandler(this.btnOperExport_Click);
            // 
            // btnOperLast
            // 
            this.btnOperLast.Location = new System.Drawing.Point(258, 375);
            this.btnOperLast.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOperLast.Name = "btnOperLast";
            this.btnOperLast.Size = new System.Drawing.Size(70, 29);
            this.btnOperLast.TabIndex = 21;
            this.btnOperLast.Text = "Last";
            this.btnOperLast.UseVisualStyleBackColor = true;
            this.btnOperLast.Click += new System.EventHandler(this.btnOperLast_Click);
            // 
            // btnOperFirst
            // 
            this.btnOperFirst.Location = new System.Drawing.Point(18, 375);
            this.btnOperFirst.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOperFirst.Name = "btnOperFirst";
            this.btnOperFirst.Size = new System.Drawing.Size(70, 29);
            this.btnOperFirst.TabIndex = 20;
            this.btnOperFirst.Text = "First";
            this.btnOperFirst.UseVisualStyleBackColor = true;
            this.btnOperFirst.Click += new System.EventHandler(this.btnOperFirst_Click);
            // 
            // textBoxOperCount
            // 
            this.textBoxOperCount.Location = new System.Drawing.Point(255, 35);
            this.textBoxOperCount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxOperCount.Name = "textBoxOperCount";
            this.textBoxOperCount.ReadOnly = true;
            this.textBoxOperCount.Size = new System.Drawing.Size(68, 21);
            this.textBoxOperCount.TabIndex = 19;
            this.textBoxOperCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label17.Location = new System.Drawing.Point(9, 37);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(285, 22);
            this.label17.TabIndex = 18;
            this.label17.Text = "Number of Operation Log Entries:";
            // 
            // btnOperClean
            // 
            this.btnOperClean.Location = new System.Drawing.Point(339, 375);
            this.btnOperClean.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOperClean.Name = "btnOperClean";
            this.btnOperClean.Size = new System.Drawing.Size(70, 29);
            this.btnOperClean.TabIndex = 1;
            this.btnOperClean.Text = "Clear";
            this.btnOperClean.UseVisualStyleBackColor = true;
            this.btnOperClean.Click += new System.EventHandler(this.btnOperClean_Click);
            // 
            // listViewOper
            // 
            this.listViewOper.FullRowSelect = true;
            this.listViewOper.GridLines = true;
            this.listViewOper.Location = new System.Drawing.Point(6, 90);
            this.listViewOper.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listViewOper.Name = "listViewOper";
            this.listViewOper.Size = new System.Drawing.Size(416, 268);
            this.listViewOper.TabIndex = 0;
            this.listViewOper.UseCompatibleStateImageBehavior = false;
            this.listViewOper.View = System.Windows.Forms.View.Details;
            this.listViewOper.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.listViewOper_ColumnWidthChanging);
            this.listViewOper.DoubleClick += new System.EventHandler(this.listViewOper_DoubleClick);
            // 
            // btnOperForward
            // 
            this.btnOperForward.Location = new System.Drawing.Point(98, 375);
            this.btnOperForward.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOperForward.Name = "btnOperForward";
            this.btnOperForward.Size = new System.Drawing.Size(70, 29);
            this.btnOperForward.TabIndex = 2;
            this.btnOperForward.Text = "<<";
            this.btnOperForward.UseVisualStyleBackColor = true;
            this.btnOperForward.Click += new System.EventHandler(this.btnOperForward_Click);
            // 
            // btnOperBack
            // 
            this.btnOperBack.Location = new System.Drawing.Point(176, 375);
            this.btnOperBack.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOperBack.Name = "btnOperBack";
            this.btnOperBack.Size = new System.Drawing.Size(70, 29);
            this.btnOperBack.TabIndex = 3;
            this.btnOperBack.Text = ">>";
            this.btnOperBack.UseVisualStyleBackColor = true;
            this.btnOperBack.Click += new System.EventHandler(this.btnOperBack_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // LogFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label16);
            this.Controls.Add(this.groupBoxErr);
            this.Controls.Add(this.textBoxLogInfo);
            this.Controls.Add(this.groupBoxOper);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "LogFiles";
            this.Size = new System.Drawing.Size(892, 497);
            this.groupBoxErr.ResumeLayout(false);
            this.groupBoxErr.PerformLayout();
            this.groupBoxOper.ResumeLayout(false);
            this.groupBoxOper.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBoxErr;
        private System.Windows.Forms.TextBox textBoxErrCount;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnErrClean;
        private System.Windows.Forms.Button btnErrForward;
        private System.Windows.Forms.Button btnErrBack;
        private System.Windows.Forms.TextBox textBoxLogInfo;
        private System.Windows.Forms.GroupBox groupBoxOper;
        private System.Windows.Forms.TextBox textBoxOperCount;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnOperClean;
        private System.Windows.Forms.ListView listViewOper;
        private System.Windows.Forms.Button btnOperForward;
        private System.Windows.Forms.Button btnOperBack;
        private System.Windows.Forms.ListView listViewErr;
        private System.Windows.Forms.Button btnErrLast;
        private System.Windows.Forms.Button btnErrFirst;
        private System.Windows.Forms.Button btnOperLast;
        private System.Windows.Forms.Button btnOperFirst;
        private System.Windows.Forms.Button btnOperExport;
        private System.Windows.Forms.Button btnOperImport;
        private System.Windows.Forms.Button btnErrImport;
        private System.Windows.Forms.Button btnErrExport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private License license_log ;
    }
}
