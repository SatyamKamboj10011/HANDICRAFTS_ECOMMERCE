namespace Form_Loading {
    partial class FormMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.panelCard = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelSubtitle = new System.Windows.Forms.Label();
            this.labelUsername = new System.Windows.Forms.Label();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.labelNoAccount = new System.Windows.Forms.Label();
            this.buttonSignUp = new System.Windows.Forms.Button();
            this.labelGuest = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            //
            // panelCard
            //
            this.panelCard.BackColor = System.Drawing.Color.White;
            this.panelCard.Controls.Add(this.labelTitle);
            this.panelCard.Controls.Add(this.labelSubtitle);
            this.panelCard.Controls.Add(this.labelUsername);
            this.panelCard.Controls.Add(this.textBoxUsername);
            this.panelCard.Controls.Add(this.labelPassword);
            this.panelCard.Controls.Add(this.textBoxPassword);
            this.panelCard.Controls.Add(this.buttonLogin);
            this.panelCard.Controls.Add(this.labelNoAccount);
            this.panelCard.Controls.Add(this.buttonSignUp);
            this.panelCard.Controls.Add(this.labelGuest);
            this.panelCard.Location = new System.Drawing.Point(640, 44);
            this.panelCard.Name = "panelCard";
            this.panelCard.Padding = new System.Windows.Forms.Padding(32);
            this.panelCard.Size = new System.Drawing.Size(380, 440);
            this.panelCard.TabIndex = 0;
            //
            // labelTitle
            //
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.labelTitle.Location = new System.Drawing.Point(30, 30);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.TabIndex = 10;
            this.labelTitle.Text = "Welcome back";
            //
            // labelSubtitle
            //
            this.labelSubtitle.AutoSize = true;
            this.labelSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
            this.labelSubtitle.Location = new System.Drawing.Point(33, 76);
            this.labelSubtitle.Name = "labelSubtitle";
            this.labelSubtitle.TabIndex = 11;
            this.labelSubtitle.Text = "Sign in to Handcraft Heaven";
            //
            // labelUsername
            //
            this.labelUsername.AutoSize = true;
            this.labelUsername.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelUsername.Location = new System.Drawing.Point(33, 118);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.TabIndex = 12;
            this.labelUsername.Text = "Username or email";
            //
            // textBoxUsername
            //
            this.textBoxUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxUsername.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUsername.Location = new System.Drawing.Point(36, 142);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(308, 34);
            this.textBoxUsername.TabIndex = 1;
            //
            // labelPassword
            //
            this.labelPassword.AutoSize = true;
            this.labelPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelPassword.Location = new System.Drawing.Point(33, 190);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.TabIndex = 13;
            this.labelPassword.Text = "Password";
            //
            // textBoxPassword
            //
            this.textBoxPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPassword.Location = new System.Drawing.Point(36, 214);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(308, 34);
            this.textBoxPassword.TabIndex = 2;
            this.textBoxPassword.UseSystemPasswordChar = true;
            //
            // buttonLogin
            //
            this.buttonLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(157)))), ((int)(((byte)(143)))));
            this.buttonLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonLogin.FlatAppearance.BorderSize = 0;
            this.buttonLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(128)))), ((int)(((byte)(116)))));
            this.buttonLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLogin.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLogin.ForeColor = System.Drawing.Color.White;
            this.buttonLogin.Location = new System.Drawing.Point(36, 270);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(308, 44);
            this.buttonLogin.TabIndex = 3;
            this.buttonLogin.Text = "Sign In";
            this.buttonLogin.UseVisualStyleBackColor = false;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            //
            // labelNoAccount
            //
            this.labelNoAccount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNoAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
            this.labelNoAccount.Location = new System.Drawing.Point(36, 326);
            this.labelNoAccount.Name = "labelNoAccount";
            this.labelNoAccount.Size = new System.Drawing.Size(308, 20);
            this.labelNoAccount.TabIndex = 14;
            this.labelNoAccount.Text = "New here?";
            this.labelNoAccount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // buttonSignUp
            //
            this.buttonSignUp.BackColor = System.Drawing.Color.White;
            this.buttonSignUp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSignUp.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(157)))), ((int)(((byte)(143)))));
            this.buttonSignUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(245)))), ((int)(((byte)(243)))));
            this.buttonSignUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSignUp.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSignUp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(157)))), ((int)(((byte)(143)))));
            this.buttonSignUp.Location = new System.Drawing.Point(36, 350);
            this.buttonSignUp.Name = "buttonSignUp";
            this.buttonSignUp.Size = new System.Drawing.Size(308, 44);
            this.buttonSignUp.TabIndex = 4;
            this.buttonSignUp.Text = "Create an account";
            this.buttonSignUp.UseVisualStyleBackColor = false;
            this.buttonSignUp.Click += new System.EventHandler(this.buttonSignUp_Click);
            //
            // labelGuest
            //
            this.labelGuest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelGuest.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGuest.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
            this.labelGuest.Location = new System.Drawing.Point(36, 404);
            this.labelGuest.Name = "labelGuest";
            this.labelGuest.Size = new System.Drawing.Size(308, 20);
            this.labelGuest.TabIndex = 5;
            this.labelGuest.Text = "Continue as guest";
            this.labelGuest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelGuest.Click += new System.EventHandler(this.labelGuest_Click);
            //
            // pictureBox1
            //
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Form_Loading.Properties.Resources.a_visually_appealing_and_meaningful_logo_for_handc_oaqGsI5nTViMB4uBa5oiKw_Bd6HOpCKT8eKPaxM6sPAVA_removebg_preview;
            this.pictureBox1.Location = new System.Drawing.Point(20, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(96, 82);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            //
            // FormMain
            //
            this.AcceptButton = this.buttonLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Form_Loading.Properties.Resources.IMG_20240824_WA0020_2;
            this.ClientSize = new System.Drawing.Size(1069, 528);
            this.Controls.Add(this.panelCard);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sign In - Handcraft Heaven";
            this.panelCard.ResumeLayout(false);
            this.panelCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelCard;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelSubtitle;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Label labelNoAccount;
        private System.Windows.Forms.Button buttonSignUp;
        private System.Windows.Forms.Label labelGuest;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
