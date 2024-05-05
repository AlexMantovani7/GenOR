using CamadaAcessoDados;
using CamadaObjetoTransferencia;
using System;
using System.Data;

namespace CamadaProcessamento
{
    public class ProcOrcamento
    {
        private AcessoDadosMySqlServer acessoDados = new AcessoDadosMySqlServer();

        public string ManterRegistro(Orcamento orcamento, string operacao)
        {
            try
            {
                acessoDados.LimparParametros();

                acessoDados.AdicionarParametro("@var_operacao", operacao);
                acessoDados.AdicionarParametro("@var_codigo", orcamento.codigo);
                acessoDados.AdicionarParametro("@var_validade", orcamento.validade);
                acessoDados.AdicionarParametro("@var_prazo_entrega", orcamento.prazo_entrega);
                acessoDados.AdicionarParametro("@var_observacao", orcamento.observacao);
                acessoDados.AdicionarParametro("@var_total_produtos_servicos", orcamento.total_produtos_servicos);
                acessoDados.AdicionarParametro("@var_desconto", orcamento.desconto);
                acessoDados.AdicionarParametro("@var_descricao_pagamento", orcamento.descricao_pagamento);
                acessoDados.AdicionarParametro("@var_valor_entrada", orcamento.valor_entrada);
                acessoDados.AdicionarParametro("@var_quantidade_parcelas", orcamento.quantidade_parcelas);
                acessoDados.AdicionarParametro("@var_juros", orcamento.juros);
                acessoDados.AdicionarParametro("@var_ativo_inativo", orcamento.ativo_inativo);
                acessoDados.AdicionarParametro("@var_cod_Usuario", orcamento.Usuario.codigo);
                acessoDados.AdicionarParametro("@var_cod_Cliente", orcamento.Cliente.codigo);

                return acessoDados.ExecutarScalar("sp_ManterOrcamento",
                    CommandType.StoredProcedure).ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ListaOrcamento ConsultarRegistro(Orcamento orcamento, bool pesquisarTodos)
        {
            try
            {
                acessoDados.LimparParametros();

                acessoDados.AdicionarParametro("@var_pesquisarTodos", pesquisarTodos);
                acessoDados.AdicionarParametro("@var_codigo", orcamento.codigo);
                acessoDados.AdicionarParametro("@var_ultima_atualizacao", orcamento.ultima_atualizacao);
                acessoDados.AdicionarParametro("@var_validade", orcamento.validade);
                acessoDados.AdicionarParametro("@var_prazo_entrega", orcamento.prazo_entrega);
                acessoDados.AdicionarParametro("@var_observacao", orcamento.observacao);
                acessoDados.AdicionarParametro("@var_total_produtos_servicos", orcamento.total_produtos_servicos);
                acessoDados.AdicionarParametro("@var_desconto", orcamento.desconto);
                acessoDados.AdicionarParametro("@var_total_orcamento", orcamento.total_orcamento);
                acessoDados.AdicionarParametro("@var_descricao_pagamento", orcamento.descricao_pagamento);
                acessoDados.AdicionarParametro("@var_valor_entrada", orcamento.valor_entrada);
                acessoDados.AdicionarParametro("@var_quantidade_parcelas", orcamento.quantidade_parcelas);
                acessoDados.AdicionarParametro("@var_valor_parcela", orcamento.valor_parcela);
                acessoDados.AdicionarParametro("@var_juros", orcamento.juros);
                acessoDados.AdicionarParametro("@var_valor_juros", orcamento.valor_juros);
                acessoDados.AdicionarParametro("@var_ativo_inativo", orcamento.ativo_inativo);
                acessoDados.AdicionarParametro("@var_cod_Usuario", orcamento.Usuario.codigo);
                acessoDados.AdicionarParametro("@var_cod_Cliente", orcamento.Cliente.codigo);

                DataTable tabela = acessoDados.ObterDataTable("sp_ConsultarOrcamento",
                    CommandType.StoredProcedure);

                ListaOrcamento lista = new ListaOrcamento();
                foreach (DataRow linha in tabela.Rows)
                {
                    orcamento = new Orcamento();

                    orcamento.codigo = Convert.ToInt32(linha["codigo"]);
                    orcamento.ultima_atualizacao = Convert.ToDateTime(linha["ultima_atualizacao"]);
                    orcamento.validade = Convert.ToDateTime(linha["validade"]);
                    orcamento.prazo_entrega = linha["prazo_entrega"].ToString();
                    orcamento.observacao = linha["observacao_Orcamento"].ToString();
                    orcamento.total_produtos_servicos = Convert.ToDecimal(linha["total_produtos_servicos"]);
                    orcamento.desconto = Convert.ToDecimal(linha["desconto"]);
                    orcamento.total_orcamento = Convert.ToDecimal(linha["total_orcamento"]);
                    orcamento.descricao_pagamento = linha["descricao_pagamento"].ToString();
                    orcamento.valor_entrada = Convert.ToDecimal(linha["valor_entrada"]);
                    orcamento.quantidade_parcelas = Convert.ToInt32(linha["quantidade_parcelas"]);
                    orcamento.valor_parcela = Convert.ToDecimal(linha["valor_parcela"]);
                    orcamento.juros = Convert.ToDecimal(linha["juros"]);
                    orcamento.valor_juros = Convert.ToDecimal(linha["valor_juros"]);
                    orcamento.ativo_inativo = Convert.ToBoolean(linha["ativo_inativo_Orcamento"]);

                    orcamento.Usuario = new Pessoa()
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
                    };

                    orcamento.Cliente = new Pessoa()
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
                    };

                    lista.Add(orcamento);
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
