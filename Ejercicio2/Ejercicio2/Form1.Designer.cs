namespace Ejercicio2
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnList = new System.Windows.Forms.Button();
            this.txbIp = new System.Windows.Forms.TextBox();
            this.txbPuerto = new System.Windows.Forms.TextBox();
            this.LblIp = new System.Windows.Forms.Label();
            this.lblPuerto = new System.Windows.Forms.Label();
            this.textbox1 = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.txbUser = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(211, 67);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnClick);
            // 
            // btnList
            // 
            this.btnList.Location = new System.Drawing.Point(211, 109);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(75, 23);
            this.btnList.TabIndex = 1;
            this.btnList.Text = "List";
            this.btnList.UseVisualStyleBackColor = true;
            this.btnList.Click += new System.EventHandler(this.btnClick);
            // 
            // txbIp
            // 
            this.txbIp.Location = new System.Drawing.Point(76, 69);
            this.txbIp.Name = "txbIp";
            this.txbIp.Size = new System.Drawing.Size(100, 20);
            this.txbIp.TabIndex = 2;
            // 
            // txbPuerto
            // 
            this.txbPuerto.Location = new System.Drawing.Point(76, 109);
            this.txbPuerto.Name = "txbPuerto";
            this.txbPuerto.Size = new System.Drawing.Size(100, 20);
            this.txbPuerto.TabIndex = 3;
            // 
            // LblIp
            // 
            this.LblIp.AutoSize = true;
            this.LblIp.Location = new System.Drawing.Point(13, 72);
            this.LblIp.Name = "LblIp";
            this.LblIp.Size = new System.Drawing.Size(17, 13);
            this.LblIp.TabIndex = 4;
            this.LblIp.Text = "IP";
            // 
            // lblPuerto
            // 
            this.lblPuerto.AutoSize = true;
            this.lblPuerto.Location = new System.Drawing.Point(13, 116);
            this.lblPuerto.Name = "lblPuerto";
            this.lblPuerto.Size = new System.Drawing.Size(38, 13);
            this.lblPuerto.TabIndex = 5;
            this.lblPuerto.Text = "Puerto";
            // 
            // textbox1
            // 
            this.textbox1.Location = new System.Drawing.Point(76, 196);
            this.textbox1.Multiline = true;
            this.textbox1.Name = "textbox1";
            this.textbox1.ReadOnly = true;
            this.textbox1.Size = new System.Drawing.Size(210, 106);
            this.textbox1.TabIndex = 6;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(13, 156);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(43, 13);
            this.lblUser.TabIndex = 8;
            this.lblUser.Text = "Usuario";
            // 
            // txbUser
            // 
            this.txbUser.Location = new System.Drawing.Point(76, 149);
            this.txbUser.Name = "txbUser";
            this.txbUser.Size = new System.Drawing.Size(100, 20);
            this.txbUser.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.txbUser);
            this.Controls.Add(this.textbox1);
            this.Controls.Add(this.lblPuerto);
            this.Controls.Add(this.LblIp);
            this.Controls.Add(this.txbPuerto);
            this.Controls.Add(this.txbIp);
            this.Controls.Add(this.btnList);
            this.Controls.Add(this.btnAdd);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnList;
        private System.Windows.Forms.TextBox txbIp;
        private System.Windows.Forms.TextBox txbPuerto;
        private System.Windows.Forms.Label LblIp;
        private System.Windows.Forms.Label lblPuerto;
        private System.Windows.Forms.TextBox textbox1;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txbUser;
    }
}

