﻿namespace EventHubExplorer.Forms
{
    partial class EventHubExplorer
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
            if(disposing && (components != null))
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
            tbxEventHubConnectionString = new TextBox();
            ssStatusBar = new StatusStrip();
            tblStatus = new ToolStripStatusLabel();
            dtvLog = new DataGridView();
            btnClear = new Button();
            btnRun = new Button();
            lblEventHubConnectionString = new Label();
            tbxEventHubName = new TextBox();
            label2 = new Label();
            tbxConsumerGroup = new TextBox();
            label3 = new Label();
            tbxEventModel = new TextBox();
            label4 = new Label();
            btnDisconnect = new Button();
            ssStatusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtvLog).BeginInit();
            SuspendLayout();
            // 
            // tbxEventHubConnectionString
            // 
            tbxEventHubConnectionString.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbxEventHubConnectionString.Font = new Font("Segoe UI",14F);
            tbxEventHubConnectionString.Location = new Point(196,14);
            tbxEventHubConnectionString.Multiline = true;
            tbxEventHubConnectionString.Name = "tbxEventHubConnectionString";
            tbxEventHubConnectionString.ScrollBars = ScrollBars.Vertical;
            tbxEventHubConnectionString.Size = new Size(684,95);
            tbxEventHubConnectionString.TabIndex = 1;
            // 
            // ssStatusBar
            // 
            ssStatusBar.Items.AddRange(new ToolStripItem[] { tblStatus });
            ssStatusBar.Location = new Point(0,759);
            ssStatusBar.Name = "ssStatusBar";
            ssStatusBar.Size = new Size(904,22);
            ssStatusBar.TabIndex = 3;
            ssStatusBar.Text = "statusStrip1";
            // 
            // tblStatus
            // 
            tblStatus.Name = "tblStatus";
            tblStatus.Size = new Size(104,17);
            tblStatus.Text = "Connection Status";
            // 
            // dtvLog
            // 
            dtvLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dtvLog.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dtvLog.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dtvLog.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtvLog.Location = new Point(0,517);
            dtvLog.Name = "dtvLog";
            dtvLog.Size = new Size(904,239);
            dtvLog.TabIndex = 4;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.FromArgb(128,255,255);
            btnClear.Font = new Font("Segoe UI",9F,FontStyle.Bold);
            btnClear.ForeColor = Color.Purple;
            btnClear.Location = new Point(784,468);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(96,43);
            btnClear.TabIndex = 5;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // btnRun
            // 
            btnRun.BackColor = Color.FromArgb(128,255,128);
            btnRun.Font = new Font("Segoe UI",10F,FontStyle.Bold);
            btnRun.Location = new Point(545,468);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(82,43);
            btnRun.TabIndex = 6;
            btnRun.Text = "Run";
            btnRun.UseVisualStyleBackColor = false;
            btnRun.Click += btnRun_Click;
            // 
            // lblEventHubConnectionString
            // 
            lblEventHubConnectionString.AutoSize = true;
            lblEventHubConnectionString.Font = new Font("Segoe UI",10F,FontStyle.Bold);
            lblEventHubConnectionString.Location = new Point(0,24);
            lblEventHubConnectionString.Name = "lblEventHubConnectionString";
            lblEventHubConnectionString.Size = new Size(179,19);
            lblEventHubConnectionString.TabIndex = 7;
            lblEventHubConnectionString.Text = "Event Hub Connect String";
            // 
            // tbxEventHubName
            // 
            tbxEventHubName.Font = new Font("Segoe UI",14F);
            tbxEventHubName.Location = new Point(196,115);
            tbxEventHubName.Multiline = true;
            tbxEventHubName.Name = "tbxEventHubName";
            tbxEventHubName.Size = new Size(260,43);
            tbxEventHubName.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI",10F,FontStyle.Bold);
            label2.Location = new Point(13,124);
            label2.Name = "label2";
            label2.Size = new Size(121,19);
            label2.TabIndex = 9;
            label2.Text = "Event Hub Name";
            // 
            // tbxConsumerGroup
            // 
            tbxConsumerGroup.Font = new Font("Segoe UI",14F);
            tbxConsumerGroup.Location = new Point(196,164);
            tbxConsumerGroup.Multiline = true;
            tbxConsumerGroup.Name = "tbxConsumerGroup";
            tbxConsumerGroup.Size = new Size(260,43);
            tbxConsumerGroup.TabIndex = 10;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI",10F,FontStyle.Bold);
            label3.Location = new Point(13,173);
            label3.Name = "label3";
            label3.Size = new Size(122,19);
            label3.TabIndex = 11;
            label3.Text = "Consumer Group";
            // 
            // tbxEventModel
            // 
            tbxEventModel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbxEventModel.Font = new Font("Segoe UI",14F);
            tbxEventModel.Location = new Point(196,213);
            tbxEventModel.Multiline = true;
            tbxEventModel.Name = "tbxEventModel";
            tbxEventModel.ScrollBars = ScrollBars.Both;
            tbxEventModel.Size = new Size(684,238);
            tbxEventModel.TabIndex = 12;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI",10F,FontStyle.Bold);
            label4.Location = new Point(12,311);
            label4.Name = "label4";
            label4.Size = new Size(92,19);
            label4.TabIndex = 13;
            label4.Text = "Event Model";
            // 
            // btnDisconnect
            // 
            btnDisconnect.BackColor = Color.Red;
            btnDisconnect.Font = new Font("Segoe UI",10F,FontStyle.Bold);
            btnDisconnect.ForeColor = SystemColors.Control;
            btnDisconnect.Location = new Point(633,468);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(145,43);
            btnDisconnect.TabIndex = 16;
            btnDisconnect.Text = "Disconnect";
            btnDisconnect.UseVisualStyleBackColor = false;
            btnDisconnect.Click += btnDisconnect_Click;
            // 
            // EventHubExplorer
            // 
            AutoScaleDimensions = new SizeF(7F,17F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(904,781);
            Controls.Add(btnDisconnect);
            Controls.Add(label4);
            Controls.Add(tbxEventModel);
            Controls.Add(label3);
            Controls.Add(tbxConsumerGroup);
            Controls.Add(label2);
            Controls.Add(tbxEventHubName);
            Controls.Add(lblEventHubConnectionString);
            Controls.Add(btnRun);
            Controls.Add(btnClear);
            Controls.Add(dtvLog);
            Controls.Add(ssStatusBar);
            Controls.Add(tbxEventHubConnectionString);
            Font = new Font("Segoe UI",10F);
            MaximumSize = new Size(920,820);
            Name = "EventHubExplorer";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Event Hub Explorer";
            ssStatusBar.ResumeLayout(false);
            ssStatusBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtvLog).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox tbxEventHubConnectionString;
        private StatusStrip ssStatusBar;
        private DataGridView dtvLog;
        private Button btnClear;
        private Button btnRun;
        private Label lblEventHubConnectionString;
        private TextBox tbxEventHubName;
        private Label label2;
        private TextBox tbxConsumerGroup;
        private Label label3;
        private TextBox tbxEventModel;
        private Label label4;
        private Button btnDisconnect;
        private ToolStripStatusLabel tblStatus;
    }
}
