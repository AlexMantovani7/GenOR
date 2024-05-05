using CamadaObjetoTransferencia;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using CamadaProcessamento;
using System.Globalization;
using System.Diagnostics;
using System.IO;

namespace GenOR
{
    public partial class FormMateriaisOuProdutos_Vinculados_GrupoUnidade : FormBase
    {
        #region Variaveis

        private Grupo_Unidade grupo_Unidade;

        private ListaMaterial listaMaterial;
        private ListaProduto_Servico listaProduto_Servico;

        private string materialOuProduto;
        private string formularioDoGrupo_Ou_Unidade;

        private Form form;

        private GerenciarMensagensPadraoSistema gerenciarMensagensPadraoSistema;

        private GerenciarTelaLoading gerenciarTelaLoading;
        private FormTelaLoading formTelaLoading;
        private Thread carregarThread;
        private bool telaLoadingFoiAberta;

        private string arquivoListaParaImpressao;
        private Excel.Application conexaoExcel;
        private Excel.Workbook arquivoExcel;
        private Excel.Worksheet planilhaExcel;

        #endregion

        public FormMateriaisOuProdutos_Vinculados_GrupoUnidade(Grupo_Unidade grupo_UnidadePesquisado, string materialOuProduto, string formularioDoGrupo_Ou_Unidade, Form formPesquisado)
        {
            try
            {
                InitializeComponent();

                grupo_Unidade = new Grupo_Unidade();
                grupo_Unidade = grupo_UnidadePesquisado;

                listaMaterial = new ListaMaterial();
                listaProduto_Servico = new ListaProduto_Servico();

                this.materialOuProduto = materialOuProduto;
                this.formularioDoGrupo_Ou_Unidade = formularioDoGrupo_Ou_Unidade;

                form = formPesquisado;

                gerenciarMensagensPadraoSistema = new GerenciarMensagensPadraoSistema();

                gerenciarTelaLoading = new GerenciarTelaLoading();
                formTelaLoading = new FormTelaLoading();
                carregarThread = null;
                telaLoadingFoiAberta = false;

                arquivoListaParaImpressao = Localizar_Imagem_Documento("LISTAGEM MATERIAIS OU PRODUTOS VINCULADOS AO GRUPO OU UNIDADE.xlsx", false);
                conexaoExcel = null;
                arquivoExcel = null;
                planilhaExcel = null;

                dgv_MateriaisOuProdutosServicos.AutoGenerateColumns = false;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
                this.Close();
            }
        }

        #region Eventos Form

        private void FormMateriaisOuProdutos_Vinculados_Load(object sender, EventArgs e)
        {
            try
            {
                this.StartPosition = FormStartPosition.Manual;

                string siglaDaUnidade = "";
                if (formularioDoGrupo_Ou_Unidade.Equals("UNIDADE"))
                    siglaDaUnidade = grupo_Unidade.sigla.ToString();
                
                this.Text = materialOuProduto + " VINCULADOS AO " + formularioDoGrupo_Ou_Unidade + " (" + grupo_Unidade.codigo.ToString() + ") - ( " + siglaDaUnidade + " ) - " + grupo_Unidade.descricao.ToString();

                this.Size = new Size(Convert.ToInt32(form.Size.Width - (form.Size.Width / 30)), Convert.ToInt32(form.Size.Height - (form.Size.Height / 16)));
                this.Location = new System.Drawing.Point(Convert.ToInt32(form.Location.X + 30), Convert.ToInt32(form.Location.Y + 30));

                ObterListagemTodosRegistrosDGV();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Funções Gerais

        private void ObterListagemTodosRegistrosDGV()
        {
            try
            {
                telaLoadingFoiAberta = true;
                gerenciarTelaLoading.Abrir(this, formTelaLoading, carregarThread);
                
                if (materialOuProduto.Equals("MATERIAL"))
                {
                    ProcMaterial procMaterial = new ProcMaterial();
                    
                    Material item = new Material
                    {
                        ativo_inativo = true
                    };
                    InstanciamentoRapida_Material(item);

                    ListaMaterial listaM;
                    
                    if (formularioDoGrupo_Ou_Unidade.Equals("GRUPO"))
                    {
                        item.Grupo = grupo_Unidade;
                        listaM = procMaterial.ConsultarRegistro(item, true);
                        
                        foreach (Material m in listaM.Where(m => m.Grupo.codigo.Equals(grupo_Unidade.codigo)))
                        {
                            listaMaterial.Add(m);
                        }
                    }
                    if (formularioDoGrupo_Ou_Unidade.Equals("UNIDADE"))
                    {
                        item.Unidade = grupo_Unidade;
                        listaM = procMaterial.ConsultarRegistro(item, true);

                        foreach (Material m in listaM.Where(m => m.Unidade.codigo.Equals(grupo_Unidade.codigo)))
                        {
                            listaMaterial.Add(m);
                        }
                    }
                }
                else if (materialOuProduto.Equals("PRODUTO"))
                {
                    ProcProduto_Servico procProduto_Servico = new ProcProduto_Servico();

                    Produto_Servico item = new Produto_Servico
                    {
                        ativo_inativo = true
                    };
                    InstanciamentoRapida_ProdutoServico(item);

                    ListaProduto_Servico listaPS;

                    if (formularioDoGrupo_Ou_Unidade.Equals("GRUPO"))
                    {
                        item.Grupo = grupo_Unidade;
                        listaPS = procProduto_Servico.ConsultarRegistro(item, true);

                        foreach (Produto_Servico ps in listaPS.Where(ps => ps.Grupo.codigo.Equals(grupo_Unidade.codigo)))
                        {
                            listaProduto_Servico.Add(ps);
                        }
                    }
                    if (formularioDoGrupo_Ou_Unidade.Equals("UNIDADE"))
                    {
                        item.Unidade = grupo_Unidade;
                        listaPS = procProduto_Servico.ConsultarRegistro(item, true);

                        foreach (Produto_Servico ps in listaPS.Where(ps => ps.Unidade.codigo.Equals(grupo_Unidade.codigo)))
                        {
                            listaProduto_Servico.Add(ps);
                        }
                    }
                }
                
                CarregarListaDGV();
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

        private void CarregarListaDGV()
        {
            try
            {
                if (listaMaterial.Count > 0 || listaProduto_Servico.Count > 0)
                {
                    dgv_MateriaisOuProdutosServicos.Rows.Clear();
                    
                    if (materialOuProduto.Equals("MATERIAL"))
                    {
                        foreach (Material item in listaMaterial)
                        {
                            AdicionarLinhasDGV_Material(item);
                        }
                    }
                    else if(materialOuProduto.Equals("PRODUTO"))
                    {
                        foreach (Produto_Servico item in listaProduto_Servico)
                        {
                            AdicionarLinhasDGV_ProdutoServico(item);
                        }
                    }
                    
                    dgv_MateriaisOuProdutosServicos.Rows[0].Selected = true;
                }
                else
                {
                    dgv_MateriaisOuProdutosServicos.Rows.Clear();

                    btn_DadosGerais_Imprimir.Enabled = false;
                    btn_DadosGerais_VoltarGrid.Enabled = false;
                    btn_DadosGerais_AvancarGrid.Enabled = false;
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void AdicionarLinhasDGV_Material(Material item)
        {
            try
            {
                string valorUnitárioFormatado = Convert.ToString(item.valor_unitario);

                dgv_MateriaisOuProdutosServicos.Rows.Add(item.codigo, DateTime.Parse(item.ultima_atualizacao.ToString()).ToString("dd/MM/yyyy HH:mm"), item.descricao, item.Grupo.descricao,
                    item.Unidade.sigla, item.altura, item.largura, item.comprimento, decimal.Parse(valorUnitárioFormatado).ToString("C2"));
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void AdicionarLinhasDGV_ProdutoServico(Produto_Servico item)
        {
            try
            {
                string valorUnitárioFormatado = Convert.ToString(item.valor_total);

                dgv_MateriaisOuProdutosServicos.Rows.Add(item.codigo, DateTime.Parse(item.ultima_atualizacao.ToString()).ToString("dd/MM/yyyy HH:mm"), item.descricao, item.Grupo.descricao,
                    item.Unidade.sigla, item.altura, item.largura, item.comprimento, decimal.Parse(valorUnitárioFormatado).ToString("C2"));
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void ModificarTabelaExcel(string patchTabela)
        {
            try
            {
                conexaoExcel = new Excel.Application();
                arquivoExcel = conexaoExcel.Workbooks.Open(patchTabela);
                planilhaExcel = arquivoExcel.Worksheets[1] as Excel.Worksheet;

                string siglaDaUnidade = "";
                if (formularioDoGrupo_Ou_Unidade.Equals("UNIDADE"))
                    siglaDaUnidade = grupo_Unidade.sigla.ToString();

                planilhaExcel.Cells[1, 1].Value = formularioDoGrupo_Ou_Unidade + ": " + grupo_Unidade.codigo.ToString();
                planilhaExcel.Cells[1, 3].Value = formularioDoGrupo_Ou_Unidade + ": ( " + siglaDaUnidade + " ) - " + grupo_Unidade.descricao.ToString();

                int linha = 3;
                if (materialOuProduto.Equals("MATERIAL"))
                {
                    foreach (Material item in listaMaterial)
                    {
                        int coluna = 1;

                        planilhaExcel.Cells[linha, coluna++].Value = item.codigo.ToString();
                        planilhaExcel.Cells[linha, coluna++].Value = item.ultima_atualizacao.ToString();
                        planilhaExcel.Cells[linha, coluna++].Value = item.descricao.ToString();
                        planilhaExcel.Cells[linha, coluna++].Value = "(" + item.Grupo.codigo.ToString() + ") " + item.Grupo.descricao.ToString();
                        planilhaExcel.Cells[linha, coluna++].Value = "(" + item.Unidade.codigo.ToString() + ") ( " + item.Unidade.sigla.ToString() + " ) - " + item.Unidade.descricao.ToString();
                        planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.altura.ToString()).ToString("N2", new CultureInfo("pt-BR"));
                        planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.largura.ToString()).ToString("N2", new CultureInfo("pt-BR"));
                        planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.comprimento.ToString()).ToString("N2", new CultureInfo("pt-BR"));
                        planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.valor_unitario.ToString()).ToString("C2", new CultureInfo("pt-BR"));

                        linha++;
                    }
                }
                else if (materialOuProduto.Equals("PRODUTO"))
                {
                    foreach (Produto_Servico item in listaProduto_Servico)
                    {
                        int coluna = 1;

                        planilhaExcel.Cells[linha, coluna++].Value = item.codigo.ToString();
                        planilhaExcel.Cells[linha, coluna++].Value = item.ultima_atualizacao.ToString();
                        planilhaExcel.Cells[linha, coluna++].Value = item.descricao.ToString();
                        planilhaExcel.Cells[linha, coluna++].Value = "(" + item.Grupo.codigo.ToString() + ") " + item.Grupo.descricao.ToString();
                        planilhaExcel.Cells[linha, coluna++].Value = "(" + item.Unidade.codigo.ToString() + ") ( " + item.Unidade.sigla.ToString() + " ) - " + item.Unidade.descricao.ToString();
                        planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.altura.ToString()).ToString("N2", new CultureInfo("pt-BR"));
                        planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.largura.ToString()).ToString("N2", new CultureInfo("pt-BR"));
                        planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.comprimento.ToString()).ToString("N2", new CultureInfo("pt-BR"));
                        planilhaExcel.Cells[linha, coluna++].Value = decimal.Parse(item.valor_total.ToString()).ToString("C2", new CultureInfo("pt-BR"));

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

        #endregion

        #region Eventos Button Click
        
        private void btn_DadosGerais_Imprimir_Click(object sender, EventArgs e)
        {
            if (listaMaterial.Count > 0 || listaProduto_Servico.Count > 0)
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
        }

        private void btn_DadosGerais_VoltarGrid_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_MateriaisOuProdutosServicos.CurrentRow.Index > 0)
                {
                    int linha = MudarLinha_DGV(dgv_MateriaisOuProdutosServicos, false);
                    dgv_MateriaisOuProdutosServicos.CurrentCell = dgv_MateriaisOuProdutosServicos.Rows[linha].Cells[dgv_MateriaisOuProdutosServicos.CurrentCell.ColumnIndex];
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
                if (dgv_MateriaisOuProdutosServicos.CurrentRow.Index < (dgv_MateriaisOuProdutosServicos.RowCount - 1))
                {
                    int linha = MudarLinha_DGV(dgv_MateriaisOuProdutosServicos, true);
                    dgv_MateriaisOuProdutosServicos.CurrentCell = dgv_MateriaisOuProdutosServicos.Rows[linha].Cells[dgv_MateriaisOuProdutosServicos.CurrentCell.ColumnIndex];
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

    }
}
