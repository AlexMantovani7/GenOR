namespace GenOR
{
    partial class FormTelaPesquisaLOG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTelaPesquisaLOG));
            this.tc_Pesquisa = new System.Windows.Forms.TabControl();
            this.tcp_Codigo = new System.Windows.Forms.TabPage();
            this.txtb_CodigoLOG = new System.Windows.Forms.TextBox();
            this.lb_CodigoLOG = new System.Windows.Forms.Label();
            this.btn_PesquisaPorCodigo = new System.Windows.Forms.Button();
            this.tcp_DataRegistro = new System.Windows.Forms.TabPage();
            this.btn_FecharCalendarioDataRegistroFinal = new System.Windows.Forms.Button();
            this.btn_FecharCalendarioDataRegistroInicial = new System.Windows.Forms.Button();
            this.btn_DataRegistroInicial = new System.Windows.Forms.Button();
            this.btn_DataRegistroFinal = new System.Windows.Forms.Button();
            this.mtxtb_DataRegistroFinal = new System.Windows.Forms.MaskedTextBox();
            this.lb_DataRegistro_Final = new System.Windows.Forms.Label();
            this.mtxtb_DataRegistroInicial = new System.Windows.Forms.MaskedTextBox();
            this.lb_DataRegistro = new System.Windows.Forms.Label();
            this.btn_PesquisaPorDataRegistro = new System.Windows.Forms.Button();
            this.tcp_Operacao = new System.Windows.Forms.TabPage();
            this.txtb_Operacao = new System.Windows.Forms.TextBox();
            this.lb_Operacao = new System.Windows.Forms.Label();
            this.btn_PesquisaPorOperacao = new System.Windows.Forms.Button();
            this.tcp_Registro = new System.Windows.Forms.TabPage();
            this.txtb_Registro = new System.Windows.Forms.TextBox();
            this.lb_Registro = new System.Windows.Forms.Label();
            this.btn_PesquisaPorRegistro = new System.Windows.Forms.Button();
            this.mc_Calendario = new System.Windows.Forms.MonthCalendar();
            this.tc_Pesquisa.SuspendLayout();
            this.tcp_Codigo.SuspendLayout();
            this.tcp_DataRegistro.SuspendLayout();
            this.tcp_Operacao.SuspendLayout();
            this.tcp_Registro.SuspendLayout();
            this.SuspendLayout();
            // 
            // tc_Pesquisa
            // 
            this.tc_Pesquisa.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tc_Pesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tc_Pesquisa.Controls.Add(this.tcp_Codigo);
            this.tc_Pesquisa.Controls.Add(this.tcp_DataRegistro);
            this.tc_Pesquisa.Controls.Add(this.tcp_Operacao);
            this.tc_Pesquisa.Controls.Add(this.tcp_Registro);
            this.tc_Pesquisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.tc_Pesquisa.HotTrack = true;
            this.tc_Pesquisa.Location = new System.Drawing.Point(-1, 1);
            this.tc_Pesquisa.Name = "tc_Pesquisa";
            this.tc_Pesquisa.SelectedIndex = 0;
            this.tc_Pesquisa.Size = new System.Drawing.Size(1027, 105);
            this.tc_Pesquisa.TabIndex = 1;
            this.tc_Pesquisa.TabStop = false;
            this.tc_Pesquisa.SelectedIndexChanged += new System.EventHandler(this.tc_Pesquisa_SelectedIndexChanged);
            // 
            // tcp_Codigo
            // 
            this.tcp_Codigo.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tcp_Codigo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tcp_Codigo.Controls.Add(this.txtb_CodigoLOG);
            this.tcp_Codigo.Controls.Add(this.lb_CodigoLOG);
            this.tcp_Codigo.Controls.Add(this.btn_PesquisaPorCodigo);
            this.tcp_Codigo.Location = new System.Drawing.Point(4, 4);
            this.tcp_Codigo.Name = "tcp_Codigo";
            this.tcp_Codigo.Padding = new System.Windows.Forms.Padding(3);
            this.tcp_Codigo.Size = new System.Drawing.Size(1019, 76);
            this.tcp_Codigo.TabIndex = 0;
            this.tcp_Codigo.Text = "CÓDIGO";
            // 
            // txtb_CodigoLOG
            // 
            this.txtb_CodigoLOG.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtb_CodigoLOG.BackColor = System.Drawing.Color.White;
            this.txtb_CodigoLOG.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb_CodigoLOG.Location = new System.Drawing.Point(89, 24);
            this.txtb_CodigoLOG.Name = "txtb_CodigoLOG";
            this.txtb_CodigoLOG.Size = new System.Drawing.Size(743, 26);
            this.txtb_CodigoLOG.TabIndex = 1;
            this.txtb_CodigoLOG.WordWrap = false;
            this.txtb_CodigoLOG.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtb_CodigoLOG_KeyPress);
            // 
            // lb_CodigoLOG
            // 
            this.lb_CodigoLOG.AutoSize = true;
            this.lb_CodigoLOG.BackColor = System.Drawing.Color.Transparent;
            this.lb_CodigoLOG.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_CodigoLOG.Location = new System.Drawing.Point(2, 28);
            this.lb_CodigoLOG.Name = "lb_CodigoLOG";
            this.lb_CodigoLOG.Size = new System.Drawing.Size(85, 18);
            this.lb_CodigoLOG.TabIndex = 52;
            this.lb_CodigoLOG.Text = "CÓDIGO :";
            // 
            // btn_PesquisaPorCodigo
            // 
            this.btn_PesquisaPorCodigo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_PesquisaPorCodigo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_PesquisaPorCodigo.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_PesquisaPorCodigo.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_PesquisaPorCodigo.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_PesquisaPorCodigo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PesquisaPorCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PesquisaPorCodigo.Image = global::GenOR.Properties.Resources.Yes;
            this.btn_PesquisaPorCodigo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_PesquisaPorCodigo.Location = new System.Drawing.Point(838, 7);
            this.btn_PesquisaPorCodigo.Name = "btn_PesquisaPorCodigo";
            this.btn_PesquisaPorCodigo.Size = new System.Drawing.Size(173, 59);
            this.btn_PesquisaPorCodigo.TabIndex = 2;
            this.btn_PesquisaPorCodigo.Text = "      PESQUISAR";
            this.btn_PesquisaPorCodigo.UseVisualStyleBackColor = false;
            this.btn_PesquisaPorCodigo.Click += new System.EventHandler(this.btn_PesquisaPorCodigo_Click);
            // 
            // tcp_DataRegistro
            // 
            this.tcp_DataRegistro.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tcp_DataRegistro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tcp_DataRegistro.Controls.Add(this.btn_FecharCalendarioDataRegistroFinal);
            this.tcp_DataRegistro.Controls.Add(this.btn_FecharCalendarioDataRegistroInicial);
            this.tcp_DataRegistro.Controls.Add(this.btn_DataRegistroInicial);
            this.tcp_DataRegistro.Controls.Add(this.btn_DataRegistroFinal);
            this.tcp_DataRegistro.Controls.Add(this.mtxtb_DataRegistroFinal);
            this.tcp_DataRegistro.Controls.Add(this.lb_DataRegistro_Final);
            this.tcp_DataRegistro.Controls.Add(this.mtxtb_DataRegistroInicial);
            this.tcp_DataRegistro.Controls.Add(this.lb_DataRegistro);
            this.tcp_DataRegistro.Controls.Add(this.btn_PesquisaPorDataRegistro);
            this.tcp_DataRegistro.Location = new System.Drawing.Point(4, 4);
            this.tcp_DataRegistro.Name = "tcp_DataRegistro";
            this.tcp_DataRegistro.Size = new System.Drawing.Size(1019, 76);
            this.tcp_DataRegistro.TabIndex = 4;
            this.tcp_DataRegistro.Text = "DATA REGISTRO";
            // 
            // btn_FecharCalendarioDataRegistroFinal
            // 
            this.btn_FecharCalendarioDataRegistroFinal.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_FecharCalendarioDataRegistroFinal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_FecharCalendarioDataRegistroFinal.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_FecharCalendarioDataRegistroFinal.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_FecharCalendarioDataRegistroFinal.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_FecharCalendarioDataRegistroFinal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_FecharCalendarioDataRegistroFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_FecharCalendarioDataRegistroFinal.Image = global::GenOR.Properties.Resources.Delete;
            this.btn_FecharCalendarioDataRegistroFinal.Location = new System.Drawing.Point(628, 18);
            this.btn_FecharCalendarioDataRegistroFinal.Name = "btn_FecharCalendarioDataRegistroFinal";
            this.btn_FecharCalendarioDataRegistroFinal.Size = new System.Drawing.Size(35, 35);
            this.btn_FecharCalendarioDataRegistroFinal.TabIndex = 169;
            this.btn_FecharCalendarioDataRegistroFinal.TabStop = false;
            this.btn_FecharCalendarioDataRegistroFinal.UseVisualStyleBackColor = false;
            this.btn_FecharCalendarioDataRegistroFinal.Visible = false;
            this.btn_FecharCalendarioDataRegistroFinal.Click += new System.EventHandler(this.btn_FecharCalendarioDataRegistroFinal_Click);
            // 
            // btn_FecharCalendarioDataRegistroInicial
            // 
            this.btn_FecharCalendarioDataRegistroInicial.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_FecharCalendarioDataRegistroInicial.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_FecharCalendarioDataRegistroInicial.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_FecharCalendarioDataRegistroInicial.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_FecharCalendarioDataRegistroInicial.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_FecharCalendarioDataRegistroInicial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_FecharCalendarioDataRegistroInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_FecharCalendarioDataRegistroInicial.Image = global::GenOR.Properties.Resources.Delete;
            this.btn_FecharCalendarioDataRegistroInicial.Location = new System.Drawing.Point(399, 18);
            this.btn_FecharCalendarioDataRegistroInicial.Name = "btn_FecharCalendarioDataRegistroInicial";
            this.btn_FecharCalendarioDataRegistroInicial.Size = new System.Drawing.Size(35, 35);
            this.btn_FecharCalendarioDataRegistroInicial.TabIndex = 168;
            this.btn_FecharCalendarioDataRegistroInicial.TabStop = false;
            this.btn_FecharCalendarioDataRegistroInicial.UseVisualStyleBackColor = false;
            this.btn_FecharCalendarioDataRegistroInicial.Visible = false;
            this.btn_FecharCalendarioDataRegistroInicial.Click += new System.EventHandler(this.btn_FecharCalendarioDataRegistroInicial_Click);
            // 
            // btn_DataRegistroInicial
            // 
            this.btn_DataRegistroInicial.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_DataRegistroInicial.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_DataRegistroInicial.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_DataRegistroInicial.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_DataRegistroInicial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DataRegistroInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DataRegistroInicial.Image = global::GenOR.Properties.Resources.Calendar;
            this.btn_DataRegistroInicial.Location = new System.Drawing.Point(399, 18);
            this.btn_DataRegistroInicial.Name = "btn_DataRegistroInicial";
            this.btn_DataRegistroInicial.Size = new System.Drawing.Size(35, 35);
            this.btn_DataRegistroInicial.TabIndex = 6;
            this.btn_DataRegistroInicial.UseVisualStyleBackColor = false;
            this.btn_DataRegistroInicial.Click += new System.EventHandler(this.btn_DataRegistroInicial_Click);
            // 
            // btn_DataRegistroFinal
            // 
            this.btn_DataRegistroFinal.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_DataRegistroFinal.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_DataRegistroFinal.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_DataRegistroFinal.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_DataRegistroFinal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DataRegistroFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DataRegistroFinal.Image = global::GenOR.Properties.Resources.Calendar;
            this.btn_DataRegistroFinal.Location = new System.Drawing.Point(628, 18);
            this.btn_DataRegistroFinal.Name = "btn_DataRegistroFinal";
            this.btn_DataRegistroFinal.Size = new System.Drawing.Size(35, 35);
            this.btn_DataRegistroFinal.TabIndex = 7;
            this.btn_DataRegistroFinal.UseVisualStyleBackColor = false;
            this.btn_DataRegistroFinal.Click += new System.EventHandler(this.btn_DataRegistroFinal_Click);
            // 
            // mtxtb_DataRegistroFinal
            // 
            this.mtxtb_DataRegistroFinal.BackColor = System.Drawing.Color.White;
            this.mtxtb_DataRegistroFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtb_DataRegistroFinal.Location = new System.Drawing.Point(536, 24);
            this.mtxtb_DataRegistroFinal.Mask = "00/00/0000";
            this.mtxtb_DataRegistroFinal.Name = "mtxtb_DataRegistroFinal";
            this.mtxtb_DataRegistroFinal.RejectInputOnFirstFailure = true;
            this.mtxtb_DataRegistroFinal.Size = new System.Drawing.Size(86, 26);
            this.mtxtb_DataRegistroFinal.TabIndex = 4;
            this.mtxtb_DataRegistroFinal.TabStop = false;
            this.mtxtb_DataRegistroFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtxtb_DataRegistroFinal.ValidatingType = typeof(System.DateTime);
            this.mtxtb_DataRegistroFinal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mtxtb_DataRegistroFinal_KeyPress);
            // 
            // lb_DataRegistro_Final
            // 
            this.lb_DataRegistro_Final.AutoSize = true;
            this.lb_DataRegistro_Final.BackColor = System.Drawing.Color.Transparent;
            this.lb_DataRegistro_Final.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_DataRegistro_Final.Location = new System.Drawing.Point(476, 28);
            this.lb_DataRegistro_Final.Name = "lb_DataRegistro_Final";
            this.lb_DataRegistro_Final.Size = new System.Drawing.Size(58, 18);
            this.lb_DataRegistro_Final.TabIndex = 59;
            this.lb_DataRegistro_Final.Text = "FINAL:";
            // 
            // mtxtb_DataRegistroInicial
            // 
            this.mtxtb_DataRegistroInicial.BackColor = System.Drawing.Color.White;
            this.mtxtb_DataRegistroInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtb_DataRegistroInicial.Location = new System.Drawing.Point(307, 24);
            this.mtxtb_DataRegistroInicial.Mask = "00/00/0000";
            this.mtxtb_DataRegistroInicial.Name = "mtxtb_DataRegistroInicial";
            this.mtxtb_DataRegistroInicial.RejectInputOnFirstFailure = true;
            this.mtxtb_DataRegistroInicial.Size = new System.Drawing.Size(86, 26);
            this.mtxtb_DataRegistroInicial.TabIndex = 3;
            this.mtxtb_DataRegistroInicial.TabStop = false;
            this.mtxtb_DataRegistroInicial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtxtb_DataRegistroInicial.ValidatingType = typeof(System.DateTime);
            this.mtxtb_DataRegistroInicial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mtxtb_DataRegistroInicial_KeyPress);
            // 
            // lb_DataRegistro
            // 
            this.lb_DataRegistro.AutoSize = true;
            this.lb_DataRegistro.BackColor = System.Drawing.Color.Transparent;
            this.lb_DataRegistro.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_DataRegistro.Location = new System.Drawing.Point(3, 28);
            this.lb_DataRegistro.Name = "lb_DataRegistro";
            this.lb_DataRegistro.Size = new System.Drawing.Size(302, 18);
            this.lb_DataRegistro.TabIndex = 55;
            this.lb_DataRegistro.Text = "DATA RESGISTRO                  INICIAL:";
            // 
            // btn_PesquisaPorDataRegistro
            // 
            this.btn_PesquisaPorDataRegistro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_PesquisaPorDataRegistro.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_PesquisaPorDataRegistro.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_PesquisaPorDataRegistro.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_PesquisaPorDataRegistro.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_PesquisaPorDataRegistro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PesquisaPorDataRegistro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PesquisaPorDataRegistro.Image = global::GenOR.Properties.Resources.Yes;
            this.btn_PesquisaPorDataRegistro.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_PesquisaPorDataRegistro.Location = new System.Drawing.Point(837, 7);
            this.btn_PesquisaPorDataRegistro.Name = "btn_PesquisaPorDataRegistro";
            this.btn_PesquisaPorDataRegistro.Size = new System.Drawing.Size(173, 59);
            this.btn_PesquisaPorDataRegistro.TabIndex = 5;
            this.btn_PesquisaPorDataRegistro.Text = "      PESQUISAR";
            this.btn_PesquisaPorDataRegistro.UseVisualStyleBackColor = false;
            this.btn_PesquisaPorDataRegistro.Click += new System.EventHandler(this.btn_PesquisaPorDataRegistro_Click);
            // 
            // tcp_Operacao
            // 
            this.tcp_Operacao.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tcp_Operacao.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tcp_Operacao.Controls.Add(this.txtb_Operacao);
            this.tcp_Operacao.Controls.Add(this.lb_Operacao);
            this.tcp_Operacao.Controls.Add(this.btn_PesquisaPorOperacao);
            this.tcp_Operacao.Location = new System.Drawing.Point(4, 4);
            this.tcp_Operacao.Name = "tcp_Operacao";
            this.tcp_Operacao.Size = new System.Drawing.Size(1019, 76);
            this.tcp_Operacao.TabIndex = 1;
            this.tcp_Operacao.Text = "OPERAÇÃO";
            // 
            // txtb_Operacao
            // 
            this.txtb_Operacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtb_Operacao.BackColor = System.Drawing.Color.White;
            this.txtb_Operacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb_Operacao.Location = new System.Drawing.Point(115, 24);
            this.txtb_Operacao.MaxLength = 11;
            this.txtb_Operacao.Name = "txtb_Operacao";
            this.txtb_Operacao.Size = new System.Drawing.Size(716, 26);
            this.txtb_Operacao.TabIndex = 8;
            this.txtb_Operacao.WordWrap = false;
            this.txtb_Operacao.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtb_Operacao_KeyPress);
            // 
            // lb_Operacao
            // 
            this.lb_Operacao.AutoSize = true;
            this.lb_Operacao.BackColor = System.Drawing.Color.Transparent;
            this.lb_Operacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Operacao.Location = new System.Drawing.Point(3, 28);
            this.lb_Operacao.Name = "lb_Operacao";
            this.lb_Operacao.Size = new System.Drawing.Size(110, 18);
            this.lb_Operacao.TabIndex = 55;
            this.lb_Operacao.Text = "OPERAÇÃO :";
            // 
            // btn_PesquisaPorOperacao
            // 
            this.btn_PesquisaPorOperacao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_PesquisaPorOperacao.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_PesquisaPorOperacao.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_PesquisaPorOperacao.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_PesquisaPorOperacao.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_PesquisaPorOperacao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PesquisaPorOperacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PesquisaPorOperacao.Image = global::GenOR.Properties.Resources.Yes;
            this.btn_PesquisaPorOperacao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_PesquisaPorOperacao.Location = new System.Drawing.Point(837, 7);
            this.btn_PesquisaPorOperacao.Name = "btn_PesquisaPorOperacao";
            this.btn_PesquisaPorOperacao.Size = new System.Drawing.Size(173, 59);
            this.btn_PesquisaPorOperacao.TabIndex = 9;
            this.btn_PesquisaPorOperacao.Text = "      PESQUISAR";
            this.btn_PesquisaPorOperacao.UseVisualStyleBackColor = false;
            this.btn_PesquisaPorOperacao.Click += new System.EventHandler(this.btn_PesquisaPorOperacao_Click);
            // 
            // tcp_Registro
            // 
            this.tcp_Registro.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tcp_Registro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tcp_Registro.Controls.Add(this.txtb_Registro);
            this.tcp_Registro.Controls.Add(this.lb_Registro);
            this.tcp_Registro.Controls.Add(this.btn_PesquisaPorRegistro);
            this.tcp_Registro.Location = new System.Drawing.Point(4, 4);
            this.tcp_Registro.Name = "tcp_Registro";
            this.tcp_Registro.Size = new System.Drawing.Size(1019, 76);
            this.tcp_Registro.TabIndex = 2;
            this.tcp_Registro.Text = "REGISTRO";
            // 
            // txtb_Registro
            // 
            this.txtb_Registro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtb_Registro.BackColor = System.Drawing.Color.White;
            this.txtb_Registro.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb_Registro.Location = new System.Drawing.Point(109, 24);
            this.txtb_Registro.MaxLength = 32;
            this.txtb_Registro.Name = "txtb_Registro";
            this.txtb_Registro.Size = new System.Drawing.Size(722, 26);
            this.txtb_Registro.TabIndex = 10;
            this.txtb_Registro.WordWrap = false;
            this.txtb_Registro.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtb_Registro_KeyPress);
            // 
            // lb_Registro
            // 
            this.lb_Registro.AutoSize = true;
            this.lb_Registro.BackColor = System.Drawing.Color.Transparent;
            this.lb_Registro.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Registro.Location = new System.Drawing.Point(3, 28);
            this.lb_Registro.Name = "lb_Registro";
            this.lb_Registro.Size = new System.Drawing.Size(104, 18);
            this.lb_Registro.TabIndex = 55;
            this.lb_Registro.Text = "REGISTRO :";
            // 
            // btn_PesquisaPorRegistro
            // 
            this.btn_PesquisaPorRegistro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_PesquisaPorRegistro.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_PesquisaPorRegistro.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_PesquisaPorRegistro.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_PesquisaPorRegistro.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_PesquisaPorRegistro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PesquisaPorRegistro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PesquisaPorRegistro.Image = global::GenOR.Properties.Resources.Yes;
            this.btn_PesquisaPorRegistro.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_PesquisaPorRegistro.Location = new System.Drawing.Point(837, 7);
            this.btn_PesquisaPorRegistro.Name = "btn_PesquisaPorRegistro";
            this.btn_PesquisaPorRegistro.Size = new System.Drawing.Size(173, 59);
            this.btn_PesquisaPorRegistro.TabIndex = 11;
            this.btn_PesquisaPorRegistro.Text = "      PESQUISAR";
            this.btn_PesquisaPorRegistro.UseVisualStyleBackColor = false;
            this.btn_PesquisaPorRegistro.Click += new System.EventHandler(this.btn_PesquisaPorRegistro_Click);
            // 
            // mc_Calendario
            // 
            this.mc_Calendario.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.mc_Calendario.FirstDayOfWeek = System.Windows.Forms.Day.Monday;
            this.mc_Calendario.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mc_Calendario.Location = new System.Drawing.Point(669, -108);
            this.mc_Calendario.MaxSelectionCount = 1;
            this.mc_Calendario.Name = "mc_Calendario";
            this.mc_Calendario.TabIndex = 171;
            this.mc_Calendario.Visible = false;
            this.mc_Calendario.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.mc_Calendario_DateSelected);
            // 
            // FormTelaPesquisaLOG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 108);
            this.Controls.Add(this.mc_Calendario);
            this.Controls.Add(this.tc_Pesquisa);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormTelaPesquisaLOG";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PESQUISANDO LOG";
            this.tc_Pesquisa.ResumeLayout(false);
            this.tcp_Codigo.ResumeLayout(false);
            this.tcp_Codigo.PerformLayout();
            this.tcp_DataRegistro.ResumeLayout(false);
            this.tcp_DataRegistro.PerformLayout();
            this.tcp_Operacao.ResumeLayout(false);
            this.tcp_Operacao.PerformLayout();
            this.tcp_Registro.ResumeLayout(false);
            this.tcp_Registro.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tc_Pesquisa;
        private System.Windows.Forms.TabPage tcp_Codigo;
        private System.Windows.Forms.TextBox txtb_CodigoLOG;
        private System.Windows.Forms.Label lb_CodigoLOG;
        private System.Windows.Forms.Button btn_PesquisaPorCodigo;
        private System.Windows.Forms.TabPage tcp_DataRegistro;
        private System.Windows.Forms.MaskedTextBox mtxtb_DataRegistroFinal;
        private System.Windows.Forms.Label lb_DataRegistro_Final;
        private System.Windows.Forms.MaskedTextBox mtxtb_DataRegistroInicial;
        private System.Windows.Forms.Label lb_DataRegistro;
        private System.Windows.Forms.Button btn_PesquisaPorDataRegistro;
        private System.Windows.Forms.TabPage tcp_Operacao;
        private System.Windows.Forms.TextBox txtb_Operacao;
        private System.Windows.Forms.Label lb_Operacao;
        private System.Windows.Forms.Button btn_PesquisaPorOperacao;
        private System.Windows.Forms.TabPage tcp_Registro;
        private System.Windows.Forms.TextBox txtb_Registro;
        private System.Windows.Forms.Label lb_Registro;
        private System.Windows.Forms.Button btn_PesquisaPorRegistro;
        private System.Windows.Forms.Button btn_DataRegistroInicial;
        private System.Windows.Forms.Button btn_DataRegistroFinal;
        private System.Windows.Forms.Button btn_FecharCalendarioDataRegistroFinal;
        private System.Windows.Forms.Button btn_FecharCalendarioDataRegistroInicial;
        private System.Windows.Forms.MonthCalendar mc_Calendario;
    }
}