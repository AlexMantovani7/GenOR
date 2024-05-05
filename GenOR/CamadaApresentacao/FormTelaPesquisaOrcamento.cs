using System;
using System.Drawing;
using System.Windows.Forms;

namespace GenOR
{
    public partial class FormTelaPesquisaOrcamento : FormBase
    {
        #region Variaveis

        public string campoPesquisado;
        public string informaçãoRetornada;
        public string informaçãoRetornada2;

        private GerenciarMensagensPadraoSistema gerenciarMensagensPadraoSistema;

        #endregion

        public FormTelaPesquisaOrcamento()
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
        
        private void btn_PesquisaPorDataValidade_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime resultadoInicial;
                DateTime resultadoFinal;
                if (DateTime.TryParse(mtxtb_DataValidadeInicial.Text.Trim(), out resultadoInicial) && !mtxtb_DataValidadeInicial.Text.Trim().Equals("")
                    && DateTime.TryParse(mtxtb_DataValidadeFinal.Text.Trim(), out resultadoFinal) && !mtxtb_DataValidadeFinal.Text.Trim().Equals(""))
                {
                    if (resultadoInicial.Date <= resultadoFinal.Date)
                    {
                        campoPesquisado = "DATA VALIDADE";
                        informaçãoRetornada = resultadoInicial.ToString();
                        informaçãoRetornada2 = resultadoFinal.ToString();
                        
                        this.Close();
                    }
                    else
                        gerenciarMensagensPadraoSistema.CampoDuploDataNullBrancoOuDatado("DATA VALIDADE (INICIAL) E (FINAL)");
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("DATA VALIDADE");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_PesquisaPorRazaoSocialCliente_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtb_RazaoSocialCliente.Text.Trim().Equals(""))
                {
                    campoPesquisado = "RAZÃO SOCIAL";
                    informaçãoRetornada = txtb_RazaoSocialCliente.Text.ToString();
                    
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

        private void btn_PesquisaPorValorEntrada_Click(object sender, EventArgs e)
        {
            try
            {
                decimal resultadoInicial;
                decimal resultadoFinal;

                txtb_ValorEntradaInicial_Enter(sender, e);
                txtb_ValorEntradaFinal_Enter(sender, e);

                if (decimal.TryParse(txtb_ValorEntradaInicial.Text.Trim(), out resultadoInicial) && !txtb_ValorEntradaInicial.Text.Trim().Equals("")
                    && decimal.TryParse(txtb_ValorEntradaFinal.Text.Trim(), out resultadoFinal) && !txtb_ValorEntradaFinal.Text.Trim().Equals(""))
                {
                    if (resultadoInicial >= 0)
                    {
                        if (resultadoFinal >= 0)
                        {
                            if (resultadoInicial <= resultadoFinal)
                            {
                                campoPesquisado = "VALOR ENTRADA";
                                informaçãoRetornada = resultadoInicial.ToString();
                                informaçãoRetornada2 = resultadoFinal.ToString();
                                
                                this.Close();
                            }
                            else
                                gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("VALOR ENTRADA (INICIAL) E (FINAL)");
                        }
                        else
                            gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("VALOR ENTRADA (FINAL)");
                    }
                    else
                        gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("VALOR ENTRADA (INICIAL)");
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("VALOR ENTRADA");

                txtb_ValorEntradaInicial_Leave(sender, e);
                txtb_ValorEntradaFinal_Leave(sender, e);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_PesquisaPorQuantidadeParcelas_Click(object sender, EventArgs e)
        {
            try
            {
                int resultadoInicial;
                int resultadoFinal;

                if (int.TryParse(txtb_QuantidadeParcelasInicial.Text.Trim(), out resultadoInicial) && !txtb_QuantidadeParcelasInicial.Text.Trim().Equals("")
                    && int.TryParse(txtb_QuantidadeParcelasFinal.Text.Trim(), out resultadoFinal) && !txtb_QuantidadeParcelasFinal.Text.Trim().Equals(""))
                {
                    if (resultadoInicial >= 1)
                    {
                        if (resultadoFinal >= 1)
                        {
                            if (resultadoInicial <= resultadoFinal)
                            {
                                campoPesquisado = "QUANTIDADE PARCELAS";
                                informaçãoRetornada = resultadoInicial.ToString();
                                informaçãoRetornada2 = resultadoFinal.ToString();
                                
                                this.Close();
                            }
                            else
                                gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("QUANTIDADE PARCELAS (INICIAL) E (FINAL)");
                        }
                        else
                            gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("QUANTIDADE PARCELAS (FINAL)");
                    }
                    else
                        gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("QUANTIDADE PARCELAS (INICIAL)");
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("QUANTIDADE PARCELAS");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_PesquisaPorValorParcela_Click(object sender, EventArgs e)
        {
            try
            {
                decimal resultadoInicial;
                decimal resultadoFinal;

                txtb_ValorParcelaInicial_Enter(sender, e);
                txtb_ValorParcelaFinal_Enter(sender, e);

                if (decimal.TryParse(txtb_ValorParcelaInicial.Text.Trim(), out resultadoInicial) && !txtb_ValorParcelaInicial.Text.Trim().Equals("")
                    && decimal.TryParse(txtb_ValorParcelaFinal.Text.Trim(), out resultadoFinal) && !txtb_ValorParcelaFinal.Text.Trim().Equals(""))
                {
                    if (resultadoInicial >= 0)
                    {
                        if (resultadoFinal > 0)
                        {
                            if (resultadoInicial <= resultadoFinal)
                            {
                                campoPesquisado = "VALOR PARCELA";
                                informaçãoRetornada = resultadoInicial.ToString();
                                informaçãoRetornada2 = resultadoFinal.ToString();
                                
                                this.Close();
                            }
                            else
                                gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("VALOR PARCELA (INICIAL) E (FINAL)");
                        }
                        else
                            gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("VALOR PARCELA (FINAL)");
                    }
                    else
                        gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("VALOR PARCELA (INICIAL)");
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("VALOR PARCELA");

                txtb_ValorParcelaInicial_Leave(sender, e);
                txtb_ValorParcelaFinal_Leave(sender, e);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_PesquisaPorJuros_Click(object sender, EventArgs e)
        {
            try
            {
                decimal resultadoInicial;
                decimal resultadoFinal;

                txtb_JurosInicial_Enter(sender, e);
                txtb_JurosFinal_Enter(sender, e);

                if (decimal.TryParse(txtb_JurosInicial.Text.Trim(), out resultadoInicial) && !txtb_JurosInicial.Text.Trim().Equals("")
                    && decimal.TryParse(txtb_JurosFinal.Text.Trim(), out resultadoFinal) && !txtb_JurosFinal.Text.Trim().Equals(""))
                {
                    if (resultadoInicial >= 0)
                    {
                        if (resultadoFinal >= 0)
                        {
                            if (resultadoInicial <= resultadoFinal)
                            {
                                campoPesquisado = "JUROS";
                                informaçãoRetornada = resultadoInicial.ToString();
                                informaçãoRetornada2 = resultadoFinal.ToString();
                                
                                this.Close();
                            }
                            else
                                gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("JUROS (INICIAL) E (FINAL)");
                        }
                        else
                            gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("JUROS (FINAL)");
                    }
                    else
                        gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("JUROS (INICIAL)");
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("JUROS");

                txtb_JurosInicial_Leave(sender, e);
                txtb_JurosFinal_Leave(sender, e);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_PesquisaPorValorJuros_Click(object sender, EventArgs e)
        {
            try
            {
                decimal resultadoInicial;
                decimal resultadoFinal;

                txtb_ValorJurosInicial_Enter(sender, e);
                txtb_ValorJurosFinal_Enter(sender, e);

                if (decimal.TryParse(txtb_ValorJurosInicial.Text.Trim(), out resultadoInicial) && !txtb_ValorJurosInicial.Text.Trim().Equals("")
                    && decimal.TryParse(txtb_ValorJurosFinal.Text.Trim(), out resultadoFinal) && !txtb_ValorJurosFinal.Text.Trim().Equals(""))
                {
                    if (resultadoInicial >= 0)
                    {
                        if (resultadoFinal >= 0)
                        {
                            if (resultadoInicial <= resultadoFinal)
                            {
                                campoPesquisado = "VALOR JUROS";
                                informaçãoRetornada = resultadoInicial.ToString();
                                informaçãoRetornada2 = resultadoFinal.ToString();
                                
                                this.Close();
                            }
                            else
                                gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("VALOR JUROS (INICIAL) E (FINAL)");
                        }
                        else
                            gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("VALOR JUROS (FINAL)");
                    }
                    else
                        gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("VALOR JUROS (INICIAL)");
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("VALOR JUROS");

                txtb_ValorJurosInicial_Leave(sender, e);
                txtb_ValorJurosFinal_Leave(sender, e);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_PesquisaPorValorProdutoServico_Click(object sender, EventArgs e)
        {
            try
            {
                decimal resultadoInicial;
                decimal resultadoFinal;

                txtb_ValorProdutoServicoInicial_Enter(sender, e);
                txtb_ValorProdutoServicoFinal_Enter(sender, e);

                if (decimal.TryParse(txtb_ValorProdutoServicoInicial.Text.Trim(), out resultadoInicial) && !txtb_ValorProdutoServicoInicial.Text.Trim().Equals("")
                    && decimal.TryParse(txtb_ValorProdutoServicoFinal.Text.Trim(), out resultadoFinal) && !txtb_ValorProdutoServicoFinal.Text.Trim().Equals(""))
                {
                    if (resultadoInicial >= 0)
                    {
                        if (resultadoFinal > 0)
                        {
                            if (resultadoInicial <= resultadoFinal)
                            {
                                campoPesquisado = "VALOR PRODUTOS/SERVIÇOS";
                                informaçãoRetornada = resultadoInicial.ToString();
                                informaçãoRetornada2 = resultadoFinal.ToString();
                                
                                this.Close();
                            }
                            else
                                gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("VALOR PRODUTOS/SERVIÇOS (INICIAL) E (FINAL)");
                        }
                        else
                            gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("VALOR PRODUTOS/SERVIÇOS (FINAL)");
                    }
                    else
                        gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("VALOR PRODUTOS/SERVIÇOS (INICIAL)");
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("VALOR PRODUTOS/SERVIÇOS");

                txtb_ValorProdutoServicoInicial_Leave(sender, e);
                txtb_ValorProdutoServicoFinal_Leave(sender, e);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_PesquisaPorDesconto_Click(object sender, EventArgs e)
        {
            try
            {
                decimal resultadoInicial;
                decimal resultadoFinal;

                txtb_DescontoInicial_Enter(sender, e);
                txtb_DescontoFinal_Enter(sender, e);

                if (decimal.TryParse(txtb_DescontoInicial.Text.Trim(), out resultadoInicial) && !txtb_DescontoInicial.Text.Trim().Equals("")
                    && decimal.TryParse(txtb_DescontoFinal.Text.Trim(), out resultadoFinal) && !txtb_DescontoFinal.Text.Trim().Equals(""))
                {
                    if (resultadoInicial >= 0)
                    {
                        if (resultadoFinal >= 0)
                        {
                            if (resultadoInicial <= resultadoFinal)
                            {
                                campoPesquisado = "DESCONTO";
                                informaçãoRetornada = resultadoInicial.ToString();
                                informaçãoRetornada2 = resultadoFinal.ToString();
                                
                                this.Close();
                            }
                            else
                                gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("DESCONTO (INICIAL) E (FINAL)");
                        }
                        else
                            gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("DESCONTO (FINAL)");
                    }
                    else
                        gerenciarMensagensPadraoSistema.CampoDuploMoedaNullBranco("DESCONTO (INICIAL)");
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("DESCONTO");

                txtb_DescontoInicial_Leave(sender, e);
                txtb_DescontoFinal_Leave(sender, e);
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

        private void txtb_ValorEntradaInicial_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_ValorEntradaInicial);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorEntradaFinal_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_ValorEntradaFinal);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorParcelaInicial_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_ValorParcelaInicial);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorParcelaFinal_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_ValorParcelaFinal);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_JurosInicial_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_JurosInicial);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_JurosFinal_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_JurosFinal);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorJurosInicial_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_ValorJurosInicial);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorJurosFinal_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_ValorJurosFinal);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorProdutoServicoInicial_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_ValorProdutoServicoInicial);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorProdutoServicoFinal_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_ValorProdutoServicoFinal);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_DescontoInicial_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_DescontoInicial);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_DescontoFinal_Enter(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_RemoverFormatacao(txtb_DescontoFinal);
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

        private void mtxtb_DataValidadeInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Enter_FocusMaskedTxtb(mtxtb_DataValidadeFinal, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void mtxtb_DataValidadeFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Enter_FocusButton(btn_PesquisaPorDataValidade, e))
                    btn_PesquisaPorDataValidade_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_RazaoSocialCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Enter_FocusButton(btn_PesquisaPorRazaoSocialCliente, e))
                    btn_PesquisaPorRazaoSocialCliente_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorEntradaInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_ValorEntradaFinal, e))
                    CampoMoeda_KeyPress(sender, e, false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorEntradaFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusButton(btn_PesquisaPorValorEntrada, e))
                    CampoMoeda_KeyPress(sender, e, false);
                else
                    btn_PesquisaPorValorEntrada_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_QuantidadeParcelasInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_QuantidadeParcelasFinal, e))
                    CampoMoeda_KeyPress(sender, e, true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_QuantidadeParcelasFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusButton(btn_PesquisaPorQuantidadeParcelas, e))
                    CampoMoeda_KeyPress(sender, e, true);
                else
                    btn_PesquisaPorQuantidadeParcelas_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorParcelaInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_ValorParcelaFinal, e))
                    CampoMoeda_KeyPress(sender, e, false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorParcelaFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusButton(btn_PesquisaPorValorParcela, e))
                    CampoMoeda_KeyPress(sender, e, false);
                else
                    btn_PesquisaPorValorParcela_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_JurosInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_JurosFinal, e))
                    CampoMoeda_KeyPress(sender, e, false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_JurosFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusButton(btn_PesquisaPorJuros, e))
                    CampoMoeda_KeyPress(sender, e, false);
                else
                    btn_PesquisaPorJuros_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorJurosInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_ValorJurosFinal, e))
                    CampoMoeda_KeyPress(sender, e, false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorJurosFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusButton(btn_PesquisaPorValorJuros, e))
                    CampoMoeda_KeyPress(sender, e, false);
                else
                    btn_PesquisaPorValorJuros_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorProdutoServicoInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_ValorProdutoServicoFinal, e))
                    CampoMoeda_KeyPress(sender, e, false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorProdutoServicoFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusButton(btn_PesquisaPorValorProdutoServico, e))
                    CampoMoeda_KeyPress(sender, e, false);
                else
                    btn_PesquisaPorValorProdutoServico_Click(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_DescontoInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_DescontoFinal, e))
                    CampoMoeda_KeyPress(sender, e, false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_DescontoFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusButton(btn_PesquisaPorDesconto, e))
                    CampoMoeda_KeyPress(sender, e, false);
                else
                    btn_PesquisaPorDesconto_Click(sender, e);
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

        private void txtb_ValorEntradaInicial_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorEntradaInicial, true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorEntradaFinal_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorEntradaFinal, true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorParcelaInicial_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorParcelaInicial, true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorParcelaFinal_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorParcelaFinal, true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_JurosInicial_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_JurosInicial, false);
                CampoFormatacao_Porcentagem(txtb_JurosInicial);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_JurosFinal_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_JurosFinal, false);
                CampoFormatacao_Porcentagem(txtb_JurosFinal);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorJurosInicial_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorJurosInicial, true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorJurosFinal_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorJurosFinal, true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorProdutoServicoInicial_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorProdutoServicoInicial, true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_ValorProdutoServicoFinal_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_ValorProdutoServicoFinal, true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_DescontoInicial_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_DescontoInicial, false);
                CampoFormatacao_Porcentagem(txtb_DescontoInicial);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtb_DescontoFinal_Leave(object sender, EventArgs e)
        {
            try
            {
                CampoMoeda_FormatacaoDecimal(txtb_DescontoFinal, false);
                CampoFormatacao_Porcentagem(txtb_DescontoFinal);
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
                    this.Size = new Size(this.Width, 285);
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
                    btn_FecharCalendarioAtualizacaoInicial_Orcamento.Visible = true;
                }
                else
                {
                    btn_UltimaAtualizacaoAgendaInicial.Visible = true;
                    btn_FecharCalendarioAtualizacaoInicial_Orcamento.Visible = false;
                }
                
                if (nomeBotao.Equals("ATUALIZACAO FINAL"))
                {
                    btn_UltimaAtualizacaoAgendaFinal.Visible = false;
                    btn_FecharCalendarioAtualizacaoFinal_Orcamento.Visible = true;
                }
                else
                {
                    btn_UltimaAtualizacaoAgendaFinal.Visible = true;
                    btn_FecharCalendarioAtualizacaoFinal_Orcamento.Visible = false;
                }
                
                if (nomeBotao.Equals("VALIDADE INICIAL"))
                {
                    btn_DataValidadeAgendaInicial.Visible = false;
                    btn_FecharCalendarioValidadeInicial_Orcamento.Visible = true;
                }
                else
                {
                    btn_DataValidadeAgendaInicial.Visible = true;
                    btn_FecharCalendarioValidadeInicial_Orcamento.Visible = false;
                }
                
                if (nomeBotao.Equals("VALIDADE FINAL"))
                {
                    btn_DataValidadeAgendaFinal.Visible = false;
                    btn_FecharCalendarioValidadeFinal_Orcamento.Visible = true;
                }
                else
                {
                    btn_DataValidadeAgendaFinal.Visible = true;
                    btn_FecharCalendarioValidadeFinal_Orcamento.Visible = false;
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
                if (mc_Calendario_Orcamento.Visible.Equals(true))
                {
                    mc_Calendario_Orcamento.Visible = false;
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
                if (mc_Calendario_Orcamento.Visible.Equals(false))
                    mc_Calendario_Orcamento.Visible = true;

                DefinirEixoXY_Calendario(btn_UltimaAtualizacaoAgendaInicial, mc_Calendario_Orcamento);
                FormularioModoCalendario(true);
                GerenciaAtivacaoButtonsFechar_Calendario("ATUALIZACAO INICIAL");

                DateTime variavelData = DateTime.Today.Date;
                if (DateTime.TryParse(mtxtb_UltimaAtualizacaoInicial.Text.Trim(), out variavelData))
                    mc_Calendario_Orcamento.SetDate(variavelData);
                else
                    mc_Calendario_Orcamento.SetDate(DateTime.Today);

                mc_Calendario_Orcamento.MinDate = DateTime.Parse("01/01/1753");
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
                if (mc_Calendario_Orcamento.Visible.Equals(false))
                    mc_Calendario_Orcamento.Visible = true;

                DefinirEixoXY_Calendario(btn_UltimaAtualizacaoAgendaFinal, mc_Calendario_Orcamento);
                FormularioModoCalendario(true);
                GerenciaAtivacaoButtonsFechar_Calendario("ATUALIZACAO FINAL");

                DateTime variavelData = DateTime.Today.Date;
                if (DateTime.TryParse(mtxtb_UltimaAtualizacaoFinal.Text.Trim(), out variavelData))
                    mc_Calendario_Orcamento.SetDate(variavelData);
                else
                    mc_Calendario_Orcamento.SetDate(DateTime.Today);

                mc_Calendario_Orcamento.MinDate = DateTime.Parse("01/01/1753");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_FecharCalendarioAtualizacaoInicial_Orcamento_Click(object sender, EventArgs e)
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

        private void btn_FecharCalendarioAtualizacaoFinal_Orcamento_Click(object sender, EventArgs e)
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

        private void btn_DataValidadeAgendaInicial_Click(object sender, EventArgs e)
        {
            try
            {
                if (mc_Calendario_Orcamento.Visible.Equals(false))
                    mc_Calendario_Orcamento.Visible = true;

                DefinirEixoXY_Calendario(btn_DataValidadeAgendaInicial, mc_Calendario_Orcamento);
                FormularioModoCalendario(true);
                GerenciaAtivacaoButtonsFechar_Calendario("VALIDADE INICIAL");

                DateTime variavelData = DateTime.Today.Date;
                if (DateTime.TryParse(mtxtb_DataValidadeInicial.Text.Trim(), out variavelData))
                    mc_Calendario_Orcamento.SetDate(variavelData);
                else
                    mc_Calendario_Orcamento.SetDate(DateTime.Today);

                mc_Calendario_Orcamento.MinDate = DateTime.Parse("01/01/1753");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_DataValidadeAgendaFinal_Click(object sender, EventArgs e)
        {
            try
            {
                if (mc_Calendario_Orcamento.Visible.Equals(false))
                    mc_Calendario_Orcamento.Visible = true;

                DefinirEixoXY_Calendario(btn_DataValidadeAgendaFinal, mc_Calendario_Orcamento);
                FormularioModoCalendario(true);
                GerenciaAtivacaoButtonsFechar_Calendario("VALIDADE FINAL");

                DateTime variavelData = DateTime.Today.Date;
                if (DateTime.TryParse(mtxtb_DataValidadeFinal.Text.Trim(), out variavelData))
                    mc_Calendario_Orcamento.SetDate(variavelData);
                else
                    mc_Calendario_Orcamento.SetDate(DateTime.Today);

                mc_Calendario_Orcamento.MinDate = DateTime.Parse("01/01/1753");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_FecharCalendarioValidadeInicial_Orcamento_Click(object sender, EventArgs e)
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

        private void btn_FecharCalendarioValidadeFinal_Orcamento_Click(object sender, EventArgs e)
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
        
        private void mc_Calendario_Orcamento_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                if (btn_FecharCalendarioAtualizacaoInicial_Orcamento.Visible.Equals(true))
                {
                    mtxtb_UltimaAtualizacaoInicial.Text = mc_Calendario_Orcamento.SelectionStart.ToShortDateString();
                    btn_FecharCalendarioAtualizacaoInicial_Orcamento_Click(sender, e);
                }
                else if (btn_FecharCalendarioAtualizacaoFinal_Orcamento.Visible.Equals(true))
                {
                    mtxtb_UltimaAtualizacaoFinal.Text = mc_Calendario_Orcamento.SelectionStart.ToShortDateString();
                    btn_FecharCalendarioAtualizacaoFinal_Orcamento_Click(sender, e);
                }
                else if (btn_FecharCalendarioValidadeInicial_Orcamento.Visible.Equals(true))
                {
                    mtxtb_DataValidadeInicial.Text = mc_Calendario_Orcamento.SelectionStart.ToShortDateString();
                    btn_FecharCalendarioValidadeInicial_Orcamento_Click(sender, e);
                }
                else if (btn_FecharCalendarioValidadeFinal_Orcamento.Visible.Equals(true))
                {
                    mtxtb_DataValidadeFinal.Text = mc_Calendario_Orcamento.SelectionStart.ToShortDateString();
                    btn_FecharCalendarioValidadeFinal_Orcamento_Click(sender, e);
                }
                else
                    mc_Calendario_Orcamento.Visible = false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

    }
}
