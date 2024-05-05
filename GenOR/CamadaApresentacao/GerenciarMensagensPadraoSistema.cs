using System;
using System.Windows.Forms;

namespace GenOR
{
    public class GerenciarMensagensPadraoSistema
    {
        #region Mensagens Para Confirmação

        public DialogResult Mensagem_Confirmacao(string operacaoRealizada)
        {
            try
            {
                return MessageBox.Show("Deseja prosseguir com ( " + operacaoRealizada + " ) desse(s) registro(s) ?", "CONFIRMAR " + operacaoRealizada, MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DialogResult Mensagem_Cancelamento()
        {
            try
            {
                return MessageBox.Show("CANCELAR MODIFICAÇÕES REALIZADAS ?", "CANCELANDO MODIFICAÇÕES ", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand,
                    MessageBoxDefaultButton.Button2);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DialogResult Mensagem_FechamentoJanela(string condicaoExtra)
        {
            try
            {
                return MessageBox.Show("Deseja fechar essa janela " + condicaoExtra + "?", "CONFIRMAR ENCERRAMENTO", MessageBoxButtons.OKCancel, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button2);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DialogResult Mensagem_AbrirDocumento()
        {
            try
            {
                return MessageBox.Show("Deseja abrir o Documento criado ?", "ABRIR DOCUMENTO", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Mensagens Referentes aos Campos

        public DialogResult CampoEstaNullOuBranco(string campo)
        {
            try
            {
                return MessageBox.Show("O campo: ( " + campo + " ) não contem INFORMAÇÕES VÁLIDAS ! \n \n Verifique se o campo não está vazio, se contem algum caractere incorreto ou se os dados estão ativos !",
                    "INFORMAÇÕES INVÁLIDAS", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DialogResult CampoMoedaZeradoInvalido(string campo)
        {
            try
            {
                return MessageBox.Show("O campo: ( " + campo + " ) não permite um VALOR ZERADO, NEGATIVO ou EM BRANCO ! \n \n Verifique se o campo não está vazio ou se contem um valor valido acima de ZERO !",
                    "INFORMAÇÕES INVÁLIDAS", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public DialogResult CampoDuploMoedaNullBranco(string campo)
        {
            try
            {
                return MessageBox.Show("O campo: ( " + campo + " ) não permite que VALOR INICIAL ultrapassa o VALOR FINAL ou contenha um VALOR ZERADO, NEGATIVO ou EM BRANCO !",
                    "INFORMAÇÕES INVÁLIDAS", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DialogResult RegistroDuplicado(string campo, string informacaoCampo)
        {
            try
            {
                return MessageBox.Show(campo + " já foi registrado e está ativo ! \n \n Não será possivel finalizar o cadastro/alteração de  \n ( " + informacaoCampo + " ) !",
                    "REGISTRO JÁ CADASTRADO", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public DialogResult CampoDuploDataNullBrancoOuDatado(string campo)
        {
            try
            {
                return MessageBox.Show("O campo: ( " + campo + " ) não pode conter um VALOR EM BRANCO, DATA informada ultrapassa a DATA DO DIA ATUAL ou DATA INICIAL ultrapassa a DATA FINAL !",
                    "INFORMAÇÕES INVÁLIDAS", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DialogResult ArquivoImagemInvalido()
        {
            try
            {
                return MessageBox.Show("O arquivo selecionado NÃO É ou NÃO SE ENCAIXA nos tipos de imagens aceitas pelo sistema (PNG, JPEG, JPG) !", "INFORMAÇÕES INVÁLIDAS",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Mensagens Exception/Erros
        
        public DialogResult MensagemException(Exception exception)
        {
            try
            {
                string mensagemErro = "Ops !!! Ocorrou alguma falha no sistema !\n(RECOMENDADO ENTRAR EM CONTATO COM A EMPRESA CONTRATANTE DO SISTEMA).\n\nMENSAGEM EXCEPTION: '" + exception.Message + "'\n\nSTACK TRACE: (" + exception.StackTrace + ")";
                return MessageBox.Show(mensagemErro, "FALHA NO SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DialogResult ExceptionBancoDados(string ex)
        {
            try
            {
                return MessageBox.Show("A seguinte EXCEPTION ocorreu ao contactar as informações no Banco de Dados: \n \n" + ex, "EXCEPTION BANCO DE DADOS", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public DialogResult InformacaoPesquisadaNaoEncontrada()
        {
            try
            {
                return MessageBox.Show("Não foi encontrado registros validos baseado na pesquisa informada !",
                    "INFORMAÇÕES NÃO ENCONTRADAS", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public DialogResult LoginOuSenhaIncorretos()
        {
            try
            {
                return MessageBox.Show("Login ou Senha estão Incorretos ! \n \n Verifique se os campos estão preenchidos corretamete ou entre em contato com o Desenvolvedor !",
                    "USUARIO NÃO ENCONTRADO", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public DialogResult UsuarioModificadoOuRemovido()
        {
            try
            {
                return MessageBox.Show("O Usuario logado foi modificado ou removido do sistema ! \n \n Efetue o processo de Login novamente (com informações atualizadas) ou entre em contato com o Desenvolvedor !",
                    "USUARIO NÃO ENCONTRADO", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Mensagens Sucesso ou Falha
        
        public DialogResult Mensagem_Sucesso(string operacao)
        {
            return MessageBox.Show("O processo de (" + operacao + ") foi um SUCESSO !", operacao + " SUCESSO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public DialogResult Mensagem_Falha(string operacao)
        {
            return MessageBox.Show("FALHA ao realizar o processo de (" + operacao + ").", operacao + " FALHOU", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        
        public DialogResult ConnexaoBD_Sucesso()
        {
            return MessageBox.Show("Conexão com o Banco de Dados estabelecida com sucesso !", "CONEXÃO ESTABELECIDA", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public DialogResult ConnexaoBD_Error()
        {
            return MessageBox.Show("Erro ao tentar estabelecer Conexão com o Banco de Dados. \n\n Verifique as informações e a conectividade do seu Banco de Dados.",
                "ERRO CONEXÃO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        #endregion

    }
}
