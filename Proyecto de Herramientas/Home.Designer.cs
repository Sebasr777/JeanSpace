namespace Proyecto_de_Herramientas
{
    partial class Home
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
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.panelLateral = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelContenedor
            // 
            this.panelContenedor.AutoScroll = true;
            this.panelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenedor.Location = new System.Drawing.Point(225, 0);
            this.panelContenedor.Margin = new System.Windows.Forms.Padding(2);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(1009, 885);
            this.panelContenedor.TabIndex = 1;
            // 
            // panelLateral
            // 
            this.panelLateral.BackColor = System.Drawing.Color.White;
            this.panelLateral.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLateral.Location = new System.Drawing.Point(0, 0);
            this.panelLateral.Margin = new System.Windows.Forms.Padding(2);
            this.panelLateral.Name = "panelLateral";
            this.panelLateral.Size = new System.Drawing.Size(225, 885);
            this.panelLateral.TabIndex = 0;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 885);
            this.Controls.Add(this.panelContenedor);
            this.Controls.Add(this.panelLateral);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Home";
            this.Text = "Home";
            this.Load += new System.EventHandler(this.Home_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.Panel panelLateral;
    }
}