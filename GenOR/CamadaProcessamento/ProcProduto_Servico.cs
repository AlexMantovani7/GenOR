using CamadaAcessoDados;
using CamadaObjetoTransferencia;
using System;
using System.Data;

namespace CamadaProcessamento
{
    public class ProcProduto_Servico
    {
        private AcessoDadosMySqlServer acessoDados = new AcessoDadosMySqlServer();
        
        public string ManterRegistro(Produto_Servico produto_servico, string operacao)
        {
            try
            {
                acessoDados.LimparParametros();

                acessoDados.AdicionarParametro("@var_operacao", operacao);
                acessoDados.AdicionarParametro("@var_codigo", produto_servico.codigo);
                acessoDados.AdicionarParametro("@var_imagem", produto_servico.imagem);
                acessoDados.AdicionarParametro("@var_descricao", produto_servico.descricao);
                acessoDados.AdicionarParametro("@var_altura", produto_servico.altura);
                acessoDados.AdicionarParametro("@var_largura", produto_servico.largura);
                acessoDados.AdicionarParametro("@var_comprimento", produto_servico.comprimento);
                acessoDados.AdicionarParametro("@var_valor_total_materiais", produto_servico.valor_total_materiais);
                acessoDados.AdicionarParametro("@var_maoObra", produto_servico.maoObra);
                acessoDados.AdicionarParametro("@var_ativo_inativo", produto_servico.ativo_inativo);
                acessoDados.AdicionarParametro("@var_cod_Unidade", produto_servico.Unidade.codigo);
                acessoDados.AdicionarParametro("@var_cod_Grupo", produto_servico.Grupo.codigo);

                return acessoDados.ExecutarScalar("sp_ManterProduto_Servico",
                    CommandType.StoredProcedure).ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ListaProduto_Servico ConsultarRegistro(Produto_Servico produto_servico, bool pesquisarTodos)
        {
            try
            {
                acessoDados.LimparParametros();

                acessoDados.AdicionarParametro("@var_pesquisarTodos", pesquisarTodos);
                acessoDados.AdicionarParametro("@var_codigo", produto_servico.codigo);
                acessoDados.AdicionarParametro("@var_ultima_atualizacao", produto_servico.ultima_atualizacao);
                acessoDados.AdicionarParametro("@var_imagem", produto_servico.imagem);
                acessoDados.AdicionarParametro("@var_descricao", produto_servico.descricao);
                acessoDados.AdicionarParametro("@var_altura", produto_servico.altura);
                acessoDados.AdicionarParametro("@var_largura", produto_servico.largura);
                acessoDados.AdicionarParametro("@var_comprimento", produto_servico.comprimento);
                acessoDados.AdicionarParametro("@var_valor_total_materiais", produto_servico.valor_total_materiais);
                acessoDados.AdicionarParametro("@var_maoObra", produto_servico.maoObra);
                acessoDados.AdicionarParametro("@var_valor_maoObra", produto_servico.valor_maoObra);
                acessoDados.AdicionarParametro("@var_valor_total", produto_servico.valor_total);
                acessoDados.AdicionarParametro("@var_ativo_inativo", produto_servico.ativo_inativo);
                acessoDados.AdicionarParametro("@var_cod_Unidade", produto_servico.Unidade.codigo);
                acessoDados.AdicionarParametro("@var_cod_Grupo", produto_servico.Grupo.codigo);

                DataTable tabela = acessoDados.ObterDataTable("sp_ConsultarProduto_Servico",
                    CommandType.StoredProcedure);

                ListaProduto_Servico lista = new ListaProduto_Servico();
                foreach (DataRow linha in tabela.Rows)
                {
                    produto_servico = new Produto_Servico();

                    produto_servico.codigo = Convert.ToInt32(linha["codigo"]);
                    produto_servico.ultima_atualizacao = Convert.ToDateTime(linha["ultima_atualizacao"]);
                    produto_servico.imagem = linha["imagem"].ToString();
                    produto_servico.descricao = linha["descricao_Produto_Servico"].ToString();
                    produto_servico.altura = Convert.ToDecimal(linha["altura"]);
                    produto_servico.largura = Convert.ToDecimal(linha["largura"]);
                    produto_servico.comprimento = Convert.ToDecimal(linha["comprimento"]);
                    produto_servico.valor_total_materiais = Convert.ToDecimal(linha["valor_total_materiais"]);
                    produto_servico.maoObra = Convert.ToDecimal(linha["maoObra"]);
                    produto_servico.valor_maoObra = Convert.ToDecimal(linha["valor_maoObra"]);
                    produto_servico.valor_total = Convert.ToDecimal(linha["valor_total"]);
                    produto_servico.ativo_inativo = Convert.ToBoolean(linha["ativo_inativo_Produto_Servico"]);

                    produto_servico.Unidade = new Grupo_Unidade()
                    {
                        codigo = Convert.ToInt32(linha["cod_Unidade"]),
                        sigla = linha["sigla"].ToString(),
                        descricao = linha["descricao_Unidade"].ToString(),
                        ativo_inativo = Convert.ToBoolean(linha["ativo_inativo_Unidade"])
                    };

                    produto_servico.Grupo = new Grupo_Unidade()
                    {
                        codigo = Convert.ToInt32(linha["cod_Grupo"]),
                        descricao = linha["descricao_Grupo"].ToString(),
                        material_ou_produto = Convert.ToChar(linha["material_ou_produto"]),
                        ativo_inativo = Convert.ToBoolean(linha["ativo_inativo_Grupo"])
                    };

                    lista.Add(produto_servico);
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
