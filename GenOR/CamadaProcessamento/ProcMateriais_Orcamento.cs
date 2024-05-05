using CamadaAcessoDados;
using CamadaObjetoTransferencia;
using System;
using System.Data;

namespace CamadaProcessamento
{
    public class ProcMateriais_Orcamento
    {
        private AcessoDadosMySqlServer acessoDados = new AcessoDadosMySqlServer();

        public string ManterRegistro(Materiais_Orcamento materiais_orcamento, string operacao)
        {
            try
            {
                acessoDados.LimparParametros();

                acessoDados.AdicionarParametro("@var_operacao", operacao);
                acessoDados.AdicionarParametro("@var_codigo", materiais_orcamento.codigo);
                acessoDados.AdicionarParametro("@var_quantidade_total", materiais_orcamento.quantidade_total);
                acessoDados.AdicionarParametro("@var_valor_total", materiais_orcamento.valor_total);
                acessoDados.AdicionarParametro("@var_cod_Produtos_Servicos_Orcamento", materiais_orcamento.Produtos_Servicos_Orcamento.codigo);
                acessoDados.AdicionarParametro("@var_cod_Materiais_Produto_Servico", materiais_orcamento.Materiais_Produto_Servico.codigo);
                acessoDados.AdicionarParametro("@var_quantidade_Materiais_Produto_Servico", materiais_orcamento.Materiais_Produto_Servico.quantidade);
                acessoDados.AdicionarParametro("@var_valor_unitario_Materiais_Produto_Servico", materiais_orcamento.Materiais_Produto_Servico.valor_total);
                acessoDados.AdicionarParametro("@var_cod_Material", materiais_orcamento.Materiais_Produto_Servico.Material.codigo);
                acessoDados.AdicionarParametro("@var_ultima_atualizacao_Material", materiais_orcamento.Materiais_Produto_Servico.Material.ultima_atualizacao);
                acessoDados.AdicionarParametro("@var_imagem_Material", materiais_orcamento.Materiais_Produto_Servico.Material.imagem);
                acessoDados.AdicionarParametro("@var_descricao_Material", materiais_orcamento.Materiais_Produto_Servico.Material.descricao);
                acessoDados.AdicionarParametro("@var_altura_Material", materiais_orcamento.Materiais_Produto_Servico.Material.altura);
                acessoDados.AdicionarParametro("@var_largura_Material", materiais_orcamento.Materiais_Produto_Servico.Material.largura);
                acessoDados.AdicionarParametro("@var_comprimento_Material", materiais_orcamento.Materiais_Produto_Servico.Material.comprimento);
                acessoDados.AdicionarParametro("@var_valor_unitario_Material", materiais_orcamento.Materiais_Produto_Servico.Material.valor_unitario);
                acessoDados.AdicionarParametro("@var_ativo_inativo_Material", materiais_orcamento.Materiais_Produto_Servico.Material.ativo_inativo);
                acessoDados.AdicionarParametro("@var_cod_Unidade", materiais_orcamento.Materiais_Produto_Servico.Material.Unidade.codigo);
                acessoDados.AdicionarParametro("@var_sigla_Unidade", materiais_orcamento.Materiais_Produto_Servico.Material.Unidade.sigla);
                acessoDados.AdicionarParametro("@var_descricao_Unidade", materiais_orcamento.Materiais_Produto_Servico.Material.Unidade.descricao);
                acessoDados.AdicionarParametro("@var_ativo_inativo_Unidade", materiais_orcamento.Materiais_Produto_Servico.Material.Unidade.ativo_inativo);
                acessoDados.AdicionarParametro("@var_cod_Grupo", materiais_orcamento.Materiais_Produto_Servico.Material.Grupo.codigo);
                acessoDados.AdicionarParametro("@var_descricao_Grupo", materiais_orcamento.Materiais_Produto_Servico.Material.Grupo.descricao);
                acessoDados.AdicionarParametro("@var_ativo_inativo_Grupo", materiais_orcamento.Materiais_Produto_Servico.Material.Grupo.ativo_inativo);
                acessoDados.AdicionarParametro("@var_cod_Fornecedor", materiais_orcamento.Materiais_Produto_Servico.Material.Fornecedor.codigo);
                acessoDados.AdicionarParametro("@var_nome_razao_social_Fornecedor", materiais_orcamento.Materiais_Produto_Servico.Material.Fornecedor.nome_razao_social);
                acessoDados.AdicionarParametro("@var_nome_fantasia_Fornecedor", materiais_orcamento.Materiais_Produto_Servico.Material.Fornecedor.nome_fantasia);
                acessoDados.AdicionarParametro("@var_ativo_inativo_Fornecedor", materiais_orcamento.Materiais_Produto_Servico.Material.Fornecedor.ativo_inativo);
                acessoDados.AdicionarParametro("@var_cod_Orcamento", materiais_orcamento.Produtos_Servicos_Orcamento.Orcamento.codigo);

                return acessoDados.ExecutarScalar("sp_ManterMateriais_Orcamento",
                    CommandType.StoredProcedure).ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ListaMateriais_Orcamento ConsultarRegistro(Materiais_Orcamento materiais_orcamento, bool pesquisarTodos)
        {
            try
            {
                acessoDados.LimparParametros();

                acessoDados.AdicionarParametro("@var_pesquisarTodos", pesquisarTodos);
                acessoDados.AdicionarParametro("@var_codigo", materiais_orcamento.codigo);
                acessoDados.AdicionarParametro("@var_quantidade_total", materiais_orcamento.quantidade_total);
                acessoDados.AdicionarParametro("@var_valor_total", materiais_orcamento.valor_total);
                acessoDados.AdicionarParametro("@var_cod_Produtos_Servicos_Orcamento", materiais_orcamento.Produtos_Servicos_Orcamento.codigo);
                acessoDados.AdicionarParametro("@var_cod_Materiais_Produto_Servico", materiais_orcamento.Materiais_Produto_Servico.codigo);
                acessoDados.AdicionarParametro("@var_quantidade_Materiais_Produto_Servico", materiais_orcamento.Materiais_Produto_Servico.quantidade);
                acessoDados.AdicionarParametro("@var_valor_unitario_Materiais_Produto_Servico", materiais_orcamento.Materiais_Produto_Servico.valor_total);
                acessoDados.AdicionarParametro("@var_cod_Material", materiais_orcamento.Materiais_Produto_Servico.Material.codigo);
                acessoDados.AdicionarParametro("@var_ultima_atualizacao_Material", materiais_orcamento.Materiais_Produto_Servico.Material.ultima_atualizacao);
                acessoDados.AdicionarParametro("@var_imagem_Material", materiais_orcamento.Materiais_Produto_Servico.Material.imagem);
                acessoDados.AdicionarParametro("@var_descricao_Material", materiais_orcamento.Materiais_Produto_Servico.Material.descricao);
                acessoDados.AdicionarParametro("@var_altura_Material", materiais_orcamento.Materiais_Produto_Servico.Material.altura);
                acessoDados.AdicionarParametro("@var_largura_Material", materiais_orcamento.Materiais_Produto_Servico.Material.largura);
                acessoDados.AdicionarParametro("@var_comprimento_Material", materiais_orcamento.Materiais_Produto_Servico.Material.comprimento);
                acessoDados.AdicionarParametro("@var_valor_unitario_Material", materiais_orcamento.Materiais_Produto_Servico.Material.valor_unitario);
                acessoDados.AdicionarParametro("@var_ativo_inativo_Material", materiais_orcamento.Materiais_Produto_Servico.Material.ativo_inativo);
                acessoDados.AdicionarParametro("@var_cod_Unidade", materiais_orcamento.Materiais_Produto_Servico.Material.Unidade.codigo);
                acessoDados.AdicionarParametro("@var_sigla_Unidade", materiais_orcamento.Materiais_Produto_Servico.Material.Unidade.sigla);
                acessoDados.AdicionarParametro("@var_descricao_Unidade", materiais_orcamento.Materiais_Produto_Servico.Material.Unidade.descricao);
                acessoDados.AdicionarParametro("@var_ativo_inativo_Unidade", materiais_orcamento.Materiais_Produto_Servico.Material.Unidade.ativo_inativo);
                acessoDados.AdicionarParametro("@var_cod_Grupo", materiais_orcamento.Materiais_Produto_Servico.Material.Grupo.codigo);
                acessoDados.AdicionarParametro("@var_descricao_Grupo", materiais_orcamento.Materiais_Produto_Servico.Material.Grupo.descricao);
                acessoDados.AdicionarParametro("@var_ativo_inativo_Grupo", materiais_orcamento.Materiais_Produto_Servico.Material.Grupo.ativo_inativo);
                acessoDados.AdicionarParametro("@var_cod_Fornecedor", materiais_orcamento.Materiais_Produto_Servico.Material.Fornecedor.codigo);
                acessoDados.AdicionarParametro("@var_nome_razao_social_Fornecedor", materiais_orcamento.Materiais_Produto_Servico.Material.Fornecedor.nome_razao_social);
                acessoDados.AdicionarParametro("@var_nome_fantasia_Fornecedor", materiais_orcamento.Materiais_Produto_Servico.Material.Fornecedor.nome_fantasia);
                acessoDados.AdicionarParametro("@var_ativo_inativo_Fornecedor", materiais_orcamento.Materiais_Produto_Servico.Material.Fornecedor.ativo_inativo);
                acessoDados.AdicionarParametro("@var_cod_Orcamento", materiais_orcamento.Produtos_Servicos_Orcamento.Orcamento.codigo);

                DataTable tabela = acessoDados.ObterDataTable("sp_ConsultarMateriais_Orcamento",
                    CommandType.StoredProcedure);

                ListaMateriais_Orcamento lista = new ListaMateriais_Orcamento();
                foreach (DataRow linha in tabela.Rows)
                {
                    materiais_orcamento = new Materiais_Orcamento()
                    {
                        codigo = Convert.ToInt32(linha["codigo"]),
                        quantidade_total = Convert.ToDecimal(linha["quantidade_total"]),
                        valor_total = Convert.ToDecimal(linha["valor_total"]),

                        Produtos_Servicos_Orcamento = new Produtos_Servicos_Orcamento()
                        {
                            codigo = Convert.ToInt32(linha["cod_Produtos_Servicos_Orcamento"]),

                            Orcamento = new Orcamento()
                            {
                                codigo = Convert.ToInt32(linha["cod_Orcamento"])
                            }
                        },

                        Materiais_Produto_Servico = new Materiais_Produto_Servico()
                        {
                            codigo = Convert.ToInt32(linha["cod_Materiais_Produto_Servico"]),
                            quantidade = Convert.ToDecimal(linha["quantidade_Materiais_Produto_Servico"]),
                            valor_total = Convert.ToDecimal(linha["valor_unitario_Materiais_Produto_Servico"]),

                            Material = new Material()
                            {
                                codigo = Convert.ToInt32(linha["cod_Material"]),
                                ultima_atualizacao = Convert.ToDateTime(linha["ultima_atualizacao_Material"]),
                                imagem = linha["imagem_Material"].ToString(),
                                descricao = linha["descricao_Material"].ToString(),
                                altura = Convert.ToDecimal(linha["altura_Material"]),
                                largura = Convert.ToDecimal(linha["largura_Material"]),
                                comprimento = Convert.ToDecimal(linha["comprimento_Material"]),
                                valor_unitario = Convert.ToDecimal(linha["valor_unitario_Material"]),
                                ativo_inativo = Convert.ToBoolean(linha["ativo_inativo_Material"]),

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
                                    ativo_inativo = Convert.ToBoolean(linha["ativo_inativo_Grupo"])
                                },

                                Fornecedor = new Pessoa()
                                {
                                    codigo = Convert.ToInt32(linha["cod_Fornecedor"]),
                                    nome_razao_social = linha["nome_razao_social_Fornecedor"].ToString(),
                                    nome_fantasia = linha["nome_fantasia_Fornecedor"].ToString(),
                                    ativo_inativo = Convert.ToBoolean(linha["ativo_inativo_Fornecedor"])
                                }
                            }
                        }
                    };

                    lista.Add(materiais_orcamento);
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
