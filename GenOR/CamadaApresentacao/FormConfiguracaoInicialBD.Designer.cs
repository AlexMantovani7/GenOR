namespace GenOR
{
    partial class FormConfiguracaoInicialBD
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
        private new void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfiguracaoInicialBD));
            this.txtb_Server = new System.Windows.Forms.TextBox();
            this.lb_Server = new System.Windows.Forms.Label();
            this.txtb_Password = new System.Windows.Forms.TextBox();
            this.lb_Password = new System.Windows.Forms.Label();
            this.txtb_Uid = new System.Windows.Forms.TextBox();
            this.lb_Uid = new System.Windows.Forms.Label();
            this.btn_Confirmar = new System.Windows.Forms.Button();
            this.btn_Cancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtb_Server
            // 
            this.txtb_Server.AccessibleDescription = "";
            this.txtb_Server.AccessibleName = "";
            this.txtb_Server.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtb_Server.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb_Server.ForeColor = System.Drawing.Color.Black;
            this.txtb_Server.Location = new System.Drawing.Point(138, 14);
            this.txtb_Server.MaxLength = 250;
            this.txtb_Server.Name = "txtb_Server";
            this.txtb_Server.Size = new System.Drawing.Size(482, 26);
            this.txtb_Server.TabIndex = 1;
            this.txtb_Server.WordWrap = false;
            this.txtb_Server.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtb_Server_KeyPress);
            // 
            // lb_Server
            // 
            this.lb_Server.AutoSize = true;
            this.lb_Server.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Server.Location = new System.Drawing.Point(10, 15);
            this.lb_Server.Name = "lb_Server";
            this.lb_Server.Size = new System.Drawing.Size(126, 24);
            this.lb_Server.TabIndex = 154;
            this.lb_Server.Text = "SERVIDOR :";
            // 
            // txtb_Password
            // 
            this.txtb_Password.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtb_Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb_Password.ForeColor = System.Drawing.Color.Black;
            this.txtb_Password.Location = new System.Drawing.Point(138, 96);
            this.txtb_Password.MaxLength = 250;
            this.txtb_Password.Name = "txtb_Password";
            this.txtb_Password.Size = new System.Drawing.Size(482, 26);
            this.txtb_Password.TabIndex = 4;
            this.txtb_Password.UseSystemPasswordChar = true;
            this.txtb_Password.WordWrap = false;
            this.txtb_Password.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtb_Password_KeyPress);
            // 
            // lb_Password
            // 
            this.lb_Password.AutoSize = true;
            this.lb_Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Password.Location = new System.Drawing.Point(5, 97);
            this.lb_Password.Name = "lb_Password";
            this.lb_Password.Size = new System.Drawing.Size(132, 24);
            this.lb_Password.TabIndex = 159;
            this.lb_Password.Text = "PASSWORD:";
            // 
            // txtb_Uid
            // 
            this.txtb_Uid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtb_Uid.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb_Uid.ForeColor = System.Drawing.Color.Black;
            this.txtb_Uid.Location = new System.Drawing.Point(138, 55);
            this.txtb_Uid.MaxLength = 250;
            this.txtb_Uid.Name = "txtb_Uid";
            this.txtb_Uid.Size = new System.Drawing.Size(482, 26);
            this.txtb_Uid.TabIndex = 3;
            this.txtb_Uid.WordWrap = false;
            this.txtb_Uid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtb_Uid_KeyPress);
            // 
            // lb_Uid
            // 
            this.lb_Uid.AutoSize = true;
            this.lb_Uid.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Uid.Location = new System.Drawing.Point(6, 56);
            this.lb_Uid.Name = "lb_Uid";
            this.lb_Uid.Size = new System.Drawing.Size(131, 24);
            this.lb_Uid.TabIndex = 158;
            this.lb_Uid.Text = "USERNAME:";
            // 
            // btn_Confirmar
            // 
            this.btn_Confirmar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Confirmar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_Confirmar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_Confirmar.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_Confirmar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_Confirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Confirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Confirmar.Image = global::GenOR.Properties.Resources.Yes;
            this.btn_Confirmar.Location = new System.Drawing.Point(639, 73);
            this.btn_Confirmar.Name = "btn_Confirmar";
            this.btn_Confirmar.Size = new System.Drawing.Size(50, 50);
            this.btn_Confirmar.TabIndex = 5;
            this.btn_Confirmar.UseVisualStyleBackColor = false;
            this.btn_Confirmar.Click += new System.EventHandler(this.btn_Confirmar_Click);
            // 
            // btn_Cancelar
            // 
            this.btn_Cancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancelar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_Cancelar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_Cancelar.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_Cancelar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_Cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Cancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancelar.Image = global::GenOR.Properties.Resources.Delete;
            this.btn_Cancelar.Location = new System.Drawing.Point(697, 73);
            this.btn_Cancelar.Name = "btn_Cancelar";
            this.btn_Cancelar.Size = new System.Drawing.Size(50, 50);
            this.btn_Cancelar.TabIndex = 6;
            this.btn_Cancelar.UseVisualStyleBackColor = false;
            this.btn_Cancelar.Click += new System.EventHandler(this.btn_Cancelar_Click);
            // 
            // FormConfiguracaoInicialBD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::GenOR.Properties.Resources.Budget_wallpaper;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(753, 133);
            this.Controls.Add(this.btn_Cancelar);
            this.Controls.Add(this.btn_Confirmar);
            this.Controls.Add(this.txtb_Password);
            this.Controls.Add(this.lb_Password);
            this.Controls.Add(this.txtb_Uid);
            this.Controls.Add(this.lb_Uid);
            this.Controls.Add(this.txtb_Server);
            this.Controls.Add(this.lb_Server);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormConfiguracaoInicialBD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CONFIGURAÇÃO DO BANCO DE DADOS";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtb_Server;
        private System.Windows.Forms.Label lb_Server;
        private System.Windows.Forms.TextBox txtb_Password;
        private System.Windows.Forms.Label lb_Password;
        private System.Windows.Forms.TextBox txtb_Uid;
        private System.Windows.Forms.Label lb_Uid;
        private System.Windows.Forms.Button btn_Confirmar;
        private System.Windows.Forms.Button btn_Cancelar;
    }
}