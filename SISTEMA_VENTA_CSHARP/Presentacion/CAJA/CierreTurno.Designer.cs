
namespace SISTEMA_VENTA_CSHARP.Presentacion.CAJA
{
    partial class CierreTurno
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CierreTurno));
            this.lblanuncio = new System.Windows.Forms.Label();
            this.BtnCerrar_turno = new System.Windows.Forms.Button();
            this.lbldiferencia = new System.Windows.Forms.Label();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.txthay = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDeberiaHaber = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblanuncio
            // 
            this.lblanuncio.BackColor = System.Drawing.Color.White;
            this.lblanuncio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblanuncio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(63)))), ((int)(((byte)(67)))));
            this.lblanuncio.Location = new System.Drawing.Point(28, 477);
            this.lblanuncio.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblanuncio.Name = "lblanuncio";
            this.lblanuncio.Size = new System.Drawing.Size(342, 47);
            this.lblanuncio.TabIndex = 621;
            this.lblanuncio.Text = "...";
            this.lblanuncio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblanuncio.Visible = false;
            // 
            // BtnCerrar_turno
            // 
            this.BtnCerrar_turno.BackColor = System.Drawing.Color.Transparent;
            this.BtnCerrar_turno.BackgroundImage = global::SISTEMA_VENTA_CSHARP.Properties.Resources.azul;
            this.BtnCerrar_turno.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnCerrar_turno.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCerrar_turno.FlatAppearance.BorderSize = 0;
            this.BtnCerrar_turno.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BtnCerrar_turno.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BtnCerrar_turno.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCerrar_turno.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCerrar_turno.ForeColor = System.Drawing.Color.White;
            this.BtnCerrar_turno.Location = new System.Drawing.Point(101, 405);
            this.BtnCerrar_turno.Name = "BtnCerrar_turno";
            this.BtnCerrar_turno.Size = new System.Drawing.Size(194, 57);
            this.BtnCerrar_turno.TabIndex = 620;
            this.BtnCerrar_turno.Text = "Cerrar Turno";
            this.BtnCerrar_turno.UseVisualStyleBackColor = false;
            this.BtnCerrar_turno.Click += new System.EventHandler(this.BtnCerrar_turno_Click);
            // 
            // lbldiferencia
            // 
            this.lbldiferencia.AutoSize = true;
            this.lbldiferencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbldiferencia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(166)))), ((int)(((byte)(63)))));
            this.lbldiferencia.Location = new System.Drawing.Point(173, 319);
            this.lbldiferencia.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbldiferencia.Name = "lbldiferencia";
            this.lbldiferencia.Size = new System.Drawing.Size(44, 20);
            this.lbldiferencia.TabIndex = 616;
            this.lbldiferencia.Text = "0.00";
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.LightGray;
            this.Panel1.Location = new System.Drawing.Point(123, 303);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(137, 2);
            this.Panel1.TabIndex = 615;
            // 
            // txthay
            // 
            this.txthay.BackColor = System.Drawing.Color.White;
            this.txthay.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txthay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txthay.Location = new System.Drawing.Point(123, 278);
            this.txthay.Name = "txthay";
            this.txthay.Size = new System.Drawing.Size(137, 19);
            this.txthay.TabIndex = 614;
            this.txthay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txthay.TextChanged += new System.EventHandler(this.txthay_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(75, 239);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(249, 20);
            this.label2.TabIndex = 613;
            this.label2.Text = "¿Cuanto de Efectivo hay en Caja?";
            // 
            // lblDeberiaHaber
            // 
            this.lblDeberiaHaber.AutoSize = true;
            this.lblDeberiaHaber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblDeberiaHaber.ForeColor = System.Drawing.Color.Black;
            this.lblDeberiaHaber.Location = new System.Drawing.Point(173, 201);
            this.lblDeberiaHaber.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblDeberiaHaber.Name = "lblDeberiaHaber";
            this.lblDeberiaHaber.Size = new System.Drawing.Size(44, 20);
            this.lblDeberiaHaber.TabIndex = 612;
            this.lblDeberiaHaber.Text = "0.00";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Label6.ForeColor = System.Drawing.Color.Black;
            this.Label6.Location = new System.Drawing.Point(97, 169);
            this.Label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(199, 20);
            this.Label6.TabIndex = 611;
            this.Label6.Text = "Efectivo esperado en Caja:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 38);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(394, 112);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 610;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(394, 38);
            this.label1.TabIndex = 609;
            this.label1.Text = "CIERRE DE TURNO";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CierreTurno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(394, 545);
            this.Controls.Add(this.BtnCerrar_turno);
            this.Controls.Add(this.txthay);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblanuncio);
            this.Controls.Add(this.lbldiferencia);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblDeberiaHaber);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CierreTurno";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.CierreTurno_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lblanuncio;
        internal System.Windows.Forms.Button BtnCerrar_turno;
        internal System.Windows.Forms.Label lbldiferencia;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.TextBox txthay;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label lblDeberiaHaber;
        internal System.Windows.Forms.Label Label6;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
    }
}