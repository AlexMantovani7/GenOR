using CamadaAcessoDados;
using CamadaObjetoTransferencia;
using System;
using System.Data;

namespace CamadaProcessamento
{
    public class ProcProdutos_Servicos_Orcamento
    {
        private AcessoDadosMySqlServer acessoDados = new AcessoDadosMySqlServer();

        public string ManterRegistro(Produtos_Servicos_Orcamento produtos_servicos_orcamento, string operacao)
        {
            try
            {
                acessoDados.LimparParametros();

                acessoDados.AdicionarParametro("@var_operacao", operacao);
                acessoDados.AdicionarParametro("@var_codigo", produtos_servicos_orcamento.codigo);
                acessoDados.AdicionarParametro("@var_quantidade", produtos_servicos_orcamento.quantidade);
                acessoDados.AdicionarParametro("@var_valor_total", produtos_servicos_orcamento.valor_total);
                acessoDados.AdicionarParametro("@var_cod_Produto_Servico", produtos_servicos_orcamento.Produto_Servico.codigo);
                acessoDados.AdicionarParametro("@var_ultima_atualizacao_Produto_Servico", produtos_servicos_orcamento.Produto_Servico.ultima_atualizacao);
                acessoDados.AdicionarParametro("@var_imagem_Produto_Servico", produtos_servicos_orcamento.Produto_Servico.imagem);
                acessoDados.AdicionarParametro("@var_descricao_Produto_Servico", produtos_servicos_orcamento.Produto_Servico.descricao);
                acessoDados.AdicionarParametro("@var_altura_Produto_Servico", produtos_servicos_orcamento.Produto_Servico.altura);
                acessoDados.AdicionarParametro("@var_largura_Produto_Servico", produtos_servicos_orcamento.Produto_Servico.largura);
                acessoDados.AdicionarParametro("@var_comprimento_Produto_Servico", produtos_servicos_orcamento.Produto_Servico.comprimento);
                acessoDados.AdicionarParametro("@var_valor_total_materiais_Produto_Servico", produtos_servicos_orcamento.Produto_Servico.valor_total_materiais);
                acessoDados.AdicionarParametro("@var_maoObra_Produto_Servico", produtos_servicos_orcamento.Produto_Servico.maoObra);
                acessoDados.AdicionarParametro("@var_valor_maoObra_Produto_Servico", produtos_servicos_orcamento.Produto_Servico.valor_maoObra);
                acessoDados.AdicionarParametro("@var_valor_unitario_Produto_Servico", produtos_servicos_orcamento.Produto_Servico.valor_total);
                acessoDados.AdicionarParametro("@var_ativo_inativo_Produto_Servico", produtos_servicos_orcamento.Produto_Servico.ativo_inativo);
                acessoDados.AdicionarParametro("@var_cod_Unidade", produtos_servicos_orcamento.Produto_Servico.Unidade.codigo);
                acessoDados.AdicionarParametro("@var_sigla_Unidade", produtos_servicos_orcamento.Produto_Servico.Unidade.sigla);
                acessoDados.AdicionarParametro("@var_descricao_Unidade", produtos_servicos_orcamento.Produto_Servico.Unidade.descricao);
                acessoDados.AdicionarParametro("@var_ativo_inativo_Unidade", produtos_servicos_orcamento.Produto_Servico.Unidade.ativo_inativo);
                acessoDados.AdicionarParametro("@var_cod_Grupo", produtos_servicos_orcamento.Produto_Servico.Grupo.codigo);
                acessoDados.AdicionarParametro("@var_descricao_Grupo", produtos_servicos_orcamento.Produto_Servico.Grupo.descricao);
                acessoDados.AdicionarParametro("@var_ativo_inativo_Grupo", produtos_servicos_orcamento.Produto_Servico.Grupo.ativo_inativo);
                acessoDados.AdicionarParametro("@var_cod_Orcamento", produtos_servicos_orcamento.Orcamento.codigo);

                return acessoDados.ExecutarScalar("sp_ManterProdutos_Servicos_Orcamento",
                    CommandType.StoredProcedure).ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ListaProdutos_Servicos_Orcamento ConsultarRegistro(Produtos_Servicos_Orcamento produtos_servicos_orcamento, bool pesquisarTodos)
        {
            try
            {
                acessoDados.LimparParametros();

                acessoDados.AdicionarParametro("@var_pesquisarTodos", pesquisarTodos);
                acessoDados.AdicionarParametro("@var_codigo", produtos_servicos_orcamento.codigo);
                acessoDados.AdicionarParametro("@var_quantidade", produtos_servicos_orcamento.quantidade);
                acessoDados.AdicionarParametro("@var_valor_total", produtos_servicos_orcamento.valor_total);
                acessoDados.AdicionarParametro("@var_cod_Produto_Servico", produtos_servicos_orcamento.Produto_Servico.codigo);
                acessoDados.AdicionarParametro("@var_ultima_atualizacao_Produto_Servico", produtos_servicos_orcamento.Produto_Servico.ultima_atualizacao);
                acessoDados.AdicionarParametro("@var_imagem_Produto_Servico", produtos_servicos_orcamento.Produto_Servico.imagem);
                acessoDados.AdicionarParametro("@var_descricao_Produto_Servico", produtos_servicos_orcamento.Produto_Servico.descricao);
                acessoDados.AdicionarParametro("@var_altura_Produto_Servico", produtos_servicos_orcamento.Produto_Servico.altura);
                acessoDados.AdicionarParametro("@var_largura_Produto_Servico", produtos_servicos_orcamento.Produto_Servico.largura);
                acessoDados.AdicionarParametro("@var_comprimento_Produto_Servico", produtos_servicos_orcamento.Produto_Servico.comprimento);
                acessoDados.AdicionarParametro("@var_valor_total_materiais_Produto_Servico", produtos_servicos_orcamento.Produto_Servico.valor_total_materiais);
                acessoDados.AdicionarParametro("@var_maoObra_Produto_Servico", produtos_servicos_orcamento.Produto_Servico.maoObra);
                acessoDados.AdicionarParametro("@var_valor_maoObra_Produto_Servico", produtos_servicos_orcamento.Produto_Servico.valor_maoObra);
                acessoDados.AdicionarParametro("@var_valor_unitario_Produto_Servico", produtos_servicos_orcamento.Produto_Servico.valor_total);
                acessoDados.AdicionarParametro("@var_ativo_inativo_Produto_Servico", produtos_servicos_orcamento.Produto_Servico.ativo_inativo);
                acessoDados.AdicionarParametro("@var_cod_Unidade", produtos_servicos_orcamento.Produto_Servico.Unidade.codigo);
                acessoDados.AdicionarParametro("@var_sigla_Unidade", produtos_servicos_orcamento.Produto_Servico.Unidade.sigla);
                acessoDados.AdicionarParametro("@var_descricao_Unidade", produtos_servicos_orcamento.Produto_Servico.Unidade.descricao);
                acessoDados.AdicionarParametro("@var_ativo_inativo_Unidade", produtos_servicos_orcamento.Produto_Servico.Unidade.ativo_inativo);
                acessoDados.AdicionarParametro("@var_cod_Grupo", produtos_servicos_orcamento.Produto_Servico.Grupo.codigo);
                acessoDados.AdicionarParametro("@var_descricao_Grupo", produtos_servicos_orcamento.Produto_Servico.Grupo.descricao);
                acessoDados.AdicionarParametro("@var_ativo_inativo_Grupo", produtos_servicos_orcamento.Produto_Servico.Grupo.ativo_inativo);
                acessoDados.AdicionarParametro("@var_cod_Orcamento", produtos_servicos_orcamento.Orcamento.codigo);

                DataTable tabela = acessoDados.ObterDataTable("sp_ConsultarProdutos_Servicos_Orcamento",
                    CommandType.StoredProcedure);

                ListaProdutos_Servicos_Orcamento lista = new ListaProdutos_Servicos_Orcamento();
                foreach (DataRow linha in tabela.Rows)
                {
                    produtos_servicos_orcamento = new Produtos_Servicos_Orcamento();

                    produtos_servicos_orcamento.codigo = Convert.ToInt32(linha["codigo"]);
                    produtos_servicos_orcamento.quantidade = Convert.ToDecimal(linha["quantidade"]);
                    produtos_servicos_orcamento.valor_total = Convert.ToDecimal(linha["valor_total"]);

                    produtos_servicos_orcamento.Produto_Servico = new Produto_Servico()
                    {
                        codigo = Convert.ToInt32(linha["cod_Produto_Servico"]),
                        ultima_atualizacao = Convert.ToDateTime(linha["ultima_atualizacao_Produto_Servico"]),
                        imagem = linha["imagem_Produto_Servico"].ToString(),
                        descricao = linha["descricao_Produto_Servico"].ToString(),
                        altura = Convert.ToDecimal(linha["altura_Produto_Servico"]),
                        largura = Convert.ToDecimal(linha["largura_Produto_Servico"]),
                        comprimento = Convert.ToDecimal(linha["comprimento_Produto_Servico"]),
                        valor_total_materiais = Convert.ToDecimal(linha["valor_total_materiais_Produto_Servico"]),
                        maoObra = Convert.ToDecimal(linha["maoObra_Produto_Servico"]),
                        valor_maoObra = Convert.ToDecimal(linha["valor_maoObra_Produto_Servico"]),
                        valor_total = Convert.ToDecimal(linha["valor_unitario_Produto_Servico"]),
                        ativo_inativo = Convert.ToBoolean(linha["ativo_inativo_Produto_Servico"]),

                        Unidade = new Grupo_Unidade()
                        {
                            codigo = Convert.ToInt32(linha["cod_Unidade"]),
                            sigla = linha["sigla_Unidade"].ToString(),
                            descricao = linha["descricao_Unidade"].ToString(),
                            ativo_inativo = Convert.ToBoolean(linha["ativo_inativo_Unidade"])
                        },

                        Grupo = new Grupo_Unidade()
                        {
                            codigo = Convert.ToInt32(linha["cod_Grupo"]),
                            descricao = linha["descricao_Grupo"].ToString(),
                            material_ou_produto = Convert.ToChar(linha["material_ou_produto_Grupo"]),
                            ativo_inativo = Convert.ToBoolean(linha["ativo_inativo_Grupo"]),
                        }
                    };

                    produtos_servicos_orcamento.Orcamento = new Orcamento()
                    {
                        codigo = Convert.ToInt32(linha["cod_Orcamento"]),
                        ultima_atualizacao = Convert.ToDateTime(linha["ultima_atualizacao"]),
                        validade = Convert.ToDateTime(linha["validade"]),
                        prazo_entrega = linha["prazo_entrega"].ToString(),
                        observacao = linha["observacao"].ToString(),
                        total_produtos_servicos = Convert.ToDecimal(linha["total_produtos_servicos"]),
                        desconto = Convert.ToDecimal(linha["desconto"]),
                        total_orcamento = Convert.ToDecimal(linha["total_orcamento"]),
                        descricao_pagamento = linha["descricao_pagamento"].ToString(),
                        valor_entrada = Convert.ToDecimal(linha["valor_entrada"]),
                        quantidade_parcelas = Convert.ToInt32(linha["quantidade_parcelas"]),
                        valor_parcela = Convert.ToDecimal(linha["valor_parcela"]),
                        juros = Convert.ToDecimal(linha["juros"]),
                        valor_juros = Convert.ToDecimal(linha["valor_juros"]),
                        ativo_inativo = Convert.ToBoolean(linha["ativo_inativo_Orcamento"]),

                        Usuario = new Pessoa()
                        {
                            codigo = Convert.ToInt32(linha["cod_Usuario"]),
                            tipo_pessoa = linha["tipo_pessoa_Usuario"].ToString(),
                            nome_razao_social = linha["nome_razao_social_Usuario"].ToString(),
                            nome_fantasia = linha["nome_fantasia_Usuario"].ToString(),
                            cpf_cnpj = linha["cpf_cnpj_Usuario"].ToString(),
                            inscricao_estadual = linha["inscricao_estadual_Usuario"].ToString(),
                            email = linha["email_Usuario"].ToString(),
                            observacao = linha["observacao_Usuario"].ToString(),
                            ativo_inativo = Convert.ToBoolean(linha["ativo_inativo_Usuario"])
                        },

                        Cliente = new Pessoa()
                        {
                            codigo = Convert.ToInt32(linha["cod_Cliente"]),
                            tipo_pessoa = linha["tipo_pessoa_Cliente"].ToString(),
                            nome_razao_social = linha["nome_razao_social_Cliente"].ToString(),
                            nome_fantasia = linha["nome_fantasia_Cliente"].ToString(),
                            cpf_cnpj = linha["cpf_cnpj_Cliente"].ToString(),
                            inscricao_estadual = linha["inscricao_estadual_Cliente"].ToString(),
                            email = linha["email_Cliente"].ToString(),
                            observacao = linha["observacao_Cliente"].ToString(),
                            ativo_inativo = Convert.ToBoolean(linha["ativo_inativo_Cliente"])
                        }
                    };

                    lista.Add(produtos_servicos_orcamento);
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
