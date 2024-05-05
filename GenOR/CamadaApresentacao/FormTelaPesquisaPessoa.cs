using System;
using System.Drawing;
using System.Windows.Forms;

namespace GenOR
{
    public partial class FormTelaPesquisaPessoa : FormBase
    {
        #region Variaveis

        public string campoPesquisado;
        public string informaçãoRetornada;

        private GerenciarMensagensPadraoSistema gerenciarMensagensPadraoSistema;

        private string formularioDoUsuario_Cliente_Fornecedor;

        #endregion

        public FormTelaPesquisaPessoa(string formularioDoUsuario_Cliente_Fornecedor)
        {
            try
            {
                InitializeComponent();

                campoPesquisado = "CANCELADO";
                informaçãoRetornada = "VAZIA";

                gerenciarMensagensPadraoSistema = new GerenciarMensagensPadraoSistema();

                this.StartPosition = FormStartPosition.Manual;
                this.StartPosition = FormStartPosition.CenterParent;

                this.formularioDoUsuario_Cliente_Fornecedor = formularioDoUsuario_Cliente_Fornecedor;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region Eventos Form

        private void FormTelaPesquisaPessoa_Load(object sender, EventArgs e)
        {
            try
            {
                cb_CpfCnpj.SelectedIndex = 0;
                cb_Estado.SelectedIndex = 24;

                this.Text = "PESQUISANDO " + formularioDoUsuario_Cliente_Fornecedor;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Eventos Button Click

        private void btn_PesquisaPorCodigo_Click(object sender, EventArgs e)
        {
            try
            {
                int resultado = 0;
                if (int.TryParse(txtb_Codigo.Text.Trim(), out resultado) && !txtb_Codigo.Text.Trim().Equals(""))
                {
                    if (resultado > 0)
                    {
                        campoPesquisado = "CÓDIGO";
                        informaçãoRetornada = resultado.ToString();
                        
                        this.Close();
                    }
                    else
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("CÓDIGO");
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CÓDIGO");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_PesquisaPorRazaoSocial_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtb_RazaoSocial.Text.Trim().Equals(""))
                {
                    campoPesquisado = "RAZÃO SOCIAL";
                    informaçãoRetornada = txtb_RazaoSocial.Text.ToString();
                    
                    this.Close();
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("RAZÃO SOCIAL");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_PesquisaPorNomeFantasia_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtb_NomeFantasia.Text.Trim().Equals(""))
                {
                    campoPesquisado = "NOME FANTASIA";
                    informaçãoRetornada = txtb_NomeFantasia.Text.ToString();
                    
                    this.Close();
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("NOME FANTASIA");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_PesquisaPorCpfCnpj_Click(object sender, EventArgs e)
        {
            try
            {
                int resultado = 0;
                string variavelSaidaTexto = CampoMaskedTxt_RemoverFormatacao(mtxtb_CpfCnpj);
                if (!variavelSaidaTexto.Equals("") || int.TryParse(variavelSaidaTexto, out resultado))
                {
                    if (resultado.ToString().Length.Equals(11) || resultado.ToString().Length.Equals(14))
                    {
                        campoPesquisado = "CPF/CNPJ";
                        informaçãoRetornada = resultado.ToString();
                        
                        this.Close();
                    }
                    else
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("CPF/CNPJ");
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CPF/CNPJ");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_PesquisaPorInscricaoEstadual_Click(object sender, EventArgs e)
        {
            try
            {
                int resultado = 0;
                string variavelSaidaTexto = CampoMaskedTxt_RemoverFormatacao(mtxtb_InscricaoEstadual);
                if (!variavelSaidaTexto.Equals("") || int.TryParse(variavelSaidaTexto, out resultado))
                {
                    if (resultado.ToString().Length.Equals(12))
                    {
                        campoPesquisado = "INSCRIÇÃO ESTADUAL";
                        informaçãoRetornada = resultado.ToString();
                        
                        this.Close();
                    }
                    else
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("INSCRIÇÃO ESTADUAL");
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("INSCRIÇÃO ESTADUAL");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_PesquisaPorEmail_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtb_Email.Text.Trim().Equals(""))
                {
                    campoPesquisado = "EMAIL";
                    informaçãoRetornada = txtb_Email.Text.ToString();
                    
                    this.Close();
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("EMAIL");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_PesquisaPorEstado_Click(object sender, EventArgs e)
        {
            try
            {
                if (!cb_Estado.Text.Trim().Equals(""))
                {
                    campoPesquisado = "ESTADO";
                    informaçãoRetornada = cb_Estado.Text.ToString();
                    
                    this.Close();
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("ESTADO");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_PesquisaPorCidade_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtb_Cidade.Text.Trim().Equals(""))
                {
                    campoPesquisado = "CIDADE";
                    informaçãoRetornada = txtb_Cidade.Text.ToString();
                    
                    this.Close();
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CIDADE");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_PesquisaPorDDD_Click(object sender, EventArgs e)
        {
            try
            {
                int resultado = 0;
                if (int.TryParse(txtb_DDD.Text.Trim(), out resultado) && !txtb_DDD.Text.Trim().Equals(""))
                {
                    if (resultado.ToString().Length.Equals(2))
                    {
                        campoPesquisado = "DDD";
                        informaçãoRetornada = resultado.ToString();
                        
                        this.Close();
                    }
                    else
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("DDD");
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("DDD");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_PesquisaPorTelefone_Click(object sender, EventArgs e)
        {
            try
            {
                int resultado = 0;
                if (int.TryParse(txtb_Telefone.Text.Trim(), out resultado) && !txtb_Telefone.Text.Trim().Equals(""))
                {
                    if (resultado.ToString().Length.Equals(8) || resultado.ToString().Length.Equals(9) || resultado.ToString().Length.Equals(11))
                    {
                        campoPesquisado = "TELEFONE";
                        informaçãoRetornada = resultado.ToString();
                        
                        this.Close();
                    }
                    else
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("TELEFONE");
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("TELEFONE");
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Eventos Enter

        private void cb_CpfCnpj_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cb_CpfCnpj.SelectedIndex.Equals(0)) //CPF
                {
                    mtxtb_CpfCnpj.Mask = "000,000,000-00";
                    mtxtb_CpfCnpj.Size = new Size(120, 26);
                }
                else if (cb_CpfCnpj.SelectedIndex.Equals(1)) //CNPJ
                {
                    mtxtb_CpfCnpj.Size = new Size(150, 26);
                    mtxtb_CpfCnpj.Mask = "00,000,000/0000-00";
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos KeyPress

        private void txtb_Codigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusButton(btn_PesquisaPorCodigo, e))
                    CampoMoeda_KeyPress(sender, e, true);
                else
                    btn_PesquisaPorCodigo_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_RazaoSocial_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Enter_FocusButton(btn_PesquisaPorRazaoSocial, e))
                    btn_PesquisaPorRazaoSocial_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_NomeFantasia_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Enter_FocusButton(btn_PesquisaPorNomeFantasia, e))
                    btn_PesquisaPorNomeFantasia_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void mtxtb_CpfCnpj_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Enter_FocusButton(btn_PesquisaPorCpfCnpj, e))
                    btn_PesquisaPorCpfCnpj_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void mtxtb_InscricaoEstadual_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Enter_FocusButton(btn_PesquisaPorInscricaoEstadual, e))
                    btn_PesquisaPorInscricaoEstadual_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_Email_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Enter_FocusButton(btn_PesquisaPorEmail, e))
                    btn_PesquisaPorEmail_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_Cidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Enter_FocusButton(btn_PesquisaPorCidade, e))
                    btn_PesquisaPorCidade_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_DDD_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusButton(btn_PesquisaPorDDD, e))
                    CampoMoeda_KeyPress(sender, e, true);
                else
                    btn_PesquisaPorDDD_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_Telefone_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusButton(btn_PesquisaPorTelefone, e))
                    CampoMoeda_KeyPress(sender, e, true);
                else
                    btn_PesquisaPorTelefone_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

    }
}
