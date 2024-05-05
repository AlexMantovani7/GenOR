using System;
using System.Windows.Forms;

namespace GenOR
{
    public partial class FormTelaPesquisaGrupo_Unidade : FormBase
    {
        #region Variaveis

        public string campoPesquisado;
        public string informaçãoRetornada;

        private GerenciarMensagensPadraoSistema gerenciarMensagensPadraoSistema;

        private string formularioGrupo_Ou_Unidade;

        #endregion

        public FormTelaPesquisaGrupo_Unidade(string formularioGrupo_Ou_Unidade)
        {
            try
            {
                InitializeComponent();

                campoPesquisado = "CANCELADO";
                informaçãoRetornada = "VAZIA";

                gerenciarMensagensPadraoSistema = new GerenciarMensagensPadraoSistema();

                this.StartPosition = FormStartPosition.Manual;
                this.StartPosition = FormStartPosition.CenterParent;

                this.formularioGrupo_Ou_Unidade = formularioGrupo_Ou_Unidade;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region Eventos Form

        private void FormTelaPesquisaGrupo_Unidade_Load(object sender, EventArgs e)
        {
            try
            {
                if (formularioGrupo_Ou_Unidade.Equals("GRUPO"))
                    tc_Pesquisa.TabPages.Remove(tcp_Sigla);
                else if (formularioGrupo_Ou_Unidade.Equals("UNIDADE"))
                    tc_Pesquisa.TabPages.Remove(tcp_Material_Ou_Produto);
                
                this.Text = "PESQUISANDO " + formularioGrupo_Ou_Unidade;
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

        private void btn_PesquisaPorSigla_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtb_Sigla.Text.Trim().Equals(""))
                {
                    campoPesquisado = "SIGLA";
                    informaçãoRetornada = txtb_Sigla.Text.ToString();
                    
                    this.Close();
                }
                else
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("SIGLA");
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

        private void btn_PesquisaPorMaterial_Ou_Produto_Click(object sender, EventArgs e)
        {
            try
            {
                if (rb_Material.Checked.Equals(true))
                    informaçãoRetornada = "M";
                else if (rb_Produto.Checked.Equals(true))
                    informaçãoRetornada = "P";
                else
                {
                    gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("MATERIAL OU PRODUTO");
                    return;
                }

                campoPesquisado = "MATERIAL OU PRODUTO";
                this.Close();
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

        private void txtb_Sigla_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Enter_FocusButton(btn_PesquisaPorSigla, e))
                    btn_PesquisaPorSigla_Click(sender, e);
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

        #endregion

    }
}
