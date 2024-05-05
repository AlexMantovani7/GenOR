using CamadaAcessoDados;
using CamadaObjetoTransferencia;
using System;
using System.Data;

namespace CamadaProcessamento
{
    public class ProcTelefone
    {
        private AcessoDadosMySqlServer acessoDados = new AcessoDadosMySqlServer();

        public string ManterRegistro(Telefone telefone, string operacao)
        {
            try
            {
                acessoDados.LimparParametros();

                acessoDados.AdicionarParametro("@var_operacao", operacao);
                acessoDados.AdicionarParametro("@var_codigo", telefone.codigo);
                acessoDados.AdicionarParametro("@var_ddd", telefone.ddd);
                acessoDados.AdicionarParametro("@var_numero", telefone.numero);
                acessoDados.AdicionarParametro("@var_observacao", telefone.observacao);
                acessoDados.AdicionarParametro("@var_ativo_inativo", telefone.ativo_inativo);
                acessoDados.AdicionarParametro("@var_cod_Pessoa", telefone.Pessoa.codigo);

                return acessoDados.ExecutarScalar("sp_ManterTelefone",
                    CommandType.StoredProcedure).ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ListaTelefone ConsultarRegistro(Telefone telefone, bool pesquisarTodos)
        {
            try
            {
                acessoDados.LimparParametros();

                acessoDados.AdicionarParametro("@var_pesquisarTodos", pesquisarTodos);
                acessoDados.AdicionarParametro("@var_codigo", telefone.codigo);
                acessoDados.AdicionarParametro("@var_ddd", telefone.ddd);
                acessoDados.AdicionarParametro("@var_numero", telefone.numero);
                acessoDados.AdicionarParametro("@var_observacao", telefone.observacao);
                acessoDados.AdicionarParametro("@var_ativo_inativo", telefone.ativo_inativo);
                acessoDados.AdicionarParametro("@var_cod_Pessoa", telefone.Pessoa.codigo);

                DataTable tabela = acessoDados.ObterDataTable("sp_ConsultarTelefone",
                    CommandType.StoredProcedure);

                ListaTelefone lista = new ListaTelefone();
                foreach (DataRow linha in tabela.Rows)
                {
                    telefone = new Telefone();

                    telefone.codigo = Convert.ToInt32(linha["codigo"]);
                    telefone.ddd = linha["ddd"].ToString();
                    telefone.numero = linha["numero"].ToString();
                    telefone.observacao = linha["observacao"].ToString();
                    telefone.ativo_inativo = Convert.ToBoolean(linha["ativo_inativo_Telefone"]);

                    telefone.Pessoa = new Pessoa()
                    {
                        codigo = Convert.ToInt32(linha["cod_Pessoa"]),
                        tipo_pessoa = linha["tipo_pessoa"].ToString(),
                        nome_razao_social = linha["nome_razao_social"].ToString(),
                        nome_fantasia = linha["nome_fantasia"].ToString(),
                        cpf_cnpj = linha["cpf_cnpj"].ToString(),
                        inscricao_estadual = linha["inscricao_estadual"].ToString(),
                        email = linha["email"].ToString(),
                        observacao = linha["observacao_Pessoa"].ToString(),
                        ativo_inativo = Convert.ToBoolean(linha["ativo_inativo_Pessoa"])
                    };

                    lista.Add(telefone);
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
