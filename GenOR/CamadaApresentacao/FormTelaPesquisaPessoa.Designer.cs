namespace GenOR
{
    partial class FormTelaPesquisaPessoa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTelaPesquisaPessoa));
            this.tc_Pesquisa = new System.Windows.Forms.TabControl();
            this.tcp_Codigo = new System.Windows.Forms.TabPage();
            this.txtb_Codigo = new System.Windows.Forms.TextBox();
            this.lb_Codigo = new System.Windows.Forms.Label();
            this.btn_PesquisaPorCodigo = new System.Windows.Forms.Button();
            this.tcp_RazaoSocial = new System.Windows.Forms.TabPage();
            this.txtb_RazaoSocial = new System.Windows.Forms.TextBox();
            this.lb_RazaoSocial = new System.Windows.Forms.Label();
            this.btn_PesquisaPorRazaoSocial = new System.Windows.Forms.Button();
            this.tcp_Descricao = new System.Windows.Forms.TabPage();
            this.txtb_NomeFantasia = new System.Windows.Forms.TextBox();
            this.lb_NomeFantasia = new System.Windows.Forms.Label();
            this.btn_PesquisaPorNomeFantasia = new System.Windows.Forms.Button();
            this.tcp_CPF_CNPJ = new System.Windows.Forms.TabPage();
            this.cb_CpfCnpj = new System.Windows.Forms.ComboBox();
            this.mtxtb_CpfCnpj = new System.Windows.Forms.MaskedTextBox();
            this.lb_CpfCnpj = new System.Windows.Forms.Label();
            this.btn_PesquisaPorCpfCnpj = new System.Windows.Forms.Button();
            this.tcp_InscricaoEstadual = new System.Windows.Forms.TabPage();
            this.mtxtb_InscricaoEstadual = new System.Windows.Forms.MaskedTextBox();
            this.lb_InscricaoEstadual = new System.Windows.Forms.Label();
            this.btn_PesquisaPorInscricaoEstadual = new System.Windows.Forms.Button();
            this.tcp_Email = new System.Windows.Forms.TabPage();
            this.txtb_Email = new System.Windows.Forms.TextBox();
            this.lb_Email = new System.Windows.Forms.Label();
            this.btn_PesquisaPorEmail = new System.Windows.Forms.Button();
            this.tcp_Estado = new System.Windows.Forms.TabPage();
            this.cb_Estado = new System.Windows.Forms.ComboBox();
            this.lb_Estado = new System.Windows.Forms.Label();
            this.btn_PesquisaPorEstado = new System.Windows.Forms.Button();
            this.tcp_Cidade = new System.Windows.Forms.TabPage();
            this.txtb_Cidade = new System.Windows.Forms.TextBox();
            this.lb_Cidade = new System.Windows.Forms.Label();
            this.btn_PesquisaPorCidade = new System.Windows.Forms.Button();
            this.tcp_DDD = new System.Windows.Forms.TabPage();
            this.txtb_DDD = new System.Windows.Forms.TextBox();
            this.lb_DDD = new System.Windows.Forms.Label();
            this.btn_PesquisaPorDDD = new System.Windows.Forms.Button();
            this.tcp_Telefone = new System.Windows.Forms.TabPage();
            this.txtb_Telefone = new System.Windows.Forms.TextBox();
            this.lb_Telefone = new System.Windows.Forms.Label();
            this.btn_PesquisaPorTelefone = new System.Windows.Forms.Button();
            this.tc_Pesquisa.SuspendLayout();
            this.tcp_Codigo.SuspendLayout();
            this.tcp_RazaoSocial.SuspendLayout();
            this.tcp_Descricao.SuspendLayout();
            this.tcp_CPF_CNPJ.SuspendLayout();
            this.tcp_InscricaoEstadual.SuspendLayout();
            this.tcp_Email.SuspendLayout();
            this.tcp_Estado.SuspendLayout();
            this.tcp_Cidade.SuspendLayout();
            this.tcp_DDD.SuspendLayout();
            this.tcp_Telefone.SuspendLayout();
            this.SuspendLayout();
            // 
            // tc_Pesquisa
            // 
            this.tc_Pesquisa.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tc_Pesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tc_Pesquisa.Controls.Add(this.tcp_Codigo);
            this.tc_Pesquisa.Controls.Add(this.tcp_RazaoSocial);
            this.tc_Pesquisa.Controls.Add(this.tcp_Descricao);
            this.tc_Pesquisa.Controls.Add(this.tcp_CPF_CNPJ);
            this.tc_Pesquisa.Controls.Add(this.tcp_InscricaoEstadual);
            this.tc_Pesquisa.Controls.Add(this.tcp_Email);
            this.tc_Pesquisa.Controls.Add(this.tcp_Estado);
            this.tc_Pesquisa.Controls.Add(this.tcp_Cidade);
            this.tc_Pesquisa.Controls.Add(this.tcp_DDD);
            this.tc_Pesquisa.Controls.Add(this.tcp_Telefone);
            this.tc_Pesquisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.tc_Pesquisa.HotTrack = true;
            this.tc_Pesquisa.Location = new System.Drawing.Point(0, 2);
            this.tc_Pesquisa.Name = "tc_Pesquisa";
            this.tc_Pesquisa.SelectedIndex = 0;
            this.tc_Pesquisa.Size = new System.Drawing.Size(1027, 105);
            this.tc_Pesquisa.TabIndex = 2;
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
            // tcp_RazaoSocial
            // 
            this.tcp_RazaoSocial.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tcp_RazaoSocial.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tcp_RazaoSocial.Controls.Add(this.txtb_RazaoSocial);
            this.tcp_RazaoSocial.Controls.Add(this.lb_RazaoSocial);
            this.tcp_RazaoSocial.Controls.Add(this.btn_PesquisaPorRazaoSocial);
            this.tcp_RazaoSocial.Location = new System.Drawing.Point(4, 4);
            this.tcp_RazaoSocial.Name = "tcp_RazaoSocial";
            this.tcp_RazaoSocial.Size = new System.Drawing.Size(1019, 76);
            this.tcp_RazaoSocial.TabIndex = 2;
            this.tcp_RazaoSocial.Text = "RAZÃO SOCIAL";
            // 
            // txtb_RazaoSocial
            // 
            this.txtb_RazaoSocial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtb_RazaoSocial.BackColor = System.Drawing.Color.White;
            this.txtb_RazaoSocial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb_RazaoSocial.Location = new System.Drawing.Point(142, 26);
            this.txtb_RazaoSocial.MaxLength = 150;
            this.txtb_RazaoSocial.Name = "txtb_RazaoSocial";
            this.txtb_RazaoSocial.Size = new System.Drawing.Size(689, 26);
            this.txtb_RazaoSocial.TabIndex = 3;
            this.txtb_RazaoSocial.WordWrap = false;
            this.txtb_RazaoSocial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtb_RazaoSocial_KeyPress);
            // 
            // lb_RazaoSocial
            // 
            this.lb_RazaoSocial.AutoSize = true;
            this.lb_RazaoSocial.BackColor = System.Drawing.Color.Transparent;
            this.lb_RazaoSocial.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_RazaoSocial.Location = new System.Drawing.Point(3, 30);
            this.lb_RazaoSocial.Name = "lb_RazaoSocial";
            this.lb_RazaoSocial.Size = new System.Drawing.Size(137, 18);
            this.lb_RazaoSocial.TabIndex = 55;
            this.lb_RazaoSocial.Text = "RAZÃO SOCIAL :";
            // 
            // btn_PesquisaPorRazaoSocial
            // 
            this.btn_PesquisaPorRazaoSocial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_PesquisaPorRazaoSocial.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_PesquisaPorRazaoSocial.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_PesquisaPorRazaoSocial.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_PesquisaPorRazaoSocial.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_PesquisaPorRazaoSocial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PesquisaPorRazaoSocial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PesquisaPorRazaoSocial.Image = global::GenOR.Properties.Resources.Yes;
            this.btn_PesquisaPorRazaoSocial.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_PesquisaPorRazaoSocial.Location = new System.Drawing.Point(837, 7);
            this.btn_PesquisaPorRazaoSocial.Name = "btn_PesquisaPorRazaoSocial";
            this.btn_PesquisaPorRazaoSocial.Size = new System.Drawing.Size(173, 59);
            this.btn_PesquisaPorRazaoSocial.TabIndex = 4;
            this.btn_PesquisaPorRazaoSocial.Text = "      PESQUISAR";
            this.btn_PesquisaPorRazaoSocial.UseVisualStyleBackColor = false;
            this.btn_PesquisaPorRazaoSocial.Click += new System.EventHandler(this.btn_PesquisaPorRazaoSocial_Click);
            // 
            // tcp_Descricao
            // 
            this.tcp_Descricao.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tcp_Descricao.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tcp_Descricao.Controls.Add(this.txtb_NomeFantasia);
            this.tcp_Descricao.Controls.Add(this.lb_NomeFantasia);
            this.tcp_Descricao.Controls.Add(this.btn_PesquisaPorNomeFantasia);
            this.tcp_Descricao.Location = new System.Drawing.Point(4, 4);
            this.tcp_Descricao.Name = "tcp_Descricao";
            this.tcp_Descricao.Size = new System.Drawing.Size(1019, 76);
            this.tcp_Descricao.TabIndex = 1;
            this.tcp_Descricao.Text = "NOME FANTASIA";
            // 
            // txtb_NomeFantasia
            // 
            this.txtb_NomeFantasia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtb_NomeFantasia.BackColor = System.Drawing.Color.White;
            this.txtb_NomeFantasia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb_NomeFantasia.Location = new System.Drawing.Point(155, 26);
            this.txtb_NomeFantasia.MaxLength = 150;
            this.txtb_NomeFantasia.Name = "txtb_NomeFantasia";
            this.txtb_NomeFantasia.Size = new System.Drawing.Size(676, 26);
            this.txtb_NomeFantasia.TabIndex = 5;
            this.txtb_NomeFantasia.WordWrap = false;
            this.txtb_NomeFantasia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtb_NomeFantasia_KeyPress);
            // 
            // lb_NomeFantasia
            // 
            this.lb_NomeFantasia.AutoSize = true;
            this.lb_NomeFantasia.BackColor = System.Drawing.Color.Transparent;
            this.lb_NomeFantasia.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_NomeFantasia.Location = new System.Drawing.Point(3, 30);
            this.lb_NomeFantasia.Name = "lb_NomeFantasia";
            this.lb_NomeFantasia.Size = new System.Drawing.Size(150, 18);
            this.lb_NomeFantasia.TabIndex = 55;
            this.lb_NomeFantasia.Text = "NOME FANTASIA :";
            // 
            // btn_PesquisaPorNomeFantasia
            // 
            this.btn_PesquisaPorNomeFantasia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_PesquisaPorNomeFantasia.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_PesquisaPorNomeFantasia.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_PesquisaPorNomeFantasia.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_PesquisaPorNomeFantasia.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_PesquisaPorNomeFantasia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PesquisaPorNomeFantasia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PesquisaPorNomeFantasia.Image = global::GenOR.Properties.Resources.Yes;
            this.btn_PesquisaPorNomeFantasia.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_PesquisaPorNomeFantasia.Location = new System.Drawing.Point(837, 7);
            this.btn_PesquisaPorNomeFantasia.Name = "btn_PesquisaPorNomeFantasia";
            this.btn_PesquisaPorNomeFantasia.Size = new System.Drawing.Size(173, 59);
            this.btn_PesquisaPorNomeFantasia.TabIndex = 6;
            this.btn_PesquisaPorNomeFantasia.Text = "      PESQUISAR";
            this.btn_PesquisaPorNomeFantasia.UseVisualStyleBackColor = false;
            this.btn_PesquisaPorNomeFantasia.Click += new System.EventHandler(this.btn_PesquisaPorNomeFantasia_Click);
            // 
            // tcp_CPF_CNPJ
            // 
            this.tcp_CPF_CNPJ.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tcp_CPF_CNPJ.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tcp_CPF_CNPJ.Controls.Add(this.cb_CpfCnpj);
            this.tcp_CPF_CNPJ.Controls.Add(this.mtxtb_CpfCnpj);
            this.tcp_CPF_CNPJ.Controls.Add(this.lb_CpfCnpj);
            this.tcp_CPF_CNPJ.Controls.Add(this.btn_PesquisaPorCpfCnpj);
            this.tcp_CPF_CNPJ.Location = new System.Drawing.Point(4, 4);
            this.tcp_CPF_CNPJ.Name = "tcp_CPF_CNPJ";
            this.tcp_CPF_CNPJ.Size = new System.Drawing.Size(1019, 76);
            this.tcp_CPF_CNPJ.TabIndex = 3;
            this.tcp_CPF_CNPJ.Text = "CPF / CNPJ";
            // 
            // cb_CpfCnpj
            // 
            this.cb_CpfCnpj.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.cb_CpfCnpj.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_CpfCnpj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_CpfCnpj.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_CpfCnpj.Items.AddRange(new object[] {
            "CPF",
            "CNPJ"});
            this.cb_CpfCnpj.Location = new System.Drawing.Point(116, 26);
            this.cb_CpfCnpj.MaxDropDownItems = 2;
            this.cb_CpfCnpj.Name = "cb_CpfCnpj";
            this.cb_CpfCnpj.Size = new System.Drawing.Size(71, 26);
            this.cb_CpfCnpj.TabIndex = 7;
            this.cb_CpfCnpj.SelectedIndexChanged += new System.EventHandler(this.cb_CpfCnpj_SelectedIndexChanged);
            // 
            // mtxtb_CpfCnpj
            // 
            this.mtxtb_CpfCnpj.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtb_CpfCnpj.Location = new System.Drawing.Point(193, 26);
            this.mtxtb_CpfCnpj.Mask = "000,000,000-00";
            this.mtxtb_CpfCnpj.Name = "mtxtb_CpfCnpj";
            this.mtxtb_CpfCnpj.RejectInputOnFirstFailure = true;
            this.mtxtb_CpfCnpj.Size = new System.Drawing.Size(150, 26);
            this.mtxtb_CpfCnpj.TabIndex = 8;
            this.mtxtb_CpfCnpj.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mtxtb_CpfCnpj_KeyPress);
            // 
            // lb_CpfCnpj
            // 
            this.lb_CpfCnpj.AutoSize = true;
            this.lb_CpfCnpj.BackColor = System.Drawing.Color.Transparent;
            this.lb_CpfCnpj.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_CpfCnpj.Location = new System.Drawing.Point(3, 30);
            this.lb_CpfCnpj.Name = "lb_CpfCnpj";
            this.lb_CpfCnpj.Size = new System.Drawing.Size(110, 18);
            this.lb_CpfCnpj.TabIndex = 55;
            this.lb_CpfCnpj.Text = "CPF / CNPJ :";
            // 
            // btn_PesquisaPorCpfCnpj
            // 
            this.btn_PesquisaPorCpfCnpj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_PesquisaPorCpfCnpj.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_PesquisaPorCpfCnpj.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_PesquisaPorCpfCnpj.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_PesquisaPorCpfCnpj.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_PesquisaPorCpfCnpj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PesquisaPorCpfCnpj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PesquisaPorCpfCnpj.Image = global::GenOR.Properties.Resources.Yes;
            this.btn_PesquisaPorCpfCnpj.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_PesquisaPorCpfCnpj.Location = new System.Drawing.Point(837, 7);
            this.btn_PesquisaPorCpfCnpj.Name = "btn_PesquisaPorCpfCnpj";
            this.btn_PesquisaPorCpfCnpj.Size = new System.Drawing.Size(173, 59);
            this.btn_PesquisaPorCpfCnpj.TabIndex = 9;
            this.btn_PesquisaPorCpfCnpj.Text = "      PESQUISAR";
            this.btn_PesquisaPorCpfCnpj.UseVisualStyleBackColor = false;
            this.btn_PesquisaPorCpfCnpj.Click += new System.EventHandler(this.btn_PesquisaPorCpfCnpj_Click);
            // 
            // tcp_InscricaoEstadual
            // 
            this.tcp_InscricaoEstadual.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tcp_InscricaoEstadual.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tcp_InscricaoEstadual.Controls.Add(this.mtxtb_InscricaoEstadual);
            this.tcp_InscricaoEstadual.Controls.Add(this.lb_InscricaoEstadual);
            this.tcp_InscricaoEstadual.Controls.Add(this.btn_PesquisaPorInscricaoEstadual);
            this.tcp_InscricaoEstadual.Location = new System.Drawing.Point(4, 4);
            this.tcp_InscricaoEstadual.Name = "tcp_InscricaoEstadual";
            this.tcp_InscricaoEstadual.Size = new System.Drawing.Size(1019, 76);
            this.tcp_InscricaoEstadual.TabIndex = 5;
            this.tcp_InscricaoEstadual.Text = "INSCRIÇÃO ESTADUAL";
            // 
            // mtxtb_InscricaoEstadual
            // 
            this.mtxtb_InscricaoEstadual.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtb_InscricaoEstadual.Location = new System.Drawing.Point(203, 26);
            this.mtxtb_InscricaoEstadual.Mask = "000,000,000,000";
            this.mtxtb_InscricaoEstadual.Name = "mtxtb_InscricaoEstadual";
            this.mtxtb_InscricaoEstadual.RejectInputOnFirstFailure = true;
            this.mtxtb_InscricaoEstadual.Size = new System.Drawing.Size(133, 26);
            this.mtxtb_InscricaoEstadual.TabIndex = 10;
            this.mtxtb_InscricaoEstadual.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtxtb_InscricaoEstadual.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mtxtb_InscricaoEstadual_KeyPress);
            // 
            // lb_InscricaoEstadual
            // 
            this.lb_InscricaoEstadual.AutoSize = true;
            this.lb_InscricaoEstadual.BackColor = System.Drawing.Color.Transparent;
            this.lb_InscricaoEstadual.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_InscricaoEstadual.Location = new System.Drawing.Point(3, 30);
            this.lb_InscricaoEstadual.Name = "lb_InscricaoEstadual";
            this.lb_InscricaoEstadual.Size = new System.Drawing.Size(198, 18);
            this.lb_InscricaoEstadual.TabIndex = 55;
            this.lb_InscricaoEstadual.Text = "INSCRIÇÃO ESTADUAL :";
            // 
            // btn_PesquisaPorInscricaoEstadual
            // 
            this.btn_PesquisaPorInscricaoEstadual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_PesquisaPorInscricaoEstadual.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_PesquisaPorInscricaoEstadual.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_PesquisaPorInscricaoEstadual.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_PesquisaPorInscricaoEstadual.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_PesquisaPorInscricaoEstadual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PesquisaPorInscricaoEstadual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PesquisaPorInscricaoEstadual.Image = global::GenOR.Properties.Resources.Yes;
            this.btn_PesquisaPorInscricaoEstadual.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_PesquisaPorInscricaoEstadual.Location = new System.Drawing.Point(837, 7);
            this.btn_PesquisaPorInscricaoEstadual.Name = "btn_PesquisaPorInscricaoEstadual";
            this.btn_PesquisaPorInscricaoEstadual.Size = new System.Drawing.Size(173, 59);
            this.btn_PesquisaPorInscricaoEstadual.TabIndex = 11;
            this.btn_PesquisaPorInscricaoEstadual.Text = "      PESQUISAR";
            this.btn_PesquisaPorInscricaoEstadual.UseVisualStyleBackColor = false;
            this.btn_PesquisaPorInscricaoEstadual.Click += new System.EventHandler(this.btn_PesquisaPorInscricaoEstadual_Click);
            // 
            // tcp_Email
            // 
            this.tcp_Email.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tcp_Email.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tcp_Email.Controls.Add(this.txtb_Email);
            this.tcp_Email.Controls.Add(this.lb_Email);
            this.tcp_Email.Controls.Add(this.btn_PesquisaPorEmail);
            this.tcp_Email.Location = new System.Drawing.Point(4, 4);
            this.tcp_Email.Name = "tcp_Email";
            this.tcp_Email.Size = new System.Drawing.Size(1019, 76);
            this.tcp_Email.TabIndex = 4;
            this.tcp_Email.Text = "EMAIL";
            // 
            // txtb_Email
            // 
            this.txtb_Email.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtb_Email.BackColor = System.Drawing.Color.White;
            this.txtb_Email.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb_Email.Location = new System.Drawing.Point(71, 26);
            this.txtb_Email.MaxLength = 256;
            this.txtb_Email.Name = "txtb_Email";
            this.txtb_Email.Size = new System.Drawing.Size(760, 26);
            this.txtb_Email.TabIndex = 12;
            this.txtb_Email.WordWrap = false;
            this.txtb_Email.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtb_Email_KeyPress);
            // 
            // lb_Email
            // 
            this.lb_Email.AutoSize = true;
            this.lb_Email.BackColor = System.Drawing.Color.Transparent;
            this.lb_Email.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Email.Location = new System.Drawing.Point(3, 30);
            this.lb_Email.Name = "lb_Email";
            this.lb_Email.Size = new System.Drawing.Size(66, 18);
            this.lb_Email.TabIndex = 55;
            this.lb_Email.Text = "EMAIL :";
            // 
            // btn_PesquisaPorEmail
            // 
            this.btn_PesquisaPorEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_PesquisaPorEmail.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_PesquisaPorEmail.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_PesquisaPorEmail.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_PesquisaPorEmail.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_PesquisaPorEmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PesquisaPorEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PesquisaPorEmail.Image = global::GenOR.Properties.Resources.Yes;
            this.btn_PesquisaPorEmail.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_PesquisaPorEmail.Location = new System.Drawing.Point(837, 7);
            this.btn_PesquisaPorEmail.Name = "btn_PesquisaPorEmail";
            this.btn_PesquisaPorEmail.Size = new System.Drawing.Size(173, 59);
            this.btn_PesquisaPorEmail.TabIndex = 13;
            this.btn_PesquisaPorEmail.Text = "      PESQUISAR";
            this.btn_PesquisaPorEmail.UseVisualStyleBackColor = false;
            this.btn_PesquisaPorEmail.Click += new System.EventHandler(this.btn_PesquisaPorEmail_Click);
            // 
            // tcp_Estado
            // 
            this.tcp_Estado.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tcp_Estado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tcp_Estado.Controls.Add(this.cb_Estado);
            this.tcp_Estado.Controls.Add(this.lb_Estado);
            this.tcp_Estado.Controls.Add(this.btn_PesquisaPorEstado);
            this.tcp_Estado.Location = new System.Drawing.Point(4, 4);
            this.tcp_Estado.Name = "tcp_Estado";
            this.tcp_Estado.Size = new System.Drawing.Size(1019, 76);
            this.tcp_Estado.TabIndex = 9;
            this.tcp_Estado.Text = "ESTADO";
            // 
            // cb_Estado
            // 
            this.cb_Estado.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.cb_Estado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Estado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_Estado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Estado.Items.AddRange(new object[] {
            "AC",
            "AL",
            "AP",
            "AM",
            "BA",
            "CE",
            "DF",
            "ES",
            "GO",
            "MA",
            "MT",
            "MS",
            "MG",
            "PA",
            "PB",
            "PR",
            "PE",
            "PI",
            "RJ",
            "RN",
            "RS",
            "RO",
            "RR",
            "SC",
            "SP",
            "SE",
            "TO"});
            this.cb_Estado.Location = new System.Drawing.Point(130, 26);
            this.cb_Estado.MaxDropDownItems = 2;
            this.cb_Estado.MaxLength = 2;
            this.cb_Estado.Name = "cb_Estado";
            this.cb_Estado.Size = new System.Drawing.Size(71, 28);
            this.cb_Estado.TabIndex = 14;
            // 
            // lb_Estado
            // 
            this.lb_Estado.AutoSize = true;
            this.lb_Estado.BackColor = System.Drawing.Color.Transparent;
            this.lb_Estado.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Estado.Location = new System.Drawing.Point(3, 30);
            this.lb_Estado.Name = "lb_Estado";
            this.lb_Estado.Size = new System.Drawing.Size(124, 18);
            this.lb_Estado.TabIndex = 55;
            this.lb_Estado.Text = "ESTADO (UF) :";
            // 
            // btn_PesquisaPorEstado
            // 
            this.btn_PesquisaPorEstado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_PesquisaPorEstado.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_PesquisaPorEstado.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_PesquisaPorEstado.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_PesquisaPorEstado.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_PesquisaPorEstado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PesquisaPorEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PesquisaPorEstado.Image = global::GenOR.Properties.Resources.Yes;
            this.btn_PesquisaPorEstado.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_PesquisaPorEstado.Location = new System.Drawing.Point(837, 7);
            this.btn_PesquisaPorEstado.Name = "btn_PesquisaPorEstado";
            this.btn_PesquisaPorEstado.Size = new System.Drawing.Size(173, 59);
            this.btn_PesquisaPorEstado.TabIndex = 15;
            this.btn_PesquisaPorEstado.Text = "      PESQUISAR";
            this.btn_PesquisaPorEstado.UseVisualStyleBackColor = false;
            this.btn_PesquisaPorEstado.Click += new System.EventHandler(this.btn_PesquisaPorEstado_Click);
            // 
            // tcp_Cidade
            // 
            this.tcp_Cidade.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tcp_Cidade.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tcp_Cidade.Controls.Add(this.txtb_Cidade);
            this.tcp_Cidade.Controls.Add(this.lb_Cidade);
            this.tcp_Cidade.Controls.Add(this.btn_PesquisaPorCidade);
            this.tcp_Cidade.Location = new System.Drawing.Point(4, 4);
            this.tcp_Cidade.Name = "tcp_Cidade";
            this.tcp_Cidade.Size = new System.Drawing.Size(1019, 76);
            this.tcp_Cidade.TabIndex = 8;
            this.tcp_Cidade.Text = "CIDADE";
            // 
            // txtb_Cidade
            // 
            this.txtb_Cidade.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtb_Cidade.BackColor = System.Drawing.Color.White;
            this.txtb_Cidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb_Cidade.Location = new System.Drawing.Point(84, 26);
            this.txtb_Cidade.MaxLength = 150;
            this.txtb_Cidade.Name = "txtb_Cidade";
            this.txtb_Cidade.Size = new System.Drawing.Size(747, 26);
            this.txtb_Cidade.TabIndex = 16;
            this.txtb_Cidade.WordWrap = false;
            this.txtb_Cidade.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtb_Cidade_KeyPress);
            // 
            // lb_Cidade
            // 
            this.lb_Cidade.AutoSize = true;
            this.lb_Cidade.BackColor = System.Drawing.Color.Transparent;
            this.lb_Cidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Cidade.Location = new System.Drawing.Point(3, 30);
            this.lb_Cidade.Name = "lb_Cidade";
            this.lb_Cidade.Size = new System.Drawing.Size(79, 18);
            this.lb_Cidade.TabIndex = 55;
            this.lb_Cidade.Text = "CIDADE :";
            // 
            // btn_PesquisaPorCidade
            // 
            this.btn_PesquisaPorCidade.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_PesquisaPorCidade.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_PesquisaPorCidade.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_PesquisaPorCidade.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_PesquisaPorCidade.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_PesquisaPorCidade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PesquisaPorCidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PesquisaPorCidade.Image = global::GenOR.Properties.Resources.Yes;
            this.btn_PesquisaPorCidade.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_PesquisaPorCidade.Location = new System.Drawing.Point(837, 7);
            this.btn_PesquisaPorCidade.Name = "btn_PesquisaPorCidade";
            this.btn_PesquisaPorCidade.Size = new System.Drawing.Size(173, 59);
            this.btn_PesquisaPorCidade.TabIndex = 17;
            this.btn_PesquisaPorCidade.Text = "      PESQUISAR";
            this.btn_PesquisaPorCidade.UseVisualStyleBackColor = false;
            this.btn_PesquisaPorCidade.Click += new System.EventHandler(this.btn_PesquisaPorCidade_Click);
            // 
            // tcp_DDD
            // 
            this.tcp_DDD.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tcp_DDD.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tcp_DDD.Controls.Add(this.txtb_DDD);
            this.tcp_DDD.Controls.Add(this.lb_DDD);
            this.tcp_DDD.Controls.Add(this.btn_PesquisaPorDDD);
            this.tcp_DDD.Location = new System.Drawing.Point(4, 4);
            this.tcp_DDD.Name = "tcp_DDD";
            this.tcp_DDD.Size = new System.Drawing.Size(1019, 76);
            this.tcp_DDD.TabIndex = 7;
            this.tcp_DDD.Text = "DDD";
            // 
            // txtb_DDD
            // 
            this.txtb_DDD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtb_DDD.BackColor = System.Drawing.Color.White;
            this.txtb_DDD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb_DDD.Location = new System.Drawing.Point(59, 26);
            this.txtb_DDD.MaxLength = 2;
            this.txtb_DDD.Name = "txtb_DDD";
            this.txtb_DDD.Size = new System.Drawing.Size(46, 26);
            this.txtb_DDD.TabIndex = 18;
            this.txtb_DDD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtb_DDD.WordWrap = false;
            this.txtb_DDD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtb_DDD_KeyPress);
            // 
            // lb_DDD
            // 
            this.lb_DDD.AutoSize = true;
            this.lb_DDD.BackColor = System.Drawing.Color.Transparent;
            this.lb_DDD.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_DDD.Location = new System.Drawing.Point(3, 30);
            this.lb_DDD.Name = "lb_DDD";
            this.lb_DDD.Size = new System.Drawing.Size(54, 18);
            this.lb_DDD.TabIndex = 55;
            this.lb_DDD.Text = "DDD :";
            // 
            // btn_PesquisaPorDDD
            // 
            this.btn_PesquisaPorDDD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_PesquisaPorDDD.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_PesquisaPorDDD.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_PesquisaPorDDD.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_PesquisaPorDDD.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_PesquisaPorDDD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PesquisaPorDDD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PesquisaPorDDD.Image = global::GenOR.Properties.Resources.Yes;
            this.btn_PesquisaPorDDD.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_PesquisaPorDDD.Location = new System.Drawing.Point(837, 7);
            this.btn_PesquisaPorDDD.Name = "btn_PesquisaPorDDD";
            this.btn_PesquisaPorDDD.Size = new System.Drawing.Size(173, 59);
            this.btn_PesquisaPorDDD.TabIndex = 19;
            this.btn_PesquisaPorDDD.Text = "      PESQUISAR";
            this.btn_PesquisaPorDDD.UseVisualStyleBackColor = false;
            this.btn_PesquisaPorDDD.Click += new System.EventHandler(this.btn_PesquisaPorDDD_Click);
            // 
            // tcp_Telefone
            // 
            this.tcp_Telefone.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tcp_Telefone.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tcp_Telefone.Controls.Add(this.txtb_Telefone);
            this.tcp_Telefone.Controls.Add(this.lb_Telefone);
            this.tcp_Telefone.Controls.Add(this.btn_PesquisaPorTelefone);
            this.tcp_Telefone.Location = new System.Drawing.Point(4, 4);
            this.tcp_Telefone.Name = "tcp_Telefone";
            this.tcp_Telefone.Size = new System.Drawing.Size(1019, 76);
            this.tcp_Telefone.TabIndex = 6;
            this.tcp_Telefone.Text = "TELEFONE";
            // 
            // txtb_Telefone
            // 
            this.txtb_Telefone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtb_Telefone.BackColor = System.Drawing.Color.White;
            this.txtb_Telefone.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb_Telefone.Location = new System.Drawing.Point(110, 26);
            this.txtb_Telefone.MaxLength = 11;
            this.txtb_Telefone.Name = "txtb_Telefone";
            this.txtb_Telefone.Size = new System.Drawing.Size(721, 26);
            this.txtb_Telefone.TabIndex = 20;
            this.txtb_Telefone.WordWrap = false;
            this.txtb_Telefone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtb_Telefone_KeyPress);
            // 
            // lb_Telefone
            // 
            this.lb_Telefone.AutoSize = true;
            this.lb_Telefone.BackColor = System.Drawing.Color.Transparent;
            this.lb_Telefone.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Telefone.Location = new System.Drawing.Point(3, 30);
            this.lb_Telefone.Name = "lb_Telefone";
            this.lb_Telefone.Size = new System.Drawing.Size(105, 18);
            this.lb_Telefone.TabIndex = 55;
            this.lb_Telefone.Text = "TELEFONE :";
            // 
            // btn_PesquisaPorTelefone
            // 
            this.btn_PesquisaPorTelefone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_PesquisaPorTelefone.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_PesquisaPorTelefone.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_PesquisaPorTelefone.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_PesquisaPorTelefone.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_PesquisaPorTelefone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PesquisaPorTelefone.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PesquisaPorTelefone.Image = global::GenOR.Properties.Resources.Yes;
            this.btn_PesquisaPorTelefone.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_PesquisaPorTelefone.Location = new System.Drawing.Point(837, 7);
            this.btn_PesquisaPorTelefone.Name = "btn_PesquisaPorTelefone";
            this.btn_PesquisaPorTelefone.Size = new System.Drawing.Size(173, 59);
            this.btn_PesquisaPorTelefone.TabIndex = 21;
            this.btn_PesquisaPorTelefone.Text = "      PESQUISAR";
            this.btn_PesquisaPorTelefone.UseVisualStyleBackColor = false;
            this.btn_PesquisaPorTelefone.Click += new System.EventHandler(this.btn_PesquisaPorTelefone_Click);
            // 
            // FormTelaPesquisaPessoa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1027, 108);
            this.Controls.Add(this.tc_Pesquisa);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormTelaPesquisaPessoa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PESQUISANDO PESSOA";
            this.Load += new System.EventHandler(this.FormTelaPesquisaPessoa_Load);
            this.tc_Pesquisa.ResumeLayout(false);
            this.tcp_Codigo.ResumeLayout(false);
            this.tcp_Codigo.PerformLayout();
            this.tcp_RazaoSocial.ResumeLayout(false);
            this.tcp_RazaoSocial.PerformLayout();
            this.tcp_Descricao.ResumeLayout(false);
            this.tcp_Descricao.PerformLayout();
            this.tcp_CPF_CNPJ.ResumeLayout(false);
            this.tcp_CPF_CNPJ.PerformLayout();
            this.tcp_InscricaoEstadual.ResumeLayout(false);
            this.tcp_InscricaoEstadual.PerformLayout();
            this.tcp_Email.ResumeLayout(false);
            this.tcp_Email.PerformLayout();
            this.tcp_Estado.ResumeLayout(false);
            this.tcp_Estado.PerformLayout();
            this.tcp_Cidade.ResumeLayout(false);
            this.tcp_Cidade.PerformLayout();
            this.tcp_DDD.ResumeLayout(false);
            this.tcp_DDD.PerformLayout();
            this.tcp_Telefone.ResumeLayout(false);
            this.tcp_Telefone.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tc_Pesquisa;
        private System.Windows.Forms.TabPage tcp_Codigo;
        private System.Windows.Forms.TextBox txtb_Codigo;
        private System.Windows.Forms.Label lb_Codigo;
        private System.Windows.Forms.Button btn_PesquisaPorCodigo;
        private System.Windows.Forms.TabPage tcp_RazaoSocial;
        private System.Windows.Forms.TextBox txtb_RazaoSocial;
        private System.Windows.Forms.Label lb_RazaoSocial;
        private System.Windows.Forms.Button btn_PesquisaPorRazaoSocial;
        private System.Windows.Forms.TabPage tcp_Descricao;
        private System.Windows.Forms.TextBox txtb_NomeFantasia;
        private System.Windows.Forms.Label lb_NomeFantasia;
        private System.Windows.Forms.Button btn_PesquisaPorNomeFantasia;
        private System.Windows.Forms.TabPage tcp_CPF_CNPJ;
        private System.Windows.Forms.Label lb_CpfCnpj;
        private System.Windows.Forms.Button btn_PesquisaPorCpfCnpj;
        private System.Windows.Forms.TabPage tcp_Email;
        private System.Windows.Forms.TabPage tcp_InscricaoEstadual;
        private System.Windows.Forms.TabPage tcp_Estado;
        private System.Windows.Forms.TabPage tcp_Cidade;
        private System.Windows.Forms.TabPage tcp_DDD;
        private System.Windows.Forms.TabPage tcp_Telefone;
        private System.Windows.Forms.Label lb_InscricaoEstadual;
        private System.Windows.Forms.Button btn_PesquisaPorInscricaoEstadual;
        private System.Windows.Forms.TextBox txtb_Email;
        private System.Windows.Forms.Label lb_Email;
        private System.Windows.Forms.Button btn_PesquisaPorEmail;
        private System.Windows.Forms.Label lb_Estado;
        private System.Windows.Forms.Button btn_PesquisaPorEstado;
        private System.Windows.Forms.TextBox txtb_Cidade;
        private System.Windows.Forms.Label lb_Cidade;
        private System.Windows.Forms.Button btn_PesquisaPorCidade;
        private System.Windows.Forms.TextBox txtb_DDD;
        private System.Windows.Forms.Label lb_DDD;
        private System.Windows.Forms.Button btn_PesquisaPorDDD;
        private System.Windows.Forms.TextBox txtb_Telefone;
        private System.Windows.Forms.Label lb_Telefone;
        private System.Windows.Forms.Button btn_PesquisaPorTelefone;
        private System.Windows.Forms.MaskedTextBox mtxtb_CpfCnpj;
        private System.Windows.Forms.MaskedTextBox mtxtb_InscricaoEstadual;
        private System.Windows.Forms.ComboBox cb_CpfCnpj;
        private System.Windows.Forms.ComboBox cb_Estado;
    }
}