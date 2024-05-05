using GenOR.Properties;
using CamadaObjetoTransferencia;
using CamadaProcessamento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace GenOR
{
    public partial class FormPessoa : FormBase
    {
        #region Variaveis Pessoa

        public GerenciarInformacaoRetornadoPesquisa gerenciarInformacaoRetornadoPesquisa;

        private Pessoa pessoa;
        private ListaPessoa listaPessoa;
        private ProcPessoa procPessoa;

        private Login login;

        private string formularioDoUsuario_Cliente_Fornecedor;

        private List<Pessoa> listaPesquisada;
        
        private GerenciarMensagensPadraoSistema gerenciarMensagensPadraoSistema;

        private GerenciarTelaLoading gerenciarTelaLoading;
        private FormTelaLoading formTelaLoading;
        private Thread carregarThread;
        private bool telaLoadingFoiAberta;

        private string operacaoRegistro;
        private bool encerramentoPadrao;

        private bool janelaModoPesquisa;
        private bool primeiraPesquisaModoPesquisa;

        private static int colunaCodigoIndex = 0;
        
        private string imagemButtomSenha;

        private string imagemButtomPesquisa;
        private FormTelaPesquisaPessoa formTelaPesquisaPessoa;

        private string arquivoListaParaImpressao;
        private Excel.Application conexaoExcel;
        private Excel.Workbook arquivoExcel;
        private Excel.Worksheet planilhaExcel;

        #endregion

        #region Variaveis Telefone

        private Telefone telefone;
        private ListaTelefone listaTelefone;
        private ProcTelefone procTelefone;
        private string operacaoRegistro_Telefone;
        private string arquivoListaParaImpressao_Telefone;

        #endregion

        #region Variaveis Endereço

        private Endereco endereco;
        private ListaEndereco listaEndereco;
        private ProcEndereco procEndereco;
        private string operacaoRegistro_Endereco;
        private string arquivoListaParaImpressao_Endereco;

        #endregion
        
        public FormPessoa(Pessoa objetoPesquisado, string formularioDoUsuario_Cliente_Fornecedor, bool modoPesquisa)
        {
            try
            {
                InitializeComponent();

                #region Pessoa

                gerenciarInformacaoRetornadoPesquisa = new GerenciarInformacaoRetornadoPesquisa();
                gerenciarInformacaoRetornadoPesquisa.codigoRetornavelPesquisa = 0;

                pessoa = new Pessoa();
                listaPessoa = new ListaPessoa();
                procPessoa = new ProcPessoa();

                login = new Login()
                {
                    Usuario = new Pessoa()
                };

                this.formularioDoUsuario_Cliente_Fornecedor = formularioDoUsuario_Cliente_Fornecedor;

                listaPesquisada = new ListaPessoa();

                gerenciarMensagensPadraoSistema = new GerenciarMensagensPadraoSistema();

                gerenciarTelaLoading = new GerenciarTelaLoading();
                formTelaLoading = new FormTelaLoading();
                carregarThread = null;
                telaLoadingFoiAberta = false;

                operacaoRegistro = "";
                encerramentoPadrao = true;

                janelaModoPesquisa = modoPesquisa;
                if (janelaModoPesquisa)
                {
                    AlterarLogo_ModoPesquisa(lb_LogoJanela, "                                    " + this.formularioDoUsuario_Cliente_Fornecedor + "  (MODO PESQUISA)", 18);
                    primeiraPesquisaModoPesquisa = true;

                    btn_RetornarModoPesquisa.Visible = true;
                    this.WindowState = FormWindowState.Normal;
                }
                else
                {
                    lb_LogoJanela.Text = "                " + formularioDoUsuario_Cliente_Fornecedor;
                    primeiraPesquisaModoPesquisa = false;
                }

                if (objetoPesquisado != null)
                    pessoa.codigo = objetoPesquisado.codigo;

                imagemButtomPesquisa = "PESQUISAR";

                imagemButtomSenha = "ESCONDIDO";

                arquivoListaParaImpressao = Localizar_Imagem_Documento("LISTAGEM PESSOA.xlsx", false);
                conexaoExcel = null;
                arquivoExcel = null;
                planilhaExcel = null;

                dgv_Pessoa.AutoGenerateColumns = false;

                #endregion

                #region Telefone

                telefone = new Telefone();
                listaTelefone = new ListaTelefone();
                procTelefone = new ProcTelefone();

                operacaoRegistro_Telefone = "";

                arquivoListaParaImpressao_Telefone = Localizar_Imagem_Documento("LISTAGEM TELEFONE.xlsx", false);

                dgv_Telefone.AutoGenerateColumns = false;

                #endregion

                #region Endereço

                endereco = new Endereco();
                listaEndereco = new ListaEndereco();
                procEndereco = new ProcEndereco();

                operacaoRegistro_Endereco = "";

                arquivoListaParaImpressao_Endereco = Localizar_Imagem_Documento("LISTAGEM ENDERECO.xlsx", false);

                dgv_Endereco.AutoGenerateColumns = false;

                #endregion

            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #region Eventos Form

        private void FormPessoa_Load(object sender, EventArgs e)
        {
            try
            {
                if (formularioDoUsuario_Cliente_Fornecedor.Equals("USUARIO"))
                {
                    lb_Obrigatorio_PE_1.Visible = true;
                    lb_NomeUsuario.Visible = true;
                    txtb_NomeUsuario.Visible = true;
                    
                    lb_Obrigatorio_PE_2.Visible= true;
                    lb_Senha.Visible = true;
                    txtb_Senha.Visible = true;
                    
                    btn_ExibirOcultarSenha.Visible = true;
                }
                else if (formularioDoUsuario_Cliente_Fornecedor.Equals("FORNECEDOR"))
                    btn_Materiais_Vinculados.Visible = true;
                
                this.Text = "GERENCIADOR DE " + formularioDoUsuario_Cliente_Fornecedor;
                
                ObterListagemTodosRegistrosDGV();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void FormPessoa_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (encerramentoPadrao)
                {
                    string condicaoExtra = "";

                    if (! btn_RetornarModoPesquisa.Visible.Equals(true))
                    {
                        if (!operacaoRegistro.Equals(""))
                            condicaoExtra = "durante o processo de " + operacaoRegistro;
                        else if (!operacaoRegistro_Telefone.Equals(""))
                            condicaoExtra = "durante o processo de " + operacaoRegistro_Telefone + " do Telefone";
                        else if (!operacaoRegistro_Endereco.Equals(""))
                            condicaoExtra = "durante o processo de " + operacaoRegistro_Endereco + " do Endereço";
                    }
                    else
                        condicaoExtra = "sem Retornar nenhum registro pesquisado";

                    if (gerenciarMensagensPadraoSistema.Mensagem_FechamentoJanela(condicaoExtra).Equals(DialogResult.Cancel))
                        e.Cancel = true;
                    else
                        gerenciarInformacaoRetornadoPesquisa.codigoRetornavelPesquisa = 0;
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Pessoa

        #region Funções Gerais

        private void LimparCamposParaCadastro()
        {
            try
            {
                txtb_NomeUsuario.Clear();
                txtb_Senha.Clear();

                txtb_RazaoSocial.Clear();
                txtb_NomeFantasia.Clear();
                
                cb_CPF_CNPJ.SelectedIndex = 0;
                mtxtb_CPF_CNPJ.Clear();

                mtxtb_InscricaoEstadual.Clear();
                txtb_Email.Clear();
                txtb_ObservacaoPessoa.Clear();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void ObterListagemTodosRegistrosDGV()
        {
            try
            {
                Pessoa item = new Pessoa
                {
                    tipo_pessoa = formularioDoUsuario_Cliente_Fornecedor,
                    ativo_inativo = true
                };

                listaPessoa = procPessoa.ConsultarRegistro(item, true);

                CarregarListaDGV();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void CarregarListaDGV()
        {
            try
            {
                if (listaPessoa.Count > 0)
                {
                    dgv_Pessoa.Rows.Clear();

                    foreach (Pessoa item in listaPessoa)
                    {
                        AdicionarLinhasDGV(item);
                    }

                    if (janelaModoPesquisa && primeiraPesquisaModoPesquisa)
                    {
                        int resultado = 0;
                        primeiraPesquisaModoPesquisa = false;

                        Pessoa converter = new Pessoa();

                        foreach (Pessoa item in listaPessoa.Where(ps => ps.codigo.Equals(pessoa.codigo)) )
                        {
                            resultado = (int)item.codigo;
                            break;
                        }

                        if (!resultado.Equals(0))
                        {
                            converter = listaPessoa.Where(lm => lm.codigo.Equals(pessoa.codigo)).Last();

                            foreach (DataGridViewRow item in dgv_Pessoa.Rows)
                            {
                                if (item.Cells[colunaCodigoIndex].Value.Equals((int)converter.codigo))
                                    dgv_Pessoa.CurrentCell = dgv_Pessoa.Rows[item.Index].Cells[colunaCodigoIndex];
                            }
                        }
                        else
                            dgv_Pessoa.Rows[0].Selected = true;
                    }
                    else
                    {
                        primeiraPesquisaModoPesquisa = false;
                        dgv_Pessoa.Rows[0].Selected = true;
                    }

                    if (! tbc_Secundario.TabPages.Contains(tbp_Secundario_Telefone))
                        Formulario_AtivarAbaSecundaria(tbc_Secundario, tbp_Secundario_Telefone, 1);
                    
                    if (! tbc_Secundario.TabPages.Contains(tbp_Secundario_Endereco))
                        Formulario_AtivarAbaSecundaria(tbc_Secundario, tbp_Secundario_Endereco, 2);

                    PreencherCampos();
                }
                else
                {
                    primeiraPesquisaModoPesquisa = false;

                    dgv_Pessoa.Rows.Clear();
                    dgv_Telefone.Rows.Clear();
                    dgv_Endereco.Rows.Clear();

                    GerenciarBotoes_ListagemGeral(false, btn_DadosGerais_Alterar, btn_DadosGerais_Deletar, btn_DadosGerais_Imprimir, btn_DadosGerais_AvancarGrid,
                        btn_DadosGerais_Pesquisar, btn_DadosGerais_VoltarGrid);

                    LimparCamposParaCadastro();
                    LimparCamposParaCadastro_Telefone();
                    LimparCamposParaCadastro_Endereco();

                    Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_Telefone);
                    Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_Endereco);
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void AdicionarLinhasDGV(Pessoa item)
        {
            try
            {
                dgv_Pessoa.Rows.Add(item.codigo, item.nome_razao_social, item.nome_fantasia,
                    item.cpf_cnpj, item.inscricao_estadual, item.email, item.observacao);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void PreencherCampos()
        {
            try
            {
                if (dgv_Pessoa.RowCount > 0)
                {
                    telaLoadingFoiAberta = true;
                    gerenciarTelaLoading.Abrir(this, formTelaLoading, carregarThread);

                    int codigoLinhaSelecionada = Convert.ToInt32(dgv_Pessoa.Rows[dgv_Pessoa.CurrentCell.RowIndex].Cells[colunaCodigoIndex].Value);

                    object sender = new object();
                    EventArgs e = new EventArgs();

                    foreach (Pessoa item in listaPessoa.Where(ps => ps.codigo.Equals(codigoLinhaSelecionada)))
                    {
                        txtb_CodigoPessoa.Text = item.codigo.ToString();

                        if (formularioDoUsuario_Cliente_Fornecedor.Equals("USUARIO"))
                        {
                            Login lg = new Login()
                            {
                                Usuario = new Pessoa()
                            };
                            lg = ObterDadosUsuario(item);

                            if (!lg.codigo.Equals(0))
                            {
                                txtb_NomeUsuario.Text = lg.nome_usuario.ToString();
                                txtb_Senha.Text = lg.senha.ToString();
                            }

                            if (imagemButtomSenha.Equals("MOSTRANDO"))
                                btn_ExibirOcultarSenha_Click(sender, e);
                        }

                        txtb_RazaoSocial.Text = item.nome_razao_social;
                        txtb_NomeFantasia.Text = item.nome_fantasia;

                        if (!item.cpf_cnpj.Length.Equals(0))
                        {
                            if (item.cpf_cnpj.Length.Equals(11))
                            {
                                cb_CPF_CNPJ.SelectedIndex = 0;
                                mtxtb_CPF_CNPJ.Text = item.cpf_cnpj;
                            }
                            else if (item.cpf_cnpj.Length.Equals(14))
                            {
                                cb_CPF_CNPJ.SelectedIndex = 1;
                                mtxtb_CPF_CNPJ.Text = item.cpf_cnpj;
                            }
                        }
                        else
                        {
                            mtxtb_CPF_CNPJ.Clear();
                            cb_CPF_CNPJ.SelectedIndex = 0;
                        }

                        mtxtb_InscricaoEstadual.Text = item.inscricao_estadual.ToString();
                        txtb_Email.Text = item.email;
                        txtb_ObservacaoPessoa.Text = item.observacao;

                        if (primeiraPesquisaModoPesquisa.Equals(false))
                            pessoa = item;

                        break;
                    }

                    if (!tbc_Secundario.TabPages.Contains(tbp_Secundario_Telefone))
                        Formulario_AtivarAbaSecundaria(tbc_Secundario, tbp_Secundario_Telefone, 1);

                    if (!tbc_Secundario.TabPages.Contains(tbp_Secundario_Endereco))
                        Formulario_AtivarAbaSecundaria(tbc_Secundario, tbp_Secundario_Endereco, 2);

                    primeiraPesquisaModoPesquisa = true;
                    ObterListagemRegistrosDGV_Telefone();

                    primeiraPesquisaModoPesquisa = true;
                    ObterListagemRegistrosDGV_Endereco();
                }
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

        private bool ValidandoCompilandoDadosCampos_TbPrincipal(Pessoa validandoPessoa)
        {
            try
            {
                bool dadosValidos = true;
                decimal variavelSaidaDecimal = 0;
                string variavelSaidaTexto = "";

                if(txtb_NomeFantasia.Text.Trim().Equals(""))
                    validandoPessoa.nome_fantasia = txtb_RazaoSocial.Text;
                else
                    validandoPessoa.nome_fantasia = txtb_NomeFantasia.Text;
                
                
                validandoPessoa.email = txtb_Email.Text;
                validandoPessoa.observacao = txtb_ObservacaoPessoa.Text;
                validandoPessoa.tipo_pessoa = formularioDoUsuario_Cliente_Fornecedor;
                validandoPessoa.ativo_inativo = true;

                if (operacaoRegistro.Equals("ALTERAÇÃO") || operacaoRegistro.Equals("INATIVAÇÃO"))
                    validandoPessoa.codigo = int.Parse(txtb_CodigoPessoa.Text);
                else
                    validandoPessoa.codigo = 0;

                if (dadosValidos)
                {
                    if (formularioDoUsuario_Cliente_Fornecedor.Equals("USUARIO"))
                    {
                        if (txtb_NomeUsuario.Text.Trim().Equals(""))
                        {
                            gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("NOME DO USUARIO");
                            dadosValidos = false;
                        }
                        else
                            login.nome_usuario = txtb_NomeUsuario.Text;

                        if (dadosValidos)
                        {
                            if (txtb_Senha.Text.Trim().Equals(""))
                            {
                                gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("SENHA DO USUARIO");
                                dadosValidos = false;
                            }
                            else
                            {
                                login.senha = txtb_Senha.Text;

                                if (operacaoRegistro.Equals("ALTERAÇÃO") || operacaoRegistro.Equals("INATIVAÇÃO"))
                                {
                                    Login lg = new Login()
                                    {
                                        Usuario = new Pessoa()
                                    };
                                    lg = ObterDadosUsuario(pessoa);

                                    if (! lg.codigo.Equals(0))
                                        login.codigo = lg.codigo;
                                    else
                                    {
                                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CÓDIGO DE LOGIN");
                                        dadosValidos = false;
                                    }
                                }
                                else
                                    login.codigo = 0;
                            }
                        }
                    }
                }
                
                if (dadosValidos)
                {
                    if (txtb_RazaoSocial.Text.Trim().Equals(""))
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("RAZÃO SOCIAL DO " + formularioDoUsuario_Cliente_Fornecedor);
                        dadosValidos = false;
                    }
                    else
                    {
                        validandoPessoa.nome_razao_social = txtb_RazaoSocial.Text;
                    }
                }
                
                if (dadosValidos)
                {
                    string Cpf_Cnpj = "";

                    if (cb_CPF_CNPJ.SelectedIndex.Equals(0))
                    {
                        Cpf_Cnpj = "CPF";
                    }
                    else
                    {
                        Cpf_Cnpj = "CNPJ";
                    }
                    
                    variavelSaidaTexto = CampoMaskedTxt_RemoverFormatacao(mtxtb_CPF_CNPJ);
                    if (variavelSaidaTexto.Equals("") || decimal.TryParse(variavelSaidaTexto, out variavelSaidaDecimal))
                    {
                        if ( (Cpf_Cnpj.Equals("CPF") && (variavelSaidaTexto.Length.Equals(0) || variavelSaidaTexto.Length.Equals(11))) 
                            || (Cpf_Cnpj.Equals("CNPJ") && (variavelSaidaTexto.Length.Equals(0) || variavelSaidaTexto.Length.Equals(14))) )
                        {
                            validandoPessoa.cpf_cnpj = variavelSaidaTexto;
                        }
                        else
                        {
                            gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco(Cpf_Cnpj + " DO " + formularioDoUsuario_Cliente_Fornecedor);
                            dadosValidos = false;
                        }
                    }
                    else
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco(Cpf_Cnpj + " DO " + formularioDoUsuario_Cliente_Fornecedor);
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    variavelSaidaTexto = CampoMaskedTxt_RemoverFormatacao(mtxtb_InscricaoEstadual);
                    if (variavelSaidaTexto.Equals("") || decimal.TryParse(variavelSaidaTexto, out variavelSaidaDecimal))
                    {
                        if (variavelSaidaTexto.Length.Equals(0) || variavelSaidaTexto.Length.Equals(12))
                        {
                            validandoPessoa.inscricao_estadual = variavelSaidaTexto;
                        }
                        else
                        {
                            gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("INSCRIÇÃO ESTADUAL DO " + formularioDoUsuario_Cliente_Fornecedor);
                            dadosValidos = false;
                        }
                    }
                    else
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("INSCRIÇÃO ESTADUAL DO " + formularioDoUsuario_Cliente_Fornecedor);
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    dadosValidos = ValidandoCadastroRegistroDuplicado_TbPrincipal(validandoPessoa);
                }
                
                return dadosValidos;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
                return false;
            }
        }

        private bool ValidandoCadastroRegistroDuplicado_TbPrincipal(Pessoa validandoPessoa)
        {
            try
            {
                bool dadosValidos = true;

                Pessoa p = new Pessoa
                {
                    tipo_pessoa = formularioDoUsuario_Cliente_Fornecedor,
                    ativo_inativo = true
                };

                ListaPessoa listaP = procPessoa.ConsultarRegistro(p, true);

                foreach (Pessoa item in listaP)
                {
                    if (item.nome_razao_social.Equals(validandoPessoa.nome_razao_social) && item.ativo_inativo.Equals(true))
                    {
                        if (operacaoRegistro.Equals("CADASTRO"))
                            dadosValidos = false;
                        else if (operacaoRegistro.Equals("ALTERAÇÃO"))
                        {
                            if (! item.codigo.Equals(validandoPessoa.codigo))
                                dadosValidos = false;
                        }

                        if (dadosValidos.Equals(false))
                            gerenciarMensagensPadraoSistema.RegistroDuplicado("Esse " + formularioDoUsuario_Cliente_Fornecedor, "Código: " + validandoPessoa.codigo + " - Razão Social: " + validandoPessoa.nome_razao_social);

                        break;
                    }
                }


                if (dadosValidos)
                {
                    if (formularioDoUsuario_Cliente_Fornecedor.Equals("USUARIO"))
                    {
                        Login lg = new Login();

                        foreach (Pessoa item in listaP)
                        {
                            lg.Usuario = item;
                            lg = ObterDadosUsuario(item);

                            if (! lg.codigo.Equals(0))
                            {
                                if (lg.nome_usuario.Equals(login.nome_usuario))
                                {
                                    if (operacaoRegistro.Equals("CADASTRO"))
                                        dadosValidos = false;
                                    else if (operacaoRegistro.Equals("ALTERAÇÃO"))
                                    {
                                        if (! item.codigo.Equals(validandoPessoa.codigo))
                                            dadosValidos = false;
                                    }
                                }

                                if (dadosValidos.Equals(false))
                                {
                                    gerenciarMensagensPadraoSistema.RegistroDuplicado("Esse " + formularioDoUsuario_Cliente_Fornecedor, "Código: " + validandoPessoa.codigo + " - Nome Usuario: " + login.nome_usuario);
                                    break;
                                }
                            }
                        }
                    }
                }
                
                return dadosValidos;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
                return false;
            }
        }

        private void ModificarTabelaExcel(string patchTabela)
        {
            try
            {
                Login lg = new Login()
                {
                    Usuario = new Pessoa()
                };
            
                List<Pessoa> listaImpressao = new ListaPessoa();

                if (imagemButtomPesquisa.Equals("PESQUISAR"))
                    listaImpressao = listaPessoa;
                else
                    listaImpressao = listaPesquisada;

                conexaoExcel = new Excel.Application();
                arquivoExcel = conexaoExcel.Workbooks.Open(patchTabela);
                planilhaExcel = arquivoExcel.Worksheets[1] as Excel.Worksheet;

                int linha = 2;
                foreach (Pessoa item in listaImpressao)
                {
                    int coluna = 1;

                    planilhaExcel.Cells[linha, coluna++].Value = item.codigo.ToString();
                    planilhaExcel.Cells[linha, coluna++].Value = item.nome_razao_social.ToString();
                    planilhaExcel.Cells[linha, coluna++].Value = item.nome_fantasia.ToString();

                    if (item.cpf_cnpj.Length.Equals(11))
                        planilhaExcel.Cells[linha, coluna++].Value = Convert.ToUInt64(item.cpf_cnpj).ToString(@"000\.000\.000\-00");
                    else if (item.cpf_cnpj.Length.Equals(14))
                        planilhaExcel.Cells[linha, coluna++].Value = Convert.ToUInt64(item.cpf_cnpj).ToString(@"00\.000\.000\/0000\-00");
                    else
                        planilhaExcel.Cells[linha, coluna++].Value = item.cpf_cnpj.ToString();

                    planilhaExcel.Cells[linha, coluna++].Value = item.inscricao_estadual.ToString();
                    planilhaExcel.Cells[linha, coluna++].Value = item.email.ToString();
                    planilhaExcel.Cells[linha, coluna++].Value = item.observacao.ToString();

                    if (formularioDoUsuario_Cliente_Fornecedor.Equals("USUARIO"))
                    {
                        lg = new Login();
                        lg = ObterDadosUsuario(item);

                        if (! lg.codigo.Equals(0))
                            planilhaExcel.Cells[linha, coluna++].Value = lg.nome_usuario.ToString();
                    }
                    
                    linha++;
                }

                arquivoExcel.Save();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
            finally
            {
                arquivoExcel.Close();
                conexaoExcel.Quit();
                arquivoExcel = null;
                planilhaExcel = null;
                conexaoExcel = null;
            }
        }

        private Login ObterDadosUsuario(Pessoa pessoaLogin)
        {
            Login lg = new Login()
            {
                codigo = 0,
                Usuario = pessoaLogin
            };

            try
            {
                ProcLogin procLogin = new ProcLogin();
                ListaLogin listaLogin = procLogin.ConsultarRegistro(lg, true);

                foreach (Login item in listaLogin)
                {
                    if (item.Usuario.codigo.Equals(pessoaLogin.codigo))
                    {
                        lg = item;
                        break;
                    }
                }

                return lg;
            }
            catch (Exception exception)
            {
                lg.codigo = 0;
                gerenciarMensagensPadraoSistema.MensagemException(exception);

                return lg;
            }
        }
        
        private ListaPessoa PesquisandoPessoaPorEndereco(string campoPesquisado, string informaçãoRetornada)
        {
            ListaPessoa listaP = new ListaPessoa();

            try
            {
                List<Endereco> listaE = new List<Endereco>();

                Endereco item = new Endereco()
                {
                    ativo_inativo = true,
                    Pessoa = new Pessoa() { ativo_inativo = true }
                };

                if (campoPesquisado.Equals("ESTADO"))
                    item.estado = informaçãoRetornada;
                else if (campoPesquisado.Equals("CIDADE"))
                    item.cidade = informaçãoRetornada;
                else
                    return listaP;

                listaE = procEndereco.ConsultarRegistro(item, false);

                bool itemNaoAdicionadoLista;
                foreach (Endereco retornoE in listaE)
                {
                    if (listaP.Count.Equals(0))
                        listaP.Add(retornoE.Pessoa);
                    else
                    {
                        itemNaoAdicionadoLista = true;
                        foreach (Pessoa retornoP in listaP)
                        {
                            if (retornoE.Pessoa.codigo.Equals(retornoP.codigo))
                            {
                                itemNaoAdicionadoLista = false;
                                break;
                            }
                        }

                        if (itemNaoAdicionadoLista)
                            listaP.Add(retornoE.Pessoa);
                    }
                }

                return listaP;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
                return listaP;
            }
        }

        private ListaPessoa PesquisandoPessoaPorTelefone(string campoPesquisado, string informaçãoRetornada)
        {
            ListaPessoa listaP = new ListaPessoa();

            try
            {
                List<Telefone> listaT = new List<Telefone>();

                Telefone item = new Telefone()
                {
                    ativo_inativo = true,
                    Pessoa = new Pessoa() { ativo_inativo = true }
                };

                if (campoPesquisado.Equals("DDD"))
                    item.ddd = informaçãoRetornada;
                else if (campoPesquisado.Equals("TELEFONE"))
                    item.numero = informaçãoRetornada;
                else
                    return listaP;

                listaT = procTelefone.ConsultarRegistro(item, false);

                bool itemNaoAdicionadoLista;
                foreach (Telefone retornoT in listaT)
                {
                    if (listaP.Count.Equals(0))
                        listaP.Add(retornoT.Pessoa);
                    else
                    {
                        itemNaoAdicionadoLista = true;
                        foreach (Pessoa retornoP in listaP)
                        {
                            if (retornoT.Pessoa.codigo.Equals(retornoP.codigo))
                            {
                                itemNaoAdicionadoLista = false;
                                break;
                            }
                        }

                        if (itemNaoAdicionadoLista)
                            listaP.Add(retornoT.Pessoa);
                    }
                }

                return listaP;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
                return listaP;
            }
        }

        #endregion

        #region Eventos Enter

        private void cb_CPF_CNPJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cb_CPF_CNPJ.SelectedIndex.Equals(0)) //CPF
                {
                    mtxtb_CPF_CNPJ.Mask = "000,000,000-00";
                    mtxtb_CPF_CNPJ.Size = new Size(120, 26);
                }
                else if (cb_CPF_CNPJ.SelectedIndex.Equals(1)) //CNPJ
                {
                    mtxtb_CPF_CNPJ.Size = new Size(150, 26);
                    mtxtb_CPF_CNPJ.Mask = "00,000,000/0000-00";
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
                Enter_FocusTxtb(txtb_RazaoSocial, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_RazaoSocial_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Enter_FocusTxtb(txtb_NomeFantasia, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_NomeFantasia_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Enter_FocusMaskedTxtb(mtxtb_CPF_CNPJ, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void mtxtb_CPF_CNPJ_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Enter_FocusMaskedTxtb(mtxtb_InscricaoEstadual, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void mtxtb_InscricaoEstadual_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Enter_FocusTxtb(txtb_Email, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_Email_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Enter_FocusButton(btn_DadosGerais_Confirmar, e))
                    btn_DadosGerais_Confirmar_Click(sender, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Leave

        private void txtb_Senha_Leave(object sender, EventArgs e)
        {
            try
            {
                if (imagemButtomSenha.Equals("MOSTRANDO"))
                    btn_ExibirOcultarSenha_Click(sender, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Button Click

        private void btn_DadosGerais_Cadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                btn_Materiais_Vinculados.Enabled = false;

                Formulario_ModoModificacao(gb_ListagemGeral, gb_DadosGerais, tbc_Principal, tbc_Secundario, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar,
                    btn_DadosGerais_Deletar, btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_Telefone);
                Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_Endereco);

                operacaoRegistro = "CADASTRO";
                LimparCamposParaCadastro();

                if (formularioDoUsuario_Cliente_Fornecedor.Equals("USUARIO"))
                    txtb_NomeUsuario.Focus();
                else
                    txtb_RazaoSocial.Focus();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_DadosGerais_Alterar_Click(object sender, EventArgs e)
        {
            try
            {
                btn_Materiais_Vinculados.Enabled = false;

                Formulario_ModoModificacao(gb_ListagemGeral, gb_DadosGerais, tbc_Principal, tbc_Secundario, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar,
                    btn_DadosGerais_Deletar, btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_Telefone);
                Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_Endereco);

                operacaoRegistro = "ALTERAÇÃO";

                if (formularioDoUsuario_Cliente_Fornecedor.Equals("USUARIO"))
                    txtb_NomeUsuario.Focus();
                else
                    txtb_RazaoSocial.Focus();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_DadosGerais_Deletar_Click(object sender, EventArgs e)
        {
            try
            {
                if (formularioDoUsuario_Cliente_Fornecedor.Equals("USUARIO"))
                {
                    if (listaPessoa.Count > 1)
                    {
                        operacaoRegistro = "INATIVAÇÃO";
                        btn_DadosGerais_Confirmar_Click(sender, e);
                    }
                    else
                    {
                        gerenciarMensagensPadraoSistema.ExceptionBancoDados("Permissão para exclusão do registro foi negada ! \n \n O processo de exclusão foi interrompido pois " +
                            "deve haver no Minimo 1 registro para que o processo de LOGIN possa ser efetuado.");
                    }
                }
                else
                {
                    operacaoRegistro = "INATIVAÇÃO";
                    btn_DadosGerais_Confirmar_Click(sender, e);
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_DadosGerais_Imprimir_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog diretorioDestinoArquivoImpressao = new FolderBrowserDialog();

                if (diretorioDestinoArquivoImpressao.ShowDialog().Equals(DialogResult.OK) && !string.IsNullOrWhiteSpace(diretorioDestinoArquivoImpressao.SelectedPath))
                {
                    telaLoadingFoiAberta = true;
                    gerenciarTelaLoading.Abrir(this, formTelaLoading, carregarThread);

                    string nomeArquivo = Path.GetFileName(arquivoListaParaImpressao);

                    string nomeArquivoModificado = nomeArquivo.Replace(".xlsx", "");
                    nomeArquivoModificado = nomeArquivoModificado.Replace("PESSOA", formularioDoUsuario_Cliente_Fornecedor) + " " + DateTime.Now.ToString("dd-MM-yyyy") + ".xlsx";

                    string arquivoListaModificado = Path.Combine(diretorioDestinoArquivoImpressao.SelectedPath, nomeArquivoModificado);

                    File.Copy(arquivoListaParaImpressao, arquivoListaModificado, true);

                    ModificarTabelaExcel(arquivoListaModificado);

                    if (telaLoadingFoiAberta)
                    {
                        telaLoadingFoiAberta = false;
                        gerenciarTelaLoading.Fechar();
                    }

                    if (gerenciarMensagensPadraoSistema.Mensagem_AbrirDocumento().Equals(DialogResult.OK))
                        Process.Start(arquivoListaModificado);
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_DadosGerais_VoltarGrid_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_Pessoa.CurrentRow.Index > 0)
                {
                    int linha = MudarLinha_DGV(dgv_Pessoa, false);

                    dgv_Pessoa.CurrentCell = dgv_Pessoa.Rows[linha].Cells[dgv_Pessoa.CurrentCell.ColumnIndex];
                    PreencherCampos();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_DadosGerais_AvancarGrid_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_Pessoa.CurrentRow.Index < (dgv_Pessoa.RowCount - 1))
                {
                    int linha = MudarLinha_DGV(dgv_Pessoa, true);

                    dgv_Pessoa.CurrentCell = dgv_Pessoa.Rows[linha].Cells[dgv_Pessoa.CurrentCell.ColumnIndex];
                    PreencherCampos();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_DadosGerais_Confirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (gerenciarMensagensPadraoSistema.Mensagem_Confirmacao(operacaoRegistro).Equals(DialogResult.OK))
                {
                    Pessoa objetoChecagem = new Pessoa();

                    string statusOperacao = "EM ANDAMENTO";

                    if (ValidandoCompilandoDadosCampos_TbPrincipal(objetoChecagem))
                    {
                        if (operacaoRegistro.Equals("CADASTRO") || operacaoRegistro.Equals("ALTERAÇÃO") || operacaoRegistro.Equals("INATIVAÇÃO"))
                            statusOperacao = procPessoa.ManterRegistro(objetoChecagem, operacaoRegistro);
                        else
                        {
                            gerenciarMensagensPadraoSistema.ExceptionBancoDados("A operação informada não existe !");
                            return;
                        }
                        
                        int codigoRegistroRetornado = 0;
                        if (int.TryParse(statusOperacao, out codigoRegistroRetornado))
                        {
                            pessoa = objetoChecagem;
                            pessoa.codigo = codigoRegistroRetornado;

                            if (formularioDoUsuario_Cliente_Fornecedor.Equals("USUARIO"))
                            {
                                login.Usuario = pessoa;

                                ProcLogin procLogin = new ProcLogin();
                                statusOperacao = procLogin.ManterRegistro(login, operacaoRegistro);

                                if (int.TryParse(statusOperacao, out codigoRegistroRetornado))
                                    login.codigo = codigoRegistroRetornado;
                                else
                                {
                                    gerenciarMensagensPadraoSistema.ExceptionBancoDados(statusOperacao);
                                    return;
                                }
                            }

                            bool gerenciarModoJanela = janelaModoPesquisa;
                            if (gerenciarModoJanela.Equals(false))
                                janelaModoPesquisa = true;

                            primeiraPesquisaModoPesquisa = true;

                            if (imagemButtomPesquisa.Equals("CANCELAR PESQUISA"))
                                btn_DadosGerais_Pesquisar_Click(sender, e);
                            else
                                ObterListagemTodosRegistrosDGV();

                            if (gerenciarModoJanela.Equals(false))
                                janelaModoPesquisa = false;

                            Formulario_ModoConsulta(gb_ListagemGeral, gb_DadosGerais, dgv_Pessoa, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar, btn_DadosGerais_Deletar,
                                btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                            operacaoRegistro = "";
                        }
                        else
                        {
                            gerenciarMensagensPadraoSistema.ExceptionBancoDados(statusOperacao);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_DadosGerais_Cancelar_Click(object sender, EventArgs e)
        {
            try
            {
                if (gerenciarMensagensPadraoSistema.Mensagem_Cancelamento().Equals(DialogResult.OK))
                {
                    Formulario_ModoConsulta(gb_ListagemGeral, gb_DadosGerais, dgv_Pessoa, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar, btn_DadosGerais_Deletar,
                        btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                    if (imagemButtomPesquisa.Equals("CANCELAR PESQUISA"))
                        btn_DadosGerais_Pesquisar_Click(sender, e);
                    else
                        ObterListagemTodosRegistrosDGV();

                    operacaoRegistro = "";
                }
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

        #endregion

        #region Eventos Button Pesquisa

        private void btn_DadosGerais_Pesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                if (imagemButtomPesquisa.Equals("PESQUISAR"))
                {
                    formTelaPesquisaPessoa = new FormTelaPesquisaPessoa(formularioDoUsuario_Cliente_Fornecedor);
                    formTelaPesquisaPessoa.ShowDialog();

                    if (!formTelaPesquisaPessoa.campoPesquisado.Equals("CANCELADO") && !formTelaPesquisaPessoa.informaçãoRetornada.Equals("VAZIA"))
                    {
                        listaPesquisada = new ListaPessoa();

                        if (formTelaPesquisaPessoa.campoPesquisado.Equals("CÓDIGO"))
                        {
                            listaPesquisada = listaPessoa.Where(p => p.codigo.Equals(int.Parse(formTelaPesquisaPessoa.informaçãoRetornada))).ToList();
                        }
                        else if (formTelaPesquisaPessoa.campoPesquisado.Equals("RAZÃO SOCIAL"))
                        {
                            listaPesquisada = listaPessoa.Where(p => p.nome_razao_social.Contains(formTelaPesquisaPessoa.informaçãoRetornada)).ToList();
                        }
                        else if (formTelaPesquisaPessoa.campoPesquisado.Equals("NOME FANTASIA"))
                        {
                            listaPesquisada = listaPessoa.Where(p => p.nome_fantasia.Contains(formTelaPesquisaPessoa.informaçãoRetornada)).ToList();
                        }
                        else if (formTelaPesquisaPessoa.campoPesquisado.Equals("CPF/CNPJ"))
                        {
                            listaPesquisada = listaPessoa.Where(p => p.cpf_cnpj.Equals(formTelaPesquisaPessoa.informaçãoRetornada)).ToList();
                        }
                        else if (formTelaPesquisaPessoa.campoPesquisado.Equals("INSCRIÇÃO ESTADUAL"))
                        {
                            listaPesquisada = listaPessoa.Where(p => p.inscricao_estadual.Equals(formTelaPesquisaPessoa.informaçãoRetornada)).ToList();
                        }
                        else if (formTelaPesquisaPessoa.campoPesquisado.Equals("EMAIL"))
                        {
                            listaPesquisada = listaPessoa.Where(p => p.email.Contains(formTelaPesquisaPessoa.informaçãoRetornada)).ToList();
                        }
                        else if (formTelaPesquisaPessoa.campoPesquisado.Equals("ESTADO") || formTelaPesquisaPessoa.campoPesquisado.Equals("CIDADE"))
                        {
                            listaPesquisada = PesquisandoPessoaPorEndereco(formTelaPesquisaPessoa.campoPesquisado, formTelaPesquisaPessoa.informaçãoRetornada);
                        }
                        else if (formTelaPesquisaPessoa.campoPesquisado.Equals("DDD") || formTelaPesquisaPessoa.campoPesquisado.Equals("TELEFONE"))
                        {
                            listaPesquisada = PesquisandoPessoaPorTelefone(formTelaPesquisaPessoa.campoPesquisado, formTelaPesquisaPessoa.informaçãoRetornada);
                        }
                        else
                        {
                            gerenciarMensagensPadraoSistema.InformacaoPesquisadaNaoEncontrada();
                            return;
                        }

                        if (listaPesquisada.Count() > 0)
                        {
                            dgv_Pessoa.Rows.Clear();

                            foreach (Pessoa item in listaPesquisada)
                            {
                                AdicionarLinhasDGV(item);
                            }

                            dgv_Pessoa.Rows[0].Selected = true;
                            PreencherCampos();
                        }
                        else
                        {
                            gerenciarMensagensPadraoSistema.InformacaoPesquisadaNaoEncontrada();
                            return;
                        }

                        btn_DadosGerais_Pesquisar.Image = Resources.Delete;
                        btn_DadosGerais_Pesquisar.Text = " CANCELAR PESQUISA";
                        imagemButtomPesquisa = "CANCELAR PESQUISA";
                    }
                }
                else
                {
                    ObterListagemTodosRegistrosDGV();

                    btn_DadosGerais_Pesquisar.Image = Resources.Find;
                    btn_DadosGerais_Pesquisar.Text = "      PESQUISAR";
                    imagemButtomPesquisa = "PESQUISAR";

                    operacaoRegistro = "";
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_RetornarModoPesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                if (listaPessoa.Count > 0)
                {
                    if (gerenciarMensagensPadraoSistema.Mensagem_Confirmacao("RETORNAR LINHA SELECIONADA").Equals(DialogResult.OK))
                    {
                        gerenciarInformacaoRetornadoPesquisa.codigoRetornavelPesquisa = (int)pessoa.codigo;

                        encerramentoPadrao = false;
                        this.Close();
                    }
                }
                else
                {
                    gerenciarInformacaoRetornadoPesquisa.codigoRetornavelPesquisa = 0;

                    encerramentoPadrao = false;
                    this.Close();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_Materiais_Vinculados_Click(object sender, EventArgs e)
        {
            try
            {
                using (FormMateriais_Vinculados_Fornecedor formMateriais_Vinculados_Fornecedor = new FormMateriais_Vinculados_Fornecedor(pessoa, this))
                {
                    formMateriais_Vinculados_Fornecedor.ShowDialog();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Data Grid View

        private void dgv_Pessoa_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue.Equals(((int)Keys.Up)) || e.KeyValue.Equals(((int)Keys.Down)))
                    PreencherCampos();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void dgv_Pessoa_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                var linhaClicada = dgv_Pessoa.HitTest(e.X, e.Y);
                if (linhaClicada != null && linhaClicada.RowIndex >= 0)
                {
                    if (e.Button.Equals(MouseButtons.Right))
                        dgv_Pessoa.CurrentCell = dgv_Pessoa.Rows[linhaClicada.RowIndex].Cells[0];

                    PreencherCampos();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void dgv_Pessoa_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                var linhaClicada = dgv_Pessoa.HitTest(e.X, e.Y);
                if (linhaClicada != null && linhaClicada.RowIndex >= 0)
                {
                    if (e.Button.Equals(MouseButtons.Left))
                        tbc_Principal.SelectedIndex = 1;
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos MouseHovar

        private void lb_Obrigatorio_PE_1_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_PE_1, "texto");
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void lb_Obrigatorio_PE_2_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_PE_2, "texto");
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void lb_Obrigatorio_PE_3_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_PE_3, "texto");
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion
        
        #endregion

        #region Telefone

        #region Funções Gerais

        private void LimparCamposParaCadastro_Telefone()
        {
            try
            {
                txtb_CodigoTelefone.Clear();
                mtxtb_DDDTelefone.Clear();
                txtb_NumeroTelefone.Clear();
                txtb_ObservacaoTelefone.Clear();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void ObterListagemRegistrosDGV_Telefone()
        {
            try
            {
                Telefone item = new Telefone
                {
                    ativo_inativo = true,
                    Pessoa = pessoa
                };

                listaTelefone = procTelefone.ConsultarRegistro(item, false);

                CarregarListaDGV_Telefone();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void AdicionarLinhasDGV_Telefone(Telefone item)
        {
            try
            {
                dgv_Telefone.Rows.Add(item.codigo, "( " + item.ddd + " )", item.numero, item.observacao);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void CarregarListaDGV_Telefone()
        {
            try
            {
                if (listaPessoa.Count > 0)
                {
                    if (listaTelefone.Count > 0)
                    {
                        dgv_Telefone.Rows.Clear();

                        foreach (Telefone item in listaTelefone)
                        {
                            if (item.Pessoa.codigo.Equals(pessoa.codigo))
                                AdicionarLinhasDGV_Telefone(item);
                        }

                        if (primeiraPesquisaModoPesquisa)
                        {
                            int resultado = 0;
                            primeiraPesquisaModoPesquisa = false;

                            Telefone converter = new Telefone();

                            foreach (Telefone item in listaTelefone.Where(t => t.codigo.Equals(telefone.codigo)))
                            {
                                resultado = (int)item.codigo;
                                break;
                            }

                            if (!resultado.Equals(0))
                            {
                                converter = listaTelefone.Where(t => t.codigo.Equals(telefone.codigo)).Last();

                                foreach (DataGridViewRow item in dgv_Telefone.Rows)
                                {
                                    if (item.Cells[colunaCodigoIndex].Value.Equals((int)converter.codigo))
                                        dgv_Telefone.CurrentCell = dgv_Telefone.Rows[item.Index].Cells[colunaCodigoIndex];
                                }
                            }
                            else
                            {
                                primeiraPesquisaModoPesquisa = true;
                                dgv_Telefone.Rows[0].Selected = true;
                            }
                        }
                        else
                            dgv_Telefone.Rows[0].Selected = true;

                        if (tbc_Secundario.TabPages.Contains(tbp_Secundario_Telefone))
                            PreencherCampos_Telefone();
                    }
                    else
                    {
                        primeiraPesquisaModoPesquisa = false;

                        dgv_Telefone.Rows.Clear();

                        GerenciarBotoes_ListagemSecundaria(false, true, btn_Telefone_Cadastrar, btn_Telefone_Alterar, btn_Telefone_Deletar, btn_Telefone_Imprimir);
                        LimparCamposParaCadastro_Telefone();
                    }

                    btn_Materiais_Vinculados.Enabled = true;
                }
                else
                {
                    btn_Materiais_Vinculados.Enabled = false;

                    primeiraPesquisaModoPesquisa = false;

                    dgv_Telefone.Rows.Clear();

                    GerenciarBotoes_ListagemSecundaria(false, false, btn_Telefone_Cadastrar, btn_Telefone_Alterar, btn_Telefone_Deletar, btn_Telefone_Imprimir);
                    LimparCamposParaCadastro_Telefone();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void PreencherCampos_Telefone()
        {
            try
            {
                if (dgv_Telefone.RowCount > 0)
                {
                    int codigoLinhaSelecionada;

                    if (primeiraPesquisaModoPesquisa)
                    {
                        primeiraPesquisaModoPesquisa = false;

                        Telefone tel = listaTelefone.First();
                        codigoLinhaSelecionada = (int)tel.codigo;
                    }
                    else
                        codigoLinhaSelecionada = Convert.ToInt32(dgv_Telefone.Rows[dgv_Telefone.CurrentCell.RowIndex].Cells[colunaCodigoIndex].Value);

                    foreach (Telefone item in listaTelefone.Where(t => t.codigo.Equals(codigoLinhaSelecionada)))
                    {
                        txtb_CodigoTelefone.Text = item.codigo.ToString();
                        mtxtb_DDDTelefone.Text = item.ddd.ToString();
                        txtb_NumeroTelefone.Text = item.numero.ToString();
                        txtb_ObservacaoTelefone.Text = item.observacao.ToString();

                        telefone = item;
                        break;
                    }
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private bool ValidandoCompilandoDadosCampos_TbSecundario_Telefone(Telefone validandoTelefone)
        {
            try
            {
                bool dadosValidos = true;
                int variavelSaidaCodigo = 0;
                decimal variavelSaidaDecimal = 0;
                string variavelSaidaTexto = "";

                validandoTelefone.observacao = txtb_ObservacaoTelefone.Text;
                validandoTelefone.ativo_inativo = true;

                if (dadosValidos)
                {
                    if (operacaoRegistro_Telefone.Equals("ALTERAÇÃO") || operacaoRegistro_Telefone.Equals("INATIVAÇÃO"))
                        validandoTelefone.codigo = int.Parse(txtb_CodigoTelefone.Text);
                    else
                        validandoTelefone.codigo = 0;
                }

                if (dadosValidos)
                {
                    variavelSaidaTexto = CampoMaskedTxt_RemoverFormatacao(mtxtb_DDDTelefone);
                    if (int.TryParse(variavelSaidaTexto, out variavelSaidaCodigo))
                    {
                        if (variavelSaidaCodigo.ToString().Length.Equals(2))
                            validandoTelefone.ddd = variavelSaidaTexto;
                        else
                        {
                            gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("DDD");
                            dadosValidos = false;
                        }
                    }
                    else
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("DDD");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    if (decimal.TryParse(txtb_NumeroTelefone.Text.Trim(), out variavelSaidaDecimal))
                    {
                        if (txtb_NumeroTelefone.Text.Length.Equals(8) || txtb_NumeroTelefone.Text.Length.Equals(9) || txtb_NumeroTelefone.Text.Length.Equals(11))
                            validandoTelefone.numero = txtb_NumeroTelefone.Text.Trim();
                        else
                        {
                            gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("NÚMERO TELEFONE");
                            dadosValidos = false;
                        }
                    }
                    else
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("NÚMERO TELEFONE");
                        dadosValidos = false;
                    }
                }
                
                if (dadosValidos)
                {
                    operacaoRegistro = "ALTERAÇÃO";
                    if (dadosValidos = ValidandoCompilandoDadosCampos_TbPrincipal(pessoa))
                        validandoTelefone.Pessoa = pessoa;

                    operacaoRegistro = "";
                }

                if (dadosValidos)
                    dadosValidos = ValidandoCadastroRegistroDuplicado_TbSecundario_Telefone(validandoTelefone);

                return dadosValidos;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
                return false;
            }
        }

        private bool ValidandoCadastroRegistroDuplicado_TbSecundario_Telefone(Telefone validandoTelefone)
        {
            try
            {
                bool dadosValidos = true;

                foreach (Telefone item in listaTelefone)
                {
                    if (operacaoRegistro_Telefone.Equals("CADASTRO"))
                    {
                        if (item.ddd.Equals(validandoTelefone.ddd) && item.numero.Equals(validandoTelefone.numero) && item.ativo_inativo.Equals(true))
                        {
                            dadosValidos = false;
                            break;
                        }
                    }
                    else if (operacaoRegistro_Telefone.Equals("ALTERAÇÃO"))
                    {
                        if (item.ddd.Equals(validandoTelefone.ddd) && item.numero.Equals(validandoTelefone.numero) && item.ativo_inativo.Equals(true))
                        {
                            if (!item.codigo.Equals(validandoTelefone.codigo))
                            {
                                dadosValidos = false;
                                break;
                            }
                        }
                    }
                }

                if (dadosValidos.Equals(false))
                    gerenciarMensagensPadraoSistema.RegistroDuplicado("Esse Telefone", "Código: " + validandoTelefone.codigo + " - (" + validandoTelefone.ddd + ") " + validandoTelefone.numero);

                return dadosValidos;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
                return false;
            }
        }

        private void ModificarTabelaExcel_Telefone(string patchTabela)
        {
            try
            {
                List<Telefone> listaImpressao = new ListaTelefone();

                listaImpressao = listaTelefone;

                conexaoExcel = new Excel.Application();
                arquivoExcel = conexaoExcel.Workbooks.Open(patchTabela);
                planilhaExcel = arquivoExcel.Worksheets[1] as Excel.Worksheet;

                planilhaExcel.Cells[1, 1].Value = formularioDoUsuario_Cliente_Fornecedor + ": (" + pessoa.codigo.ToString()+ ")";
                planilhaExcel.Cells[1, 3].Value = "RAZÃO SOCIAL: " + pessoa.nome_razao_social.ToString();

                int linha = 3;
                foreach (Telefone item in listaImpressao)
                {
                    if (item.Pessoa.codigo.Equals(pessoa.codigo))
                    {
                        int coluna = 1;

                        planilhaExcel.Cells[linha, coluna++].Value = item.codigo.ToString();
                        planilhaExcel.Cells[linha, coluna++].Value = "(" + item.ddd.ToString() + ")";
                        planilhaExcel.Cells[linha, coluna++].Value = item.numero.ToString();
                        planilhaExcel.Cells[linha, coluna++].Value = item.observacao.ToString();

                        linha++;
                    }
                }

                arquivoExcel.Save();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
            finally
            {
                arquivoExcel.Close();
                conexaoExcel.Quit();
                arquivoExcel = null;
                planilhaExcel = null;
                conexaoExcel = null;
            }
        }

        private void AtivaInativaBotoes_TbSecundario_Telefone(bool ativaInativa, bool registrosJaInseridos)
        {
            try
            {
                gb_Telefone_Interno.Enabled = ativaInativa;
                btn_Telefone_Confirmar.Enabled = ativaInativa;
                btn_Telefone_Cancelar.Enabled = ativaInativa;

                btn_Telefone_Cadastrar.Enabled = !ativaInativa;
                dgv_Telefone.Enabled = !ativaInativa;

                if (registrosJaInseridos)
                {
                    btn_Telefone_Alterar.Enabled = !ativaInativa;
                    btn_Telefone_Deletar.Enabled = !ativaInativa;
                    btn_Telefone_Imprimir.Enabled = !ativaInativa;
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos KeyPress

        private void mtxtb_DDDTelefone_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Enter_FocusTxtb(txtb_NumeroTelefone, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_NumeroTelefone_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusButton(btn_Telefone_Confirmar, e))
                    CampoMoeda_KeyPress(sender, e, true);
                else
                    btn_Telefone_Confirmar_Click(sender, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Button Click

        private void btn_Telefone_Cadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                btn_Materiais_Vinculados.Enabled = false;

                Formulario_ModoModificacao(gb_ListagemGeral, gb_DadosGerais, tbc_Principal, tbc_Secundario, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar,
                    btn_DadosGerais_Deletar, btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_DadosGerais);
                Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_Endereco);

                AtivaInativaBotoes_TbSecundario_Telefone(true, true);

                operacaoRegistro_Telefone = "CADASTRO";
                LimparCamposParaCadastro_Telefone();

                mtxtb_DDDTelefone.Focus();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_Telefone_Alterar_Click(object sender, EventArgs e)
        {
            try
            {
                btn_Materiais_Vinculados.Enabled = false;

                Formulario_ModoModificacao(gb_ListagemGeral, gb_DadosGerais, tbc_Principal, tbc_Secundario, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar,
                    btn_DadosGerais_Deletar, btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_DadosGerais);
                Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_Endereco);

                AtivaInativaBotoes_TbSecundario_Telefone(true, true);

                operacaoRegistro_Telefone = "ALTERAÇÃO";
                mtxtb_DDDTelefone.Focus();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_Telefone_Deletar_Click(object sender, EventArgs e)
        {
            try
            {
                operacaoRegistro_Telefone = "INATIVAÇÃO";
                btn_Telefone_Confirmar_Click(sender, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_Telefone_Imprimir_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog diretorioDestinoArquivoImpressao = new FolderBrowserDialog();

                if (diretorioDestinoArquivoImpressao.ShowDialog().Equals(DialogResult.OK) && !string.IsNullOrWhiteSpace(diretorioDestinoArquivoImpressao.SelectedPath))
                {
                    telaLoadingFoiAberta = true;
                    gerenciarTelaLoading.Abrir(this, formTelaLoading, carregarThread);

                    string nomeArquivo = Path.GetFileName(arquivoListaParaImpressao_Telefone);

                    string nomeArquivoModificado = nomeArquivo.Replace(".xlsx", "");
                    nomeArquivoModificado = nomeArquivoModificado + " " + DateTime.Now.ToString("dd-MM-yyyy") + ".xlsx";

                    string arquivoListaModificado = Path.Combine(diretorioDestinoArquivoImpressao.SelectedPath, nomeArquivoModificado);

                    File.Copy(arquivoListaParaImpressao_Telefone, arquivoListaModificado, true);

                    ModificarTabelaExcel_Telefone(arquivoListaModificado);

                    if (telaLoadingFoiAberta)
                    {
                        telaLoadingFoiAberta = false;
                        gerenciarTelaLoading.Fechar();
                    }

                    if (gerenciarMensagensPadraoSistema.Mensagem_AbrirDocumento().Equals(DialogResult.OK))
                        Process.Start(arquivoListaModificado);
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_Telefone_Confirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (gerenciarMensagensPadraoSistema.Mensagem_Confirmacao(operacaoRegistro_Telefone).Equals(DialogResult.OK))
                {
                    Telefone objetoChecagem = new Telefone()
                    {
                        Pessoa = new Pessoa()
                    };

                    string statusOperacao = "EM ANDAMENTO";

                    if (ValidandoCompilandoDadosCampos_TbSecundario_Telefone(objetoChecagem))
                    {
                        if (operacaoRegistro_Telefone.Equals("CADASTRO") || operacaoRegistro_Telefone.Equals("ALTERAÇÃO")
                            || operacaoRegistro_Telefone.Equals("INATIVAÇÃO"))
                        {
                            statusOperacao = procTelefone.ManterRegistro(objetoChecagem, operacaoRegistro_Telefone);
                        }
                        else
                        {
                            gerenciarMensagensPadraoSistema.ExceptionBancoDados("A operação informada não existe !");
                            return;
                        }

                        int codigoRegistroRetornado = 0;
                        if (int.TryParse(statusOperacao, out codigoRegistroRetornado))
                        {
                            telefone = objetoChecagem;
                            telefone.codigo = codigoRegistroRetornado;

                            ObterListagemRegistrosDGV_Telefone();

                            Formulario_ModoConsulta(gb_ListagemGeral, gb_DadosGerais, dgv_Pessoa, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar, btn_DadosGerais_Deletar,
                                btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                            if (!tbc_Secundario.TabPages.Contains(tbp_Secundario_DadosGerais))
                                Formulario_AtivarAbaSecundaria(tbc_Secundario, tbp_Secundario_DadosGerais, 0);

                            if (!tbc_Secundario.TabPages.Contains(tbp_Secundario_Endereco))
                                Formulario_AtivarAbaSecundaria(tbc_Secundario, tbp_Secundario_Endereco, 2);
                            
                            AtivaInativaBotoes_TbSecundario_Telefone(false, true);

                            operacaoRegistro_Telefone = "";
                        }
                        else
                            gerenciarMensagensPadraoSistema.ExceptionBancoDados(statusOperacao);
                    }
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_Telefone_Cancelar_Click(object sender, EventArgs e)
        {
            try
            {
                if (gerenciarMensagensPadraoSistema.Mensagem_Cancelamento().Equals(DialogResult.OK))
                {
                    Formulario_ModoConsulta(gb_ListagemGeral, gb_DadosGerais, dgv_Pessoa, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar, btn_DadosGerais_Deletar,
                        btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                    if (!tbc_Secundario.TabPages.Contains(tbp_Secundario_DadosGerais))
                        Formulario_AtivarAbaSecundaria(tbc_Secundario, tbp_Secundario_DadosGerais, 0);

                    if (!tbc_Secundario.TabPages.Contains(tbp_Secundario_Endereco))
                        Formulario_AtivarAbaSecundaria(tbc_Secundario, tbp_Secundario_Endereco, 2);

                    bool registrosJaInseridos;
                    if (listaTelefone.Count > 0)
                        registrosJaInseridos = true;
                    else
                        registrosJaInseridos = false;

                    ObterListagemRegistrosDGV_Telefone();

                    AtivaInativaBotoes_TbSecundario_Telefone(false, registrosJaInseridos);

                    operacaoRegistro_Telefone = "";
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Data Grid View

        private void dgv_Telefone_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue.Equals(((int)Keys.Up)) || e.KeyValue.Equals(((int)Keys.Down)))
                    PreencherCampos_Telefone();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void dgv_Telefone_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                var linhaClicada = dgv_Telefone.HitTest(e.X, e.Y);
                if (linhaClicada != null && linhaClicada.RowIndex >= 0)
                {
                    if (e.Button.Equals(MouseButtons.Right))
                        dgv_Telefone.CurrentCell = dgv_Telefone.Rows[linhaClicada.RowIndex].Cells[0];

                    PreencherCampos_Telefone();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos MouseHovar

        private void lb_Obrigatorio_TE_1_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_TE_1, "numerico");
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void lb_Obrigatorio_TE_2_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_TE_2, "numerico");
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #endregion

        #region Endereço

        #region Funções Gerais

        private void LimparCamposParaCadastro_Endereco()
        {
            try
            {
                txtb_CodigoEndereco.Clear();
                txtb_Endereco.Clear();
                txtb_ComplementoEndereco.Clear();
                txtb_NumeroEndereco.Clear();
                cb_EstadoEndereco.SelectedIndex = 24;
                txtb_CidadeEndereco.Clear();
                txtb_BairroEndereco.Clear();
                mtxtb_CEPEndereco.Clear();
                txtb_ObservacaoEndereco.Clear();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void ObterListagemRegistrosDGV_Endereco()
        {
            try
            {
                Endereco item = new Endereco
                {
                    ativo_inativo = true,
                    Pessoa = pessoa
                };

                listaEndereco = procEndereco.ConsultarRegistro(item, false);

                CarregarListaDGV_Endereco();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void AdicionarLinhasDGV_Endereco(Endereco item)
        {
            try
            {
                dgv_Endereco.Rows.Add(item.codigo, item.endereco, item.complemento, item.numero, item.bairro, item.cidade, item.estado, item.cep, item.observacao);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void CarregarListaDGV_Endereco()
        {
            try
            {
                if (listaPessoa.Count > 0)
                {
                    if (listaEndereco.Count > 0)
                    {
                        dgv_Endereco.Rows.Clear();

                        foreach (Endereco item in listaEndereco)
                        {
                            if (item.Pessoa.codigo.Equals(pessoa.codigo))
                                AdicionarLinhasDGV_Endereco(item);
                        }

                        if (primeiraPesquisaModoPesquisa)
                        {
                            int resultado = 0;
                            primeiraPesquisaModoPesquisa = false;

                            Endereco converter = new Endereco();

                            foreach (Endereco item in listaEndereco.Where(e => e.codigo.Equals(endereco.codigo)))
                            {
                                resultado = (int)item.codigo;
                                break;
                            }

                            if (!resultado.Equals(0))
                            {
                                converter = listaEndereco.Where(e => e.codigo.Equals(endereco.codigo)).Last();

                                foreach (DataGridViewRow item in dgv_Endereco.Rows)
                                {
                                    if (item.Cells[colunaCodigoIndex].Value.Equals((int)converter.codigo))
                                        dgv_Endereco.CurrentCell = dgv_Endereco.Rows[item.Index].Cells[colunaCodigoIndex];
                                }
                            }
                            else
                            {
                                primeiraPesquisaModoPesquisa = true;
                                dgv_Endereco.Rows[0].Selected = true;
                            }
                        }
                        else
                            dgv_Endereco.Rows[0].Selected = true;

                        if (tbc_Secundario.TabPages.Contains(tbp_Secundario_Endereco))
                            PreencherCampos_Endereco();
                    }
                    else
                    {
                        primeiraPesquisaModoPesquisa = false;

                        dgv_Endereco.Rows.Clear();

                        GerenciarBotoes_ListagemSecundaria(false, true, btn_Endereco_Cadastrar, btn_Endereco_Alterar, btn_Endereco_Deletar, btn_Endereco_Imprimir);
                        LimparCamposParaCadastro_Endereco();
                    }

                    btn_Materiais_Vinculados.Enabled = true;
                }
                else
                {
                    btn_Materiais_Vinculados.Enabled = false;

                    primeiraPesquisaModoPesquisa = false;

                    dgv_Endereco.Rows.Clear();
                    
                    GerenciarBotoes_ListagemSecundaria(false, false, btn_Endereco_Cadastrar, btn_Endereco_Alterar, btn_Endereco_Deletar, btn_Endereco_Imprimir);
                    LimparCamposParaCadastro_Endereco();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void PreencherCampos_Endereco()
        {
            try
            {
                if (dgv_Endereco.RowCount > 0)
                {
                    int codigoLinhaSelecionada;

                    if (primeiraPesquisaModoPesquisa)
                    {
                        primeiraPesquisaModoPesquisa = false;

                        Endereco ende = listaEndereco.First();
                        codigoLinhaSelecionada = (int)ende.codigo;
                    }
                    else
                        codigoLinhaSelecionada = Convert.ToInt32(dgv_Endereco.Rows[dgv_Endereco.CurrentCell.RowIndex].Cells[colunaCodigoIndex].Value);

                    foreach (Endereco item in listaEndereco.Where(e => e.codigo.Equals(codigoLinhaSelecionada)))
                    {
                        txtb_CodigoEndereco.Text = item.codigo.ToString();
                        txtb_Endereco.Text = item.endereco.ToString();
                        txtb_ComplementoEndereco.Text = item.complemento.ToString();
                        txtb_NumeroEndereco.Text = item.numero.ToString();
                        cb_EstadoEndereco.SelectedIndex = cb_EstadoEndereco.FindStringExact(item.estado);
                        txtb_CidadeEndereco.Text = item.cidade.ToString();
                        txtb_BairroEndereco.Text = item.bairro.ToString();
                        mtxtb_CEPEndereco.Text = item.cep.ToString();
                        txtb_ObservacaoEndereco.Text = item.observacao.ToString();

                        endereco = item;
                        break;
                    }
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private bool ValidandoCompilandoDadosCampos_TbSecundario_Endereco(Endereco validandoEndereco)
        {
            try
            {
                bool dadosValidos = true;
                decimal variavelSaidaDecimal = 0;

                validandoEndereco.complemento = txtb_ComplementoEndereco.Text;
                validandoEndereco.cep = CampoMaskedTxt_RemoverFormatacao(mtxtb_CEPEndereco);
                validandoEndereco.observacao = txtb_ObservacaoEndereco.Text;
                validandoEndereco.ativo_inativo = true;

                if (dadosValidos)
                {
                    if (operacaoRegistro_Endereco.Equals("ALTERAÇÃO") || operacaoRegistro_Endereco.Equals("INATIVAÇÃO"))
                        validandoEndereco.codigo = int.Parse(txtb_CodigoEndereco.Text);
                    else
                        validandoEndereco.codigo = 0;
                }

                if (dadosValidos)
                {
                    if (txtb_Endereco.Text.Trim().Equals(""))
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("ENDEREÇO DO " + formularioDoUsuario_Cliente_Fornecedor);
                        dadosValidos = false;
                    }
                    else
                        validandoEndereco.endereco = txtb_Endereco.Text;
                }

                if (dadosValidos)
                {
                    if (decimal.TryParse(txtb_NumeroEndereco.Text.Trim(), out variavelSaidaDecimal))
                    {
                        if (variavelSaidaDecimal < 0)
                        {
                            gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("NÚMERO DO ENDEREÇO");
                            dadosValidos = false;
                        }
                        else
                            validandoEndereco.numero = txtb_NumeroEndereco.Text;
                    }
                    else
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("NÚMERO DO ENDEREÇO");
                        dadosValidos = false;
                    }
                }

                if (dadosValidos)
                {
                    if (cb_EstadoEndereco.Text.Trim().Equals("") || cb_EstadoEndereco.Text.Trim().Length != 2)
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("ESTADO(UF) DO ENDEREÇO");
                        dadosValidos = false;
                    }
                    else
                        validandoEndereco.estado = cb_EstadoEndereco.Text;
                }

                if (dadosValidos)
                {
                    if (txtb_CidadeEndereco.Text.Trim().Equals(""))
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("CIDADE DO ENDEREÇO");
                        dadosValidos = false;
                    }
                    else
                        validandoEndereco.cidade = txtb_CidadeEndereco.Text;
                }

                if (dadosValidos)
                {
                    if (txtb_BairroEndereco.Text.Trim().Equals(""))
                    {
                        gerenciarMensagensPadraoSistema.CampoEstaNullOuBranco("BAIRRO DO ENDEREÇO");
                        dadosValidos = false;
                    }
                    else
                        validandoEndereco.bairro = txtb_BairroEndereco.Text;
                }

                if (dadosValidos)
                {
                    operacaoRegistro = "ALTERAÇÃO";
                    if (dadosValidos = ValidandoCompilandoDadosCampos_TbPrincipal(pessoa))
                        validandoEndereco.Pessoa = pessoa;

                    operacaoRegistro = "";
                }

                if (dadosValidos)
                    dadosValidos = ValidandoCadastroRegistroDuplicado_TbSecundario_Endereco(validandoEndereco);

                return dadosValidos;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
                return false;
            }
        }

        private bool ValidandoCadastroRegistroDuplicado_TbSecundario_Endereco(Endereco validandoEndereco)
        {
            try
            {
                bool dadosValidos = true;

                foreach (Endereco item in listaEndereco)
                {
                    if (operacaoRegistro_Endereco.Equals("CADASTRO"))
                    {
                        if (item.endereco.Equals(validandoEndereco.endereco) && item.numero.Equals(validandoEndereco.numero) && item.estado.Equals(validandoEndereco.estado)
                             && item.cidade.Equals(validandoEndereco.cidade) && item.bairro.Equals(validandoEndereco.bairro) && item.ativo_inativo.Equals(true))
                        {
                            dadosValidos = false;
                            break;
                        }
                    }
                    else if (operacaoRegistro_Endereco.Equals("ALTERAÇÃO"))
                    {
                        if (item.endereco.Equals(validandoEndereco.endereco) && item.numero.Equals(validandoEndereco.numero) && item.estado.Equals(validandoEndereco.estado)
                             && item.cidade.Equals(validandoEndereco.cidade) && item.bairro.Equals(validandoEndereco.bairro) && item.ativo_inativo.Equals(true))
                        {
                            if (!item.codigo.Equals(validandoEndereco.codigo))
                            {
                                dadosValidos = false;
                                break;
                            }
                        }
                    }
                }

                if (dadosValidos.Equals(false))
                    gerenciarMensagensPadraoSistema.RegistroDuplicado("Esse Endereço", "Código: " + validandoEndereco.codigo + " - End: " + validandoEndereco.endereco + " n° " + validandoEndereco.numero
                        + " - " + validandoEndereco.estado + "/" + validandoEndereco.cidade + " - Bairro: " + validandoEndereco.bairro);

                return dadosValidos;
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
                return false;
            }
        }

        private void ModificarTabelaExcel_Endereco(string patchTabela)
        {
            try
            {
                List<Endereco> listaImpressao = new ListaEndereco();

                listaImpressao = listaEndereco;

                conexaoExcel = new Excel.Application();
                arquivoExcel = conexaoExcel.Workbooks.Open(patchTabela);
                planilhaExcel = arquivoExcel.Worksheets[1] as Excel.Worksheet;

                planilhaExcel.Cells[1, 1].Value = formularioDoUsuario_Cliente_Fornecedor + ": (" + pessoa.codigo.ToString() + ")";
                planilhaExcel.Cells[1, 2].Value = "RAZÃO SOCIAL: " + pessoa.nome_razao_social.ToString();

                int linha = 3;
                foreach (Endereco item in listaImpressao)
                {
                    if (item.Pessoa.codigo.Equals(pessoa.codigo))
                    {
                        int coluna = 1;

                        planilhaExcel.Cells[linha, coluna++].Value = item.codigo.ToString();
                        planilhaExcel.Cells[linha, coluna++].Value = item.endereco.ToString();
                        planilhaExcel.Cells[linha, coluna++].Value = item.complemento.ToString();
                        planilhaExcel.Cells[linha, coluna++].Value = item.numero.ToString();
                        planilhaExcel.Cells[linha, coluna++].Value = item.estado.ToString();
                        planilhaExcel.Cells[linha, coluna++].Value = item.cidade.ToString();
                        planilhaExcel.Cells[linha, coluna++].Value = item.bairro.ToString();
                        planilhaExcel.Cells[linha, coluna++].Value = item.cep.ToString();
                        planilhaExcel.Cells[linha, coluna++].Value = item.observacao.ToString();

                        linha++;
                    }
                }

                arquivoExcel.Save();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
            finally
            {
                arquivoExcel.Close();
                conexaoExcel.Quit();
                arquivoExcel = null;
                planilhaExcel = null;
                conexaoExcel = null;
            }
        }

        private void AtivaInativaBotoes_TbSecundario_Endereco(bool ativaInativa, bool registrosJaInseridos)
        {
            try
            {
                gb_Endereco_Interno.Enabled = ativaInativa;
                btn_Endereco_Confirmar.Enabled = ativaInativa;
                btn_Endereco_Cancelar.Enabled = ativaInativa;

                btn_Endereco_Cadastrar.Enabled = !ativaInativa;
                dgv_Endereco.Enabled = !ativaInativa;

                if (registrosJaInseridos)
                {
                    btn_Endereco_Alterar.Enabled = !ativaInativa;
                    btn_Endereco_Deletar.Enabled = !ativaInativa;
                    btn_Endereco_Imprimir.Enabled = !ativaInativa;
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos KeyPress

        private void txtb_Endereco_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Enter_FocusTxtb(txtb_NumeroEndereco, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_NumeroEndereco_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Enter_FocusTxtb(txtb_ComplementoEndereco, e))
                    CampoMoeda_KeyPress(sender, e, true);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_ComplementoEndereco_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Enter_FocusTxtb(txtb_CidadeEndereco, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_CidadeEndereco_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Enter_FocusTxtb(txtb_BairroEndereco, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void txtb_BairroEndereco_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Enter_FocusMaskedTxtb(mtxtb_CEPEndereco, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void mtxtb_CEPEndereco_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Enter_FocusButton(btn_Endereco_Confirmar, e))
                    btn_Endereco_Confirmar_Click(sender, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Button Click

        private void btn_Endereco_Cadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                btn_Materiais_Vinculados.Enabled = false;

                Formulario_ModoModificacao(gb_ListagemGeral, gb_DadosGerais, tbc_Principal, tbc_Secundario, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar,
                    btn_DadosGerais_Deletar, btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_DadosGerais);
                Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_Telefone);

                AtivaInativaBotoes_TbSecundario_Endereco(true, true);

                operacaoRegistro_Endereco = "CADASTRO";
                LimparCamposParaCadastro_Endereco();

                txtb_Endereco.Focus();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_Endereco_Alterar_Click(object sender, EventArgs e)
        {
            try
            {
                btn_Materiais_Vinculados.Enabled = false;

                Formulario_ModoModificacao(gb_ListagemGeral, gb_DadosGerais, tbc_Principal, tbc_Secundario, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar,
                    btn_DadosGerais_Deletar, btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_DadosGerais);
                Formulario_InativarAbaSecundaria(tbc_Secundario, tbp_Secundario_Telefone);

                AtivaInativaBotoes_TbSecundario_Endereco(true, true);

                operacaoRegistro_Endereco = "ALTERAÇÃO";
                txtb_Endereco.Focus();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_Endereco_Deletar_Click(object sender, EventArgs e)
        {
            try
            {
                operacaoRegistro_Telefone = "INATIVAÇÃO";
                btn_Endereco_Confirmar_Click(sender, e);
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_Endereco_Imprimir_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog diretorioDestinoArquivoImpressao = new FolderBrowserDialog();

                if (diretorioDestinoArquivoImpressao.ShowDialog().Equals(DialogResult.OK) && !string.IsNullOrWhiteSpace(diretorioDestinoArquivoImpressao.SelectedPath))
                {
                    telaLoadingFoiAberta = true;
                    gerenciarTelaLoading.Abrir(this, formTelaLoading, carregarThread);

                    string nomeArquivo = Path.GetFileName(arquivoListaParaImpressao_Endereco);

                    string nomeArquivoModificado = nomeArquivo.Replace(".xlsx", "");
                    nomeArquivoModificado = nomeArquivoModificado + " " + DateTime.Now.ToString("dd-MM-yyyy") + ".xlsx";

                    string arquivoListaModificado = Path.Combine(diretorioDestinoArquivoImpressao.SelectedPath, nomeArquivoModificado);

                    File.Copy(arquivoListaParaImpressao_Endereco, arquivoListaModificado, true);

                    ModificarTabelaExcel_Endereco(arquivoListaModificado);

                    if (telaLoadingFoiAberta)
                    {
                        telaLoadingFoiAberta = false;
                        gerenciarTelaLoading.Fechar();
                    }

                    if (gerenciarMensagensPadraoSistema.Mensagem_AbrirDocumento().Equals(DialogResult.OK))
                        Process.Start(arquivoListaModificado);
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_Endereco_Confirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (gerenciarMensagensPadraoSistema.Mensagem_Confirmacao(operacaoRegistro_Endereco).Equals(DialogResult.OK))
                {
                    Endereco objetoChecagem = new Endereco()
                    {
                        Pessoa = new Pessoa()
                    };

                    string statusOperacao = "EM ANDAMENTO";

                    if (ValidandoCompilandoDadosCampos_TbSecundario_Endereco(objetoChecagem))
                    {
                        if (operacaoRegistro_Endereco.Equals("CADASTRO") || operacaoRegistro_Endereco.Equals("ALTERAÇÃO")
                            || operacaoRegistro_Endereco.Equals("INATIVAÇÃO"))
                        {
                            statusOperacao = procEndereco.ManterRegistro(objetoChecagem, operacaoRegistro_Endereco);
                        }
                        else
                        {
                            gerenciarMensagensPadraoSistema.ExceptionBancoDados("A operação informada não existe !");
                            return;
                        }

                        int codigoRegistroRetornado = 0;
                        if (int.TryParse(statusOperacao, out codigoRegistroRetornado))
                        {
                            endereco = objetoChecagem;
                            endereco.codigo = codigoRegistroRetornado;

                            ObterListagemRegistrosDGV_Endereco();

                            Formulario_ModoConsulta(gb_ListagemGeral, gb_DadosGerais, dgv_Pessoa, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar, btn_DadosGerais_Deletar,
                                btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);
                            
                            if (!tbc_Secundario.TabPages.Contains(tbp_Secundario_DadosGerais))
                                Formulario_AtivarAbaSecundaria(tbc_Secundario, tbp_Secundario_DadosGerais, 0);

                            if (!tbc_Secundario.TabPages.Contains(tbp_Secundario_Telefone))
                                Formulario_AtivarAbaSecundaria(tbc_Secundario, tbp_Secundario_Telefone, 1);

                            AtivaInativaBotoes_TbSecundario_Endereco(false, true);

                            operacaoRegistro_Endereco = "";
                        }
                        else
                            gerenciarMensagensPadraoSistema.ExceptionBancoDados(statusOperacao);
                    }
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void btn_Endereco_Cancelar_Click(object sender, EventArgs e)
        {
            try
            {
                if (gerenciarMensagensPadraoSistema.Mensagem_Cancelamento().Equals(DialogResult.OK))
                {
                    Formulario_ModoConsulta(gb_ListagemGeral, gb_DadosGerais, dgv_Pessoa, btn_DadosGerais_Cadastrar, btn_DadosGerais_Alterar, btn_DadosGerais_Deletar,
                        btn_DadosGerais_Imprimir, btn_DadosGerais_VoltarGrid, btn_DadosGerais_Pesquisar, btn_DadosGerais_AvancarGrid, btn_RetornarModoPesquisa);

                    if (!tbc_Secundario.TabPages.Contains(tbp_Secundario_DadosGerais))
                        Formulario_AtivarAbaSecundaria(tbc_Secundario, tbp_Secundario_DadosGerais, 0);

                    if (!tbc_Secundario.TabPages.Contains(tbp_Secundario_Telefone))
                        Formulario_AtivarAbaSecundaria(tbc_Secundario, tbp_Secundario_Telefone, 1);

                    bool registrosJaInseridos;
                    if (listaEndereco.Count > 0)
                        registrosJaInseridos = true;
                    else
                        registrosJaInseridos = false;
                    
                    ObterListagemRegistrosDGV_Endereco();

                    AtivaInativaBotoes_TbSecundario_Endereco(false, registrosJaInseridos);

                    operacaoRegistro_Endereco = "";
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos Data Grid View

        private void dgv_Endereco_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue.Equals(((int)Keys.Up)) || e.KeyValue.Equals(((int)Keys.Down)))
                    PreencherCampos_Endereco();
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void dgv_Endereco_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                var linhaClicada = dgv_Endereco.HitTest(e.X, e.Y);
                if (linhaClicada != null && linhaClicada.RowIndex >= 0)
                {
                    if (e.Button.Equals(MouseButtons.Right))
                        dgv_Endereco.CurrentCell = dgv_Endereco.Rows[linhaClicada.RowIndex].Cells[0];

                    PreencherCampos_Endereco();
                }
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #region Eventos MouseHovar

        private void lb_Obrigatorio_ED_1_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_ED_1, "texto");
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void lb_Obrigatorio_ED_2_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_ED_2, "texto");
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void lb_Obrigatorio_ED_3_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_ED_3, "outros");
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void lb_Obrigatorio_ED_4_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_ED_4, "texto");
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        private void lb_Obrigatorio_ED_5_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Campo_PreenchimentoObrigatorio(lb_Obrigatorio_ED_5, "texto");
            }
            catch (Exception exception)
            {
                gerenciarMensagensPadraoSistema.MensagemException(exception);
            }
        }

        #endregion

        #endregion

    }
}
