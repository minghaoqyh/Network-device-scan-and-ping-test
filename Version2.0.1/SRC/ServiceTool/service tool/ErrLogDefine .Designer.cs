namespace service_tool
{
    partial class ErrLogDefineForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrLogDefineForm));
            this.btnErrPrevious = new System.Windows.Forms.Button();
            this.btnErrNext = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ErrLogDate = new System.Windows.Forms.TextBox();
            this.ErrLogTime = new System.Windows.Forms.TextBox();
            this.textBoxErrInfo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridViewDownload = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDownload)).BeginInit();
            this.SuspendLayout();
            // 
            // btnErrPrevious
            // 
            this.btnErrPrevious.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnErrPrevious.Location = new System.Drawing.Point(757, 352);
            this.btnErrPrevious.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnErrPrevious.Name = "btnErrPrevious";
            this.btnErrPrevious.Size = new System.Drawing.Size(100, 27);
            this.btnErrPrevious.TabIndex = 1;
            this.btnErrPrevious.Text = "Previous";
            this.btnErrPrevious.UseVisualStyleBackColor = true;
            this.btnErrPrevious.Click += new System.EventHandler(this.btnErrPrevious_Click);
            // 
            // btnErrNext
            // 
            this.btnErrNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnErrNext.Location = new System.Drawing.Point(896, 352);
            this.btnErrNext.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnErrNext.Name = "btnErrNext";
            this.btnErrNext.Size = new System.Drawing.Size(100, 27);
            this.btnErrNext.TabIndex = 2;
            this.btnErrNext.Text = "Next";
            this.btnErrNext.UseVisualStyleBackColor = true;
            this.btnErrNext.Click += new System.EventHandler(this.btnErrNext_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label1.Location = new System.Drawing.Point(9, 358);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Date:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label2.Location = new System.Drawing.Point(285, 358);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Time:";
            // 
            // ErrLogDate
            // 
            this.ErrLogDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ErrLogDate.Location = new System.Drawing.Point(65, 354);
            this.ErrLogDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ErrLogDate.Name = "ErrLogDate";
            this.ErrLogDate.Size = new System.Drawing.Size(132, 21);
            this.ErrLogDate.TabIndex = 5;
            // 
            // ErrLogTime
            // 
            this.ErrLogTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ErrLogTime.Location = new System.Drawing.Point(344, 354);
            this.ErrLogTime.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ErrLogTime.Name = "ErrLogTime";
            this.ErrLogTime.Size = new System.Drawing.Size(132, 21);
            this.ErrLogTime.TabIndex = 6;
            // 
            // textBoxErrInfo
            // 
            this.textBoxErrInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.textBoxErrInfo.Location = new System.Drawing.Point(65, 398);
            this.textBoxErrInfo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxErrInfo.Name = "textBoxErrInfo";
            this.textBoxErrInfo.ReadOnly = true;
            this.textBoxErrInfo.Size = new System.Drawing.Size(929, 21);
            this.textBoxErrInfo.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label3.Location = new System.Drawing.Point(12, 402);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Info:";
            // 
            // dataGridViewDownload
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewDownload.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewDownload.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewDownload.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewDownload.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewDownload.ColumnHeadersHeight = 30;
            this.dataGridViewDownload.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewDownload.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewDownload.EnableHeadersVisualStyles = false;
            this.dataGridViewDownload.Location = new System.Drawing.Point(16, 13);
            this.dataGridViewDownload.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridViewDownload.MultiSelect = false;
            this.dataGridViewDownload.Name = "dataGridViewDownload";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewDownload.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewDownload.RowHeadersVisible = false;
            this.dataGridViewDownload.RowHeadersWidth = 50;
            this.dataGridViewDownload.RowTemplate.Height = 126;
            this.dataGridViewDownload.RowTemplate.ReadOnly = true;
            this.dataGridViewDownload.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewDownload.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect;
            this.dataGridViewDownload.Size = new System.Drawing.Size(983, 329);
            this.dataGridViewDownload.TabIndex = 9;
            // 
            // Column1
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Column1.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column1.FillWeight = 42.96924F;
            this.Column1.HeaderText = "Error Code";
            this.Column1.MinimumWidth = 30;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Column2.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column2.FillWeight = 76.14214F;
            this.Column2.HeaderText = "Detail";
            this.Column2.MinimumWidth = 40;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column3.FillWeight = 180.8887F;
            this.Column3.HeaderText = "Maintenance Advice";
            this.Column3.MinimumWidth = 100;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ErrLogDefineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 441);
            this.Controls.Add(this.dataGridViewDownload);
            this.Controls.Add(this.textBoxErrInfo);
            this.Controls.Add(this.ErrLogTime);
            this.Controls.Add(this.ErrLogDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnErrNext);
            this.Controls.Add(this.btnErrPrevious);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ErrLogDefineForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Content of Error Log Entry";
            this.Load += new System.EventHandler(this.LogDefine_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDownload)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnErrPrevious;
        private System.Windows.Forms.Button btnErrNext;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ErrLogDate;
        private System.Windows.Forms.TextBox ErrLogTime;
        private System.Windows.Forms.TextBox textBoxErrInfo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridViewDownload;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}