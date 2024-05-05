using CamadaAcessoDados;
using CamadaObjetoTransferencia;
using System;
using System.Data;

namespace CamadaProcessamento
{
    public class ProcMateriais_Produto_Servico
    {
        private AcessoDadosMySqlServer acessoDados = new AcessoDadosMySqlServer();

        public string ManterRegistro(Materiais_Produto_Servico materiais_produto_servico, string operacao)
        {
            try
            {
                acessoDados.LimparParametros();

                acessoDados.AdicionarParametro("@var_operacao", operacao);
                acessoDados.AdicionarParametro("@var_codigo", materiais_produto_servico.codigo);
                acessoDados.AdicionarParametro("@var_quantidade", materiais_produto_servico.quantidade);
                acessoDados.AdicionarParametro("@var_valor_total", materiais_produto_servico.valor_total);
                acessoDados.AdicionarParametro("@var_ativo_inativo", materiais_produto_servico.ativo_inativo);
                acessoDados.AdicionarParametro("@var_cod_Material", materiais_produto_servico.Material.codigo);
                acessoDados.AdicionarParametro("@var_cod_Produto_Servico", materiais_produto_servico.Produto_Servico.codigo);

                return acessoDados.ExecutarScalar("sp_ManterMateriais_Produto_Servico",
                    CommandType.StoredProcedure).ToString();
            }
            catch (Exception)
            {
                throw;
            }       
        }

        public ListaMateriais_Produto_Servico ConsultarRegistro(Materiais_Produto_Servico materiais_produto_servico, bool pesquisarTodos)
        {
            try
            {
                acessoDados.LimparParametros();

                acessoDados.AdicionarParametro("@var_pesquisarTodos", pesquisarTodos);
                acessoDados.AdicionarParametro("@var_codigo", materiais_produto_servico.codigo);
                acessoDados.AdicionarParametro("@var_quantidade", materiais_produto_servico.quantidade);
                acessoDados.AdicionarParametro("@var_valor_total", materiais_produto_servico.valor_total);
                acessoDados.AdicionarParametro("@var_ativo_inativo", materiais_produto_servico.ativo_inativo);
                acessoDados.AdicionarParametro("@var_cod_Material", materiais_produto_servico.Material.codigo);
                acessoDados.AdicionarParametro("@var_cod_Produto_Servico", materiais_produto_servico.Produto_Servico.codigo);

                DataTable tabela = acessoDados.ObterDataTable("sp_ConsultarMateriais_Produto_Servico",
                    CommandType.StoredProcedure);

                ListaMateriais_Produto_Servico lista = new ListaMateriais_Produto_Servico();
                foreach (DataRow linha in tabela.Rows)
                {
                    materiais_produto_servico = new Materiais_Produto_Servico()
                    {
                        codigo = Convert.ToInt32(linha["codigo"]),
                        quantidade = Convert.ToDecimal(linha["quantidade"]),
                        valor_total = Convert.ToDecimal(linha["valor_total"]),
                        ativo_inativo = Convert.ToBoolean(linha["ativo_inativo_Materiais_Produto_Servico"]),

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
                                codigo = Convert.ToInt32(linha["cod_Unidade_Material"]),
                                sigla = linha["sigla_Unidade_Material"].ToString(),
                                descricao = linha["descricao_Unidade_Material"].ToString(),
                                ativo_inativo = Convert.ToBoolean(linha["ativo_inativo_Unidade_Material"])
                            },

                            Grupo = new Grupo_Unidade()
                            {
                                codigo = Convert.ToInt32(linha["cod_Grupo_Material"]),
                                descricao = linha["descricao_Grupo_Material"].ToString(),
                                material_ou_produto = Convert.ToChar(linha["material_ou_produto_Grupo_Material"]),
                                ativo_inativo = Convert.ToBoolean(linha["ativo_inativo_Grupo_Material"])
                            },

                            Fornecedor = new Pessoa()
                            {
                                codigo = Convert.ToInt32(linha["cod_Fornecedor_Material"]),
                                tipo_pessoa = linha["tipo_pessoa"].ToString(),
                                nome_razao_social = linha["nome_razao_social"].ToString(),
                                nome_fantasia = linha["nome_fantasia"].ToString(),
                                cpf_cnpj = linha["cpf_cnpj"].ToString(),
                                inscricao_estadual = linha["inscricao_estadual"].ToString(),
                                email = linha["email"].ToString(),
                                observacao = linha["observacao"].ToString(),
                                ativo_inativo = Convert.ToBoolean(linha["ativo_inativo_Fornecedor_Material"])
                            }
                        },

                        Produto_Servico = new Produto_Servico()
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
                            valor_total = Convert.ToDecimal(linha["valor_total_Produto_Servico"]),
                            ativo_inativo = Convert.ToBoolean(linha["ativo_inativo_Produto_Servico"]),

                            Unidade = new Grupo_Unidade()
                            {
                                codigo = Convert.ToInt32(linha["cod_Unidade_Produto_Servico"]),
                                sigla = linha["sigla_Unidade_Produto_Servico"].ToString(),
                                descricao = linha["descricao_Unidade_Produto_Servico"].ToString(),
                                ativo_inativo = Convert.ToBoolean(linha["ativo_inativo_Unidade_Produto_Servico"])
                            },

                            Grupo = new Grupo_Unidade()
                            {
                                codigo = Convert.ToInt32(linha["cod_Grupo_Produto_Servico"]),
                                descricao = linha["descricao_Grupo_Produto_Servico"].ToString(),
                                material_ou_produto = Convert.ToChar(linha["material_ou_produto_Grupo_Produto_Servico"]),
                                ativo_inativo = Convert.ToBoolean(linha["ativo_inativo_Grupo_Produto_Servico"])
                            }
                        }
                    };

                    lista.Add(materiais_produto_servico);
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
