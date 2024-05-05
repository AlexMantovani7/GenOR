using CamadaAcessoDados;
using CamadaObjetoTransferencia;
using System;
using System.Data;

namespace CamadaProcessamento
{
    public class ProcLogin
    {
        private AcessoDadosMySqlServer acessoDados = new AcessoDadosMySqlServer();

        public string ManterRegistro(Login login, string operacao)
        {
            try
            {
                acessoDados.LimparParametros();

                acessoDados.AdicionarParametro("@var_operacao", operacao);
                acessoDados.AdicionarParametro("@var_codigo", login.codigo);
                acessoDados.AdicionarParametro("@var_nome_usuario", login.nome_usuario);
                acessoDados.AdicionarParametro("@var_senha", login.senha);
                acessoDados.AdicionarParametro("@var_cod_Usuario", login.Usuario.codigo);

                return acessoDados.ExecutarScalar("sp_ManterLogin",
                    CommandType.StoredProcedure).ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ListaLogin ConsultarRegistro(Login login, bool pesquisarPorUsu)
        {
            try
            {
                acessoDados.LimparParametros();

                acessoDados.AdicionarParametro("@var_pesquisarPorUsu", pesquisarPorUsu);
                acessoDados.AdicionarParametro("@var_codigo", login.codigo);
                acessoDados.AdicionarParametro("@var_nome_usuario", login.nome_usuario);
                acessoDados.AdicionarParametro("@var_senha", login.senha);
                acessoDados.AdicionarParametro("@var_cod_Usuario", login.Usuario.codigo);

                DataTable tabela = acessoDados.ObterDataTable("sp_ConsultarLogin",
                    CommandType.StoredProcedure);

                ListaLogin lista = new ListaLogin();
                foreach (DataRow linha in tabela.Rows)
                {
                    login = new Login();

                    login.codigo = Convert.ToInt32(linha["codigo"]);
                    login.nome_usuario = linha["nome_usuario"].ToString();
                    login.senha = linha["senha"].ToString();

                    login.Usuario = new Pessoa()
                    {
                        codigo = Convert.ToInt32(linha["cod_Usuario"]),
                        tipo_pessoa = linha["tipo_pessoa"].ToString(),
                        nome_razao_social = linha["nome_razao_social"].ToString(),
                        nome_fantasia = linha["nome_fantasia"].ToString(),
                        cpf_cnpj = linha["cpf_cnpj"].ToString(),
                        inscricao_estadual = linha["inscricao_estadual"].ToString(),
                        email = linha["email"].ToString(),
                        observacao = linha["observacao"].ToString(),
                        ativo_inativo = Convert.ToBoolean(linha["ativo_inativo"])
                    };

                    lista.Add(login);
                }

                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
