namespace Proyecto_de_Herramientas
{
    partial class Pantalla_para_realizar_compras
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pantalla_para_realizar_compras));
            this.dvgProductos = new System.Windows.Forms.DataGridView();
            this.picFinalizarCompra = new System.Windows.Forms.PictureBox();
            this.picSalir = new System.Windows.Forms.PictureBox();
            this.btnregresar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dvgProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFinalizarCompra)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSalir)).BeginInit();
            this.SuspendLayout();
            // 
            // dvgProductos
            // 
            this.dvgProductos.AllowUserToAddRows = false;
            this.dvgProductos.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dvgProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgProductos.Location = new System.Drawing.Point(43, 65);
            this.dvgProductos.Name = "dvgProductos";
            this.dvgProductos.RowHeadersWidth = 51;
            this.dvgProductos.RowTemplate.Height = 24;
            this.dvgProductos.Size = new System.Drawing.Size(716, 312);
            this.dvgProductos.TabIndex = 1;
            // 
            // picFinalizarCompra
            // 
            this.picFinalizarCompra.Image = ((System.Drawing.Image)(resources.GetObject("picFinalizarCompra.Image")));
            this.picFinalizarCompra.Location = new System.Drawing.Point(572, 389);
            this.picFinalizarCompra.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picFinalizarCompra.Name = "picFinalizarCompra";
            this.picFinalizarCompra.Size = new System.Drawing.Size(65, 50);
            this.picFinalizarCompra.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picFinalizarCompra.TabIndex = 3;
            this.picFinalizarCompra.TabStop = false;
            this.picFinalizarCompra.Click += new System.EventHandler(this.picFinalizarCompra_Click);
            // 
            // picSalir
            // 
            this.picSalir.Image = ((System.Drawing.Image)(resources.GetObject("picSalir.Image")));
            this.picSalir.Location = new System.Drawing.Point(173, 390);
            this.picSalir.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picSalir.Name = "picSalir";
            this.picSalir.Size = new System.Drawing.Size(60, 50);
            this.picSalir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSalir.TabIndex = 36;
            this.picSalir.TabStop = false;
            this.picSalir.Click += new System.EventHandler(this.picSalir_Click);
            // 
            // btnregresar
            // 
            this.btnregresar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnregresar.BackgroundImage")));
            this.btnregresar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnregresar.Location = new System.Drawing.Point(366, 389);
            this.btnregresar.Name = "btnregresar";
            this.btnregresar.Size = new System.Drawing.Size(74, 50);
            this.btnregresar.TabIndex = 37;
            this.btnregresar.UseVisualStyleBackColor = true;
            this.btnregresar.Click += new System.EventHandler(this.btnregresar_Click_1);
            // 
            // Pantalla_para_realizar_compras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnregresar);
            this.Controls.Add(this.picSalir);
            this.Controls.Add(this.picFinalizarCompra);
            this.Controls.Add(this.dvgProductos);
            this.Name = "Pantalla_para_realizar_compras";
            this.Text = "Pantalla_para_realizar_compras";
            this.Load += new System.EventHandler(this.Pantalla_para_realizar_compras_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dvgProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFinalizarCompra)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSalir)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dvgProductos;
        private System.Windows.Forms.PictureBox picFinalizarCompra;
        private System.Windows.Forms.PictureBox picSalir;
        private System.Windows.Forms.Button btnregresar;
    }
}