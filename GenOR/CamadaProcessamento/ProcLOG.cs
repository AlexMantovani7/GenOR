using CamadaAcessoDados;
using CamadaObjetoTransferencia;
using System;
using System.Data;

namespace CamadaProcessamento
{
    public class ProcLOG
    {
        private AcessoDadosMySqlServer acessoDados = new AcessoDadosMySqlServer();

        public ListaLOG ConsultarRegistro(LOG log, bool pesquisarTodos)
        {
            try
            {
                acessoDados.LimparParametros();

                acessoDados.AdicionarParametro("@var_pesquisarTodos", pesquisarTodos);
                acessoDados.AdicionarParametro("@var_codigo", log.codigo);
                acessoDados.AdicionarParametro("@var_data_registro", log.data_registro);
                acessoDados.AdicionarParametro("@var_operacao", log.operacao);
                acessoDados.AdicionarParametro("@var_registro", log.registro);
                acessoDados.AdicionarParametro("@var_informacoes_registro", log.informacoes_registro);

                DataTable tabela = acessoDados.ObterDataTable("sp_ConsultarLOG",
                    CommandType.StoredProcedure);

                ListaLOG lista = new ListaLOG();
                foreach (DataRow linha in tabela.Rows)
                {
                    log = new LOG();

                    log.codigo = Convert.ToInt32(linha["codigo"]);
                    log.data_registro = Convert.ToDateTime(linha["data_registro"]);
                    log.operacao = linha["operacao"].ToString();
                    log.registro = linha["registro"].ToString();
                    log.informacoes_registro = linha["informacoes_registro"].ToString();

                    lista.Add(log);
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
