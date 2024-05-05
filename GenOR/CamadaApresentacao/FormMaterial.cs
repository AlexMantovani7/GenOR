using GenOR.Properties;
using CamadaObjetoTransferencia;
using CamadaProcessamento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;

namespace GenOR
{
    public partial class FormMaterial : FormBase
    {
        #region Variaveis

        public GerenciarInformacaoRetornadoPesquisa gerenciarInformacaoRetornadoPesquisa;

        private Material material;
        private ListaMaterial listaMaterial;
        private ProcMaterial procMaterial;

        private List<Material> listaPesquisada;

        private GerenciarMensagensPadraoSistema gerenciarMensagensPadraoSistema;

        private GerenciarTelaLoading gerenciarTelaLoading;
        private FormTelaLoading formTelaLoading;
        private Thread carregarThread;
        private bool telaLoadingFoiAberta;

        private string operacaoRegistro;
        private bool encerramentoPadrao;

        private bool janelaModoPesquisa;
        private bool primeiraPesquisaModoPesquisa;
        
        private Material checkLinhasAtivas;
        private static int colunaCodigoIndex = 0;

        private string imagemButtomPesquisa;
        private FormTelaPesquisaMaterial formTelaPesquisaMaterial;

        private string imagemFundoPadrao;
        private OpenFileDialog buscadorImagem_OFD;
        
        private string restaurarImagem;
        
        private string imagemButtomZoom;

        private string arquivoListaParaImpressao;
        private Excel.Application conexaoExcel;
        private Excel.Workbook arquivoExcel;
        private Excel.Worksheet planilhaExcel;

        #endregion

        public FormMaterial(Material objetoPesquisado, bool modoPesquisa)
        {
            try
            {
                InitializeComponent();

                gerenciarInformacaoRetornadoPesquisa = new GerenciarInformacaoRetornadoPesquisa();
                gerenciarInformacaoRetornadoPesquisa.codigoRetornavelPesquisa = 0;

                material = new Material();
                InstanciamentoRapida_Material(material);
                listaMaterial = new ListaMaterial();
                procMaterial = new ProcMaterial();

                listaPesquisada = new ListaMaterial();
                
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
                    AlterarLogo_ModoPesquisa(lb_LogoJanela, "                                     MATERIAL  (MODO PESQUISA)", 18);
                    primeiraPesquisaModoPesquisa = true;

                    btn_RetornarModoPesquisa.Visible = true;
                    this.WindowState = FormWindowState.Normal;
                }
                else
                    primeiraPesquisaModoPesquisa = false;

                if (objetoPesquisado != null)
                    material.codigo = objetoPesquisado.codigo;

                imagemButtomPesquisa = "PESQUISAR";
                formTelaPesquisaMaterial = new FormTelaPesquisaMaterial();

                imagemFundoPadrao = Localizar_Imagem_Documento("Picture.png", true);
                buscadorImagem_OFD = new OpenFileDialog();
                buscadorImagem_OFD.Title = "BUSCANDO IMAGEM DO MATERIAL";
                buscadorImagem_OFD.Filter = "PNG (*.png)|*.png| JPEG (*.jpeg)|*jpeg| JPG (*.jpg)|*jpg| Todos Arquivos(*.*)|*.*";
                buscadorImagem_OFD.FileName = imagemFundoPadrao;
                
                restaurarImagem = null;
                
                imagemButtomZoom = "ZOOM-IN";
                
                pb_ExibindoImagemDGV.Load(imagemFundoPadrao);
                
                arquivoListaParaImpressao = Localizar_Imagem_Documento("LISTAGEM MATERIAL.xlsx", false);
                conexaoExcel = null;
                arquivoExcel = null;
                planilhaExcel = null;

                dgv_Material.AutoGenerateColumns = false;
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
                object sender = new object();
                EventArgs e = new EventArgs();

                txtb_CodigoMaterial.Clear();
                mtxtb_UltimaAtualizacaoMaterial.Clear();

                txtb_DescricaoMaterial.Clear();

                txtb_CodigoFornecedor.Clear();
                txtb_DescricaoFornecedor.Clear();

                txtb_CodigoUnidade.Clear();
                txtb_SiglaUnidade.Clear();
                txtb_DescricaoUnidade.Clear();

                txtb_AlturaMaterial.Clear();
                txtb_AlturaMaterial_Leave(sender, e);
                txtb_LarguraMaterial.Clear();
                txtb_LarguraMaterial_Leave(sender, e);
                txtb_ComprimentoMaterial.Clear();
                txtb_ComprimentoMaterial_Leave(sender, e);

                txtb_CodigoGrupo.Clear();
                txtb_DescricaoGrupo.Clear();

                txtb_ValorUnitarioMaterial.Clear();
                txtb_ValorUnitarioMaterial_Leave(sender, e);

                pb_ImagemMaterial.Load(imagemFundoPadrao);
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
                Material item = new Material
                {
                    ativo_inativo = true
                };
                InstanciamentoRapida_Material(item);

                listaMaterial = procMaterial.ConsultarRegistro(item, true);

                CarregarListaDGV();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void AdicionarLinhasDGV(Material item)
        {
            try
            {
                string valorUnitárioFormatado = Convert.ToString(item.valor_unitario);

                checkLinhasAtivas = item;

                dgv_Material.Rows.Add(item.codigo, DateTime.Parse(item.ultima_atualizacao.ToString()).ToString("dd/MM/yyyy HH:mm"), item.descricao, item.Grupo.descricao,
                    item.Unidade.sigla, item.altura, item.largura, item.comprimento, decimal.Parse(valorUnitárioFormatado).ToString("C2", new CultureInfo("pt-BR")), item.Fornecedor.nome_razao_social);
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
                if (listaMaterial.Count > 0)
                {
                    dgv_Material.Rows.Clear();
                    checkLinhasAtivas = new Material();
                    InstanciamentoRapida_Material(checkLinhasAtivas);

                    foreach (Material item in listaMaterial)
                    {
                        AdicionarLinhasDGV(item);
                    }

                    if (janelaModoPesquisa && primeiraPesquisaModoPesquisa)
                    {
                        int resultado = 0;
                        primeiraPesquisaModoPesquisa = false;

                        Material converter = new Material();
                        InstanciamentoRapida_Material(converter);

                        foreach (Material item in listaMaterial.Where(lm => lm.codigo.Equals(material.codigo)))
                        {
                            resultado = (int)item.codigo;
                            break;
                        }

                        if (!resultado.Equals(0))
                        {
                            converter = listaMaterial.Where(lm => lm.codigo.Equals(material.codigo)).Last();

                            foreach (DataGridViewRow item in dgv_Material.Rows)
                            {
                                if (item.Cells[colunaCodigoIndex].Value.Equals((int)converter.codigo))
                                    dgv_Material.CurrentCell = dgv_Material.Rows[item.Index].Cells[colunaCodigoIndex];
                            }
                        }
                        else
                            dgv_Material.Rows[0].Selected = true;
                    }
                    else
                    {
                        primeiraPesquisaModoPesquisa = false;
                        dgv_Material.Rows[0].Selected = true;
                    }

                    btn_ProdutosServicos_Vinculados.Enabled = true;

                    PreencherCampos();
                }
                else
                {
                    btn_ProdutosServicos_Vinculados.Enabled = false;

                    primeiraPesquisaModoPesquisa = false;

                    dgv_Material.Rows.Clear();

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
                if (dgv_Material.RowCount > 0)
                {
                    telaLoadingFoiAberta = true;
                    gerenciarTelaLoading.Abrir(this, formTelaLoading, carregarThread);

                    int codigoLinhaSelecionada = Convert.ToInt32(dgv_Material.Rows[dgv_Material.CurrentCell.RowIndex].Cells[colunaCodigoIndex].Value);

                    object sender = new object();
                    EventArgs e = new EventArgs();

                    foreach (Material item in listaMaterial.Where(lm => lm.codigo.Equals(codigoLinhaSelecionada)))
                    {
                        txtb_CodigoMaterial.Text = item.codigo.ToString();
                        mtxtb_UltimaAtualizacaoMaterial.Text = DateTime.Parse(item.ultima_atualizacao.ToString()).ToString("dd/MM/yyyy HH:mm");

                        txtb_DescricaoMaterial.Text = item.descricao;

                        txtb_CodigoFornecedor.Text = item.Fornecedor.codigo.ToString();
                        txtb_DescricaoFornecedor.Text = item.Fornecedor.nome_razao_social;
                        if (item.Fornecedor.ativo_inativo.Equals(false) || !item.Fornecedor.tipo_pessoa.Equals("FORNECEDOR"))
                        {
                            PintarBackground_CampoValido_Invalido(txtb_CodigoFornecedor, false, false);
                            PintarBackground_CampoValido_Invalido(txtb_DescricaoFornecedor, false, false);
                        }
                        else
                        {
                            PintarBackground_CampoValido_Invalido(txtb_CodigoFornecedor, true, false);
                            PintarBackground_CampoValido_Invalido(txtb_DescricaoFornecedor, true, false);
                        }

                        txtb_CodigoUnidade.Text = item.Unidade.codigo.ToString();
                        txtb_SiglaUnidade.Text = item.Unidade.sigla;
                        txtb_DescricaoUnidade.Text = item.Unidade.descricao;
                        if (item.Unidade.ativo_inativo.Equals(false))
                        {
                            PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade, false, false);
                            PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade, false, false);
                            PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade, false, false);
                        }
                        else
                        {
                            PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade, true, false);
                            PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade, true, false);
                            PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade, true, false);
                        }

                        txtb_AlturaMaterial.Text = item.altura.ToString();
                        txtb_AlturaMaterial_Leave(sender, e);
                        txtb_LarguraMaterial.Text = item.largura.ToString();
                        txtb_LarguraMaterial_Leave(sender, e);
                        txtb_ComprimentoMaterial.Text = item.comprimento.ToString();
                        txtb_ComprimentoMaterial_Leave(sender, e);

                        txtb_CodigoGrupo.Text = item.Grupo.codigo.ToString();
                        txtb_DescricaoGrupo.Text = item.Grupo.descricao;
                        if (item.Grupo.ativo_inativo.Equals(false) || !item.Grupo.material_ou_produto.Equals('M'))
                        {
                            PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo, false, false);
                            PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo, false, false);
                        }
                        else
                        {
                            PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo, true, false);
                            PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo, true, false);
                        }

                        txtb_ValorUnitarioMaterial.Text = item.valor_unitario.ToString();
                        txtb_ValorUnitarioMaterial_Leave(sender, e);

                        if (File.Exists(item.imagem))
                        {
                            pb_ImagemMaterial.Load(item.imagem);
                            buscadorImagem_OFD.FileName = item.imagem;

                            pb_ExibindoImagemDGV.Load(item.imagem);
                        }
                        else
                        {
                            pb_ImagemMaterial.Load(imagemFundoPadrao);
                            buscadorImagem_OFD.FileName = imagemFundoPadrao;

                            pb_ExibindoImagemDGV.Load(imagemFundoPadrao);

                            item.imagem = imagemFundoPadrao;
                        }

                        if (primeiraPesquisaModoPesquisa.Equals(false))
                            material = item;

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

        private bool ValidandoCompilandoDadosCampos_TbPrincipal(Material validandoMaterial)
        {
            try
            {
                bool dadosValidos = true;
                int variavelSaidaCodigo = 0;
                decimal variavelSaidaCamposDecimais = 0;

                object sender = new object();
                EventArgs e = new EventArgs();

                validandoMaterial.ultima_atualizacao = DateTime.Now;
                validandoMaterial.ativo_inativo = true;

                if (operacaoRegistro.Equals("ALTERAÇÃO") || operacaoRegistro.Equals("INATIVAÇÃO"))
                    validandoMaterial.codigo = int.Parse(txtb_CodigoMaterial.Text);
                else
                    validandoMaterial.codigo = 0;

                if (dadosValidos)
                {
                    if (txtb_DescricaoMaterial.Text.Trim().Equals(""))
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("DESCRIÇÃO DO MATERIAL");
                        dadosValidos = false;
                    }
                    else
                        validandoMaterial.descricao = txtb_DescricaoMaterial.Text;
                }

                if (dadosValidos)
                {
                    if (int.TryParse(txtb_CodigoFornecedor.Text.Trim(), out variavelSaidaCodigo))
                    {
                        if (txtb_CodigoFornecedor.BackColor.Equals(System.Drawing.Color.RosyBrown))
                        {
                            gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("FORNECEDOR");
                            dadosValidos = false;
                        }
                        else
                        {
                            validandoMaterial.Fornecedor.codigo = variavelSaidaCodigo;
                            validandoMaterial.Fornecedor.nome_razao_social = txtb_DescricaoFornecedor.Text;
                            validandoMaterial.Fornecedor.tipo_pessoa = "FORNECEDOR";
                        }
                    }
                    else
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("FORNECEDOR");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    if (int.TryParse(txtb_CodigoUnidade.Text.Trim(), out variavelSaidaCodigo))
                    {
                        if (txtb_CodigoUnidade.BackColor.Equals(System.Drawing.Color.RosyBrown))
                        {
                            gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("UNIDADE");
                            dadosValidos = false;
                        }
                        else
                        {
                            validandoMaterial.Unidade.codigo = variavelSaidaCodigo;
                            validandoMaterial.Unidade.sigla = txtb_SiglaUnidade.Text;
                            validandoMaterial.Unidade.descricao = txtb_DescricaoUnidade.Text;
                        }
                    }
                    else
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("UNIDADE");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    variavelSaidaCamposDecimais = CampoMoeda_RemoverFormatacao(txtb_AlturaMaterial);
                    if (variavelSaidaCamposDecimais >= 0)
                    {
                        validandoMaterial.altura = variavelSaidaCamposDecimais;
                        txtb_AlturaMaterial_Leave(sender, e);
                    }
                    else
                    {
                        txtb_AlturaMaterial_Leave(sender, e);
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("ALTURA");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    variavelSaidaCamposDecimais = CampoMoeda_RemoverFormatacao(txtb_LarguraMaterial);
                    if (variavelSaidaCamposDecimais >= 0)
                    {
                        validandoMaterial.largura = variavelSaidaCamposDecimais;
                        txtb_LarguraMaterial_Leave(sender, e);
                    }
                    else
                    {
                        txtb_LarguraMaterial_Leave(sender, e);
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("LARGURA");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    variavelSaidaCamposDecimais = CampoMoeda_RemoverFormatacao(txtb_ComprimentoMaterial);
                    if (variavelSaidaCamposDecimais >= 0)
                    {
                        validandoMaterial.comprimento = variavelSaidaCamposDecimais;
                        txtb_ComprimentoMaterial_Leave(sender, e);
                    }
                    else
                    {
                        txtb_ComprimentoMaterial_Leave(sender, e);
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("COMPRIMENTO");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    if (int.TryParse(txtb_CodigoGrupo.Text.Trim(), out variavelSaidaCodigo))
                    {
                        if (txtb_CodigoGrupo.BackColor.Equals(System.Drawing.Color.RosyBrown))
                        {
                            gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("GRUPO");
                            dadosValidos = false;
                        }
                        else
                        {
                            validandoMaterial.Grupo.codigo = variavelSaidaCodigo;
                            validandoMaterial.Grupo.descricao = txtb_DescricaoGrupo.Text;
                            validandoMaterial.Grupo.material_ou_produto = 'M';
                        }
                    }
                    else
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("GRUPO");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    variavelSaidaCamposDecimais = CampoMoeda_RemoverFormatacao(txtb_ValorUnitarioMaterial);
                    if (variavelSaidaCamposDecimais > 0)
                    {
                        validandoMaterial.valor_unitario = variavelSaidaCamposDecimais;
                        txtb_ValorUnitarioMaterial_Leave(sender, e);
                    }
                    else
                    {
                        txtb_ValorUnitarioMaterial_Leave(sender, e);
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("VALOR UNITÁRIO");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    if (buscadorImagem_OFD.CheckFileExists)
                    {
                        string nomeImagem = Path.GetFileName(buscadorImagem_OFD.FileName);

                        if (nomeImagem.Equals("Picture.png")
                            || (nomeImagem.Equals(Path.GetFileName(material.imagem)) && (operacaoRegistro.Equals("ALTERAÇÃO") || operacaoRegistro.Equals("INATIVAÇÃO"))))
                        {
                            validandoMaterial.imagem = buscadorImagem_OFD.FileName;
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
                                    validandoMaterial.imagem = novoDiretorioImagem.FileName;
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
                {
                    dadosValidos = ValidandoCadastroRegistroDuplicado_TbPrincipal(validandoMaterial);
                }

                return dadosValidos;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
                return false;
            }
        }

        private bool ValidandoCadastroRegistroDuplicado_TbPrincipal(Material validandoMaterial)
        {
            try
            {
                bool dadosValidos = true;

                Material m = new Material
                {
                    ativo_inativo = true
                };
                InstanciamentoRapida_Material(m);

                ListaMaterial listaM = procMaterial.ConsultarRegistro(m, true);

                foreach (Material item in listaM)
                {
                    if (item.descricao.Equals(validandoMaterial.descricao) && item.ativo_inativo.Equals(true))
                    {
                        if (operacaoRegistro.Equals("CADASTRO"))
                        {
                            dadosValidos = false;
                            break;
                        }
                        else if (operacaoRegistro.Equals("ALTERAÇÃO"))
                        {
                            if (!item.codigo.Equals(validandoMaterial.codigo))
                            {
                                dadosValidos = false;
                                break;
                            }
                        }
                    }
                }

                if (dadosValidos.Equals(false))
                    gerenciarMensagensPadraoSistema.RegistroDuplicado("Esse Material", "Código: " + validandoMaterial.codigo + " - " + validandoMaterial.descricao);

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
                List<Material> listaImpressao = new ListaMaterial();
                
                if (imagemButtomPesquisa.Equals("PESQUISAR"))
                    listaImpressao = listaMaterial;
                else
                    listaImpressao = listaPesquisada;

                conexaoExcel = new Excel.Application();
                arquivoExcel = conexaoExcel.Workbooks.Open(patchTabela);
                planilhaExcel = arquivoExcel.Worksheets[1] as Excel.Worksheet;

                int linha = 2;
                foreach (Material item in listaImpressao)
                {
                    int coluna = 1;

                    planilhaExcel.Cells[linha, coluna++].Value = item.codigo.ToString();
                    planilhaExcel.Cells[linha, coluna++].Value = item.ultima_atualizacao.ToString();
                    planilhaExcel.Cells[linha, coluna++].Value = item.descricao.ToString();
                    planilhaExcel.Cells[linha, coluna++].Value = "(" + item.Grupo.codigo.ToString() + ") " + item.Grupo.descricao.ToString();
                    planilhaExcel.Cells[linha, coluna++].Value = "(" + item.Unidade.codigo.ToString() + ") ( " + item.Unidade.sigla.ToString() + " ) - " + item.Unidade.descricao.ToString();
                    planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.valor_unitario.ToString()).ToString("C2", new CultureInfo("pt-BR"));
                    planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.altura.ToString()).ToString("N2", new CultureInfo("pt-BR"));
                    planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.largura.ToString()).ToString("N2", new CultureInfo("pt-BR"));
                    planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.comprimento.ToString()).ToString("N2", new CultureInfo("pt-BR"));
                    planilhaExcel.Cells[linha, coluna++].Value = "(" + item.Fornecedor.codigo.ToString() + ") " + item.Fornecedor.nome_razao_social.ToString();

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

        private void AlterarValores_MateriaisDoProduto_Servico()
        {
            try
            {
                Materiais_Produto_Servico materiais_Produto_Servico = new Materiais_Produto_Servico();
                InstanciamentoRapida_Materiais_ProdutoServico(materiais_Produto_Servico);

                ProcMateriais_Produto_Servico procMateriais_Produto_Servico = new ProcMateriais_Produto_Servico();

                materiais_Produto_Servico.ativo_inativo = true;
                List<Materiais_Produto_Servico> listaMateriais_Produto_Servicos = procMateriais_Produto_Servico.ConsultarRegistro(materiais_Produto_Servico, true);
                listaMateriais_Produto_Servicos = listaMateriais_Produto_Servicos.Where(lmps => lmps.Material.codigo.Equals(material.codigo) && lmps.ativo_inativo.Equals(true)).ToList();

                if (listaMateriais_Produto_Servicos.Count > 0)
                {
                    foreach (Materiais_Produto_Servico item in listaMateriais_Produto_Servicos)
                    {
                        item.valor_total = item.quantidade * material.valor_unitario;
                        procMateriais_Produto_Servico.ManterRegistro(item, operacaoRegistro);
                    }
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Form

        private void FormMaterial_Load(object sender, EventArgs e)
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

        private void FormMaterial_FormClosing(object sender, FormClosingEventArgs e)
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

        #region Eventos Enter

        private void txtb_AlturaMaterial_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_AlturaMaterial);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_LarguraMaterial_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_LarguraMaterial);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ComprimentoMaterial_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_ComprimentoMaterial);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ValorUnitarioMaterial_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_ValorUnitarioMaterial);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos KeyPress

        private void txtb_DescricaoMaterial_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Enter_FocusTxtb(txtb_CodigoFornecedor, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_CodigoFornecedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_CodigoUnidade, e))
                    CampoMoeda_KeyPress(sender, e, true);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_CodigoUnidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_AlturaMaterial, e))
                    CampoMoeda_KeyPress(sender, e, true);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_AlturaMaterial_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_LarguraMaterial, e))
                    CampoMoeda_KeyPress(sender, e, false);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_LarguraMaterial_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_ComprimentoMaterial, e))
                    CampoMoeda_KeyPress(sender, e, false);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ComprimentoMaterial_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_CodigoGrupo, e))
                    CampoMoeda_KeyPress(sender, e, false);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_CodigoGrupo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_ValorUnitarioMaterial, e))
                    CampoMoeda_KeyPress(sender, e, true);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ValorUnitarioMaterial_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtb_CodigoFornecedor_Leave(object sender, EventArgs e)
        {
            try
            {
                telaLoadingFoiAberta = true;
                gerenciarTelaLoading.Abrir(this, formTelaLoading, carregarThread);

                int codigoRetorno = 0;
                if (int.TryParse(txtb_CodigoFornecedor.Text, out codigoRetorno) && codigoRetorno > 0)
                {
                    Pessoa obj = new Pessoa
                    {
                        codigo = codigoRetorno,
                        tipo_pessoa = "FORNECEDOR",
                        ativo_inativo = true
                    };

                    ProcPessoa procFornecedor  = new ProcPessoa();
                    
                    ListaPessoa listaFornecedor = new ListaPessoa();
                    listaFornecedor = procFornecedor.ConsultarRegistro(obj, false);

                    if (listaFornecedor.Count >  0)
                    {
                        foreach (Pessoa item in listaFornecedor)
                        {
                            if (item.codigo.Equals(codigoRetorno) && item.tipo_pessoa.Equals("FORNECEDOR") && item.ativo_inativo.Equals(true))
                            {
                                txtb_CodigoFornecedor.Text = item.codigo.ToString();
                                txtb_DescricaoFornecedor.Text = item.nome_razao_social;

                                PintarBackground_CampoValido_Invalido(txtb_CodigoFornecedor, true, false);
                                PintarBackground_CampoValido_Invalido(txtb_DescricaoFornecedor, true, false);

                                break;
                            }
                            else
                            {
                                telaLoadingFoiAberta = false;
                                gerenciarTelaLoading.Fechar();

                                gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CÓDIGO FORNECEDOR");
                                PintarBackground_CampoValido_Invalido(txtb_CodigoFornecedor, false, false);
                                PintarBackground_CampoValido_Invalido(txtb_DescricaoFornecedor, false, true);

                                break;
                            }
                        }
                    }
                    else
                    {
                        telaLoadingFoiAberta = false;
                        gerenciarTelaLoading.Fechar();

                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CÓDIGO FORNECEDOR");
                        PintarBackground_CampoValido_Invalido(txtb_CodigoFornecedor, false, false);
                        PintarBackground_CampoValido_Invalido(txtb_DescricaoFornecedor, false, true);
                    }
                }
                else
                {
                    telaLoadingFoiAberta = false;
                    gerenciarTelaLoading.Fechar();

                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CÓDIGO FORNECEDOR");
                    PintarBackground_CampoValido_Invalido(txtb_CodigoFornecedor, false, false);
                    PintarBackground_CampoValido_Invalido(txtb_DescricaoFornecedor, false, true);
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

        private void txtb_CodigoUnidade_Leave(object sender, EventArgs e)
        {
            try
            {
                telaLoadingFoiAberta = true;
                gerenciarTelaLoading.Abrir(this, formTelaLoading, carregarThread);

                int codigoRetorno = 0;
                if (int.TryParse(txtb_CodigoUnidade.Text, out codigoRetorno) && codigoRetorno > 0)
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
                                txtb_CodigoUnidade.Text = item.codigo.ToString();
                                txtb_SiglaUnidade.Text = item.sigla;
                                txtb_DescricaoUnidade.Text = item.descricao;

                                PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade, true, false);
                                PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade, true, false);
                                PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade, true, false);

                                break;
                            }
                            else
                            {
                                telaLoadingFoiAberta = false;
                                gerenciarTelaLoading.Fechar();

                                gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CÓDIGO UNIDADE");
                                PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade, false, false);
                                PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade, false, true);
                                PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade, false, true);

                                break;
                            }
                        }
                    }
                    else
                    {
                        telaLoadingFoiAberta = false;
                        gerenciarTelaLoading.Fechar();

                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CÓDIGO UNIDADE");
                        PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade, false, false);
                        PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade, false, true);
                        PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade, false, true);
                    }
                }
                else
                {
                    telaLoadingFoiAberta = false;
                    gerenciarTelaLoading.Fechar();

                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CÓDIGO UNIDADE");
                    PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade, false, false);
                    PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade, false, true);
                    PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade, false, true);
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

        private void txtb_AlturaMaterial_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_AlturaMaterial, false);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_LarguraMaterial_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_LarguraMaterial, false);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ComprimentoMaterial_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ComprimentoMaterial, false);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_CodigoGrupo_Leave(object sender, EventArgs e)
        {
            try
            {
                telaLoadingFoiAberta = true;
                gerenciarTelaLoading.Abrir(this, formTelaLoading, carregarThread);

                int codigoRetorno = 0;
                if (int.TryParse(txtb_CodigoGrupo.Text, out codigoRetorno) && codigoRetorno > 0)
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
                                txtb_CodigoGrupo.Text = item.codigo.ToString();
                                txtb_DescricaoGrupo.Text = item.descricao;

                                PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo, true, false);
                                PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo, true, false);

                                break;
                            }
                            else
                            {
                                telaLoadingFoiAberta = false;
                                gerenciarTelaLoading.Fechar();

                                gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CÓDIGO GRUPO");
                                PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo, false, false);
                                PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo, false, true);

                                break;
                            }
                        }
                    }
                    else
                    {
                        telaLoadingFoiAberta = false;
                        gerenciarTelaLoading.Fechar();

                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CÓDIGO GRUPO");
                        PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo, false, false);
                        PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo, false, true);
                    }
                }
                else
                {
                    telaLoadingFoiAberta = false;
                    gerenciarTelaLoading.Fechar();

                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CÓDIGO GRUPO");
                    PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo, false, false);
                    PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo, false, true);
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

        private void txtb_ValorUnitarioMaterial_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorUnitarioMaterial, true);
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

                btn_ProdutosServicos_Vinculados.Enabled = false;

                Formulario_ModoModificacao(gb_ListagemGeral, gb_DadosGerais, tbc_Principal, tbc_Secundario, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar,
                    btn_DadosGerais_Deletar, btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                operacaoRegistro = "CADASTRO";
                pb_ImagemMaterial.Load(imagemFundoPadrao);
                buscadorImagem_OFD.FileName = imagemFundoPadrao;
                LimparCamposParaCadastro();
                
                txtb_DescricaoMaterial.Focus();

                PintarBackground_CampoValido_Invalido(txtb_CodigoFornecedor, true, true);
                PintarBackground_CampoValido_Invalido(txtb_DescricaoFornecedor, true, true);

                PintarBackground_CampoValido_Invalido(txtb_CodigoUnidade, true, true);
                PintarBackground_CampoValido_Invalido(txtb_SiglaUnidade, true, true);
                PintarBackground_CampoValido_Invalido(txtb_DescricaoUnidade, true, true);

                PintarBackground_CampoValido_Invalido(txtb_CodigoGrupo, true, true);
                PintarBackground_CampoValido_Invalido(txtb_DescricaoGrupo, true, true);

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

                btn_ProdutosServicos_Vinculados.Enabled = false;

                Formulario_ModoModificacao(gb_ListagemGeral, gb_DadosGerais, tbc_Principal, tbc_Secundario, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar,
                    btn_DadosGerais_Deletar, btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                operacaoRegistro = "ALTERAÇÃO";
                buscadorImagem_OFD.FileName = pb_ImagemMaterial.ImageLocation;
                txtb_DescricaoMaterial.Focus();

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
                if (dgv_Material.CurrentRow.Index > 0)
                {
                    int linha = MudarLinha_DGV(dgv_Material, false);

                    dgv_Material.CurrentCell = dgv_Material.Rows[linha].Cells[dgv_Material.CurrentCell.ColumnIndex];
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
                if (dgv_Material.CurrentRow.Index < (dgv_Material.RowCount - 1))
                {
                    int linha = MudarLinha_DGV(dgv_Material, true);

                    dgv_Material.CurrentCell = dgv_Material.Rows[linha].Cells[dgv_Material.CurrentCell.ColumnIndex];
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
                    Material objetoChecagem = new Material();
                    InstanciamentoRapida_Material(objetoChecagem);

                    string statusOperacao = "EM ANDAMENTO";

                    if (ValidandoCompilandoDadosCampos_TbPrincipal(objetoChecagem))
                    {
                        if (operacaoRegistro.Equals("CADASTRO") || operacaoRegistro.Equals("ALTERAÇÃO") || operacaoRegistro.Equals("INATIVAÇÃO"))
                            statusOperacao = procMaterial.ManterRegistro(objetoChecagem, operacaoRegistro);
                        else
                        {
                            gerenciarMensagensPadraoSistema.ExceptionBancoDados("A operação informada não existe !");
                            return;
                        }

                        int codigoRegistroRetornado = 0;
                        if (int.TryParse(statusOperacao, out codigoRegistroRetornado))
                        {
                            string imagemAntiga = material.imagem;

                            material = objetoChecagem;
                            material.codigo = codigoRegistroRetornado;

                            if (operacaoRegistro.Equals("CADASTRO"))
                            {
                                if (!material.imagem.Equals(imagemFundoPadrao))
                                    File.Copy(buscadorImagem_OFD.FileName, material.imagem, false);
                            }
                            else if (operacaoRegistro.Equals("ALTERAÇÃO"))
                            {
                                if (!imagemAntiga.Equals(imagemFundoPadrao) && !material.imagem.Equals(imagemAntiga))
                                {
                                    pb_ExibindoImagemDGV.Load(imagemFundoPadrao);
                                    pb_ImagemMaterial.Load(imagemFundoPadrao);

                                    File.Delete(imagemAntiga);
                                    File.Copy(buscadorImagem_OFD.FileName, material.imagem, false);
                                }
                                else if (imagemAntiga.Equals(imagemFundoPadrao) && !material.imagem.Equals(imagemAntiga))
                                    File.Copy(buscadorImagem_OFD.FileName, material.imagem, false);

                                AlterarValores_MateriaisDoProduto_Servico();
                            }
                            else if (operacaoRegistro.Equals("INATIVAÇÃO"))
                            {
                                if (!imagemAntiga.Equals(imagemFundoPadrao))
                                {
                                    pb_ExibindoImagemDGV.Load(imagemFundoPadrao);
                                    pb_ImagemMaterial.Load(imagemFundoPadrao);

                                    File.Delete(imagemAntiga);
                                }

                                AlterarValores_MateriaisDoProduto_Servico();
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

                            Formulario_ModoConsulta(gb_ListagemGeral, gb_DadosGerais, dgv_Material, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar, btn_DadosGerais_Deletar,
                                btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                            btn_ReverterImagem_Material.Visible = false;

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
                    Formulario_ModoConsulta(gb_ListagemGeral, gb_DadosGerais, dgv_Material, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar, btn_DadosGerais_Deletar,
                        btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);
                    
                    btn_ReverterImagem_Material.Visible = false;

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

        private void btn_AdicionarImagem_Material_Click(object sender, EventArgs e)
        {
            try
            {
                string preRestauracao_Imagem = buscadorImagem_OFD.FileName;

                if (buscadorImagem_OFD.ShowDialog().Equals(DialogResult.OK))
                {
                    string imagemPesquisada_OFD = Path.GetExtension(buscadorImagem_OFD.FileName);

                    if (ValidarImagem_OpenFile(imagemPesquisada_OFD))
                    {
                        pb_ImagemMaterial.Load(buscadorImagem_OFD.FileName);
                        btn_ReverterImagem_Material.Visible = true;
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

        private void btn_ZoomImagem_Material_Click(object sender, EventArgs e)
        {
            try
            {
                imagemButtomZoom = Imagem_ZoomIn_ZoomOut(imagemButtomZoom, btn_ZoomImagem_Material, btn_ReverterImagem_Material, pb_ImagemMaterial);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_ReverterImagem_Material_Click(object sender, EventArgs e)
        {
            try
            {
                if (gerenciarMensagensPadraoSistema.Mensagem_Confirmacao("RETORNANDO A IMAGEM ANTERIOR").Equals(DialogResult.OK))
                {
                    pb_ImagemMaterial.Load(restaurarImagem);
                    buscadorImagem_OFD.FileName = restaurarImagem;
                    btn_ReverterImagem_Material.Visible = false;
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
                    formTelaPesquisaMaterial = new FormTelaPesquisaMaterial();
                    formTelaPesquisaMaterial.ShowDialog();

                    if (!formTelaPesquisaMaterial.campoPesquisado.Equals("CANCELADO") && !formTelaPesquisaMaterial.informaçãoRetornada.Equals("VAZIA"))
                    {
                        listaPesquisada = new ListaMaterial();

                        if (formTelaPesquisaMaterial.campoPesquisado.Equals("CÓDIGO"))
                        {
                            listaPesquisada = listaMaterial.Where(lm => lm.codigo.Equals(int.Parse(formTelaPesquisaMaterial.informaçãoRetornada))).ToList();
                        }
                        else if (formTelaPesquisaMaterial.campoPesquisado.Equals("ULTIMA ATUALIZAÇÃO") && !formTelaPesquisaMaterial.informaçãoRetornada2.Equals("VAZIA"))
                        {
                            listaPesquisada = listaMaterial.Where(lm => DateTime.Parse(lm.ultima_atualizacao.ToString()).Date >= DateTime.Parse(formTelaPesquisaMaterial.informaçãoRetornada).Date
                            && DateTime.Parse(lm.ultima_atualizacao.ToString()).Date <= DateTime.Parse(formTelaPesquisaMaterial.informaçãoRetornada2).Date).ToList();
                        }
                        else if (formTelaPesquisaMaterial.campoPesquisado.Equals("DESCRIÇÃO"))
                        {
                            listaPesquisada = listaMaterial.Where(lm => lm.descricao.Contains(formTelaPesquisaMaterial.informaçãoRetornada)).ToList();
                        }
                        else if (formTelaPesquisaMaterial.campoPesquisado.Equals("GRUPO"))
                        {
                            listaPesquisada = listaMaterial.Where(lm => lm.Grupo.descricao.Contains(formTelaPesquisaMaterial.informaçãoRetornada)).ToList();
                        }
                        else if (formTelaPesquisaMaterial.campoPesquisado.Equals("UNIDADE"))
                        {
                            listaPesquisada = listaMaterial.Where(lm => lm.Unidade.sigla.Contains(formTelaPesquisaMaterial.informaçãoRetornada)).ToList();
                        }
                        else if (formTelaPesquisaMaterial.campoPesquisado.Equals("LARGURA") && !formTelaPesquisaMaterial.informaçãoRetornada2.Equals("VAZIA"))
                        {
                            listaPesquisada = listaMaterial.Where(lm => decimal.Parse(lm.largura.ToString()) >= decimal.Parse(formTelaPesquisaMaterial.informaçãoRetornada)
                            && decimal.Parse(lm.largura.ToString()) <= decimal.Parse(formTelaPesquisaMaterial.informaçãoRetornada2)).ToList();
                        }
                        else if (formTelaPesquisaMaterial.campoPesquisado.Equals("ALTURA") && !formTelaPesquisaMaterial.informaçãoRetornada2.Equals("VAZIA"))
                        {
                            listaPesquisada = listaMaterial.Where(lm => decimal.Parse(lm.altura.ToString()) >= decimal.Parse(formTelaPesquisaMaterial.informaçãoRetornada)
                            && decimal.Parse(lm.altura.ToString()) <= decimal.Parse(formTelaPesquisaMaterial.informaçãoRetornada2)).ToList();
                        }
                        else if (formTelaPesquisaMaterial.campoPesquisado.Equals("COMPRIMENTO") && !formTelaPesquisaMaterial.informaçãoRetornada2.Equals("VAZIA"))
                        {
                            listaPesquisada = listaMaterial.Where(lm => decimal.Parse(lm.comprimento.ToString()) >= decimal.Parse(formTelaPesquisaMaterial.informaçãoRetornada)
                            && decimal.Parse(lm.comprimento.ToString()) <= decimal.Parse(formTelaPesquisaMaterial.informaçãoRetornada2)).ToList();
                        }
                        else if (formTelaPesquisaMaterial.campoPesquisado.Equals("VALOR UNITÁRIO") && !formTelaPesquisaMaterial.informaçãoRetornada2.Equals("VAZIA"))
                        {
                            listaPesquisada = listaMaterial.Where(lm => decimal.Parse(lm.valor_unitario.ToString()) >= decimal.Parse(formTelaPesquisaMaterial.informaçãoRetornada)
                            && decimal.Parse(lm.valor_unitario.ToString()) <= decimal.Parse(formTelaPesquisaMaterial.informaçãoRetornada2)).ToList();
                        }
                        else if (formTelaPesquisaMaterial.campoPesquisado.Equals("FORNECEDOR"))
                        {
                            listaPesquisada = listaMaterial.Where(lm => lm.Fornecedor.nome_razao_social.Contains(formTelaPesquisaMaterial.informaçãoRetornada)).ToList();
                        }
                        else
                        {
                            gerenciarMensagensPadraoSistema.InformacaoPesquisadaNaoEncontrada();
                            return;
                        }

                        if (listaPesquisada.Count() > 0)
                        {
                            dgv_Material.Rows.Clear();

                            foreach (Material item in listaPesquisada)
                            {
                                AdicionarLinhasDGV(item);
                            }

                            dgv_Material.Rows[0].Selected = true;
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
                if (listaMaterial.Count > 0)
                {
                    if (!dgv_Material.Rows[dgv_Material.CurrentRow.Index].DefaultCellStyle.BackColor.Equals(System.Drawing.Color.RosyBrown))
                    {
                        if (gerenciarMensagensPadraoSistema.Mensagem_Confirmacao("RETORNAR LINHA SELECIONADA").Equals(DialogResult.OK))
                        {
                            gerenciarInformacaoRetornadoPesquisa.codigoRetornavelPesquisa = (int)material.codigo;

                            encerramentoPadrao = false;
                            this.Close();
                        }
                    }
                    else
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("QUE ESTÁ SELECIONADO NA LISTA DE MATERIAIS");
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

        private void btn_PesquisarFornecedor_Click(object sender, EventArgs e)
        {
            try
            {
                Pessoa objetoPesquisado = new Pessoa();

                int result;
                if (txtb_CodigoFornecedor.BackColor.Equals(System.Drawing.Color.RosyBrown) || txtb_CodigoFornecedor.Text.Trim().Equals("")
                    || ((int.TryParse(txtb_CodigoFornecedor.Text, out result)) && result <= 0) )
                {
                    objetoPesquisado.codigo = 0;
                }
                else
                    objetoPesquisado.codigo = int.Parse(txtb_CodigoFornecedor.Text);

                using (FormPessoa formFornecedor = new FormPessoa(objetoPesquisado, "FORNECEDOR", true))
                {
                    formFornecedor.ShowDialog();

                    gerenciarInformacaoRetornadoPesquisa = formFornecedor.gerenciarInformacaoRetornadoPesquisa;
                    if (!gerenciarInformacaoRetornadoPesquisa.codigoRetornavelPesquisa.Equals(0))
                    {
                        txtb_CodigoFornecedor.Text = gerenciarInformacaoRetornadoPesquisa.codigoRetornavelPesquisa.ToString();
                        txtb_CodigoFornecedor_Leave(sender, e);
                    }
                    else if (!txtb_CodigoFornecedor.Text.Trim().Equals("") && (int.TryParse(txtb_CodigoFornecedor.Text, out result)) && result > 0)
                        txtb_CodigoFornecedor_Leave(sender, e);

                    txtb_CodigoUnidade.Focus();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_PesquisarUnidade_Click(object sender, EventArgs e)
        {
            try
            {
                Grupo_Unidade objetoPesquisado = new Grupo_Unidade();

                int result;
                if (txtb_CodigoUnidade.BackColor.Equals(System.Drawing.Color.RosyBrown) || txtb_CodigoUnidade.Text.Trim().Equals("")
                    || ((int.TryParse(txtb_CodigoUnidade.Text, out result)) && result <= 0) )
                {
                    objetoPesquisado.codigo = 0;
                }
                else
                    objetoPesquisado.codigo = int.Parse(txtb_CodigoUnidade.Text);

                using (FormGrupo_Unidade formUnidade = new FormGrupo_Unidade(objetoPesquisado, true, "UNIDADE", 'M'))
                {
                    formUnidade.ShowDialog();

                    gerenciarInformacaoRetornadoPesquisa = formUnidade.gerenciarInformacaoRetornadoPesquisa;
                    if (!gerenciarInformacaoRetornadoPesquisa.codigoRetornavelPesquisa.Equals(0))
                    {
                        txtb_CodigoUnidade.Text = gerenciarInformacaoRetornadoPesquisa.codigoRetornavelPesquisa.ToString();
                        txtb_CodigoUnidade_Leave(sender, e);
                    }
                    else if (!txtb_CodigoUnidade.Text.Trim().Equals("") && (int.TryParse(txtb_CodigoUnidade.Text, out result)) && result > 0)
                        txtb_CodigoUnidade_Leave(sender, e);

                    txtb_AlturaMaterial.Focus();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_PesquisarGrupo_Click(object sender, EventArgs e)
        {
            try
            {
                Grupo_Unidade objetoPesquisado = new Grupo_Unidade();

                int result;
                if (txtb_CodigoGrupo.BackColor.Equals(System.Drawing.Color.RosyBrown) || txtb_CodigoGrupo.Text.Trim().Equals("")
                    || ((int.TryParse(txtb_CodigoGrupo.Text, out result)) && result <= 0))
                {
                    objetoPesquisado.codigo = 0;
                }
                else
                    objetoPesquisado.codigo = int.Parse(txtb_CodigoGrupo.Text);

                using (FormGrupo_Unidade formGrupo = new FormGrupo_Unidade(objetoPesquisado, true, "GRUPO", 'M'))
                {
                    formGrupo.ShowDialog();

                    gerenciarInformacaoRetornadoPesquisa = formGrupo.gerenciarInformacaoRetornadoPesquisa;
                    if (!gerenciarInformacaoRetornadoPesquisa.codigoRetornavelPesquisa.Equals(0))
                    {
                        txtb_CodigoGrupo.Text = gerenciarInformacaoRetornadoPesquisa.codigoRetornavelPesquisa.ToString();
                        txtb_CodigoGrupo_Leave(sender, e);
                    }
                    else if (!txtb_CodigoGrupo.Text.Trim().Equals("") && (int.TryParse(txtb_CodigoGrupo.Text, out result)) && result > 0)
                        txtb_CodigoGrupo_Leave(sender, e);

                    txtb_ValorUnitarioMaterial.Focus();
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
                using (FormProdutosServicos_Vinculados_Material formProdutosServicos_Vinculados_Material = new FormProdutosServicos_Vinculados_Material(material, this))
                {
                    formProdutosServicos_Vinculados_Material.ShowDialog();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Data Grid View
        
        private void dgv_Material_KeyUp(object sender, KeyEventArgs e)
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

        private void dgv_Material_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                var linhaClicada = dgv_Material.HitTest(e.X, e.Y);
                if (linhaClicada != null && linhaClicada.RowIndex >= 0)
                {
                    if (e.Button.Equals(MouseButtons.Right))
                    {
                        Point localizacaoInicial = new Point(e.Location.X, e.Location.Y);

                        if (linhaClicada.RowIndex.Equals(dgv_Material.CurrentRow.Index))
                        {
                            if (pb_ExibindoImagemDGV.Visible.Equals(true))
                            {
                                pb_ExibindoImagemDGV.Visible = false;
                                btn_FecharImagemDGV.Visible = false;
                            }
                            else
                            {
                                ImagemDGV_RightClick(pb_ExibindoImagemDGV, btn_FecharImagemDGV, pb_ImagemMaterial, localizacaoInicial);
                                pb_ExibindoImagemDGV.Visible = true;
                                btn_FecharImagemDGV.Visible = true;
                                btn_FecharImagemDGV.BringToFront();
                            }
                        }
                        else
                        {
                            dgv_Material.CurrentCell = dgv_Material.Rows[linhaClicada.RowIndex].Cells[0];

                            ImagemDGV_RightClick(pb_ExibindoImagemDGV, btn_FecharImagemDGV, pb_ImagemMaterial, localizacaoInicial);
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

        private void dgv_Material_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                var linhaClicada = dgv_Material.HitTest(e.X, e.Y);
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

        private void dgv_Material_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                if (checkLinhasAtivas.Unidade.ativo_inativo.Equals(false)
                    || checkLinhasAtivas.Grupo.ativo_inativo.Equals(false) || !checkLinhasAtivas.Grupo.material_ou_produto.Equals('M')
                    || checkLinhasAtivas.Fornecedor.ativo_inativo.Equals(false) || !checkLinhasAtivas.Fornecedor.tipo_pessoa.Equals("FORNECEDOR"))
                {
                    dgv_Material.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.RosyBrown;
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
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_1, "texto");
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
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_2, "numerico");
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
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_3, "numerico");
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void lb_Obrigatorio_4_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_4, "numerico");
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void lb_Obrigatorio_5_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_5, "numerico");
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

    }
}
