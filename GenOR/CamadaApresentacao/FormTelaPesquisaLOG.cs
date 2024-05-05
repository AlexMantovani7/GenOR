using System;
using System.Drawing;
using System.Windows.Forms;

namespace GenOR
{
    public partial class FormTelaPesquisaLOG : FormBase
    {
        #region Variaveis

        public string campoPesquisado;
        public string informaçãoRetornada;
        public string informaçãoRetornada2;

        private GerenciarMensagensPadraoSistema gerenciarMensagensPadraoSistema;

        #endregion

        public FormTelaPesquisaLOG()
        {
            try
            {
                InitializeComponent();

                campoPesquisado = "CANCELADO";
                informaçãoRetornada = "VAZIA";
                informaçãoRetornada2 = "VAZIA";

                gerenciarMensagensPadraoSistema = new GerenciarMensagensPadraoSistema();

                this.StartPosition = FormStartPosition.Manual;
                this.StartPosition = FormStartPosition.CenterParent;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region Eventos Button Click

        private void btn_PesquisaPorCodigo_Click(object sender, EventArgs e)
        {
            try
            {
                int resultado = 0;
                if (int.TryParse(txtb_CodigoLOG.Text.Trim(), out resultado) && !txtb_CodigoLOG.Text.Trim().Equals(""))
                {
                    if (resultado > 0)
                    {
                        campoPesquisado = "CÓDIGO LOG";
                        informaçãoRetornada = resultado.ToString();
                        
                        this.Close();
                    }
                    else
                        gerenciarMensagensPadraoSistema.CampoMoedaZeradoInvalido("CÓDIGO LOG");
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CÓDIGO LOG");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_PesquisaPorDataRegistro_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime resultadoInicial;
                DateTime resultadoFinal;
                if (DateTime.TryParse(mtxtb_DataRegistroInicial.Text.Trim(), out resultadoInicial) && !mtxtb_DataRegistroInicial.Text.Trim().Equals("")
                    && DateTime.TryParse(mtxtb_DataRegistroFinal.Text.Trim(), out resultadoFinal) && !mtxtb_DataRegistroFinal.Text.Trim().Equals(""))
                {
                    if (resultadoInicial.Date <= DateTime.Now.Date)
                    {
                        if (resultadoFinal.Date <= DateTime.Now.Date)
                        {
                            if (resultadoInicial.Date <= resultadoFinal.Date)
                            {
                                campoPesquisado = "DATA REGISTRO";
                                informaçãoRetornada = resultadoInicial.ToString();
                                informaçãoRetornada2 = resultadoFinal.ToString();
                                
                                this.Close();
                            }
                            else
                                gerenciarMensagensPadraoSistema.CampoDuploDataNullBrancoOuDatado("DATA REGISTRO (INICIAL) E (FINAL)");
                        }
                        else
                            gerenciarMensagensPadraoSistema.CampoDuploDataNullBrancoOuDatado("DATA REGISTRO (FINAL)");
                    }
                    else
                        gerenciarMensagensPadraoSistema.CampoDuploDataNullBrancoOuDatado("DATA REGISTRO (INICIAL)");
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("DATA REGISTRO");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_PesquisaPorOperacao_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtb_Operacao.Text.Trim().Equals(""))
                {
                    campoPesquisado = "OPERAÇÃO";
                    informaçãoRetornada = txtb_Operacao.Text.ToString();
                    
                    this.Close();
                }
                else
                {
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("OPERAÇÃO");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_PesquisaPorRegistro_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtb_Registro.Text.Trim().Equals(""))
                {
                    campoPesquisado = "REGISTRO";
                    informaçãoRetornada = txtb_Registro.Text.ToString();
                    
                    this.Close();
                }
                else
                {
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("REGISTRO");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Eventos KeyPress

        private void txtb_CodigoLOG_KeyPress(object sender, KeyPressEventArgs e)
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

        private void mtxtb_DataRegistroInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Enter_FocusMaskedTxtb(mtxtb_DataRegistroFinal, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void mtxtb_DataRegistroFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Enter_FocusButton(btn_PesquisaPorDataRegistro, e))
                    btn_PesquisaPorDataRegistro_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_Operacao_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Enter_FocusButton(btn_PesquisaPorOperacao, e))
                    btn_PesquisaPorOperacao_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_Registro_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Enter_FocusButton(btn_PesquisaPorDataRegistro, e))
                    btn_PesquisaPorDataRegistro_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Eventos Calendário

        private void FormularioModoCalendario(bool ativo)
        {
            try
            {
                if (ativo)
                    this.Size = new Size(this.Width, 290);
                else
                    this.Size = new Size(this.Width, 147);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void GerenciaAtivacaoButtonsFechar_Calendario(string nomeBotao)
        {
            try
            {
                if (nomeBotao.Equals("DATA REGISTRO INICIAL"))
                {
                    btn_DataRegistroInicial.Visible = false;
                    btn_FecharCalendarioDataRegistroInicial.Visible = true;
                }
                else
                {
                    btn_DataRegistroInicial.Visible = true;
                    btn_FecharCalendarioDataRegistroInicial.Visible = false;
                }

                if (nomeBotao.Equals("DATA REGISTRO FINAL"))
                {
                    btn_DataRegistroFinal.Visible = false;
                    btn_FecharCalendarioDataRegistroFinal.Visible = true;
                }
                else
                {
                    btn_DataRegistroFinal.Visible = true;
                    btn_FecharCalendarioDataRegistroFinal.Visible = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private void tc_Pesquisa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (mc_Calendario.Visible.Equals(true))
                {
                    mc_Calendario.Visible = false;
                    GerenciaAtivacaoButtonsFechar_Calendario("NENHUM BOTÃO ATIVO");
                    FormularioModoCalendario(false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private void btn_DataRegistroInicial_Click(object sender, EventArgs e)
        {
            try
            {
                if (mc_Calendario.Visible.Equals(false))
                    mc_Calendario.Visible = true;

                DefinirEixoXY_Calendario(btn_DataRegistroInicial, mc_Calendario);
                FormularioModoCalendario(true);
                GerenciaAtivacaoButtonsFechar_Calendario("DATA REGISTRO INICIAL");

                DateTime variavelData = DateTime.Today.Date;
                if (DateTime.TryParse(btn_DataRegistroInicial.Text.Trim(), out variavelData))
                    mc_Calendario.SetDate(variavelData);
                else
                    mc_Calendario.SetDate(DateTime.Today);

                mc_Calendario.MinDate = DateTime.Parse("01/01/1753");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_DataRegistroFinal_Click(object sender, EventArgs e)
        {
            try
            {
                if (mc_Calendario.Visible.Equals(false))
                    mc_Calendario.Visible = true;

                DefinirEixoXY_Calendario(btn_DataRegistroFinal, mc_Calendario);
                FormularioModoCalendario(true);
                GerenciaAtivacaoButtonsFechar_Calendario("DATA REGISTRO FINAL");

                DateTime variavelData = DateTime.Today.Date;
                if (DateTime.TryParse(mtxtb_DataRegistroFinal.Text.Trim(), out variavelData))
                    mc_Calendario.SetDate(variavelData);
                else
                    mc_Calendario.SetDate(DateTime.Today);

                mc_Calendario.MinDate = DateTime.Parse("01/01/1753");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_FecharCalendarioDataRegistroInicial_Click(object sender, EventArgs e)
        {
            try
            {
                tc_Pesquisa_SelectedIndexChanged(sender, e);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_FecharCalendarioDataRegistroFinal_Click(object sender, EventArgs e)
        {
            try
            {
                tc_Pesquisa_SelectedIndexChanged(sender, e);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void mc_Calendario_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                if (btn_FecharCalendarioDataRegistroInicial.Visible.Equals(true))
                {
                    mtxtb_DataRegistroInicial.Text = mc_Calendario.SelectionStart.ToShortDateString();
                    btn_FecharCalendarioDataRegistroInicial_Click(sender, e);
                }
                else if (btn_FecharCalendarioDataRegistroFinal.Visible.Equals(true))
                {
                    mtxtb_DataRegistroFinal.Text = mc_Calendario.SelectionStart.ToShortDateString();
                    btn_FecharCalendarioDataRegistroFinal_Click(sender, e);
                }
                else
                    mc_Calendario.Visible = false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

    }
}
