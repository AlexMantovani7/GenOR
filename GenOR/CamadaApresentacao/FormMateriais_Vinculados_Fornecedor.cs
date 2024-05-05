using CamadaObjetoTransferencia;
using CamadaProcessamento;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace GenOR
{
    public partial class FormMateriais_Vinculados_Fornecedor : FormBase
    {
        #region Variaveis

        private Pessoa pessoa;
        private ListaMaterial listaMaterial;

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

        public FormMateriais_Vinculados_Fornecedor(Pessoa pessoaPesquisado, Form formPesquisado)
        {
            try
            {
                InitializeComponent();

                pessoa = new Pessoa();

                pessoa = pessoaPesquisado;
                listaMaterial = new ListaMaterial();

                form = formPesquisado;

                gerenciarMensagensPadraoSistema = new GerenciarMensagensPadraoSistema();

                gerenciarTelaLoading = new GerenciarTelaLoading();
                formTelaLoading = new FormTelaLoading();
                carregarThread = null;
                telaLoadingFoiAberta = false;

                arquivoListaParaImpressao = Localizar_Imagem_Documento("LISTAGEM MATERIAIS VINCULADOS AO FORNECEDOR.xlsx", false);
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

        #region Eventos Form
        
        private void FormMateriais_Vinculados_Fornecedor_Load(object sender, EventArgs e)
        {
            try
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Text = "MATERIAIS VINCULADOS AO MATERIAL ( " + pessoa.codigo.ToString() + " ) - " + pessoa.nome_razao_social.ToString();

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

                ProcMaterial procMaterial = new ProcMaterial();

                Material item = new Material
                {
                    ativo_inativo = true
                };
                InstanciamentoRapida_Material(item);

                ListaMaterial listaM;

                item.Fornecedor = pessoa;
                listaM = procMaterial.ConsultarRegistro(item, true);

                foreach (Material m in listaM.Where(m => m.Fornecedor.codigo.Equals(pessoa.codigo)))
                {
                    listaMaterial.Add(m);
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
                if (listaMaterial.Count > 0)
                {
                    dgv_Material.Rows.Clear();

                    foreach (Material item in listaMaterial)
                    {
                        AdicionarLinhasDGV(item);
                    }

                    dgv_Material.Rows[0].Selected = true;
                }
                else
                {
                    dgv_Material.Rows.Clear();
                    
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

        private void AdicionarLinhasDGV(Material item)
        {
            try
            {
                string valorUnitárioFormatado = Convert.ToString(item.valor_unitario);

                dgv_Material.Rows.Add(item.codigo, DateTime.Parse(item.ultima_atualizacao.ToString()).ToString("dd/MM/yyyy HH:mm"), item.descricao, item.Grupo.descricao,
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

                planilhaExcel.Cells[1, 1].Value = "FORNECEDOR: (" + pessoa.codigo.ToString() + ")";
                planilhaExcel.Cells[1, 3].Value = "RAZÃO SOCIAL: " + pessoa.nome_razao_social.ToString();

                int linha = 3;
                foreach (Material item in listaMaterial)
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
                if (listaMaterial.Count > 0)
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
                if (dgv_Material.CurrentRow.Index > 0)
                {
                    int linha = MudarLinha_DGV(dgv_Material, false);
                    dgv_Material.CurrentCell = dgv_Material.Rows[linha].Cells[dgv_Material.CurrentCell.ColumnIndex];
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
