using CamadaObjetoTransferencia;
using CamadaProcessamento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using GenOR.Properties;
using System.Diagnostics;

namespace GenOR
{
    public partial class FormProduto_Servico : FormBase
    {
        #region Variaveis Produto/Serviço

        public GerenciarInformacaoRetornadoPesquisa gerenciarInformacaoRetornadoPesquisa;

        private Produto_Servico produto_Servico;
        private ListaProduto_Servico listaProduto_Servico;
        private ProcProduto_Servico procProduto_Servico;

        private List<Produto_Servico> listaPesquisada;

        private GerenciarMensagensPadraoSistema gerenciarMensagensPadraoSistema;

        private GerenciarTelaLoading gerenciarTelaLoading;
        private FormTelaLoading formTelaLoading;
        private Thread carregarThread;
        private bool telaLoadingFoiAberta;

        private string operacaoRegistro;
        private bool encerramentoPadrao;

        private bool janelaModoPesquisa;
        private bool primeiraPesquisaModoPesquisa;

        private Produto_Servico checkLinhasAtivas;
        private static int colunaCodigoIndex = 0;

        private string imagemButtomPesquisa;
        private FormTelaPesquisaProduto_Servico formTelaPesquisaProduto_Servico;

        private string imagemFundoPadrao;
        private OpenFileDialog buscadorImagem_OFD;

        private string restaurarImagem;

        private string imagemButtomZoom;

        private string arquivoListaParaImpressao;
        private Excel.Application conexaoExcel;
        private Excel.Workbook arquivoExcel;
        private Excel.Worksheet planilhaExcel;

        #endregion

        #region Variaveis Materiais do Produto/Serviço

        private Materiais_Produto_Servico materiais_Produto_Servico;
        private ListaMateriais_Produto_Servico listaMateriais_Produto_Servico;
        private ProcMateriais_Produto_Servico procMateriais_Produto_Servico;

        private string operacaoRegistro_Materiais_Produto_Servico;

        private Materiais_Produto_Servico checkLinhasAtivas_Materiais_Produto_Servico;

        private string arquivoListaParaImpressao_Materiais_Produto_Servico;

        private decimal valorTotalMateriais;

        #endregion

        public FormProduto_Servico(Produto_Servico objetoPesquisado, bool modoPesquisa)
        {
            try
            {
                InitializeComponent();
                
                #region Produto/Serviço

                gerenciarInformacaoRetornadoPesquisa = new GerenciarInformacaoRetornadoPesquisa();
                gerenciarInformacaoRetornadoPesquisa.codigoRetornavelPesquisa = 0;

                produto_Servico = new Produto_Servico();
                InstanciamentoRapida_ProdutoServico(produto_Servico);
                listaProduto_Servico = new ListaProduto_Servico();
                procProduto_Servico = new ProcProduto_Servico();

                listaPesquisada = new ListaProduto_Servico();

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
                    AlterarLogo_ModoPesquisa(lb_LogoJanela, "                             PRODUTO/SERVIÇO  (MODO PESQUISA)", 18);
                    primeiraPesquisaModoPesquisa = true;

                    btn_RetornarModoPesquisa.Visible = true;
                    this.WindowState = FormWindowState.Normal;
                }
                else
                    primeiraPesquisaModoPesquisa = false;

                if (objetoPesquisado != null)
                    produto_Servico.codigo = objetoPesquisado.codigo;

                imagemButtomPesquisa = "PESQUISAR";
                formTelaPesquisaProduto_Servico = new FormTelaPesquisaProduto_Servico();

                imagemFundoPadrao = Localizar_Imagem_Documento("Picture.png", true);
                buscadorImagem_OFD = new OpenFileDialog();
                buscadorImagem_OFD.Title = "BUSCANDO IMAGEM DO PRODUTO/SERVIÇO";
                buscadorImagem_OFD.Filter = "PNG (*.png)|*.png| JPEG (*.jpeg)|*jpeg| JPG (*.jpg)|*jpg| Todos Arquivos(*.*)|*.*";
                buscadorImagem_OFD.FileName = imagemFundoPadrao;

                restaurarImagem = null;

                imagemButtomZoom = "ZOOM-IN";

                pb_ExibindoImagemDGV.Load(imagemFundoPadrao);

                arquivoListaParaImpressao = Localizar_Imagem_Documento("LISTAGEM PRODUTO SERVIÇO.xlsx", false);
                conexaoExcel = null;
                arquivoExcel = null;
                planilhaExcel = null;

                dgv_ProdutoServico.AutoGenerateColumns = false;

                #endregion

                #region Materiais do Produto/Serviço

                materiais_Produto_Servico = new Materiais_Produto_Servico();
                InstanciamentoRapida_Materiais_ProdutoServico(materiais_Produto_Servico);

                listaMateriais_Produto_Servico = new ListaMateriais_Produto_Servico();
                procMateriais_Produto_Servico = new ProcMateriais_Produto_Servico();

                operacaoRegistro_Materiais_Produto_Servico = "";

                pb_ImagemMaterial.Load(Localizar_Imagem_Documento("Picture.png", true));

                arquivoListaParaImpressao_Materiais_Produto_Servico = Localizar_Imagem_Documento("LISTAGEM MATERIAIS PRODUTO SERVIÇO.xlsx",false);

                dgv_Materiais_Produto_Servico.AutoGenerateColumns = false;

                valorTotalMateriais = 0;

                #endregion

            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
                this.Close();
            }
        }

        #region Eventos Form

        private void FormProduto_Servico_Load(object sender, EventArgs e)
        {
            try
            {
                ObterListagemTodosRegistrosDGV();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void FormProduto_Servico_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (encerramentoPadrao)
                {
                    string condicaoExtra = "";

                    if (! btn_RetornarModoPesquisa.Visible.Equals(true))
                    {
                        if (!operacaoRegistro.Equals(""))
                            condicaoExtra = "durante o processo de " + operacaoRegistro;
                        else if (!operacaoRegistro_Materiais_Produto_Servico.Equals(""))
                            condicaoExtra = "durante o processo de " + operacaoRegistro_Materiais_Produto_Servico + " dos Materiais do Produtos/Serviços";
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

        private void FormProduto_Servico_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (janelaModoPesquisa)
                {
                    bool maximized = this.WindowState == FormWindowState.Maximized;
                    if (maximized)
                        AlterarLogo_ModoPesquisa(lb_LogoJanela, "                   PRODUTO/SERVIÇO  (MODO PESQUISA)", 30);
                    else
                        AlterarLogo_ModoPesquisa(lb_LogoJanela, "                             PRODUTO/SERVIÇO  (MODO PESQUISA)", 18);
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Produto/Serviço

        #region Funções Gerais

        private void LimparCamposParaCadastro()
        {
            try
            {
                object sender = new object();
                EventArgs e = new EventArgs();

                txtb_Codigo_ProdutoServico.Clear();
                mtxtb_UltimaAtualizacao_ProdutoServico.Clear();
                txtb_Descricao_ProdutoServico.Clear();

                txtb_CodigoUnidade_ProdutoServico.Clear();
                txtb_SiglaUnidade_ProdutoServico.Clear();
                txtb_DescricaoUnidade_ProdutoServico.Clear();

                txtb_Altura_ProdutoServico.Clear();
                txtb_Altura_ProdutoServico_Leave(sender, e);
                txtb_Largura_ProdutoServico.Clear();
                txtb_Largura_ProdutoServico_Leave(sender, e);
                txtb_Comprimento_ProdutoServico.Clear();
                txtb_Comprimento_ProdutoServico_Leave(sender, e);

                txtb_CodigoGrupo_ProdutoServico.Clear();
                txtb_DescricaoGrupo_ProdutoServico.Clear();

                txtb_ValorMateriais_ProdutoServico.Clear();
                txtb_ValorMateriais_ProdutoServico_Leave(sender, e);

                txtb_ValorMaoObra_ProdutoServico.Clear();
                txtb_ValorMaoObra_ProdutoServico_Leave(sender, e);

                txtb_ValorTotal_ProdutoServico.Clear();
                txtb_ValorTotal_ProdutoServico_Leave(sender, e);
                
                txtb_MaoObra_ProdutoServico.Text = "0";
                txtb_MaoObra_ProdutoServico_Leave(sender, e);
                
                pb_ImagemProdutoServico.Load(imagemFundoPadrao);
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
                Produto_Servico item = new Produto_Servico
                {
                    ativo_inativo = true
                };
                InstanciamentoRapida_ProdutoServico(item);

                listaProduto_Servico = procProduto_Servico.ConsultarRegistro(item, true);

                CarregarListaDGV();
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
                if (listaProduto_Servico.Count > 0)
                {
                    dgv_ProdutoServico.Rows.Clear();
                    checkLinhasAtivas = new Produto_Servico();
                    InstanciamentoRapida_ProdutoServico(checkLinhasAtivas);

                    foreach (Produto_Servico item in listaProduto_Servico)
                    {
                        AdicionarLinhasDGV(item);
                    }

                    if (janelaModoPesquisa && primeiraPesquisaModoPesquisa)
                    {
                        int resultado = 0;
                        primeiraPesquisaModoPesquisa = false;

                        Produto_Servico converter = new Produto_Servico();
                        InstanciamentoRapida_ProdutoServico(converter);

                        foreach (Produto_Servico item in listaProduto_Servico.Where(ps => ps.codigo.Equals(produto_Servico.codigo)))
                        {
                            resultado = (int)item.codigo;
                            break;
                        }

                        if (!resultado.Equals(0))
                        {
                            converter = listaProduto_Servico.Where(lm => lm.codigo.Equals(produto_Servico.codigo)).Last();

                            foreach (DataGridViewRow item in dgv_ProdutoServico.Rows)
                            {
                                if (item.Cells[colunaCodigoIndex].Value.Equals((int)converter.codigo))
                                    dgv_ProdutoServico.CurrentCell = dgv_ProdutoServico.Rows[item.Index].Cells[colunaCodigoIndex];
                            }
                        }
                        else
                            dgv_ProdutoServico.Rows[0].Selected = true;
                    }
                    else
                    {
                        primeiraPesquisaModoPesquisa = false;
                        dgv_ProdutoServico.Rows[0].Selected = true;
                    }

                    if (!tbc_Secundario.TabPages.Contains(tbp_Secundario_Material_ProdutoServico))
                        Formulario_AtivarAbaSecundaria(tbc_Secundario, tbp_Secundario_Material_ProdutoServico, 1);
                    
                    PreencherCampos();
                }
                else
                {
                    primeiraPesquisaModoPesquisa = false;

                    dgv_ProdutoServico.Rows.Clear();
                    dgv_Materiais_Produto_Servico.Rows.Clear();

                    GerenciarBotoes_ListagemGeral(false, btn_DadosGerais_Alterar, btn_DadosGerais_Deletar, btn_DadosGerais_Imprimir, btn_DadosGerais_AvancarGrid,
                        btn_DadosGerais_Pesquisar, btn_DadosGerais_VoltarGrid);

                    LimparCamposParaCadastro();
                    LimparCamposParaCadastro_Materiais_Produto_Servico();

                    Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_Material_ProdutoServico);
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }
        
        private void AdicionarLinhasDGV(Produto_Servico item)
        {
            try
            {
                string maoObraFormatado = Convert.ToString(item.maoObra);
                string valorTotalMateriaisFormatado = Convert.ToString(item.valor_total_materiais);
                string valorMaoObraFormatado = Convert.ToString(item.valor_maoObra);
                string valorTotalFormatado = Convert.ToString(item.valor_total);

                checkLinhasAtivas = item;

                dgv_ProdutoServico.Rows.Add(item.codigo, DateTime.Parse(item.ultima_atualizacao.ToString()).ToString("dd/MM/yyyy HH:mm"), item.descricao, item.Grupo.descricao,
                    item.Unidade.sigla, item.altura, item.largura, item.comprimento, decimal.Parse(maoObraFormatado).ToString("N2", new CultureInfo("pt-BR")) + " %",
                    decimal.Parse(valorTotalMateriaisFormatado).ToString("C2", new CultureInfo("pt-BR")),
                    decimal.Parse(valorMaoObraFormatado).ToString("C2", new CultureInfo("pt-BR")), decimal.Parse(valorTotalFormatado).ToString("C2", new CultureInfo("pt-BR")));
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
                if (dgv_ProdutoServico.RowCount > 0)
                {
                    telaLoadingFoiAberta = true;
                    gerenciarTelaLoading.Abrir(this, formTelaLoading, carregarThread);

                    int codigoLinhaSelecionada = Convert.ToInt32(dgv_ProdutoServico.Rows[dgv_ProdutoServico.CurrentCell.RowIndex].Cells[colunaCodigoIndex].Value);

                    object sender = new object();
                    EventArgs e = new EventArgs();

                    foreach (Produto_Servico item in listaProduto_Servico.Where(ps => ps.codigo.Equals(codigoLinhaSelecionada)))
                    {
                        txtb_Codigo_ProdutoServico.Text = item.codigo.ToString();
                        mtxtb_UltimaAtualizacao_ProdutoServico.Text = DateTime.Parse(item.ultima_atualizacao.ToString()).ToString("dd/MM/yyyy HH:mm");
                        txtb_Descricao_ProdutoServico.Text = item.descricao;

                        txtb_CodigoUnidade_ProdutoServico.Text = item.Unidade.codigo.ToString();
                        txtb_SiglaUnidade_ProdutoServico.Text = item.Unidade.sigla;
                        txtb_DescricaoUnidade_ProdutoServico.Text = item.Unidade.descricao;
                        if (item.Unidade.ativo_inativo.Equals(false))
                        {
                            PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade_ProdutoServico, false, false);
                            PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade_ProdutoServico, false, false);
                            PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade_ProdutoServico, false, false);
                        }
                        else
                        {
                            PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade_ProdutoServico, true, false);
                            PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade_ProdutoServico, true, false);
                            PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade_ProdutoServico, true, false);
                        }

                        txtb_Altura_ProdutoServico.Text = item.altura.ToString();
                        txtb_Altura_ProdutoServico_Leave(sender, e);
                        txtb_Largura_ProdutoServico.Text = item.largura.ToString();
                        txtb_Largura_ProdutoServico_Leave(sender, e);
                        txtb_Comprimento_ProdutoServico.Text = item.comprimento.ToString();
                        txtb_Comprimento_ProdutoServico_Leave(sender, e);

                        txtb_CodigoGrupo_ProdutoServico.Text = item.Grupo.codigo.ToString();
                        txtb_DescricaoGrupo_ProdutoServico.Text = item.Grupo.descricao;
                        if (item.Grupo.ativo_inativo.Equals(false) || !item.Grupo.material_ou_produto.Equals('P'))
                        {
                            PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo_ProdutoServico, false, false);
                            PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo_ProdutoServico, false, false);
                        }
                        else
                        {
                            PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo_ProdutoServico, true, false);
                            PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo_ProdutoServico, true, false);
                        }
                        
                        txtb_ValorMateriais_ProdutoServico.Text = item.valor_total_materiais.ToString();
                        txtb_ValorMateriais_ProdutoServico_Leave(sender, e);

                        txtb_ValorMaoObra_ProdutoServico.Text = item.valor_maoObra.ToString();
                        txtb_ValorMaoObra_ProdutoServico_Leave(sender, e);

                        txtb_ValorTotal_ProdutoServico.Text = item.valor_total.ToString();
                        txtb_ValorTotal_ProdutoServico_Leave(sender, e);
                        
                        txtb_MaoObra_ProdutoServico.Text = item.maoObra.ToString();
                        txtb_MaoObra_ProdutoServico_Leave(sender, e);
                        
                        if (File.Exists(item.imagem))
                        {
                            pb_ImagemProdutoServico.Load(item.imagem);
                            buscadorImagem_OFD.FileName = item.imagem;

                            pb_ExibindoImagemDGV.Load(item.imagem);
                        }
                        else
                        {
                            pb_ImagemProdutoServico.Load(imagemFundoPadrao);
                            buscadorImagem_OFD.FileName = imagemFundoPadrao;

                            pb_ExibindoImagemDGV.Load(imagemFundoPadrao);

                            item.imagem = imagemFundoPadrao;
                        }

                        if (primeiraPesquisaModoPesquisa.Equals(false))
                            produto_Servico = item;

                        break;
                    }
                    
                    if (txtb_CodigoUnidade_ProdutoServico.BackColor.Equals(System.Drawing.Color.RosyBrown)
                        || txtb_CodigoGrupo_ProdutoServico.BackColor.Equals(System.Drawing.Color.RosyBrown))
                    {
                        Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_Material_ProdutoServico);
                    }
                    else
                    {
                        if (!tbc_Secundario.TabPages.Contains(tbp_Secundario_Material_ProdutoServico))
                            Formulario_AtivarAbaSecundaria(tbc_Secundario, tbp_Secundario_Material_ProdutoServico, 1);

                        primeiraPesquisaModoPesquisa = true;
                        ObterListagemRegistrosDGV_Materiais_Produto_Servico();
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

        private bool ValidandoCompilandoDadosCampos_TbPrincipal(Produto_Servico validandoProdutoServico)
        {
            try
            {
                object sender = new object();
                EventArgs e = new EventArgs();

                bool dadosValidos = true;
                int variavelSaidaCodigo = 0;
                decimal variavelSaidaCamposDecimais = 0;
                
                validandoProdutoServico.ultima_atualizacao = DateTime.Now;
                validandoProdutoServico.ativo_inativo = true;

                if (operacaoRegistro.Equals("ALTERAÇÃO") || operacaoRegistro.Equals("INATIVAÇÃO"))
                    validandoProdutoServico.codigo = int.Parse(txtb_Codigo_ProdutoServico.Text);
                else
                    validandoProdutoServico.codigo = 0;

                if (dadosValidos)
                {
                    if (txtb_Descricao_ProdutoServico.Text.Trim().Equals(""))
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("DESCRIÇÃO DO PRODUTO/SERVIÇO");
                        dadosValidos = false;
                    }
                    else
                        validandoProdutoServico.descricao = txtb_Descricao_ProdutoServico.Text;
                }

                if (dadosValidos)
                {
                    if (int.TryParse(txtb_CodigoUnidade_ProdutoServico.Text.Trim(), out variavelSaidaCodigo))
                    {
                        if (txtb_CodigoUnidade_ProdutoServico.BackColor.Equals(System.Drawing.Color.RosyBrown))
                        {
                            gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("UNIDADE DO PRODUTO/SERVIÇO");
                            dadosValidos = false;
                        }
                        else
                        {
                            validandoProdutoServico.Unidade.codigo = variavelSaidaCodigo;
                            validandoProdutoServico.Unidade.sigla = txtb_SiglaUnidade_ProdutoServico.Text;
                            validandoProdutoServico.Unidade.descricao = txtb_DescricaoUnidade_ProdutoServico.Text;
                        }
                    }
                    else
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("UNIDADE DO PRODUTO/SERVIÇO");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    variavelSaidaCamposDecimais = CampoMoeda_RemoverFormatacao(txtb_Altura_ProdutoServico);
                    if (variavelSaidaCamposDecimais >= 0)
                    {
                        validandoProdutoServico.altura = variavelSaidaCamposDecimais;
                        txtb_Altura_ProdutoServico_Leave(sender, e);
                    }
                    else
                    {
                        txtb_Altura_ProdutoServico_Leave(sender, e);
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("ALTURA DO PRODUTO/SERVIÇO");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    variavelSaidaCamposDecimais = CampoMoeda_RemoverFormatacao(txtb_Largura_ProdutoServico);
                    if (variavelSaidaCamposDecimais >= 0)
                    {
                        validandoProdutoServico.largura = variavelSaidaCamposDecimais;
                        txtb_Largura_ProdutoServico_Leave(sender, e);
                    }
                    else
                    {
                        txtb_Largura_ProdutoServico_Leave(sender, e);
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("LARGURA DO PRODUTO/SERVIÇO");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    variavelSaidaCamposDecimais = CampoMoeda_RemoverFormatacao(txtb_Comprimento_ProdutoServico);
                    if (variavelSaidaCamposDecimais >= 0)
                    {
                        validandoProdutoServico.comprimento = variavelSaidaCamposDecimais;
                        txtb_Comprimento_ProdutoServico_Leave(sender, e);
                    }
                    else
                    {
                        txtb_Comprimento_ProdutoServico_Leave(sender, e);
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("COMPRIMENTO DO PRODUTO/SERVIÇO");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    if (int.TryParse(txtb_CodigoGrupo_ProdutoServico.Text.Trim(), out variavelSaidaCodigo))
                    {
                        if (txtb_CodigoGrupo_ProdutoServico.BackColor.Equals(System.Drawing.Color.RosyBrown))
                        {
                            gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("GRUPO DO PRODUTO/SERVIÇO");
                            dadosValidos = false;
                        }
                        else
                        {
                            validandoProdutoServico.Grupo.codigo = variavelSaidaCodigo;
                            validandoProdutoServico.Grupo.descricao = txtb_DescricaoGrupo_ProdutoServico.Text;
                            validandoProdutoServico.Grupo.material_ou_produto = 'P';
                        }
                    }
                    else
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("GRUPO DO PRODUTO/SERVIÇO");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    variavelSaidaCamposDecimais = CampoMoeda_RemoverFormatacao(txtb_MaoObra_ProdutoServico);
                    if (variavelSaidaCamposDecimais >= 0)
                    {
                        validandoProdutoServico.maoObra = variavelSaidaCamposDecimais;
                        txtb_MaoObra_ProdutoServico_Leave(sender, e);
                    }
                    else
                    {
                        txtb_MaoObra_ProdutoServico_Leave(sender, e);
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("MÃO OBRA DO PRODUTO/SERVIÇO");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    variavelSaidaCamposDecimais = CampoMoeda_RemoverFormatacao(txtb_ValorMateriais_ProdutoServico);
                    if (variavelSaidaCamposDecimais >= 0)
                    {
                        validandoProdutoServico.valor_total_materiais = variavelSaidaCamposDecimais;
                        txtb_ValorMateriais_ProdutoServico_Leave(sender, e);
                    }
                    else
                    {
                        txtb_ValorMateriais_ProdutoServico_Leave(sender, e);
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("VALOR MATERIAIS DO PRODUTO/SERVIÇO");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    variavelSaidaCamposDecimais = CampoMoeda_RemoverFormatacao(txtb_ValorMaoObra_ProdutoServico);
                    if (variavelSaidaCamposDecimais >= 0)
                    {
                        validandoProdutoServico.valor_maoObra = variavelSaidaCamposDecimais;
                        txtb_ValorMaoObra_ProdutoServico_Leave(sender, e);
                    }
                    else
                    {
                        txtb_ValorMaoObra_ProdutoServico_Leave(sender, e);
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("VALOR MÃO OBRA DO PRODUTO/SERVIÇO");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    variavelSaidaCamposDecimais = CampoMoeda_RemoverFormatacao(txtb_ValorTotal_ProdutoServico);
                    if (variavelSaidaCamposDecimais >= 0)
                    {
                        validandoProdutoServico.valor_total = variavelSaidaCamposDecimais;
                        txtb_ValorTotal_ProdutoServico_Leave(sender, e);
                    }
                    else
                    {
                        txtb_ValorTotal_ProdutoServico_Leave(sender, e);
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("VALOR TOTAL DO PRODUTO/SERVIÇO");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    if (buscadorImagem_OFD.CheckFileExists)
                    {
                        string nomeImagem = Path.GetFileName(buscadorImagem_OFD.FileName);

                        if (nomeImagem.Equals("Picture.png")
                            || (nomeImagem.Equals(Path.GetFileName(produto_Servico.imagem)) && (operacaoRegistro.Equals("ALTERAÇÃO") || operacaoRegistro.Equals("INATIVAÇÃO"))))
                        {
                            validandoProdutoServico.imagem = buscadorImagem_OFD.FileName;
                        }
                        else
                        {
                            OpenFileDialog novoDiretorioImagem = new OpenFileDialog();
                            novoDiretorioImagem.FileName = imagemFundoPadrao;

                            bool imagemExiste = true;
                            int contadorCopias = 1;

                            string nomeModificadoImagem = nomeImagem.Replace(".", contadorCopias + ".");
                            novoDiretorioImagem.FileName = novoDiretorioImagem.FileName.Replace("Picture.png", nomeModificadoImagem);
                            do
                            {
                                if (File.Exists(novoDiretorioImagem.FileName))
                                {
                                    novoDiretorioImagem.FileName = novoDiretorioImagem.FileName.Replace(nomeModificadoImagem, nomeModificadoImagem.Replace(contadorCopias + ".", (contadorCopias + 1) + "."));
                                    contadorCopias++;
                                }
                                else
                                {
                                    imagemExiste = false;
                                    validandoProdutoServico.imagem = novoDiretorioImagem.FileName;
                                }

                            } while (imagemExiste);
                        }
                    }
                    else
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("ARQUIVO DE IMAGEM");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                    dadosValidos = ValidandoCadastroRegistroDuplicado_TbPrincipal(validandoProdutoServico);

                return dadosValidos;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
                return false;
            }
        }

        private bool ValidandoCadastroRegistroDuplicado_TbPrincipal(Produto_Servico validandoProdutoServico)
        {
            try
            {
                bool dadosValidos = true;

                Produto_Servico ps = new Produto_Servico
                {
                    ativo_inativo = true
                };
                InstanciamentoRapida_ProdutoServico(ps);

                ListaProduto_Servico listaPS = procProduto_Servico.ConsultarRegistro(ps, true);

                foreach (Produto_Servico item in listaPS)
                {
                    if (item.descricao.Equals(validandoProdutoServico.descricao) && item.ativo_inativo.Equals(true))
                    {
                        if (operacaoRegistro.Equals("CADASTRO"))
                            dadosValidos = false;
                        else if (operacaoRegistro.Equals("ALTERAÇÃO"))
                        {
                            if (!item.codigo.Equals(validandoProdutoServico.codigo))
                                dadosValidos = false;
                        }

                        break;
                    }
                }

                if (dadosValidos.Equals(false))
                    gerenciarMensagensPadraoSistema.RegistroDuplicado("Esse Produto/Serviço", "Código: " + validandoProdutoServico.codigo + " - " + validandoProdutoServico.descricao);

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
                List<Produto_Servico> listaImpressao = new ListaProduto_Servico();

                if (imagemButtomPesquisa.Equals("PESQUISAR"))
                    listaImpressao = listaProduto_Servico;
                else
                    listaImpressao = listaPesquisada;

                conexaoExcel = new Excel.Application();
                arquivoExcel = conexaoExcel.Workbooks.Open(patchTabela);
                planilhaExcel = arquivoExcel.Worksheets[1] as Excel.Worksheet;

                int linha = 2;
                foreach (Produto_Servico item in listaImpressao)
                {
                    int coluna = 1;

                    planilhaExcel.Cells[linha, coluna++].Value = item.codigo.ToString();
                    planilhaExcel.Cells[linha, coluna++].Value = item.ultima_atualizacao.ToString();
                    planilhaExcel.Cells[linha, coluna++].Value = item.descricao.ToString();
                    planilhaExcel.Cells[linha, coluna++].Value = "(" + item.Grupo.codigo.ToString() + ") " + item.Grupo.descricao.ToString();
                    planilhaExcel.Cells[linha, coluna++].Value = "(" + item.Unidade.codigo.ToString() + ") ( " + item.Unidade.sigla.ToString() + " ) - " + item.Unidade.descricao.ToString();
                    planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.maoObra.ToString()).ToString("N2", new CultureInfo("pt-BR")) + " %";
                    planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.valor_total_materiais.ToString()).ToString("C2", new CultureInfo("pt-BR"));
                    planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.valor_maoObra.ToString()).ToString("C2", new CultureInfo("pt-BR"));
                    planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.valor_total.ToString()).ToString("C2", new CultureInfo("pt-BR"));
                    planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.altura.ToString()).ToString("N2", new CultureInfo("pt-BR"));
                    planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.largura.ToString()).ToString("N2", new CultureInfo("pt-BR"));
                    planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.comprimento.ToString()).ToString("N2", new CultureInfo("pt-BR"));

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

        #region Eventos Enter

        private void txtb_Altura_ProdutoServico_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_Altura_ProdutoServico);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_Largura_ProdutoServico_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_Largura_ProdutoServico);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_Comprimento_ProdutoServico_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_Comprimento_ProdutoServico);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_MaoObra_ProdutoServico_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_MaoObra_ProdutoServico);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ValorMateriais_ProdutoServico_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_ValorMateriais_ProdutoServico);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ValorMaoObra_ProdutoServico_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_ValorMaoObra_ProdutoServico);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ValorTotal_ProdutoServico_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_ValorTotal_ProdutoServico);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos KeyPress

        private void txtb_Descricao_ProdutoServico_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Enter_FocusTxtb(txtb_CodigoUnidade_ProdutoServico, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_CodigoUnidade_ProdutoServico_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_Altura_ProdutoServico, e))
                    CampoMoeda_KeyPress(sender, e, true);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_Altura_ProdutoServico_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_Largura_ProdutoServico, e))
                    CampoMoeda_KeyPress(sender, e, false);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_Largura_ProdutoServico_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_Comprimento_ProdutoServico, e))
                    CampoMoeda_KeyPress(sender, e, false);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_Comprimento_ProdutoServico_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_CodigoGrupo_ProdutoServico, e))
                    CampoMoeda_KeyPress(sender, e, false);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_CodigoGrupo_ProdutoServico_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_MaoObra_ProdutoServico, e))
                    CampoMoeda_KeyPress(sender, e, true);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_MaoObra_ProdutoServico_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusButton(btn_DadosGerais_Confirmar, e))
                    CampoMoeda_KeyPress(sender, e, false);
                else
                    btn_DadosGerais_Confirmar_Click(sender, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Leave

        private void txtb_CodigoUnidade_ProdutoServico_Leave(object sender, EventArgs e)
        {
            try
            {
                telaLoadingFoiAberta = true;
                gerenciarTelaLoading.Abrir(this, formTelaLoading, carregarThread);

                int codigoRetorno = 0;
                if (int.TryParse(txtb_CodigoUnidade_ProdutoServico.Text, out codigoRetorno) && codigoRetorno > 0)
                {
                    Grupo_Unidade obj = new Grupo_Unidade()
                    {
                        codigo = codigoRetorno,
                        ativo_inativo = true
                    };

                    ProcUnidade procUnidade = new ProcUnidade();

                    ListaGrupo_Unidade listaUnidade = new ListaGrupo_Unidade();
                    listaUnidade = procUnidade.ConsultarRegistro(obj, false);

                    if (listaUnidade.Count > 0)
                    {
                        foreach (Grupo_Unidade item in listaUnidade)
                        {
                            if (item.codigo.Equals(codigoRetorno) && item.ativo_inativo.Equals(true))
                            {
                                txtb_CodigoUnidade_ProdutoServico.Text = item.codigo.ToString();
                                txtb_SiglaUnidade_ProdutoServico.Text = item.sigla;
                                txtb_DescricaoUnidade_ProdutoServico.Text = item.descricao;

                                PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade_ProdutoServico, true, false);
                                PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade_ProdutoServico, true, false);
                                PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade_ProdutoServico, true, false);

                                break;
                            }
                            else
                            {
                                telaLoadingFoiAberta = false;
                                gerenciarTelaLoading.Fechar();

                                gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CÓDIGO UNIDADE DO PRODUTO SERVIÇO");
                                PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade_ProdutoServico, false, false);
                                PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade_ProdutoServico, false, true);
                                PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade_ProdutoServico, false, true);

                                break;
                            }
                        }
                    }
                    else
                    {
                        telaLoadingFoiAberta = false;
                        gerenciarTelaLoading.Fechar();

                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CÓDIGO UNIDADE DO PRODUTO SERVIÇO");
                        PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade_ProdutoServico, false, false);
                        PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade_ProdutoServico, false, true);
                        PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade_ProdutoServico, false, true);
                    }
                }
                else
                {
                    telaLoadingFoiAberta = false;
                    gerenciarTelaLoading.Fechar();

                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CÓDIGO UNIDADE DO PRODUTO SERVIÇO");
                    PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade_ProdutoServico, false, false);
                    PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade_ProdutoServico, false, true);
                    PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade_ProdutoServico, false, true);
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

        private void txtb_Altura_ProdutoServico_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_Altura_ProdutoServico, false);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_Largura_ProdutoServico_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_Largura_ProdutoServico, false);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_Comprimento_ProdutoServico_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_Comprimento_ProdutoServico, false);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_CodigoGrupo_ProdutoServico_Leave(object sender, EventArgs e)
        {
            try
            {
                telaLoadingFoiAberta = true;
                gerenciarTelaLoading.Abrir(this, formTelaLoading, carregarThread);

                int codigoRetorno = 0;
                if (int.TryParse(txtb_CodigoGrupo_ProdutoServico.Text, out codigoRetorno) && codigoRetorno > 0)
                {
                    Grupo_Unidade obj = new Grupo_Unidade()
                    {
                        codigo = codigoRetorno,
                        material_ou_produto = 'P',
                        ativo_inativo = true
                    };

                    ProcGrupo procGrupo = new ProcGrupo();

                    ListaGrupo_Unidade listaGrupo = new ListaGrupo_Unidade();
                    listaGrupo = procGrupo.ConsultarRegistro(obj, false);

                    if (listaGrupo.Count > 0)
                    {
                        foreach (Grupo_Unidade item in listaGrupo)
                        {
                            if (item.codigo.Equals(codigoRetorno) && item.material_ou_produto.Equals('P') && item.ativo_inativo.Equals(true))
                            {
                                txtb_CodigoGrupo_ProdutoServico.Text = item.codigo.ToString();
                                txtb_DescricaoGrupo_ProdutoServico.Text = item.descricao;

                                PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo_ProdutoServico, true, false);
                                PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo_ProdutoServico, true, false);

                                break;
                            }
                            else
                            {
                                telaLoadingFoiAberta = false;
                                gerenciarTelaLoading.Fechar();

                                gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CÓDIGO GRUPO DO PRODUTO SERVIÇO");
                                PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo_ProdutoServico, false, false);
                                PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo_ProdutoServico, false, true);

                                break;
                            }
                        }
                    }
                    else
                    {
                        telaLoadingFoiAberta = false;
                        gerenciarTelaLoading.Fechar();

                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CÓDIGO GRUPO DO PRODUTO SERVIÇO");
                        PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo_ProdutoServico, false, false);
                        PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo_ProdutoServico, false, true);
                    }
                }
                else
                {
                    telaLoadingFoiAberta = false;
                    gerenciarTelaLoading.Fechar();

                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CÓDIGO GRUPO DO PRODUTO SERVIÇO");
                    PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo_ProdutoServico, false, false);
                    PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo_ProdutoServico, false, true);
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

        private void txtb_MaoObra_ProdutoServico_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_MaoObra_ProdutoServico);
                CampoMoeda_FormatacaoDecimal(txtb_MaoObra_ProdutoServico, false);

                txtb_ValorMateriais_ProdutoServico_Enter(sender, e);
                txtb_ValorMaoObra_ProdutoServico_Enter(sender, e);
                txtb_ValorTotal_ProdutoServico_Enter(sender, e);

                decimal calcularValor_MaoObra = (decimal.Parse(txtb_MaoObra_ProdutoServico.Text) * decimal.Parse(txtb_ValorMateriais_ProdutoServico.Text)) / 100;
                txtb_ValorMaoObra_ProdutoServico.Text = calcularValor_MaoObra.ToString();

                decimal calcularValorTotal_ProdutoServico = decimal.Parse(txtb_ValorMateriais_ProdutoServico.Text) + calcularValor_MaoObra;
                txtb_ValorTotal_ProdutoServico.Text = calcularValorTotal_ProdutoServico.ToString();

                CampoFormatacao_Porcentagem(txtb_MaoObra_ProdutoServico);

                txtb_ValorMateriais_ProdutoServico_Leave(sender, e);
                txtb_ValorMaoObra_ProdutoServico_Leave(sender, e);
                txtb_ValorTotal_ProdutoServico_Leave(sender, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ValorMateriais_ProdutoServico_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorMateriais_ProdutoServico, true);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ValorMaoObra_ProdutoServico_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorMaoObra_ProdutoServico, true);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ValorTotal_ProdutoServico_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorTotal_ProdutoServico, true);
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
                if (pb_ExibindoImagemDGV.Visible.Equals(true))
                {
                    pb_ExibindoImagemDGV.Visible = false;
                    btn_FecharImagemDGV.Visible = false;
                }

                Formulario_ModoModificacao(gb_ListagemGeral, gb_DadosGerais, tbc_Principal, tbc_Secundario, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar,
                    btn_DadosGerais_Deletar, btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_Material_ProdutoServico);

                operacaoRegistro = "CADASTRO";
                pb_ImagemProdutoServico.Load(imagemFundoPadrao);
                buscadorImagem_OFD.FileName = imagemFundoPadrao;
                LimparCamposParaCadastro();

                txtb_Descricao_ProdutoServico.Focus();

                PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade_ProdutoServico, true, true);
                PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade_ProdutoServico, true, true);
                PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade_ProdutoServico, true, true);

                PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo_ProdutoServico, true, true);
                PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo_ProdutoServico, true, true);

                restaurarImagem = buscadorImagem_OFD.FileName;
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
                if (pb_ExibindoImagemDGV.Visible.Equals(true))
                {
                    pb_ExibindoImagemDGV.Visible = false;
                    btn_FecharImagemDGV.Visible = false;
                }

                Formulario_ModoModificacao(gb_ListagemGeral, gb_DadosGerais, tbc_Principal, tbc_Secundario, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar,
                    btn_DadosGerais_Deletar, btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_Material_ProdutoServico);

                operacaoRegistro = "ALTERAÇÃO";
                buscadorImagem_OFD.FileName = pb_ImagemProdutoServico.ImageLocation;

                txtb_Descricao_ProdutoServico.Focus();

                restaurarImagem = buscadorImagem_OFD.FileName;
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
                    if (pb_ExibindoImagemDGV.Visible.Equals(true))
                    {
                        pb_ExibindoImagemDGV.Visible = false;
                        btn_FecharImagemDGV.Visible = false;
                    }

                    telaLoadingFoiAberta = true;
                    gerenciarTelaLoading.Abrir(this, formTelaLoading, carregarThread);

                    string nomeArquivo = Path.GetFileName(arquivoListaParaImpressao);

                    string nomeArquivoModificado = nomeArquivo.Replace(".xlsx", "");
                    nomeArquivoModificado = nomeArquivoModificado + " " + DateTime.Now.ToString("dd-MM-yyyy") + ".xlsx";

                    string arquivoListaModificado = Path.Combine(diretorioDestinoArquivoImpressao.SelectedPath, nomeArquivoModificado);

                    File.Copy(arquivoListaParaImpressao, arquivoListaModificado, true);

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
                if (dgv_ProdutoServico.CurrentRow.Index > 0)
                {
                    int linha = MudarLinha_DGV(dgv_ProdutoServico, false);

                    dgv_ProdutoServico.CurrentCell = dgv_ProdutoServico.Rows[linha].Cells[dgv_ProdutoServico.CurrentCell.ColumnIndex];
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
                if (dgv_ProdutoServico.CurrentRow.Index < (dgv_ProdutoServico.RowCount - 1))
                {
                    int linha = MudarLinha_DGV(dgv_ProdutoServico, true);

                    dgv_ProdutoServico.CurrentCell = dgv_ProdutoServico.Rows[linha].Cells[dgv_ProdutoServico.CurrentCell.ColumnIndex];
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
                    Produto_Servico objetoChecagem = new Produto_Servico();
                    InstanciamentoRapida_ProdutoServico(objetoChecagem);

                    string statusOperacao = "EM ANDAMENTO";

                    if (ValidandoCompilandoDadosCampos_TbPrincipal(objetoChecagem))
                    {
                        if (operacaoRegistro.Equals("CADASTRO") || operacaoRegistro.Equals("ALTERAÇÃO") || operacaoRegistro.Equals("INATIVAÇÃO"))
                            statusOperacao = procProduto_Servico.ManterRegistro(objetoChecagem, operacaoRegistro);
                        else
                        {
                            gerenciarMensagensPadraoSistema.ExceptionBancoDados("A operação informada não existe !");
                            return;
                        }

                        int codigoRegistroRetornado = 0;
                        if (int.TryParse(statusOperacao, out codigoRegistroRetornado))
                        {
                            string imagemAntiga = produto_Servico.imagem;

                            produto_Servico = objetoChecagem;
                            produto_Servico.codigo = codigoRegistroRetornado;

                            if (operacaoRegistro.Equals("CADASTRO") && !produto_Servico.imagem.Equals(imagemFundoPadrao))
                            {
                                if (!produto_Servico.imagem.Equals(imagemFundoPadrao))
                                    File.Copy(buscadorImagem_OFD.FileName, produto_Servico.imagem, false);
                            }
                            else if (operacaoRegistro.Equals("ALTERAÇÃO"))
                            {
                                if (!imagemAntiga.Equals(imagemFundoPadrao) && !produto_Servico.imagem.Equals(imagemAntiga))
                                {
                                    pb_ExibindoImagemDGV.Load(imagemFundoPadrao);
                                    pb_ImagemProdutoServico.Load(imagemFundoPadrao);

                                    File.Delete(imagemAntiga);
                                    File.Copy(buscadorImagem_OFD.FileName, produto_Servico.imagem, false);
                                }
                                else if (imagemAntiga.Equals(imagemFundoPadrao) && !produto_Servico.imagem.Equals(imagemAntiga))
                                    File.Copy(buscadorImagem_OFD.FileName, produto_Servico.imagem, false);
                            }
                            else if (operacaoRegistro.Equals("INATIVAÇÃO"))
                            {
                                if (!imagemAntiga.Equals(imagemFundoPadrao))
                                {
                                    pb_ExibindoImagemDGV.Load(imagemFundoPadrao);
                                    pb_ImagemProdutoServico.Load(imagemFundoPadrao);

                                    File.Delete(imagemAntiga);
                                }
                            }

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

                            Formulario_ModoConsulta(gb_ListagemGeral, gb_DadosGerais, dgv_ProdutoServico, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar, btn_DadosGerais_Deletar,
                                btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                            btn_ReverterImagem_ProdutoServico.Visible = false;
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
            finally
            {
                restaurarImagem = null;
            }
        }

        private void btn_DadosGerais_Cancelar_Click(object sender, EventArgs e)
        {
            try
            {
                if (gerenciarMensagensPadraoSistema.Mensagem_Cancelamento().Equals(DialogResult.OK))
                {
                    Formulario_ModoConsulta(gb_ListagemGeral, gb_DadosGerais, dgv_ProdutoServico, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar, btn_DadosGerais_Deletar,
                        btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                    btn_ReverterImagem_ProdutoServico.Visible = false;

                    if (imagemButtomPesquisa.Equals("CANCELAR PESQUISA"))
                        btn_DadosGerais_Pesquisar_Click(sender, e);
                    else
                        ObterListagemTodosRegistrosDGV();

                    operacaoRegistro = "";
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
            finally
            {
                restaurarImagem = null;
            }
        }

        private void btn_AdicionarImagem_ProdutoServico_Click(object sender, EventArgs e)
        {
            try
            {
                string preRestauracao_Imagem = buscadorImagem_OFD.FileName;

                if (buscadorImagem_OFD.ShowDialog().Equals(DialogResult.OK))
                {
                    string imagemPesquisada_OFD = Path.GetExtension(buscadorImagem_OFD.FileName);

                    if (ValidarImagem_OpenFile(imagemPesquisada_OFD))
                    {
                        pb_ImagemProdutoServico.Load(buscadorImagem_OFD.FileName);
                        btn_ReverterImagem_ProdutoServico.Visible = true;
                    }
                    else
                    {
                        buscadorImagem_OFD.FileName = preRestauracao_Imagem;
                        gerenciarMensagensPadraoSistema.ArquivoImagemInvalido();
                    }
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_ZoomImagem_ProdutoServico_Click(object sender, EventArgs e)
        {
            try
            {
                imagemButtomZoom = Imagem_ZoomIn_ZoomOut(imagemButtomZoom, btn_ZoomImagem_ProdutoServico, btn_ReverterImagem_ProdutoServico, pb_ImagemProdutoServico);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_ReverterImagem_ProdutoServico_Click(object sender, EventArgs e)
        {
            try
            {
                if (gerenciarMensagensPadraoSistema.Mensagem_Confirmacao("RETORNANDO A IMAGEM ANTERIOR").Equals(DialogResult.OK))
                {
                    pb_ImagemProdutoServico.Load(restaurarImagem);
                    buscadorImagem_OFD.FileName = restaurarImagem;
                    btn_ReverterImagem_ProdutoServico.Visible = false;
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
                    formTelaPesquisaProduto_Servico = new FormTelaPesquisaProduto_Servico();
                    formTelaPesquisaProduto_Servico.ShowDialog();

                    if (!formTelaPesquisaProduto_Servico.campoPesquisado.Equals("CANCELADO") && !formTelaPesquisaProduto_Servico.informaçãoRetornada.Equals("VAZIA"))
                    {
                        listaPesquisada = new ListaProduto_Servico();

                        if (formTelaPesquisaProduto_Servico.campoPesquisado.Equals("CÓDIGO"))
                        {
                            listaPesquisada = listaProduto_Servico.Where(ps => ps.codigo.Equals(int.Parse(formTelaPesquisaProduto_Servico.informaçãoRetornada))).ToList();
                        }
                        else if (formTelaPesquisaProduto_Servico.campoPesquisado.Equals("ULTIMA ATUALIZAÇÃO") && !formTelaPesquisaProduto_Servico.informaçãoRetornada2.Equals("VAZIA"))
                        {
                            listaPesquisada = listaProduto_Servico.Where(ps => DateTime.Parse(ps.ultima_atualizacao.ToString()).Date >= DateTime.Parse(formTelaPesquisaProduto_Servico.informaçãoRetornada).Date
                            && DateTime.Parse(ps.ultima_atualizacao.ToString()).Date <= DateTime.Parse(formTelaPesquisaProduto_Servico.informaçãoRetornada2).Date).ToList();
                        }
                        else if (formTelaPesquisaProduto_Servico.campoPesquisado.Equals("DESCRIÇÃO"))
                        {
                            listaPesquisada = listaProduto_Servico.Where(ps => ps.descricao.Contains(formTelaPesquisaProduto_Servico.informaçãoRetornada)).ToList();
                        }
                        else if (formTelaPesquisaProduto_Servico.campoPesquisado.Equals("GRUPO"))
                        {
                            listaPesquisada = listaProduto_Servico.Where(ps => ps.Grupo.descricao.Contains(formTelaPesquisaProduto_Servico.informaçãoRetornada)).ToList();
                        }
                        else if (formTelaPesquisaProduto_Servico.campoPesquisado.Equals("UNIDADE"))
                        {
                            listaPesquisada = listaProduto_Servico.Where(ps => ps.Unidade.sigla.Contains(formTelaPesquisaProduto_Servico.informaçãoRetornada)).ToList();
                        }
                        else if (formTelaPesquisaProduto_Servico.campoPesquisado.Equals("LARGURA") && !formTelaPesquisaProduto_Servico.informaçãoRetornada2.Equals("VAZIA"))
                        {
                            listaPesquisada = listaProduto_Servico.Where(ps => decimal.Parse(ps.largura.ToString()) >= decimal.Parse(formTelaPesquisaProduto_Servico.informaçãoRetornada)
                            && decimal.Parse(ps.largura.ToString()) <= decimal.Parse(formTelaPesquisaProduto_Servico.informaçãoRetornada2)).ToList();
                        }
                        else if (formTelaPesquisaProduto_Servico.campoPesquisado.Equals("ALTURA") && !formTelaPesquisaProduto_Servico.informaçãoRetornada2.Equals("VAZIA"))
                        {
                            listaPesquisada = listaProduto_Servico.Where(ps => decimal.Parse(ps.altura.ToString()) >= decimal.Parse(formTelaPesquisaProduto_Servico.informaçãoRetornada)
                            && decimal.Parse(ps.altura.ToString()) <= decimal.Parse(formTelaPesquisaProduto_Servico.informaçãoRetornada2)).ToList();
                        }
                        else if (formTelaPesquisaProduto_Servico.campoPesquisado.Equals("COMPRIMENTO") && !formTelaPesquisaProduto_Servico.informaçãoRetornada2.Equals("VAZIA"))
                        {
                            listaPesquisada = listaProduto_Servico.Where(ps => decimal.Parse(ps.comprimento.ToString()) >= decimal.Parse(formTelaPesquisaProduto_Servico.informaçãoRetornada)
                            && decimal.Parse(ps.comprimento.ToString()) <= decimal.Parse(formTelaPesquisaProduto_Servico.informaçãoRetornada2)).ToList();
                        }
                        else if (formTelaPesquisaProduto_Servico.campoPesquisado.Equals("MÃO OBRA") && !formTelaPesquisaProduto_Servico.informaçãoRetornada2.Equals("VAZIA"))
                        {
                            listaPesquisada = listaProduto_Servico.Where(ps => decimal.Parse(ps.maoObra.ToString()) >= decimal.Parse(formTelaPesquisaProduto_Servico.informaçãoRetornada)
                            && decimal.Parse(ps.maoObra.ToString()) <= decimal.Parse(formTelaPesquisaProduto_Servico.informaçãoRetornada2)).ToList();
                        }
                        else if (formTelaPesquisaProduto_Servico.campoPesquisado.Equals("VALOR MATERIAIS") && !formTelaPesquisaProduto_Servico.informaçãoRetornada2.Equals("VAZIA"))
                        {
                            listaPesquisada = listaProduto_Servico.Where(ps => decimal.Parse(ps.valor_total_materiais.ToString()) >= decimal.Parse(formTelaPesquisaProduto_Servico.informaçãoRetornada)
                            && decimal.Parse(ps.valor_total_materiais.ToString()) <= decimal.Parse(formTelaPesquisaProduto_Servico.informaçãoRetornada2)).ToList();
                        }
                        else if (formTelaPesquisaProduto_Servico.campoPesquisado.Equals("VALOR MÃO OBRA") && !formTelaPesquisaProduto_Servico.informaçãoRetornada2.Equals("VAZIA"))
                        {
                            listaPesquisada = listaProduto_Servico.Where(ps => decimal.Parse(ps.valor_maoObra.ToString()) >= decimal.Parse(formTelaPesquisaProduto_Servico.informaçãoRetornada)
                            && decimal.Parse(ps.valor_maoObra.ToString()) <= decimal.Parse(formTelaPesquisaProduto_Servico.informaçãoRetornada2)).ToList();
                        }
                        else if (formTelaPesquisaProduto_Servico.campoPesquisado.Equals("VALOR TOTAL") && !formTelaPesquisaProduto_Servico.informaçãoRetornada2.Equals("VAZIA"))
                        {
                            listaPesquisada = listaProduto_Servico.Where(ps => decimal.Parse(ps.valor_total.ToString()) >= decimal.Parse(formTelaPesquisaProduto_Servico.informaçãoRetornada)
                            && decimal.Parse(ps.valor_total.ToString()) <= decimal.Parse(formTelaPesquisaProduto_Servico.informaçãoRetornada2)).ToList();
                        }
                        else
                        {
                            gerenciarMensagensPadraoSistema.InformacaoPesquisadaNaoEncontrada();
                            return;
                        }

                        if (listaPesquisada.Count() > 0)
                        {
                            dgv_ProdutoServico.Rows.Clear();

                            foreach (Produto_Servico item in listaPesquisada)
                            {
                                AdicionarLinhasDGV(item);
                            }

                            dgv_ProdutoServico.Rows[0].Selected = true;
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
                if (listaMateriais_Produto_Servico.Count > 0)
                {
                    if (!dgv_ProdutoServico.Rows[dgv_ProdutoServico.CurrentRow.Index].DefaultCellStyle.BackColor.Equals(System.Drawing.Color.RosyBrown))
                    {
                        if (gerenciarMensagensPadraoSistema.Mensagem_Confirmacao("RETORNAR LINHA SELECIONADA").Equals(DialogResult.OK))
                        {
                            gerenciarInformacaoRetornadoPesquisa.codigoRetornavelPesquisa = (int)produto_Servico.codigo;

                            encerramentoPadrao = false;
                            this.Close();
                        }
                    }
                    else
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("QUE ESTÁ SELECIONADO NA LISTA DE PRODUTO/SERVIÇO");
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

        private void btn_PesquisarUnidade_ProdutoServico_Click(object sender, EventArgs e)
        {
            try
            {
                Grupo_Unidade objetoPesquisado = new Grupo_Unidade();

                int result;
                if (txtb_CodigoUnidade_ProdutoServico.BackColor.Equals(System.Drawing.Color.RosyBrown) || txtb_CodigoUnidade_ProdutoServico.Text.Trim().Equals("")
                    || ((int.TryParse(txtb_CodigoUnidade_ProdutoServico.Text, out result)) && result <= 0))
                {
                    objetoPesquisado.codigo = 0;
                }
                else
                    objetoPesquisado.codigo = int.Parse(txtb_CodigoUnidade_ProdutoServico.Text);

                using (FormGrupo_Unidade formUnidade = new FormGrupo_Unidade(objetoPesquisado, true, "UNIDADE", 'P'))
                {
                    formUnidade.ShowDialog();
                    gerenciarInformacaoRetornadoPesquisa = formUnidade.gerenciarInformacaoRetornadoPesquisa;

                    if (!gerenciarInformacaoRetornadoPesquisa.codigoRetornavelPesquisa.Equals(0))
                    {
                        txtb_CodigoUnidade_ProdutoServico.Text = gerenciarInformacaoRetornadoPesquisa.codigoRetornavelPesquisa.ToString();
                        txtb_CodigoUnidade_ProdutoServico_Leave(sender, e);
                    }
                    else if (!txtb_CodigoUnidade_ProdutoServico.Text.Trim().Equals("") && (int.TryParse(txtb_CodigoUnidade_ProdutoServico.Text, out result)) && result > 0)
                        txtb_CodigoUnidade_ProdutoServico_Leave(sender, e);

                    txtb_Altura_ProdutoServico.Focus();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_PesquisarGrupo_ProdutoServico_Click(object sender, EventArgs e)
        {
            try
            {
                Grupo_Unidade objetoPesquisado = new Grupo_Unidade();

                int result;
                if (txtb_CodigoGrupo_ProdutoServico.BackColor.Equals(System.Drawing.Color.RosyBrown) || txtb_CodigoGrupo_ProdutoServico.Text.Trim().Equals("")
                    || ((int.TryParse(txtb_CodigoGrupo_ProdutoServico.Text, out result)) && result <= 0))
                {
                    objetoPesquisado.codigo = 0;
                }
                else
                    objetoPesquisado.codigo = int.Parse(txtb_CodigoGrupo_ProdutoServico.Text);

                using (FormGrupo_Unidade formGrupo = new FormGrupo_Unidade(objetoPesquisado, true, "GRUPO", 'P'))
                {
                    formGrupo.ShowDialog();
                    gerenciarInformacaoRetornadoPesquisa = formGrupo.gerenciarInformacaoRetornadoPesquisa;

                    if (!gerenciarInformacaoRetornadoPesquisa.codigoRetornavelPesquisa.Equals(0))
                    {
                        txtb_CodigoGrupo_ProdutoServico.Text = gerenciarInformacaoRetornadoPesquisa.codigoRetornavelPesquisa.ToString();
                        txtb_CodigoGrupo_ProdutoServico_Leave(sender, e);
                    }
                    else if (!txtb_CodigoGrupo_ProdutoServico.Text.Trim().Equals("") && (int.TryParse(txtb_CodigoGrupo_ProdutoServico.Text, out result)) && result > 0)
                        txtb_CodigoGrupo_ProdutoServico_Leave(sender, e);

                    txtb_MaoObra_ProdutoServico.Focus();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Data Grid View

        private void dgv_ProdutoServico_KeyUp(object sender, KeyEventArgs e)
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

        private void dgv_ProdutoServico_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                var linhaClicada = dgv_ProdutoServico.HitTest(e.X, e.Y);
                if (linhaClicada != null && linhaClicada.RowIndex >= 0)
                {
                    if (e.Button.Equals(MouseButtons.Right))
                    {
                        Point localizacaoInicial = new Point(e.Location.X, e.Location.Y);

                        if (linhaClicada.RowIndex.Equals(dgv_ProdutoServico.CurrentRow.Index))
                        {
                            if (pb_ExibindoImagemDGV.Visible.Equals(true))
                            {
                                pb_ExibindoImagemDGV.Visible = false;
                                btn_FecharImagemDGV.Visible = false;
                            }
                            else
                            {
                                ImagemDGV_RightClick(pb_ExibindoImagemDGV, btn_FecharImagemDGV, pb_ImagemProdutoServico, localizacaoInicial);
                                pb_ExibindoImagemDGV.Visible = true;
                                btn_FecharImagemDGV.Visible = true;
                                btn_FecharImagemDGV.BringToFront();
                            }
                        }
                        else
                        {
                            dgv_ProdutoServico.CurrentCell = dgv_ProdutoServico.Rows[linhaClicada.RowIndex].Cells[0];

                            ImagemDGV_RightClick(pb_ExibindoImagemDGV, btn_FecharImagemDGV, pb_ImagemProdutoServico, localizacaoInicial);
                            pb_ExibindoImagemDGV.Visible = true;
                            btn_FecharImagemDGV.Visible = true;
                            btn_FecharImagemDGV.BringToFront();
                        }
                    }
                    
                    PreencherCampos();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void dgv_ProdutoServico_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                var linhaClicada = dgv_ProdutoServico.HitTest(e.X, e.Y);
                if (linhaClicada != null && linhaClicada.RowIndex >= 0)
                {
                    if (e.Button.Equals(MouseButtons.Left))
                    {
                        btn_FecharImagemDGV_Click(sender, e);
                        tbc_Principal.SelectedIndex = 1;
                    }
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_FecharImagemDGV_Click(object sender, EventArgs e)
        {
            try
            {
                if (pb_ExibindoImagemDGV.Visible.Equals(true))
                {
                    pb_ExibindoImagemDGV.Visible = false;
                    btn_FecharImagemDGV.Visible = false;
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void dgv_ProdutoServico_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                if (checkLinhasAtivas.Unidade.ativo_inativo.Equals(false)
                    || checkLinhasAtivas.Grupo.ativo_inativo.Equals(false) || !checkLinhasAtivas.Grupo.material_ou_produto.Equals('P'))
                {
                    dgv_ProdutoServico.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.RosyBrown;
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos MouseHovar

        private void lb_Obrigatorio_PS_1_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_PS_1, "texto");
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void lb_Obrigatorio_PS_2_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_PS_2, "numerico");
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void lb_Obrigatorio_PS_3_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_PS_3, "numerico");
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #endregion

        #region Materiais do Produto/Serviço

        #region Funções Gerais

        private void LimparCamposParaCadastro_Materiais_Produto_Servico()
        {
            try
            {
                object sender = new object();
                EventArgs e = new EventArgs();

                lb_Codigo_Materiais_Produto_Servico.Text = "0";
                txtb_Codigo_Material.Clear();
                mtxtb_UltimaAtualizacao_Material.Clear();
                txtb_Descricao_Material.Clear();
                PintarBackground_CampoValido_Invalido(txtb_Codigo_Material, true, true);
                PintarBackground_CampoValido_Invalido(txtb_Descricao_Material, true, true);
                
                txtb_CodigoFornecedor_Material.Clear();
                txtb_DescricaoFornecedor_Material.Clear();
                PintarBackground_CampoValido_Invalido(txtb_CodigoFornecedor_Material, true, true);
                PintarBackground_CampoValido_Invalido(txtb_DescricaoFornecedor_Material, true, true);

                txtb_CodigoUnidade_Material.Clear();
                txtb_SiglaUnidade_Material.Clear();
                txtb_DescricaoUnidade_Material.Clear();
                PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade_Material, true, true);
                PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade_Material, true, true);
                PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade_Material, true, true);

                txtb_Altura_Material.Clear();
                txtb_Altura_Material_Leave(sender, e);
                txtb_Largura_Material.Clear();
                txtb_Largura_Material_Leave(sender, e);
                txtb_Comprimento_Material.Clear();
                txtb_Comprimento_Material_Leave(sender, e);

                txtb_CodigoGrupo_Material.Clear();
                txtb_DescricaoGrupo_Material.Clear();
                PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo_Material, true, true);
                PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo_Material, true, true);

                txtb_ValorUnitario_Material.Clear();
                txtb_ValorUnitario_Material_Leave(sender, e);

                txtb_Quantidade_Material.Clear();
                txtb_Quantidade_Material.Text = "1,00";

                txtb_ValorTotal_Material.Clear();
                txtb_ValorTotal_Material_Leave(sender, e);

                txtb_TotalMateriais.Clear();
                txtb_TotalMateriais_Leave(sender, e);

                pb_ImagemMaterial.Load(imagemFundoPadrao);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void ObterListagemRegistrosDGV_Materiais_Produto_Servico()
        {
            try
            {
                Materiais_Produto_Servico item = new Materiais_Produto_Servico
                {
                    ativo_inativo = true
                };
                InstanciamentoRapida_Materiais_ProdutoServico(item);
                item.Produto_Servico = produto_Servico;

                listaMateriais_Produto_Servico = procMateriais_Produto_Servico.ConsultarRegistro(item, false);

                CarregarListaDGV_Materiais_Produto_Servico();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void AdicionarLinhasDGV_Materiais_Produto_Servico(Materiais_Produto_Servico item)
        {
            try
            {
                string quantidadeFormatado = Convert.ToString(item.quantidade);
                string valorUnitarioFormatado = Convert.ToString(item.Material.valor_unitario);
                string valorTotalFormatado = Convert.ToString(item.valor_total);

                checkLinhasAtivas_Materiais_Produto_Servico = item;

                dgv_Materiais_Produto_Servico.Rows.Add(item.codigo, item.Material.codigo, DateTime.Parse(item.Material.ultima_atualizacao.ToString()).ToString("dd/MM/yyyy HH:mm"),
                    item.Material.descricao, item.Material.Grupo.descricao, item.Material.Unidade.sigla, decimal.Parse(quantidadeFormatado).ToString("N2", new CultureInfo("pt-BR")),
                    decimal.Parse(valorUnitarioFormatado).ToString("C2", new CultureInfo("pt-BR")), decimal.Parse(valorTotalFormatado).ToString("C2", new CultureInfo("pt-BR")),
                    item.Material.Fornecedor.nome_razao_social);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void CarregarListaDGV_Materiais_Produto_Servico()
        {
            try
            {
                if (listaProduto_Servico.Count > 0)
                {
                    if (listaMateriais_Produto_Servico.Count > 0)
                    {
                        dgv_Materiais_Produto_Servico.Rows.Clear();
                        checkLinhasAtivas_Materiais_Produto_Servico = new Materiais_Produto_Servico();
                        InstanciamentoRapida_Materiais_ProdutoServico(checkLinhasAtivas_Materiais_Produto_Servico);
                        
                        object sender = new object();
                        EventArgs e = new EventArgs();

                        txtb_ValorMateriais_ProdutoServico_Enter(sender, e);
                        txtb_TotalMateriais_Enter(sender, e);
                        
                        valorTotalMateriais = 0;
                        foreach (Materiais_Produto_Servico item in listaMateriais_Produto_Servico)
                        {
                            if (item.Produto_Servico.codigo.Equals(produto_Servico.codigo))
                            {
                                AdicionarLinhasDGV_Materiais_Produto_Servico(item);
                                CalcularValorTotal_Materiais_Produto_Servico(item);
                            }
                        }

                        txtb_ValorMateriais_ProdutoServico.Text = valorTotalMateriais.ToString();
                        txtb_ValorMateriais_ProdutoServico_Leave(sender, e);
                        txtb_TotalMateriais.Text = valorTotalMateriais.ToString();
                        txtb_TotalMateriais_Leave(sender, e);

                        txtb_MaoObra_ProdutoServico_Leave(sender, e);

                        if (primeiraPesquisaModoPesquisa)
                        {
                            int resultado = 0;
                            primeiraPesquisaModoPesquisa = false;

                            Materiais_Produto_Servico converter = new Materiais_Produto_Servico();
                            InstanciamentoRapida_Materiais_ProdutoServico(converter);

                            foreach (Materiais_Produto_Servico item in listaMateriais_Produto_Servico.Where(mps => mps.codigo.Equals(materiais_Produto_Servico.codigo)))
                            {
                                resultado = (int)item.codigo;
                                break;
                            }

                            if (!resultado.Equals(0))
                            {
                                converter = listaMateriais_Produto_Servico.Where(mps => mps.codigo.Equals(materiais_Produto_Servico.codigo)).Last();

                                foreach (DataGridViewRow item in dgv_Materiais_Produto_Servico.Rows)
                                {
                                    if (item.Cells[colunaCodigoIndex].Value.Equals((int)converter.codigo))
                                        dgv_Materiais_Produto_Servico.CurrentCell = dgv_Materiais_Produto_Servico.Rows[item.Index].Cells[colunaCodigoIndex];
                                }
                            }
                            else
                            {
                                primeiraPesquisaModoPesquisa = true;
                                dgv_Materiais_Produto_Servico.Rows[0].Selected = true;
                            }
                        }
                        else
                            dgv_Materiais_Produto_Servico.Rows[0].Selected = true;

                        if (tbc_Secundario.TabPages.Contains(tbp_Secundario_Material_ProdutoServico))
                        {
                            PreencherCampos_Materiais_Produto_Servico();
                            GerenciarBotoes_ListagemSecundaria(true, false, btn_Materiais_Cadastrar, btn_Materiais_Alterar, btn_Materiais_Deletar, btn_Materiais_Imprimir);
                        }
                    }
                    else
                    {
                        primeiraPesquisaModoPesquisa = false;

                        dgv_Materiais_Produto_Servico.Rows.Clear();

                        GerenciarBotoes_ListagemSecundaria(false, true, btn_Materiais_Cadastrar, btn_Materiais_Alterar, btn_Materiais_Deletar, btn_Materiais_Imprimir);
                        LimparCamposParaCadastro_Materiais_Produto_Servico();
                    }
                }
                else
                {
                    primeiraPesquisaModoPesquisa = false;
                    dgv_Materiais_Produto_Servico.Rows.Clear();
                    
                    GerenciarBotoes_ListagemSecundaria(false, false, btn_Materiais_Cadastrar, btn_Materiais_Alterar, btn_Materiais_Deletar, btn_Materiais_Imprimir);
                    LimparCamposParaCadastro_Materiais_Produto_Servico();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void PreencherCampos_Materiais_Produto_Servico()
        {
            try
            {
                if (dgv_Materiais_Produto_Servico.RowCount > 0)
                {
                    int codigoLinhaSelecionada;

                    if (primeiraPesquisaModoPesquisa)
                    {
                        primeiraPesquisaModoPesquisa = false;

                        Materiais_Produto_Servico mps = listaMateriais_Produto_Servico.First();
                        codigoLinhaSelecionada = (int)mps.codigo;
                    }
                    else
                        codigoLinhaSelecionada = Convert.ToInt32(dgv_Materiais_Produto_Servico.Rows[dgv_Materiais_Produto_Servico.CurrentCell.RowIndex].Cells[colunaCodigoIndex].Value);

                    object sender = new object();
                    EventArgs e = new EventArgs();

                    foreach (Materiais_Produto_Servico item in listaMateriais_Produto_Servico.Where(mps => mps.codigo.Equals(codigoLinhaSelecionada)))
                    {
                        lb_Codigo_Materiais_Produto_Servico.Text = item.codigo.ToString();

                        PreencherCampos_Material(item.Material);

                        txtb_Quantidade_Material.Text = item.quantidade.ToString();
                        txtb_Quantidade_Material_Leave(sender, e);

                        txtb_ValorTotal_Material.Text = item.valor_total.ToString();
                        txtb_ValorTotal_Material_Leave(sender, e);
                        
                        materiais_Produto_Servico = item;
                        break;
                    }
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private bool ValidandoCompilandoDadosCampos_TbSecundario(Materiais_Produto_Servico validandoMaterialProdutoServico)
        {
            try
            {
                bool dadosValidos = true;
                int variavelSaidaCodigo = 0;
                decimal variavelSaidaCamposDecimais = 0;

                object sender = new object();
                EventArgs e = new EventArgs();

                validandoMaterialProdutoServico.ativo_inativo = true;

                if (dadosValidos)
                {
                    if (operacaoRegistro_Materiais_Produto_Servico.Equals("ALTERAÇÃO") || operacaoRegistro_Materiais_Produto_Servico.Equals("INATIVAÇÃO"))
                        validandoMaterialProdutoServico.codigo = int.Parse(lb_Codigo_Materiais_Produto_Servico.Text);
                    else
                        validandoMaterialProdutoServico.codigo = 0;
                }

                if (dadosValidos)
                {
                    if (int.TryParse(txtb_Codigo_Material.Text.Trim(), out variavelSaidaCodigo))
                    {
                        if (txtb_Codigo_Material.BackColor.Equals(System.Drawing.Color.RosyBrown))
                        {
                            gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("MATERIAL");
                            dadosValidos = false;
                        }
                        else
                        {
                            validandoMaterialProdutoServico.Material.codigo = variavelSaidaCodigo;
                            validandoMaterialProdutoServico.Material.ultima_atualizacao = DateTime.Parse(mtxtb_UltimaAtualizacao_Material.Text);
                            validandoMaterialProdutoServico.Material.descricao = txtb_Descricao_Material.Text;
                            validandoMaterialProdutoServico.Material.ativo_inativo = true;
                        }
                    }
                    else
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("MATERIAL");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    if (int.TryParse(txtb_CodigoFornecedor_Material.Text.Trim(), out variavelSaidaCodigo))
                    {
                        if (txtb_CodigoFornecedor_Material.BackColor.Equals(System.Drawing.Color.RosyBrown))
                        {
                            gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("FORNECEDOR DO MATERIAL");
                            dadosValidos = false;
                        }
                        else
                        {
                            validandoMaterialProdutoServico.Material.Fornecedor.codigo = variavelSaidaCodigo;
                            validandoMaterialProdutoServico.Material.Fornecedor.nome_razao_social = txtb_DescricaoFornecedor_Material.Text;
                            validandoMaterialProdutoServico.Material.Fornecedor.tipo_pessoa = "FORNECEDOR";
                        }
                    }
                    else
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("FORNECEDOR DO MATERIAL");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    if (int.TryParse(txtb_CodigoUnidade_Material.Text.Trim(), out variavelSaidaCodigo))
                    {
                        if (txtb_CodigoUnidade_Material.BackColor.Equals(System.Drawing.Color.RosyBrown))
                        {
                            gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("UNIDADE DO MATERIAL");
                            dadosValidos = false;
                        }
                        else
                        {
                            validandoMaterialProdutoServico.Material.Unidade.codigo = variavelSaidaCodigo;
                            validandoMaterialProdutoServico.Material.Unidade.sigla = txtb_SiglaUnidade_Material.Text;
                            validandoMaterialProdutoServico.Material.Unidade.descricao = txtb_DescricaoUnidade_Material.Text;
                        }
                    }
                    else
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("UNIDADE DO MATERIAL");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    variavelSaidaCamposDecimais = CampoMoeda_RemoverFormatacao(txtb_Altura_Material);
                    if (variavelSaidaCamposDecimais >= 0)
                    {
                        validandoMaterialProdutoServico.Material.altura = variavelSaidaCamposDecimais;
                        txtb_Altura_Material_Leave(sender, e);
                    }
                    else
                    {
                        txtb_Altura_Material_Leave(sender, e);
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("ALTURA DO MATERIAL");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    variavelSaidaCamposDecimais = CampoMoeda_RemoverFormatacao(txtb_Largura_Material);
                    if (variavelSaidaCamposDecimais >= 0)
                    {
                        validandoMaterialProdutoServico.Material.largura = variavelSaidaCamposDecimais;
                        txtb_Largura_Material_Leave(sender, e);
                    }
                    else
                    {
                        txtb_Largura_Material_Leave(sender, e);
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("LARGURA DO MATERIAL");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    variavelSaidaCamposDecimais = CampoMoeda_RemoverFormatacao(txtb_Comprimento_Material);
                    if (variavelSaidaCamposDecimais >= 0)
                    {
                        validandoMaterialProdutoServico.Material.comprimento = variavelSaidaCamposDecimais;
                        txtb_Comprimento_Material_Leave(sender, e);
                    }
                    else
                    {
                        txtb_Comprimento_Material_Leave(sender, e);
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("COMPRIMENTO DO MATERIAL");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    if (int.TryParse(txtb_CodigoGrupo_Material.Text.Trim(), out variavelSaidaCodigo))
                    {
                        if (txtb_CodigoGrupo_Material.BackColor.Equals(System.Drawing.Color.RosyBrown))
                        {
                            gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("GRUPO DO MATERIAL");
                            dadosValidos = false;
                        }
                        else
                        {
                            validandoMaterialProdutoServico.Material.Grupo.codigo = variavelSaidaCodigo;
                            validandoMaterialProdutoServico.Material.Grupo.descricao = txtb_DescricaoGrupo_Material.Text;
                            validandoMaterialProdutoServico.Material.Grupo.material_ou_produto = 'M';
                        }
                    }
                    else
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("GRUPO DO MATERIAL");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    variavelSaidaCamposDecimais = CampoMoeda_RemoverFormatacao(txtb_ValorUnitario_Material);
                    if (variavelSaidaCamposDecimais > 0)
                    {
                        validandoMaterialProdutoServico.Material.valor_unitario = variavelSaidaCamposDecimais;
                        txtb_ValorUnitario_Material_Leave(sender, e);
                    }
                    else
                    {
                        txtb_ValorUnitario_Material_Leave(sender, e);
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("VALOR UNITÁRIO DO MATERIAL");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    variavelSaidaCamposDecimais = CampoMoeda_RemoverFormatacao(txtb_Quantidade_Material);
                    if (variavelSaidaCamposDecimais > 0)
                    {
                        validandoMaterialProdutoServico.quantidade = variavelSaidaCamposDecimais;
                        txtb_Quantidade_Material_Leave(sender, e);
                    }
                    else
                    {
                        txtb_Quantidade_Material_Leave(sender, e);
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("QUANTIDADE DE MATERIAL");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    variavelSaidaCamposDecimais = CampoMoeda_RemoverFormatacao(txtb_ValorTotal_Material);
                    if (variavelSaidaCamposDecimais > 0)
                    {
                        validandoMaterialProdutoServico.valor_total = variavelSaidaCamposDecimais;
                        txtb_ValorTotal_Material_Leave(sender, e);
                    }
                    else
                    {
                        txtb_ValorTotal_Material_Leave(sender, e);
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("VALOR TOTAL DO MATERIAL");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    OpenFileDialog checkImagem = new OpenFileDialog();
                    checkImagem.FileName = pb_ImagemMaterial.ImageLocation;

                    if (checkImagem.CheckFileExists)
                        validandoMaterialProdutoServico.Material.imagem = checkImagem.FileName;
                    else
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("ARQUIVO DE IMAGEM DO MATERIAL");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    operacaoRegistro = "ALTERAÇÃO";
                    if (dadosValidos = ValidandoCompilandoDadosCampos_TbPrincipal(produto_Servico))
                        validandoMaterialProdutoServico.Produto_Servico = produto_Servico;

                    operacaoRegistro = "";
                }

                if (dadosValidos)
                    dadosValidos = ValidandoCadastroRegistroDuplicado_TbSecundario(validandoMaterialProdutoServico);

                return dadosValidos;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
                return false;
            }
        }

        private bool ValidandoCadastroRegistroDuplicado_TbSecundario(Materiais_Produto_Servico validandoMaterialProdutoServico)
        {
            try
            {
                bool dadosValidos = true;

                foreach (Materiais_Produto_Servico item in listaMateriais_Produto_Servico)
                {
                    if (operacaoRegistro_Materiais_Produto_Servico.Equals("CADASTRO"))
                    {
                        if (item.Material.codigo.Equals(validandoMaterialProdutoServico.Material.codigo) && item.ativo_inativo.Equals(true))
                        {
                            dadosValidos = false;
                            break;
                        }
                    }
                    else if (operacaoRegistro_Materiais_Produto_Servico.Equals("ALTERAÇÃO"))
                    {
                        if (item.Material.codigo.Equals(validandoMaterialProdutoServico.Material.codigo) && item.ativo_inativo.Equals(true))
                        {
                            if (! item.codigo.Equals(validandoMaterialProdutoServico.codigo))
                            {
                                dadosValidos = false;
                                break;
                            }
                        }
                    }
                }
                
                if(dadosValidos.Equals(false))
                    gerenciarMensagensPadraoSistema.RegistroDuplicado("Esse Material", "Código: " + validandoMaterialProdutoServico.Material.codigo + " - " + validandoMaterialProdutoServico.Material.descricao);
                
                return dadosValidos;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
                return false;
            }
        }

        private void ModificarTabelaExcel_Materiais_Produto_Servico(string patchTabela)
        {
            try
            {
                List<Materiais_Produto_Servico> listaImpressao = new ListaMateriais_Produto_Servico();

                listaImpressao = listaMateriais_Produto_Servico;

                conexaoExcel = new Excel.Application();
                arquivoExcel = conexaoExcel.Workbooks.Open(patchTabela);
                planilhaExcel = arquivoExcel.Worksheets[1] as Excel.Worksheet;

                planilhaExcel.Cells[1, 1].Value = "PRODUTO/SERVIÇO: " + produto_Servico.codigo.ToString();
                planilhaExcel.Cells[1, 2].Value = produto_Servico.ultima_atualizacao.ToString();
                planilhaExcel.Cells[1, 3].Value = "PRODUTO/SERVIÇO: " + produto_Servico.descricao.ToString();
                planilhaExcel.Cells[1, 4].Value = "(" + produto_Servico.Grupo.codigo.ToString() + ") " + produto_Servico.Grupo.descricao.ToString();
                planilhaExcel.Cells[1, 5].Value = "(" + produto_Servico.Unidade.codigo.ToString() + ") ( " + produto_Servico.Unidade.sigla.ToString() + " ) - " + produto_Servico.Unidade.descricao.ToString();

                int linha = 3;
                foreach (Materiais_Produto_Servico item in listaImpressao)
                {
                    if (item.Produto_Servico.codigo.Equals(produto_Servico.codigo))
                    {
                        int coluna = 1;

                        planilhaExcel.Cells[linha, coluna++].Value = item.Material.codigo.ToString();
                        planilhaExcel.Cells[linha, coluna++].Value = item.Material.ultima_atualizacao.ToString();
                        planilhaExcel.Cells[linha, coluna++].Value = item.Material.descricao.ToString();
                        planilhaExcel.Cells[linha, coluna++].Value = "(" + item.Material.Grupo.codigo.ToString() + ") " + item.Material.Grupo.descricao.ToString();
                        planilhaExcel.Cells[linha, coluna++].Value = "(" + item.Material.Unidade.codigo.ToString() + ") ( " + item.Material.Unidade.sigla.ToString() + " ) - " + item.Material.Unidade.descricao.ToString();
                        planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.quantidade.ToString()).ToString("N2", new CultureInfo("pt-BR"));
                        planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.Material.valor_unitario.ToString()).ToString("C2", new CultureInfo("pt-BR"));
                        planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.valor_total.ToString()).ToString("C2", new CultureInfo("pt-BR"));
                        planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.Material.altura.ToString()).ToString("N2", new CultureInfo("pt-BR"));
                        planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.Material.largura.ToString()).ToString("N2", new CultureInfo("pt-BR"));
                        planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.Material.comprimento.ToString()).ToString("N2", new CultureInfo("pt-BR"));
                        planilhaExcel.Cells[linha, coluna++].Value = "(" + item.Material.Fornecedor.codigo.ToString() + ") " + item.Material.Fornecedor.nome_razao_social.ToString();

                        linha++;
                    }
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

        private void AtivaInativaBotoes_TbSecundario(bool ativaInativa, bool registrosJaInseridos)
        {
            try
            {
                txtb_Codigo_Material.Enabled = ativaInativa;
                btn_PesquisarMaterial.Enabled = ativaInativa;
                txtb_Quantidade_Material.Enabled = ativaInativa;
                btn_Materiais_Confirmar.Enabled = ativaInativa;
                btn_Materiais_Cancelar.Enabled = ativaInativa;

                btn_Materiais_Cadastrar.Enabled = !ativaInativa;
                dgv_Materiais_Produto_Servico.Enabled = !ativaInativa;

                if (registrosJaInseridos)
                {
                    btn_Materiais_Alterar.Enabled = !ativaInativa;
                    btn_Materiais_Deletar.Enabled = !ativaInativa;
                    btn_Materiais_Imprimir.Enabled = !ativaInativa;
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void PreencherCampos_Material(Material item)
        {
            try
            {
                object sender = new object();
                EventArgs e = new EventArgs();

                txtb_Codigo_Material.Text = item.codigo.ToString();
                mtxtb_UltimaAtualizacao_Material.Text = DateTime.Parse(item.ultima_atualizacao.ToString()).ToString("dd/MM/yyyy HH:mm");
                txtb_Descricao_Material.Text = item.descricao;
                
                if (item.ativo_inativo.Equals(false))
                {
                    PintarBackground_CampoValido_Invalido(txtb_Codigo_Material, false, false);
                    PintarBackground_CampoValido_Invalido(txtb_Descricao_Material, false, false);
                }
                else
                {
                    PintarBackground_CampoValido_Invalido(txtb_Codigo_Material, true, false);
                    PintarBackground_CampoValido_Invalido(txtb_Descricao_Material, true, false);
                }

                txtb_CodigoFornecedor_Material.Text = item.Fornecedor.codigo.ToString();
                txtb_DescricaoFornecedor_Material.Text = item.Fornecedor.nome_razao_social;
                if (item.Fornecedor.ativo_inativo.Equals(false))
                {
                    PintarBackground_CampoValido_Invalido(txtb_CodigoFornecedor_Material, false, false);
                    PintarBackground_CampoValido_Invalido(txtb_DescricaoFornecedor_Material, false, false);
                }
                else
                {
                    PintarBackground_CampoValido_Invalido(txtb_CodigoFornecedor_Material, true, false);
                    PintarBackground_CampoValido_Invalido(txtb_DescricaoFornecedor_Material, true, false);
                }

                txtb_CodigoUnidade_Material.Text = item.Unidade.codigo.ToString();
                txtb_SiglaUnidade_Material.Text = item.Unidade.sigla;
                txtb_DescricaoUnidade_Material.Text = item.Unidade.descricao;
                if (item.Unidade.ativo_inativo.Equals(false))
                {
                    PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade_Material, false, false);
                    PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade_Material, false, false);
                    PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade_Material, false, false);
                }
                else
                {
                    PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade_Material, true, false);
                    PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade_Material, true, false);
                    PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade_Material, true, false);
                }

                txtb_Altura_Material.Text = item.altura.ToString();
                txtb_Altura_Material_Leave(sender, e);
                txtb_Largura_Material.Text = item.largura.ToString();
                txtb_Largura_Material_Leave(sender, e);
                txtb_Comprimento_Material.Text = item.comprimento.ToString();
                txtb_Comprimento_Material_Leave(sender, e);

                txtb_CodigoGrupo_Material.Text = item.Grupo.codigo.ToString();
                txtb_DescricaoGrupo_Material.Text = item.Grupo.descricao;
                if (item.Grupo.ativo_inativo.Equals(false) || !item.Grupo.material_ou_produto.Equals('M'))
                {
                    PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo_Material, false, false);
                    PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo_Material, false, false);
                }
                else
                {
                    PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo_Material, true, false);
                    PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo_Material, true, false);
                }

                txtb_ValorUnitario_Material.Text = item.valor_unitario.ToString();
                txtb_ValorUnitario_Material_Leave(sender, e);

                if (File.Exists(item.imagem))
                    pb_ImagemMaterial.Load(item.imagem);
                else
                {
                    pb_ImagemMaterial.Load(imagemFundoPadrao);
                    item.imagem = imagemFundoPadrao;
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void CalcularValorTotal_Material()
        {
            try
            {
                object sender = new object();
                EventArgs e = new EventArgs();

                txtb_ValorTotal_Material.Text = Convert.ToString(CampoMoeda_RemoverFormatacao(txtb_ValorUnitario_Material) * decimal.Parse(txtb_Quantidade_Material.Text));

                txtb_ValorUnitario_Material_Leave(sender, e);
                txtb_ValorTotal_Material_Leave(sender, e);
                CampoMoeda_FormatacaoDecimal(txtb_Quantidade_Material, false);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void CalcularValorTotal_Materiais_Produto_Servico(Materiais_Produto_Servico item)
        {
            try
            {
                valorTotalMateriais = valorTotalMateriais + (decimal)item.valor_total;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Enter

        private void txtb_TotalMateriais_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_TotalMateriais);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos KeyPress

        private void txtb_Codigo_Material_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_Quantidade_Material, e))
                    CampoMoeda_KeyPress(sender, e, true);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_Quantidade_Material_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusButton(btn_Materiais_Confirmar, e))
                    CampoMoeda_KeyPress(sender, e, false);
                else
                    btn_Materiais_Confirmar_Click(sender, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Leave

        private void txtb_Codigo_Material_Leave(object sender, EventArgs e)
        {
            try
            {
                int codigoRetorno = 0;
                if (int.TryParse(txtb_Codigo_Material.Text, out codigoRetorno) && codigoRetorno > 0)
                {
                    Material obj = new Material()
                    {
                        codigo = codigoRetorno,
                        ativo_inativo = true
                    };
                    InstanciamentoRapida_Material(obj);

                    ProcMaterial procMaterial = new ProcMaterial();

                    ListaMaterial listaMaterial = new ListaMaterial();
                    listaMaterial = procMaterial.ConsultarRegistro(obj, false);

                    if (listaMaterial.Count > 0)
                    {
                        foreach (Material item in listaMaterial)
                        {
                            if (item.codigo.Equals(codigoRetorno) && item.ativo_inativo.Equals(true))
                            {
                                PreencherCampos_Material(item);
                                CalcularValorTotal_Material();

                                PintarBackground_CampoValido_Invalido(txtb_Codigo_Material, true, false);
                                PintarBackground_CampoValido_Invalido(txtb_Descricao_Material, true, false);

                                break;
                            }
                            else
                            {
                                gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CÓDIGO MATERIAL");
                                PintarBackground_CampoValido_Invalido(txtb_Codigo_Material, false, false);
                                PintarBackground_CampoValido_Invalido(txtb_Descricao_Material, false, true);

                                break;
                            }
                        }
                    }
                    else
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CÓDIGO MATERIAL");
                        PintarBackground_CampoValido_Invalido(txtb_Codigo_Material, false, false);
                        PintarBackground_CampoValido_Invalido(txtb_Descricao_Material, false, true);
                    }
                }
                else
                {
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CÓDIGO MATERIAL");
                    PintarBackground_CampoValido_Invalido(txtb_Codigo_Material, false, false);
                    PintarBackground_CampoValido_Invalido(txtb_Descricao_Material, false, true);
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_CodigoFornecedor_Material_Leave(object sender, EventArgs e)
        {
            try
            {
                int codigoRetorno = 0;
                if (int.TryParse(txtb_CodigoFornecedor_Material.Text, out codigoRetorno) && codigoRetorno > 0)
                {
                    Pessoa obj = new Pessoa
                    {
                        codigo = codigoRetorno,
                        tipo_pessoa = "FORNECEDOR",
                        ativo_inativo = true
                    };

                    ProcPessoa procFornecedor = new ProcPessoa();

                    ListaPessoa listaFornecedor = new ListaPessoa();
                    listaFornecedor = procFornecedor.ConsultarRegistro(obj, false);

                    if (listaFornecedor.Count > 0)
                    {
                        foreach (Pessoa item in listaFornecedor)
                        {
                            if (item.codigo.Equals(codigoRetorno) && item.tipo_pessoa.Equals("FORNECEDOR") && item.ativo_inativo.Equals(true))
                            {
                                txtb_CodigoFornecedor_Material.Text = item.codigo.ToString();
                                txtb_DescricaoFornecedor_Material.Text = item.nome_razao_social;

                                PintarBackground_CampoValido_Invalido(txtb_CodigoFornecedor_Material, true, false);
                                PintarBackground_CampoValido_Invalido(txtb_DescricaoFornecedor_Material, true, false);

                                break;
                            }
                            else
                            {
                                PintarBackground_CampoValido_Invalido(txtb_CodigoFornecedor_Material, false, false);
                                PintarBackground_CampoValido_Invalido(txtb_DescricaoFornecedor_Material, false, true);

                                break;
                            }
                        }
                    }
                    else
                    {
                        PintarBackground_CampoValido_Invalido(txtb_CodigoFornecedor_Material, false, false);
                        PintarBackground_CampoValido_Invalido(txtb_DescricaoFornecedor_Material, false, true);
                    }
                }
                else
                {
                    PintarBackground_CampoValido_Invalido(txtb_CodigoFornecedor_Material, false, false);
                    PintarBackground_CampoValido_Invalido(txtb_DescricaoFornecedor_Material, false, true);
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_CodigoUnidade_Material_Leave(object sender, EventArgs e)
        {
            try
            {
                int codigoRetorno = 0;
                if (int.TryParse(txtb_CodigoUnidade_Material.Text, out codigoRetorno) && codigoRetorno > 0)
                {
                    Grupo_Unidade obj = new Grupo_Unidade()
                    {
                        codigo = codigoRetorno,
                        ativo_inativo = true
                    };

                    ProcUnidade procUnidade = new ProcUnidade();

                    ListaGrupo_Unidade listaUnidade = new ListaGrupo_Unidade();
                    listaUnidade = procUnidade.ConsultarRegistro(obj, false);

                    if (listaUnidade.Count > 0)
                    {
                        foreach (Grupo_Unidade item in listaUnidade)
                        {
                            if (item.codigo.Equals(codigoRetorno) && item.ativo_inativo.Equals(true))
                            {
                                txtb_CodigoUnidade_Material.Text = item.codigo.ToString();
                                txtb_SiglaUnidade_Material.Text = item.sigla;
                                txtb_DescricaoUnidade_Material.Text = item.descricao;

                                PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade_Material, true, false);
                                PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade_Material, true, false);
                                PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade_Material, true, false);

                                break;
                            }
                            else
                            {
                                PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade_Material, false, false);
                                PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade_Material, false, true);
                                PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade_Material, false, true);

                                break;
                            }
                        }
                    }
                    else
                    {
                        PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade_Material, false, false);
                        PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade_Material, false, true);
                        PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade_Material, false, true);
                    }
                }
                else
                {
                    PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade_Material, false, false);
                    PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade_Material, false, true);
                    PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade_Material, false, true);
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_Altura_Material_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_Altura_Material, false);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_Largura_Material_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_Largura_Material, false);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_Comprimento_Material_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_Comprimento_Material, false);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_CodigoGrupo_Material_Leave(object sender, EventArgs e)
        {
            try
            {
                int codigoRetorno = 0;
                if (int.TryParse(txtb_CodigoGrupo_Material.Text, out codigoRetorno) && codigoRetorno > 0)
                {
                    Grupo_Unidade obj = new Grupo_Unidade()
                    {
                        codigo = codigoRetorno,
                        material_ou_produto = 'M',
                        ativo_inativo = true
                    };

                    ProcGrupo procGrupo = new ProcGrupo();

                    ListaGrupo_Unidade listaGrupo = new ListaGrupo_Unidade();
                    listaGrupo = procGrupo.ConsultarRegistro(obj, false);

                    if (listaGrupo.Count > 0)
                    {
                        foreach (Grupo_Unidade item in listaGrupo)
                        {
                            if (item.codigo.Equals(codigoRetorno) && item.material_ou_produto.Equals('M') && item.ativo_inativo.Equals(true))
                            {
                                txtb_CodigoGrupo_Material.Text = item.codigo.ToString();
                                txtb_DescricaoGrupo_Material.Text = item.descricao;

                                PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo_Material, true, false);
                                PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo_Material, true, false);

                                break;
                            }
                            else
                            {
                                PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo_Material, false, false);
                                PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo_Material, false, true);

                                break;
                            }
                        }
                    }
                    else
                    {
                        PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo_Material, false, false);
                        PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo_Material, false, true);
                    }
                }
                else
                {
                    PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo_Material, false, false);
                    PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo_Material, false, true);
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ValorUnitario_Material_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorUnitario_Material, true);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_Quantidade_Material_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal variavelQuantidadeParcelas = 0;
                if (!decimal.TryParse(txtb_Quantidade_Material.Text, out variavelQuantidadeParcelas))
                    variavelQuantidadeParcelas = 1;

                if (variavelQuantidadeParcelas <= 0)
                {
                    txtb_Quantidade_Material.Text = "1,00";
                    variavelQuantidadeParcelas = 1;
                }
                else
                    txtb_Quantidade_Material.Text = variavelQuantidadeParcelas.ToString();

                CalcularValorTotal_Material();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ValorTotal_Material_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorTotal_Material, true);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_TotalMateriais_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_TotalMateriais, true);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Button Click

        private void btn_Materiais_Cadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (pb_ImagemProdutoServico.Visible.Equals(true))
                {
                    pb_ImagemProdutoServico.Visible = false;
                    btn_FecharImagem_Material.Visible = false;
                }

                Formulario_ModoModificacao(gb_ListagemGeral, gb_DadosGerais, tbc_Principal, tbc_Secundario, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar,
                    btn_DadosGerais_Deletar, btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_DadosGerais);

                AtivaInativaBotoes_TbSecundario(true, true);

                operacaoRegistro_Materiais_Produto_Servico = "CADASTRO";
                LimparCamposParaCadastro_Materiais_Produto_Servico();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_Materiais_Alterar_Click(object sender, EventArgs e)
        {
            try
            {
                if (pb_ImagemMaterial.Visible.Equals(true))
                {
                    pb_ImagemMaterial.Visible = false;
                    btn_FecharImagem_Material.Visible = false;
                }

                Formulario_ModoModificacao(gb_ListagemGeral, gb_DadosGerais, tbc_Principal, tbc_Secundario, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar,
                    btn_DadosGerais_Deletar, btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_DadosGerais);

                AtivaInativaBotoes_TbSecundario(true, true);

                operacaoRegistro_Materiais_Produto_Servico = "ALTERAÇÃO";
                txtb_Codigo_Material.Focus();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_Materiais_Deletar_Click(object sender, EventArgs e)
        {
            try
            {
                operacaoRegistro_Materiais_Produto_Servico = "INATIVAÇÃO";
                btn_Materiais_Confirmar_Click(sender, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_Materiais_Imprimir_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog diretorioDestinoArquivoImpressao = new FolderBrowserDialog();

                if (diretorioDestinoArquivoImpressao.ShowDialog().Equals(DialogResult.OK) && !string.IsNullOrWhiteSpace(diretorioDestinoArquivoImpressao.SelectedPath))
                {
                    if (pb_ImagemMaterial.Visible.Equals(true))
                    {
                        pb_ImagemMaterial.Visible = false;
                        btn_FecharImagem_Material.Visible = false;
                    }

                    telaLoadingFoiAberta = true;
                    gerenciarTelaLoading.Abrir(this, formTelaLoading, carregarThread);

                    string nomeArquivo = Path.GetFileName(arquivoListaParaImpressao_Materiais_Produto_Servico);

                    string nomeArquivoModificado = nomeArquivo.Replace(".xlsx", "");
                    nomeArquivoModificado = nomeArquivoModificado + " " + DateTime.Now.ToString("dd-MM-yyyy") + ".xlsx";

                    string arquivoListaModificado = Path.Combine(diretorioDestinoArquivoImpressao.SelectedPath, nomeArquivoModificado);

                    File.Copy(arquivoListaParaImpressao_Materiais_Produto_Servico, arquivoListaModificado, true);

                    ModificarTabelaExcel_Materiais_Produto_Servico(arquivoListaModificado);

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

        private void btn_Materiais_Confirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (gerenciarMensagensPadraoSistema.Mensagem_Confirmacao(operacaoRegistro_Materiais_Produto_Servico).Equals(DialogResult.OK))
                {
                    Materiais_Produto_Servico objetoChecagem = new Materiais_Produto_Servico();
                    InstanciamentoRapida_Materiais_ProdutoServico(objetoChecagem);

                    string statusOperacao = "EM ANDAMENTO";

                    if (ValidandoCompilandoDadosCampos_TbSecundario(objetoChecagem))
                    {
                        if (operacaoRegistro_Materiais_Produto_Servico.Equals("CADASTRO") || operacaoRegistro_Materiais_Produto_Servico.Equals("ALTERAÇÃO")
                            || operacaoRegistro_Materiais_Produto_Servico.Equals("INATIVAÇÃO"))
                        {
                            statusOperacao = procMateriais_Produto_Servico.ManterRegistro(objetoChecagem, operacaoRegistro_Materiais_Produto_Servico);
                        }
                        else
                        {
                            gerenciarMensagensPadraoSistema.ExceptionBancoDados("A operação informada não existe !");
                            return;
                        }

                        int codigoRegistroRetornado = 0;
                        if (int.TryParse(statusOperacao, out codigoRegistroRetornado))
                        {
                            materiais_Produto_Servico = objetoChecagem;
                            materiais_Produto_Servico.codigo = codigoRegistroRetornado;
                            
                            ObterListagemRegistrosDGV_Materiais_Produto_Servico();
                            
                            Formulario_ModoConsulta(gb_ListagemGeral, gb_DadosGerais, dgv_ProdutoServico, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar, btn_DadosGerais_Deletar,
                                btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                            if (! tbc_Secundario.TabPages.Contains(tbp_Secundario_DadosGerais))
                                Formulario_AtivarAbaSecundaria(tbc_Secundario, tbp_Secundario_DadosGerais, 0);

                            AtivaInativaBotoes_TbSecundario(false, true);

                            operacaoRegistro_Materiais_Produto_Servico = "";
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

        private void btn_Materiais_Cancelar_Click(object sender, EventArgs e)
        {
            try
            {
                if (gerenciarMensagensPadraoSistema.Mensagem_Cancelamento().Equals(DialogResult.OK))
                {
                    Formulario_ModoConsulta(gb_ListagemGeral, gb_DadosGerais, dgv_ProdutoServico, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar, btn_DadosGerais_Deletar,
                        btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                    if (!tbc_Secundario.TabPages.Contains(tbp_Secundario_DadosGerais))
                        Formulario_AtivarAbaSecundaria(tbc_Secundario, tbp_Secundario_DadosGerais, 0);

                    bool registrosJaInseridos;
                    if (listaMateriais_Produto_Servico.Count > 0)
                        registrosJaInseridos = true;
                    else
                        registrosJaInseridos = false;

                    ObterListagemRegistrosDGV_Materiais_Produto_Servico();

                    AtivaInativaBotoes_TbSecundario(false, registrosJaInseridos);

                    operacaoRegistro_Materiais_Produto_Servico = "";
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Button Pesquisa

        private void btn_PesquisarMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                Material objetoPesquisado = new Material();

                int result;
                if (txtb_Codigo_Material.BackColor.Equals(System.Drawing.Color.RosyBrown) || txtb_Codigo_Material.Text.Trim().Equals("")
                    || ((int.TryParse(txtb_Codigo_Material.Text, out result)) && result <= 0))
                {
                    objetoPesquisado.codigo = 0;
                }
                else
                    objetoPesquisado.codigo = int.Parse(txtb_Codigo_Material.Text);

                using (FormMaterial formMaterial = new FormMaterial(objetoPesquisado, true))
                {
                    formMaterial.ShowDialog();
                    gerenciarInformacaoRetornadoPesquisa = formMaterial.gerenciarInformacaoRetornadoPesquisa;

                    if (!gerenciarInformacaoRetornadoPesquisa.codigoRetornavelPesquisa.Equals(0))
                    {
                        txtb_Codigo_Material.Text = gerenciarInformacaoRetornadoPesquisa.codigoRetornavelPesquisa.ToString();
                        txtb_Codigo_Material_Leave(sender, e);
                    }
                    else if (!txtb_Codigo_Material.Text.Trim().Equals("") && (int.TryParse(txtb_Codigo_Material.Text, out result)) && result > 0)
                        txtb_Codigo_Material_Leave(sender, e);

                    txtb_Quantidade_Material.Focus();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Data Grid View

        private void dgv_Materiais_Produto_Servico_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue.Equals(((int)Keys.Up)) || e.KeyValue.Equals(((int)Keys.Down)))
                    PreencherCampos_Materiais_Produto_Servico();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void dgv_Materiais_Produto_Servico_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                var linhaClicada = dgv_Materiais_Produto_Servico.HitTest(e.X, e.Y);
                if (linhaClicada != null && linhaClicada.RowIndex >= 0)
                {
                    if (e.Button.Equals(MouseButtons.Right))
                    {
                        Point localizacaoInicial = new Point(txtb_Codigo_Material.Location.X, txtb_Codigo_Material.Location.Y + txtb_Codigo_Material.Size.Height);

                        if (linhaClicada.RowIndex.Equals(dgv_Materiais_Produto_Servico.CurrentRow.Index))
                        {
                            if (pb_ImagemMaterial.Visible.Equals(true))
                            {
                                pb_ImagemMaterial.Visible = false;
                                btn_FecharImagem_Material.Visible = false;
                            }
                            else
                            {
                                ImagemDGV_RightClick(pb_ImagemMaterial, btn_FecharImagem_Material, pb_ImagemProdutoServico, localizacaoInicial);
                                pb_ImagemMaterial.Visible = true;
                                btn_FecharImagem_Material.Visible = true;
                                btn_FecharImagem_Material.BringToFront();
                            }
                        }
                        else
                        {
                            dgv_Materiais_Produto_Servico.CurrentCell = dgv_Materiais_Produto_Servico.Rows[linhaClicada.RowIndex].Cells[0];

                            ImagemDGV_RightClick(pb_ImagemMaterial, btn_FecharImagem_Material, pb_ImagemProdutoServico, localizacaoInicial);
                            pb_ImagemMaterial.Visible = true;
                            btn_FecharImagem_Material.Visible = true;
                            btn_FecharImagem_Material.BringToFront();
                        }
                    }
                    
                    PreencherCampos_Materiais_Produto_Servico();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_FecharImagem_Material_Click(object sender, EventArgs e)
        {
            try
            {
                if (pb_ImagemMaterial.Visible.Equals(true))
                {
                    pb_ImagemMaterial.Visible = false;
                    btn_FecharImagem_Material.Visible = false;
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void dgv_Materiais_Produto_Servico_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                if (checkLinhasAtivas_Materiais_Produto_Servico.Material.ativo_inativo.Equals(false)
                    || checkLinhasAtivas_Materiais_Produto_Servico.Material.Unidade.ativo_inativo.Equals(false)
                    || checkLinhasAtivas_Materiais_Produto_Servico.Material.Grupo.ativo_inativo.Equals(false) || !checkLinhasAtivas_Materiais_Produto_Servico.Material.Grupo.material_ou_produto.Equals('M')
                    || checkLinhasAtivas_Materiais_Produto_Servico.Material.Fornecedor.ativo_inativo.Equals(false) || !checkLinhasAtivas_Materiais_Produto_Servico.Material.Fornecedor.tipo_pessoa.Equals("FORNECEDOR"))
                {
                    dgv_Materiais_Produto_Servico.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.RosyBrown;
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos MouseHovar

        private void lb_Obrigatorio_MT_1_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_MT_1, "numerico");
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void lb_Obrigatorio_MT_2_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_MT_2, "numerico");
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #endregion

    }
}