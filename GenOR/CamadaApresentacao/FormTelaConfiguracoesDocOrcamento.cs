using CamadaObjetoTransferencia;
using CamadaProcessamento;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace GenOR
{
    public partial class FormTelaConfiguracoesDocOrcamento : FormBase
    {
        #region Variaveis Configuracoes DOC. Orçamento

        private Orcamento orcamento;
        private ListaProdutos_Servicos_Orcamento listaProdutos_Servicos_Orcamento;

        private ProcTelefone procTelefone;
        private ProcEndereco procEndereco;

        private Telefone telefone_Usuario;
        private Endereco endereco_Usuario;
        private ListaTelefone listaTelefone_Usuario;
        private ListaEndereco listaEndereco_Usuario;

        private Telefone telefone_Cliente;
        private Endereco endereco_Cliente;
        private ListaTelefone listaTelefone_Cliente;
        private ListaEndereco listaEndereco_Cliente;

        private GerenciarMensagensPadraoSistema gerenciarMensagensPadraoSistema;

        private GerenciarTelaLoading gerenciarTelaLoading;
        private FormTelaLoading formTelaLoading;
        private Thread carregarThread;
        private bool telaLoadingFoiAberta;

        private string documentoOrcamento;
        private Word.Application conexaoWord;
        private Word.Document arquivoWord;
        private Word.Table planilhaWord;
        private object objAusente;

        #endregion

        public FormTelaConfiguracoesDocOrcamento(Orcamento orcamentoSelecionado, ListaProdutos_Servicos_Orcamento listaProdutosServicos_OrcamentoSelecionado)
        {
            try
            {
                InitializeComponent();

                orcamento = new Orcamento();
                InstanciamentoRapida_Orcamento(this.orcamento);
                orcamento = orcamentoSelecionado;

                listaProdutos_Servicos_Orcamento = new ListaProdutos_Servicos_Orcamento();
                listaProdutos_Servicos_Orcamento = listaProdutosServicos_OrcamentoSelecionado;

                procTelefone = new ProcTelefone();
                procEndereco = new ProcEndereco();

                telefone_Usuario = new Telefone { Pessoa = new Pessoa() };
                endereco_Usuario = new Endereco { Pessoa = new Pessoa() };
                listaTelefone_Usuario = new ListaTelefone();
                listaEndereco_Usuario = new ListaEndereco();

                telefone_Cliente = new Telefone { Pessoa = new Pessoa() };
                endereco_Cliente = new Endereco { Pessoa = new Pessoa() };
                listaTelefone_Cliente = new ListaTelefone();
                listaEndereco_Cliente = new ListaEndereco();

                gerenciarMensagensPadraoSistema = new GerenciarMensagensPadraoSistema();

                gerenciarTelaLoading = new GerenciarTelaLoading();
                formTelaLoading = new FormTelaLoading();
                carregarThread = null;
                telaLoadingFoiAberta = false;

                documentoOrcamento = Localizar_Imagem_Documento("ORÇAMENTO.doc", false);
                conexaoWord = null;
                arquivoWord = null;
                planilhaWord = null;
                objAusente = Missing.Value;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
                this.Close();
            }
        }

        #region Eventos Form

        private void FormTelaConfiguracoesDocOrcamento_Load(object sender, EventArgs e)
        {
            try
            {
                ObterListagemEndereco_Usuario();
                ObterListagemTelefone_Usuario();

                ObterListagemEndereco_Cliente();
                ObterListagemTelefone_Cliente();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Funções Gerais

        private void SubstituirTags(string tag, string itemSubstituir)
        {
            try
            {
                conexaoWord.Selection.Find.Execute(tag, true, true, false, false, false, true, false, 1, itemSubstituir, 2, false, false, false, false);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void ChecagemLabel(bool chek, Label label)
        {
            try
            {
                if (chek)
                    label.Enabled = chek;
                else
                    label.Enabled = chek;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void ChecagemGrid(bool chek, DataGridView grid)
        {
            try
            {
                if (chek)
                    grid.Enabled = chek;
                else
                    grid.Enabled = chek;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void ModificarDocumento_Orcamento(string patchDocumento)
        {
            try
            {
                conexaoWord = new Word.Application();
                arquivoWord = conexaoWord.Documents.Open(patchDocumento, ref objAusente, ref objAusente, ref objAusente, ref objAusente, ref objAusente, ref objAusente, ref objAusente, ref objAusente, ref objAusente, ref objAusente, ref objAusente, ref objAusente, ref objAusente, ref objAusente, ref objAusente);

                #region Informações Usuario

                if (chk_RazaoSocial_Usuario.Checked)
                    SubstituirTags("[razaoSocial_Usuario]", orcamento.Usuario.nome_razao_social.ToString());
                else
                    SubstituirTags("[razaoSocial_Usuario]", "");

                if (chk_NomeFantasia_Usuario.Checked && !orcamento.Usuario.nome_fantasia.Equals(""))
                    SubstituirTags("[nomeFantasia_Usuario]", orcamento.Usuario.nome_fantasia.ToString());
                else
                    SubstituirTags("([nomeFantasia_Usuario])", "");

                if (chk_GridEndereco_Usuario.Checked && !endereco_Usuario.codigo.Equals(null))
                {
                    SubstituirTags("[endereco_Usuario]", endereco_Usuario.endereco.ToString());
                    SubstituirTags("[numero_Usuario]", endereco_Usuario.numero.ToString());
                    SubstituirTags("[complemento_Usuario]", endereco_Usuario.complemento.ToString());
                    SubstituirTags("[cidade_Usuario]", endereco_Usuario.cidade.ToString());
                    SubstituirTags("[uf_Usuario]", endereco_Usuario.estado.ToString());
                    SubstituirTags("[bairro_Usuario]", endereco_Usuario.bairro.ToString());
                    SubstituirTags("[cep_Usuario]", endereco_Usuario.cep.ToString());
                }
                else
                {
                    SubstituirTags("[endereco_Usuario]", "");
                    SubstituirTags("[numero_Usuario]", "");
                    SubstituirTags("([complemento_Usuario])", "");
                    SubstituirTags("[cidade_Usuario]", "");
                    SubstituirTags("[uf_Usuario]", "");
                    SubstituirTags("[bairro_Usuario]", "");
                    SubstituirTags("[cep_Usuario]", "");
                }

                if (chk_CpfCnpj_Usuario.Checked && !orcamento.Usuario.cpf_cnpj.Equals(""))
                {
                    if (orcamento.Usuario.cpf_cnpj.Length.Equals(11))
                        SubstituirTags("[cpf_cnpj_Usuario]", Int64.Parse(orcamento.Usuario.cpf_cnpj).ToString(@"000\.000\.000\-00"));
                    else if (orcamento.Usuario.cpf_cnpj.Length.Equals(14))
                        SubstituirTags("[cpf_cnpj_Usuario]", Int64.Parse(orcamento.Usuario.cpf_cnpj).ToString(@"00\.000\.000\/0000-00"));
                    else
                        SubstituirTags("[cpf_cnpj_Usuario]", "");
                }
                else
                    SubstituirTags("[cpf_cnpj_Usuario]", "");

                if (chk_InscricaoEstadual_Usuario.Checked && !orcamento.Usuario.inscricao_estadual.Equals(""))
                {
                    if (orcamento.Usuario.inscricao_estadual.Length.Equals(12))
                        SubstituirTags("[inscricaoEstadual_Usuario]", Int64.Parse(orcamento.Usuario.inscricao_estadual).ToString(@"000\.000\.000\.000"));
                    else
                        SubstituirTags("[inscricaoEstadual_Usuario]", "");
                }
                else
                    SubstituirTags("[inscricaoEstadual_Usuario]", "");

                if (chk_Email_Usuario.Checked && !orcamento.Usuario.email.Equals(""))
                    SubstituirTags("[email_Usuario]", orcamento.Usuario.email.ToString());
                else
                    SubstituirTags("[email_Usuario]", "");

                if (chk_GridTelefone_Usuario.Checked && !telefone_Usuario.codigo.Equals(null))
                {
                    SubstituirTags("[ddd_Usuario]", telefone_Usuario.ddd.ToString());
                    SubstituirTags("[telefone_Usuario]", telefone_Usuario.numero.ToString());
                }
                else
                {
                    SubstituirTags("[ddd_Usuario]", "");
                    SubstituirTags("[telefone_Usuario]", "");
                }

                #endregion

                #region Informações Cliente

                if (chk_RazaoSocial_Cliente.Checked)
                    SubstituirTags("[razaoSocial_Cliente]", orcamento.Cliente.nome_razao_social.ToString());
                else
                    SubstituirTags("[razaoSocial_Cliente]", "");

                if (chk_NomeFantasia_Cliente.Checked && !orcamento.Cliente.nome_fantasia.Equals(""))
                    SubstituirTags("[nomeFantasia_Cliente]", orcamento.Cliente.nome_fantasia.ToString());
                else
                    SubstituirTags("([nomeFantasia_Cliente])", "");

                if (chk_GridEndereco_Cliente.Checked && !endereco_Cliente.codigo.Equals(null))
                {
                    SubstituirTags("[endereco_Cliente]", endereco_Cliente.endereco.ToString());
                    SubstituirTags("[numero_Cliente]", endereco_Cliente.numero.ToString());
                    SubstituirTags("[complemento_Cliente]", endereco_Cliente.complemento.ToString());
                    SubstituirTags("[cidade_Cliente]", endereco_Cliente.cidade.ToString());
                    SubstituirTags("[uf_Cliente]", endereco_Cliente.estado.ToString());
                    SubstituirTags("[bairro_Cliente]", endereco_Cliente.bairro.ToString());
                    SubstituirTags("[cep_Cliente]", endereco_Cliente.cep.ToString());
                }
                else
                {
                    SubstituirTags("[endereco_Cliente]", "");
                    SubstituirTags("[numero_Cliente]", "");
                    SubstituirTags("([complemento_Cliente])", "");
                    SubstituirTags("[cidade_Cliente]", "");
                    SubstituirTags("[uf_Cliente]", "");
                    SubstituirTags("[bairro_Cliente]", "");
                    SubstituirTags("[cep_Cliente]", "");
                }

                if (chk_CpfCnpj_Cliente.Checked && !orcamento.Cliente.cpf_cnpj.Equals(""))
                {
                    if (orcamento.Cliente.cpf_cnpj.Length.Equals(11))
                        SubstituirTags("[cpf_cnpj_Cliente]", Int64.Parse(orcamento.Cliente.cpf_cnpj).ToString(@"000\.000\.000\-00"));
                    else if (orcamento.Cliente.cpf_cnpj.Length.Equals(14))
                        SubstituirTags("[cpf_cnpj_Cliente]", Int64.Parse(orcamento.Cliente.cpf_cnpj).ToString(@"00\.000\.000\/0000-00"));
                    else
                        SubstituirTags("[cpf_cnpj_Cliente]", "");
                }
                else
                    SubstituirTags("[cpf_cnpj_Cliente]", "");

                if (chk_InscricaoEstadual_Clietne.Checked && !orcamento.Cliente.inscricao_estadual.Equals(""))
                {
                    if (orcamento.Cliente.inscricao_estadual.Length.Equals(12))
                        SubstituirTags("[inscricaoEstadual_Cliente]", Int64.Parse(orcamento.Cliente.inscricao_estadual).ToString(@"000\.000\.000\.000"));
                    else
                        SubstituirTags("[inscricaoEstadual_Cliente]", "");
                }
                else
                    SubstituirTags("[inscricaoEstadual_Cliente]", "");

                if (chk_Email_Cliente.Checked && !orcamento.Cliente.email.Equals(""))
                    SubstituirTags("[email_Cliente]", orcamento.Cliente.email.ToString());
                else
                    SubstituirTags("[email_Cliente]", "");

                if (chk_GridTelefone_Cliente.Checked && !telefone_Cliente.codigo.Equals(null))
                {
                    SubstituirTags("[ddd_Cliente]", telefone_Cliente.ddd.ToString());
                    SubstituirTags("[telefone_Cliente]", telefone_Cliente.numero.ToString());
                }
                else
                {
                    SubstituirTags("[ddd_Cliente]", "");
                    SubstituirTags("[telefone_Cliente]", "");
                }

                #endregion

                #region Add Produtos/Serviços

                int linha = 2;
                planilhaWord = arquivoWord.Tables[2];
                foreach (Produtos_Servicos_Orcamento item in listaProdutos_Servicos_Orcamento)
                {
                    int coluna = 1;

                    planilhaWord.Rows.Add(ref objAusente);
                    planilhaWord.Cell(linha, coluna++).Range.Text = decimal.Parse(item.quantidade.ToString()).ToString("N2", new CultureInfo("pt-BR"));
                    planilhaWord.Cell(linha, coluna++).Range.Text = item.Produto_Servico.descricao.ToString();
                    planilhaWord.Cell(linha, coluna++).Range.Text = decimal.Parse(item.Produto_Servico.valor_total.ToString()).ToString("C2", new CultureInfo("pt-BR"));
                    planilhaWord.Cell(linha, coluna++).Range.Text = decimal.Parse(item.valor_total.ToString()).ToString("C2", new CultureInfo("pt-BR"));

                    linha++;
                }

                #endregion

                #region Informações Gerais

                SubstituirTags("[total_ProdutosServicos]", decimal.Parse(orcamento.total_produtos_servicos.ToString()).ToString("C2", new CultureInfo("pt-BR")));
                SubstituirTags("[desconto_orcamento]", decimal.Parse(orcamento.desconto.ToString()).ToString("N2", new CultureInfo("pt-BR")) + " %");
                SubstituirTags("[total_orcamento]", decimal.Parse(orcamento.total_orcamento.ToString()).ToString("C2", new CultureInfo("pt-BR")));
                
                if (chk_ValorEntrada.Checked && !orcamento.valor_entrada.Equals(0))
                    SubstituirTags("[valorEntrada_Orcamento]", decimal.Parse(orcamento.valor_entrada.ToString()).ToString("C2", new CultureInfo("pt-BR")));
                else
                    SubstituirTags("[valorEntrada_Orcamento]", "");

                if (chk_QuantidadeParcelas.Checked && !orcamento.quantidade_parcelas.Equals(0))
                    SubstituirTags("[quantidadeParcelas_Orcamento]", orcamento.quantidade_parcelas.ToString());
                else
                    SubstituirTags("([quantidadeParcelas_Orcamento])", "");

                if (chk_ValorParcela.Checked && !orcamento.valor_parcela.Equals(0))
                    SubstituirTags("[valorParcela_Orcamento]", decimal.Parse(orcamento.valor_parcela.ToString()).ToString("C2", new CultureInfo("pt-BR")));
                else
                    SubstituirTags("[valorParcela_Orcamento]", "");

                if (chk_Juros.Checked && !orcamento.valor_juros.Equals(0))
                    SubstituirTags("[juros_Orcamento]", decimal.Parse(orcamento.valor_juros.ToString()).ToString("C2", new CultureInfo("pt-BR")));
                else
                    SubstituirTags("[juros_Orcamento]", "");

                if (chk_PrazoEntrega.Checked && !orcamento.prazo_entrega.Equals(""))
                    SubstituirTags("[prazoEntrega_Orcamento]", orcamento.prazo_entrega.ToString());
                else
                    SubstituirTags("[prazoEntrega_Orcamento].", "");

                if (chk_DescricaoPagamento.Checked && !orcamento.descricao_pagamento.Equals(""))
                    SubstituirTags("[descricaoPagamento_Orcamento]", orcamento.descricao_pagamento.ToString());
                else
                    SubstituirTags("[descricaoPagamento_Orcamento].", "");

                if (chk_DataEmissao.Checked)
                    SubstituirTags("[dataEmissao_Orcamento]", DateTime.Parse(orcamento.ultima_atualizacao.ToString()).ToString("dd/MM/yyyy"));
                else
                    SubstituirTags("[dataEmissao_Orcamento]", "");

                if (chk_Validade.Checked)
                    SubstituirTags("[dataValidade_Orcamento]", DateTime.Parse(orcamento.validade.ToString()).ToString("dd/MM/yyyy"));
                else
                    SubstituirTags("[dataValidade_Orcamento]", "");

                if (chk_Observacao.Checked && !orcamento.observacao.Equals(""))
                    SubstituirTags("[observações_Orcamento]", orcamento.observacao.ToString());
                else
                    SubstituirTags("[observações_Orcamento].", "");

                #endregion

                arquivoWord.Save();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
            finally
            {
                arquivoWord.Close();
                conexaoWord.Quit();
                arquivoWord = null;
                planilhaWord = null;
                conexaoWord = null;
            }
        }

        #endregion

        #region Eventos Button Click

        private void btn_Confirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (gerenciarMensagensPadraoSistema.Mensagem_Confirmacao("GERAR DOCUMENTO").Equals(DialogResult.OK))
                {
                    FolderBrowserDialog diretorioDestinoArquivoImpressao = new FolderBrowserDialog();

                    if (diretorioDestinoArquivoImpressao.ShowDialog().Equals(DialogResult.OK) && !string.IsNullOrWhiteSpace(diretorioDestinoArquivoImpressao.SelectedPath))
                    {
                        telaLoadingFoiAberta = true;
                        gerenciarTelaLoading.Abrir(this, formTelaLoading, carregarThread);

                        string nomeArquivo = Path.GetFileName(documentoOrcamento);

                        string nomeArquivoModificado = nomeArquivo.Replace(".doc", "");
                        nomeArquivoModificado = nomeArquivoModificado + " " + DateTime.Now.ToString("dd-MM-yyyy") + ".doc";

                        string arquivoListaModificado = Path.Combine(diretorioDestinoArquivoImpressao.SelectedPath, nomeArquivoModificado);

                        File.Copy(documentoOrcamento, arquivoListaModificado, true);

                        ModificarDocumento_Orcamento(arquivoListaModificado);

                        if (telaLoadingFoiAberta)
                        {
                            telaLoadingFoiAberta = false;
                            gerenciarTelaLoading.Fechar();
                        }

                        if (gerenciarMensagensPadraoSistema.Mensagem_AbrirDocumento().Equals(DialogResult.OK))
                            Process.Start(arquivoListaModificado);
                        
                        this.Close();
                    }
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            try
            {
                if (gerenciarMensagensPadraoSistema.Mensagem_FechamentoJanela("durante o processo de configuração do DOCUMENTO").Equals(DialogResult.OK))
                    this.Close();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Informações Usuario

        #region Endereco

        private void ObterListagemEndereco_Usuario()
        {
            try
            {
                Endereco item = new Endereco
                {
                    ativo_inativo = true,
                    Pessoa = orcamento.Usuario
                };

                listaEndereco_Usuario = procEndereco.ConsultarRegistro(item, false);

                CarregarListaDGVEndereco_Usuario();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void AdicionarLinhasDGVEndereco_Usuario(Endereco item)
        {
            try
            {
                dgv_Endereco_Usuario.Rows.Add(false, item.codigo, item.endereco, item.complemento, item.numero, item.bairro, item.cidade, item.estado, item.cep, item.observacao);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void CarregarListaDGVEndereco_Usuario()
        {
            try
            {
                if (listaEndereco_Usuario.Count > 0)
                {
                    const int linha = 0;
                    const int colCheckBox = 0;

                    dgv_Endereco_Usuario.Rows.Clear();

                    foreach (Endereco item in listaEndereco_Usuario)
                    {
                        if (item.Pessoa.codigo.Equals(orcamento.Usuario.codigo))
                            AdicionarLinhasDGVEndereco_Usuario(item);
                    }

                    dgv_Endereco_Usuario.Rows[linha].Selected = true;

                    dgv_Endereco_Usuario[colCheckBox, linha].Value = true;
                    endereco_Usuario = new Endereco
                    {
                        codigo = (int)dgv_Endereco_Usuario[colCheckBox + 1, linha].Value,
                        endereco = dgv_Endereco_Usuario[colCheckBox + 2, linha].Value.ToString(),
                        complemento = dgv_Endereco_Usuario[colCheckBox + 3, linha].Value.ToString(),
                        numero = dgv_Endereco_Usuario[colCheckBox + 4, linha].Value.ToString(),
                        bairro = dgv_Endereco_Usuario[colCheckBox + 5, linha].Value.ToString(),
                        cidade = dgv_Endereco_Usuario[colCheckBox + 6, linha].Value.ToString(),
                        estado = dgv_Endereco_Usuario[colCheckBox + 7, linha].Value.ToString(),
                        cep = dgv_Endereco_Usuario[colCheckBox + 8, linha].Value.ToString(),
                        observacao = dgv_Endereco_Usuario[colCheckBox + 9, linha].Value.ToString(),
                        ativo_inativo = true,
                        Pessoa = new Pessoa()
                    };
                }
                else
                {
                    chk_GridEndereco_Usuario.Checked = false;
                    chk_GridEndereco_Usuario.Enabled = false;
                    ChecagemGrid(chk_GridEndereco_Usuario.Checked, dgv_Endereco_Usuario);
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void dgv_Endereco_Usuario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                const int colCheckBox = 0;

                DataGridView tabelaDGV = (DataGridView)sender;

                if (e.ColumnIndex.Equals(colCheckBox))
                {
                    tabelaDGV.EndEdit();

                    bool campoChecked = (bool)tabelaDGV[e.ColumnIndex, e.RowIndex].Value;

                    if (campoChecked)
                    {
                        tabelaDGV[e.ColumnIndex, e.RowIndex].Value = !campoChecked;
                        endereco_Usuario = new Endereco { Pessoa = new Pessoa() };
                    }
                    else
                    {
                        foreach (DataGridViewRow linha in tabelaDGV.Rows)
                        {
                            if (linha.Index != e.RowIndex)
                                linha.Cells[colCheckBox].Value = campoChecked;
                        }

                        tabelaDGV[e.ColumnIndex, e.RowIndex].Value = !campoChecked;
                        endereco_Usuario = new Endereco
                        {
                            codigo = (int)tabelaDGV[(colCheckBox + 1), e.RowIndex].Value,
                            endereco = tabelaDGV[(colCheckBox + 2), e.RowIndex].Value.ToString(),
                            complemento = tabelaDGV[(colCheckBox + 3), e.RowIndex].Value.ToString(),
                            numero = tabelaDGV[(colCheckBox + 4), e.RowIndex].Value.ToString(),
                            bairro = tabelaDGV[(colCheckBox + 5), e.RowIndex].Value.ToString(),
                            cidade = tabelaDGV[(colCheckBox + 6), e.RowIndex].Value.ToString(),
                            estado = tabelaDGV[(colCheckBox + 7), e.RowIndex].Value.ToString(),
                            cep = tabelaDGV[(colCheckBox + 8), e.RowIndex].Value.ToString(),
                            observacao = tabelaDGV[(colCheckBox + 9), e.RowIndex].Value.ToString(),
                            ativo_inativo = true,
                            Pessoa = new Pessoa()
                        };
                    }
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Telefone

        private void ObterListagemTelefone_Usuario()
        {
            try
            {
                Telefone item = new Telefone
                {
                    ativo_inativo = true,
                    Pessoa = orcamento.Usuario
                };

                listaTelefone_Usuario = procTelefone.ConsultarRegistro(item, false);

                CarregarListaDGVTelefone_Usuario();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void AdicionarLinhasDGVTelefone_Usuario(Telefone item)
        {
            try
            {
                dgv_Telefone_Usuario.Rows.Add(false, item.codigo, "( " + item.ddd + " )", item.numero, item.observacao);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void CarregarListaDGVTelefone_Usuario()
        {
            try
            {
                if (listaTelefone_Usuario.Count > 0)
                {
                    const int linha = 0;
                    const int colCheckBox = 0;

                    dgv_Telefone_Usuario.Rows.Clear();

                    foreach (Telefone item in listaTelefone_Usuario)
                    {
                        if (item.Pessoa.codigo.Equals(orcamento.Usuario.codigo))
                            AdicionarLinhasDGVTelefone_Usuario(item);
                    }

                    dgv_Telefone_Usuario.Rows[linha].Selected = true;

                    dgv_Telefone_Usuario[colCheckBox, linha].Value = true;
                    telefone_Usuario = new Telefone
                    {
                        codigo = (int)dgv_Telefone_Usuario[colCheckBox + 1, linha].Value,
                        ddd = dgv_Telefone_Usuario[colCheckBox + 2, linha].Value.ToString(),
                        numero = dgv_Telefone_Usuario[colCheckBox + 3, linha].Value.ToString(),
                        observacao = dgv_Telefone_Usuario[colCheckBox + 4, linha].Value.ToString(),
                        ativo_inativo = true,
                        Pessoa = new Pessoa()
                    };
                }
                else
                {
                    chk_GridTelefone_Usuario.Checked = false;
                    chk_GridTelefone_Usuario.Enabled = false;
                    ChecagemGrid(chk_GridTelefone_Usuario.Checked, dgv_Telefone_Usuario);
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void dgv_Telefone_Usuario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                const int colCheckBox = 0;

                DataGridView tabelaDGV = (DataGridView)sender;

                if (e.ColumnIndex.Equals(colCheckBox))
                {
                    tabelaDGV.EndEdit();

                    bool campoChecked = (bool)tabelaDGV[e.ColumnIndex, e.RowIndex].Value;

                    if (campoChecked)
                    {
                        tabelaDGV[e.ColumnIndex, e.RowIndex].Value = !campoChecked;
                        telefone_Usuario = new Telefone { Pessoa = new Pessoa() };
                    }
                    else
                    {
                        foreach (DataGridViewRow linha in tabelaDGV.Rows)
                        {
                            if (linha.Index != e.RowIndex)
                                linha.Cells[colCheckBox].Value = campoChecked;
                        }

                        tabelaDGV[e.ColumnIndex, e.RowIndex].Value = !campoChecked;
                        telefone_Usuario = new Telefone
                        {
                            codigo = (int)tabelaDGV[(colCheckBox + 1), e.RowIndex].Value,
                            ddd = tabelaDGV[(colCheckBox + 2), e.RowIndex].Value.ToString(),
                            numero = tabelaDGV[(colCheckBox + 3), e.RowIndex].Value.ToString(),
                            observacao = tabelaDGV[(colCheckBox + 4), e.RowIndex].Value.ToString(),
                            ativo_inativo = true,
                            Pessoa = new Pessoa()
                        };
                    }
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos CheckedChanged

        private void chk_RazaoSocial_Usuario_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ChecagemLabel(chk_RazaoSocial_Usuario.Checked, lb_RazaoSocial_Usuario);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void chk_NomeFantasia_Usuario_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ChecagemLabel(chk_NomeFantasia_Usuario.Checked, lb_NomeFantasia_Usuario);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void chk_CpfCnpj_Usuario_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ChecagemLabel(chk_CpfCnpj_Usuario.Checked, lb_CpfCnpj_Usuario);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void chk_InscricaoEstadual_Usuario_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ChecagemLabel(chk_InscricaoEstadual_Usuario.Checked, lb_InscricaoEstadual_Usuario);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void chk_Email_Usuario_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ChecagemLabel(chk_Email_Usuario.Checked, lb_Email_Usuario);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void chk_GridEndereco_Usuario_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ChecagemGrid(chk_GridEndereco_Usuario.Checked, dgv_Endereco_Usuario);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void chk_GridTelefone_Usuario_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ChecagemGrid(chk_GridTelefone_Usuario.Checked, dgv_Telefone_Usuario);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #endregion

        #region Informações Cliente

        #region Endereco

        private void ObterListagemEndereco_Cliente()
        {
            try
            {
                Endereco item = new Endereco
                {
                    ativo_inativo = true,
                    Pessoa = orcamento.Cliente
                };

                listaEndereco_Cliente = procEndereco.ConsultarRegistro(item, false);

                CarregarListaDGVEndereco_Cliente();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void AdicionarLinhasDGVEndereco_Cliente(Endereco item)
        {
            try
            {
                dgv_Endereco_Cliente.Rows.Add(false, item.codigo, item.endereco, item.complemento, item.numero, item.bairro, item.cidade, item.estado, item.cep, item.observacao);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void CarregarListaDGVEndereco_Cliente()
        {
            try
            {
                if (listaEndereco_Cliente.Count > 0)
                {
                    const int linha = 0;
                    const int colCheckBox = 0;

                    dgv_Endereco_Cliente.Rows.Clear();

                    foreach (Endereco item in listaEndereco_Cliente)
                    {
                        if (item.Pessoa.codigo.Equals(orcamento.Cliente.codigo))
                            AdicionarLinhasDGVEndereco_Cliente(item);
                    }

                    dgv_Endereco_Cliente.Rows[linha].Selected = true;

                    dgv_Endereco_Cliente[colCheckBox, linha].Value = true;
                    endereco_Cliente = new Endereco
                    {
                        codigo = (int)dgv_Endereco_Cliente[colCheckBox + 1, linha].Value,
                        endereco = dgv_Endereco_Cliente[colCheckBox + 2, linha].Value.ToString(),
                        complemento = dgv_Endereco_Cliente[colCheckBox + 3, linha].Value.ToString(),
                        numero = dgv_Endereco_Cliente[colCheckBox + 4, linha].Value.ToString(),
                        bairro = dgv_Endereco_Cliente[colCheckBox + 5, linha].Value.ToString(),
                        cidade = dgv_Endereco_Cliente[colCheckBox + 6, linha].Value.ToString(),
                        estado = dgv_Endereco_Cliente[colCheckBox + 7, linha].Value.ToString(),
                        cep = dgv_Endereco_Cliente[colCheckBox + 8, linha].Value.ToString(),
                        observacao = dgv_Endereco_Cliente[colCheckBox + 9, linha].Value.ToString(),
                        ativo_inativo = true,
                        Pessoa = new Pessoa()
                    };
                }
                else
                {
                    chk_GridEndereco_Cliente.Checked = false;
                    chk_GridEndereco_Cliente.Enabled = false;
                    ChecagemGrid(chk_GridEndereco_Cliente.Checked, dgv_Endereco_Cliente);
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void dgv_Endereco_Cliente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                const int colCheckBox = 0;

                DataGridView tabelaDGV = (DataGridView)sender;

                if (e.ColumnIndex.Equals(colCheckBox))
                {
                    tabelaDGV.EndEdit();

                    bool campoChecked = (bool)tabelaDGV[e.ColumnIndex, e.RowIndex].Value;

                    if (campoChecked)
                    {
                        tabelaDGV[e.ColumnIndex, e.RowIndex].Value = !campoChecked;
                        endereco_Cliente = new Endereco { Pessoa = new Pessoa() };
                    }
                    else
                    {
                        foreach (DataGridViewRow linha in tabelaDGV.Rows)
                        {
                            if (linha.Index != e.RowIndex)
                                linha.Cells[colCheckBox].Value = campoChecked;
                        }

                        tabelaDGV[e.ColumnIndex, e.RowIndex].Value = !campoChecked;
                        endereco_Cliente = new Endereco
                        {
                            codigo = (int)tabelaDGV[(colCheckBox + 1), e.RowIndex].Value,
                            endereco = tabelaDGV[(colCheckBox + 2), e.RowIndex].Value.ToString(),
                            complemento = tabelaDGV[(colCheckBox + 3), e.RowIndex].Value.ToString(),
                            numero = tabelaDGV[(colCheckBox + 4), e.RowIndex].Value.ToString(),
                            bairro = tabelaDGV[(colCheckBox + 5), e.RowIndex].Value.ToString(),
                            cidade = tabelaDGV[(colCheckBox + 6), e.RowIndex].Value.ToString(),
                            estado = tabelaDGV[(colCheckBox + 7), e.RowIndex].Value.ToString(),
                            cep = tabelaDGV[(colCheckBox + 8), e.RowIndex].Value.ToString(),
                            observacao = tabelaDGV[(colCheckBox + 9), e.RowIndex].Value.ToString(),
                            ativo_inativo = true,
                            Pessoa = new Pessoa()
                        };
                    }
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Telefone

        private void ObterListagemTelefone_Cliente()
        {
            try
            {
                Telefone item = new Telefone
                {
                    ativo_inativo = true,
                    Pessoa = orcamento.Cliente
                };

                listaTelefone_Cliente = procTelefone.ConsultarRegistro(item, false);

                CarregarListaDGVTelefone_Cliente();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void AdicionarLinhasDGVTelefone_Cliente(Telefone item)
        {
            try
            {
                dgv_Telefone_Cliente.Rows.Add(false, item.codigo, "( " + item.ddd + " )", item.numero, item.observacao);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void CarregarListaDGVTelefone_Cliente()
        {
            try
            {
                if (listaTelefone_Cliente.Count > 0)
                {
                    const int linha = 0;
                    const int colCheckBox = 0;

                    dgv_Telefone_Cliente.Rows.Clear();

                    foreach (Telefone item in listaTelefone_Cliente)
                    {
                        if (item.Pessoa.codigo.Equals(orcamento.Cliente.codigo))
                            AdicionarLinhasDGVTelefone_Cliente(item);
                    }

                    dgv_Telefone_Cliente.Rows[linha].Selected = true;

                    dgv_Telefone_Cliente[colCheckBox, linha].Value = true;
                    telefone_Cliente = new Telefone
                    {
                        codigo = (int)dgv_Telefone_Cliente[colCheckBox + 1, linha].Value,
                        ddd = dgv_Telefone_Cliente[colCheckBox + 2, linha].Value.ToString(),
                        numero = dgv_Telefone_Cliente[colCheckBox + 3, linha].Value.ToString(),
                        observacao = dgv_Telefone_Cliente[colCheckBox + 4, linha].Value.ToString(),
                        ativo_inativo = true,
                        Pessoa = new Pessoa()
                    };
                }
                else
                {
                    chk_GridTelefone_Cliente.Checked = false;
                    chk_GridTelefone_Cliente.Enabled = false;
                    ChecagemGrid(chk_GridTelefone_Cliente.Checked, dgv_Telefone_Cliente);
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void dgv_Telefone_Cliente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                const int colCheckBox = 0;

                DataGridView tabelaDGV = (DataGridView)sender;

                if (e.ColumnIndex.Equals(colCheckBox))
                {
                    tabelaDGV.EndEdit();

                    bool campoChecked = (bool)tabelaDGV[e.ColumnIndex, e.RowIndex].Value;

                    if (campoChecked)
                    {
                        tabelaDGV[e.ColumnIndex, e.RowIndex].Value = !campoChecked;
                        telefone_Cliente = new Telefone { Pessoa = new Pessoa() };
                    }
                    else
                    {
                        foreach (DataGridViewRow linha in tabelaDGV.Rows)
                        {
                            if (linha.Index != e.RowIndex)
                                linha.Cells[colCheckBox].Value = campoChecked;
                        }

                        tabelaDGV[e.ColumnIndex, e.RowIndex].Value = !campoChecked;
                        telefone_Cliente = new Telefone
                        {
                            codigo = (int)tabelaDGV[(colCheckBox + 1), e.RowIndex].Value,
                            ddd = tabelaDGV[(colCheckBox + 2), e.RowIndex].Value.ToString(),
                            numero = tabelaDGV[(colCheckBox + 3), e.RowIndex].Value.ToString(),
                            observacao = tabelaDGV[(colCheckBox + 4), e.RowIndex].Value.ToString(),
                            ativo_inativo = true,
                            Pessoa = new Pessoa()
                        };
                    }
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos CheckedChanged

        private void chk_RazaoSocial_Cliente_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ChecagemLabel(chk_RazaoSocial_Cliente.Checked, lb_RazaoSocial_Cliente);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void chk_NomeFantasia_Cliente_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ChecagemLabel(chk_NomeFantasia_Cliente.Checked, lb_NomeFantasia_Cliente);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void chk_CpfCnpj_Cliente_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ChecagemLabel(chk_CpfCnpj_Cliente.Checked, lb_CpfCnpj_Cliente);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void chk_InscricaoEstadual_Clietne_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ChecagemLabel(chk_InscricaoEstadual_Clietne.Checked, lb_InscricaoEstadual_Clietne);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void chk_Email_Cliente_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ChecagemLabel(chk_Email_Cliente.Checked, lb_Email_Cliente);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void chk_GridEndereco_Cliente_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ChecagemGrid(chk_GridEndereco_Cliente.Checked, dgv_Endereco_Cliente);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void chk_GridTelefone_Cliente_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ChecagemGrid(chk_GridTelefone_Cliente.Checked, dgv_Telefone_Cliente);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #endregion

        #region Informações Gerais

        #region Eventos CheckedChanged

        private void chk_ValorEntrada_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ChecagemLabel(chk_ValorEntrada.Checked, lb_ValorEntrada);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void chk_QuantidadeParcelas_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ChecagemLabel(chk_QuantidadeParcelas.Checked, lb_QuantidadeParcelas);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void chk_ValorParcela_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ChecagemLabel(chk_ValorParcela.Checked, lb_ValorParcela);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void chk_Juros_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ChecagemLabel(chk_Juros.Checked, lb_Juros);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void chk_PrazoEntrega_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ChecagemLabel(chk_PrazoEntrega.Checked, lb_PrazoEntrega);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void chk_DescricaoPagamento_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ChecagemLabel(chk_DescricaoPagamento.Checked, lb_DescricaoPagamento);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void chk_DataEmissao_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ChecagemLabel(chk_DataEmissao.Checked, lb_DataEmissao);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void chk_Validade_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ChecagemLabel(chk_Validade.Checked, lb_Validade);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void chk_Observacao_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ChecagemLabel(chk_Observacao.Checked, lb_Observacao);
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
