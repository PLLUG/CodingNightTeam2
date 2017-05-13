namespace BotTest1
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.tbTimer = new System.Windows.Forms.Timer(this.components);
            this.tokenLabel = new System.Windows.Forms.Label();
            this.tokenTextBox = new System.Windows.Forms.TextBox();
            this.tokenButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // messageBox
            // 
            this.messageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageBox.Location = new System.Drawing.Point(13, 45);
            this.messageBox.Multiline = true;
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(538, 229);
            this.messageBox.TabIndex = 0;
            // 
            // tbTimer
            // 
            this.tbTimer.Tick += new System.EventHandler(this.tbTimer_Tick);
            // 
            // tokenLabel
            // 
            this.tokenLabel.AutoSize = true;
            this.tokenLabel.Location = new System.Drawing.Point(13, 13);
            this.tokenLabel.Name = "tokenLabel";
            this.tokenLabel.Size = new System.Drawing.Size(94, 15);
            this.tokenLabel.TabIndex = 1;
            this.tokenLabel.Text = "Telegram bot ID:";
            // 
            // tokenTextBox
            // 
            this.tokenTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tokenTextBox.Location = new System.Drawing.Point(113, 10);
            this.tokenTextBox.Name = "tokenTextBox";
            this.tokenTextBox.Size = new System.Drawing.Size(348, 23);
            this.tokenTextBox.TabIndex = 2;
            // 
            // tokenButton
            // 
            this.tokenButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tokenButton.Location = new System.Drawing.Point(476, 10);
            this.tokenButton.Name = "tokenButton";
            this.tokenButton.Size = new System.Drawing.Size(75, 23);
            this.tokenButton.TabIndex = 3;
            this.tokenButton.Text = "Launch";
            this.tokenButton.UseVisualStyleBackColor = true;
            this.tokenButton.Click += new System.EventHandler(this.tokenButton_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 302);
            this.Controls.Add(this.tokenButton);
            this.Controls.Add(this.tokenTextBox);
            this.Controls.Add(this.tokenLabel);
            this.Controls.Add(this.messageBox);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "Main";
            this.Text = "BotTest";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox messageBox;
        private System.Windows.Forms.Timer tbTimer;
        private System.Windows.Forms.Label tokenLabel;
        private System.Windows.Forms.TextBox tokenTextBox;
        private System.Windows.Forms.Button tokenButton;
    }
}

