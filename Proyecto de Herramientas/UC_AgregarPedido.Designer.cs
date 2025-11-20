namespace Proyecto_de_Herramientas
{
    partial class UC_AgregarPedido
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtNombreCliente = new System.Windows.Forms.TextBox();
            this.cmbProducto = new System.Windows.Forms.ComboBox();
            this.cmbTalla = new System.Windows.Forms.ComboBox();
            this.btnAgregarAlPedido = new System.Windows.Forms.Button();
            this.lvDetallePedido = new System.Windows.Forms.ListView();
            this.numericCantidad = new System.Windows.Forms.NumericUpDown();
            this.btnConfirmarPedido = new System.Windows.Forms.Button();
            this.lblCantidadTotal = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericCantidad)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(32)))), ((int)(((byte)(240)))));
            this.label1.Location = new System.Drawing.Point(35, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(284, 46);
            this.label1.TabIndex = 23;
            this.label1.Text = "Registrar Pedido";
            // 
            // txtNombreCliente
            // 
            this.txtNombreCliente.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreCliente.Location = new System.Drawing.Point(252, 177);
            this.txtNombreCliente.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNombreCliente.Name = "txtNombreCliente";
            this.txtNombreCliente.Size = new System.Drawing.Size(248, 26);
            this.txtNombreCliente.TabIndex = 24;
            // 
            // cmbProducto
            // 
            this.cmbProducto.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProducto.FormattingEnabled = true;
            this.cmbProducto.Location = new System.Drawing.Point(252, 262);
            this.cmbProducto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbProducto.Name = "cmbProducto";
            this.cmbProducto.Size = new System.Drawing.Size(281, 27);
            this.cmbProducto.TabIndex = 25;
            // 
            // cmbTalla
            // 
            this.cmbTalla.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTalla.FormattingEnabled = true;
            this.cmbTalla.Location = new System.Drawing.Point(252, 343);
            this.cmbTalla.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbTalla.Name = "cmbTalla";
            this.cmbTalla.Size = new System.Drawing.Size(281, 27);
            this.cmbTalla.TabIndex = 26;
            // 
            // btnAgregarAlPedido
            // 
            this.btnAgregarAlPedido.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarAlPedido.Location = new System.Drawing.Point(301, 892);
            this.btnAgregarAlPedido.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAgregarAlPedido.Name = "btnAgregarAlPedido";
            this.btnAgregarAlPedido.Size = new System.Drawing.Size(239, 74);
            this.btnAgregarAlPedido.TabIndex = 27;
            this.btnAgregarAlPedido.Text = "Agregar al Pedido";
            this.btnAgregarAlPedido.UseVisualStyleBackColor = true;
            this.btnAgregarAlPedido.Click += new System.EventHandler(this.btnAgregarAlPedido_Click);
            // 
            // lvDetallePedido
            // 
            this.lvDetallePedido.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvDetallePedido.HideSelection = false;
            this.lvDetallePedido.Location = new System.Drawing.Point(252, 428);
            this.lvDetallePedido.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lvDetallePedido.Name = "lvDetallePedido";
            this.lvDetallePedido.Size = new System.Drawing.Size(759, 339);
            this.lvDetallePedido.TabIndex = 28;
            this.lvDetallePedido.UseCompatibleStateImageBehavior = false;
            this.lvDetallePedido.SelectedIndexChanged += new System.EventHandler(this.lvDetallePedido_SelectedIndexChanged);
            // 
            // numericCantidad
            // 
            this.numericCantidad.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericCantidad.Location = new System.Drawing.Point(632, 261);
            this.numericCantidad.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericCantidad.Name = "numericCantidad";
            this.numericCantidad.Size = new System.Drawing.Size(380, 26);
            this.numericCantidad.TabIndex = 29;
            // 
            // btnConfirmarPedido
            // 
            this.btnConfirmarPedido.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmarPedido.Location = new System.Drawing.Point(733, 895);
            this.btnConfirmarPedido.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnConfirmarPedido.Name = "btnConfirmarPedido";
            this.btnConfirmarPedido.Size = new System.Drawing.Size(239, 71);
            this.btnConfirmarPedido.TabIndex = 32;
            this.btnConfirmarPedido.Text = "Confirmar Pedido";
            this.btnConfirmarPedido.UseVisualStyleBackColor = true;
            this.btnConfirmarPedido.Click += new System.EventHandler(this.btnConfirmarPedido_Click);
            // 
            // lblCantidadTotal
            // 
            this.lblCantidadTotal.AutoSize = true;
            this.lblCantidadTotal.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidadTotal.Location = new System.Drawing.Point(657, 809);
            this.lblCantidadTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCantidadTotal.Name = "lblCantidadTotal";
            this.lblCantidadTotal.Size = new System.Drawing.Size(87, 46);
            this.lblCantidadTotal.TabIndex = 34;
            this.lblCantidadTotal.Text = "num";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(32)))), ((int)(((byte)(240)))));
            this.lblTotal.Location = new System.Drawing.Point(535, 809);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(92, 46);
            this.lblTotal.TabIndex = 33;
            this.lblTotal.Text = "Total";
            // 
            // UC_AgregarPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblCantidadTotal);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnConfirmarPedido);
            this.Controls.Add(this.numericCantidad);
            this.Controls.Add(this.lvDetallePedido);
            this.Controls.Add(this.btnAgregarAlPedido);
            this.Controls.Add(this.cmbTalla);
            this.Controls.Add(this.cmbProducto);
            this.Controls.Add(this.txtNombreCliente);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "UC_AgregarPedido";
            this.Size = new System.Drawing.Size(1339, 1147);
            this.Load += new System.EventHandler(this.UC_AgregarPedido_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericCantidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNombreCliente;
        private System.Windows.Forms.ComboBox cmbProducto;
        private System.Windows.Forms.ComboBox cmbTalla;
        private System.Windows.Forms.Button btnAgregarAlPedido;
        private System.Windows.Forms.ListView lvDetallePedido;
        private System.Windows.Forms.NumericUpDown numericCantidad;
        private System.Windows.Forms.Button btnConfirmarPedido;
        private System.Windows.Forms.Label lblCantidadTotal;
        private System.Windows.Forms.Label lblTotal;
    }
}
