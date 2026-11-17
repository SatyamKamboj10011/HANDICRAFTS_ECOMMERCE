namespace Form_Loading
{
    partial class FormJewellery
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
            this.navigation1 = new Form_Loading.Navigation();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelSubtitle = new System.Windows.Forms.Label();
            this.flowProducts = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            //
            // navigation1
            //
            this.navigation1.Location = new System.Drawing.Point(-3, -1);
            this.navigation1.Name = "navigation1";
            this.navigation1.Size = new System.Drawing.Size(228, 575);
            this.navigation1.TabIndex = 0;
            //
            // labelTitle
            //
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.labelTitle.Location = new System.Drawing.Point(247, 14);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "Jewellery";
            //
            // labelSubtitle
            //
            this.labelSubtitle.AutoSize = true;
            this.labelSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
            this.labelSubtitle.Location = new System.Drawing.Point(250, 52);
            this.labelSubtitle.Name = "labelSubtitle";
            this.labelSubtitle.TabIndex = 2;
            this.labelSubtitle.Text = "Beads, pearls and hand-painted pieces";
            //
            // flowProducts
            //
            this.flowProducts.AutoScroll = true;
            this.flowProducts.Location = new System.Drawing.Point(250, 84);
            this.flowProducts.Name = "flowProducts";
            this.flowProducts.Size = new System.Drawing.Size(812, 444);
            this.flowProducts.TabIndex = 3;
            //
            // FormJewellery
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(243)))), ((int)(((byte)(236)))));
            this.ClientSize = new System.Drawing.Size(1069, 528);
            this.Controls.Add(this.flowProducts);
            this.Controls.Add(this.labelSubtitle);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.navigation1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormJewellery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Jewellery - Handcraft Heaven";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Navigation navigation1;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelSubtitle;
        private System.Windows.Forms.FlowLayoutPanel flowProducts;
    }
}
