namespace Cliente
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnTIme = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.btnDate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblResultado = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.btnCambiar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTIme
            // 
            this.btnTIme.Location = new System.Drawing.Point(221, 93);
            this.btnTIme.Name = "btnTIme";
            this.btnTIme.Size = new System.Drawing.Size(75, 23);
            this.btnTIme.TabIndex = 0;
            this.btnTIme.Text = "time";
            this.btnTIme.UseVisualStyleBackColor = true;
            this.btnTIme.Click += new System.EventHandler(this.buttonClick);
            // 
            // btnAll
            // 
            this.btnAll.Location = new System.Drawing.Point(59, 93);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(75, 23);
            this.btnAll.TabIndex = 1;
            this.btnAll.Text = "all";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.buttonClick);
            // 
            // btnDate
            // 
            this.btnDate.Location = new System.Drawing.Point(140, 93);
            this.btnDate.Name = "btnDate";
            this.btnDate.Size = new System.Drawing.Size(75, 23);
            this.btnDate.TabIndex = 2;
            this.btnDate.Text = "date";
            this.btnDate.UseVisualStyleBackColor = true;
            this.btnDate.Click += new System.EventHandler(this.buttonClick);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(276, 58);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.buttonClick);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(58, 58);
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '*';
            this.textBox1.Size = new System.Drawing.Size(212, 20);
            this.textBox1.TabIndex = 4;
            // 
            // lblResultado
            // 
            this.lblResultado.AutoSize = true;
            this.lblResultado.Location = new System.Drawing.Point(56, 138);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(25, 13);
            this.lblResultado.TabIndex = 5;
            this.lblResultado.Text = "aaa";
            // 
            // btnCambiar
            // 
            this.btnCambiar.Location = new System.Drawing.Point(318, 93);
            this.btnCambiar.Name = "btnCambiar";
            this.btnCambiar.Size = new System.Drawing.Size(113, 23);
            this.btnCambiar.TabIndex = 6;
            this.btnCambiar.Text = "Cambiar ip y puerto";
            this.btnCambiar.UseVisualStyleBackColor = true;
            this.btnCambiar.Click += new System.EventHandler(this.btnCambiarClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCambiar);
            this.Controls.Add(this.lblResultado);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDate);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.btnTIme);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Cliente";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTIme;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.Button btnDate;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button btnCambiar;
    }
}

