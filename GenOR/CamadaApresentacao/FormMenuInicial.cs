using GenOR.Properties;
using CamadaObjetoTransferencia;
using CamadaProcessamento;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GenOR
{
    public partial class FormMenuInicial : FormBase
    {
        #region Variaveis MENU

        private Pessoa usuario;
        private GerenciarMensagensPadraoSistema gerenciarMensagensPadraoSistema;

        public bool desconexao;

        #endregion

        public FormMenuInicial(Pessoa usuarioLogado)
        {
            try
            {
                InitializeComponent();
                
                desconexao = false;

                usuario = new Pessoa();
                usuario = usuarioLogado;

                gerenciarMensagensPadraoSistema = new GerenciarMensagensPadraoSistema();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
                this.Close();
            }
        }

        #region Funções Gerais

        private void FormMenuInicial_FormClosing(object sender, FormClosingEventArgs e)
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

        private void FormMenuInicial_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                bool maximized = this.WindowState == FormWindowState.Maximized;
                if (maximized)
                {
                    btn_Fornecedor.Font = new Font("Microsoft Sans Serif", 36, FontStyle.Bold);
                    btn_ProdutoServico.Font = new Font("Microsoft Sans Serif", 32, FontStyle.Bold);
                }
                else
                {
                    btn_Fornecedor.Font = new Font("Microsoft Sans Serif", 30, FontStyle.Bold);
                    btn_ProdutoServico.Font = new Font("Microsoft Sans Serif", 21, FontStyle.Bold);
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void AdicionaRemoveImagensMenu(bool adicionarImagens)
        {
            try
            {
                if (adicionarImagens)
                {
                    //this.Icon = Resources.Icon;
                    btn_LOG.Image = Resources.Directory_Listing;
                    btn_Usuario.Image = Resources.Premium_Support;
                    btn_Cliente.Image = Resources.Client_Account_Template;
                    btn_Fornecedor.Image = Resources.Box_Closed;
                    btn_Unidade.Image = Resources.Coin_Single_Gold;
                    btn_Grupo.Image = Resources.Sort_Columns;
                    btn_Material.Image = Resources.Toolbox;
                    btn_ProdutoServico.Image = Resources.Setting_Tools;
                    btn_Orcamento.Image = Resources.Column_Right;
                    btn_Configuracoes.Image = Resources.Cog;

                    pb_ImagemFundo.Image = Resources.Budget_wallpaper;

                    this.Visible = true;
                    //pb_BloqueioTela.SendToBack();
                }
                else
                {
                    this.Visible = false;
                    //pb_BloqueioTela.BringToFront();

                    //this.Icon = null;
                    btn_LOG.Image = null;
                    btn_Usuario.Image = null;
                    btn_Cliente.Image = null;
                    btn_Fornecedor.Image = null;
                    btn_Unidade.Image = null;
                    btn_Grupo.Image = null;
                    btn_Material.Image = null;
                    btn_ProdutoServico.Image = null;
                    btn_Orcamento.Image = null;
                    btn_Configuracoes.Image = null;

                    pb_ImagemFundo.Image = null;
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Button Click

        private void btn_LOG_Click(object sender, EventArgs e)
        {
            try
            {
                using (FormLOG formLog = new FormLOG())
                {
                    Gerenciamento_FormVisivel(false);

                    formLog.ShowDialog();

                    Gerenciamento_FormVisivel(true);
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_Usuario_Click(object sender, EventArgs e)
        {
            try
            {
                using (FormPessoa formUsuario = new FormPessoa(null, "USUARIO", false))
                {
                    Gerenciamento_FormVisivel(false);

                    formUsuario.ShowDialog();

                    Gerenciamento_FormVisivel(true);
                }

                Pessoa item = new Pessoa
                {
                    codigo = usuario.codigo,
                    tipo_pessoa = usuario.tipo_pessoa,
                    ativo_inativo = usuario.ativo_inativo
                };

                ListaPessoa listaPessoa = new ListaPessoa();
                ProcPessoa procPessoa = new ProcPessoa();
                listaPessoa = procPessoa.ConsultarRegistro(item, false);

                if (listaPessoa.Count.Equals(1))
                {
                    desconexao = false;

                    foreach (Pessoa userRetornado in listaPessoa)
                    {
                        usuario = userRetornado;
                        break;
                    }
                }
                else
                {
                    desconexao = true;
                    this.Close();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_Cliente_Click(object sender, EventArgs e)
        {
            try
            {
                using (FormPessoa formCliente = new FormPessoa(null, "CLIENTE", false))
                {
                    Gerenciamento_FormVisivel(false);

                    formCliente.ShowDialog();

                    Gerenciamento_FormVisivel(true);
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_Fornecedor_Click(object sender, EventArgs e)
        {
            try
            {
                using (FormPessoa formFornecedor = new FormPessoa(null, "FORNECEDOR", false))
                {
                    Gerenciamento_FormVisivel(false);

                    formFornecedor.ShowDialog();

                    Gerenciamento_FormVisivel(true);
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_Unidade_Click(object sender, EventArgs e)
        {
            try
            {
                using (FormGrupo_Unidade formUnidade = new FormGrupo_Unidade(null, false, "UNIDADE", 'U'))
                {
                    Gerenciamento_FormVisivel(false);

                    formUnidade.ShowDialog();

                    Gerenciamento_FormVisivel(true);
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_Grupo_Click(object sender, EventArgs e)
        {
            try
            {
                using (FormGrupo_Unidade formGrupo = new FormGrupo_Unidade(null, false, "GRUPO", 'A'))
                {
                    Gerenciamento_FormVisivel(false);

                    formGrupo.ShowDialog();

                    Gerenciamento_FormVisivel(true);
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_Material_Click(object sender, EventArgs e)
        {
            try
            {
                using (FormMaterial formMaterial = new FormMaterial(null, false))
                {
                    Gerenciamento_FormVisivel(false);

                    formMaterial.ShowDialog();

                    Gerenciamento_FormVisivel(true);
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_ProdutoServico_Click(object sender, EventArgs e)
        {
            try
            {
                using (FormProduto_Servico formProduto_Servico = new FormProduto_Servico(null, false))
                {
                    Gerenciamento_FormVisivel(false);

                    formProduto_Servico.ShowDialog();

                    Gerenciamento_FormVisivel(true);
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_Orcamento_Click(object sender, EventArgs e)
        {
            try
            {
                using (FormOrcamento formOrcamento = new FormOrcamento(usuario))
                {
                    Gerenciamento_FormVisivel(false);

                    formOrcamento.ShowDialog();

                    Gerenciamento_FormVisivel(true);
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_Configuracoes_Click(object sender, EventArgs e)
        {
            try
            {
                using (FormBackup_Restore formBackup_Restore = new FormBackup_Restore())
                {
                    AdicionaRemoveImagensMenu(false);

                    formBackup_Restore.ShowDialog();

                    AdicionaRemoveImagensMenu(true);
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
