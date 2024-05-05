using CamadaProcessamento;
using System;
using System.IO;
using System.Windows.Forms;

namespace GenOR
{
    public partial class FormBackup_Restore : FormBase
    {
        #region Variaveis

        private ProcBD procBD;
        private GerenciarMensagensPadraoSistema gerenciarMensagensPadraoSistema;

        private OpenFileDialog path_ArquivoBackupZip;

        #endregion

        public FormBackup_Restore()
        {
            try
            {
                InitializeComponent();

                procBD = new ProcBD();
                gerenciarMensagensPadraoSistema = new GerenciarMensagensPadraoSistema();

                path_ArquivoBackupZip = new OpenFileDialog();
                path_ArquivoBackupZip.Filter = "Arquivo Backup (*.zip)|*.zip";
                path_ArquivoBackupZip.Title = "RESTORE DO BANCO DADOS";
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
                this.Close();
            }
        }

        #region Eventos Form

        private void FormBackup_Restore_Load(object sender, EventArgs e)
        {
            try
            {
                this.StartPosition = FormStartPosition.Manual;
                this.StartPosition = FormStartPosition.CenterParent;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Funções Gerais

        private void TelaCarregamento(string acaoRealizada)
        {
            try
            {
                if (acaoRealizada.Trim().Equals(""))
                {
                    btn_TelaEspera_SemImagens.SendToBack();
                    btn_TelaEspera_SemImagens.Text = acaoRealizada;
                    btn_TelaEspera_SemImagens.Visible = false;

                    lb_Backup.Visible= true;
                    btn_Backup.Visible = true;

                    lb_Restore.Visible = true;
                    btn_Restore.Visible= true;
                }
                else
                {
                    btn_TelaEspera_SemImagens.BringToFront();
                    btn_TelaEspera_SemImagens.Text = "REALIZANDO " + acaoRealizada;
                    btn_TelaEspera_SemImagens.Visible= true;

                    lb_Backup.Visible = false;
                    btn_Backup.Visible = false;

                    lb_Restore.Visible = false;
                    btn_Restore.Visible = false;
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Button Click

        private void btn_Backup_Click(object sender, EventArgs e)
        {
            try
            {
                TelaCarregamento("BACKUP DO SISTEMA . . .\nAguarde um instante !");
                
                if (gerenciarMensagensPadraoSistema.Mensagem_Confirmacao("REALIZAR O BACKUP DO SISTEMA").Equals(DialogResult.OK))
                {
                    FolderBrowserDialog pathDestino = new FolderBrowserDialog();
                    if (pathDestino.ShowDialog().Equals(DialogResult.OK) && !string.IsNullOrWhiteSpace(pathDestino.SelectedPath))
                    {
                        string pathDestinoFormatado = Path.Combine(pathDestino.SelectedPath, "GenOR_Backup(" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ").zip");
                        if (procBD.Executar_BackupBD(pathDestinoFormatado))
                            gerenciarMensagensPadraoSistema.Mensagem_Sucesso("BACKUP DO SISTEMA");
                        else
                            gerenciarMensagensPadraoSistema.Mensagem_Falha("BACKUP DO SISTEMA");
                    }
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
            finally
            {
                TelaCarregamento("");
            }
        }

        private void btn_Restore_Click(object sender, EventArgs e)
        {
            try
            {
                TelaCarregamento("RESTORE DO SISTEMA . . .\nAguarde um instante !");

                if (gerenciarMensagensPadraoSistema.Mensagem_Confirmacao("REALIZAR O RESTORE DO SISTEMA").Equals(DialogResult.OK))
                {
                    if (path_ArquivoBackupZip.ShowDialog().Equals(DialogResult.OK) && !string.IsNullOrWhiteSpace(path_ArquivoBackupZip.FileName))
                    {
                        if (procBD.Executar_RestoreBD(path_ArquivoBackupZip.FileName))
                            gerenciarMensagensPadraoSistema.Mensagem_Sucesso("RESTORE DO SISTEMA");
                        else
                            gerenciarMensagensPadraoSistema.Mensagem_Falha("RESTORE DO SISTEMA: Arquivo está Incorreto ou Vazio");
                    }
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
            finally
            {
                TelaCarregamento("");
            }
        }

        #endregion

    }
}
