using System;
using System.Drawing;
using System.Windows.Forms;

namespace GenOR
{
    public partial class FormTelaPesquisaProduto_Servico : FormBase
    {
        #region Variaveis

        public string campoPesquisado;
        public string informaçãoRetornada;
        public string informaçãoRetornada2;

        private GerenciarMensagensPadraoSistema gerenciarMensagensPadraoSistema;

        #endregion

        public FormTelaPesquisaProduto_Servico()
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

        private void btn_PesquisaPorDescricaoProdutoServico_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtb_DescricaoProdutoServico.Text.Trim().Equals(""))
                {
                    campoPesquisado = "DESCRIÇÃO";
                    informaçãoRetornada = txtb_DescricaoProdutoServico.Text.ToString();
                    
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
                if (!txtb_Grupo.Text.Trim().Equals(""))
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

        private void btn_PesquisaPorMaoObra_Click(object sender, EventArgs e)
        {
            try
            {
                decimal resultadoInicial;
                decimal resultadoFinal;

                txtb_MaoObraInicial_Enter(sender, e);
                txtb_MaoObraFinal_Enter(sender, e);

                if (decimal.TryParse(txtb_MaoObraInicial.Text.Trim(), out resultadoInicial) && !txtb_MaoObraInicial.Text.Trim().Equals("")
                    && decimal.TryParse(txtb_MaoObraFinal.Text.Trim(), out resultadoFinal) && !txtb_MaoObraFinal.Text.Trim().Equals(""))
                {
                    if (resultadoInicial >= 0)
                    {
                        if (resultadoFinal > 0)
                        {
                            if (resultadoInicial <= resultadoFinal)
                            {
                                campoPesquisado = "MÃO OBRA";
                                informaçãoRetornada = resultadoInicial.ToString();
                                informaçãoRetornada2 = resultadoFinal.ToString();
                                
                                this.Close();
                            }
                            else
                                gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("MÃO OBRA (INICIAL) E (FINAL)");
                        }
                        else
                            gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("MÃO OBRA (FINAL)");
                    }
                    else
                        gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("MÃO OBRA (INICIAL)");
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("MÃO OBRA");

                txtb_MaoObraInicial_Leave(sender, e);
                txtb_MaoObraFinal_Leave(sender, e);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_PesquisaPorValorMateriais_Click(object sender, EventArgs e)
        {
            try
            {
                decimal resultadoInicial;
                decimal resultadoFinal;

                txtb_ValorMateriaisInicial_Enter(sender, e);
                txtb_ValorMateriaisFinal_Enter(sender, e);

                if (decimal.TryParse(txtb_ValorMateriaisInicial.Text.Trim(), out resultadoInicial) && !txtb_ValorMateriaisInicial.Text.Trim().Equals("")
                    && decimal.TryParse(txtb_ValorMateriaisFinal.Text.Trim(), out resultadoFinal) && !txtb_ValorMateriaisFinal.Text.Trim().Equals(""))
                {
                    if (resultadoInicial >= 0)
                    {
                        if (resultadoFinal > 0)
                        {
                            if (resultadoInicial <= resultadoFinal)
                            {
                                campoPesquisado = "VALOR MATERIAIS";
                                informaçãoRetornada = resultadoInicial.ToString();
                                informaçãoRetornada2 = resultadoFinal.ToString();
                                
                                this.Close();
                            }
                            else
                                gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("VALOR MATERIAIS (INICIAL) E (FINAL)");
                        }
                        else
                            gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("VALOR MATERIAIS (FINAL)");
                    }
                    else
                        gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("VALOR MATERIAIS (INICIAL)");
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("VALOR MATERIAIS");

                txtb_ValorMateriaisInicial_Leave(sender, e);
                txtb_ValorMateriaisFinal_Leave(sender, e);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_PesquisaPorValorMaoObra_Click(object sender, EventArgs e)
        {
            try
            {
                decimal resultadoInicial;
                decimal resultadoFinal;

                txtb_ValorMaoObraInicial_Enter(sender, e);
                txtb_ValorMaoObraFinal_Enter(sender, e);

                if (decimal.TryParse(txtb_ValorMaoObraInicial.Text.Trim(), out resultadoInicial) && !txtb_ValorMaoObraInicial.Text.Trim().Equals("")
                    && decimal.TryParse(txtb_ValorMaoObraFinal.Text.Trim(), out resultadoFinal) && !txtb_ValorMaoObraFinal.Text.Trim().Equals(""))
                {
                    if (resultadoInicial >= 0)
                    {
                        if (resultadoFinal > 0)
                        {
                            if (resultadoInicial <= resultadoFinal)
                            {
                                campoPesquisado = "VALOR MÃO OBRA";
                                informaçãoRetornada = resultadoInicial.ToString();
                                informaçãoRetornada2 = resultadoFinal.ToString();
                                
                                this.Close();
                            }
                            else
                                gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("VALOR MÃO OBRA (INICIAL) E (FINAL)");
                        }
                        else
                            gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("VALOR MÃO OBRA (FINAL)");
                    }
                    else
                        gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("VALOR MÃO OBRA (INICIAL)");
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("VALOR MÃO OBRA");

                txtb_ValorMaoObraInicial_Leave(sender, e);
                txtb_ValorMaoObraFinal_Leave(sender, e);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_PesquisaPorValorTotal_Click(object sender, EventArgs e)
        {
            try
            {
                decimal resultadoInicial;
                decimal resultadoFinal;

                txtb_ValorTotalInicial_Enter(sender, e);
                txtb_ValorTotalFinal_Enter(sender, e);

                if (decimal.TryParse(txtb_ValorTotalInicial.Text.Trim(), out resultadoInicial) && !txtb_ValorTotalInicial.Text.Trim().Equals("")
                    && decimal.TryParse(txtb_ValorTotalFinal.Text.Trim(), out resultadoFinal) && !txtb_ValorTotalFinal.Text.Trim().Equals(""))
                {
                    if (resultadoInicial >= 0)
                    {
                        if (resultadoFinal > 0)
                        {
                            if (resultadoInicial <= resultadoFinal)
                            {
                                campoPesquisado = "VALOR TOTAL";
                                informaçãoRetornada = resultadoInicial.ToString();
                                informaçãoRetornada2 = resultadoFinal.ToString();
                                
                                this.Close();
                            }
                            else
                                gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("VALOR TOTAL (INICIAL) E (FINAL)");
                        }
                        else
                            gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("VALOR TOTAL (FINAL)");
                    }
                    else
                        gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("VALOR TOTAL (INICIAL)");
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("VALOR TOTAL");

                txtb_ValorTotalInicial_Leave(sender, e);
                txtb_ValorTotalFinal_Leave(sender, e);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Eventos Enter

        private void txtb_MaoObraInicial_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_MaoObraInicial);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_MaoObraFinal_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_MaoObraFinal);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorMateriaisInicial_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_ValorMateriaisInicial);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorMateriaisFinal_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_ValorMateriaisFinal);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorMaoObraInicial_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_ValorMaoObraInicial);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorMaoObraFinal_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_ValorMaoObraFinal);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorTotalInicial_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_ValorTotalInicial);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorTotalFinal_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_ValorTotalFinal);
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
                if (!Enter_FocusMaskedTxtb(mtxtb_UltimaAtualizacaoFinal, e))
                    CampoMoeda_KeyPress(sender, e, false);
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
                if (!Enter_FocusButton(btn_PesquisaPorUltimaAtualizacao, e))
                    CampoMoeda_KeyPress(sender, e, true);
                else
                    btn_PesquisaPorUltimaAtualizacao_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_DescricaoProdutoServico_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Enter_FocusButton(btn_PesquisaPorDescricaoProdutoServico, e))
                    btn_PesquisaPorDescricaoProdutoServico_Click(sender, e);
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

        private void txtb_MaoObraInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_MaoObraFinal, e))
                    CampoMoeda_KeyPress(sender, e, false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_MaoObraFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusButton(btn_PesquisaPorMaoObra, e))
                    CampoMoeda_KeyPress(sender, e, false);
                else
                    btn_PesquisaPorMaoObra_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorMateriaisInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_ValorMateriaisFinal, e))
                    CampoMoeda_KeyPress(sender, e, false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorMateriaisFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusButton(btn_PesquisaPorValorMateriais, e))
                    CampoMoeda_KeyPress(sender, e, false);
                else
                    btn_PesquisaPorValorMateriais_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorMaoObraInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_ValorMaoObraFinal, e))
                    CampoMoeda_KeyPress(sender, e, false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorMaoObraFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusButton(btn_PesquisaPorValorMaoObra, e))
                    CampoMoeda_KeyPress(sender, e, false);
                else
                    btn_PesquisaPorValorMaoObra_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorTotalInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_ValorTotalFinal, e))
                    CampoMoeda_KeyPress(sender, e, false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorTotalFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusButton(btn_PesquisaPorValorTotal, e))
                    CampoMoeda_KeyPress(sender, e, false);
                else
                    btn_PesquisaPorValorTotal_Click(sender, e);
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

        private void txtb_MaoObraInicial_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_MaoObraInicial, false);
                CampoFormatacao_Porcentagem(txtb_MaoObraInicial);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_MaoObraFinal_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_MaoObraFinal, false);
                CampoFormatacao_Porcentagem(txtb_MaoObraFinal);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorMateriaisInicial_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorMateriaisInicial, true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorMateriaisFinal_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorMateriaisFinal, true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorMaoObraInicial_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorMaoObraInicial, true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorMaoObraFinal_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorMaoObraFinal, true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorTotalInicial_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorTotalInicial, true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorTotalFinal_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorTotalFinal, true);
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
