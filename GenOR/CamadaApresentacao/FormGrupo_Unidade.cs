using CamadaObjetoTransferencia;
using CamadaProcessamento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using GenOR.Properties;
using Excel = Microsoft.Office.Interop.Excel;

namespace GenOR
{
    public partial class FormGrupo_Unidade : FormBase
    {
        #region Variaveis

        public GerenciarInformacaoRetornadoPesquisa gerenciarInformacaoRetornadoPesquisa;

        private Grupo_Unidade grupo_Unidade;
        private ListaGrupo_Unidade listaGrupo_Unidade;
        private ProcGrupo procGrupo;
        private ProcUnidade procUnidade;

        private string formularioDoGrupo_Ou_Unidade;
        private char grupoDoMaterial_Ou_Produto;

        private List<Grupo_Unidade> listaPesquisada;

        private GerenciarMensagensPadraoSistema gerenciarMensagensPadraoSistema;

        private GerenciarTelaLoading gerenciarTelaLoading;
        private FormTelaLoading formTelaLoading;
        private Thread carregarThread;
        private bool telaLoadingFoiAberta;

        private string operacaoRegistro;
        private bool encerramentoPadrao;

        private bool janelaModoPesquisa;
        private bool primeiraPesquisaModoPesquisa;

        private static int colunaCodigoIndex = 0;

        private string imagemButtomPesquisa;
        private FormTelaPesquisaGrupo_Unidade formTelaPesquisaGrupo_Unidade;

        private string arquivoListaParaImpressaoGrupo;
        private string arquivoListaParaImpressaoUnidade;
        private Excel.Application conexaoExcel;
        private Excel.Workbook arquivoExcel;
        private Excel.Worksheet planilhaExcel;

        #endregion

        public FormGrupo_Unidade(Grupo_Unidade objetoPesquisado, bool modoPesquisa, string formularioDoGrupo_Ou_Unidade, char grupoDoMaterial_Ou_Produto)
        {
            try
            {
                InitializeComponent();

                gerenciarInformacaoRetornadoPesquisa = new GerenciarInformacaoRetornadoPesquisa();
                gerenciarInformacaoRetornadoPesquisa.codigoRetornavelPesquisa = 0;

                grupo_Unidade = new Grupo_Unidade();
                listaGrupo_Unidade = new ListaGrupo_Unidade();
                procGrupo = new ProcGrupo();
                procUnidade = new ProcUnidade();

                this.formularioDoGrupo_Ou_Unidade = formularioDoGrupo_Ou_Unidade;
                this.grupoDoMaterial_Ou_Produto = grupoDoMaterial_Ou_Produto;

                listaPesquisada = new ListaGrupo_Unidade();

                gerenciarMensagensPadraoSistema = new GerenciarMensagensPadraoSistema();

                gerenciarTelaLoading = new GerenciarTelaLoading();
                formTelaLoading = new FormTelaLoading();
                carregarThread = null;
                telaLoadingFoiAberta = false;

                operacaoRegistro = "";
                encerramentoPadrao = true;

                janelaModoPesquisa = modoPesquisa;
                if (janelaModoPesquisa)
                {
                    AlterarLogo_ModoPesquisa(lb_LogoJanela, "                                            " + formularioDoGrupo_Ou_Unidade + "  (MODO PESQUISA)", 18);
                    primeiraPesquisaModoPesquisa = true;

                    btn_RetornarModoPesquisa.Visible = true;
                    this.WindowState = FormWindowState.Normal;
                }
                else
                {
                    lb_LogoJanela.Text = "                  " + formularioDoGrupo_Ou_Unidade;
                    primeiraPesquisaModoPesquisa = false;
                }

                if (objetoPesquisado != null)
                    grupo_Unidade.codigo = objetoPesquisado.codigo;

                imagemButtomPesquisa = "PESQUISAR";
                formTelaPesquisaGrupo_Unidade = new FormTelaPesquisaGrupo_Unidade(formularioDoGrupo_Ou_Unidade);

                arquivoListaParaImpressaoGrupo = Localizar_Imagem_Documento("LISTAGEM GRUPO.xlsx", false);
                arquivoListaParaImpressaoUnidade = Localizar_Imagem_Documento("LISTAGEM UNIDADE.xlsx", false);
                conexaoExcel = null;
                arquivoExcel = null;
                planilhaExcel = null;

                dgv_Grupo_Unidade.AutoGenerateColumns = false;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
                this.Close();
            }
        }

        #region Funções Gerais

        private void LimparCamposParaCadastro()
        {
            try
            {
                txtb_CodigoGrupo_Unidade.Clear();
                txtb_Sigla_Unidade.Clear();
                txtb_DescricaoGrupo_Unidade.Clear();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void ObterListagemTodosRegistrosDGV()
        {
            try
            {
                Grupo_Unidade item = new Grupo_Unidade()
                {
                    ativo_inativo = true
                };

                if (formularioDoGrupo_Ou_Unidade.Equals("GRUPO"))
                {
                    if (grupoDoMaterial_Ou_Produto.Equals('M') || grupoDoMaterial_Ou_Produto.Equals('P'))
                        item.material_ou_produto = grupoDoMaterial_Ou_Produto;

                    listaGrupo_Unidade = procGrupo.ConsultarRegistro(item, true);
                }
                else if (formularioDoGrupo_Ou_Unidade.Equals("UNIDADE"))
                    listaGrupo_Unidade = procUnidade.ConsultarRegistro(item, true);

                CarregarListaDGV();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void AdicionarLinhasDGV(Grupo_Unidade item)
        {
            try
            {
                if (formularioDoGrupo_Ou_Unidade.Equals("GRUPO"))
                {
                    string M_Ou_P = "";
                    if (item.material_ou_produto.Equals('M'))
                        M_Ou_P = "Material";
                    else if (item.material_ou_produto.Equals('P'))
                        M_Ou_P = "Produto";

                    dgv_Grupo_Unidade.Rows.Add(item.codigo, item.descricao, M_Ou_P);
                }
                else if (formularioDoGrupo_Ou_Unidade.Equals("UNIDADE"))
                    dgv_Grupo_Unidade.Rows.Add(item.codigo, item.sigla, item.descricao);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void CarregarListaDGV()
        {
            try
            {
                if (listaGrupo_Unidade.Count > 0)
                {
                    dgv_Grupo_Unidade.Rows.Clear();

                    foreach (Grupo_Unidade item in listaGrupo_Unidade)
                    {
                        AdicionarLinhasDGV(item);
                    }

                    if (janelaModoPesquisa && primeiraPesquisaModoPesquisa)
                    {
                        int resultado = 0;
                        primeiraPesquisaModoPesquisa = false;

                        Grupo_Unidade converter = new Grupo_Unidade();

                        foreach (Grupo_Unidade item in listaGrupo_Unidade.Where(lm => lm.codigo.Equals(grupo_Unidade.codigo)))
                        {
                            resultado = (int)item.codigo;
                            break;
                        }

                        if (!resultado.Equals(0))
                        {
                            converter = listaGrupo_Unidade.Where(lm => lm.codigo.Equals(grupo_Unidade.codigo)).Last();

                            foreach (DataGridViewRow item in dgv_Grupo_Unidade.Rows)
                            {
                                if (item.Cells[colunaCodigoIndex].Value.Equals((int)converter.codigo))
                                    dgv_Grupo_Unidade.CurrentCell = dgv_Grupo_Unidade.Rows[item.Index].Cells[colunaCodigoIndex];
                            }
                        }
                        else
                            dgv_Grupo_Unidade.Rows[0].Selected = true;
                    }
                    else
                    {
                        primeiraPesquisaModoPesquisa = false;
                        dgv_Grupo_Unidade.Rows[0].Selected = true;
                    }

                    btn_Materiais_Vinculados.Enabled = true;
                    btn_ProdutosServicos_Vinculados.Enabled = true;

                    PreencherCampos();
                }
                else
                {
                    btn_Materiais_Vinculados.Enabled = false;
                    btn_ProdutosServicos_Vinculados.Enabled = false;

                    primeiraPesquisaModoPesquisa = false;

                    dgv_Grupo_Unidade.Rows.Clear();

                    GerenciarBotoes_ListagemGeral(false, btn_DadosGerais_Alterar, btn_DadosGerais_Deletar, btn_DadosGerais_Imprimir, btn_DadosGerais_AvancarGrid,
                        btn_DadosGerais_Pesquisar, btn_DadosGerais_VoltarGrid);

                    LimparCamposParaCadastro();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void PreencherCampos()
        {
            try
            {
                if (dgv_Grupo_Unidade.RowCount > 0)
                {
                    telaLoadingFoiAberta = true;
                    gerenciarTelaLoading.Abrir(this, formTelaLoading, carregarThread);

                    int codigoLinhaSelecionada = Convert.ToInt32(dgv_Grupo_Unidade.Rows[dgv_Grupo_Unidade.CurrentCell.RowIndex].Cells[colunaCodigoIndex].Value);

                    object sender = new object();
                    EventArgs e = new EventArgs();

                    foreach (Grupo_Unidade item in listaGrupo_Unidade.Where(lm => lm.codigo.Equals(codigoLinhaSelecionada)))
                    {
                        txtb_CodigoGrupo_Unidade.Text = item.codigo.ToString();
                        txtb_DescricaoGrupo_Unidade.Text = item.descricao;

                        if (formularioDoGrupo_Ou_Unidade.Equals("GRUPO"))
                        {
                            if (item.material_ou_produto.Equals('M'))
                            {
                                rb_Material.Checked = true;

                                btn_Materiais_Vinculados.Enabled = true;
                                btn_ProdutosServicos_Vinculados.Enabled = false;
                            }
                            else if (item.material_ou_produto.Equals('P'))
                            {
                                rb_Produto.Checked = true;
                                
                                btn_Materiais_Vinculados.Enabled = false;
                                btn_ProdutosServicos_Vinculados.Enabled = true;
                            }
                        }
                        else if (formularioDoGrupo_Ou_Unidade.Equals("UNIDADE"))
                            txtb_Sigla_Unidade.Text = item.sigla;

                        if (primeiraPesquisaModoPesquisa.Equals(false))
                            grupo_Unidade = item;

                        break;
                    }
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
            finally
            {
                if (telaLoadingFoiAberta)
                {
                    telaLoadingFoiAberta = false;
                    gerenciarTelaLoading.Fechar();
                }
            }
        }

        private bool ValidandoCompilandoDadosCampos_TbPrincipal(Grupo_Unidade validandoGrupo_Unidade)
        {
            try
            {
                bool dadosValidos = true;

                validandoGrupo_Unidade.ativo_inativo = true;

                if (operacaoRegistro.Equals("ALTERAÇÃO") || operacaoRegistro.Equals("INATIVAÇÃO"))
                    validandoGrupo_Unidade.codigo = int.Parse(txtb_CodigoGrupo_Unidade.Text);
                else
                    validandoGrupo_Unidade.codigo = 0;

                if (dadosValidos)
                {
                    if (txtb_DescricaoGrupo_Unidade.Text.Trim().Equals(""))
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("DESCRIÇÃO " + formularioDoGrupo_Ou_Unidade);
                        dadosValidos = false;
                    }
                    else
                        validandoGrupo_Unidade.descricao = txtb_DescricaoGrupo_Unidade.Text;
                }

                if (dadosValidos && formularioDoGrupo_Ou_Unidade.Equals("UNIDADE"))
                {
                    if (txtb_Sigla_Unidade.Text.Trim().Equals(""))
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("SIGLA DA UNIDADE");
                        dadosValidos = false;
                    }
                    else
                        validandoGrupo_Unidade.sigla = txtb_Sigla_Unidade.Text;
                }

                if (dadosValidos && formularioDoGrupo_Ou_Unidade.Equals("GRUPO"))
                {
                    if (rb_Material.Checked.Equals(true))
                        validandoGrupo_Unidade.material_ou_produto = 'M';
                    else if (rb_Produto.Checked.Equals(true))
                        validandoGrupo_Unidade.material_ou_produto = 'P';
                    else
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("GRUPO DO MATERIAL OU PRODUTO");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                    dadosValidos = ValidandoCadastroRegistroDuplicado_TbPrincipal(validandoGrupo_Unidade);

                return dadosValidos;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
                return false;
            }
        }

        private bool ValidandoCadastroRegistroDuplicado_TbPrincipal(Grupo_Unidade validandoGrupo_Unidade)
        {
            try
            {
                bool dadosValidos = true;
                ListaGrupo_Unidade listaGU = new ListaGrupo_Unidade();

                Grupo_Unidade gu = new Grupo_Unidade()
                {
                    ativo_inativo = true
                };

                if (formularioDoGrupo_Ou_Unidade.Equals("GRUPO"))
                {
                    if (grupoDoMaterial_Ou_Produto.Equals('M') || grupoDoMaterial_Ou_Produto.Equals('P'))
                        gu.material_ou_produto = grupoDoMaterial_Ou_Produto;

                    listaGU = procGrupo.ConsultarRegistro(gu, true);
                }
                else if (formularioDoGrupo_Ou_Unidade.Equals("UNIDADE"))
                    listaGU = procUnidade.ConsultarRegistro(gu, true);

                foreach (Grupo_Unidade item in listaGU)
                {
                    if (item.descricao.Equals(validandoGrupo_Unidade.descricao) && item.ativo_inativo.Equals(true))
                    {
                        if (operacaoRegistro.Equals("CADASTRO"))
                        {
                            dadosValidos = false;
                            break;
                        }
                        else if (operacaoRegistro.Equals("ALTERAÇÃO"))
                        {
                            if (!item.codigo.Equals(validandoGrupo_Unidade.codigo))
                            {
                                dadosValidos = false;
                                break;
                            }
                        }
                    }

                    if (formularioDoGrupo_Ou_Unidade.Equals("UNIDADE"))
                    {
                        if (item.sigla.Equals(validandoGrupo_Unidade.sigla) && item.ativo_inativo.Equals(true))
                        {
                            if (operacaoRegistro.Equals("CADASTRO"))
                            {
                                dadosValidos = false;
                                break;
                            }
                            else if (operacaoRegistro.Equals("ALTERAÇÃO"))
                            {
                                if (!item.codigo.Equals(validandoGrupo_Unidade.codigo))
                                {
                                    dadosValidos = false;
                                    break;
                                }
                            }
                        }
                    }
                }

                if (dadosValidos.Equals(false))
                {
                    if (formularioDoGrupo_Ou_Unidade.Equals("UNIDADE"))
                        gerenciarMensagensPadraoSistema.RegistroDuplicado("Essa Unidade", "Código: " + validandoGrupo_Unidade.codigo + " - ( " + validandoGrupo_Unidade.sigla + " ) " + validandoGrupo_Unidade.descricao);
                    else
                        gerenciarMensagensPadraoSistema.RegistroDuplicado("Esse Grupo", "Código: " + validandoGrupo_Unidade.codigo + " - " + validandoGrupo_Unidade.descricao);
                }
                    
                return dadosValidos;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
                return false;
            }
        }

        private void ModificarTabelaExcel(string patchTabela)
        {
            try
            {
                List<Grupo_Unidade> listaImpressao = new ListaGrupo_Unidade();

                if (imagemButtomPesquisa.Equals("PESQUISAR"))
                    listaImpressao = listaGrupo_Unidade;
                else
                    listaImpressao = listaPesquisada;

                conexaoExcel = new Excel.Application();
                arquivoExcel = conexaoExcel.Workbooks.Open(patchTabela);
                planilhaExcel = arquivoExcel.Worksheets[1] as Excel.Worksheet;

                int linha = 2;
                foreach (Grupo_Unidade item in listaImpressao)
                {
                    int coluna = 1;

                    planilhaExcel.Cells[linha, coluna++].Value = item.codigo.ToString();

                    if (formularioDoGrupo_Ou_Unidade.Equals("UNIDADE"))
                        planilhaExcel.Cells[linha, coluna++].Value = item.sigla.ToString();

                    planilhaExcel.Cells[linha, coluna++].Value = item.descricao.ToString();

                    if (formularioDoGrupo_Ou_Unidade.Equals("GRUPO"))
                    {
                        string M_Ou_P = "";
                        if (item.material_ou_produto.Equals('M'))
                            M_Ou_P = "Material";
                        else if (item.material_ou_produto.Equals('P'))
                            M_Ou_P = "Produto";

                        planilhaExcel.Cells[linha, coluna++].Value = M_Ou_P;
                    }

                    linha++;
                }

                arquivoExcel.Save();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
            finally
            {
                arquivoExcel.Close();
                conexaoExcel.Quit();
                arquivoExcel = null;
                planilhaExcel = null;
                conexaoExcel = null;
            }
        }

        #endregion

        #region Eventos Form

        private void FormGrupo_Unidade_Load(object sender, EventArgs e)
        {
            try
            {
                if (formularioDoGrupo_Ou_Unidade.Equals("GRUPO"))
                {
                    string grupoMaterial_Produto;
                    if (grupoDoMaterial_Ou_Produto.Equals('M'))
                    {
                        grupoMaterial_Produto = " ( Materiais )";

                        btn_ProdutosServicos_Vinculados.Visible = false;
                    }
                    else if (grupoDoMaterial_Ou_Produto.Equals('P'))
                    {
                        grupoMaterial_Produto = " ( Produtos / Serviços )";

                        Point novaLocalizacao = new Point(502, 9);
                        btn_Materiais_Vinculados.Visible = false;
                        btn_ProdutosServicos_Vinculados.Location = novaLocalizacao;
                    }
                    else
                        grupoMaterial_Produto = " ( Materiais - Produtos / Serviços )";

                    this.Text = "GERENCIADOR DE GRUPO" + grupoMaterial_Produto;

                    dgv_Grupo_Unidade.Columns.Remove("colSigla");
                    
                    lb_Obrigatorio_2.Visible = false;
                    lb_Sigla_Unidade.Visible = false;
                    txtb_Sigla_Unidade.Visible = false;
                }
                else if (formularioDoGrupo_Ou_Unidade.Equals("UNIDADE"))
                {
                    this.Text = "GERENCIADOR DE UNIDADE";

                    dgv_Grupo_Unidade.Columns.Remove("colMaterial_Ou_produto");
                    
                    lb_Obrigatorio_1.Visible= false;
                    lb_Material_Ou_Produto.Visible = false;
                    rb_Material.Visible = false;
                    rb_Produto.Visible = false;
                    
                    txtb_DadosGerais_FundoVisual_RadioButtons.Visible = false;
                }

                ObterListagemTodosRegistrosDGV();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void FormGrupo_Unidade_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (encerramentoPadrao)
                {
                    string condicaoExtra = "";

                    if (!btn_RetornarModoPesquisa.Visible.Equals(true))
                    {
                        if (!operacaoRegistro.Equals(""))
                            condicaoExtra = "durante o processo de " + operacaoRegistro;
                    }
                    else
                        condicaoExtra = "sem Retornar nenhum registro pesquisado";

                    if (gerenciarMensagensPadraoSistema.Mensagem_FechamentoJanela(condicaoExtra).Equals(DialogResult.Cancel))
                        e.Cancel = true;
                    else
                        gerenciarInformacaoRetornadoPesquisa.codigoRetornavelPesquisa = 0;
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos KeyPress

        private void txtb_Sigla_Unidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Enter_FocusTxtb(txtb_DescricaoGrupo_Unidade, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_DescricaoGrupo_Unidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Enter_FocusButton(btn_DadosGerais_Confirmar, e))
                    btn_DadosGerais_Confirmar_Click(sender, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Button Click

        private void btn_DadosGerais_Cadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                btn_Materiais_Vinculados.Enabled = false;
                btn_ProdutosServicos_Vinculados.Enabled = false;

                Formulario_ModoModificacao(gb_ListagemGeral, gb_DadosGerais, tbc_Principal, tbc_Secundario, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar,
                    btn_DadosGerais_Deletar, btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                operacaoRegistro = "CADASTRO";
                LimparCamposParaCadastro();

                if (formularioDoGrupo_Ou_Unidade.Equals("GRUPO"))
                {
                    if (grupoDoMaterial_Ou_Produto.Equals('M'))
                        rb_Material.Checked = true;
                    else if (grupoDoMaterial_Ou_Produto.Equals('P'))
                        rb_Produto.Checked = true;
                    else
                    {
                        rb_Material.Enabled = true;
                        rb_Produto.Enabled = true;
                    }
                    
                    txtb_DescricaoGrupo_Unidade.Focus();
                }
                else if (formularioDoGrupo_Ou_Unidade.Equals("UNIDADE"))
                    txtb_Sigla_Unidade.Focus();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_DadosGerais_Alterar_Click(object sender, EventArgs e)
        {
            try
            {
                btn_Materiais_Vinculados.Enabled = false;
                btn_ProdutosServicos_Vinculados.Enabled = false;

                Formulario_ModoModificacao(gb_ListagemGeral, gb_DadosGerais, tbc_Principal, tbc_Secundario, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar,
                    btn_DadosGerais_Deletar, btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                operacaoRegistro = "ALTERAÇÃO";

                if (formularioDoGrupo_Ou_Unidade.Equals("GRUPO"))
                    txtb_DescricaoGrupo_Unidade.Focus();
                else if (formularioDoGrupo_Ou_Unidade.Equals("UNIDADE"))
                    txtb_Sigla_Unidade.Focus();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_DadosGerais_Deletar_Click(object sender, EventArgs e)
        {
            try
            {
                operacaoRegistro = "INATIVAÇÃO";
                btn_DadosGerais_Confirmar_Click(sender, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_DadosGerais_Imprimir_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog diretorioDestinoArquivoImpressao = new FolderBrowserDialog();

                if (diretorioDestinoArquivoImpressao.ShowDialog().Equals(DialogResult.OK) && !string.IsNullOrWhiteSpace(diretorioDestinoArquivoImpressao.SelectedPath))
                {
                    telaLoadingFoiAberta = true;
                    gerenciarTelaLoading.Abrir(this, formTelaLoading, carregarThread);

                    string nomeArquivo = "";

                    if (formularioDoGrupo_Ou_Unidade.Equals("GRUPO"))
                        nomeArquivo = Path.GetFileName(arquivoListaParaImpressaoGrupo);
                    else if (formularioDoGrupo_Ou_Unidade.Equals("UNIDADE"))
                        nomeArquivo = Path.GetFileName(arquivoListaParaImpressaoUnidade);

                    string nomeArquivoModificado = nomeArquivo.Replace(".xlsx", "");
                    nomeArquivoModificado = nomeArquivoModificado + " " + DateTime.Now.ToString("dd-MM-yyyy") + ".xlsx";

                    string arquivoListaModificado = Path.Combine(diretorioDestinoArquivoImpressao.SelectedPath, nomeArquivoModificado);

                    if (formularioDoGrupo_Ou_Unidade.Equals("GRUPO"))
                        File.Copy(arquivoListaParaImpressaoGrupo, arquivoListaModificado, true);
                    else if (formularioDoGrupo_Ou_Unidade.Equals("UNIDADE"))
                        File.Copy(arquivoListaParaImpressaoUnidade, arquivoListaModificado, true);

                    ModificarTabelaExcel(arquivoListaModificado);

                    if (telaLoadingFoiAberta)
                    {
                        telaLoadingFoiAberta = false;
                        gerenciarTelaLoading.Fechar();
                    }

                    if (gerenciarMensagensPadraoSistema.Mensagem_AbrirDocumento().Equals(DialogResult.OK))
                        Process.Start(arquivoListaModificado);
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_DadosGerais_VoltarGrid_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_Grupo_Unidade.CurrentRow.Index > 0)
                {
                    int linha = MudarLinha_DGV(dgv_Grupo_Unidade, false);

                    dgv_Grupo_Unidade.CurrentCell = dgv_Grupo_Unidade.Rows[linha].Cells[dgv_Grupo_Unidade.CurrentCell.ColumnIndex];
                    PreencherCampos();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_DadosGerais_AvancarGrid_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_Grupo_Unidade.CurrentRow.Index < (dgv_Grupo_Unidade.RowCount - 1))
                {
                    int linha = MudarLinha_DGV(dgv_Grupo_Unidade, true);

                    dgv_Grupo_Unidade.CurrentCell = dgv_Grupo_Unidade.Rows[linha].Cells[dgv_Grupo_Unidade.CurrentCell.ColumnIndex];
                    PreencherCampos();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_DadosGerais_Confirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (gerenciarMensagensPadraoSistema.Mensagem_Confirmacao(operacaoRegistro).Equals(DialogResult.OK))
                {
                    Grupo_Unidade objetoChecagem = new Grupo_Unidade();

                    string statusOperacao = "EM ANDAMENTO";

                    if (ValidandoCompilandoDadosCampos_TbPrincipal(objetoChecagem))
                    {
                        if (operacaoRegistro.Equals("CADASTRO") || operacaoRegistro.Equals("ALTERAÇÃO") || operacaoRegistro.Equals("INATIVAÇÃO"))
                        {
                            if (formularioDoGrupo_Ou_Unidade.Equals("GRUPO"))
                                statusOperacao = procGrupo.ManterRegistro(objetoChecagem, operacaoRegistro);
                            else if (formularioDoGrupo_Ou_Unidade.Equals("UNIDADE"))
                                statusOperacao = procUnidade.ManterRegistro(objetoChecagem, operacaoRegistro);
                        }
                        else
                        {
                            gerenciarMensagensPadraoSistema.ExceptionBancoDados("A operação informada não existe !");
                            return;
                        }

                        int codigoRegistroRetornado = 0;
                        if (int.TryParse(statusOperacao, out codigoRegistroRetornado))
                        {
                            grupo_Unidade = objetoChecagem;
                            grupo_Unidade.codigo = codigoRegistroRetornado;

                            bool gerenciarModoJanela = janelaModoPesquisa;
                            if (gerenciarModoJanela.Equals(false))
                                janelaModoPesquisa = true;

                            primeiraPesquisaModoPesquisa = true;

                            if (imagemButtomPesquisa.Equals("CANCELAR PESQUISA"))
                                btn_DadosGerais_Pesquisar_Click(sender, e);
                            else
                                ObterListagemTodosRegistrosDGV();

                            if (gerenciarModoJanela.Equals(false))
                                janelaModoPesquisa = false;

                            Formulario_ModoConsulta(gb_ListagemGeral, gb_DadosGerais, dgv_Grupo_Unidade, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar, btn_DadosGerais_Deletar,
                                btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                            rb_Material.Enabled = false;
                            rb_Produto.Enabled = false;

                            operacaoRegistro = "";
                        }
                        else
                        {
                            gerenciarMensagensPadraoSistema.ExceptionBancoDados(statusOperacao);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_DadosGerais_Cancelar_Click(object sender, EventArgs e)
        {
            try
            {
                if (gerenciarMensagensPadraoSistema.Mensagem_Cancelamento().Equals(DialogResult.OK))
                {
                    Formulario_ModoConsulta(gb_ListagemGeral, gb_DadosGerais, dgv_Grupo_Unidade, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar, btn_DadosGerais_Deletar,
                        btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                    if (imagemButtomPesquisa.Equals("CANCELAR PESQUISA"))
                        btn_DadosGerais_Pesquisar_Click(sender, e);
                    else
                        ObterListagemTodosRegistrosDGV();

                    rb_Material.Enabled = false;
                    rb_Produto.Enabled = false;

                    operacaoRegistro = "";
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Button Pesquisa

        private void btn_DadosGerais_Pesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                if (imagemButtomPesquisa.Equals("PESQUISAR"))
                {
                    formTelaPesquisaGrupo_Unidade = new FormTelaPesquisaGrupo_Unidade(formularioDoGrupo_Ou_Unidade);
                    formTelaPesquisaGrupo_Unidade.ShowDialog();

                    if (!formTelaPesquisaGrupo_Unidade.campoPesquisado.Equals("CANCELADO") && !formTelaPesquisaGrupo_Unidade.informaçãoRetornada.Equals("VAZIA"))
                    {
                        listaPesquisada = new ListaGrupo_Unidade();

                        if (formTelaPesquisaGrupo_Unidade.campoPesquisado.Equals("CÓDIGO"))
                        {
                            listaPesquisada = listaGrupo_Unidade.Where(gu => gu.codigo.Equals(int.Parse(formTelaPesquisaGrupo_Unidade.informaçãoRetornada))).ToList();
                        }
                        else if (formTelaPesquisaGrupo_Unidade.campoPesquisado.Equals("SIGLA"))
                        {
                            listaPesquisada = listaGrupo_Unidade.Where(gu => gu.sigla.Contains(formTelaPesquisaGrupo_Unidade.informaçãoRetornada)).ToList();
                        }
                        else if (formTelaPesquisaGrupo_Unidade.campoPesquisado.Equals("DESCRIÇÃO"))
                        {
                            listaPesquisada = listaGrupo_Unidade.Where(gu => gu.descricao.Contains(formTelaPesquisaGrupo_Unidade.informaçãoRetornada)).ToList();
                        }
                        else if (formTelaPesquisaGrupo_Unidade.campoPesquisado.Equals("MATERIAL OU PRODUTO"))
                        {
                            listaPesquisada = listaGrupo_Unidade.Where(gu => gu.material_ou_produto.Equals(char.Parse(formTelaPesquisaGrupo_Unidade.informaçãoRetornada))).ToList();
                        }
                        else
                        {
                            gerenciarMensagensPadraoSistema.InformacaoPesquisadaNaoEncontrada();
                            return;
                        }

                        if (listaPesquisada.Count() > 0)
                        {
                            dgv_Grupo_Unidade.Rows.Clear();

                            foreach (Grupo_Unidade item in listaPesquisada)
                            {
                                AdicionarLinhasDGV(item);
                            }

                            dgv_Grupo_Unidade.Rows[0].Selected = true;
                            PreencherCampos();
                        }
                        else
                        {
                            gerenciarMensagensPadraoSistema.InformacaoPesquisadaNaoEncontrada();
                            return;
                        }

                        btn_DadosGerais_Pesquisar.Image = Resources.Delete;
                        btn_DadosGerais_Pesquisar.Text = " CANCELAR PESQUISA";
                        imagemButtomPesquisa = "CANCELAR PESQUISA";
                    }
                }
                else
                {
                    ObterListagemTodosRegistrosDGV();

                    btn_DadosGerais_Pesquisar.Image = Resources.Find;
                    btn_DadosGerais_Pesquisar.Text = "      PESQUISAR";
                    imagemButtomPesquisa = "PESQUISAR";

                    operacaoRegistro = "";
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_RetornarModoPesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                if (listaGrupo_Unidade.Count > 0)
                {
                    if (gerenciarMensagensPadraoSistema.Mensagem_Confirmacao("RETORNAR LINHA SELECIONADA").Equals(DialogResult.OK))
                    {
                        gerenciarInformacaoRetornadoPesquisa.codigoRetornavelPesquisa = (int)grupo_Unidade.codigo;

                        encerramentoPadrao = false;
                        this.Close();
                    }
                }
                else
                {
                    gerenciarInformacaoRetornadoPesquisa.codigoRetornavelPesquisa = 0;
                    
                    encerramentoPadrao = false;
                    this.Close();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }
        
        private void btn_Materiais_Vinculados_Click(object sender, EventArgs e)
        {
            try
            {
                using (FormMateriaisOuProdutos_Vinculados_GrupoUnidade formMateriaisOuProdutos_Vinculados_GrupoUnidade = new FormMateriaisOuProdutos_Vinculados_GrupoUnidade(grupo_Unidade, "MATERIAL", formularioDoGrupo_Ou_Unidade, this))
                {
                    formMateriaisOuProdutos_Vinculados_GrupoUnidade.ShowDialog();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_ProdutosServicos_Vinculados_Click(object sender, EventArgs e)
        {
            try
            {
                using (FormMateriaisOuProdutos_Vinculados_GrupoUnidade formMateriaisOuProdutos_Vinculados_GrupoUnidade = new FormMateriaisOuProdutos_Vinculados_GrupoUnidade(grupo_Unidade, "PRODUTO", formularioDoGrupo_Ou_Unidade, this))
                {
                    formMateriaisOuProdutos_Vinculados_GrupoUnidade.ShowDialog();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Data Grid View

        private void dgv_Grupo_Unidade_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue.Equals(((int)Keys.Up)) || e.KeyValue.Equals(((int)Keys.Down)))
                    PreencherCampos();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void dgv_Grupo_Unidade_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                var linhaClicada = dgv_Grupo_Unidade.HitTest(e.X, e.Y);
                if (linhaClicada != null && linhaClicada.RowIndex >= 0)
                {
                    if (e.Button.Equals(MouseButtons.Right))
                        dgv_Grupo_Unidade.CurrentCell = dgv_Grupo_Unidade.Rows[linhaClicada.RowIndex].Cells[0];

                    PreencherCampos();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void dgv_Grupo_Unidade_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                var linhaClicada = dgv_Grupo_Unidade.HitTest(e.X, e.Y);
                if (linhaClicada != null && linhaClicada.RowIndex >= 0)
                {
                    if (e.Button.Equals(MouseButtons.Left))
                        tbc_Principal.SelectedIndex = 1;
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos MouseHovar

        private void lb_Obrigatorio_1_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_1, "outros");
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void lb_Obrigatorio_2_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_2, "texto");
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void lb_Obrigatorio_3_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_3, "texto");
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

    }
}
