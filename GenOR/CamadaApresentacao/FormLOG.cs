using CamadaObjetoTransferencia;
using CamadaProcessamento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using GenOR.Properties;
using System.Diagnostics;

namespace GenOR
{
    public partial class FormLOG : FormBase
    {
        #region Variaveis

        private LOG log;
        private ListaLOG listaLOG;
        private ProcLOG procLOG;

        private List<LOG> listaPesquisada;

        private GerenciarMensagensPadraoSistema gerenciarMensagensPadraoSistema;

        private GerenciarTelaLoading gerenciarTelaLoading;
        private FormTelaLoading formTelaLoading;
        private Thread carregarThread;
        private bool telaLoadingFoiAberta;

        private static int colunaCodigoIndex = 0;

        private string imagemButtomPesquisa;
        private FormTelaPesquisaLOG formTelaPesquisaLOG;

        private string arquivoListaParaImpressao;
        private Excel.Application conexaoExcel;
        private Excel.Workbook arquivoExcel;
        private Excel.Worksheet planilhaExcel;

        #endregion

        public FormLOG()
        {
            try
            {
                InitializeComponent();

                log = new LOG();
                listaLOG = new ListaLOG();
                procLOG = new ProcLOG();

                listaPesquisada = new ListaLOG();

                gerenciarMensagensPadraoSistema = new GerenciarMensagensPadraoSistema();

                gerenciarTelaLoading = new GerenciarTelaLoading();
                formTelaLoading = new FormTelaLoading();
                carregarThread = null;
                telaLoadingFoiAberta = false;

                imagemButtomPesquisa = "PESQUISAR";
                formTelaPesquisaLOG = new FormTelaPesquisaLOG();

                arquivoListaParaImpressao = Localizar_Imagem_Documento("LISTAGEM LOG.xlsx", false);
                conexaoExcel = null;
                arquivoExcel = null;
                planilhaExcel = null;

                dgv_LOG.AutoGenerateColumns = false;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
                this.Close();
            }
        }

        #region Funções Gerais

        private void ObterListagemTodosRegistrosDGV()
        {
            try
            {
                LOG log = new LOG();
                listaLOG = procLOG.ConsultarRegistro(log, true);

                CarregarListaDGV();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void AdicionarLinhasDGV(LOG item)
        {
            try
            {
                dgv_LOG.Rows.Add(item.codigo, DateTime.Parse(item.data_registro.ToString()).ToString("dd/MM/yyyy HH:mm"), item.operacao, item.registro, item.informacoes_registro);
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
                if (listaLOG.Count > 0)
                {
                    dgv_LOG.Rows.Clear();

                    foreach (LOG item in listaLOG)
                    {
                        AdicionarLinhasDGV(item);
                    }

                    dgv_LOG.Rows[0].Selected = true;
                    PreencherCampos();
                }
                else
                    dgv_LOG.Rows.Clear();
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
                if (dgv_LOG.RowCount > 0)
                {
                    telaLoadingFoiAberta = true;
                    gerenciarTelaLoading.Abrir(this, formTelaLoading, carregarThread);

                    int codigoLinhaSelecionada = Convert.ToInt32(dgv_LOG.Rows[dgv_LOG.CurrentCell.RowIndex].Cells[colunaCodigoIndex].Value);

                    object sender = new object();
                    EventArgs e = new EventArgs();

                    foreach (LOG item in listaLOG.Where(lm => lm.codigo.Equals(codigoLinhaSelecionada)))
                    {
                        txtb_CodigoLOG.Text = item.codigo.ToString();
                        txtb_DataRegistroLOG.Text = DateTime.Parse(item.data_registro.ToString()).ToString("dd/MM/yyyy HH:mm");

                        txtb_OperacaoLOG.Text = item.operacao;
                        txtb_RegistroLOG.Text = item.registro;
                        txtb_InformacoesRegistroLOG.Text = item.informacoes_registro;

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

        private void ModificarTabelaExcel(string patchTabela)
        {
            try
            {
                List<LOG> listaImpressao = new ListaLOG();

                if (imagemButtomPesquisa.Equals("PESQUISAR"))
                    listaImpressao = listaLOG;
                else
                    listaImpressao = listaPesquisada;

                conexaoExcel = new Excel.Application();
                arquivoExcel = conexaoExcel.Workbooks.Open(patchTabela);
                planilhaExcel = arquivoExcel.Worksheets[1] as Excel.Worksheet;

                int linha = 2;
                foreach (LOG item in listaImpressao)
                {
                    int coluna = 1;

                    planilhaExcel.Cells[linha, coluna++].Value = item.codigo.ToString();
                    planilhaExcel.Cells[linha, coluna++].Value = item.data_registro.ToString();
                    planilhaExcel.Cells[linha, coluna++].Value = item.operacao.ToString();
                    planilhaExcel.Cells[linha, coluna++].Value = item.registro.ToString();
                    planilhaExcel.Cells[linha, coluna++].Value = item.informacoes_registro.ToString();

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

        private void FormLOG_Load(object sender, EventArgs e)
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

        private void FormLOG_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (gerenciarMensagensPadraoSistema.Mensagem_FechamentoJanela("").Equals(DialogResult.Cancel))
                    e.Cancel = true;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Button Click

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
                if (dgv_LOG.CurrentRow.Index > 0)
                {
                    int linha = MudarLinha_DGV(dgv_LOG, false);

                    dgv_LOG.CurrentCell = dgv_LOG.Rows[linha].Cells[dgv_LOG.CurrentCell.ColumnIndex];
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
                if (dgv_LOG.CurrentRow.Index < (dgv_LOG.RowCount - 1))
                {
                    int linha = MudarLinha_DGV(dgv_LOG, true);

                    dgv_LOG.CurrentCell = dgv_LOG.Rows[linha].Cells[dgv_LOG.CurrentCell.ColumnIndex];
                    PreencherCampos();
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
                    formTelaPesquisaLOG = new FormTelaPesquisaLOG();
                    formTelaPesquisaLOG.ShowDialog();

                    if (!formTelaPesquisaLOG.campoPesquisado.Equals("CANCELADO") && !formTelaPesquisaLOG.informaçãoRetornada.Equals("VAZIA"))
                    {
                        listaPesquisada = new ListaLOG();

                        if (formTelaPesquisaLOG.campoPesquisado.Equals("CÓDIGO LOG"))
                        {
                            listaPesquisada = listaLOG.Where(lm => lm.codigo.Equals(int.Parse(formTelaPesquisaLOG.informaçãoRetornada))).ToList();
                        }
                        else if (formTelaPesquisaLOG.campoPesquisado.Equals("DATA REGISTRO") && !formTelaPesquisaLOG.informaçãoRetornada2.Equals("VAZIA"))
                        {
                            listaPesquisada = listaLOG.Where(lm => DateTime.Parse(lm.data_registro.ToString()).Date >= DateTime.Parse(formTelaPesquisaLOG.informaçãoRetornada).Date
                            && DateTime.Parse(lm.data_registro.ToString()).Date <= DateTime.Parse(formTelaPesquisaLOG.informaçãoRetornada2).Date).ToList();
                        }
                        else if (formTelaPesquisaLOG.campoPesquisado.Equals("OPERAÇÃO"))
                        {
                            listaPesquisada = listaLOG.Where(lm => lm.operacao.Contains(formTelaPesquisaLOG.informaçãoRetornada)).ToList();
                        }
                        else if (formTelaPesquisaLOG.campoPesquisado.Equals("REGISTRO"))
                        {
                            listaPesquisada = listaLOG.Where(lm => lm.registro.Contains(formTelaPesquisaLOG.informaçãoRetornada)).ToList();
                        }
                        else
                        {
                            gerenciarMensagensPadraoSistema.InformacaoPesquisadaNaoEncontrada();
                            return;
                        }

                        if (listaPesquisada.Count() > 0)
                        {
                            dgv_LOG.Rows.Clear();

                            foreach (LOG item in listaPesquisada)
                            {
                                AdicionarLinhasDGV(item);
                            }

                            dgv_LOG.Rows[0].Selected = true;
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
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Data Grid View LOG

        private void dgv_LOG_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue.Equals((int)Keys.Up) || e.KeyValue.Equals((int)Keys.Down))
                    PreencherCampos();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }
        
        private void dgv_LOG_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                var linhaClicada = dgv_LOG.HitTest(e.X, e.Y);
                if (linhaClicada != null && linhaClicada.RowIndex >= 0)
                {
                    if (e.Button.Equals(MouseButtons.Right))
                        dgv_LOG.CurrentCell = dgv_LOG.Rows[linhaClicada.RowIndex].Cells[0];
                    
                    PreencherCampos();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void dgv_LOG_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                var linhaClicada = dgv_LOG.HitTest(e.X, e.Y);
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
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_2, "outros");
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
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_3, "outros");
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
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_4, "outros");
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
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_5, "outros");
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

    }
}