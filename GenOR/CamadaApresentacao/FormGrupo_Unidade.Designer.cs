namespace GenOR
{
    partial class FormGrupo_Unidade
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGrupo_Unidade));
            this.tbc_Principal = new System.Windows.Forms.TabControl();
            this.tbp_Principal_ListaRegistros = new System.Windows.Forms.TabPage();
            this.gb_ListagemGeral = new System.Windows.Forms.GroupBox();
            this.dgv_Grupo_Unidade = new System.Windows.Forms.DataGridView();
            this.colCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSigla = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaterial_Ou_Produto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbp_Principal_DadosRegistro = new System.Windows.Forms.TabPage();
            this.tbc_Secundario = new System.Windows.Forms.TabControl();
            this.tbp_Secundario_DadosGerais = new System.Windows.Forms.TabPage();
            this.gb_DadosGerais = new System.Windows.Forms.GroupBox();
            this.lb_DescricaoGrupo_Unidade = new System.Windows.Forms.Label();
            this.lb_Obrigatorio_3 = new System.Windows.Forms.Label();
            this.lb_Material_Ou_Produto = new System.Windows.Forms.Label();
            this.lb_Obrigatorio_1 = new System.Windows.Forms.Label();
            this.lb_Sigla_Unidade = new System.Windows.Forms.Label();
            this.lb_Obrigatorio_2 = new System.Windows.Forms.Label();
            this.rb_Produto = new System.Windows.Forms.RadioButton();
            this.rb_Material = new System.Windows.Forms.RadioButton();
            this.txtb_DadosGerais_FundoVisual_RadioButtons = new System.Windows.Forms.TextBox();
            this.txtb_CodigoGrupo_Unidade = new System.Windows.Forms.TextBox();
            this.lb_CodigoGrupo_Unidade = new System.Windows.Forms.Label();
            this.txtb_Sigla_Unidade = new System.Windows.Forms.TextBox();
            this.txtb_DescricaoGrupo_Unidade = new System.Windows.Forms.TextBox();
            this.btn_DadosGerais_Cancelar = new System.Windows.Forms.Button();
            this.btn_DadosGerais_Confirmar = new System.Windows.Forms.Button();
            this.txtb_DadosGerais_FundoVisual = new System.Windows.Forms.TextBox();
            this.btn_ProdutosServicos_Vinculados = new System.Windows.Forms.Button();
            this.btn_Materiais_Vinculados = new System.Windows.Forms.Button();
            this.lb_LogoJanela = new System.Windows.Forms.Label();
            this.btn_DadosGerais_Imprimir = new System.Windows.Forms.Button();
            this.btn_DadosGerais_Pesquisar = new System.Windows.Forms.Button();
            this.btn_DadosGerais_AvancarGrid = new System.Windows.Forms.Button();
            this.btn_DadosGerais_VoltarGrid = new System.Windows.Forms.Button();
            this.btn_DadosGerais_Deletar = new System.Windows.Forms.Button();
            this.btn_DadosGerais_Alterar = new System.Windows.Forms.Button();
            this.btn_DadosGerais_Cadastrar = new System.Windows.Forms.Button();
            this.btn_RetornarModoPesquisa = new System.Windows.Forms.Button();
            this.tbc_Principal.SuspendLayout();
            this.tbp_Principal_ListaRegistros.SuspendLayout();
            this.gb_ListagemGeral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Grupo_Unidade)).BeginInit();
            this.tbp_Principal_DadosRegistro.SuspendLayout();
            this.tbc_Secundario.SuspendLayout();
            this.tbp_Secundario_DadosGerais.SuspendLayout();
            this.gb_DadosGerais.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbc_Principal
            // 
            this.tbc_Principal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbc_Principal.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tbc_Principal.Controls.Add(this.tbp_Principal_ListaRegistros);
            this.tbc_Principal.Controls.Add(this.tbp_Principal_DadosRegistro);
            this.tbc_Principal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbc_Principal.HotTrack = true;
            this.tbc_Principal.ItemSize = new System.Drawing.Size(171, 34);
            this.tbc_Principal.Location = new System.Drawing.Point(1, 61);
            this.tbc_Principal.Name = "tbc_Principal";
            this.tbc_Principal.SelectedIndex = 0;
            this.tbc_Principal.Size = new System.Drawing.Size(1264, 624);
            this.tbc_Principal.TabIndex = 16;
            // 
            // tbp_Principal_ListaRegistros
            // 
            this.tbp_Principal_ListaRegistros.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tbp_Principal_ListaRegistros.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tbp_Principal_ListaRegistros.Controls.Add(this.gb_ListagemGeral);
            this.tbp_Principal_ListaRegistros.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tbp_Principal_ListaRegistros.Location = new System.Drawing.Point(4, 38);
            this.tbp_Principal_ListaRegistros.Name = "tbp_Principal_ListaRegistros";
            this.tbp_Principal_ListaRegistros.Padding = new System.Windows.Forms.Padding(3);
            this.tbp_Principal_ListaRegistros.Size = new System.Drawing.Size(1256, 582);
            this.tbp_Principal_ListaRegistros.TabIndex = 0;
            this.tbp_Principal_ListaRegistros.Text = "LISTA DE REGISTROS";
            // 
            // gb_ListagemGeral
            // 
            this.gb_ListagemGeral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_ListagemGeral.BackColor = System.Drawing.Color.Transparent;
            this.gb_ListagemGeral.Controls.Add(this.dgv_Grupo_Unidade);
            this.gb_ListagemGeral.Location = new System.Drawing.Point(2, -2);
            this.gb_ListagemGeral.Name = "gb_ListagemGeral";
            this.gb_ListagemGeral.Size = new System.Drawing.Size(1252, 582);
            this.gb_ListagemGeral.TabIndex = 1;
            this.gb_ListagemGeral.TabStop = false;
            // 
            // dgv_Grupo_Unidade
            // 
            this.dgv_Grupo_Unidade.AllowUserToAddRows = false;
            this.dgv_Grupo_Unidade.AllowUserToDeleteRows = false;
            this.dgv_Grupo_Unidade.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_Grupo_Unidade.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_Grupo_Unidade.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgv_Grupo_Unidade.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Grupo_Unidade.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_Grupo_Unidade.ColumnHeadersHeight = 30;
            this.dgv_Grupo_Unidade.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCodigo,
            this.colSigla,
            this.colDescricao,
            this.colMaterial_Ou_Produto});
            this.dgv_Grupo_Unidade.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgv_Grupo_Unidade.Location = new System.Drawing.Point(-6, 0);
            this.dgv_Grupo_Unidade.MultiSelect = false;
            this.dgv_Grupo_Unidade.Name = "dgv_Grupo_Unidade";
            this.dgv_Grupo_Unidade.ReadOnly = true;
            this.dgv_Grupo_Unidade.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Grupo_Unidade.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_Grupo_Unidade.RowHeadersVisible = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            this.dgv_Grupo_Unidade.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgv_Grupo_Unidade.RowTemplate.ReadOnly = true;
            this.dgv_Grupo_Unidade.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Grupo_Unidade.Size = new System.Drawing.Size(1261, 583);
            this.dgv_Grupo_Unidade.TabIndex = 1;
            this.dgv_Grupo_Unidade.TabStop = false;
            this.dgv_Grupo_Unidade.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgv_Grupo_Unidade_KeyUp);
            this.dgv_Grupo_Unidade.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgv_Grupo_Unidade_MouseClick);
            this.dgv_Grupo_Unidade.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgv_Grupo_Unidade_MouseDoubleClick);
            // 
            // colCodigo
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.colCodigo.DefaultCellStyle = dataGridViewCellStyle2;
            this.colCodigo.HeaderText = "CÓDIGO";
            this.colCodigo.Name = "colCodigo";
            this.colCodigo.ReadOnly = true;
            this.colCodigo.Width = 200;
            // 
            // colSigla
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.colSigla.DefaultCellStyle = dataGridViewCellStyle3;
            this.colSigla.HeaderText = "SIGLA";
            this.colSigla.Name = "colSigla";
            this.colSigla.ReadOnly = true;
            this.colSigla.Width = 300;
            // 
            // colDescricao
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.colDescricao.DefaultCellStyle = dataGridViewCellStyle4;
            this.colDescricao.HeaderText = "DESCRIÇÃO";
            this.colDescricao.Name = "colDescricao";
            this.colDescricao.ReadOnly = true;
            this.colDescricao.Width = 1397;
            // 
            // colMaterial_Ou_Produto
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            this.colMaterial_Ou_Produto.DefaultCellStyle = dataGridViewCellStyle5;
            this.colMaterial_Ou_Produto.HeaderText = "MATERIAL / PRODUTO";
            this.colMaterial_Ou_Produto.Name = "colMaterial_Ou_Produto";
            this.colMaterial_Ou_Produto.ReadOnly = true;
            this.colMaterial_Ou_Produto.Width = 314;
            // 
            // tbp_Principal_DadosRegistro
            // 
            this.tbp_Principal_DadosRegistro.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tbp_Principal_DadosRegistro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tbp_Principal_DadosRegistro.Controls.Add(this.tbc_Secundario);
            this.tbp_Principal_DadosRegistro.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbp_Principal_DadosRegistro.Location = new System.Drawing.Point(4, 38);
            this.tbp_Principal_DadosRegistro.Name = "tbp_Principal_DadosRegistro";
            this.tbp_Principal_DadosRegistro.Padding = new System.Windows.Forms.Padding(3);
            this.tbp_Principal_DadosRegistro.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbp_Principal_DadosRegistro.Size = new System.Drawing.Size(1256, 582);
            this.tbp_Principal_DadosRegistro.TabIndex = 1;
            this.tbp_Principal_DadosRegistro.Text = "DADOS DO REGISTRO";
            // 
            // tbc_Secundario
            // 
            this.tbc_Secundario.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tbc_Secundario.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbc_Secundario.Controls.Add(this.tbp_Secundario_DadosGerais);
            this.tbc_Secundario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbc_Secundario.HotTrack = true;
            this.tbc_Secundario.ItemSize = new System.Drawing.Size(171, 34);
            this.tbc_Secundario.Location = new System.Drawing.Point(-2, -2);
            this.tbc_Secundario.Name = "tbc_Secundario";
            this.tbc_Secundario.SelectedIndex = 0;
            this.tbc_Secundario.Size = new System.Drawing.Size(1253, 582);
            this.tbc_Secundario.TabIndex = 1;
            // 
            // tbp_Secundario_DadosGerais
            // 
            this.tbp_Secundario_DadosGerais.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tbp_Secundario_DadosGerais.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tbp_Secundario_DadosGerais.Controls.Add(this.gb_DadosGerais);
            this.tbp_Secundario_DadosGerais.Location = new System.Drawing.Point(4, 4);
            this.tbp_Secundario_DadosGerais.Name = "tbp_Secundario_DadosGerais";
            this.tbp_Secundario_DadosGerais.Padding = new System.Windows.Forms.Padding(3);
            this.tbp_Secundario_DadosGerais.Size = new System.Drawing.Size(1245, 540);
            this.tbp_Secundario_DadosGerais.TabIndex = 0;
            this.tbp_Secundario_DadosGerais.Text = "DADOS GERAIS";
            // 
            // gb_DadosGerais
            // 
            this.gb_DadosGerais.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_DadosGerais.BackColor = System.Drawing.Color.Transparent;
            this.gb_DadosGerais.Controls.Add(this.lb_DescricaoGrupo_Unidade);
            this.gb_DadosGerais.Controls.Add(this.lb_Obrigatorio_3);
            this.gb_DadosGerais.Controls.Add(this.lb_Material_Ou_Produto);
            this.gb_DadosGerais.Controls.Add(this.lb_Obrigatorio_1);
            this.gb_DadosGerais.Controls.Add(this.lb_Sigla_Unidade);
            this.gb_DadosGerais.Controls.Add(this.lb_Obrigatorio_2);
            this.gb_DadosGerais.Controls.Add(this.rb_Produto);
            this.gb_DadosGerais.Controls.Add(this.rb_Material);
            this.gb_DadosGerais.Controls.Add(this.txtb_DadosGerais_FundoVisual_RadioButtons);
            this.gb_DadosGerais.Controls.Add(this.txtb_CodigoGrupo_Unidade);
            this.gb_DadosGerais.Controls.Add(this.lb_CodigoGrupo_Unidade);
            this.gb_DadosGerais.Controls.Add(this.txtb_Sigla_Unidade);
            this.gb_DadosGerais.Controls.Add(this.txtb_DescricaoGrupo_Unidade);
            this.gb_DadosGerais.Controls.Add(this.btn_DadosGerais_Cancelar);
            this.gb_DadosGerais.Controls.Add(this.btn_DadosGerais_Confirmar);
            this.gb_DadosGerais.Controls.Add(this.txtb_DadosGerais_FundoVisual);
            this.gb_DadosGerais.Enabled = false;
            this.gb_DadosGerais.Location = new System.Drawing.Point(-2, -6);
            this.gb_DadosGerais.Name = "gb_DadosGerais";
            this.gb_DadosGerais.Size = new System.Drawing.Size(1249, 544);
            this.gb_DadosGerais.TabIndex = 0;
            this.gb_DadosGerais.TabStop = false;
            // 
            // lb_DescricaoGrupo_Unidade
            // 
            this.lb_DescricaoGrupo_Unidade.AutoSize = true;
            this.lb_DescricaoGrupo_Unidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_DescricaoGrupo_Unidade.Location = new System.Drawing.Point(332, 284);
            this.lb_DescricaoGrupo_Unidade.Name = "lb_DescricaoGrupo_Unidade";
            this.lb_DescricaoGrupo_Unidade.Size = new System.Drawing.Size(115, 18);
            this.lb_DescricaoGrupo_Unidade.TabIndex = 41;
            this.lb_DescricaoGrupo_Unidade.Text = "DESCRIÇÃO :";
            // 
            // lb_Obrigatorio_3
            // 
            this.lb_Obrigatorio_3.AutoSize = true;
            this.lb_Obrigatorio_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Obrigatorio_3.ForeColor = System.Drawing.Color.IndianRed;
            this.lb_Obrigatorio_3.Location = new System.Drawing.Point(319, 281);
            this.lb_Obrigatorio_3.Name = "lb_Obrigatorio_3";
            this.lb_Obrigatorio_3.Size = new System.Drawing.Size(21, 25);
            this.lb_Obrigatorio_3.TabIndex = 121;
            this.lb_Obrigatorio_3.Text = "*";
            this.lb_Obrigatorio_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_Obrigatorio_3.MouseHover += new System.EventHandler(this.lb_Obrigatorio_3_MouseHover);
            // 
            // lb_Material_Ou_Produto
            // 
            this.lb_Material_Ou_Produto.AutoSize = true;
            this.lb_Material_Ou_Produto.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Material_Ou_Produto.Location = new System.Drawing.Point(267, 48);
            this.lb_Material_Ou_Produto.Name = "lb_Material_Ou_Produto";
            this.lb_Material_Ou_Produto.Size = new System.Drawing.Size(196, 18);
            this.lb_Material_Ou_Produto.TabIndex = 48;
            this.lb_Material_Ou_Produto.Text = "MATERIAL / PRODUTO :";
            // 
            // lb_Obrigatorio_1
            // 
            this.lb_Obrigatorio_1.AutoSize = true;
            this.lb_Obrigatorio_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Obrigatorio_1.ForeColor = System.Drawing.Color.IndianRed;
            this.lb_Obrigatorio_1.Location = new System.Drawing.Point(254, 43);
            this.lb_Obrigatorio_1.Name = "lb_Obrigatorio_1";
            this.lb_Obrigatorio_1.Size = new System.Drawing.Size(21, 25);
            this.lb_Obrigatorio_1.TabIndex = 120;
            this.lb_Obrigatorio_1.Text = "*";
            this.lb_Obrigatorio_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_Obrigatorio_1.MouseHover += new System.EventHandler(this.lb_Obrigatorio_1_MouseHover);
            // 
            // lb_Sigla_Unidade
            // 
            this.lb_Sigla_Unidade.AutoSize = true;
            this.lb_Sigla_Unidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Sigla_Unidade.Location = new System.Drawing.Point(382, 249);
            this.lb_Sigla_Unidade.Name = "lb_Sigla_Unidade";
            this.lb_Sigla_Unidade.Size = new System.Drawing.Size(65, 18);
            this.lb_Sigla_Unidade.TabIndex = 44;
            this.lb_Sigla_Unidade.Text = "SIGLA :";
            // 
            // lb_Obrigatorio_2
            // 
            this.lb_Obrigatorio_2.AutoSize = true;
            this.lb_Obrigatorio_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Obrigatorio_2.ForeColor = System.Drawing.Color.IndianRed;
            this.lb_Obrigatorio_2.Location = new System.Drawing.Point(368, 245);
            this.lb_Obrigatorio_2.Name = "lb_Obrigatorio_2";
            this.lb_Obrigatorio_2.Size = new System.Drawing.Size(21, 25);
            this.lb_Obrigatorio_2.TabIndex = 119;
            this.lb_Obrigatorio_2.Text = "*";
            this.lb_Obrigatorio_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_Obrigatorio_2.MouseHover += new System.EventHandler(this.lb_Obrigatorio_2_MouseHover);
            // 
            // rb_Produto
            // 
            this.rb_Produto.AutoSize = true;
            this.rb_Produto.BackColor = System.Drawing.Color.Transparent;
            this.rb_Produto.Enabled = false;
            this.rb_Produto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_Produto.Location = new System.Drawing.Point(472, 60);
            this.rb_Produto.Name = "rb_Produto";
            this.rb_Produto.Size = new System.Drawing.Size(123, 24);
            this.rb_Produto.TabIndex = 3;
            this.rb_Produto.Text = " PRODUTO ";
            this.rb_Produto.UseVisualStyleBackColor = false;
            // 
            // rb_Material
            // 
            this.rb_Material.AutoSize = true;
            this.rb_Material.BackColor = System.Drawing.Color.Transparent;
            this.rb_Material.Checked = true;
            this.rb_Material.Enabled = false;
            this.rb_Material.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_Material.Location = new System.Drawing.Point(472, 34);
            this.rb_Material.Name = "rb_Material";
            this.rb_Material.Size = new System.Drawing.Size(121, 24);
            this.rb_Material.TabIndex = 2;
            this.rb_Material.TabStop = true;
            this.rb_Material.Text = " MATERIAL";
            this.rb_Material.UseVisualStyleBackColor = false;
            // 
            // txtb_DadosGerais_FundoVisual_RadioButtons
            // 
            this.txtb_DadosGerais_FundoVisual_RadioButtons.AcceptsTab = true;
            this.txtb_DadosGerais_FundoVisual_RadioButtons.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtb_DadosGerais_FundoVisual_RadioButtons.Enabled = false;
            this.txtb_DadosGerais_FundoVisual_RadioButtons.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb_DadosGerais_FundoVisual_RadioButtons.Location = new System.Drawing.Point(465, 25);
            this.txtb_DadosGerais_FundoVisual_RadioButtons.MaxLength = 150;
            this.txtb_DadosGerais_FundoVisual_RadioButtons.Multiline = true;
            this.txtb_DadosGerais_FundoVisual_RadioButtons.Name = "txtb_DadosGerais_FundoVisual_RadioButtons";
            this.txtb_DadosGerais_FundoVisual_RadioButtons.Size = new System.Drawing.Size(132, 67);
            this.txtb_DadosGerais_FundoVisual_RadioButtons.TabIndex = 51;
            // 
            // txtb_CodigoGrupo_Unidade
            // 
            this.txtb_CodigoGrupo_Unidade.BackColor = System.Drawing.Color.White;
            this.txtb_CodigoGrupo_Unidade.Enabled = false;
            this.txtb_CodigoGrupo_Unidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb_CodigoGrupo_Unidade.Location = new System.Drawing.Point(108, 42);
            this.txtb_CodigoGrupo_Unidade.Multiline = true;
            this.txtb_CodigoGrupo_Unidade.Name = "txtb_CodigoGrupo_Unidade";
            this.txtb_CodigoGrupo_Unidade.Size = new System.Drawing.Size(124, 29);
            this.txtb_CodigoGrupo_Unidade.TabIndex = 1;
            this.txtb_CodigoGrupo_Unidade.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtb_CodigoGrupo_Unidade.WordWrap = false;
            // 
            // lb_CodigoGrupo_Unidade
            // 
            this.lb_CodigoGrupo_Unidade.AutoSize = true;
            this.lb_CodigoGrupo_Unidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_CodigoGrupo_Unidade.Location = new System.Drawing.Point(21, 47);
            this.lb_CodigoGrupo_Unidade.Name = "lb_CodigoGrupo_Unidade";
            this.lb_CodigoGrupo_Unidade.Size = new System.Drawing.Size(85, 18);
            this.lb_CodigoGrupo_Unidade.TabIndex = 46;
            this.lb_CodigoGrupo_Unidade.Text = "CÓDIGO :";
            // 
            // txtb_Sigla_Unidade
            // 
            this.txtb_Sigla_Unidade.BackColor = System.Drawing.Color.White;
            this.txtb_Sigla_Unidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb_Sigla_Unidade.Location = new System.Drawing.Point(449, 245);
            this.txtb_Sigla_Unidade.MaxLength = 8;
            this.txtb_Sigla_Unidade.Name = "txtb_Sigla_Unidade";
            this.txtb_Sigla_Unidade.Size = new System.Drawing.Size(89, 26);
            this.txtb_Sigla_Unidade.TabIndex = 4;
            this.txtb_Sigla_Unidade.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtb_Sigla_Unidade.WordWrap = false;
            this.txtb_Sigla_Unidade.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtb_Sigla_Unidade_KeyPress);
            // 
            // txtb_DescricaoGrupo_Unidade
            // 
            this.txtb_DescricaoGrupo_Unidade.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtb_DescricaoGrupo_Unidade.BackColor = System.Drawing.Color.White;
            this.txtb_DescricaoGrupo_Unidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb_DescricaoGrupo_Unidade.Location = new System.Drawing.Point(449, 280);
            this.txtb_DescricaoGrupo_Unidade.MaxLength = 150;
            this.txtb_DescricaoGrupo_Unidade.Name = "txtb_DescricaoGrupo_Unidade";
            this.txtb_DescricaoGrupo_Unidade.Size = new System.Drawing.Size(462, 26);
            this.txtb_DescricaoGrupo_Unidade.TabIndex = 5;
            this.txtb_DescricaoGrupo_Unidade.WordWrap = false;
            this.txtb_DescricaoGrupo_Unidade.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtb_DescricaoGrupo_Unidade_KeyPress);
            // 
            // btn_DadosGerais_Cancelar
            // 
            this.btn_DadosGerais_Cancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_DadosGerais_Cancelar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_DadosGerais_Cancelar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_DadosGerais_Cancelar.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_DadosGerais_Cancelar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_DadosGerais_Cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DadosGerais_Cancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DadosGerais_Cancelar.Image = global::GenOR.Properties.Resources.Delete;
            this.btn_DadosGerais_Cancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_DadosGerais_Cancelar.Location = new System.Drawing.Point(1064, 477);
            this.btn_DadosGerais_Cancelar.Name = "btn_DadosGerais_Cancelar";
            this.btn_DadosGerais_Cancelar.Size = new System.Drawing.Size(173, 59);
            this.btn_DadosGerais_Cancelar.TabIndex = 7;
            this.btn_DadosGerais_Cancelar.Text = "    CANCELAR";
            this.btn_DadosGerais_Cancelar.UseVisualStyleBackColor = false;
            this.btn_DadosGerais_Cancelar.Click += new System.EventHandler(this.btn_DadosGerais_Cancelar_Click);
            // 
            // btn_DadosGerais_Confirmar
            // 
            this.btn_DadosGerais_Confirmar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_DadosGerais_Confirmar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_DadosGerais_Confirmar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_DadosGerais_Confirmar.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_DadosGerais_Confirmar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_DadosGerais_Confirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DadosGerais_Confirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DadosGerais_Confirmar.Image = global::GenOR.Properties.Resources.Yes;
            this.btn_DadosGerais_Confirmar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_DadosGerais_Confirmar.Location = new System.Drawing.Point(885, 477);
            this.btn_DadosGerais_Confirmar.Name = "btn_DadosGerais_Confirmar";
            this.btn_DadosGerais_Confirmar.Size = new System.Drawing.Size(173, 59);
            this.btn_DadosGerais_Confirmar.TabIndex = 6;
            this.btn_DadosGerais_Confirmar.Text = "     CONFIRMAR";
            this.btn_DadosGerais_Confirmar.UseVisualStyleBackColor = false;
            this.btn_DadosGerais_Confirmar.Click += new System.EventHandler(this.btn_DadosGerais_Confirmar_Click);
            // 
            // txtb_DadosGerais_FundoVisual
            // 
            this.txtb_DadosGerais_FundoVisual.AcceptsTab = true;
            this.txtb_DadosGerais_FundoVisual.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtb_DadosGerais_FundoVisual.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtb_DadosGerais_FundoVisual.Enabled = false;
            this.txtb_DadosGerais_FundoVisual.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb_DadosGerais_FundoVisual.Location = new System.Drawing.Point(8, 12);
            this.txtb_DadosGerais_FundoVisual.MaxLength = 0;
            this.txtb_DadosGerais_FundoVisual.Multiline = true;
            this.txtb_DadosGerais_FundoVisual.Name = "txtb_DadosGerais_FundoVisual";
            this.txtb_DadosGerais_FundoVisual.Size = new System.Drawing.Size(1229, 454);
            this.txtb_DadosGerais_FundoVisual.TabIndex = 43;
            // 
            // btn_ProdutosServicos_Vinculados
            // 
            this.btn_ProdutosServicos_Vinculados.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_ProdutosServicos_Vinculados.Enabled = false;
            this.btn_ProdutosServicos_Vinculados.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_ProdutosServicos_Vinculados.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_ProdutosServicos_Vinculados.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_ProdutosServicos_Vinculados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ProdutosServicos_Vinculados.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ProdutosServicos_Vinculados.Image = global::GenOR.Properties.Resources.Link;
            this.btn_ProdutosServicos_Vinculados.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_ProdutosServicos_Vinculados.Location = new System.Drawing.Point(558, 9);
            this.btn_ProdutosServicos_Vinculados.Name = "btn_ProdutosServicos_Vinculados";
            this.btn_ProdutosServicos_Vinculados.Size = new System.Drawing.Size(50, 46);
            this.btn_ProdutosServicos_Vinculados.TabIndex = 90;
            this.btn_ProdutosServicos_Vinculados.Text = "P/S";
            this.btn_ProdutosServicos_Vinculados.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_ProdutosServicos_Vinculados.UseVisualStyleBackColor = false;
            this.btn_ProdutosServicos_Vinculados.Click += new System.EventHandler(this.btn_ProdutosServicos_Vinculados_Click);
            // 
            // btn_Materiais_Vinculados
            // 
            this.btn_Materiais_Vinculados.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_Materiais_Vinculados.Enabled = false;
            this.btn_Materiais_Vinculados.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_Materiais_Vinculados.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_Materiais_Vinculados.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_Materiais_Vinculados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Materiais_Vinculados.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Materiais_Vinculados.Image = global::GenOR.Properties.Resources.Link;
            this.btn_Materiais_Vinculados.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_Materiais_Vinculados.Location = new System.Drawing.Point(502, 9);
            this.btn_Materiais_Vinculados.Name = "btn_Materiais_Vinculados";
            this.btn_Materiais_Vinculados.Size = new System.Drawing.Size(50, 46);
            this.btn_Materiais_Vinculados.TabIndex = 88;
            this.btn_Materiais_Vinculados.Text = "M";
            this.btn_Materiais_Vinculados.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_Materiais_Vinculados.UseVisualStyleBackColor = false;
            this.btn_Materiais_Vinculados.Click += new System.EventHandler(this.btn_Materiais_Vinculados_Click);
            // 
            // lb_LogoJanela
            // 
            this.lb_LogoJanela.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_LogoJanela.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lb_LogoJanela.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lb_LogoJanela.Font = new System.Drawing.Font("Microsoft Tai Le", 51F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_LogoJanela.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lb_LogoJanela.Location = new System.Drawing.Point(0, -5);
            this.lb_LogoJanela.Name = "lb_LogoJanela";
            this.lb_LogoJanela.Size = new System.Drawing.Size(1264, 68);
            this.lb_LogoJanela.TabIndex = 23;
            this.lb_LogoJanela.Text = "         GRUPO / UNIDADE";
            this.lb_LogoJanela.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_DadosGerais_Imprimir
            // 
            this.btn_DadosGerais_Imprimir.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_DadosGerais_Imprimir.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_DadosGerais_Imprimir.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_DadosGerais_Imprimir.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_DadosGerais_Imprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DadosGerais_Imprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DadosGerais_Imprimir.Image = global::GenOR.Properties.Resources.Printer;
            this.btn_DadosGerais_Imprimir.Location = new System.Drawing.Point(446, 9);
            this.btn_DadosGerais_Imprimir.Name = "btn_DadosGerais_Imprimir";
            this.btn_DadosGerais_Imprimir.Size = new System.Drawing.Size(50, 46);
            this.btn_DadosGerais_Imprimir.TabIndex = 24;
            this.btn_DadosGerais_Imprimir.UseVisualStyleBackColor = false;
            this.btn_DadosGerais_Imprimir.Click += new System.EventHandler(this.btn_DadosGerais_Imprimir_Click);
            // 
            // btn_DadosGerais_Pesquisar
            // 
            this.btn_DadosGerais_Pesquisar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_DadosGerais_Pesquisar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_DadosGerais_Pesquisar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_DadosGerais_Pesquisar.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_DadosGerais_Pesquisar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_DadosGerais_Pesquisar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DadosGerais_Pesquisar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DadosGerais_Pesquisar.Image = global::GenOR.Properties.Resources.Find;
            this.btn_DadosGerais_Pesquisar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_DadosGerais_Pesquisar.Location = new System.Drawing.Point(1040, 9);
            this.btn_DadosGerais_Pesquisar.Name = "btn_DadosGerais_Pesquisar";
            this.btn_DadosGerais_Pesquisar.Size = new System.Drawing.Size(141, 46);
            this.btn_DadosGerais_Pesquisar.TabIndex = 22;
            this.btn_DadosGerais_Pesquisar.Text = "      PESQUISAR";
            this.btn_DadosGerais_Pesquisar.UseVisualStyleBackColor = false;
            this.btn_DadosGerais_Pesquisar.Click += new System.EventHandler(this.btn_DadosGerais_Pesquisar_Click);
            // 
            // btn_DadosGerais_AvancarGrid
            // 
            this.btn_DadosGerais_AvancarGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_DadosGerais_AvancarGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_DadosGerais_AvancarGrid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_DadosGerais_AvancarGrid.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_DadosGerais_AvancarGrid.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_DadosGerais_AvancarGrid.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_DadosGerais_AvancarGrid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DadosGerais_AvancarGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DadosGerais_AvancarGrid.Image = global::GenOR.Properties.Resources.Forward;
            this.btn_DadosGerais_AvancarGrid.Location = new System.Drawing.Point(1192, 9);
            this.btn_DadosGerais_AvancarGrid.Name = "btn_DadosGerais_AvancarGrid";
            this.btn_DadosGerais_AvancarGrid.Size = new System.Drawing.Size(61, 46);
            this.btn_DadosGerais_AvancarGrid.TabIndex = 21;
            this.btn_DadosGerais_AvancarGrid.UseVisualStyleBackColor = false;
            this.btn_DadosGerais_AvancarGrid.Click += new System.EventHandler(this.btn_DadosGerais_AvancarGrid_Click);
            // 
            // btn_DadosGerais_VoltarGrid
            // 
            this.btn_DadosGerais_VoltarGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_DadosGerais_VoltarGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_DadosGerais_VoltarGrid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_DadosGerais_VoltarGrid.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_DadosGerais_VoltarGrid.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_DadosGerais_VoltarGrid.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_DadosGerais_VoltarGrid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DadosGerais_VoltarGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DadosGerais_VoltarGrid.Image = global::GenOR.Properties.Resources.Back;
            this.btn_DadosGerais_VoltarGrid.Location = new System.Drawing.Point(967, 9);
            this.btn_DadosGerais_VoltarGrid.Name = "btn_DadosGerais_VoltarGrid";
            this.btn_DadosGerais_VoltarGrid.Size = new System.Drawing.Size(61, 46);
            this.btn_DadosGerais_VoltarGrid.TabIndex = 20;
            this.btn_DadosGerais_VoltarGrid.UseVisualStyleBackColor = false;
            this.btn_DadosGerais_VoltarGrid.Click += new System.EventHandler(this.btn_DadosGerais_VoltarGrid_Click);
            // 
            // btn_DadosGerais_Deletar
            // 
            this.btn_DadosGerais_Deletar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_DadosGerais_Deletar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_DadosGerais_Deletar.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_DadosGerais_Deletar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_DadosGerais_Deletar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DadosGerais_Deletar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DadosGerais_Deletar.Image = global::GenOR.Properties.Resources.No_entry;
            this.btn_DadosGerais_Deletar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_DadosGerais_Deletar.Location = new System.Drawing.Point(299, 9);
            this.btn_DadosGerais_Deletar.Name = "btn_DadosGerais_Deletar";
            this.btn_DadosGerais_Deletar.Size = new System.Drawing.Size(141, 46);
            this.btn_DadosGerais_Deletar.TabIndex = 19;
            this.btn_DadosGerais_Deletar.Text = "      DELETAR";
            this.btn_DadosGerais_Deletar.UseVisualStyleBackColor = false;
            this.btn_DadosGerais_Deletar.Click += new System.EventHandler(this.btn_DadosGerais_Deletar_Click);
            // 
            // btn_DadosGerais_Alterar
            // 
            this.btn_DadosGerais_Alterar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_DadosGerais_Alterar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_DadosGerais_Alterar.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_DadosGerais_Alterar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_DadosGerais_Alterar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DadosGerais_Alterar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DadosGerais_Alterar.Image = global::GenOR.Properties.Resources.Modify;
            this.btn_DadosGerais_Alterar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_DadosGerais_Alterar.Location = new System.Drawing.Point(152, 9);
            this.btn_DadosGerais_Alterar.Name = "btn_DadosGerais_Alterar";
            this.btn_DadosGerais_Alterar.Size = new System.Drawing.Size(141, 46);
            this.btn_DadosGerais_Alterar.TabIndex = 18;
            this.btn_DadosGerais_Alterar.Text = "      ALTERAR";
            this.btn_DadosGerais_Alterar.UseVisualStyleBackColor = false;
            this.btn_DadosGerais_Alterar.Click += new System.EventHandler(this.btn_DadosGerais_Alterar_Click);
            // 
            // btn_DadosGerais_Cadastrar
            // 
            this.btn_DadosGerais_Cadastrar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_DadosGerais_Cadastrar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_DadosGerais_Cadastrar.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_DadosGerais_Cadastrar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_DadosGerais_Cadastrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DadosGerais_Cadastrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DadosGerais_Cadastrar.Image = global::GenOR.Properties.Resources.Add;
            this.btn_DadosGerais_Cadastrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_DadosGerais_Cadastrar.Location = new System.Drawing.Point(5, 9);
            this.btn_DadosGerais_Cadastrar.Name = "btn_DadosGerais_Cadastrar";
            this.btn_DadosGerais_Cadastrar.Size = new System.Drawing.Size(141, 46);
            this.btn_DadosGerais_Cadastrar.TabIndex = 17;
            this.btn_DadosGerais_Cadastrar.Text = "      CADASTRAR";
            this.btn_DadosGerais_Cadastrar.UseVisualStyleBackColor = false;
            this.btn_DadosGerais_Cadastrar.Click += new System.EventHandler(this.btn_DadosGerais_Cadastrar_Click);
            // 
            // btn_RetornarModoPesquisa
            // 
            this.btn_RetornarModoPesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_RetornarModoPesquisa.BackColor = System.Drawing.Color.Lime;
            this.btn_RetornarModoPesquisa.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_RetornarModoPesquisa.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_RetornarModoPesquisa.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_RetornarModoPesquisa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_RetornarModoPesquisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RetornarModoPesquisa.Image = global::GenOR.Properties.Resources.Apply;
            this.btn_RetornarModoPesquisa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_RetornarModoPesquisa.Location = new System.Drawing.Point(967, 62);
            this.btn_RetornarModoPesquisa.Name = "btn_RetornarModoPesquisa";
            this.btn_RetornarModoPesquisa.Size = new System.Drawing.Size(286, 35);
            this.btn_RetornarModoPesquisa.TabIndex = 25;
            this.btn_RetornarModoPesquisa.Text = "      RETORNAR LINHA SELECIONADA";
            this.btn_RetornarModoPesquisa.UseVisualStyleBackColor = false;
            this.btn_RetornarModoPesquisa.Visible = false;
            this.btn_RetornarModoPesquisa.Click += new System.EventHandler(this.btn_RetornarModoPesquisa_Click);
            // 
            // FormGrupo_Unidade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.btn_ProdutosServicos_Vinculados);
            this.Controls.Add(this.btn_RetornarModoPesquisa);
            this.Controls.Add(this.btn_Materiais_Vinculados);
            this.Controls.Add(this.btn_DadosGerais_Imprimir);
            this.Controls.Add(this.btn_DadosGerais_Pesquisar);
            this.Controls.Add(this.btn_DadosGerais_AvancarGrid);
            this.Controls.Add(this.btn_DadosGerais_VoltarGrid);
            this.Controls.Add(this.btn_DadosGerais_Deletar);
            this.Controls.Add(this.btn_DadosGerais_Alterar);
            this.Controls.Add(this.btn_DadosGerais_Cadastrar);
            this.Controls.Add(this.tbc_Principal);
            this.Controls.Add(this.lb_LogoJanela);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormGrupo_Unidade";
            this.Text = "GERENCIADOR DE GRUPO / UNIDADE";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormGrupo_Unidade_FormClosing);
            this.Load += new System.EventHandler(this.FormGrupo_Unidade_Load);
            this.tbc_Principal.ResumeLayout(false);
            this.tbp_Principal_ListaRegistros.ResumeLayout(false);
            this.gb_ListagemGeral.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Grupo_Unidade)).EndInit();
            this.tbp_Principal_DadosRegistro.ResumeLayout(false);
            this.tbc_Secundario.ResumeLayout(false);
            this.tbp_Secundario_DadosGerais.ResumeLayout(false);
            this.gb_DadosGerais.ResumeLayout(false);
            this.gb_DadosGerais.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_DadosGerais_Pesquisar;
        private System.Windows.Forms.Button btn_DadosGerais_AvancarGrid;
        private System.Windows.Forms.Button btn_DadosGerais_VoltarGrid;
        private System.Windows.Forms.Button btn_DadosGerais_Deletar;
        private System.Windows.Forms.Button btn_DadosGerais_Alterar;
        private System.Windows.Forms.Button btn_DadosGerais_Cadastrar;
        private System.Windows.Forms.TabControl tbc_Principal;
        private System.Windows.Forms.TabPage tbp_Principal_ListaRegistros;
        private System.Windows.Forms.TabPage tbp_Principal_DadosRegistro;
        private System.Windows.Forms.TabControl tbc_Secundario;
        private System.Windows.Forms.Label lb_LogoJanela;
        private System.Windows.Forms.TabPage tbp_Secundario_DadosGerais;
        private System.Windows.Forms.GroupBox gb_DadosGerais;
        private System.Windows.Forms.GroupBox gb_ListagemGeral;
        private System.Windows.Forms.DataGridView dgv_Grupo_Unidade;
        private System.Windows.Forms.TextBox txtb_DescricaoGrupo_Unidade;
        private System.Windows.Forms.Label lb_DescricaoGrupo_Unidade;
        private System.Windows.Forms.Button btn_DadosGerais_Cancelar;
        private System.Windows.Forms.Button btn_DadosGerais_Confirmar;
        private System.Windows.Forms.TextBox txtb_DadosGerais_FundoVisual;
        private System.Windows.Forms.TextBox txtb_Sigla_Unidade;
        private System.Windows.Forms.Label lb_Sigla_Unidade;
        private System.Windows.Forms.TextBox txtb_CodigoGrupo_Unidade;
        private System.Windows.Forms.Label lb_CodigoGrupo_Unidade;
        private System.Windows.Forms.Button btn_DadosGerais_Imprimir;
        private System.Windows.Forms.Button btn_RetornarModoPesquisa;
        private System.Windows.Forms.Label lb_Material_Ou_Produto;
        private System.Windows.Forms.RadioButton rb_Produto;
        private System.Windows.Forms.RadioButton rb_Material;
        private System.Windows.Forms.TextBox txtb_DadosGerais_FundoVisual_RadioButtons;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSigla;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescricao;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaterial_Ou_Produto;
        private System.Windows.Forms.Button btn_Materiais_Vinculados;
        private System.Windows.Forms.Button btn_ProdutosServicos_Vinculados;
        private System.Windows.Forms.Label lb_Obrigatorio_3;
        private System.Windows.Forms.Label lb_Obrigatorio_1;
        private System.Windows.Forms.Label lb_Obrigatorio_2;
    }
}