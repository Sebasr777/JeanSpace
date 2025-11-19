namespace Proyecto_de_Herramientas
{
    partial class login
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

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(login));
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.txtContraseña = new System.Windows.Forms.TextBox();
            this.btnIngresar = new System.Windows.Forms.Button();
            this.picVer = new System.Windows.Forms.PictureBox();
            this.picOcultar = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picVer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOcultar)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(449, 320);
            this.txtUsuario.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(189, 20);
            this.txtUsuario.TabIndex = 1;
            // 
            // txtContraseña
            // 
            this.txtContraseña.Location = new System.Drawing.Point(448, 391);
            this.txtContraseña.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtContraseña.Name = "txtContraseña";
            this.txtContraseña.Size = new System.Drawing.Size(151, 20);
            this.txtContraseña.TabIndex = 2;
            this.txtContraseña.UseSystemPasswordChar = true;
            // 
            // btnIngresar
            // 
            this.btnIngresar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnIngresar.BackgroundImage")));
            this.btnIngresar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnIngresar.ForeColor = System.Drawing.Color.Coral;
            this.btnIngresar.Location = new System.Drawing.Point(678, 469);
            this.btnIngresar.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.Size = new System.Drawing.Size(219, 57);
            this.btnIngresar.TabIndex = 4;
            this.btnIngresar.UseVisualStyleBackColor = true;
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click_1);
            // 
            // picVer
            // 
            this.picVer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picVer.BackgroundImage")));
            this.picVer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picVer.Location = new System.Drawing.Point(603, 393);
            this.picVer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.picVer.Name = "picVer";
            this.picVer.Size = new System.Drawing.Size(32, 15);
            this.picVer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picVer.TabIndex = 5;
            this.picVer.TabStop = false;
            this.picVer.Click += new System.EventHandler(this.picVer_Click);
            // 
            // picOcultar
            // 
            this.picOcultar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picOcultar.BackgroundImage")));
            this.picOcultar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picOcultar.Location = new System.Drawing.Point(602, 390);
            this.picOcultar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.picOcultar.Name = "picOcultar";
            this.picOcultar.Size = new System.Drawing.Size(35, 18);
            this.picOcultar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picOcultar.TabIndex = 6;
            this.picOcultar.TabStop = false;
            this.picOcultar.Click += new System.EventHandler(this.picOcultar_Click);
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(947, 573);
            this.Controls.Add(this.picOcultar);
            this.Controls.Add(this.picVer);
            this.Controls.Add(this.btnIngresar);
            this.Controls.Add(this.txtContraseña);
            this.Controls.Add(this.txtUsuario);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "login";
            this.Text = "Iniciar Sesión";
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picVer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOcultar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.TextBox txtContraseña;
        private System.Windows.Forms.Button btnIngresar;
        private System.Windows.Forms.PictureBox picVer;
        private System.Windows.Forms.PictureBox picOcultar;
    }
}

