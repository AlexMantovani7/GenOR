using CamadaAcessoDados;
using CamadaObjetoTransferencia;
using System;
using System.Data;

namespace CamadaProcessamento
{
    public class ProcEndereco
    {
        private AcessoDadosMySqlServer acessoDados = new AcessoDadosMySqlServer();
        
        public string ManterRegistro(Endereco endereco, string operacao)
        {
            try
            {
                acessoDados.LimparParametros();
                
                acessoDados.AdicionarParametro("@var_operacao", operacao);
                acessoDados.AdicionarParametro("@var_codigo", endereco.codigo);
                acessoDados.AdicionarParametro("@var_endereco", endereco.endereco);
                acessoDados.AdicionarParametro("@var_complemento", endereco.complemento);
                acessoDados.AdicionarParametro("@var_numero", endereco.numero);
                acessoDados.AdicionarParametro("@var_bairro", endereco.bairro);
                acessoDados.AdicionarParametro("@var_cidade", endereco.cidade);
                acessoDados.AdicionarParametro("@var_estado", endereco.estado);
                acessoDados.AdicionarParametro("@var_cep", endereco.cep);
                acessoDados.AdicionarParametro("@var_observacao", endereco.observacao);
                acessoDados.AdicionarParametro("@var_ativo_inativo", endereco.ativo_inativo);
                acessoDados.AdicionarParametro("@var_cod_Pessoa", endereco.Pessoa.codigo);

                return acessoDados.ExecutarScalar("sp_ManterEndereco",
                    CommandType.StoredProcedure).ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ListaEndereco ConsultarRegistro(Endereco endereco, bool pesquisarTodos)
        {
            try
            {
                acessoDados.LimparParametros();

                acessoDados.AdicionarParametro("@var_pesquisarTodos", pesquisarTodos);
                acessoDados.AdicionarParametro("@var_codigo", endereco.codigo);
                acessoDados.AdicionarParametro("@var_endereco", endereco.endereco);
                acessoDados.AdicionarParametro("@var_complemento", endereco.complemento);
                acessoDados.AdicionarParametro("@var_numero", endereco.numero);
                acessoDados.AdicionarParametro("@var_bairro", endereco.bairro);
                acessoDados.AdicionarParametro("@var_cidade", endereco.cidade);
                acessoDados.AdicionarParametro("@var_estado", endereco.estado);
                acessoDados.AdicionarParametro("@var_cep", endereco.cep);
                acessoDados.AdicionarParametro("@var_observacao", endereco.observacao);
                acessoDados.AdicionarParametro("@var_ativo_inativo", endereco.ativo_inativo);
                acessoDados.AdicionarParametro("@var_cod_Pessoa", endereco.Pessoa.codigo);

                DataTable tabela = acessoDados.ObterDataTable("sp_ConsultarEndereco",
                    CommandType.StoredProcedure);

                ListaEndereco lista = new ListaEndereco();
                foreach (DataRow linha in tabela.Rows)
                {
                    endereco = new Endereco();

                    endereco.codigo = Convert.ToInt32(linha["codigo"]);
                    endereco.endereco = linha["endereco"].ToString();
                    endereco.complemento = linha["complemento"].ToString();
                    endereco.numero = linha["numero"].ToString();
                    endereco.bairro = linha["bairro"].ToString();
                    endereco.cidade = linha["cidade"].ToString();
                    endereco.estado = linha["estado"].ToString();
                    endereco.cep = linha["cep"].ToString();
                    endereco.observacao = linha["observacao"].ToString();
                    endereco.ativo_inativo = Convert.ToBoolean(linha["ativo_inativo_Endereco"]);

                    endereco.Pessoa = new Pessoa()
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

                    lista.Add(endereco);
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
