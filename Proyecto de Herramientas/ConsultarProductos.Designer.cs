namespace Proyecto_de_Herramientas
{
    partial class ConsultarProductos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsultarProductos));
            this.btnVolver = new System.Windows.Forms.Button();
            this.linkActualizar = new System.Windows.Forms.LinkLabel();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // btnVolver
            // 
            this.btnVolver.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnVolver.BackgroundImage")));
            this.btnVolver.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnVolver.Location = new System.Drawing.Point(521, 290);
            this.btnVolver.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(58, 50);
            this.btnVolver.TabIndex = 5;
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // linkActualizar
            // 
            this.linkActualizar.AutoSize = true;
            this.linkActualizar.Location = new System.Drawing.Point(36, 302);
            this.linkActualizar.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkActualizar.Name = "linkActualizar";
            this.linkActualizar.Size = new System.Drawing.Size(196, 13);
            this.linkActualizar.TabIndex = 4;
            this.linkActualizar.TabStop = true;
            this.linkActualizar.Text = "Actualiza o elimina algún producto aquí.";
            this.linkActualizar.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkActualizar_LinkClicked);
            // 
            // dgvProductos
            // 
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Location = new System.Drawing.Point(23, 25);
            this.dgvProductos.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.RowHeadersWidth = 51;
            this.dgvProductos.RowTemplate.Height = 24;
            this.dgvProductos.Size = new System.Drawing.Size(556, 233);
            this.dgvProductos.TabIndex = 3;
            // 
            // ConsultarProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 365);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.linkActualizar);
            this.Controls.Add(this.dgvProductos);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "ConsultarProductos";
            this.Text = "ConsultarProductos";
            this.Load += new System.EventHandler(this.ConsultarProductos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.LinkLabel linkActualizar;
        private System.Windows.Forms.DataGridView dgvProductos;
    }
}