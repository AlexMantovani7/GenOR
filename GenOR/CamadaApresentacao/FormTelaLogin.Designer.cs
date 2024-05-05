namespace GenOR
{
    partial class FormTelaLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTelaLogin));
            this.gb_Login = new System.Windows.Forms.GroupBox();
            this.btn_TesteConexao = new System.Windows.Forms.Button();
            this.btn_ExibirOcultarSenha = new System.Windows.Forms.Button();
            this.txtb_Senha = new System.Windows.Forms.TextBox();
            this.lb_Senha = new System.Windows.Forms.Label();
            this.txtb_NomeUsuario = new System.Windows.Forms.TextBox();
            this.lb_NomeUsuario = new System.Windows.Forms.Label();
            this.btn_Confirmar = new System.Windows.Forms.Button();
            this.gb_Login.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_Login
            // 
            this.gb_Login.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_Login.BackColor = System.Drawing.SystemColors.ControlLight;
            this.gb_Login.BackgroundImage = global::GenOR.Properties.Resources.Budget_wallpaper;
            this.gb_Login.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gb_Login.Controls.Add(this.txtb_Senha);
            this.gb_Login.Controls.Add(this.btn_TesteConexao);
            this.gb_Login.Controls.Add(this.btn_ExibirOcultarSenha);
            this.gb_Login.Controls.Add(this.lb_Senha);
            this.gb_Login.Controls.Add(this.txtb_NomeUsuario);
            this.gb_Login.Controls.Add(this.lb_NomeUsuario);
            this.gb_Login.Controls.Add(this.btn_Confirmar);
            this.gb_Login.Location = new System.Drawing.Point(-1, -2);
            this.gb_Login.Name = "gb_Login";
            this.gb_Login.Size = new System.Drawing.Size(905, 103);
            this.gb_Login.TabIndex = 3;
            this.gb_Login.TabStop = false;
            // 
            // btn_TesteConexao
            // 
            this.btn_TesteConexao.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_TesteConexao.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_TesteConexao.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_TesteConexao.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_TesteConexao.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_TesteConexao.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_TesteConexao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_TesteConexao.Location = new System.Drawing.Point(4, 64);
            this.btn_TesteConexao.Name = "btn_TesteConexao";
            this.btn_TesteConexao.Size = new System.Drawing.Size(96, 35);
            this.btn_TesteConexao.TabIndex = 5;
            this.btn_TesteConexao.Text = "TESTE CONEXÃO";
            this.btn_TesteConexao.UseVisualStyleBackColor = false;
            this.btn_TesteConexao.Click += new System.EventHandler(this.btn_TesteConexao_Click);
            // 
            // btn_ExibirOcultarSenha
            // 
            this.btn_ExibirOcultarSenha.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_ExibirOcultarSenha.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_ExibirOcultarSenha.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_ExibirOcultarSenha.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_ExibirOcultarSenha.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_ExibirOcultarSenha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ExibirOcultarSenha.Font = new System.Drawing.Font("Arial Narrow", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ExibirOcultarSenha.Image = global::GenOR.Properties.Resources.Eye;
            this.btn_ExibirOcultarSenha.Location = new System.Drawing.Point(655, 61);
            this.btn_ExibirOcultarSenha.Name = "btn_ExibirOcultarSenha";
            this.btn_ExibirOcultarSenha.Size = new System.Drawing.Size(40, 35);
            this.btn_ExibirOcultarSenha.TabIndex = 3;
            this.btn_ExibirOcultarSenha.UseVisualStyleBackColor = false;
            this.btn_ExibirOcultarSenha.Click += new System.EventHandler(this.btn_ExibirOcultarSenha_Click);
            // 
            // txtb_Senha
            // 
            this.txtb_Senha.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb_Senha.Location = new System.Drawing.Point(200, 66);
            this.txtb_Senha.MaxLength = 50;
            this.txtb_Senha.Name = "txtb_Senha";
            this.txtb_Senha.PasswordChar = '*';
            this.txtb_Senha.Size = new System.Drawing.Size(449, 26);
            this.txtb_Senha.TabIndex = 2;
            this.txtb_Senha.WordWrap = false;
            this.txtb_Senha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtb_Senha_KeyPress);
            // 
            // lb_Senha
            // 
            this.lb_Senha.AutoSize = true;
            this.lb_Senha.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Senha.Location = new System.Drawing.Point(106, 67);
            this.lb_Senha.Name = "lb_Senha";
            this.lb_Senha.Size = new System.Drawing.Size(93, 24);
            this.lb_Senha.TabIndex = 54;
            this.lb_Senha.Text = "SENHA :";
            // 
            // txtb_NomeUsuario
            // 
            this.txtb_NomeUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb_NomeUsuario.Location = new System.Drawing.Point(200, 22);
            this.txtb_NomeUsuario.MaxLength = 256;
            this.txtb_NomeUsuario.Name = "txtb_NomeUsuario";
            this.txtb_NomeUsuario.Size = new System.Drawing.Size(495, 26);
            this.txtb_NomeUsuario.TabIndex = 1;
            this.txtb_NomeUsuario.WordWrap = false;
            this.txtb_NomeUsuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtb_NomeUsuario_KeyPress);
            // 
            // lb_NomeUsuario
            // 
            this.lb_NomeUsuario.AutoSize = true;
            this.lb_NomeUsuario.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lb_NomeUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_NomeUsuario.Location = new System.Drawing.Point(19, 23);
            this.lb_NomeUsuario.Name = "lb_NomeUsuario";
            this.lb_NomeUsuario.Size = new System.Drawing.Size(180, 24);
            this.lb_NomeUsuario.TabIndex = 52;
            this.lb_NomeUsuario.Text = "NOME USUÁRIO :";
            // 
            // btn_Confirmar
            // 
            this.btn_Confirmar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Confirmar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_Confirmar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_Confirmar.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_Confirmar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_Confirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Confirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Confirmar.Image = global::GenOR.Properties.Resources.Yes;
            this.btn_Confirmar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Confirmar.Location = new System.Drawing.Point(719, 22);
            this.btn_Confirmar.Name = "btn_Confirmar";
            this.btn_Confirmar.Size = new System.Drawing.Size(180, 70);
            this.btn_Confirmar.TabIndex = 4;
            this.btn_Confirmar.Text = "     CONFIRMAR";
            this.btn_Confirmar.UseVisualStyleBackColor = false;
            this.btn_Confirmar.Click += new System.EventHandler(this.btn_Confirmar_Click);
            // 
            // FormTelaLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 101);
            this.Controls.Add(this.gb_Login);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormTelaLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LOGIN GERENCIADOR DE ORÇAMENTOS";
            this.Load += new System.EventHandler(this.FormTelaLogin_Load);
            this.gb_Login.ResumeLayout(false);
            this.gb_Login.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_Login;
        private System.Windows.Forms.Button btn_ExibirOcultarSenha;
        private System.Windows.Forms.TextBox txtb_Senha;
        private System.Windows.Forms.Label lb_Senha;
        private System.Windows.Forms.TextBox txtb_NomeUsuario;
        private System.Windows.Forms.Label lb_NomeUsuario;
        private System.Windows.Forms.Button btn_Confirmar;
        private System.Windows.Forms.Button btn_TesteConexao;
    }
}