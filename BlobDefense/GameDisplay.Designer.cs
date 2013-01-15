namespace BlobDefense
{
    sealed partial class GameDisplay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameDisplay));
            this.PlayBtn = new System.Windows.Forms.Button();
            this.NameTxt = new System.Windows.Forms.TextBox();
            this.ContinueBtn = new System.Windows.Forms.Button();
            this.ToggleStoryBtn = new System.Windows.Forms.Button();
            this.StoryLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PlayBtn
            // 
            this.PlayBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayBtn.Location = new System.Drawing.Point(211, 74);
            this.PlayBtn.Name = "PlayBtn";
            this.PlayBtn.Size = new System.Drawing.Size(269, 95);
            this.PlayBtn.TabIndex = 0;
            this.PlayBtn.Text = "NEW  GAME";
            this.PlayBtn.UseVisualStyleBackColor = true;
            this.PlayBtn.Click += new System.EventHandler(this.PlayBtn_Click);
            // 
            // NameTxt
            // 
            this.NameTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameTxt.Location = new System.Drawing.Point(211, 324);
            this.NameTxt.Name = "NameTxt";
            this.NameTxt.Size = new System.Drawing.Size(269, 38);
            this.NameTxt.TabIndex = 1;
            this.NameTxt.TextChanged += new System.EventHandler(this.NameTxt_TextChanged);
            // 
            // ContinueBtn
            // 
            this.ContinueBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ContinueBtn.Location = new System.Drawing.Point(211, 193);
            this.ContinueBtn.Name = "ContinueBtn";
            this.ContinueBtn.Size = new System.Drawing.Size(269, 95);
            this.ContinueBtn.TabIndex = 2;
            this.ContinueBtn.Text = "CONTINUE";
            this.ContinueBtn.UseVisualStyleBackColor = true;
            this.ContinueBtn.Click += new System.EventHandler(this.ContinueBtn_Click);
            // 
            // ToggleStoryBtn
            // 
            this.ToggleStoryBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToggleStoryBtn.Location = new System.Drawing.Point(585, 394);
            this.ToggleStoryBtn.Name = "ToggleStoryBtn";
            this.ToggleStoryBtn.Size = new System.Drawing.Size(107, 35);
            this.ToggleStoryBtn.TabIndex = 3;
            this.ToggleStoryBtn.Text = "The Story";
            this.ToggleStoryBtn.UseVisualStyleBackColor = true;
            this.ToggleStoryBtn.Click += new System.EventHandler(this.ToggleStoryBtn_Click);
            // 
            // StoryLbl
            // 
            this.StoryLbl.AutoSize = true;
            this.StoryLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StoryLbl.ForeColor = System.Drawing.Color.White;
            this.StoryLbl.Location = new System.Drawing.Point(12, 39);
            this.StoryLbl.Name = "StoryLbl";
            this.StoryLbl.Size = new System.Drawing.Size(685, 323);
            this.StoryLbl.TabIndex = 4;
            this.StoryLbl.Text = resources.GetString("StoryLbl.Text");
            this.StoryLbl.Visible = false;
            // 
            // GameDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(704, 441);
            this.Controls.Add(this.ToggleStoryBtn);
            this.Controls.Add(this.ContinueBtn);
            this.Controls.Add(this.NameTxt);
            this.Controls.Add(this.PlayBtn);
            this.Controls.Add(this.StoryLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "GameDisplay";
            this.Text = "Blob Defense";
            this.Load += new System.EventHandler(this.GameDisplay_Load);
            this.Click += new System.EventHandler(this.GameDisplay_Click);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GameDisplay_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GameDisplay_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button PlayBtn;
        private System.Windows.Forms.TextBox NameTxt;
        private System.Windows.Forms.Button ContinueBtn;
        private System.Windows.Forms.Button ToggleStoryBtn;
        private System.Windows.Forms.Label StoryLbl;


    }
}

