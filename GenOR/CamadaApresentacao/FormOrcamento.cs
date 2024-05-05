using CamadaObjetoTransferencia;
using CamadaProcessamento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;
using GenOR.Properties;

namespace GenOR
{
    public partial class FormOrcamento : FormBase
    {
        private Pessoa usuario;

        #region Variaveis Orçamento

        public GerenciarInformacaoRetornadoPesquisa gerenciarInformacaoRetornadoPesquisa;

        private Orcamento orcamento;
        private ListaOrcamento listaOrcamento;
        private ProcOrcamento procOrcamento;

        private List<Orcamento> listaPesquisada;

        private GerenciarMensagensPadraoSistema gerenciarMensagensPadraoSistema;

        private GerenciarTelaLoading gerenciarTelaLoading;
        private FormTelaLoading formTelaLoading;
        private Thread carregarThread;
        private bool telaLoadingFoiAberta;

        private string operacaoRegistro;
        private bool encerramentoPadrao;

        private bool primeiraPesquisaModoPesquisa;

        private Orcamento checkLinhasAtivas;
        private static int colunaCodigoIndex = 0;

        private string imagemFundoPadrao;
        private PictureBox pb_FundoBase;

        private string imagemButtomPesquisa;
        private FormTelaPesquisaOrcamento formTelaPesquisaOrcamento;

        private string arquivoListaParaImpressao;
        private Excel.Application conexaoExcel;
        private Excel.Workbook arquivoExcel;
        private Excel.Worksheet planilhaExcel;

        private FormTelaConfiguracoesDocOrcamento formTelaConfiguracoesDocOrcamento;

        #endregion

        #region Variaveis Produtos/Serviços do Orçamento

        private Produtos_Servicos_Orcamento produtos_Servicos_Orcamento;
        private ListaProdutos_Servicos_Orcamento listaProdutos_Servicos_Orcamento;
        private ProcProdutos_Servicos_Orcamento procProdutos_Servicos_Orcamento;

        private string operacaoRegistro_Produtos_Servicos_Orcamento;

        private Produtos_Servicos_Orcamento checkLinhasAtivas_Produtos_Servicos_Orcamento;

        private string arquivoListaParaImpressao_Produtos_Servicos_Orcamento;

        private decimal valorTotalProdutos_Servicos_Orcamento;

        #endregion

        #region Variaveis Materiais dos Produtos/Serviços do Orçamento

        private Materiais_Orcamento materiais_Orcamento;
        private ListaMateriais_Orcamento listaMateriais_Orcamento;
        private ProcMateriais_Orcamento procMateriais_Orcamento;

        private Materiais_Orcamento checkLinhasAtivas_Materiais_Orcamento;

        private string arquivoListaParaImpressao_Materiais_Orcamento;

        private decimal valorTotalMateriais_Orcamento;

        #endregion

        public FormOrcamento(Pessoa usuario)
        {
            try
            {
                InitializeComponent();

                this.usuario = usuario;

                #region Orçamento

                gerenciarInformacaoRetornadoPesquisa = new GerenciarInformacaoRetornadoPesquisa();
                gerenciarInformacaoRetornadoPesquisa.codigoRetornavelPesquisa = 0;

                orcamento = new Orcamento();
                InstanciamentoRapida_Orcamento(orcamento);
                listaOrcamento = new ListaOrcamento();
                procOrcamento = new ProcOrcamento();

                listaPesquisada = new ListaOrcamento();

                gerenciarMensagensPadraoSistema = new GerenciarMensagensPadraoSistema();

                gerenciarTelaLoading = new GerenciarTelaLoading();
                formTelaLoading = new FormTelaLoading();
                carregarThread = null;
                telaLoadingFoiAberta = false;

                operacaoRegistro = "";
                encerramentoPadrao = true;

                primeiraPesquisaModoPesquisa = false;

                imagemFundoPadrao = Localizar_Imagem_Documento("Picture.png", true);

                pb_FundoBase = new PictureBox();
                pb_FundoBase.Size = new Size(300, 250);
                pb_FundoBase.SizeMode = PictureBoxSizeMode.StretchImage;
                pb_FundoBase.Load(imagemFundoPadrao);
                pb_FundoBase.Visible = false;

                imagemButtomPesquisa = "PESQUISAR";
                formTelaPesquisaOrcamento = new FormTelaPesquisaOrcamento();

                arquivoListaParaImpressao = Localizar_Imagem_Documento("LISTAGEM ORCAMENTO.xlsx", false);
                conexaoExcel = null;
                arquivoExcel = null;
                planilhaExcel = null;

                dgv_Orcamento.AutoGenerateColumns = false;

                #endregion

                #region Produtos/Serviços do Orçamento

                produtos_Servicos_Orcamento = new Produtos_Servicos_Orcamento();
                InstanciamentoRapida_ProdutosServicos_Orcamento(produtos_Servicos_Orcamento);

                listaProdutos_Servicos_Orcamento = new ListaProdutos_Servicos_Orcamento();
                procProdutos_Servicos_Orcamento = new ProcProdutos_Servicos_Orcamento();

                operacaoRegistro_Produtos_Servicos_Orcamento = "";

                pb_ExibindoImagemDGV_ProdutoServico_Orcamento.Load(imagemFundoPadrao);

                arquivoListaParaImpressao_Produtos_Servicos_Orcamento = Localizar_Imagem_Documento("LISTAGEM PRODUTOS SERVIÇOS DO ORÇAMENTO.xlsx", false);

                dgv_ProdutosServicosOrcamento.AutoGenerateColumns = false;

                valorTotalProdutos_Servicos_Orcamento = 0;

                #endregion

                #region Materiais dos Produtos/Serviços do Orçamento

                materiais_Orcamento = new Materiais_Orcamento();
                InstanciamentoRapida_Materiais_Orcamento(materiais_Orcamento);

                listaMateriais_Orcamento = new ListaMateriais_Orcamento();
                procMateriais_Orcamento = new ProcMateriais_Orcamento();

                pb_ExibindoImagemDGV_Material_Orcamento.Load(imagemFundoPadrao);

                arquivoListaParaImpressao_Materiais_Orcamento = Localizar_Imagem_Documento("LISTAGEM MATERIAIS DO ORÇAMENTO.xlsx", false);

                dgv_MateriaisOrcamento.AutoGenerateColumns = false;

                valorTotalMateriais_Orcamento = 0;

                #endregion

            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
                this.Close();
            }
        }

        #region Eventos Form

        private void FormOrcamento_Load(object sender, EventArgs e)
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

        private void FormOrcamento_FormClosing(object sender, FormClosingEventArgs e)
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
                        else if (!operacaoRegistro_Produtos_Servicos_Orcamento.Equals(""))
                            condicaoExtra = "durante o processo de " + operacaoRegistro_Produtos_Servicos_Orcamento + " do Produto/Serviço do Orçamento";
                    }
                    else
                        condicaoExtra = "sem Retornar nenhum registro pesquisado";

                    if (gerenciarMensagensPadraoSistema.Mensagem_FechamentoJanela(condicaoExtra).Equals(DialogResult.Cancel))
                        e.Cancel = true;
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Orçamento

        #region Funções Gerais

        private void LimparCamposParaCadastro()
        {
            try
            {
                object sender = new object();
                EventArgs e = new EventArgs();

                txtb_Codigo_Orcamento.Clear();
                mtxtb_UltimaAtualizacao_Orcamento.Clear();
                mtxtb_DataValidade_Orcamento.Clear();

                PintarBackground_CampoValido_Invalido(txtb_CodigoCliente, true, true);
                PintarBackground_CampoValido_Invalido(txtb_DescricaoCliente, true, true);

                txtb_PrazoEntrega_Orcamento.Clear();
                txtb_DescricaoPagamento_Orcamento.Clear();
                txtb_Observacao_Orcamento.Clear();
                
                txtb_ValorEntrada_Orcamento.Text = "0,00";

                txtb_QuantidadeParcelas_Orcamento.Text = "1";
                txtb_ValorParcela_Orcamento.Clear();
                txtb_ValorParcela_Orcamento_Leave(sender, e);

                txtb_Juros_Orcamento.Text = "0,00";
                txtb_ValorJuros_Orcamento.Text = "0,00";

                txtb_ValorProdutos_Orcamento.Clear();
                txtb_ValorProdutos_Orcamento_Leave(sender, e);

                txtb_Desconto_Orcamento.Clear();
                txtb_Desconto_Orcamento_Leave(sender, e);

                txtb_ValorTotal_Orcamento.Clear();
                txtb_ValorTotal_Orcamento_Leave(sender, e);

                lb_Usuario_Orcamento.Text = "(  ) - ";
                lb_CriadoModificadoUsuario_Orcamento.BackColor = Color.White;
                lb_Usuario_Orcamento.BackColor = Color.White;
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
                Orcamento item = new Orcamento
                {
                    ativo_inativo = true
                };
                InstanciamentoRapida_Orcamento(item);

                listaOrcamento = procOrcamento.ConsultarRegistro(item, true);

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
                if (listaOrcamento.Count > 0)
                {
                    dgv_Orcamento.Rows.Clear();
                    checkLinhasAtivas = new Orcamento();
                    InstanciamentoRapida_Orcamento(checkLinhasAtivas);

                    foreach (Orcamento item in listaOrcamento)
                    {
                        AdicionarLinhasDGV(item);
                    }

                    if (primeiraPesquisaModoPesquisa)
                    {
                        int resultado = 0;
                        primeiraPesquisaModoPesquisa = false;

                        Orcamento converter = new Orcamento();
                        InstanciamentoRapida_Orcamento(converter);

                        foreach (Orcamento item in listaOrcamento.Where(o => o.codigo.Equals(orcamento.codigo)))
                        {
                            resultado = (int)item.codigo;
                            break;
                        }

                        if (!resultado.Equals(0))
                        {
                            converter = listaOrcamento.Where(o => o.codigo.Equals(orcamento.codigo)).Last();

                            foreach (DataGridViewRow item in dgv_Orcamento.Rows)
                            {
                                if (item.Cells[colunaCodigoIndex].Value.Equals((int)converter.codigo))
                                    dgv_Orcamento.CurrentCell = dgv_Orcamento.Rows[item.Index].Cells[colunaCodigoIndex];
                            }
                        }
                        else
                            dgv_Orcamento.Rows[0].Selected = true;
                    }
                    else
                    {
                        primeiraPesquisaModoPesquisa = false;
                        dgv_Orcamento.Rows[0].Selected = true;
                    }

                    if (!tbc_Secundario.TabPages.Contains(tbp_Secundario_ProdutosServicos_Orcamento))
                        Formulario_AtivarAbaSecundaria(tbc_Secundario, tbp_Secundario_ProdutosServicos_Orcamento, 1);

                    if (!tbc_Secundario.TabPages.Contains(tbp_Secundario_Materiais_Orcamento))
                        Formulario_AtivarAbaSecundaria(tbc_Secundario, tbp_Secundario_Materiais_Orcamento, 2);

                    PreencherCampos();
                }
                else
                {
                    primeiraPesquisaModoPesquisa = false;

                    dgv_Orcamento.Rows.Clear();
                    dgv_ProdutosServicosOrcamento.Rows.Clear();
                    dgv_MateriaisOrcamento.Rows.Clear();

                    GerenciarBotoes_ListagemGeral(false, btn_DadosGerais_Alterar, btn_DadosGerais_Deletar, btn_DadosGerais_Imprimir, btn_DadosGerais_AvancarGrid,
                        btn_DadosGerais_Pesquisar, btn_DadosGerais_VoltarGrid);

                    LimparCamposParaCadastro();
                    LimparCamposParaCadastro_Produtos_Servicos_Orcamento();
                    LimparCamposParaCadastro_Materiais_Orcamento();

                    Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_ProdutosServicos_Orcamento);
                    Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_Materiais_Orcamento);
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void AdicionarLinhasDGV(Orcamento item)
        {
            try
            {
                string valorEntradaFormatado = Convert.ToString(item.valor_entrada);
                string valorParcelaFormatado = Convert.ToString(item.valor_parcela);
                string jurosFormatado = Convert.ToString(item.juros);
                string valorJurosFormatado = Convert.ToString(item.valor_juros);
                string valorProdutosFormatado = Convert.ToString(item.total_produtos_servicos);
                string descontoFormatado = Convert.ToString(item.desconto);
                string valorTotalFormatado = Convert.ToString(item.total_orcamento);

                checkLinhasAtivas = item;

                dgv_Orcamento.Rows.Add(item.codigo, DateTime.Parse(item.ultima_atualizacao.ToString()).ToString("dd/MM/yyyy HH:mm"), DateTime.Parse(item.validade.ToString()).ToString("dd/MM/yyyy"),
                    item.Cliente.nome_razao_social, decimal.Parse(valorEntradaFormatado).ToString("C2", new CultureInfo("pt-BR")), item.quantidade_parcelas.ToString(),
                    decimal.Parse(valorParcelaFormatado).ToString("C2", new CultureInfo("pt-BR")), decimal.Parse(jurosFormatado).ToString("N2", new CultureInfo("pt-BR")) + " %",
                    decimal.Parse(valorJurosFormatado).ToString("C2", new CultureInfo("pt-BR")), decimal.Parse(valorProdutosFormatado).ToString("C2", new CultureInfo("pt-BR")),
                    decimal.Parse(descontoFormatado).ToString("N2", new CultureInfo("pt-BR")) + " %", decimal.Parse(valorTotalFormatado).ToString("C2", new CultureInfo("pt-BR")));
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
                if (dgv_Orcamento.RowCount > 0)
                {
                    telaLoadingFoiAberta = true;
                    gerenciarTelaLoading.Abrir(this, formTelaLoading, carregarThread);

                    int codigoLinhaSelecionada = Convert.ToInt32(dgv_Orcamento.Rows[dgv_Orcamento.CurrentCell.RowIndex].Cells[colunaCodigoIndex].Value);

                    object sender = new object();
                    EventArgs e = new EventArgs();

                    foreach (Orcamento item in listaOrcamento.Where(o => o.codigo.Equals(codigoLinhaSelecionada)))
                    {
                        txtb_Codigo_Orcamento.Text = item.codigo.ToString();
                        mtxtb_UltimaAtualizacao_Orcamento.Text = DateTime.Parse(item.ultima_atualizacao.ToString()).ToString("dd/MM/yyyy HH:mm");
                        mtxtb_DataValidade_Orcamento.Text = DateTime.Parse(item.validade.ToString()).ToString("dd/MM/yyyy");

                        txtb_CodigoCliente.Text = item.Cliente.codigo.ToString();
                        txtb_DescricaoCliente.Text = item.Cliente.nome_razao_social.ToString();
                        if (item.Cliente.ativo_inativo.Equals(false) || !item.Cliente.tipo_pessoa.Equals("CLIENTE"))
                        {
                            PintarBackground_CampoValido_Invalido(txtb_CodigoCliente, false, false);
                            PintarBackground_CampoValido_Invalido(txtb_DescricaoCliente, false, false);
                        }
                        else
                        {
                            PintarBackground_CampoValido_Invalido(txtb_CodigoCliente, true, false);
                            PintarBackground_CampoValido_Invalido(txtb_DescricaoCliente, true, false);
                        }

                        txtb_PrazoEntrega_Orcamento.Text = item.prazo_entrega.ToString();
                        txtb_DescricaoPagamento_Orcamento.Text = item.descricao_pagamento.ToString();
                        txtb_Observacao_Orcamento.Text = item.observacao.ToString();

                        txtb_ValorEntrada_Orcamento.Text = item.valor_entrada.ToString();
                        txtb_ValorEntrada_Orcamento_Leave(sender, e);

                        txtb_QuantidadeParcelas_Orcamento.Text = item.quantidade_parcelas.ToString();
                        txtb_ValorParcela_Orcamento.Text = item.valor_parcela.ToString();
                        txtb_ValorParcela_Orcamento_Leave(sender, e);

                        txtb_Juros_Orcamento.Text = item.juros.ToString();
                        txtb_Juros_Orcamento_Leave(sender, e);
                        txtb_ValorJuros_Orcamento.Text = item.valor_juros.ToString();
                        txtb_ValorJuros_Orcamento_Leave(sender, e);

                        txtb_ValorProdutos_Orcamento.Text = item.total_produtos_servicos.ToString();
                        txtb_ValorProdutos_Orcamento_Leave(sender, e);

                        txtb_Desconto_Orcamento.Text = item.desconto.ToString();
                        txtb_Desconto_Orcamento_Leave(sender, e);

                        txtb_ValorTotal_Orcamento.Text = item.total_orcamento.ToString();
                        txtb_ValorTotal_Orcamento_Leave(sender, e);

                        lb_Usuario_Orcamento.Text = "(" + item.Usuario.codigo.ToString() + ") - " + item.Usuario.nome_razao_social.ToString();
                        if (item.Usuario.ativo_inativo.Equals(false))
                        {
                            lb_CriadoModificadoUsuario_Orcamento.BackColor = Color.RosyBrown;
                            lb_Usuario_Orcamento.BackColor = Color.RosyBrown;
                        }
                        else
                        {
                            lb_CriadoModificadoUsuario_Orcamento.BackColor = Color.White;
                            lb_Usuario_Orcamento.BackColor = Color.White;
                        }

                        if (primeiraPesquisaModoPesquisa.Equals(false))
                            orcamento = item;

                        break;
                    }

                    if (txtb_CodigoCliente.BackColor.Equals(Color.RosyBrown))
                    {
                        Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_ProdutosServicos_Orcamento);
                        Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_Materiais_Orcamento);
                    }
                    else
                    {
                        if (!tbc_Secundario.TabPages.Contains(tbp_Secundario_ProdutosServicos_Orcamento))
                            Formulario_AtivarAbaSecundaria(tbc_Secundario, tbp_Secundario_ProdutosServicos_Orcamento, 1);

                        if (!tbc_Secundario.TabPages.Contains(tbp_Secundario_Materiais_Orcamento))
                            Formulario_AtivarAbaSecundaria(tbc_Secundario, tbp_Secundario_Materiais_Orcamento, 2);

                        primeiraPesquisaModoPesquisa = true;
                        ObterListagemRegistrosDGV_Produtos_Servicos_Orcamento();
                        ObterListagemTodosRegistrosDGV_Materiais_Orcamento();
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

        private bool ValidandoCompilandoDadosCampos_TbPrincipal(Orcamento validandoOrcamento)
        {
            try
            {
                object sender = new object();
                EventArgs e = new EventArgs();

                bool dadosValidos = true;
                int variavelSaidaCodigo = 0;
                decimal variavelSaidaCamposDecimais = 0;
                DateTime variavelSaidaCamposData = DateTime.Now;

                validandoOrcamento.Usuario = usuario;
                validandoOrcamento.ultima_atualizacao = DateTime.Now;
                validandoOrcamento.prazo_entrega = txtb_PrazoEntrega_Orcamento.Text;
                validandoOrcamento.descricao_pagamento = txtb_DescricaoPagamento_Orcamento.Text;
                validandoOrcamento.observacao = txtb_Observacao_Orcamento.Text;
                validandoOrcamento.ativo_inativo = true;

                if (operacaoRegistro.Equals("ALTERAÇÃO") || operacaoRegistro.Equals("INATIVAÇÃO"))
                    validandoOrcamento.codigo = int.Parse(txtb_Codigo_Orcamento.Text);
                else
                    validandoOrcamento.codigo = 0;

                if (dadosValidos)
                {
                    if (DateTime.TryParse(mtxtb_DataValidade_Orcamento.Text.Trim(), out variavelSaidaCamposData))
                    {
                        if (variavelSaidaCamposData.Date >= DateTime.Now.Date)
                            validandoOrcamento.validade = variavelSaidaCamposData.Date;
                        else
                        {
                            gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("DATA DE VALIDADE DO ORÇAMENTO");
                            dadosValidos = false;
                        }

                    }
                    else
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("DATA DE VALIDADE DO ORÇAMENTO");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    if (int.TryParse(txtb_CodigoCliente.Text.Trim(), out variavelSaidaCodigo))
                    {
                        if (txtb_CodigoCliente.BackColor.Equals(System.Drawing.Color.RosyBrown))
                        {
                            gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CLIENTE DO ORÇAMENTO");
                            dadosValidos = false;
                        }
                        else
                        {
                            validandoOrcamento.Cliente.codigo = variavelSaidaCodigo;
                            validandoOrcamento.Cliente.nome_razao_social = txtb_DescricaoCliente.Text;
                        }
                    }
                    else
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CLIENTE DO ORÇAMENTO");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    variavelSaidaCamposDecimais = CampoMoeda_RemoverFormatacao(txtb_ValorEntrada_Orcamento);
                    if (variavelSaidaCamposDecimais >= 0)
                    {
                        validandoOrcamento.valor_entrada = variavelSaidaCamposDecimais;
                        txtb_ValorEntrada_Orcamento_Leave(sender, e);
                    }
                    else
                    {
                        txtb_ValorEntrada_Orcamento_Leave(sender, e);
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("VALOR ENTRADA DO ORÇAMENTO");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    if (int.TryParse(txtb_QuantidadeParcelas_Orcamento.Text.Trim(), out variavelSaidaCodigo))
                    {
                        if (variavelSaidaCodigo > 0)
                        {
                            validandoOrcamento.quantidade_parcelas = variavelSaidaCodigo;

                            variavelSaidaCamposDecimais = CampoMoeda_RemoverFormatacao(txtb_ValorParcela_Orcamento);
                            validandoOrcamento.valor_parcela = variavelSaidaCamposDecimais;
                            txtb_ValorParcela_Orcamento_Leave(sender, e);
                        }
                        else
                        {
                            gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("QUANTIDADE DE PARCELAS DO ORÇAMENTO");
                            dadosValidos = false;
                        }
                    }
                    else
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("QUANTIDADE DE PARCELAS DO ORÇAMENTO");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    variavelSaidaCamposDecimais = CampoMoeda_RemoverFormatacao(txtb_Juros_Orcamento);
                    if (variavelSaidaCamposDecimais >= 0)
                    {
                        validandoOrcamento.juros = variavelSaidaCamposDecimais;
                        txtb_Juros_Orcamento_Leave(sender, e);

                        variavelSaidaCamposDecimais = CampoMoeda_RemoverFormatacao(txtb_ValorJuros_Orcamento);
                        validandoOrcamento.valor_juros = variavelSaidaCamposDecimais;
                        txtb_ValorJuros_Orcamento_Leave(sender, e);
                    }
                    else
                    {
                        txtb_Juros_Orcamento_Leave(sender, e);
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("JUROS POR PARCELAS DO ORÇAMENTO");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    variavelSaidaCamposDecimais = CampoMoeda_RemoverFormatacao(txtb_Desconto_Orcamento);
                    if (variavelSaidaCamposDecimais >= 0)
                    {
                        validandoOrcamento.desconto = variavelSaidaCamposDecimais;
                        txtb_Desconto_Orcamento_Leave(sender, e);
                    }
                    else
                    {
                        txtb_Desconto_Orcamento_Leave(sender, e);
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("DESCONTO DO ORÇAMENTO");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    variavelSaidaCamposDecimais = CampoMoeda_RemoverFormatacao(txtb_ValorProdutos_Orcamento);
                    if (variavelSaidaCamposDecimais >= 0)
                    {
                        validandoOrcamento.total_produtos_servicos = variavelSaidaCamposDecimais;
                        txtb_ValorProdutos_Orcamento_Leave(sender, e);
                    }
                    else
                    {
                        txtb_ValorProdutos_Orcamento_Leave(sender, e);
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("VALOR PRODUTOS/SERVIÇOS DO ORÇAMENTO");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    variavelSaidaCamposDecimais = CampoMoeda_RemoverFormatacao(txtb_ValorTotal_Orcamento);
                    if (variavelSaidaCamposDecimais >= 0)
                    {
                        validandoOrcamento.total_orcamento = variavelSaidaCamposDecimais;
                        txtb_ValorTotal_Orcamento_Leave(sender, e);
                    }
                    else
                    {
                        txtb_ValorTotal_Orcamento_Leave(sender, e);
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("VALOR TOTAL DO ORÇAMENTO");
                        dadosValidos = false;
                    }
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
                List<Orcamento> listaImpressao = new ListaOrcamento();

                if (imagemButtomPesquisa.Equals("PESQUISAR"))
                    listaImpressao = listaOrcamento;
                else
                    listaImpressao = listaPesquisada;

                conexaoExcel = new Excel.Application();
                arquivoExcel = conexaoExcel.Workbooks.Open(patchTabela);
                planilhaExcel = arquivoExcel.Worksheets[1] as Excel.Worksheet;

                int linha = 2;
                foreach (Orcamento item in listaImpressao)
                {
                    int coluna = 1;

                    planilhaExcel.Cells[linha, coluna++].Value = item.codigo.ToString();
                    planilhaExcel.Cells[linha, coluna++].Value = item.ultima_atualizacao.ToString();
                    planilhaExcel.Cells[linha, coluna++].Value = DateTime.Parse(item.validade.ToString()).ToString("dd/MM/yyyy");
                    planilhaExcel.Cells[linha, coluna++].Value = "(" + item.Cliente.codigo.ToString() + ") " + item.Cliente.nome_razao_social.ToString();
                    planilhaExcel.Cells[linha, coluna++].Value = item.prazo_entrega.ToString();
                    planilhaExcel.Cells[linha, coluna++].Value = item.descricao_pagamento.ToString();
                    planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.valor_entrada.ToString()).ToString("C2", new CultureInfo("pt-BR"));
                    planilhaExcel.Cells[linha, coluna++].Value = item.quantidade_parcelas.ToString();
                    planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.valor_parcela.ToString()).ToString("C2", new CultureInfo("pt-BR"));
                    planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.juros.ToString()).ToString("N2", new CultureInfo("pt-BR")) + " %";
                    planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.valor_juros.ToString()).ToString("C2", new CultureInfo("pt-BR"));
                    planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.total_produtos_servicos.ToString()).ToString("C2", new CultureInfo("pt-BR"));
                    planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.desconto.ToString()).ToString("N2", new CultureInfo("pt-BR")) + " %";
                    planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.total_orcamento.ToString()).ToString("C2", new CultureInfo("pt-BR"));
                    planilhaExcel.Cells[linha, coluna++].Value = "(" + item.Usuario.codigo.ToString() + ") " + item.Usuario.nome_razao_social.ToString();
                    planilhaExcel.Cells[linha, coluna++].Value = item.observacao.ToString();

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

        private void txtb_ValorEntrada_Orcamento_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_ValorEntrada_Orcamento);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_QuantidadeParcelas_Orcamento_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_QuantidadeParcelas_Orcamento);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ValorParcela_Orcamento_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_ValorParcela_Orcamento);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_Juros_Orcamento_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_Juros_Orcamento);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ValorJuros_Orcamento_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_ValorJuros_Orcamento);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ValorProdutos_Orcamento_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_ValorProdutos_Orcamento);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_Desconto_Orcamento_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_Desconto_Orcamento);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ValorTotal_Orcamento_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_ValorTotal_Orcamento);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos KeyPress

        private void txtb_CodigoCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_PrazoEntrega_Orcamento, e))
                    CampoMoeda_KeyPress(sender, e, true);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_PrazoEntrega_Orcamento_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Enter_FocusTxtb(txtb_DescricaoPagamento_Orcamento, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_DescricaoPagamento_Orcamento_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Enter_FocusTxtb(txtb_ValorEntrada_Orcamento, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ValorEntrada_Orcamento_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_QuantidadeParcelas_Orcamento, e))
                    CampoMoeda_KeyPress(sender, e, false);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_QuantidadeParcelas_Orcamento_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_Juros_Orcamento, e))
                    CampoMoeda_KeyPress(sender, e, true);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_Juros_Orcamento_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_Desconto_Orcamento, e))
                    CampoMoeda_KeyPress(sender, e, false);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_Desconto_Orcamento_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtb_CodigoCliente_Leave(object sender, EventArgs e)
        {
            try
            {
                telaLoadingFoiAberta = true;
                gerenciarTelaLoading.Abrir(this, formTelaLoading, carregarThread);

                int codigoRetorno = 0;
                if (int.TryParse(txtb_CodigoCliente.Text, out codigoRetorno) && codigoRetorno > 0)
                {
                    Pessoa obj = new Pessoa()
                    {
                        codigo = codigoRetorno,
                        tipo_pessoa = "CLIENTE",
                        ativo_inativo = true
                    };

                    ProcPessoa procPessoa = new ProcPessoa();

                    ListaPessoa listaPessoa = new ListaPessoa();
                    listaPessoa = procPessoa.ConsultarRegistro(obj, false);

                    if (listaPessoa.Count > 0)
                    {
                        foreach (Pessoa item in listaPessoa)
                        {
                            if (item.codigo.Equals(codigoRetorno) && item.tipo_pessoa.Equals("CLIENTE") && item.ativo_inativo.Equals(true))
                            {
                                txtb_CodigoCliente.Text = item.codigo.ToString();
                                txtb_DescricaoCliente.Text = item.nome_razao_social;

                                PintarBackground_CampoValido_Invalido(txtb_CodigoCliente, true, false);
                                PintarBackground_CampoValido_Invalido(txtb_DescricaoCliente, true, false);

                                break;
                            }
                            else
                            {
                                telaLoadingFoiAberta = false;
                                gerenciarTelaLoading.Fechar();

                                gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CÓDIGO CLIENTE DO ORÇAMENTO");
                                PintarBackground_CampoValido_Invalido(txtb_CodigoCliente, false, false);
                                PintarBackground_CampoValido_Invalido(txtb_DescricaoCliente, false, true);

                                break;
                            }
                        }
                    }
                    else
                    {
                        telaLoadingFoiAberta = false;
                        gerenciarTelaLoading.Fechar();

                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CÓDIGO CLIENTE DO ORÇAMENTO");
                        PintarBackground_CampoValido_Invalido(txtb_CodigoCliente, false, false);
                        PintarBackground_CampoValido_Invalido(txtb_DescricaoCliente, false, true);
                    }
                }
                else
                {
                    telaLoadingFoiAberta = false;
                    gerenciarTelaLoading.Fechar();

                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CÓDIGO CLIENTE DO ORÇAMENTO");
                    PintarBackground_CampoValido_Invalido(txtb_CodigoCliente, false, false);
                    PintarBackground_CampoValido_Invalido(txtb_DescricaoCliente, false, true);
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

        private void txtb_ValorEntrada_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorEntrada_Orcamento, false);

                decimal variavelValorEntrada = decimal.Parse(txtb_ValorEntrada_Orcamento.Text);
                if (variavelValorEntrada < 0)
                    txtb_ValorEntrada_Orcamento.Text = "0";

                txtb_ValorTotal_Orcamento_Enter(sender, e);
                decimal variavelValorTotal_Orcamento = decimal.Parse(txtb_ValorTotal_Orcamento.Text);

                if (variavelValorEntrada > variavelValorTotal_Orcamento)
                    txtb_ValorEntrada_Orcamento.Text = variavelValorTotal_Orcamento.ToString();

                txtb_QuantidadeParcelas_Orcamento_Leave(sender, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_QuantidadeParcelas_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                int variavelQuantidadeParcelas = 0;
                if (! int.TryParse(txtb_QuantidadeParcelas_Orcamento.Text, out variavelQuantidadeParcelas))
                    variavelQuantidadeParcelas = 1;

                if (variavelQuantidadeParcelas <= 0)
                {
                    txtb_QuantidadeParcelas_Orcamento.Text = "1";
                    variavelQuantidadeParcelas = 1;
                }
                else
                    txtb_QuantidadeParcelas_Orcamento.Text = variavelQuantidadeParcelas.ToString();

                txtb_ValorParcela_Orcamento_Enter(sender, e);
                txtb_ValorEntrada_Orcamento_Enter(sender, e);
                txtb_ValorTotal_Orcamento_Enter(sender, e);

                txtb_ValorParcela_Orcamento.Text = ((decimal.Parse(txtb_ValorTotal_Orcamento.Text) - decimal.Parse(txtb_ValorEntrada_Orcamento.Text)) / variavelQuantidadeParcelas).ToString();

                txtb_Juros_Orcamento_Enter(sender, e);
                txtb_Juros_Orcamento_Leave(sender, e);

                CampoMoeda_FormatacaoDecimal(txtb_ValorEntrada_Orcamento, true);
                txtb_ValorTotal_Orcamento_Leave(sender, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ValorParcela_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorParcela_Orcamento, true);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_Juros_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_Juros_Orcamento, false);

                txtb_ValorJuros_Orcamento_Enter(sender, e);
                txtb_ValorParcela_Orcamento_Enter(sender, e);

                txtb_ValorJuros_Orcamento.Text = ((decimal.Parse(txtb_Juros_Orcamento.Text) * decimal.Parse(txtb_ValorParcela_Orcamento.Text)) / 100).ToString();

                CampoFormatacao_Porcentagem(txtb_Juros_Orcamento);

                txtb_ValorJuros_Orcamento_Leave(sender, e);
                txtb_ValorParcela_Orcamento_Leave(sender, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ValorJuros_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorJuros_Orcamento, true);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ValorProdutos_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorProdutos_Orcamento, true);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_Desconto_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_Desconto_Orcamento, false);

                txtb_ValorProdutos_Orcamento_Enter(sender, e);
                txtb_ValorTotal_Orcamento_Enter(sender, e);

                decimal ValorDesconto = (decimal.Parse(txtb_Desconto_Orcamento.Text) * decimal.Parse(txtb_ValorProdutos_Orcamento.Text)) / 100;
                txtb_ValorTotal_Orcamento.Text = (decimal.Parse(txtb_ValorProdutos_Orcamento.Text) - ValorDesconto).ToString();

                CampoFormatacao_Porcentagem(txtb_Desconto_Orcamento);

                txtb_ValorEntrada_Orcamento_Enter(sender, e);
                txtb_ValorEntrada_Orcamento_Leave(sender, e);

                txtb_ValorProdutos_Orcamento_Leave(sender, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ValorTotal_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorTotal_Orcamento, true);
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
                btn_DadosGerais_Orcar.Enabled = false;

                Formulario_ModoModificacao(gb_ListagemGeral, gb_DadosGerais, tbc_Principal, tbc_Secundario, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar,
                    btn_DadosGerais_Deletar, btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_ProdutosServicos_Orcamento);
                Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_Materiais_Orcamento);

                operacaoRegistro = "CADASTRO";
                LimparCamposParaCadastro();

                mtxtb_DataValidade_Orcamento.Focus();
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
                btn_DadosGerais_Orcar.Enabled = false;

                Formulario_ModoModificacao(gb_ListagemGeral, gb_DadosGerais, tbc_Principal, tbc_Secundario, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar,
                    btn_DadosGerais_Deletar, btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_ProdutosServicos_Orcamento);
                Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_Materiais_Orcamento);

                operacaoRegistro = "ALTERAÇÃO";

                mtxtb_DataValidade_Orcamento.Focus();
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
                if (dgv_Orcamento.CurrentRow.Index > 0)
                {
                    int linha = MudarLinha_DGV(dgv_Orcamento, false);

                    dgv_Orcamento.CurrentCell = dgv_Orcamento.Rows[linha].Cells[dgv_Orcamento.CurrentCell.ColumnIndex];
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
                if (dgv_Orcamento.CurrentRow.Index < (dgv_Orcamento.RowCount - 1))
                {
                    int linha = MudarLinha_DGV(dgv_Orcamento, true);

                    dgv_Orcamento.CurrentCell = dgv_Orcamento.Rows[linha].Cells[dgv_Orcamento.CurrentCell.ColumnIndex];
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
                    Orcamento objetoChecagem = new Orcamento();
                    InstanciamentoRapida_Orcamento(objetoChecagem);

                    string statusOperacao = "EM ANDAMENTO";

                    if (ValidandoCompilandoDadosCampos_TbPrincipal(objetoChecagem))
                    {
                        int codigoRegistroRetornado = 0;

                        if (operacaoRegistro.Equals("CADASTRO") || operacaoRegistro.Equals("ALTERAÇÃO") || operacaoRegistro.Equals("INATIVAÇÃO"))
                        {
                            if (operacaoRegistro.Equals("INATIVAÇÃO"))
                            {
                                foreach (Produtos_Servicos_Orcamento item in listaProdutos_Servicos_Orcamento)
                                {
                                    if (item.Orcamento.codigo.Equals(orcamento.codigo))
                                    {
                                        if (ModificadorRegistros_Materiais_Orcamento(item, operacaoRegistro).Equals(false))
                                        {
                                            gerenciarMensagensPadraoSistema.ExceptionBancoDados("NÃO foi possivel realizar o processo de " + operacaoRegistro + " dos Materiais do Produto/Serviço (" + item.Produto_Servico.codigo + ") " + item.Produto_Servico.descricao + " !");
                                            return;
                                        }

                                        statusOperacao = procProdutos_Servicos_Orcamento.ManterRegistro(item, operacaoRegistro);

                                        if (int.TryParse(statusOperacao, out codigoRegistroRetornado).Equals(false))
                                        {
                                            gerenciarMensagensPadraoSistema.ExceptionBancoDados(statusOperacao);
                                            return;
                                        }
                                    }
                                }
                            }
                            else
                                statusOperacao = procOrcamento.ManterRegistro(objetoChecagem, operacaoRegistro);
                        }
                        else
                        {
                            gerenciarMensagensPadraoSistema.ExceptionBancoDados("A operação informada não existe !");
                            return;
                        }

                        if (int.TryParse(statusOperacao, out codigoRegistroRetornado))
                        {
                            orcamento = objetoChecagem;
                            orcamento.codigo = codigoRegistroRetornado;
                            
                            primeiraPesquisaModoPesquisa = true;

                            if (imagemButtomPesquisa.Equals("CANCELAR PESQUISA"))
                                btn_DadosGerais_Pesquisar_Click(sender, e);
                            else
                                ObterListagemTodosRegistrosDGV();

                            Formulario_ModoConsulta(gb_ListagemGeral, gb_DadosGerais, dgv_Orcamento, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar, btn_DadosGerais_Deletar,
                                btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                            if (btn_FecharCalendarioValidade_Orcamento.Visible.Equals(true))
                                btn_FecharCalendarioValidade_Orcamento_Click(sender, e);

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
                    Formulario_ModoConsulta(gb_ListagemGeral, gb_DadosGerais, dgv_Orcamento, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar, btn_DadosGerais_Deletar,
                        btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                    if (imagemButtomPesquisa.Equals("CANCELAR PESQUISA"))
                        btn_DadosGerais_Pesquisar_Click(sender, e);
                    else
                        ObterListagemTodosRegistrosDGV();

                    if (btn_FecharCalendarioValidade_Orcamento.Visible.Equals(true))
                        btn_FecharCalendarioValidade_Orcamento_Click(sender, e);

                    operacaoRegistro = "";
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_DadosGerais_Orcar_Click(object sender, EventArgs e)
        {
            try
            {
                if (listaProdutos_Servicos_Orcamento.Count > 0)
                {
                    using (formTelaConfiguracoesDocOrcamento = new FormTelaConfiguracoesDocOrcamento(orcamento, listaProdutos_Servicos_Orcamento))
                    {
                        formTelaConfiguracoesDocOrcamento.ShowDialog();
                    };
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("LISTA DE PRODUTOS / SERVIÇOS DO ORÇAMENTO");
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
                    formTelaPesquisaOrcamento = new FormTelaPesquisaOrcamento();
                    formTelaPesquisaOrcamento.ShowDialog();

                    if (!formTelaPesquisaOrcamento.campoPesquisado.Equals("CANCELADO") && !formTelaPesquisaOrcamento.informaçãoRetornada.Equals("VAZIA"))
                    {
                        listaPesquisada = new ListaOrcamento();

                        if (formTelaPesquisaOrcamento.campoPesquisado.Equals("CÓDIGO"))
                        {
                            listaPesquisada = listaOrcamento.Where(o => o.codigo.Equals(int.Parse(formTelaPesquisaOrcamento.informaçãoRetornada))).ToList();
                        }
                        else if (formTelaPesquisaOrcamento.campoPesquisado.Equals("ULTIMA ATUALIZAÇÃO") && !formTelaPesquisaOrcamento.informaçãoRetornada2.Equals("VAZIA"))
                        {
                            listaPesquisada = listaOrcamento.Where(o => DateTime.Parse(o.ultima_atualizacao.ToString()).Date >= DateTime.Parse(formTelaPesquisaOrcamento.informaçãoRetornada).Date
                            && DateTime.Parse(o.ultima_atualizacao.ToString()).Date <= DateTime.Parse(formTelaPesquisaOrcamento.informaçãoRetornada2).Date).ToList();
                        }
                        else if (formTelaPesquisaOrcamento.campoPesquisado.Equals("DATA VALIDADE") && !formTelaPesquisaOrcamento.informaçãoRetornada2.Equals("VAZIA"))
                        {
                            listaPesquisada = listaOrcamento.Where(o => DateTime.Parse(o.validade.ToString()).Date >= DateTime.Parse(formTelaPesquisaOrcamento.informaçãoRetornada).Date
                            && DateTime.Parse(o.validade.ToString()).Date <= DateTime.Parse(formTelaPesquisaOrcamento.informaçãoRetornada2).Date).ToList();
                        }
                        else if (formTelaPesquisaOrcamento.campoPesquisado.Equals("RAZÃO SOCIAL"))
                        {
                            listaPesquisada = listaOrcamento.Where(o => o.Cliente.nome_razao_social.Contains(formTelaPesquisaOrcamento.informaçãoRetornada)).ToList();
                        }
                        else if (formTelaPesquisaOrcamento.campoPesquisado.Equals("VALOR ENTRADA") && !formTelaPesquisaOrcamento.informaçãoRetornada2.Equals("VAZIA"))
                        {
                            listaPesquisada = listaOrcamento.Where(o => decimal.Parse(o.valor_entrada.ToString()) >= decimal.Parse(formTelaPesquisaOrcamento.informaçãoRetornada)
                            && decimal.Parse(o.valor_entrada.ToString()) <= decimal.Parse(formTelaPesquisaOrcamento.informaçãoRetornada2)).ToList();
                        }
                        else if (formTelaPesquisaOrcamento.campoPesquisado.Equals("QUANTIDADE PARCELAS") && !formTelaPesquisaOrcamento.informaçãoRetornada2.Equals("VAZIA"))
                        {
                            listaPesquisada = listaOrcamento.Where(o => int.Parse(o.quantidade_parcelas.ToString()) >= int.Parse(formTelaPesquisaOrcamento.informaçãoRetornada)
                            && int.Parse(o.quantidade_parcelas.ToString()) <= int.Parse(formTelaPesquisaOrcamento.informaçãoRetornada2)).ToList();
                        }
                        else if (formTelaPesquisaOrcamento.campoPesquisado.Equals("VALOR PARCELA") && !formTelaPesquisaOrcamento.informaçãoRetornada2.Equals("VAZIA"))
                        {
                            listaPesquisada = listaOrcamento.Where(o => decimal.Parse(o.valor_parcela.ToString()) >= decimal.Parse(formTelaPesquisaOrcamento.informaçãoRetornada)
                            && decimal.Parse(o.valor_parcela.ToString()) <= decimal.Parse(formTelaPesquisaOrcamento.informaçãoRetornada2)).ToList();
                        }
                        else if (formTelaPesquisaOrcamento.campoPesquisado.Equals("JUROS") && !formTelaPesquisaOrcamento.informaçãoRetornada2.Equals("VAZIA"))
                        {
                            listaPesquisada = listaOrcamento.Where(o => decimal.Parse(o.juros.ToString()) >= decimal.Parse(formTelaPesquisaOrcamento.informaçãoRetornada)
                            && decimal.Parse(o.juros.ToString()) <= decimal.Parse(formTelaPesquisaOrcamento.informaçãoRetornada2)).ToList();
                        }
                        else if (formTelaPesquisaOrcamento.campoPesquisado.Equals("VALOR JUROS") && !formTelaPesquisaOrcamento.informaçãoRetornada2.Equals("VAZIA"))
                        {
                            listaPesquisada = listaOrcamento.Where(o => decimal.Parse(o.valor_juros.ToString()) >= decimal.Parse(formTelaPesquisaOrcamento.informaçãoRetornada)
                            && decimal.Parse(o.valor_juros.ToString()) <= decimal.Parse(formTelaPesquisaOrcamento.informaçãoRetornada2)).ToList();
                        }
                        else if (formTelaPesquisaOrcamento.campoPesquisado.Equals("VALOR PRODUTOS/SERVIÇOS") && !formTelaPesquisaOrcamento.informaçãoRetornada2.Equals("VAZIA"))
                        {
                            listaPesquisada = listaOrcamento.Where(o => decimal.Parse(o.total_produtos_servicos.ToString()) >= decimal.Parse(formTelaPesquisaOrcamento.informaçãoRetornada)
                            && decimal.Parse(o.total_produtos_servicos.ToString()) <= decimal.Parse(formTelaPesquisaOrcamento.informaçãoRetornada2)).ToList();
                        }
                        else if (formTelaPesquisaOrcamento.campoPesquisado.Equals("DESCONTO") && !formTelaPesquisaOrcamento.informaçãoRetornada2.Equals("VAZIA"))
                        {
                            listaPesquisada = listaOrcamento.Where(o => decimal.Parse(o.desconto.ToString()) >= decimal.Parse(formTelaPesquisaOrcamento.informaçãoRetornada)
                            && decimal.Parse(o.desconto.ToString()) <= decimal.Parse(formTelaPesquisaOrcamento.informaçãoRetornada2)).ToList();
                        }
                        else if (formTelaPesquisaOrcamento.campoPesquisado.Equals("VALOR TOTAL") && !formTelaPesquisaOrcamento.informaçãoRetornada2.Equals("VAZIA"))
                        {
                            listaPesquisada = listaOrcamento.Where(o => decimal.Parse(o.total_orcamento.ToString()) >= decimal.Parse(formTelaPesquisaOrcamento.informaçãoRetornada)
                            && decimal.Parse(o.total_orcamento.ToString()) <= decimal.Parse(formTelaPesquisaOrcamento.informaçãoRetornada2)).ToList();
                        }
                        else
                        {
                            gerenciarMensagensPadraoSistema.InformacaoPesquisadaNaoEncontrada();
                            return;
                        }

                        if (listaPesquisada.Count() > 0)
                        {
                            dgv_Orcamento.Rows.Clear();

                            foreach (Orcamento item in listaPesquisada)
                            {
                                AdicionarLinhasDGV(item);
                            }

                            dgv_Orcamento.Rows[0].Selected = true;
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

        private void btn_PesquisarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                Pessoa objetoPesquisado = new Pessoa();

                int result;
                if (txtb_CodigoCliente.BackColor.Equals(System.Drawing.Color.RosyBrown) || txtb_CodigoCliente.Text.Trim().Equals("")
                    || ((int.TryParse(txtb_CodigoCliente.Text, out result)) && result <= 0))
                {
                    objetoPesquisado.codigo = 0;
                }
                else
                    objetoPesquisado.codigo = int.Parse(txtb_CodigoCliente.Text);

                using (FormPessoa formFornecedor = new FormPessoa(objetoPesquisado, "CLIENTE", true))
                {
                    formFornecedor.ShowDialog();

                    gerenciarInformacaoRetornadoPesquisa = formFornecedor.gerenciarInformacaoRetornadoPesquisa;
                    if (!gerenciarInformacaoRetornadoPesquisa.codigoRetornavelPesquisa.Equals(0))
                    {
                        txtb_CodigoCliente.Text = gerenciarInformacaoRetornadoPesquisa.codigoRetornavelPesquisa.ToString();
                        txtb_CodigoCliente_Leave(sender, e);
                    }
                    else if (!txtb_CodigoCliente.Text.Trim().Equals("") && (int.TryParse(txtb_CodigoCliente.Text, out result)) && result > 0)
                        txtb_CodigoCliente_Leave(sender, e);

                    txtb_PrazoEntrega_Orcamento.Focus();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_CalendarioValidade_Orcamento_Click(object sender, EventArgs e)
        {
            try
            {
                if (mc_CalendarioValidade_Orcamento.Visible.Equals(false))
                {
                    DateTime variavelData = DateTime.Today.Date;
                    if (DateTime.TryParse(mtxtb_DataValidade_Orcamento.Text.Trim(), out variavelData))
                    {
                        if (variavelData.Date >= DateTime.Today)
                            mc_CalendarioValidade_Orcamento.SetDate(variavelData);
                    }
                    
                    mc_CalendarioValidade_Orcamento.MinDate = DateTime.Today.Date;
                    mc_CalendarioValidade_Orcamento.Visible = true;
                    mc_CalendarioValidade_Orcamento.Focus();
                    
                    btn_CalendarioValidade_Orcamento.Visible = false;
                    btn_FecharCalendarioValidade_Orcamento.Visible = true;
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_FecharCalendarioValidade_Orcamento_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_FecharCalendarioValidade_Orcamento.Visible.Equals(true) && mc_CalendarioValidade_Orcamento.Visible.Equals(true))
                {
                    mtxtb_DataValidade_Orcamento.Focus();
                    btn_FecharCalendarioValidade_Orcamento.Visible = false;
                    mc_CalendarioValidade_Orcamento.Visible = false;

                    btn_CalendarioValidade_Orcamento.Visible = true;
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void mc_CalendarioValidade_Orcamento_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                if (mc_CalendarioValidade_Orcamento.Visible.Equals(true))
                {
                    mtxtb_DataValidade_Orcamento.Text = mc_CalendarioValidade_Orcamento.SelectionStart.ToShortDateString();
                    btn_FecharCalendarioValidade_Orcamento_Click(sender, e);
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Data Grid View

        private void dgv_Orcamento_KeyUp(object sender, KeyEventArgs e)
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

        private void dgv_Orcamento_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                var linhaClicada = dgv_Orcamento.HitTest(e.X, e.Y);
                if (linhaClicada != null && linhaClicada.RowIndex >= 0)
                    PreencherCampos();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void dgv_Orcamento_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                var linhaClicada = dgv_Orcamento.HitTest(e.X, e.Y);
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

        private void dgv_Orcamento_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                if (checkLinhasAtivas.Cliente.ativo_inativo.Equals(false) || !checkLinhasAtivas.Cliente.tipo_pessoa.Equals("CLIENTE"))
                    dgv_Orcamento.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.RosyBrown;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos MouseHovar

        private void lb_Obrigatorio_OC_1_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_OC_1, "outros");
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void lb_Obrigatorio_OC_2_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_OC_2, "numerico");
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void lb_Obrigatorio_OC_3_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_OC_3, "numerico");
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #endregion

        #region Produtos/Serviços do Orçamento

        #region Funções Gerais

        private void LimparCamposParaCadastro_Produtos_Servicos_Orcamento()
        {
            try
            {
                object sender = new object();
                EventArgs e = new EventArgs();

                lb_CodigoRelacional_Produtos_Servicos_Orcamento.Text = "0";
                mtxtb_DataAtualizacao_ProdutoServico_Orcamento.Clear();
                PintarBackground_CampoValido_Invalido(txtb_CodigoProdutoServico_Orcamento, true, true);
                PintarBackground_CampoValido_Invalido(txtb_DescricaoProdutoServico_Orcamento, true, true);
                
                PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade_ProdutoServico_Orcamento, true, true);
                PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade_ProdutoServico_Orcamento, true, true);
                PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade_ProdutoServico_Orcamento, true, true);
                
                txtb_AlturaProdutoServico_Orcamento.Clear();
                txtb_AlturaProdutoServico_Orcamento_Leave(sender, e);
                txtb_LarguraProdutoServico_Orcamento.Clear();
                txtb_LarguraProdutoServico_Orcamento_Leave(sender, e);
                txtb_ComprimentoProdutoServico_Orcamento.Clear();
                txtb_ComprimentoProdutoServico_Orcamento_Leave(sender, e);

                PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo_ProdutoServico_Orcamento, true, true);
                PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo_ProdutoServico_Orcamento, true, true);

                txtb_QuantidadeProdutoServico_Orcamento.Clear();
                txtb_QuantidadeProdutoServico_Orcamento.Text = "1,00";

                txtb_MaoObra_ProdutoServico_Orcamento.Clear();
                txtb_MaoObra_ProdutoServico_Orcamento_Leave(sender, e);

                txtb_ValorMateriais_ProdutoServico_Orcamento.Clear();
                txtb_ValorMateriais_ProdutoServico_Orcamento_Leave(sender, e);

                txtb_ValorMaoObra_ProdutoServico_Orcamento.Clear();
                txtb_ValorMaoObra_ProdutoServico_Orcamento_Leave(sender, e);

                txtb_ValorUnitario_ProdutoServico_Orcamento.Clear();
                txtb_ValorUnitario_ProdutoServico_Orcamento_Leave(sender, e);

                txtb_ValorTotal_ProdutoServico_Orcamento.Clear();
                txtb_ValorTotal_ProdutoServico_Orcamento_Leave(sender, e);

                txtb_TotalProdutosServicos_Orcamento.Clear();
                txtb_TotalProdutosServicos_Orcamento_Leave(sender, e);

                pb_ExibindoImagemDGV_ProdutoServico_Orcamento.Load(imagemFundoPadrao);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void ObterListagemRegistrosDGV_Produtos_Servicos_Orcamento()
        {
            try
            {
                Produtos_Servicos_Orcamento item = new Produtos_Servicos_Orcamento();
                InstanciamentoRapida_ProdutosServicos_Orcamento(item);
                item.Orcamento = orcamento;

                listaProdutos_Servicos_Orcamento = procProdutos_Servicos_Orcamento.ConsultarRegistro(item, false);

                CarregarListaDGV_Produtos_Servicos_Orcamento();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void AdicionarLinhasDGV_Produtos_Servicos_Orcamento(Produtos_Servicos_Orcamento item)
        {
            try
            {
                string quantidadeFormatado = Convert.ToString(item.quantidade);
                string valorUnitarioFormatado = Convert.ToString(item.Produto_Servico.valor_total);
                string valorTotalFormatado = Convert.ToString(item.valor_total);

                checkLinhasAtivas_Produtos_Servicos_Orcamento = item;

                dgv_ProdutosServicosOrcamento.Rows.Add(item.codigo, item.Produto_Servico.codigo, DateTime.Parse(item.Produto_Servico.ultima_atualizacao.ToString()).ToString("dd/MM/yyyy HH:mm"),
                    item.Produto_Servico.descricao, item.Produto_Servico.Grupo.descricao, item.Produto_Servico.Unidade.sigla, decimal.Parse(quantidadeFormatado).ToString("N2", new CultureInfo("pt-BR")),
                    decimal.Parse(valorUnitarioFormatado).ToString("C2", new CultureInfo("pt-BR")), decimal.Parse(valorTotalFormatado).ToString("C2", new CultureInfo("pt-BR")));
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void CarregarListaDGV_Produtos_Servicos_Orcamento()
        {
            try
            {
                if (listaOrcamento.Count > 0)
                {
                    if (listaProdutos_Servicos_Orcamento.Count > 0)
                    {
                        dgv_ProdutosServicosOrcamento.Rows.Clear();
                        checkLinhasAtivas_Produtos_Servicos_Orcamento = new Produtos_Servicos_Orcamento();
                        InstanciamentoRapida_ProdutosServicos_Orcamento(checkLinhasAtivas_Produtos_Servicos_Orcamento);

                        object sender = new object();
                        EventArgs e = new EventArgs();

                        txtb_ValorProdutos_Orcamento_Enter(sender, e);
                        txtb_TotalProdutosServicos_Orcamento_Enter(sender, e);
                        valorTotalProdutos_Servicos_Orcamento = 0;

                        foreach (Produtos_Servicos_Orcamento item in listaProdutos_Servicos_Orcamento)
                        {
                            if (item.Orcamento.codigo.Equals(orcamento.codigo))
                            {
                                AdicionarLinhasDGV_Produtos_Servicos_Orcamento(item);
                                CalcularValorTotal_Produtos_Servicos_Orcamento(item);
                            }
                        }

                        txtb_ValorProdutos_Orcamento.Text = valorTotalProdutos_Servicos_Orcamento.ToString();
                        txtb_TotalProdutosServicos_Orcamento.Text = valorTotalProdutos_Servicos_Orcamento.ToString();
                        txtb_ValorProdutos_Orcamento_Leave(sender, e);
                        txtb_TotalProdutosServicos_Orcamento_Leave(sender, e);

                        if (primeiraPesquisaModoPesquisa)
                        {
                            int resultado = 0;
                            primeiraPesquisaModoPesquisa = false;

                            Produtos_Servicos_Orcamento converter = new Produtos_Servicos_Orcamento();
                            InstanciamentoRapida_ProdutosServicos_Orcamento(converter);

                            foreach (Produtos_Servicos_Orcamento item in listaProdutos_Servicos_Orcamento.Where(pso => pso.codigo.Equals(produtos_Servicos_Orcamento.codigo)))
                            {
                                resultado = (int)item.codigo;
                                break;
                            }

                            if (!resultado.Equals(0))
                            {
                                converter = listaProdutos_Servicos_Orcamento.Where(pso => pso.codigo.Equals(produtos_Servicos_Orcamento.codigo)).Last();

                                foreach (DataGridViewRow item in dgv_ProdutosServicosOrcamento.Rows)
                                {
                                    if (item.Cells[colunaCodigoIndex].Value.Equals((int)converter.codigo))
                                        dgv_ProdutosServicosOrcamento.CurrentCell = dgv_ProdutosServicosOrcamento.Rows[item.Index].Cells[colunaCodigoIndex];
                                }
                            }
                            else
                            {
                                primeiraPesquisaModoPesquisa = true;
                                dgv_ProdutosServicosOrcamento.Rows[0].Selected = true;
                            }
                        }
                        else
                            dgv_ProdutosServicosOrcamento.Rows[0].Selected = true;

                        btn_DadosGerais_Orcar.Enabled = true;

                        if (tbc_Secundario.TabPages.Contains(tbp_Secundario_ProdutosServicos_Orcamento))
                        {
                            PreencherCampos_Produtos_Servicos_Orcamento();
                            GerenciarBotoes_ListagemSecundaria(true, false, btn_ProdutoServico_Orcamento_Cadastrar, btn_ProdutoServico_Orcamento_Alterar, btn_ProdutoServico_Orcamento_Deletar, btn_ProdutoServico_Orcamento_Imprimir);
                        }
                    }
                    else
                    {
                        btn_DadosGerais_Orcar.Enabled = false;

                        primeiraPesquisaModoPesquisa = false;
                        dgv_ProdutosServicosOrcamento.Rows.Clear();

                        GerenciarBotoes_ListagemSecundaria(false, true, btn_ProdutoServico_Orcamento_Cadastrar, btn_ProdutoServico_Orcamento_Alterar, btn_ProdutoServico_Orcamento_Deletar, btn_ProdutoServico_Orcamento_Imprimir);
                        LimparCamposParaCadastro_Produtos_Servicos_Orcamento();
                    }
                }
                else
                {
                    btn_DadosGerais_Orcar.Enabled = false;

                    primeiraPesquisaModoPesquisa = false;
                    dgv_ProdutosServicosOrcamento.Rows.Clear();

                    GerenciarBotoes_ListagemSecundaria(false, false, btn_ProdutoServico_Orcamento_Cadastrar, btn_ProdutoServico_Orcamento_Alterar, btn_ProdutoServico_Orcamento_Deletar, btn_ProdutoServico_Orcamento_Imprimir);
                    LimparCamposParaCadastro_Produtos_Servicos_Orcamento();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void PreencherCampos_Produtos_Servicos_Orcamento()
        {
            try
            {
                if (dgv_ProdutosServicosOrcamento.RowCount > 0)
                {
                    int codigoLinhaSelecionada;

                    if (primeiraPesquisaModoPesquisa)
                    {
                        primeiraPesquisaModoPesquisa = false;

                        Produtos_Servicos_Orcamento pso = listaProdutos_Servicos_Orcamento.First();
                        codigoLinhaSelecionada = (int)pso.codigo;
                    }
                    else
                        codigoLinhaSelecionada = Convert.ToInt32(dgv_ProdutosServicosOrcamento.Rows[dgv_ProdutosServicosOrcamento.CurrentCell.RowIndex].Cells[colunaCodigoIndex].Value);

                    object sender = new object();
                    EventArgs e = new EventArgs();

                    foreach (Produtos_Servicos_Orcamento item in listaProdutos_Servicos_Orcamento.Where(mps => mps.codigo.Equals(codigoLinhaSelecionada)))
                    {
                        lb_CodigoRelacional_Produtos_Servicos_Orcamento.Text = item.codigo.ToString();

                        PreencherCampos_ProdutosServicos(item.Produto_Servico);

                        txtb_QuantidadeProdutoServico_Orcamento.Text = item.quantidade.ToString();
                        txtb_QuantidadeProdutoServico_Orcamento_Leave(sender, e);

                        txtb_ValorTotal_ProdutoServico_Orcamento.Text = item.valor_total.ToString();
                        txtb_ValorTotal_ProdutoServico_Orcamento_Leave(sender, e);

                        produtos_Servicos_Orcamento = item;
                        break;
                    }
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private bool ValidandoCompilandoDadosCampos_TbSecundario(Produtos_Servicos_Orcamento validandoProdutos_Servicos_Orcamento)
        {
            try
            {
                bool dadosValidos = true;
                int variavelSaidaCodigo = 0;
                decimal variavelSaidaCamposDecimais = 0;

                object sender = new object();
                EventArgs e = new EventArgs();

                if (dadosValidos)
                {
                    if (operacaoRegistro_Produtos_Servicos_Orcamento.Equals("ALTERAÇÃO") || operacaoRegistro_Produtos_Servicos_Orcamento.Equals("INATIVAÇÃO"))
                        validandoProdutos_Servicos_Orcamento.codigo = int.Parse(lb_CodigoRelacional_Produtos_Servicos_Orcamento.Text);
                    else
                        validandoProdutos_Servicos_Orcamento.codigo = 0;
                }

                if (dadosValidos)
                {
                    if (int.TryParse(txtb_CodigoProdutoServico_Orcamento.Text.Trim(), out variavelSaidaCodigo))
                    {
                        if (txtb_CodigoProdutoServico_Orcamento.BackColor.Equals(System.Drawing.Color.RosyBrown))
                        {
                            gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("PRODUTO/SERVIÇO");
                            dadosValidos = false;
                        }
                        else
                        {
                            validandoProdutos_Servicos_Orcamento.Produto_Servico.codigo = variavelSaidaCodigo;
                            validandoProdutos_Servicos_Orcamento.Produto_Servico.ultima_atualizacao = DateTime.Parse(mtxtb_DataAtualizacao_ProdutoServico_Orcamento.Text);
                            validandoProdutos_Servicos_Orcamento.Produto_Servico.descricao = txtb_DescricaoProdutoServico_Orcamento.Text;
                            validandoProdutos_Servicos_Orcamento.Produto_Servico.maoObra = CampoMoeda_RemoverFormatacao(txtb_MaoObra_ProdutoServico_Orcamento);
                            validandoProdutos_Servicos_Orcamento.Produto_Servico.valor_total_materiais = CampoMoeda_RemoverFormatacao(txtb_ValorMateriais_ProdutoServico_Orcamento);
                            validandoProdutos_Servicos_Orcamento.Produto_Servico.valor_maoObra = CampoMoeda_RemoverFormatacao(txtb_ValorMaoObra_ProdutoServico_Orcamento);
                            validandoProdutos_Servicos_Orcamento.Produto_Servico.ativo_inativo = true;

                            txtb_MaoObra_ProdutoServico_Orcamento_Leave(sender, e);
                            txtb_ValorMateriais_ProdutoServico_Orcamento_Leave(sender, e);
                            txtb_ValorMaoObra_ProdutoServico_Orcamento_Leave(sender, e);
                        }
                    }
                    else
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("PRODUTO/SERVIÇO");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    if (int.TryParse(txtb_CodigoUnidade_ProdutoServico_Orcamento.Text.Trim(), out variavelSaidaCodigo))
                    {
                        if (txtb_CodigoUnidade_ProdutoServico_Orcamento.BackColor.Equals(System.Drawing.Color.RosyBrown))
                        {
                            gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("UNIDADE DO PRODUTO/SERVIÇO");
                            dadosValidos = false;
                        }
                        else
                        {
                            validandoProdutos_Servicos_Orcamento.Produto_Servico.Unidade.codigo = variavelSaidaCodigo;
                            validandoProdutos_Servicos_Orcamento.Produto_Servico.Unidade.sigla = txtb_SiglaUnidade_ProdutoServico_Orcamento.Text;
                            validandoProdutos_Servicos_Orcamento.Produto_Servico.Unidade.descricao = txtb_DescricaoUnidade_ProdutoServico_Orcamento.Text;
                            validandoProdutos_Servicos_Orcamento.Produto_Servico.Unidade.ativo_inativo = true;
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
                    variavelSaidaCamposDecimais = CampoMoeda_RemoverFormatacao(txtb_AlturaProdutoServico_Orcamento);
                    if (variavelSaidaCamposDecimais >= 0)
                    {
                        validandoProdutos_Servicos_Orcamento.Produto_Servico.altura = variavelSaidaCamposDecimais;
                        txtb_AlturaProdutoServico_Orcamento_Leave(sender, e);
                    }
                    else
                    {
                        txtb_AlturaProdutoServico_Orcamento_Leave(sender, e);
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("ALTURA DO PRODUTO/SERVIÇO");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    variavelSaidaCamposDecimais = CampoMoeda_RemoverFormatacao(txtb_LarguraProdutoServico_Orcamento);
                    if (variavelSaidaCamposDecimais >= 0)
                    {
                        validandoProdutos_Servicos_Orcamento.Produto_Servico.largura = variavelSaidaCamposDecimais;
                        txtb_LarguraProdutoServico_Orcamento_Leave(sender, e);
                    }
                    else
                    {
                        txtb_LarguraProdutoServico_Orcamento_Leave(sender, e);
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("LARGURA DO PRODUTO/SERVIÇO");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    variavelSaidaCamposDecimais = CampoMoeda_RemoverFormatacao(txtb_ComprimentoProdutoServico_Orcamento);
                    if (variavelSaidaCamposDecimais >= 0)
                    {
                        validandoProdutos_Servicos_Orcamento.Produto_Servico.comprimento = variavelSaidaCamposDecimais;
                        txtb_ComprimentoProdutoServico_Orcamento_Leave(sender, e);
                    }
                    else
                    {
                        txtb_ComprimentoProdutoServico_Orcamento_Leave(sender, e);
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("COMPRIMENTO DO PRODUTO/SERVIÇO");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    if (int.TryParse(txtb_CodigoGrupo_ProdutoServico_Orcamento.Text.Trim(), out variavelSaidaCodigo))
                    {
                        if (txtb_CodigoGrupo_ProdutoServico_Orcamento.BackColor.Equals(System.Drawing.Color.RosyBrown))
                        {
                            gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("GRUPO DO PRODUTO/SERVIÇO");
                            dadosValidos = false;
                        }
                        else
                        {
                            validandoProdutos_Servicos_Orcamento.Produto_Servico.Grupo.codigo = variavelSaidaCodigo;
                            validandoProdutos_Servicos_Orcamento.Produto_Servico.Grupo.descricao = txtb_DescricaoGrupo_ProdutoServico_Orcamento.Text;
                            validandoProdutos_Servicos_Orcamento.Produto_Servico.Grupo.material_ou_produto = 'P';
                            validandoProdutos_Servicos_Orcamento.Produto_Servico.Grupo.ativo_inativo = true;
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
                    variavelSaidaCamposDecimais = CampoMoeda_RemoverFormatacao(txtb_ValorUnitario_ProdutoServico_Orcamento);
                    if (variavelSaidaCamposDecimais > 0)
                    {
                        validandoProdutos_Servicos_Orcamento.Produto_Servico.valor_total = variavelSaidaCamposDecimais;
                        txtb_ValorUnitario_ProdutoServico_Orcamento_Leave(sender, e);
                    }
                    else
                    {
                        txtb_ValorUnitario_ProdutoServico_Orcamento_Leave(sender, e);
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("VALOR UNITÁRIO DO PRODUTO/SERVIÇO");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    variavelSaidaCamposDecimais = CampoMoeda_RemoverFormatacao(txtb_QuantidadeProdutoServico_Orcamento);
                    if (variavelSaidaCamposDecimais > 0)
                    {
                        validandoProdutos_Servicos_Orcamento.quantidade = variavelSaidaCamposDecimais;
                        txtb_QuantidadeProdutoServico_Orcamento_Leave(sender, e);
                    }
                    else
                    {
                        txtb_QuantidadeProdutoServico_Orcamento_Leave(sender, e);
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("QUANTIDADE DO PRODUTO/SERVIÇO");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    variavelSaidaCamposDecimais = CampoMoeda_RemoverFormatacao(txtb_ValorTotal_ProdutoServico_Orcamento);
                    if (variavelSaidaCamposDecimais > 0)
                    {
                        validandoProdutos_Servicos_Orcamento.valor_total = variavelSaidaCamposDecimais;
                        txtb_ValorTotal_ProdutoServico_Orcamento_Leave(sender, e);
                    }
                    else
                    {
                        txtb_ValorTotal_ProdutoServico_Orcamento_Leave(sender, e);
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("VALOR TOTAL DO PRODUTO/SERVIÇO");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    OpenFileDialog checkImagem = new OpenFileDialog();
                    checkImagem.FileName = pb_ExibindoImagemDGV_ProdutoServico_Orcamento.ImageLocation;

                    if (checkImagem.CheckFileExists)
                        validandoProdutos_Servicos_Orcamento.Produto_Servico.imagem = checkImagem.FileName;
                    else
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("ARQUIVO DE IMAGEM DO PRODUTO/SERVIÇO");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    operacaoRegistro = "ALTERAÇÃO";
                    if (dadosValidos = ValidandoCompilandoDadosCampos_TbPrincipal(orcamento))
                        validandoProdutos_Servicos_Orcamento.Orcamento = orcamento;

                    operacaoRegistro = "";
                }

                if (dadosValidos)
                    dadosValidos = ValidandoCadastroRegistroDuplicado_TbSecundario(validandoProdutos_Servicos_Orcamento);

                return dadosValidos;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
                return false;
            }
        }

        private bool ValidandoCadastroRegistroDuplicado_TbSecundario(Produtos_Servicos_Orcamento validandoProdutos_Servicos_Orcamento)
        {
            try
            {
                bool dadosValidos = true;

                foreach (Produtos_Servicos_Orcamento item in listaProdutos_Servicos_Orcamento)
                {
                    if (operacaoRegistro_Produtos_Servicos_Orcamento.Equals("CADASTRO"))
                    {
                        if (item.Produto_Servico.codigo.Equals(validandoProdutos_Servicos_Orcamento.Produto_Servico.codigo))
                        {
                            dadosValidos = false;
                            break;
                        }
                    }
                    else if (operacaoRegistro_Produtos_Servicos_Orcamento.Equals("ALTERAÇÃO"))
                    {
                        if (item.Produto_Servico.codigo.Equals(validandoProdutos_Servicos_Orcamento.Produto_Servico.codigo))
                        {
                            if (!item.codigo.Equals(validandoProdutos_Servicos_Orcamento.codigo))
                            {
                                dadosValidos = false;
                                break;
                            }
                        }
                    }
                }

                if (dadosValidos.Equals(false))
                    gerenciarMensagensPadraoSistema.RegistroDuplicado("Esse Produto/Serviço", "Código: " + validandoProdutos_Servicos_Orcamento.Produto_Servico.codigo + " - " + validandoProdutos_Servicos_Orcamento.Produto_Servico.descricao);

                return dadosValidos;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
                return false;
            }
        }

        private void ModificarTabelaExcel_Produtos_Servicos_Orcamento(string patchTabela)
        {
            try
            {
                List<Produtos_Servicos_Orcamento> listaImpressao = new ListaProdutos_Servicos_Orcamento();

                listaImpressao = listaProdutos_Servicos_Orcamento;

                conexaoExcel = new Excel.Application();
                arquivoExcel = conexaoExcel.Workbooks.Open(patchTabela);
                planilhaExcel = arquivoExcel.Worksheets[1] as Excel.Worksheet;

                planilhaExcel.Cells[1, 1].Value = "ORÇAMENTO: " + orcamento.codigo.ToString();
                planilhaExcel.Cells[1, 2].Value = orcamento.ultima_atualizacao.ToString();
                planilhaExcel.Cells[1, 3].Value = "CLIENTE: " + "(" + orcamento.Cliente.codigo.ToString() + ") " + orcamento.Cliente.nome_razao_social.ToString();

                int linha = 3;
                foreach (Produtos_Servicos_Orcamento item in listaImpressao)
                {
                    if (item.Orcamento.codigo.Equals(orcamento.codigo))
                    {
                        int coluna = 1;

                        planilhaExcel.Cells[linha, coluna++].Value = item.codigo.ToString();
                        planilhaExcel.Cells[linha, coluna++].Value = item.Produto_Servico.ultima_atualizacao.ToString();
                        planilhaExcel.Cells[linha, coluna++].Value = item.Produto_Servico.descricao.ToString();
                        planilhaExcel.Cells[linha, coluna++].Value = "(" + item.Produto_Servico.Grupo.codigo.ToString() + ") " + item.Produto_Servico.Grupo.descricao.ToString();
                        planilhaExcel.Cells[linha, coluna++].Value = "(" + item.Produto_Servico.Unidade.codigo.ToString() + ") ( " + item.Produto_Servico.Unidade.sigla.ToString() + " ) - " + item.Produto_Servico.Unidade.descricao.ToString();
                        planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.Produto_Servico.maoObra.ToString()).ToString("N2", new CultureInfo("pt-BR")) + " %";
                        planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.Produto_Servico.valor_total_materiais.ToString()).ToString("C2", new CultureInfo("pt-BR"));
                        planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.Produto_Servico.valor_maoObra.ToString()).ToString("C2", new CultureInfo("pt-BR"));
                        planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.quantidade.ToString()).ToString("N2", new CultureInfo("pt-BR"));
                        planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.Produto_Servico.valor_total.ToString()).ToString("C2", new CultureInfo("pt-BR"));
                        planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.valor_total.ToString()).ToString("C2", new CultureInfo("pt-BR"));
                        planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.Produto_Servico.altura.ToString()).ToString("N2", new CultureInfo("pt-BR"));
                        planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.Produto_Servico.largura.ToString()).ToString("N2", new CultureInfo("pt-BR"));
                        planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.Produto_Servico.comprimento.ToString()).ToString("N2", new CultureInfo("pt-BR"));

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
                txtb_CodigoProdutoServico_Orcamento.Enabled = ativaInativa;
                btn_PesquisarProdutoServico_Orcamento.Enabled = ativaInativa;
                txtb_QuantidadeProdutoServico_Orcamento.Enabled = ativaInativa;
                btn_ProdutoServico_Orcamento_Confirmar.Enabled = ativaInativa;
                btn_ProdutoServico_Orcamento_Cancelar.Enabled = ativaInativa;

                btn_ProdutoServico_Orcamento_Cadastrar.Enabled = !ativaInativa;
                dgv_ProdutosServicosOrcamento.Enabled = !ativaInativa;

                if (registrosJaInseridos)
                {
                    btn_ProdutoServico_Orcamento_Alterar.Enabled = !ativaInativa;
                    btn_ProdutoServico_Orcamento_Deletar.Enabled = !ativaInativa;
                    btn_ProdutoServico_Orcamento_Imprimir.Enabled = !ativaInativa;
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void PreencherCampos_ProdutosServicos(Produto_Servico item)
        {
            try
            {
                object sender = new object();
                EventArgs e = new EventArgs();

                txtb_CodigoProdutoServico_Orcamento.Text = item.codigo.ToString();
                mtxtb_DataAtualizacao_ProdutoServico_Orcamento.Text = DateTime.Parse(item.ultima_atualizacao.ToString()).ToString("dd/MM/yyyy HH:mm");
                txtb_DescricaoProdutoServico_Orcamento.Text = item.descricao;

                if (item.ativo_inativo.Equals(false))
                {
                    PintarBackground_CampoValido_Invalido(txtb_CodigoProdutoServico_Orcamento, false, false);
                    PintarBackground_CampoValido_Invalido(txtb_DescricaoProdutoServico_Orcamento, false, false);
                }
                else
                {
                    PintarBackground_CampoValido_Invalido(txtb_CodigoProdutoServico_Orcamento, true, false);
                    PintarBackground_CampoValido_Invalido(txtb_DescricaoProdutoServico_Orcamento, true, false);
                }

                txtb_CodigoUnidade_ProdutoServico_Orcamento.Text = item.Unidade.codigo.ToString();
                txtb_SiglaUnidade_ProdutoServico_Orcamento.Text = item.Unidade.sigla;
                txtb_DescricaoUnidade_ProdutoServico_Orcamento.Text = item.Unidade.descricao;
                if (item.Unidade.ativo_inativo.Equals(false))
                {
                    PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade_ProdutoServico_Orcamento, false, false);
                    PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade_ProdutoServico_Orcamento, false, false);
                    PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade_ProdutoServico_Orcamento, false, false);
                }
                else
                {
                    PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade_ProdutoServico_Orcamento, true, false);
                    PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade_ProdutoServico_Orcamento, true, false);
                    PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade_ProdutoServico_Orcamento, true, false);
                }

                txtb_AlturaProdutoServico_Orcamento.Text = item.altura.ToString();
                txtb_AlturaProdutoServico_Orcamento_Leave(sender, e);
                txtb_LarguraProdutoServico_Orcamento.Text = item.largura.ToString();
                txtb_LarguraProdutoServico_Orcamento_Leave(sender, e);
                txtb_ComprimentoProdutoServico_Orcamento.Text = item.comprimento.ToString();
                txtb_ComprimentoProdutoServico_Orcamento_Leave(sender, e);

                txtb_CodigoGrupo_ProdutoServico_Orcamento.Text = item.Grupo.codigo.ToString();
                txtb_DescricaoGrupo_ProdutoServico_Orcamento.Text = item.Grupo.descricao;
                if (item.Grupo.ativo_inativo.Equals(false) || !item.Grupo.material_ou_produto.Equals('P'))
                {
                    PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo_ProdutoServico_Orcamento, false, false);
                    PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo_ProdutoServico_Orcamento, false, false);
                }
                else
                {
                    PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo_ProdutoServico_Orcamento, true, false);
                    PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo_ProdutoServico_Orcamento, true, false);
                }

                txtb_MaoObra_ProdutoServico_Orcamento.Text = item.maoObra.ToString();
                txtb_MaoObra_ProdutoServico_Orcamento_Leave(sender, e);

                txtb_ValorMateriais_ProdutoServico_Orcamento.Text = item.valor_total_materiais.ToString();
                txtb_ValorMateriais_ProdutoServico_Orcamento_Leave(sender, e);

                txtb_ValorMaoObra_ProdutoServico_Orcamento.Text = item.valor_maoObra.ToString();
                txtb_ValorMaoObra_ProdutoServico_Orcamento_Leave(sender, e);

                txtb_ValorUnitario_ProdutoServico_Orcamento.Text = item.valor_total.ToString();
                txtb_ValorUnitario_ProdutoServico_Orcamento_Leave(sender, e);

                if (File.Exists(item.imagem))
                    pb_ExibindoImagemDGV_ProdutoServico_Orcamento.Load(item.imagem);
                else
                {
                    pb_ExibindoImagemDGV_ProdutoServico_Orcamento.Load(imagemFundoPadrao);
                    item.imagem = imagemFundoPadrao;
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void CalcularValorTotal_ProdutoServico()
        {
            try
            {
                object sender = new object();
                EventArgs e = new EventArgs();

                txtb_ValorTotal_ProdutoServico_Orcamento.Text = Convert.ToString(CampoMoeda_RemoverFormatacao(txtb_ValorUnitario_ProdutoServico_Orcamento) * decimal.Parse(txtb_QuantidadeProdutoServico_Orcamento.Text));

                txtb_ValorUnitario_ProdutoServico_Orcamento_Leave(sender, e);
                txtb_ValorTotal_ProdutoServico_Orcamento_Leave(sender, e);
                CampoMoeda_FormatacaoDecimal(txtb_QuantidadeProdutoServico_Orcamento, false);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void CalcularValorTotal_Produtos_Servicos_Orcamento(Produtos_Servicos_Orcamento item)
        {
            try
            {
                valorTotalProdutos_Servicos_Orcamento = valorTotalProdutos_Servicos_Orcamento + (decimal)item.valor_total;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Enter

        private void txtb_TotalProdutosServicos_Orcamento_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_TotalProdutosServicos_Orcamento);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos KeyPress

        private void txtb_CodigoProdutoServico_Orcamento_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_QuantidadeProdutoServico_Orcamento, e))
                    CampoMoeda_KeyPress(sender, e, true);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_QuantidadeProdutoServico_Orcamento_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusButton(btn_ProdutoServico_Orcamento_Confirmar, e))
                    CampoMoeda_KeyPress(sender, e, false);
                else
                    btn_ProdutoServico_Orcamento_Confirmar_Click(sender, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Leave

        private void txtb_CodigoProdutoServico_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                int codigoRetorno = 0;
                if (int.TryParse(txtb_CodigoProdutoServico_Orcamento.Text, out codigoRetorno) && codigoRetorno > 0)
                {
                    Produto_Servico obj = new Produto_Servico()
                    {
                        codigo = codigoRetorno,
                        ativo_inativo = true
                    };
                    InstanciamentoRapida_ProdutoServico(obj);

                    ProcProduto_Servico procProduto_Servico = new ProcProduto_Servico();

                    ListaProduto_Servico listaProduto_Servico = new ListaProduto_Servico();
                    listaProduto_Servico = procProduto_Servico.ConsultarRegistro(obj, false);

                    if (listaProduto_Servico.Count > 0)
                    {
                        foreach (Produto_Servico item in listaProduto_Servico)
                        {
                            if (item.codigo.Equals(codigoRetorno) && item.ativo_inativo.Equals(true))
                            {
                                PreencherCampos_ProdutosServicos(item);
                                CalcularValorTotal_ProdutoServico();

                                PintarBackground_CampoValido_Invalido(txtb_CodigoProdutoServico_Orcamento, true, false);
                                PintarBackground_CampoValido_Invalido(txtb_DescricaoProdutoServico_Orcamento, true, false);

                                break;
                            }
                            else
                            {
                                gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CÓDIGO PRODUTO/SERVIÇO");
                                PintarBackground_CampoValido_Invalido(txtb_CodigoProdutoServico_Orcamento, false, false);
                                PintarBackground_CampoValido_Invalido(txtb_DescricaoProdutoServico_Orcamento, false, true);

                                break;
                            }
                        }
                    }
                    else
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CÓDIGO PRODUTO/SERVIÇO");
                        PintarBackground_CampoValido_Invalido(txtb_CodigoProdutoServico_Orcamento, false, false);
                        PintarBackground_CampoValido_Invalido(txtb_DescricaoProdutoServico_Orcamento, false, true);
                    }
                }
                else
                {
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CÓDIGO PRODUTO/SERVIÇO");
                    PintarBackground_CampoValido_Invalido(txtb_CodigoProdutoServico_Orcamento, false, false);
                    PintarBackground_CampoValido_Invalido(txtb_DescricaoProdutoServico_Orcamento, false, true);
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_CodigoUnidade_ProdutoServico_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                int codigoRetorno = 0;
                if (int.TryParse(txtb_CodigoUnidade_ProdutoServico_Orcamento.Text, out codigoRetorno) && codigoRetorno > 0)
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
                                txtb_CodigoUnidade_ProdutoServico_Orcamento.Text = item.codigo.ToString();
                                txtb_SiglaUnidade_ProdutoServico_Orcamento.Text = item.sigla;
                                txtb_DescricaoUnidade_ProdutoServico_Orcamento.Text = item.descricao;

                                PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade_ProdutoServico_Orcamento, true, false);
                                PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade_ProdutoServico_Orcamento, true, false);
                                PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade_ProdutoServico_Orcamento, true, false);

                                break;
                            }
                            else
                            {
                                PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade_ProdutoServico_Orcamento, false, false);
                                PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade_ProdutoServico_Orcamento, false, true);
                                PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade_ProdutoServico_Orcamento, false, true);

                                break;
                            }
                        }
                    }
                    else
                    {
                        PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade_ProdutoServico_Orcamento, false, false);
                        PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade_ProdutoServico_Orcamento, false, true);
                        PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade_ProdutoServico_Orcamento, false, true);
                    }
                }
                else
                {
                    PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade_ProdutoServico_Orcamento, false, false);
                    PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade_ProdutoServico_Orcamento, false, true);
                    PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade_ProdutoServico_Orcamento, false, true);
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_AlturaProdutoServico_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_AlturaProdutoServico_Orcamento, false);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_LarguraProdutoServico_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_LarguraProdutoServico_Orcamento, false);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ComprimentoProdutoServico_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ComprimentoProdutoServico_Orcamento, false);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_CodigoGrupo_ProdutoServico_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                int codigoRetorno = 0;
                if (int.TryParse(txtb_CodigoGrupo_ProdutoServico_Orcamento.Text, out codigoRetorno) && codigoRetorno > 0)
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
                                txtb_CodigoGrupo_ProdutoServico_Orcamento.Text = item.codigo.ToString();
                                txtb_DescricaoGrupo_ProdutoServico_Orcamento.Text = item.descricao;

                                PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo_ProdutoServico_Orcamento, true, false);
                                PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo_ProdutoServico_Orcamento, true, false);

                                break;
                            }
                            else
                            {
                                PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo_ProdutoServico_Orcamento, false, false);
                                PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo_ProdutoServico_Orcamento, false, true);

                                break;
                            }
                        }
                    }
                    else
                    {
                        PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo_ProdutoServico_Orcamento, false, false);
                        PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo_ProdutoServico_Orcamento, false, true);
                    }
                }
                else
                {
                    PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo_ProdutoServico_Orcamento, false, false);
                    PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo_ProdutoServico_Orcamento, false, true);
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_MaoObra_ProdutoServico_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoFormatacao_Porcentagem(txtb_MaoObra_ProdutoServico_Orcamento);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ValorMateriais_ProdutoServico_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorMateriais_ProdutoServico_Orcamento, true);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ValorMaoObra_ProdutoServico_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorMaoObra_ProdutoServico_Orcamento, true);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_QuantidadeProdutoServico_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal variavelQuantidadeParcelas = 0;
                if (!decimal.TryParse(txtb_QuantidadeProdutoServico_Orcamento.Text, out variavelQuantidadeParcelas))
                    variavelQuantidadeParcelas = 1;

                if (variavelQuantidadeParcelas <= 0)
                {
                    txtb_QuantidadeProdutoServico_Orcamento.Text = "1,00";
                    variavelQuantidadeParcelas = 1;
                }
                else
                    txtb_QuantidadeProdutoServico_Orcamento.Text = variavelQuantidadeParcelas.ToString();
                
                CalcularValorTotal_ProdutoServico();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ValorUnitario_ProdutoServico_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorUnitario_ProdutoServico_Orcamento, true);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ValorTotal_ProdutoServico_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorTotal_ProdutoServico_Orcamento, true);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_TotalProdutosServicos_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_TotalProdutosServicos_Orcamento, true);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Button Click

        private void btn_ProdutoServico_Orcamento_Cadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (pb_ExibindoImagemDGV_ProdutoServico_Orcamento.Visible.Equals(true))
                {
                    pb_ExibindoImagemDGV_ProdutoServico_Orcamento.Visible = false;
                    btn_FecharImagemDGV_ProdutoServico_Orcamento.Visible = false;
                }

                btn_DadosGerais_Orcar.Enabled = false;

                Formulario_ModoModificacao(gb_ListagemGeral, gb_DadosGerais, tbc_Principal, tbc_Secundario, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar,
                    btn_DadosGerais_Deletar, btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_DadosGerais);
                Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_Materiais_Orcamento);

                AtivaInativaBotoes_TbSecundario(true, true);

                operacaoRegistro_Produtos_Servicos_Orcamento = "CADASTRO";
                LimparCamposParaCadastro_Produtos_Servicos_Orcamento();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_ProdutoServico_Orcamento_Alterar_Click(object sender, EventArgs e)
        {
            try
            {
                if (pb_ExibindoImagemDGV_ProdutoServico_Orcamento.Visible.Equals(true))
                {
                    pb_ExibindoImagemDGV_ProdutoServico_Orcamento.Visible = false;
                    btn_FecharImagemDGV_ProdutoServico_Orcamento.Visible = false;
                }

                btn_DadosGerais_Orcar.Enabled = false;

                Formulario_ModoModificacao(gb_ListagemGeral, gb_DadosGerais, tbc_Principal, tbc_Secundario, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar,
                    btn_DadosGerais_Deletar, btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_DadosGerais);
                Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_Materiais_Orcamento);

                AtivaInativaBotoes_TbSecundario(true, true);

                operacaoRegistro_Produtos_Servicos_Orcamento = "ALTERAÇÃO";
                txtb_CodigoProdutoServico_Orcamento.Focus();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_ProdutoServico_Orcamento_Deletar_Click(object sender, EventArgs e)
        {
            try
            {
                operacaoRegistro_Produtos_Servicos_Orcamento = "INATIVAÇÃO";
                btn_ProdutoServico_Orcamento_Confirmar_Click(sender, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_ProdutoServico_Orcamento_Imprimir_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog diretorioDestinoArquivoImpressao = new FolderBrowserDialog();

                if (diretorioDestinoArquivoImpressao.ShowDialog().Equals(DialogResult.OK) && !string.IsNullOrWhiteSpace(diretorioDestinoArquivoImpressao.SelectedPath))
                {
                    if (pb_ExibindoImagemDGV_ProdutoServico_Orcamento.Visible.Equals(true))
                    {
                        pb_ExibindoImagemDGV_ProdutoServico_Orcamento.Visible = false;
                        btn_FecharImagemDGV_ProdutoServico_Orcamento.Visible = false;
                    }

                    telaLoadingFoiAberta = true;
                    gerenciarTelaLoading.Abrir(this, formTelaLoading, carregarThread);

                    string nomeArquivo = Path.GetFileName(arquivoListaParaImpressao_Produtos_Servicos_Orcamento);

                    string nomeArquivoModificado = nomeArquivo.Replace(".xlsx", "");
                    nomeArquivoModificado = nomeArquivoModificado + " " + DateTime.Now.ToString("dd-MM-yyyy") + ".xlsx";

                    string arquivoListaModificado = Path.Combine(diretorioDestinoArquivoImpressao.SelectedPath, nomeArquivoModificado);

                    File.Copy(arquivoListaParaImpressao_Produtos_Servicos_Orcamento, arquivoListaModificado, true);

                    ModificarTabelaExcel_Produtos_Servicos_Orcamento(arquivoListaModificado);

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

        private void btn_ProdutoServico_Orcamento_Confirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (gerenciarMensagensPadraoSistema.Mensagem_Confirmacao(operacaoRegistro_Produtos_Servicos_Orcamento).Equals(DialogResult.OK))
                {
                    Produtos_Servicos_Orcamento objetoChecagem = new Produtos_Servicos_Orcamento();
                    InstanciamentoRapida_ProdutosServicos_Orcamento(objetoChecagem);

                    string statusOperacao = "EM ANDAMENTO";

                    if (ValidandoCompilandoDadosCampos_TbSecundario(objetoChecagem))
                    {
                        bool realizar_Modificacao_Materiais_Orcamento = true;

                        if (operacaoRegistro_Produtos_Servicos_Orcamento.Equals("CADASTRO") || operacaoRegistro_Produtos_Servicos_Orcamento.Equals("ALTERAÇÃO")
                            || operacaoRegistro_Produtos_Servicos_Orcamento.Equals("INATIVAÇÃO"))
                        {
                            if (operacaoRegistro_Produtos_Servicos_Orcamento.Equals("ALTERAÇÃO") || operacaoRegistro_Produtos_Servicos_Orcamento.Equals("INATIVAÇÃO"))
                            {
                                if (ModificadorRegistros_Materiais_Orcamento(objetoChecagem, operacaoRegistro_Produtos_Servicos_Orcamento).Equals(false))
                                {
                                    gerenciarMensagensPadraoSistema.ExceptionBancoDados("NÃO foi possivel realizar o processo de " + operacaoRegistro_Produtos_Servicos_Orcamento + " dos Materiais deste Produto/Serviço !");
                                    return;
                                }

                                if (operacaoRegistro_Produtos_Servicos_Orcamento.Equals("INATIVAÇÃO"))
                                    realizar_Modificacao_Materiais_Orcamento = false;
                            }
                            
                            statusOperacao = procProdutos_Servicos_Orcamento.ManterRegistro(objetoChecagem, operacaoRegistro_Produtos_Servicos_Orcamento);
                        }
                        else
                        {
                            gerenciarMensagensPadraoSistema.ExceptionBancoDados("A operação informada não existe !");
                            return;
                        }

                        int codigoRegistroRetornado = 0;
                        if (int.TryParse(statusOperacao, out codigoRegistroRetornado))
                        {
                            objetoChecagem.codigo = codigoRegistroRetornado;

                            if (realizar_Modificacao_Materiais_Orcamento)
                            {
                                operacaoRegistro_Produtos_Servicos_Orcamento = "CADASTRO";

                                if (ModificadorRegistros_Materiais_Orcamento(objetoChecagem, operacaoRegistro_Produtos_Servicos_Orcamento).Equals(false))
                                {
                                    gerenciarMensagensPadraoSistema.ExceptionBancoDados("NÃO foi possivel realizar o processo de " + operacaoRegistro_Produtos_Servicos_Orcamento + " dos Materiais deste Produto/Serviço !");
                                    return;
                                }
                            }
                            
                            produtos_Servicos_Orcamento = objetoChecagem;

                            ObterListagemRegistrosDGV_Produtos_Servicos_Orcamento();

                            Formulario_ModoConsulta(gb_ListagemGeral, gb_DadosGerais, dgv_Orcamento, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar, btn_DadosGerais_Deletar,
                                btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                            if (!tbc_Secundario.TabPages.Contains(tbp_Secundario_DadosGerais))
                                Formulario_AtivarAbaSecundaria(tbc_Secundario, tbp_Secundario_DadosGerais, 0);

                            if (!tbc_Secundario.TabPages.Contains(tbp_Secundario_Materiais_Orcamento))
                                Formulario_AtivarAbaSecundaria(tbc_Secundario, tbp_Secundario_Materiais_Orcamento, 2);

                            AtivaInativaBotoes_TbSecundario(false, true);

                            txtb_Desconto_Orcamento_Enter(sender, e);
                            txtb_Desconto_Orcamento_Leave(sender, e);

                            ObterListagemTodosRegistrosDGV_Materiais_Orcamento();

                            operacaoRegistro_Produtos_Servicos_Orcamento = "";
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

        private void btn_ProdutoServico_Orcamento_Cancelar_Click(object sender, EventArgs e)
        {
            try
            {
                if (gerenciarMensagensPadraoSistema.Mensagem_Cancelamento().Equals(DialogResult.OK))
                {
                    Formulario_ModoConsulta(gb_ListagemGeral, gb_DadosGerais, dgv_Orcamento, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar, btn_DadosGerais_Deletar,
                        btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                    if (!tbc_Secundario.TabPages.Contains(tbp_Secundario_DadosGerais))
                        Formulario_AtivarAbaSecundaria(tbc_Secundario, tbp_Secundario_DadosGerais, 0);

                    if (!tbc_Secundario.TabPages.Contains(tbp_Secundario_Materiais_Orcamento))
                        Formulario_AtivarAbaSecundaria(tbc_Secundario, tbp_Secundario_Materiais_Orcamento, 2);

                    bool registrosJaInseridos;
                    if (listaProdutos_Servicos_Orcamento.Count > 0)
                        registrosJaInseridos = true;
                    else
                        registrosJaInseridos = false;

                    ObterListagemRegistrosDGV_Produtos_Servicos_Orcamento();

                    AtivaInativaBotoes_TbSecundario(false, registrosJaInseridos);

                    operacaoRegistro_Produtos_Servicos_Orcamento = "";
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Button Pesquisa

        private void btn_PesquisarProdutoServico_Orcamento_Click(object sender, EventArgs e)
        {
            try
            {
                Produto_Servico objetoPesquisado = new Produto_Servico();

                int result;
                if (txtb_CodigoProdutoServico_Orcamento.BackColor.Equals(System.Drawing.Color.RosyBrown) || txtb_CodigoProdutoServico_Orcamento.Text.Trim().Equals("")
                    || ((int.TryParse(txtb_CodigoProdutoServico_Orcamento.Text, out result)) && result <= 0))
                {
                    objetoPesquisado.codigo = 0;
                }
                else
                    objetoPesquisado.codigo = int.Parse(txtb_CodigoProdutoServico_Orcamento.Text);

                using (FormProduto_Servico formProduto_Servico = new FormProduto_Servico(objetoPesquisado, true))
                {
                    formProduto_Servico.ShowDialog();

                    gerenciarInformacaoRetornadoPesquisa = formProduto_Servico.gerenciarInformacaoRetornadoPesquisa;
                    if (!gerenciarInformacaoRetornadoPesquisa.codigoRetornavelPesquisa.Equals(0))
                    {
                        txtb_CodigoProdutoServico_Orcamento.Text = gerenciarInformacaoRetornadoPesquisa.codigoRetornavelPesquisa.ToString();
                        txtb_CodigoProdutoServico_Orcamento_Leave(sender, e);
                    }
                    else if (!txtb_CodigoProdutoServico_Orcamento.Text.Trim().Equals("") && (int.TryParse(txtb_CodigoProdutoServico_Orcamento.Text, out result)) && result > 0)
                        txtb_CodigoProdutoServico_Orcamento_Leave(sender, e);

                    txtb_QuantidadeProdutoServico_Orcamento.Focus();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Data Grid View

        private void dgv_ProdutosServicosOrcamento_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue.Equals(((int)Keys.Up)) || e.KeyValue.Equals(((int)Keys.Down)))
                    PreencherCampos_Produtos_Servicos_Orcamento();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void dgv_ProdutosServicosOrcamento_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                var linhaClicada = dgv_ProdutosServicosOrcamento.HitTest(e.X, e.Y);
                if (linhaClicada != null && linhaClicada.RowIndex >= 0)
                {
                    if (e.Button.Equals(MouseButtons.Right))
                    {
                        Point localizacaoInicial = new Point(txtb_CodigoProdutoServico_Orcamento.Location.X, txtb_CodigoProdutoServico_Orcamento.Location.Y + txtb_CodigoProdutoServico_Orcamento.Size.Height);

                        if (linhaClicada.RowIndex.Equals(dgv_ProdutosServicosOrcamento.CurrentRow.Index))
                        {
                            if (pb_ExibindoImagemDGV_ProdutoServico_Orcamento.Visible.Equals(true))
                            {
                                pb_ExibindoImagemDGV_ProdutoServico_Orcamento.Visible = false;
                                btn_FecharImagemDGV_ProdutoServico_Orcamento.Visible = false;
                            }
                            else
                            {
                                ImagemDGV_RightClick(pb_ExibindoImagemDGV_ProdutoServico_Orcamento, btn_FecharImagemDGV_ProdutoServico_Orcamento, pb_FundoBase, localizacaoInicial);
                                pb_ExibindoImagemDGV_ProdutoServico_Orcamento.Visible = true;
                                btn_FecharImagemDGV_ProdutoServico_Orcamento.Visible = true;
                                btn_FecharImagemDGV_ProdutoServico_Orcamento.BringToFront();
                            }
                        }
                        else
                        {
                            dgv_ProdutosServicosOrcamento.CurrentCell = dgv_ProdutosServicosOrcamento.Rows[linhaClicada.RowIndex].Cells[0];

                            ImagemDGV_RightClick(pb_ExibindoImagemDGV_ProdutoServico_Orcamento, btn_FecharImagemDGV_ProdutoServico_Orcamento, pb_FundoBase, localizacaoInicial);
                            pb_ExibindoImagemDGV_ProdutoServico_Orcamento.Visible = true;
                            btn_FecharImagemDGV_ProdutoServico_Orcamento.Visible = true;
                            btn_FecharImagemDGV_ProdutoServico_Orcamento.BringToFront();
                        }
                    }

                    PreencherCampos_Produtos_Servicos_Orcamento();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_FecharImagemDGV_ProdutoServico_Orcamento_Click(object sender, EventArgs e)
        {
            try
            {
                if (pb_ExibindoImagemDGV_ProdutoServico_Orcamento.Visible.Equals(true))
                {
                    pb_ExibindoImagemDGV_ProdutoServico_Orcamento.Visible = false;
                    btn_FecharImagemDGV_ProdutoServico_Orcamento.Visible = false;
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void dgv_ProdutosServicosOrcamento_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                if (checkLinhasAtivas_Produtos_Servicos_Orcamento.Produto_Servico.ativo_inativo.Equals(false)
                    || checkLinhasAtivas_Produtos_Servicos_Orcamento.Produto_Servico.Unidade.ativo_inativo.Equals(false)
                    || checkLinhasAtivas_Produtos_Servicos_Orcamento.Produto_Servico.Grupo.ativo_inativo.Equals(false) || !checkLinhasAtivas_Produtos_Servicos_Orcamento.Produto_Servico.Grupo.material_ou_produto.Equals('P'))
                {
                    dgv_ProdutosServicosOrcamento.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.RosyBrown;
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
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_PS_1, "numerico");
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

        #endregion

        #endregion

        #region Materiais do Orçamento

        #region Funções Gerais

        private void LimparCamposParaCadastro_Materiais_Orcamento()
        {
            try
            {
                object sender = new object();
                EventArgs e = new EventArgs();

                lb_codigoRelacionamental_Materiais_Orcamento.Text = "0";

                PintarBackground_CampoValido_Invalido(txtb_CodigoProdutoServico_Material_Orcamento, true, true);
                PintarBackground_CampoValido_Invalido(txtb_DescricaoProdutoServico_Material_Orcamento, true, true);

                txtb_QuantidadeDesseProdutoServico_Material_Orcamento.Clear();
                txtb_QuantidadeDesseProdutoServico_Material_Orcamento_Leave(sender, e);

                PintarBackground_CampoValido_Invalido(txtb_CodigoMaterial_Orcamento, true, true);
                PintarBackground_CampoValido_Invalido(txtb_DescricaoMaterial_Orcamento, true, true);

                txtb_QuantidadePorProdutoServico_Material_Orcamento.Clear();
                txtb_QuantidadePorProdutoServico_Material_Orcamento_Leave(sender, e);
                txtb_ValorOriginalMaterial_Material_Orcamento.Clear();
                txtb_ValorOriginalMaterial_Material_Orcamento_Leave(sender, e);

                PintarBackground_CampoValido_Invalido(txtb_CodigoFornecedor_Material_Orcamento, true, true);
                PintarBackground_CampoValido_Invalido(txtb_DescricaoFornecedor_Material_Orcamento, true, true);

                PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade_Material_Orcamento, true, true);
                PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade_Material_Orcamento, true, true);
                PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade_Material_Orcamento, true, true);

                txtb_AlturaMaterial_Orcamento.Clear();
                txtb_AlturaMaterial_Orcamento_Leave(sender, e);
                txtb_LarguraMaterial_Orcamento.Clear();
                txtb_LarguraMaterial_Orcamento_Leave(sender, e);
                txtb_ComprimentoMaterial_Orcamento.Clear();
                txtb_ComprimentoMaterial_Orcamento_Leave(sender, e);

                PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo_Material_Orcamento, true, true);
                PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo_Material_Orcamento, true, true);

                txtb_QuantidadeMaterial_Orcamento.Clear();
                txtb_QuantidadeMaterial_Orcamento_Leave(sender, e);

                txtb_ValorPorProdutoServico_Material_Orcamento.Clear();
                txtb_ValorPorProdutoServico_Material_Orcamento_Leave(sender, e);

                txtb_ValorTotal_Material_Orcamento.Clear();
                txtb_ValorTotal_Material_Orcamento_Leave(sender, e);

                txtb_TotalMateriais_Orcamento.Clear();
                txtb_TotalMateriais_Orcamento_Leave(sender, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void ObterListagemTodosRegistrosDGV_Materiais_Orcamento()
        {
            try
            {
                Materiais_Orcamento item = new Materiais_Orcamento();
                InstanciamentoRapida_Materiais_Orcamento(item);
                item.Produtos_Servicos_Orcamento.Orcamento = orcamento;
                
                listaMateriais_Orcamento = procMateriais_Orcamento.ConsultarRegistro(item, true);

                CarregarListaDGV_Materiais_Orcamento();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void CarregarListaDGV_Materiais_Orcamento()
        {
            try
            {
                if (listaProdutos_Servicos_Orcamento.Count > 0)
                {
                    if (listaMateriais_Orcamento.Count > 0)
                    {
                        dgv_MateriaisOrcamento.Rows.Clear();
                        checkLinhasAtivas_Materiais_Orcamento = new Materiais_Orcamento();
                        InstanciamentoRapida_Materiais_Orcamento(checkLinhasAtivas_Materiais_Orcamento);

                        object sender = new object();
                        EventArgs e = new EventArgs();

                        txtb_TotalMateriais_Orcamento_Enter(sender, e);
                        valorTotalMateriais_Orcamento = 0;

                        foreach (Materiais_Orcamento item in listaMateriais_Orcamento)
                        {
                            if (item.Produtos_Servicos_Orcamento.Orcamento.codigo.Equals(orcamento.codigo))
                            {
                                foreach (Produtos_Servicos_Orcamento ps_Orcamento in listaProdutos_Servicos_Orcamento)
                                {
                                    if (ps_Orcamento.codigo.Equals(item.Produtos_Servicos_Orcamento.codigo))
                                    {
                                        item.Produtos_Servicos_Orcamento = ps_Orcamento;
                                        
                                        AdicionarLinhasDGV_Materiais_Orcamento(item);
                                        CalcularValorTotal_Materiais_Orcamento(item);
                                        break;
                                    }
                                }
                            }
                        }
                        
                        txtb_TotalMateriais_Orcamento.Text = valorTotalMateriais_Orcamento.ToString();
                        txtb_TotalMateriais_Orcamento_Leave(sender, e);

                        btn_Material_Orcamento_Imprimir.Enabled = true;
                        dgv_MateriaisOrcamento.Rows[0].Selected = true;
                    }
                    else
                    {
                        primeiraPesquisaModoPesquisa = false;
                        dgv_MateriaisOrcamento.Rows.Clear();

                        btn_Material_Orcamento_Imprimir.Enabled = false;
                        LimparCamposParaCadastro_Materiais_Orcamento();
                    }

                    if (tbc_Secundario.TabPages.Contains(tbp_Secundario_Materiais_Orcamento))
                        PreencherCampos_Materiais_Orcamento();
                }
                else
                {
                    primeiraPesquisaModoPesquisa = false;
                    dgv_MateriaisOrcamento.Rows.Clear();

                    btn_Material_Orcamento_Imprimir.Enabled = false;
                    LimparCamposParaCadastro_Materiais_Orcamento();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void AdicionarLinhasDGV_Materiais_Orcamento(Materiais_Orcamento item)
        {
            try
            {
                string valorUnitarioFormatado = Convert.ToString(item.Materiais_Produto_Servico.valor_total);
                string valorQuantidadeFormatado = Convert.ToString(item.quantidade_total);
                string valorTotalFormatado = Convert.ToString(item.valor_total);

                checkLinhasAtivas_Materiais_Orcamento = item;

                dgv_MateriaisOrcamento.Rows.Add(item.codigo, "(" + item.Produtos_Servicos_Orcamento.Produto_Servico.codigo + ") " + item.Produtos_Servicos_Orcamento.Produto_Servico.descricao,
                    "(" + item.Materiais_Produto_Servico.Material.codigo + ") " + item.Materiais_Produto_Servico.Material.descricao,
                    "(" + item.Materiais_Produto_Servico.Material.Fornecedor.codigo + ") " + item.Materiais_Produto_Servico.Material.Fornecedor.nome_razao_social,
                    item.Materiais_Produto_Servico.Material.Grupo.descricao, item.Materiais_Produto_Servico.Material.Unidade.sigla, item.Materiais_Produto_Servico.Material.altura,
                    item.Materiais_Produto_Servico.Material.largura, item.Materiais_Produto_Servico.Material.comprimento, decimal.Parse(valorUnitarioFormatado).ToString("C2", new CultureInfo("pt-BR")),
                    item.quantidade_total.ToString(), decimal.Parse(valorTotalFormatado).ToString("C2", new CultureInfo("pt-BR")));
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void PreencherCampos_Materiais_Orcamento()
        {
            try
            {
                if (dgv_MateriaisOrcamento.RowCount > 0)
                {
                    int codigoLinhaSelecionada;

                    if (primeiraPesquisaModoPesquisa)
                    {
                        primeiraPesquisaModoPesquisa = false;

                        Materiais_Orcamento mo = listaMateriais_Orcamento.First();
                        codigoLinhaSelecionada = (int)mo.codigo;
                    }
                    else
                        codigoLinhaSelecionada = Convert.ToInt32(dgv_MateriaisOrcamento.Rows[dgv_MateriaisOrcamento.CurrentCell.RowIndex].Cells[colunaCodigoIndex].Value);

                    object sender = new object();
                    EventArgs e = new EventArgs();

                    foreach (Materiais_Orcamento item in listaMateriais_Orcamento.Where(mo => mo.codigo.Equals(codigoLinhaSelecionada)))
                    {
                        lb_codigoRelacionamental_Materiais_Orcamento.Text = item.codigo.ToString();

                        txtb_CodigoProdutoServico_Material_Orcamento.Text = item.Produtos_Servicos_Orcamento.Produto_Servico.codigo.ToString();
                        txtb_DescricaoProdutoServico_Material_Orcamento.Text = item.Produtos_Servicos_Orcamento.Produto_Servico.descricao;
                        if (item.Produtos_Servicos_Orcamento.Produto_Servico.ativo_inativo.Equals(false))
                        {
                            PintarBackground_CampoValido_Invalido(txtb_CodigoProdutoServico_Material_Orcamento, false, false);
                            PintarBackground_CampoValido_Invalido(txtb_DescricaoProdutoServico_Material_Orcamento, false, false);
                        }
                        else
                        {
                            PintarBackground_CampoValido_Invalido(txtb_CodigoProdutoServico_Material_Orcamento, true, false);
                            PintarBackground_CampoValido_Invalido(txtb_DescricaoProdutoServico_Material_Orcamento, true, false);
                        }

                        txtb_QuantidadeDesseProdutoServico_Material_Orcamento.Text = item.Produtos_Servicos_Orcamento.quantidade.ToString();
                        txtb_QuantidadeDesseProdutoServico_Material_Orcamento_Leave(sender, e);

                        txtb_CodigoMaterial_Orcamento.Text = item.Materiais_Produto_Servico.Material.codigo.ToString();
                        txtb_DescricaoMaterial_Orcamento.Text = item.Materiais_Produto_Servico.Material.descricao;
                        if (item.Materiais_Produto_Servico.Material.ativo_inativo.Equals(false))
                        {
                            PintarBackground_CampoValido_Invalido(txtb_CodigoMaterial_Orcamento, false, false);
                            PintarBackground_CampoValido_Invalido(txtb_DescricaoMaterial_Orcamento, false, false);
                        }
                        else
                        {
                            PintarBackground_CampoValido_Invalido(txtb_CodigoMaterial_Orcamento, true, false);
                            PintarBackground_CampoValido_Invalido(txtb_DescricaoMaterial_Orcamento, true, false);
                        }

                        txtb_QuantidadePorProdutoServico_Material_Orcamento.Text = item.Materiais_Produto_Servico.quantidade.ToString();
                        txtb_QuantidadePorProdutoServico_Material_Orcamento_Leave(sender, e);
                        txtb_ValorOriginalMaterial_Material_Orcamento.Text = item.Materiais_Produto_Servico.Material.valor_unitario.ToString();
                        txtb_ValorOriginalMaterial_Material_Orcamento_Leave(sender, e);

                        txtb_CodigoFornecedor_Material_Orcamento.Text = item.Materiais_Produto_Servico.Material.Fornecedor.codigo.ToString();
                        txtb_DescricaoFornecedor_Material_Orcamento.Text = item.Materiais_Produto_Servico.Material.Fornecedor.nome_razao_social;
                        if (item.Materiais_Produto_Servico.Material.Fornecedor.ativo_inativo.Equals(false))
                        {
                            PintarBackground_CampoValido_Invalido(txtb_CodigoFornecedor_Material_Orcamento, false, false);
                            PintarBackground_CampoValido_Invalido(txtb_DescricaoFornecedor_Material_Orcamento, false, false);
                        }
                        else
                        {
                            PintarBackground_CampoValido_Invalido(txtb_CodigoFornecedor_Material_Orcamento, true, false);
                            PintarBackground_CampoValido_Invalido(txtb_DescricaoFornecedor_Material_Orcamento, true, false);
                        }

                        txtb_CodigoUnidade_Material_Orcamento.Text = item.Materiais_Produto_Servico.Material.Unidade.codigo.ToString();
                        txtb_SiglaUnidade_Material_Orcamento.Text = item.Materiais_Produto_Servico.Material.Unidade.sigla;
                        txtb_DescricaoUnidade_Material_Orcamento.Text = item.Materiais_Produto_Servico.Material.Unidade.descricao;
                        if (item.Materiais_Produto_Servico.Material.Unidade.ativo_inativo.Equals(false))
                        {
                            PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade_Material_Orcamento, false, false);
                            PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade_Material_Orcamento, false, false);
                            PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade_Material_Orcamento, false, false);
                        }
                        else
                        {
                            PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade_Material_Orcamento, true, false);
                            PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade_Material_Orcamento, true, false);
                            PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade_Material_Orcamento, true, false);
                        }

                        txtb_AlturaMaterial_Orcamento.Text = item.Materiais_Produto_Servico.Material.altura.ToString();
                        txtb_AlturaMaterial_Orcamento_Leave(sender, e);
                        txtb_LarguraMaterial_Orcamento.Text = item.Materiais_Produto_Servico.Material.largura.ToString();
                        txtb_LarguraMaterial_Orcamento_Leave(sender, e);
                        txtb_ComprimentoMaterial_Orcamento.Text = item.Materiais_Produto_Servico.Material.comprimento.ToString();
                        txtb_ComprimentoMaterial_Orcamento_Leave(sender, e);

                        txtb_CodigoGrupo_Material_Orcamento.Text = item.Materiais_Produto_Servico.Material.Grupo.codigo.ToString();
                        txtb_DescricaoGrupo_Material_Orcamento.Text = item.Materiais_Produto_Servico.Material.Grupo.descricao;
                        if (item.Materiais_Produto_Servico.Material.Grupo.ativo_inativo.Equals(false))
                        {
                            PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo_Material_Orcamento, false, false);
                            PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo_Material_Orcamento, false, false);
                        }
                        else
                        {
                            PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo_Material_Orcamento, true, false);
                            PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo_Material_Orcamento, true, false);
                        }
                        
                        txtb_QuantidadeMaterial_Orcamento.Text = item.quantidade_total.ToString();
                        txtb_QuantidadeMaterial_Orcamento_Leave(sender, e);

                        txtb_ValorPorProdutoServico_Material_Orcamento.Text = item.Materiais_Produto_Servico.valor_total.ToString();
                        txtb_ValorPorProdutoServico_Material_Orcamento_Leave(sender, e);

                        txtb_ValorTotal_Material_Orcamento.Text = item.valor_total.ToString();
                        txtb_ValorTotal_Material_Orcamento_Leave(sender, e);

                        if (File.Exists(item.Materiais_Produto_Servico.Material.imagem))
                            pb_ExibindoImagemDGV_Material_Orcamento.Load(item.Materiais_Produto_Servico.Material.imagem);
                        else
                        {
                            pb_ExibindoImagemDGV_Material_Orcamento.Load(imagemFundoPadrao);
                            item.Materiais_Produto_Servico.Material.imagem = imagemFundoPadrao;
                        }

                        materiais_Orcamento = item;
                        break;
                    }
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void ModificarTabelaExcel_Materiais_Orcamento(string patchTabela)
        {
            try
            {
                List<Materiais_Orcamento> listaImpressao = new ListaMateriais_Orcamento();
                listaImpressao = listaMateriais_Orcamento;

                conexaoExcel = new Excel.Application();
                arquivoExcel = conexaoExcel.Workbooks.Open(patchTabela);
                planilhaExcel = arquivoExcel.Worksheets[1] as Excel.Worksheet;

                planilhaExcel.Cells[1, 1].Value = "ORÇAMENTO: " + orcamento.codigo.ToString();
                planilhaExcel.Cells[1, 2].Value = "ULTIMA ATUALIZAÇÃO: " + orcamento.ultima_atualizacao.ToString();
                planilhaExcel.Cells[1, 3].Value = "CLIENTE: " + "(" + orcamento.Cliente.codigo.ToString() + ") " + orcamento.Cliente.nome_razao_social.ToString();

                int linha = 3;
                foreach (Materiais_Orcamento item in listaImpressao)
                {
                    int coluna = 1;

                    planilhaExcel.Cells[linha, coluna++].Value = item.codigo.ToString();
                    planilhaExcel.Cells[linha, coluna++].Value = "(" + item.Produtos_Servicos_Orcamento.Produto_Servico.codigo.ToString() + ") " + item.Produtos_Servicos_Orcamento.Produto_Servico.descricao.ToString();
                    planilhaExcel.Cells[linha, coluna++].Value = "(" + item.Materiais_Produto_Servico.Material.codigo.ToString() + ") " + item.Materiais_Produto_Servico.Material.descricao.ToString();
                    planilhaExcel.Cells[linha, coluna++].Value = "(" + item.Materiais_Produto_Servico.Material.Grupo.codigo.ToString() + ") " + item.Materiais_Produto_Servico.Material.Grupo.descricao.ToString();
                    planilhaExcel.Cells[linha, coluna++].Value = "(" + item.Materiais_Produto_Servico.Material.Unidade.codigo.ToString() + ") ( " + item.Materiais_Produto_Servico.Material.Unidade.sigla.ToString() + " ) - " + item.Materiais_Produto_Servico.Material.Unidade.descricao.ToString();
                    planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.Materiais_Produto_Servico.valor_total.ToString()).ToString("C2", new CultureInfo("pt-BR"));
                    planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.quantidade_total.ToString()).ToString("N2", new CultureInfo("pt-BR"));
                    planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.valor_total.ToString()).ToString("C2", new CultureInfo("pt-BR"));
                    planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.Materiais_Produto_Servico.Material.altura.ToString()).ToString("N2", new CultureInfo("pt-BR"));
                    planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.Materiais_Produto_Servico.Material.largura.ToString()).ToString("N2", new CultureInfo("pt-BR"));
                    planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.Materiais_Produto_Servico.Material.comprimento.ToString()).ToString("N2", new CultureInfo("pt-BR"));
                    planilhaExcel.Cells[linha, coluna++].Value = "(" + item.Materiais_Produto_Servico.Material.Fornecedor.codigo.ToString() + ") " + item.Materiais_Produto_Servico.Material.Fornecedor.nome_razao_social.ToString();

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

        private bool ModificadorRegistros_Materiais_Orcamento(Produtos_Servicos_Orcamento novoProdutoServico_Orcamento, string operacao_Materiais_Orcamento)
        {
            try
            {
                if (operacao_Materiais_Orcamento.Equals("CADASTRO"))
                {
                    Materiais_Orcamento cadastrar_Materiais_Orcamento = new Materiais_Orcamento();
                    InstanciamentoRapida_Materiais_Orcamento(cadastrar_Materiais_Orcamento);
                    cadastrar_Materiais_Orcamento.Produtos_Servicos_Orcamento = novoProdutoServico_Orcamento;

                    ListaMateriais_Produto_Servico listaMateriais_Produto_Servico = new ListaMateriais_Produto_Servico();
                    listaMateriais_Produto_Servico = ObtendoLista_Materiais_ProdutoServico(cadastrar_Materiais_Orcamento.Produtos_Servicos_Orcamento.Produto_Servico);

                    if (listaMateriais_Produto_Servico.Count > 0)
                    {
                        decimal quantidadeTotal = 0;
                        decimal valorTotal = 0;
                        foreach (Materiais_Produto_Servico mps in listaMateriais_Produto_Servico)
                        {
                            cadastrar_Materiais_Orcamento.codigo = 0;
                            if (mps.Produto_Servico.codigo.Equals(cadastrar_Materiais_Orcamento.Produtos_Servicos_Orcamento.Produto_Servico.codigo))
                            {
                                cadastrar_Materiais_Orcamento.Materiais_Produto_Servico = mps;

                                quantidadeTotal = (decimal)(cadastrar_Materiais_Orcamento.Produtos_Servicos_Orcamento.quantidade * mps.quantidade);
                                valorTotal = (decimal)(cadastrar_Materiais_Orcamento.Produtos_Servicos_Orcamento.quantidade * mps.valor_total);

                                cadastrar_Materiais_Orcamento.quantidade_total = decimal.Parse(quantidadeTotal.ToString("N2", new CultureInfo("pt-BR")));
                                cadastrar_Materiais_Orcamento.valor_total = decimal.Parse(valorTotal.ToString("N2", new CultureInfo("pt-BR")));

                                procMateriais_Orcamento.ManterRegistro(cadastrar_Materiais_Orcamento, operacao_Materiais_Orcamento);
                            }
                        }
                    }
                    else
                    {
                        gerenciarMensagensPadraoSistema.ExceptionBancoDados("Nenhum Material vinculado a este Produto/Serviço foi encontrado!\n A lista de Materiais do deste Produto/Serviço está vazia !");
                        return false;
                    }

                    return true;
                }
                else if (operacao_Materiais_Orcamento.Equals("ALTERAÇÃO") || operacao_Materiais_Orcamento.Equals("INATIVAÇÃO"))
                {
                    ListaMateriais_Orcamento listaMO = new ListaMateriais_Orcamento();
                    listaMO = ObtendoLista_Materiais_Orcamento(produtos_Servicos_Orcamento);

                    if (listaMO.Count > 0)
                    {
                        foreach (Materiais_Orcamento item in listaMO)
                        {
                            procMateriais_Orcamento.ManterRegistro(item, "INATIVAÇÃO");
                        }

                        return true;
                    }
                    else
                    {
                        gerenciarMensagensPadraoSistema.ExceptionBancoDados("Nenhum Material vinculado a este Produto/Serviço foi encontrado !");
                        return false;
                    }
                }
                else
                {
                    gerenciarMensagensPadraoSistema.ExceptionBancoDados("A operação informada não existe !");
                    return false;
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
                return false;
            }
        }

        private ListaMateriais_Produto_Servico ObtendoLista_Materiais_ProdutoServico(Produto_Servico instProdutoServico)
        {
            ListaMateriais_Produto_Servico listaMateriais_Produto_Servico = new ListaMateriais_Produto_Servico();

            try
            {
                Materiais_Produto_Servico item = new Materiais_Produto_Servico
                {
                    ativo_inativo = true
                };
                InstanciamentoRapida_Materiais_ProdutoServico(item);
                item.Produto_Servico.codigo = instProdutoServico.codigo;

                ProcMateriais_Produto_Servico procMateriais_Produto_Servico = new ProcMateriais_Produto_Servico();
                listaMateriais_Produto_Servico = procMateriais_Produto_Servico.ConsultarRegistro(item, false);

                return listaMateriais_Produto_Servico;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
                return listaMateriais_Produto_Servico;
            }
        }

        private ListaMateriais_Orcamento ObtendoLista_Materiais_Orcamento(Produtos_Servicos_Orcamento pesquisar_ProdutosServicos_Orcamento)
        {
            ListaMateriais_Orcamento ListaMO = new ListaMateriais_Orcamento();
            
            try
            {
                foreach (Materiais_Orcamento item in listaMateriais_Orcamento)
                {
                    if (item.Produtos_Servicos_Orcamento.Produto_Servico.codigo.Equals(pesquisar_ProdutosServicos_Orcamento.Produto_Servico.codigo))
                        ListaMO.Add(item);
                }

                return ListaMO;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
                return ListaMO;
            }
        }

        private void CalcularValorTotal_Materiais_Orcamento(Materiais_Orcamento item)
        {
            try
            {
                valorTotalMateriais_Orcamento = valorTotalMateriais_Orcamento + (decimal)item.valor_total;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Enter

        private void txtb_TotalMateriais_Orcamento_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_TotalMateriais_Orcamento);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Leave

        private void txtb_QuantidadeDesseProdutoServico_Material_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_QuantidadeDesseProdutoServico_Material_Orcamento, false);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_QuantidadePorProdutoServico_Material_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_QuantidadePorProdutoServico_Material_Orcamento, false);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ValorOriginalMaterial_Material_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorOriginalMaterial_Material_Orcamento, true);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_AlturaMaterial_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_AlturaMaterial_Orcamento, false);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_LarguraMaterial_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_LarguraMaterial_Orcamento, false);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ComprimentoMaterial_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ComprimentoMaterial_Orcamento, false);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ValorPorProdutoServico_Material_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorPorProdutoServico_Material_Orcamento, true);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_QuantidadeMaterial_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_QuantidadeMaterial_Orcamento, false);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ValorTotal_Material_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorTotal_Material_Orcamento, true);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_TotalMateriais_Orcamento_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_TotalMateriais_Orcamento, true);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Button Click

        private void btn_Material_Orcamento_Imprimir_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog diretorioDestinoArquivoImpressao = new FolderBrowserDialog();

                if (diretorioDestinoArquivoImpressao.ShowDialog().Equals(DialogResult.OK) && !string.IsNullOrWhiteSpace(diretorioDestinoArquivoImpressao.SelectedPath))
                {
                    if (pb_ExibindoImagemDGV_Material_Orcamento.Visible.Equals(true))
                    {
                        pb_ExibindoImagemDGV_Material_Orcamento.Visible = false;
                        btn_FecharImagemDGV_Material_Orcamento.Visible = false;
                    }

                    telaLoadingFoiAberta = true;
                    gerenciarTelaLoading.Abrir(this, formTelaLoading, carregarThread);

                    string nomeArquivo = Path.GetFileName(arquivoListaParaImpressao_Materiais_Orcamento);

                    string nomeArquivoModificado = nomeArquivo.Replace(".xlsx", "");
                    nomeArquivoModificado = nomeArquivoModificado + " " + DateTime.Now.ToString("dd-MM-yyyy") + ".xlsx";

                    string arquivoListaModificado = Path.Combine(diretorioDestinoArquivoImpressao.SelectedPath, nomeArquivoModificado);

                    File.Copy(arquivoListaParaImpressao_Materiais_Orcamento, arquivoListaModificado, true);

                    ModificarTabelaExcel_Materiais_Orcamento(arquivoListaModificado);

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

        #endregion

        #region Eventos Data Grid View
        
        private void dgv_MateriaisOrcamento_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue.Equals(((int)Keys.Up)) || e.KeyValue.Equals(((int)Keys.Down)))
                    PreencherCampos_Materiais_Orcamento();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void dgv_MateriaisOrcamento_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                var linhaClicada = dgv_MateriaisOrcamento.HitTest(e.X, e.Y);
                if (linhaClicada != null && linhaClicada.RowIndex >= 0)
                {
                    if (e.Button.Equals(MouseButtons.Right))
                    {
                        Point localizacaoInicial = new Point(txtb_CodigoMaterial_Orcamento.Location.X, txtb_CodigoMaterial_Orcamento.Location.Y + txtb_CodigoMaterial_Orcamento.Size.Height);

                        if (linhaClicada.RowIndex.Equals(dgv_MateriaisOrcamento.CurrentRow.Index))
                        {
                            if (pb_ExibindoImagemDGV_Material_Orcamento.Visible.Equals(true))
                            {
                                pb_ExibindoImagemDGV_Material_Orcamento.Visible = false;
                                btn_FecharImagemDGV_Material_Orcamento.Visible = false;
                            }
                            else
                            {
                                ImagemDGV_RightClick(pb_ExibindoImagemDGV_Material_Orcamento, btn_FecharImagemDGV_Material_Orcamento, pb_FundoBase, localizacaoInicial);
                                pb_ExibindoImagemDGV_Material_Orcamento.Visible = true;
                                btn_FecharImagemDGV_Material_Orcamento.Visible = true;
                                btn_FecharImagemDGV_Material_Orcamento.BringToFront();
                            }
                        }
                        else
                        {
                            dgv_MateriaisOrcamento.CurrentCell = dgv_MateriaisOrcamento.Rows[linhaClicada.RowIndex].Cells[0];

                            ImagemDGV_RightClick(pb_ExibindoImagemDGV_Material_Orcamento, btn_FecharImagemDGV_Material_Orcamento, pb_FundoBase, localizacaoInicial);
                            pb_ExibindoImagemDGV_Material_Orcamento.Visible = true;
                            btn_FecharImagemDGV_Material_Orcamento.Visible = true;
                            btn_FecharImagemDGV_Material_Orcamento.BringToFront();
                        }
                    }

                    PreencherCampos_Materiais_Orcamento();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_FecharImagemDGV_Material_Orcamento_Click(object sender, EventArgs e)
        {
            try
            {
                if (pb_ExibindoImagemDGV_Material_Orcamento.Visible.Equals(true))
                {
                    pb_ExibindoImagemDGV_Material_Orcamento.Visible = false;
                    btn_FecharImagemDGV_Material_Orcamento.Visible = false;
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void dgv_MateriaisOrcamento_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                if (checkLinhasAtivas_Materiais_Orcamento.Produtos_Servicos_Orcamento.Produto_Servico.ativo_inativo.Equals(false)
                    || checkLinhasAtivas_Materiais_Orcamento.Materiais_Produto_Servico.Material.ativo_inativo.Equals(false)
                    || checkLinhasAtivas_Materiais_Orcamento.Materiais_Produto_Servico.Material.Fornecedor.ativo_inativo.Equals(false)
                    || checkLinhasAtivas_Materiais_Orcamento.Materiais_Produto_Servico.Material.Unidade.ativo_inativo.Equals(false)
                    || checkLinhasAtivas_Materiais_Orcamento.Materiais_Produto_Servico.Material.Grupo.ativo_inativo.Equals(false))
                {
                    dgv_MateriaisOrcamento.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.RosyBrown;
                }
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