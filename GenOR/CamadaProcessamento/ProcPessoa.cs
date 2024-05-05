using System;
using System.Data;
using CamadaAcessoDados;
using CamadaObjetoTransferencia;

namespace CamadaProcessamento
{
    public class ProcPessoa
    {
        private AcessoDadosMySqlServer acessoDados = new AcessoDadosMySqlServer();

        public string ManterRegistro(Pessoa pessoa, string operacao)
        {
            try
            {
                acessoDados.LimparParametros();

                acessoDados.AdicionarParametro("@var_operacao", operacao);
                acessoDados.AdicionarParametro("@var_codigo", pessoa.codigo);
                acessoDados.AdicionarParametro("@var_tipo_pessoa", pessoa.tipo_pessoa);
                acessoDados.AdicionarParametro("@var_nome_razao_social", pessoa.nome_razao_social);
                acessoDados.AdicionarParametro("@var_nome_fantasia", pessoa.nome_fantasia);
                acessoDados.AdicionarParametro("@var_cpf_cnpj", pessoa.cpf_cnpj);
                acessoDados.AdicionarParametro("@var_inscricao_estadual", pessoa.inscricao_estadual);
                acessoDados.AdicionarParametro("@var_email", pessoa.email);
                acessoDados.AdicionarParametro("@var_observacao", pessoa.observacao);
                acessoDados.AdicionarParametro("@var_ativo_inativo", pessoa.ativo_inativo);

                return acessoDados.ExecutarScalar("sp_ManterPessoa",
                    CommandType.StoredProcedure).ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ListaPessoa ConsultarRegistro(Pessoa pessoa, bool pesquisarTodos)
        {
            try
            {
                acessoDados.LimparParametros();

                acessoDados.AdicionarParametro("@var_pesquisarTodos", pesquisarTodos);
                acessoDados.AdicionarParametro("@var_codigo", pessoa.codigo);
                acessoDados.AdicionarParametro("@var_tipo_pessoa", pessoa.tipo_pessoa);
                acessoDados.AdicionarParametro("@var_nome_razao_social", pessoa.nome_razao_social);
                acessoDados.AdicionarParametro("@var_nome_fantasia", pessoa.nome_fantasia);
                acessoDados.AdicionarParametro("@var_cpf_cnpj", pessoa.cpf_cnpj);
                acessoDados.AdicionarParametro("@var_inscricao_estadual", pessoa.inscricao_estadual);
                acessoDados.AdicionarParametro("@var_email", pessoa.email);
                acessoDados.AdicionarParametro("@var_observacao", pessoa.observacao);
                acessoDados.AdicionarParametro("@var_ativo_inativo", pessoa.ativo_inativo);

                DataTable tabela = acessoDados.ObterDataTable("sp_ConsultarPessoa",
                    CommandType.StoredProcedure);

                ListaPessoa lista = new ListaPessoa();
                foreach (DataRow linha in tabela.Rows)
                {
                    pessoa = new Pessoa();

                    pessoa.codigo = Convert.ToInt32(linha["codigo"]);
                    pessoa.tipo_pessoa = linha["tipo_pessoa"].ToString();
                    pessoa.nome_razao_social = linha["nome_razao_social"].ToString();
                    pessoa.nome_fantasia = linha["nome_fantasia"].ToString();
                    pessoa.cpf_cnpj = linha["cpf_cnpj"].ToString();
                    pessoa.inscricao_estadual = linha["inscricao_estadual"].ToString();
                    pessoa.email = linha["email"].ToString();
                    pessoa.observacao = linha["observacao"].ToString();
                    pessoa.ativo_inativo = Convert.ToBoolean(linha["ativo_inativo"]);

                    lista.Add(pessoa);
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
