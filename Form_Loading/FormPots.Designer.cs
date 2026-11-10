namespace Form_Loading
{
    partial class FormPots
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
            this.labelTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelTitle.Font = new System.Drawing.Font("Cheddar Gothic Rough", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.labelTitle.Location = new System.Drawing.Point(530, 12);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "HANDCRAFTED POTS";
            //
            // flowProducts
            //
            this.flowProducts.BackColor = System.Drawing.Color.Transparent;
            this.flowProducts.Location = new System.Drawing.Point(250, 70);
            this.flowProducts.Name = "flowProducts";
            this.flowProducts.Size = new System.Drawing.Size(800, 440);
            this.flowProducts.TabIndex = 2;
            //
            // FormPots
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Form_Loading.Properties.Resources.paper_1074131_19201;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1069, 528);
            this.Controls.Add(this.flowProducts);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.navigation1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormPots";
            this.Text = "Handcrafted Pots - Handcraft Heaven";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Navigation navigation1;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.FlowLayoutPanel flowProducts;
    }
}
