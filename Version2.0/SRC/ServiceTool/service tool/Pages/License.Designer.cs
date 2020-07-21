namespace service_tool
{
    partial class License
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DgvLocalLicense = new System.Windows.Forms.DataGridView();
            this.tbLocalClassroom = new System.Windows.Forms.TextBox();
            this.btnServer = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.tbFile = new System.Windows.Forms.TextBox();
            this.lblNew6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DgvSystemLicense = new System.Windows.Forms.DataGridView();
            this.tbBoardClassroom = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdvanced = new System.Windows.Forms.Button();
            this.btnGet = new System.Windows.Forms.Button();
            this.tbPrograss = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label7 = new System.Windows.Forms.Label();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvLocalLicense)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvSystemLicense)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DgvLocalLicense);
            this.groupBox1.Controls.Add(this.tbLocalClassroom);
            this.groupBox1.Controls.Add(this.btnServer);
            this.groupBox1.Controls.Add(this.btnUpdate);
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Controls.Add(this.tbFile);
            this.groupBox1.Controls.Add(this.lblNew6);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(7, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(346, 337);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Local License File Feature List";
            // 
            // DgvLocalLicense
            // 
            this.DgvLocalLicense.AllowUserToDeleteRows = false;
            this.DgvLocalLicense.AllowUserToResizeRows = false;
            this.DgvLocalLicense.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvLocalLicense.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Category,
            this.name,
            this.sex,
            this.Column1});
            this.DgvLocalLicense.Location = new System.Drawing.Point(5, 40);
            this.DgvLocalLicense.Name = "DgvLocalLicense";
            this.DgvLocalLicense.ReadOnly = true;
            this.DgvLocalLicense.RowHeadersVisible = false;
            this.DgvLocalLicense.RowTemplate.Height = 23;
            this.DgvLocalLicense.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.DgvLocalLicense.Size = new System.Drawing.Size(335, 187);
            this.DgvLocalLicense.TabIndex = 21;
            this.DgvLocalLicense.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DgvLocalLicense_CellPainting);
            // 
            // tbLocalClassroom
            // 
            this.tbLocalClassroom.Location = new System.Drawing.Point(141, 224);
            this.tbLocalClassroom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbLocalClassroom.Name = "tbLocalClassroom";
            this.tbLocalClassroom.ReadOnly = true;
            this.tbLocalClassroom.Size = new System.Drawing.Size(67, 21);
            this.tbLocalClassroom.TabIndex = 12;
            this.tbLocalClassroom.Visible = false;
            // 
            // btnServer
            // 
            this.btnServer.Location = new System.Drawing.Point(103, 294);
            this.btnServer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnServer.Name = "btnServer";
            this.btnServer.Size = new System.Drawing.Size(155, 28);
            this.btnServer.TabIndex = 20;
            this.btnServer.Text = "Download From Server";
            this.btnServer.UseVisualStyleBackColor = true;
            this.btnServer.Click += new System.EventHandler(this.btnServer_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Location = new System.Drawing.Point(269, 251);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(70, 71);
            this.btnUpdate.TabIndex = 3;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(222, 251);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(36, 25);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "...";
            this.btnBrowse.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // tbFile
            // 
            this.tbFile.Location = new System.Drawing.Point(7, 252);
            this.tbFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbFile.Name = "tbFile";
            this.tbFile.ReadOnly = true;
            this.tbFile.Size = new System.Drawing.Size(201, 21);
            this.tbFile.TabIndex = 0;
            // 
            // lblNew6
            // 
            this.lblNew6.AutoSize = true;
            this.lblNew6.Location = new System.Drawing.Point(9, 226);
            this.lblNew6.Name = "lblNew6";
            this.lblNew6.Size = new System.Drawing.Size(137, 16);
            this.lblNew6.TabIndex = 9;
            this.lblNew6.Text = "Classroom Streaming";
            this.lblNew6.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DgvSystemLicense);
            this.groupBox2.Controls.Add(this.tbBoardClassroom);
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnAdvanced);
            this.groupBox2.Controls.Add(this.btnGet);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(362, 13);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(346, 337);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "System License File Feature List";
            // 
            // DgvSystemLicense
            // 
            this.DgvSystemLicense.AllowUserToDeleteRows = false;
            this.DgvSystemLicense.AllowUserToResizeRows = false;
            this.DgvSystemLicense.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvSystemLicense.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.DgvSystemLicense.Location = new System.Drawing.Point(5, 40);
            this.DgvSystemLicense.Name = "DgvSystemLicense";
            this.DgvSystemLicense.ReadOnly = true;
            this.DgvSystemLicense.RowHeadersVisible = false;
            this.DgvSystemLicense.RowTemplate.Height = 23;
            this.DgvSystemLicense.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.DgvSystemLicense.Size = new System.Drawing.Size(335, 187);
            this.DgvSystemLicense.TabIndex = 23;
            this.DgvSystemLicense.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DgvLocalLicense_CellPainting);
            // 
            // tbBoardClassroom
            // 
            this.tbBoardClassroom.Location = new System.Drawing.Point(138, 224);
            this.tbBoardClassroom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbBoardClassroom.Name = "tbBoardClassroom";
            this.tbBoardClassroom.ReadOnly = true;
            this.tbBoardClassroom.Size = new System.Drawing.Size(67, 21);
            this.tbBoardClassroom.TabIndex = 12;
            this.tbBoardClassroom.Visible = false;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(194, 290);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(145, 28);
            this.btnDelete.TabIndex = 21;
            this.btnDelete.Text = "Delete License";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 226);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Classroom Streaming";
            this.label1.Visible = false;
            // 
            // btnAdvanced
            // 
            this.btnAdvanced.Location = new System.Drawing.Point(230, 220);
            this.btnAdvanced.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAdvanced.Name = "btnAdvanced";
            this.btnAdvanced.Size = new System.Drawing.Size(99, 25);
            this.btnAdvanced.TabIndex = 20;
            this.btnAdvanced.Text = "Advanced...";
            this.btnAdvanced.UseVisualStyleBackColor = true;
            this.btnAdvanced.Visible = false;
            this.btnAdvanced.Click += new System.EventHandler(this.btnAdvanced_Click);
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(194, 249);
            this.btnGet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(145, 28);
            this.btnGet.TabIndex = 3;
            this.btnGet.Text = "Get";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // tbPrograss
            // 
            this.tbPrograss.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.tbPrograss.Location = new System.Drawing.Point(70, 372);
            this.tbPrograss.Margin = new System.Windows.Forms.Padding(0);
            this.tbPrograss.Name = "tbPrograss";
            this.tbPrograss.ReadOnly = true;
            this.tbPrograss.Size = new System.Drawing.Size(638, 21);
            this.tbPrograss.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "|*.bin";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 376);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 16);
            this.label7.TabIndex = 3;
            this.label7.Text = "Info:";
            // 
            // Category
            // 
            this.Category.DataPropertyName = "Category";
            this.Category.HeaderText = "Category";
            this.Category.Name = "Category";
            this.Category.ReadOnly = true;
            this.Category.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Category.Width = 68;
            // 
            // name
            // 
            this.name.DataPropertyName = "ModeName";
            this.name.HeaderText = "Feature Name";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.name.Width = 122;
            // 
            // sex
            // 
            this.sex.DataPropertyName = "ExpirationDate";
            this.sex.HeaderText = "Expiration Date";
            this.sex.Name = "sex";
            this.sex.ReadOnly = true;
            this.sex.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.sex.Width = 97;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Status";
            this.Column1.HeaderText = "Status";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 45;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Category";
            this.dataGridViewTextBoxColumn1.HeaderText = "Category";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 68;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "FeatureName";
            this.dataGridViewTextBoxColumn2.HeaderText = "Feature Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 122;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "ExpirationDate";
            this.dataGridViewTextBoxColumn3.HeaderText = "Expiration Date";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 97;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Mode";
            this.dataGridViewTextBoxColumn4.HeaderText = "Status";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 45;
            // 
            // License
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbPrograss);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "License";
            this.Size = new System.Drawing.Size(716, 420);
            this.Load += new System.EventHandler(this.License_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvLocalLicense)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvSystemLicense)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox tbFile;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label lblNew6;
        private System.Windows.Forms.TextBox tbLocalClassroom;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbBoardClassroom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.Button btnAdvanced;
        private System.Windows.Forms.TextBox tbPrograss;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnServer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView DgvLocalLicense;
        private System.Windows.Forms.DataGridView DgvSystemLicense;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn sex;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}
