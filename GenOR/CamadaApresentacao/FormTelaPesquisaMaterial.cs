using System;
using System.Drawing;
using System.Windows.Forms;

namespace GenOR
{
    public partial class FormTelaPesquisaMaterial : FormBase
    {
        #region Variaveis

        public string campoPesquisado;
        public string informaçãoRetornada;
        public string informaçãoRetornada2;

        private GerenciarMensagensPadraoSistema gerenciarMensagensPadraoSistema;

        #endregion

        public FormTelaPesquisaMaterial()
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

        private void btn_PesquisaPorUltimaAtualizacao_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime resultadoInicial;
                DateTime resultadoFinal;
                if (DateTime.TryParse(mtxtb_UltimaAtualizacaoInicial.Text.Trim(), out resultadoInicial) && !mtxtb_UltimaAtualizacaoInicial.Text.Trim().Equals("")
                    && DateTime.TryParse(mtxtb_UltimaAtualizacaoFinal.Text.Trim(), out resultadoFinal) && !mtxtb_UltimaAtualizacaoFinal.Text.Trim().Equals(""))
                {
                    if (resultadoInicial.Date <= DateTime.Now.Date)
                    {
                        if (resultadoFinal.Date <= DateTime.Now.Date)
                        {
                            if (resultadoInicial.Date <= resultadoFinal.Date)
                            {
                                campoPesquisado = "ULTIMA ATUALIZAÇÃO";
                                informaçãoRetornada = resultadoInicial.ToString();
                                informaçãoRetornada2 = resultadoFinal.ToString();
                                
                                this.Close();
                            }
                            else
                                gerenciarMensagensPadraoSistema.CampoDuploDataNullBrancoOuDatado("ULTIMA ATUALIZAÇÃO (INICIAL) E (FINAL)");
                        }
                        else
                            gerenciarMensagensPadraoSistema.CampoDuploDataNullBrancoOuDatado("ULTIMA ATUALIZAÇÃO (FINAL)");
                    }
                    else
                        gerenciarMensagensPadraoSistema.CampoDuploDataNullBrancoOuDatado("ULTIMA ATUALIZAÇÃO (INICIAL)");
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("ULTIMA ATUALIZAÇÃO");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_PesquisaPorDescricao_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtb_Descricao.Text.Trim().Equals(""))
                {
                    campoPesquisado = "DESCRIÇÃO";
                    informaçãoRetornada = txtb_Descricao.Text.ToString();
                    
                    this.Close();
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("DESCRIÇÃO");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_PesquisaPorGrupo_Click(object sender, EventArgs e)
        {
            try
            {
                if (! txtb_Grupo.Text.Trim().Equals(""))
                {
                    campoPesquisado = "GRUPO";
                    informaçãoRetornada = txtb_Grupo.Text.ToString();
                    
                    this.Close();
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("GRUPO");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_PesquisaPorUnidade_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtb_Unidade.Text.Trim().Equals(""))
                {
                    campoPesquisado = "UNIDADE";
                    informaçãoRetornada = txtb_Unidade.Text.ToString();
                    
                    this.Close();
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("UNIDADE");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_PesquisaPorAltura_Click(object sender, EventArgs e)
        {
            try
            {
                decimal resultadoInicial;
                decimal resultadoFinal;

                if (decimal.TryParse(txtb_AlturaInicial.Text.Trim(), out resultadoInicial) && !txtb_AlturaInicial.Text.Trim().Equals("")
                    && decimal.TryParse(txtb_AlturaFinal.Text.Trim(), out resultadoFinal) && !txtb_AlturaFinal.Text.Trim().Equals(""))
                {
                    if (resultadoInicial >= 0)
                    {
                        if (resultadoFinal > 0)
                        {
                            if (resultadoInicial <= resultadoFinal)
                            {
                                campoPesquisado = "ALTURA";
                                informaçãoRetornada = resultadoInicial.ToString();
                                informaçãoRetornada2 = resultadoFinal.ToString();
                                
                                this.Close();
                            }
                            else
                                gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("ALTURA (INICIAL) E (FINAL)");
                        }
                        else
                            gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("ALTURA (FINAL)");
                    }
                    else
                        gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("ALTURA (INICIAL)");
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("ALTURA");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_PesquisaPorLargura_Click(object sender, EventArgs e)
        {
            try
            {
                decimal resultadoInicial;
                decimal resultadoFinal;

                if (decimal.TryParse(txtb_LarguraInicial.Text.Trim(), out resultadoInicial) && !txtb_LarguraInicial.Text.Trim().Equals("")
                    && decimal.TryParse(txtb_LarguraFinal.Text.Trim(), out resultadoFinal) && !txtb_LarguraFinal.Text.Trim().Equals(""))
                {
                    if (resultadoInicial >= 0)
                    {
                        if (resultadoFinal > 0)
                        {
                            if (resultadoInicial <= resultadoFinal)
                            {
                                campoPesquisado = "LARGURA";
                                informaçãoRetornada = resultadoInicial.ToString();
                                informaçãoRetornada2 = resultadoFinal.ToString();
                                
                                this.Close();
                            }
                            else
                                gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("LARGURA (INICIAL) E (FINAL)");
                        }
                        else
                            gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("LARGURA (FINAL)");
                    }
                    else
                        gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("LARGURA (INICIAL)");
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("LARGURA");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_PesquisaPorComprimento_Click(object sender, EventArgs e)
        {
            try
            {
                decimal resultadoInicial;
                decimal resultadoFinal;

                if (decimal.TryParse(txtb_ComprimentoInicial.Text.Trim(), out resultadoInicial) && !txtb_ComprimentoInicial.Text.Trim().Equals("")
                    && decimal.TryParse(txtb_ComprimentoFinal.Text.Trim(), out resultadoFinal) && !txtb_ComprimentoFinal.Text.Trim().Equals(""))
                {
                    if (resultadoInicial >= 0)
                    {
                        if (resultadoFinal > 0)
                        {
                            if (resultadoInicial <= resultadoFinal)
                            {
                                campoPesquisado = "COMPRIMENTO";
                                informaçãoRetornada = resultadoInicial.ToString();
                                informaçãoRetornada2 = resultadoFinal.ToString();
                                
                                this.Close();
                            }
                            else
                                gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("COMPRIMENTO (INICIAL) E (FINAL)");
                        }
                        else
                            gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("COMPRIMENTO (FINAL)");
                    }
                    else
                        gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("COMPRIMENTO (INICIAL)");
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("COMPRIMENTO");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_PesquisaPorValorUnitario_Click(object sender, EventArgs e)
        {
            try
            {
                decimal resultadoInicial;
                decimal resultadoFinal;

                txtb_ValorUnitarioInicial_Enter(sender, e);
                txtb_ValorUnitarioFinal_Enter(sender, e);

                if (decimal.TryParse(txtb_ValorUnitarioInicial.Text.Trim(), out resultadoInicial) && !txtb_ValorUnitarioInicial.Text.Trim().Equals("")
                    && decimal.TryParse(txtb_ValorUnitarioFinal.Text.Trim(), out resultadoFinal) && !txtb_ValorUnitarioFinal.Text.Trim().Equals(""))
                {
                    if (resultadoInicial >= 0)
                    {
                        if (resultadoFinal > 0)
                        {
                            if (resultadoInicial <= resultadoFinal)
                            {
                                campoPesquisado = "VALOR UNITÁRIO";
                                informaçãoRetornada = resultadoInicial.ToString();
                                informaçãoRetornada2 = resultadoFinal.ToString();
                                
                                this.Close();
                            }
                            else
                                gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("VALOR UNITÁRIO (INICIAL) E (FINAL)");
                        }
                        else
                            gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("VALOR UNITÁRIO (FINAL)");
                    }
                    else
                        gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("VALOR UNITÁRIO (INICIAL)");
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("VALOR UNITÁRIO");

                txtb_ValorUnitarioInicial_Leave(sender, e);
                txtb_ValorUnitarioFinal_Leave(sender, e);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_PesquisaPorFornecedor_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtb_Fornecedor.Text.Trim().Equals(""))
                {
                    campoPesquisado = "FORNECEDOR";
                    informaçãoRetornada = txtb_Fornecedor.Text.ToString();
                    
                    this.Close();
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("FORNECEDOR");
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Eventos Enter

        private void txtb_ValorUnitarioInicial_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_ValorUnitarioInicial);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorUnitarioFinal_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_ValorUnitarioFinal);
            }
            catch (Exception)
            {

                throw;
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

        private void mtxtb_UltimaAtualizacaoInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Enter_FocusMaskedTxtb(mtxtb_UltimaAtualizacaoFinal, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void mtxtb_UltimaAtualizacaoFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Enter_FocusButton(btn_PesquisaPorUltimaAtualizacao, e))
                    btn_PesquisaPorUltimaAtualizacao_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_Descricao_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Enter_FocusButton(btn_PesquisaPorDescricao, e))
                    btn_PesquisaPorDescricao_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_Grupo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Enter_FocusButton(btn_PesquisaPorGrupo, e))
                    btn_PesquisaPorGrupo_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_Unidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Enter_FocusButton(btn_PesquisaPorUnidade, e))
                    btn_PesquisaPorUnidade_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_AlturaInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_AlturaFinal, e))
                    CampoMoeda_KeyPress(sender, e, false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_AlturaFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusButton(btn_PesquisaPorAltura, e))
                    CampoMoeda_KeyPress(sender, e, false);
                else
                    btn_PesquisaPorAltura_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_LarguraInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_LarguraFinal, e))
                    CampoMoeda_KeyPress(sender, e, false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_LarguraFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusButton(btn_PesquisaPorLargura, e))
                    CampoMoeda_KeyPress(sender, e, false);
                else
                    btn_PesquisaPorLargura_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ComprimentoInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_ComprimentoFinal, e))
                    CampoMoeda_KeyPress(sender, e, false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ComprimentoFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusButton(btn_PesquisaPorComprimento, e))
                    CampoMoeda_KeyPress(sender, e, false);
                else
                    btn_PesquisaPorComprimento_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorUnitarioInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_ValorUnitarioFinal, e))
                    CampoMoeda_KeyPress(sender, e, false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorUnitarioFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusButton(btn_PesquisaPorValorUnitario, e))
                    CampoMoeda_KeyPress(sender, e, false);
                else
                    btn_PesquisaPorValorUnitario_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_Fornecedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Enter_FocusButton(btn_PesquisaPorFornecedor, e))
                    btn_PesquisaPorFornecedor_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Eventos Leave

        private void txtb_AlturaInicial_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_AlturaInicial, false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_AlturaFinal_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_AlturaFinal, false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_LarguraInicial_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_LarguraInicial, false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_LarguraFinal_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_LarguraFinal, false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ComprimentoInicial_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ComprimentoInicial, false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ComprimentoFinal_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ComprimentoFinal, false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorUnitarioInicial_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorUnitarioInicial, true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorUnitarioFinal_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorUnitarioFinal, true);
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
                if (nomeBotao.Equals("ATUALIZACAO INICIAL"))
                {
                    btn_UltimaAtualizacaoAgendaInicial.Visible = false;
                    btn_FecharCalendarioAtualizacaoInicial.Visible = true;
                }
                else
                {
                    btn_UltimaAtualizacaoAgendaInicial.Visible = true;
                    btn_FecharCalendarioAtualizacaoInicial.Visible = false;
                }

                if (nomeBotao.Equals("ATUALIZACAO FINAL"))
                {
                    btn_UltimaAtualizacaoAgendaFinal.Visible = false;
                    btn_FecharCalendarioAtualizacaoFinal.Visible = true;
                }
                else
                {
                    btn_UltimaAtualizacaoAgendaFinal.Visible = true;
                    btn_FecharCalendarioAtualizacaoFinal.Visible = false;
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

        private void btn_UltimaAtualizacaoAgendaInicial_Click(object sender, EventArgs e)
        {
            try
            {
                if (mc_Calendario.Visible.Equals(false))
                    mc_Calendario.Visible = true;

                DefinirEixoXY_Calendario(btn_UltimaAtualizacaoAgendaInicial, mc_Calendario);
                FormularioModoCalendario(true);
                GerenciaAtivacaoButtonsFechar_Calendario("ATUALIZACAO INICIAL");

                DateTime variavelData = DateTime.Today.Date;
                if (DateTime.TryParse(mtxtb_UltimaAtualizacaoInicial.Text.Trim(), out variavelData))
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

        private void btn_UltimaAtualizacaoAgendaFinal_Click(object sender, EventArgs e)
        {
            try
            {
                if (mc_Calendario.Visible.Equals(false))
                    mc_Calendario.Visible = true;

                DefinirEixoXY_Calendario(btn_UltimaAtualizacaoAgendaFinal, mc_Calendario);
                FormularioModoCalendario(true);
                GerenciaAtivacaoButtonsFechar_Calendario("ATUALIZACAO FINAL");

                DateTime variavelData = DateTime.Today.Date;
                if (DateTime.TryParse(mtxtb_UltimaAtualizacaoFinal.Text.Trim(), out variavelData))
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

        private void btn_FecharCalendarioAtualizacaoInicial_Click(object sender, EventArgs e)
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

        private void btn_FecharCalendarioAtualizacaoFinal_Click(object sender, EventArgs e)
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
                if (btn_FecharCalendarioAtualizacaoInicial.Visible.Equals(true))
                {
                    mtxtb_UltimaAtualizacaoInicial.Text = mc_Calendario.SelectionStart.ToShortDateString();
                    btn_FecharCalendarioAtualizacaoInicial_Click(sender, e);
                }
                else if (btn_FecharCalendarioAtualizacaoFinal.Visible.Equals(true))
                {
                    mtxtb_UltimaAtualizacaoFinal.Text = mc_Calendario.SelectionStart.ToShortDateString();
                    btn_FecharCalendarioAtualizacaoFinal_Click(sender, e);
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
