using CamadaObjetoTransferencia;
using CamadaProcessamento;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.IO;

namespace GenOR
{
    public partial class FormProdutosServicos_Vinculados_Material : FormBase
    {
        #region Variaveis

        private Material material;
        private ListaProduto_Servico listaProduto_Servico;

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

        public FormProdutosServicos_Vinculados_Material(Material materialPesquisado, Form formPesquisado)
        {
            try
            {
                InitializeComponent();
                
                material = new Material();
                InstanciamentoRapida_Material(material);
                
                material = materialPesquisado;
                listaProduto_Servico = new ListaProduto_Servico();

                form = formPesquisado;

                gerenciarMensagensPadraoSistema = new GerenciarMensagensPadraoSistema();

                gerenciarTelaLoading = new GerenciarTelaLoading();
                formTelaLoading = new FormTelaLoading();
                carregarThread = null;
                telaLoadingFoiAberta = false;
                
                arquivoListaParaImpressao = Localizar_Imagem_Documento("LISTAGEM PRODUTOS SERVIÇOS VINCULADOS AO MATERIAL.xlsx", false);
                conexaoExcel = null;
                arquivoExcel = null;
                planilhaExcel = null;

                dgv_ProdutoServico.AutoGenerateColumns = false;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
                this.Close();
            }
        }

        #region Eventos Form

        private void FormProdutosServicos_Vinculados_Load(object sender, EventArgs e)
        {
            try
            {
                this.StartPosition = FormStartPosition.Manual;
                
                this.Text = "PRODUTOS / SERVIÇOS VINCULADOS AO MATERIAL ( " + material.codigo.ToString() + " ) - " + material.descricao.ToString();
                
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

                ProcMateriais_Produto_Servico procMateriais_Produto_Servico = new ProcMateriais_Produto_Servico();

                Materiais_Produto_Servico item = new Materiais_Produto_Servico
                {
                    ativo_inativo = true
                };
                InstanciamentoRapida_Materiais_ProdutoServico(item);

                ListaMateriais_Produto_Servico listaMPS = procMateriais_Produto_Servico.ConsultarRegistro(item, true);

                foreach (Materiais_Produto_Servico mps in listaMPS.Where(mps => mps.Material.codigo.Equals(material.codigo)))
                {
                    listaProduto_Servico.Add(mps.Produto_Servico);
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
                if (listaProduto_Servico.Count > 0)
                {
                    dgv_ProdutoServico.Rows.Clear();

                    foreach (Produto_Servico item in listaProduto_Servico)
                    {
                        AdicionarLinhasDGV(item);
                    }
                    
                    dgv_ProdutoServico.Rows[0].Selected = true;
                }
                else
                {
                    dgv_ProdutoServico.Rows.Clear();

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

        private void AdicionarLinhasDGV(Produto_Servico item)
        {
            try
            {
                string maoObraFormatado = Convert.ToString(item.maoObra);
                string valorTotalMateriaisFormatado = Convert.ToString(item.valor_total_materiais);
                string valorMaoObraFormatado = Convert.ToString(item.valor_maoObra);
                string valorTotalFormatado = Convert.ToString(item.valor_total);

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

        private void ModificarTabelaExcel(string patchTabela)
        {
            try
            {
                conexaoExcel = new Excel.Application();
                arquivoExcel = conexaoExcel.Workbooks.Open(patchTabela);
                planilhaExcel = arquivoExcel.Worksheets[1] as Excel.Worksheet;

                planilhaExcel.Cells[1, 1].Value = "MATERIAL: " + material.codigo.ToString();
                planilhaExcel.Cells[1, 2].Value = material.ultima_atualizacao.ToString();
                planilhaExcel.Cells[1, 3].Value = "MATERIAL: " + material.descricao.ToString();
                planilhaExcel.Cells[1, 4].Value = "(" + material.Grupo.codigo.ToString() + ") " + material.Grupo.descricao.ToString();
                planilhaExcel.Cells[1, 5].Value = "(" + material.Unidade.codigo.ToString() + ") ( " + material.Unidade.sigla.ToString() + " ) - " + material.Unidade.descricao.ToString();

                int linha = 3;
                foreach (Produto_Servico item in listaProduto_Servico)
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

        #region Eventos Button Click
        
        private void btn_DadosGerais_Imprimir_Click(object sender, EventArgs e)
        {
            try
            {
                if (listaProduto_Servico.Count > 0)
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
