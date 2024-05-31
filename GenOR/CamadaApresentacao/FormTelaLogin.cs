using GenOR.Properties;
using CamadaObjetoTransferencia;
using CamadaProcessamento;
using System;
using System.Threading;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Diagnostics;
using System.IO;

namespace GenOR
{
    public partial class FormTelaLogin : FormBase
    {
        #region Variaveis Login

        private Login login;
        private ListaLogin listaLogin;
        private ProcLogin procLogin;

        private string imagemButtomSenha;

        private GerenciarMensagensPadraoSistema gerenciarMensagensPadraoSistema;

        private GerenciarTelaLoading gerenciarTelaLoading;
        private FormTelaLoading formTelaLoading;
        private Thread carregarThread;
        private bool telaLoadingFoiAberta;

        #endregion

        public FormTelaLogin()
        {
            try
            {
                InitializeComponent();

                login = new Login()
                {
                    Usuario = new Pessoa()
                };
                listaLogin = new ListaLogin();
                procLogin = new ProcLogin();

                imagemButtomSenha = "ESCONDIDO";

                gerenciarMensagensPadraoSistema = new GerenciarMensagensPadraoSistema();
                
                gerenciarTelaLoading = new GerenciarTelaLoading();
                formTelaLoading = new FormTelaLoading();
                carregarThread = null;
                telaLoadingFoiAberta = false;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
                this.Close();
            }
        }

        #region Eventos Form

        private void FormTelaLogin_Load(object sender, EventArgs e)
        {
            try
            {
                if (CheckMySql_EstaInstalado())
                {
                    ProcBD procBD = new ProcBD();

                    if (procBD.Check_PrimeiraBDConnection())
                    {
                        bool conexaoBD = false;
                        using (FormConfiguracaoInicialBD fmrConfiguracaoInicialBD = new FormConfiguracaoInicialBD())
                        {
                            AdicionaRemoveImagensBotoes(false);
                            Gerenciamento_FormVisivel(false);

                            fmrConfiguracaoInicialBD.ShowDialog();
                            conexaoBD = fmrConfiguracaoInicialBD.conexaoBD;
                        }

                        if (conexaoBD)
                        {
                            AdicionaRemoveImagensBotoes(true);
                            Gerenciamento_FormVisivel(true);
                        }
                        else
                            this.Close();
                    }
                }
                else
                {
                    gerenciarMensagensPadraoSistema.ExceptionBancoDados("O banco de dados MySQL não está ATIVO ou não foi INSTALADO.\n\n" +
                        "PROCEDIMENTOS POSSIVEIS:\n" +
                        "1 - Verifique se o SERVIÇO MySQL está ATIVO ou contem nomenclatura correta (Exs: 'MySql' ou MySQL mais algum número de 1 até 80 'MySql80').\n" +
                        "2 - Prossiga com a INSTALAÇÃO do banco de dados MySQL que será executado logo após a mensagem.\n" +
                        "3 - Entre em contato com o DESENVOLVEDOR do sistema.");

                    Process.Start(Localizar_Imagem_Documento("mysql-installer-community-8.0.31.0.msi", false));
                    this.Close();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
                this.Close();
            }
        }

        #endregion

        #region Funções Gerais
        
        private bool CheckMySql_EstaInstalado()
        {
            try
            {
                string nome_MySqlService = "MySql";
                int contador_NomeService = 1;

                ServiceController serviceController;

                while (nome_MySqlService != "MySql81")
                {
                    serviceController = new ServiceController(nome_MySqlService);

                    try
                    {
                        if (serviceController.Status.Equals(ServiceControllerStatus.Running))
                            return true;
                    }
                    catch (Exception)
                    {
                        nome_MySqlService = "MySql" + contador_NomeService.ToString();
                        contador_NomeService++;
                    }
                }
                
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void AdicionaRemoveImagensBotoes(bool adicionarImagens)
        {
            try
            {
                if (adicionarImagens)
                {
                    if (imagemButtomSenha.Equals("MOSTRANDO"))
                    {
                        btn_ExibirOcultarSenha.Image = null;
                        btn_ExibirOcultarSenha.Text = "*";
                    }
                    else if (imagemButtomSenha.Equals("ESCONDIDO"))
                    {
                        btn_ExibirOcultarSenha.Image = Resources.Eye;
                        btn_ExibirOcultarSenha.Text = "";
                    }
                        
                    btn_Confirmar.Image = Resources.Yes;
                }
                else
                {
                    btn_ExibirOcultarSenha.Image = null;
                    btn_Confirmar.Image = null;
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }
        
        #endregion

        #region Eventos KeyPress

        private void txtb_NomeUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Enter_FocusTxtb(txtb_Senha, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_Senha_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btn_TesteConexao_Click(object sender, EventArgs e)
        {
            try
            {
                ProcBD procBD = new ProcBD();
                if (!procBD.Check_PrimeiraBDConnection())
                {
                    if (procBD.Testar_BDConnection())
                        gerenciarMensagensPadraoSistema.ConnexaoBD_Sucesso();
                    else
                        gerenciarMensagensPadraoSistema.ConnexaoBD_Error();
                }
                else
                    gerenciarMensagensPadraoSistema.ConnexaoBD_Error();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_ExibirOcultarSenha_Click(object sender, EventArgs e)
        {
            try
            {
                if (imagemButtomSenha.Equals("ESCONDIDO"))
                {
                    txtb_Senha.PasswordChar = '\0';
                    btn_ExibirOcultarSenha.Image = null;
                    btn_ExibirOcultarSenha.Text = "*";
                    imagemButtomSenha = "MOSTRANDO";
                }
                else if (imagemButtomSenha.Equals("MOSTRANDO"))
                {
                    txtb_Senha.PasswordChar = '*';
                    btn_ExibirOcultarSenha.Image = Resources.Eye;
                    btn_ExibirOcultarSenha.Text = "";
                    imagemButtomSenha = "ESCONDIDO";
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_Confirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtb_NomeUsuario.Text.Trim().Equals("") && !txtb_Senha.Text.Trim().Equals(""))
                {
                    telaLoadingFoiAberta = true;
                    gerenciarTelaLoading.Abrir(this, formTelaLoading, carregarThread);

                    Login item = new Login()
                    {
                        codigo = 0,
                        nome_usuario = txtb_NomeUsuario.Text,
                        senha = txtb_Senha.Text,
                        Usuario = new Pessoa()
                        {
                            codigo = 0,
                            tipo_pessoa = "USUARIO",
                            ativo_inativo = true
                        }
                    };

                    listaLogin = procLogin.ConsultarRegistro(item, false);

                    if (listaLogin.Count.Equals(1))
                    {
                        foreach (Login loginAcessado in listaLogin)
                        {
                            login = loginAcessado;

                            break;
                        }
                        
                        listaLogin = new ListaLogin();
                        item = new Login()
                        {
                            Usuario = new Pessoa()
                        };

                        telaLoadingFoiAberta = false;
                        gerenciarTelaLoading.Fechar();

                        bool desconexao = false;
                        using (FormMenuInicial fmrMenu = new FormMenuInicial(login.Usuario))
                        {
                            AdicionaRemoveImagensBotoes(false);
                            Gerenciamento_FormVisivel(false);

                            fmrMenu.ShowDialog();
                            desconexao = fmrMenu.desconexao;
                        }

                        if (desconexao)
                        {
                            login = new Login()
                            {
                                Usuario = new Pessoa()
                            };

                            if (imagemButtomSenha.Equals("MOSTRANDO"))
                                btn_ExibirOcultarSenha_Click(sender, e);

                            txtb_NomeUsuario.Clear();
                            txtb_Senha.Clear();

                            AdicionaRemoveImagensBotoes(true);
                            Gerenciamento_FormVisivel(true);

                            gerenciarMensagensPadraoSistema.UsuarioModificadoOuRemovido();
                        }
                        else
                            this.Close();
                    }
                    else
                    {
                        telaLoadingFoiAberta = false;
                        gerenciarTelaLoading.Fechar();

                        gerenciarMensagensPadraoSistema.LoginOuSenhaIncorretos();
                    }
                }
                else
                    gerenciarMensagensPadraoSistema.LoginOuSenhaIncorretos();
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

        #endregion

    }
}
