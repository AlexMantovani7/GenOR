using CamadaProcessamento;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GenOR
{
    public partial class FormConfiguracaoInicialBD : FormBase
    {
        #region Variaveis

        public bool conexaoBD;
        private GerenciarMensagensPadraoSistema gerenciarMensagensPadraoSistema;

        #endregion

        public FormConfiguracaoInicialBD()
        {
            InitializeComponent();

            conexaoBD = false;
            gerenciarMensagensPadraoSistema = new GerenciarMensagensPadraoSistema();
        }

        #region Eventos KeyPress

        private void txtb_Server_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Enter_FocusTxtb(txtb_Uid, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_Uid_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Enter_FocusTxtb(txtb_Password, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_Password_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Enter_FocusButton(btn_Confirmar, e))
                    btn_Confirmar_Click(sender, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Button Click

        private void btn_Confirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtb_Server.Text.Trim().Equals("") && !txtb_Uid.Text.Trim().Equals("") && !txtb_Password.Text.Trim().Equals(""))
                {
                    if (gerenciarMensagensPadraoSistema.Mensagem_Confirmacao("Configuração Banco de Dados").Equals(DialogResult.OK))
                    {
                        ProcBD procBD = new ProcBD();
                        if (procBD.Cadastrar_BDConnection(txtb_Server.Text, "GenOR_BD", txtb_Uid.Text, txtb_Password.Text))
                        {
                            gerenciarMensagensPadraoSistema.ConnexaoBD_Sucesso();

                            conexaoBD = true;
                            this.Close();
                        }
                        else
                        {
                            conexaoBD = false;
                            gerenciarMensagensPadraoSistema.ConnexaoBD_Error();
                        }
                    }
                }
                else
                {
                    string campoEmBranco = "";

                    if (txtb_Server.Text.Trim().Equals(""))
                        campoEmBranco = "SERVER";
                    else if (txtb_Uid.Text.Trim().Equals(""))
                        campoEmBranco = "USERNAME";
                    else if (txtb_Password.Text.Trim().Equals(""))
                        campoEmBranco = "PASSWORD";

                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco(campoEmBranco);
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
                if (gerenciarMensagensPadraoSistema.Mensagem_Cancelamento().Equals(DialogResult.OK))
                {
                    conexaoBD = false;
                    this.Close();
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