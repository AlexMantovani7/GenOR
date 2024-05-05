using CamadaAcessoDados;
using CamadaObjetoTransferencia;
using System;
using System.Data;

namespace CamadaProcessamento
{
    public class ProcUnidade
    {
        private AcessoDadosMySqlServer acessoDados = new AcessoDadosMySqlServer();

        public string ManterRegistro(Grupo_Unidade grupo_Unidade, string operacao)
        {
            try
            {
                acessoDados.LimparParametros();

                acessoDados.AdicionarParametro("@var_operacao", operacao);
                acessoDados.AdicionarParametro("@var_codigo", grupo_Unidade.codigo);
                acessoDados.AdicionarParametro("@var_sigla", grupo_Unidade.sigla);
                acessoDados.AdicionarParametro("@var_descricao", grupo_Unidade.descricao);
                acessoDados.AdicionarParametro("@var_ativo_inativo", grupo_Unidade.ativo_inativo);

                return acessoDados.ExecutarScalar("sp_ManterUnidade",
                    CommandType.StoredProcedure).ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ListaGrupo_Unidade ConsultarRegistro(Grupo_Unidade grupo_Unidade, bool pesquisarTodos)
        {
            try
            {
                acessoDados.LimparParametros();

                acessoDados.AdicionarParametro("@var_pesquisarTodos", pesquisarTodos);
                acessoDados.AdicionarParametro("@var_codigo", grupo_Unidade.codigo);
                acessoDados.AdicionarParametro("@var_sigla", grupo_Unidade.sigla);
                acessoDados.AdicionarParametro("@var_descricao", grupo_Unidade.descricao);
                acessoDados.AdicionarParametro("@var_ativo_inativo", grupo_Unidade.ativo_inativo);

                DataTable tabela = acessoDados.ObterDataTable("sp_ConsultarUnidade",
                    CommandType.StoredProcedure);

                ListaGrupo_Unidade lista = new ListaGrupo_Unidade();
                foreach (DataRow linha in tabela.Rows)
                {
                    grupo_Unidade = new Grupo_Unidade();

                    grupo_Unidade.codigo = Convert.ToInt32(linha["codigo"]);
                    grupo_Unidade.sigla = linha["sigla"].ToString();
                    grupo_Unidade.descricao = linha["descricao"].ToString();
                    grupo_Unidade.ativo_inativo = Convert.ToBoolean(linha["ativo_inativo"]);

                    lista.Add(grupo_Unidade);
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
