namespace Morris
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
            btnPlayerVsPlayer = new Button();
            btnPlayerVsAI = new Button();
            btnAIVsAI = new Button();
            SuspendLayout();
            // 
            // btnPlayerVsPlayer
            // 
            btnPlayerVsPlayer.BackColor = Color.AliceBlue;
            btnPlayerVsPlayer.Location = new Point(385, 128);
            btnPlayerVsPlayer.Margin = new Padding(3, 4, 3, 4);
            btnPlayerVsPlayer.Name = "btnPlayerVsPlayer";
            btnPlayerVsPlayer.Size = new Size(86, 31);
            btnPlayerVsPlayer.TabIndex = 0;
            btnPlayerVsPlayer.Text = "Player vs Player";
            btnPlayerVsPlayer.UseVisualStyleBackColor = true;
            btnPlayerVsPlayer.Click += btnPlayerVsPlayer_Click;
            // 
            // btnPlayerVsAI
            // 
            btnPlayerVsAI.BackColor = Color.LavenderBlush;
            btnPlayerVsAI.Location = new Point(385, 231);
            btnPlayerVsAI.Margin = new Padding(3, 4, 3, 4);
            btnPlayerVsAI.Name = "btnPlayerVsAI";
            btnPlayerVsAI.Size = new Size(86, 31);
            btnPlayerVsAI.TabIndex = 1;
            btnPlayerVsAI.Text = "Player vs AI";
            btnPlayerVsAI.UseVisualStyleBackColor = true;
            btnPlayerVsAI.Click += btnPlayerVsAI_Click;
            // 
            // btnAIVsAI
            // 
            btnAIVsAI.BackColor = Color.Honeydew;
            btnAIVsAI.Location = new Point(385, 348);
            btnAIVsAI.Margin = new Padding(3, 4, 3, 4);
            btnAIVsAI.Name = "btnAIVsAI";
            btnAIVsAI.Size = new Size(86, 31);
            btnAIVsAI.TabIndex = 2;
            btnAIVsAI.Text = "AI vs AI";
            btnAIVsAI.UseVisualStyleBackColor = true;
            btnAIVsAI.Click += btnAIVsAI_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(btnAIVsAI);
            Controls.Add(btnPlayerVsAI);
            Controls.Add(btnPlayerVsPlayer);
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainForm";
            Text = "9 Men Morris";
            Load += MainForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnPlayerVsPlayer;
        private Button btnPlayerVsAI;
        private Button btnAIVsAI;
    }
}