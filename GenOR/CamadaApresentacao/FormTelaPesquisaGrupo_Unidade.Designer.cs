namespace GenOR
{
    partial class FormTelaPesquisaGrupo_Unidade
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTelaPesquisaGrupo_Unidade));
            this.tc_Pesquisa = new System.Windows.Forms.TabControl();
            this.tcp_Codigo = new System.Windows.Forms.TabPage();
            this.txtb_Codigo = new System.Windows.Forms.TextBox();
            this.lb_Codigo = new System.Windows.Forms.Label();
            this.btn_PesquisaPorCodigo = new System.Windows.Forms.Button();
            this.tcp_Sigla = new System.Windows.Forms.TabPage();
            this.txtb_Sigla = new System.Windows.Forms.TextBox();
            this.lb_Sigla = new System.Windows.Forms.Label();
            this.btn_PesquisaPorSigla = new System.Windows.Forms.Button();
            this.tcp_Descricao = new System.Windows.Forms.TabPage();
            this.txtb_Descricao = new System.Windows.Forms.TextBox();
            this.lb_Descricao = new System.Windows.Forms.Label();
            this.btn_PesquisaPorDescricao = new System.Windows.Forms.Button();
            this.tcp_Material_Ou_Produto = new System.Windows.Forms.TabPage();
            this.rb_Produto = new System.Windows.Forms.RadioButton();
            this.rb_Material = new System.Windows.Forms.RadioButton();
            this.txtb_FundoVisual_RadioButtons = new System.Windows.Forms.TextBox();
            this.lb_Material_Ou_Produto = new System.Windows.Forms.Label();
            this.btn_PesquisaPorMaterial_Ou_Produto = new System.Windows.Forms.Button();
            this.tc_Pesquisa.SuspendLayout();
            this.tcp_Codigo.SuspendLayout();
            this.tcp_Sigla.SuspendLayout();
            this.tcp_Descricao.SuspendLayout();
            this.tcp_Material_Ou_Produto.SuspendLayout();
            this.SuspendLayout();
            // 
            // tc_Pesquisa
            // 
            this.tc_Pesquisa.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tc_Pesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tc_Pesquisa.Controls.Add(this.tcp_Codigo);
            this.tc_Pesquisa.Controls.Add(this.tcp_Sigla);
            this.tc_Pesquisa.Controls.Add(this.tcp_Descricao);
            this.tc_Pesquisa.Controls.Add(this.tcp_Material_Ou_Produto);
            this.tc_Pesquisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.tc_Pesquisa.HotTrack = true;
            this.tc_Pesquisa.Location = new System.Drawing.Point(-1, 1);
            this.tc_Pesquisa.Name = "tc_Pesquisa";
            this.tc_Pesquisa.SelectedIndex = 0;
            this.tc_Pesquisa.Size = new System.Drawing.Size(1027, 105);
            this.tc_Pesquisa.TabIndex = 1;
            this.tc_Pesquisa.TabStop = false;
            // 
            // tcp_Codigo
            // 
            this.tcp_Codigo.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tcp_Codigo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tcp_Codigo.Controls.Add(this.txtb_Codigo);
            this.tcp_Codigo.Controls.Add(this.lb_Codigo);
            this.tcp_Codigo.Controls.Add(this.btn_PesquisaPorCodigo);
            this.tcp_Codigo.Location = new System.Drawing.Point(4, 4);
            this.tcp_Codigo.Name = "tcp_Codigo";
            this.tcp_Codigo.Padding = new System.Windows.Forms.Padding(3);
            this.tcp_Codigo.Size = new System.Drawing.Size(1019, 76);
            this.tcp_Codigo.TabIndex = 0;
            this.tcp_Codigo.Text = "CÓDIGO";
            // 
            // txtb_Codigo
            // 
            this.txtb_Codigo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtb_Codigo.BackColor = System.Drawing.Color.White;
            this.txtb_Codigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb_Codigo.Location = new System.Drawing.Point(90, 26);
            this.txtb_Codigo.Name = "txtb_Codigo";
            this.txtb_Codigo.Size = new System.Drawing.Size(741, 26);
            this.txtb_Codigo.TabIndex = 1;
            this.txtb_Codigo.WordWrap = false;
            this.txtb_Codigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtb_Codigo_KeyPress);
            // 
            // lb_Codigo
            // 
            this.lb_Codigo.AutoSize = true;
            this.lb_Codigo.BackColor = System.Drawing.Color.Transparent;
            this.lb_Codigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Codigo.Location = new System.Drawing.Point(3, 30);
            this.lb_Codigo.Name = "lb_Codigo";
            this.lb_Codigo.Size = new System.Drawing.Size(85, 18);
            this.lb_Codigo.TabIndex = 52;
            this.lb_Codigo.Text = "CÓDIGO :";
            // 
            // btn_PesquisaPorCodigo
            // 
            this.btn_PesquisaPorCodigo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_PesquisaPorCodigo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_PesquisaPorCodigo.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_PesquisaPorCodigo.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_PesquisaPorCodigo.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_PesquisaPorCodigo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PesquisaPorCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PesquisaPorCodigo.Image = global::GenOR.Properties.Resources.Yes;
            this.btn_PesquisaPorCodigo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_PesquisaPorCodigo.Location = new System.Drawing.Point(837, 7);
            this.btn_PesquisaPorCodigo.Name = "btn_PesquisaPorCodigo";
            this.btn_PesquisaPorCodigo.Size = new System.Drawing.Size(173, 59);
            this.btn_PesquisaPorCodigo.TabIndex = 2;
            this.btn_PesquisaPorCodigo.Text = "      PESQUISAR";
            this.btn_PesquisaPorCodigo.UseVisualStyleBackColor = false;
            this.btn_PesquisaPorCodigo.Click += new System.EventHandler(this.btn_PesquisaPorCodigo_Click);
            // 
            // tcp_Sigla
            // 
            this.tcp_Sigla.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tcp_Sigla.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tcp_Sigla.Controls.Add(this.txtb_Sigla);
            this.tcp_Sigla.Controls.Add(this.lb_Sigla);
            this.tcp_Sigla.Controls.Add(this.btn_PesquisaPorSigla);
            this.tcp_Sigla.Location = new System.Drawing.Point(4, 4);
            this.tcp_Sigla.Name = "tcp_Sigla";
            this.tcp_Sigla.Size = new System.Drawing.Size(1019, 76);
            this.tcp_Sigla.TabIndex = 2;
            this.tcp_Sigla.Text = "SIGLA";
            // 
            // txtb_Sigla
            // 
            this.txtb_Sigla.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtb_Sigla.BackColor = System.Drawing.Color.White;
            this.txtb_Sigla.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb_Sigla.Location = new System.Drawing.Point(70, 26);
            this.txtb_Sigla.MaxLength = 8;
            this.txtb_Sigla.Name = "txtb_Sigla";
            this.txtb_Sigla.Size = new System.Drawing.Size(761, 26);
            this.txtb_Sigla.TabIndex = 3;
            this.txtb_Sigla.WordWrap = false;
            this.txtb_Sigla.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtb_Sigla_KeyPress);
            // 
            // lb_Sigla
            // 
            this.lb_Sigla.AutoSize = true;
            this.lb_Sigla.BackColor = System.Drawing.Color.Transparent;
            this.lb_Sigla.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Sigla.Location = new System.Drawing.Point(3, 30);
            this.lb_Sigla.Name = "lb_Sigla";
            this.lb_Sigla.Size = new System.Drawing.Size(65, 18);
            this.lb_Sigla.TabIndex = 55;
            this.lb_Sigla.Text = "SIGLA :";
            // 
            // btn_PesquisaPorSigla
            // 
            this.btn_PesquisaPorSigla.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_PesquisaPorSigla.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_PesquisaPorSigla.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_PesquisaPorSigla.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_PesquisaPorSigla.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_PesquisaPorSigla.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PesquisaPorSigla.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PesquisaPorSigla.Image = global::GenOR.Properties.Resources.Yes;
            this.btn_PesquisaPorSigla.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_PesquisaPorSigla.Location = new System.Drawing.Point(837, 7);
            this.btn_PesquisaPorSigla.Name = "btn_PesquisaPorSigla";
            this.btn_PesquisaPorSigla.Size = new System.Drawing.Size(173, 59);
            this.btn_PesquisaPorSigla.TabIndex = 4;
            this.btn_PesquisaPorSigla.Text = "      PESQUISAR";
            this.btn_PesquisaPorSigla.UseVisualStyleBackColor = false;
            this.btn_PesquisaPorSigla.Click += new System.EventHandler(this.btn_PesquisaPorSigla_Click);
            // 
            // tcp_Descricao
            // 
            this.tcp_Descricao.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tcp_Descricao.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tcp_Descricao.Controls.Add(this.txtb_Descricao);
            this.tcp_Descricao.Controls.Add(this.lb_Descricao);
            this.tcp_Descricao.Controls.Add(this.btn_PesquisaPorDescricao);
            this.tcp_Descricao.Location = new System.Drawing.Point(4, 4);
            this.tcp_Descricao.Name = "tcp_Descricao";
            this.tcp_Descricao.Size = new System.Drawing.Size(1019, 76);
            this.tcp_Descricao.TabIndex = 1;
            this.tcp_Descricao.Text = "DESCRIÇÃO";
            // 
            // txtb_Descricao
            // 
            this.txtb_Descricao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtb_Descricao.BackColor = System.Drawing.Color.White;
            this.txtb_Descricao.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb_Descricao.Location = new System.Drawing.Point(120, 26);
            this.txtb_Descricao.MaxLength = 150;
            this.txtb_Descricao.Name = "txtb_Descricao";
            this.txtb_Descricao.Size = new System.Drawing.Size(711, 26);
            this.txtb_Descricao.TabIndex = 5;
            this.txtb_Descricao.WordWrap = false;
            this.txtb_Descricao.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtb_Descricao_KeyPress);
            // 
            // lb_Descricao
            // 
            this.lb_Descricao.AutoSize = true;
            this.lb_Descricao.BackColor = System.Drawing.Color.Transparent;
            this.lb_Descricao.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Descricao.Location = new System.Drawing.Point(3, 30);
            this.lb_Descricao.Name = "lb_Descricao";
            this.lb_Descricao.Size = new System.Drawing.Size(115, 18);
            this.lb_Descricao.TabIndex = 55;
            this.lb_Descricao.Text = "DESCRIÇÃO :";
            // 
            // btn_PesquisaPorDescricao
            // 
            this.btn_PesquisaPorDescricao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_PesquisaPorDescricao.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_PesquisaPorDescricao.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_PesquisaPorDescricao.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_PesquisaPorDescricao.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_PesquisaPorDescricao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PesquisaPorDescricao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PesquisaPorDescricao.Image = global::GenOR.Properties.Resources.Yes;
            this.btn_PesquisaPorDescricao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_PesquisaPorDescricao.Location = new System.Drawing.Point(837, 7);
            this.btn_PesquisaPorDescricao.Name = "btn_PesquisaPorDescricao";
            this.btn_PesquisaPorDescricao.Size = new System.Drawing.Size(173, 59);
            this.btn_PesquisaPorDescricao.TabIndex = 6;
            this.btn_PesquisaPorDescricao.Text = "      PESQUISAR";
            this.btn_PesquisaPorDescricao.UseVisualStyleBackColor = false;
            this.btn_PesquisaPorDescricao.Click += new System.EventHandler(this.btn_PesquisaPorDescricao_Click);
            // 
            // tcp_Material_Ou_Produto
            // 
            this.tcp_Material_Ou_Produto.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tcp_Material_Ou_Produto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tcp_Material_Ou_Produto.Controls.Add(this.rb_Produto);
            this.tcp_Material_Ou_Produto.Controls.Add(this.rb_Material);
            this.tcp_Material_Ou_Produto.Controls.Add(this.txtb_FundoVisual_RadioButtons);
            this.tcp_Material_Ou_Produto.Controls.Add(this.lb_Material_Ou_Produto);
            this.tcp_Material_Ou_Produto.Controls.Add(this.btn_PesquisaPorMaterial_Ou_Produto);
            this.tcp_Material_Ou_Produto.Location = new System.Drawing.Point(4, 4);
            this.tcp_Material_Ou_Produto.Name = "tcp_Material_Ou_Produto";
            this.tcp_Material_Ou_Produto.Size = new System.Drawing.Size(1019, 76);
            this.tcp_Material_Ou_Produto.TabIndex = 3;
            this.tcp_Material_Ou_Produto.Text = "MATERIAL OU PRODUTO";
            // 
            // rb_Produto
            // 
            this.rb_Produto.AutoSize = true;
            this.rb_Produto.BackColor = System.Drawing.Color.Transparent;
            this.rb_Produto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_Produto.Location = new System.Drawing.Point(486, 24);
            this.rb_Produto.Name = "rb_Produto";
            this.rb_Produto.Size = new System.Drawing.Size(123, 24);
            this.rb_Produto.TabIndex = 8;
            this.rb_Produto.Text = " PRODUTO ";
            this.rb_Produto.UseVisualStyleBackColor = false;
            // 
            // rb_Material
            // 
            this.rb_Material.AutoSize = true;
            this.rb_Material.BackColor = System.Drawing.Color.Transparent;
            this.rb_Material.Checked = true;
            this.rb_Material.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_Material.Location = new System.Drawing.Point(340, 24);
            this.rb_Material.Name = "rb_Material";
            this.rb_Material.Size = new System.Drawing.Size(121, 24);
            this.rb_Material.TabIndex = 7;
            this.rb_Material.TabStop = true;
            this.rb_Material.Text = " MATERIAL";
            this.rb_Material.UseVisualStyleBackColor = false;
            // 
            // txtb_FundoVisual_RadioButtons
            // 
            this.txtb_FundoVisual_RadioButtons.AcceptsTab = true;
            this.txtb_FundoVisual_RadioButtons.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtb_FundoVisual_RadioButtons.Enabled = false;
            this.txtb_FundoVisual_RadioButtons.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb_FundoVisual_RadioButtons.Location = new System.Drawing.Point(317, 7);
            this.txtb_FundoVisual_RadioButtons.MaxLength = 0;
            this.txtb_FundoVisual_RadioButtons.Multiline = true;
            this.txtb_FundoVisual_RadioButtons.Name = "txtb_FundoVisual_RadioButtons";
            this.txtb_FundoVisual_RadioButtons.Size = new System.Drawing.Size(316, 59);
            this.txtb_FundoVisual_RadioButtons.TabIndex = 58;
            // 
            // lb_Material_Ou_Produto
            // 
            this.lb_Material_Ou_Produto.AutoSize = true;
            this.lb_Material_Ou_Produto.BackColor = System.Drawing.Color.Transparent;
            this.lb_Material_Ou_Produto.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Material_Ou_Produto.Location = new System.Drawing.Point(3, 30);
            this.lb_Material_Ou_Produto.Name = "lb_Material_Ou_Produto";
            this.lb_Material_Ou_Produto.Size = new System.Drawing.Size(312, 18);
            this.lb_Material_Ou_Produto.TabIndex = 55;
            this.lb_Material_Ou_Produto.Text = "GRUPO DO MATERIAL OU PRODUTO :";
            // 
            // btn_PesquisaPorMaterial_Ou_Produto
            // 
            this.btn_PesquisaPorMaterial_Ou_Produto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_PesquisaPorMaterial_Ou_Produto.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_PesquisaPorMaterial_Ou_Produto.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_PesquisaPorMaterial_Ou_Produto.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_PesquisaPorMaterial_Ou_Produto.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_PesquisaPorMaterial_Ou_Produto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PesquisaPorMaterial_Ou_Produto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PesquisaPorMaterial_Ou_Produto.Image = global::GenOR.Properties.Resources.Yes;
            this.btn_PesquisaPorMaterial_Ou_Produto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_PesquisaPorMaterial_Ou_Produto.Location = new System.Drawing.Point(837, 7);
            this.btn_PesquisaPorMaterial_Ou_Produto.Name = "btn_PesquisaPorMaterial_Ou_Produto";
            this.btn_PesquisaPorMaterial_Ou_Produto.Size = new System.Drawing.Size(173, 59);
            this.btn_PesquisaPorMaterial_Ou_Produto.TabIndex = 9;
            this.btn_PesquisaPorMaterial_Ou_Produto.Text = "      PESQUISAR";
            this.btn_PesquisaPorMaterial_Ou_Produto.UseVisualStyleBackColor = false;
            this.btn_PesquisaPorMaterial_Ou_Produto.Click += new System.EventHandler(this.btn_PesquisaPorMaterial_Ou_Produto_Click);
            // 
            // FormTelaPesquisaGrupo_Unidade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1027, 108);
            this.Controls.Add(this.tc_Pesquisa);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormTelaPesquisaGrupo_Unidade";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PESQUISANDO GRUPO / UNIDADE";
            this.Load += new System.EventHandler(this.FormTelaPesquisaGrupo_Unidade_Load);
            this.tc_Pesquisa.ResumeLayout(false);
            this.tcp_Codigo.ResumeLayout(false);
            this.tcp_Codigo.PerformLayout();
            this.tcp_Sigla.ResumeLayout(false);
            this.tcp_Sigla.PerformLayout();
            this.tcp_Descricao.ResumeLayout(false);
            this.tcp_Descricao.PerformLayout();
            this.tcp_Material_Ou_Produto.ResumeLayout(false);
            this.tcp_Material_Ou_Produto.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tc_Pesquisa;
        private System.Windows.Forms.TabPage tcp_Codigo;
        private System.Windows.Forms.TextBox txtb_Codigo;
        private System.Windows.Forms.Label lb_Codigo;
        private System.Windows.Forms.Button btn_PesquisaPorCodigo;
        private System.Windows.Forms.TabPage tcp_Descricao;
        private System.Windows.Forms.TextBox txtb_Descricao;
        private System.Windows.Forms.Label lb_Descricao;
        private System.Windows.Forms.Button btn_PesquisaPorDescricao;
        private System.Windows.Forms.TabPage tcp_Sigla;
        private System.Windows.Forms.TextBox txtb_Sigla;
        private System.Windows.Forms.Label lb_Sigla;
        private System.Windows.Forms.Button btn_PesquisaPorSigla;
        private System.Windows.Forms.TabPage tcp_Material_Ou_Produto;
        private System.Windows.Forms.Label lb_Material_Ou_Produto;
        private System.Windows.Forms.Button btn_PesquisaPorMaterial_Ou_Produto;
        private System.Windows.Forms.RadioButton rb_Produto;
        private System.Windows.Forms.RadioButton rb_Material;
        private System.Windows.Forms.TextBox txtb_FundoVisual_RadioButtons;
    }
}