using CamadaAcessoDados;
using CamadaObjetoTransferencia;
using System;
using System.Data;

namespace CamadaProcessamento
{
    public class ProcMaterial
    {
        private AcessoDadosMySqlServer acessoDados = new AcessoDadosMySqlServer();

        public string ManterRegistro(Material material, string operacao)
        {
            try
            {
                acessoDados.LimparParametros();

                acessoDados.AdicionarParametro("@var_operacao", operacao);
                acessoDados.AdicionarParametro("@var_codigo", material.codigo);
                acessoDados.AdicionarParametro("@var_imagem", material.imagem);
                acessoDados.AdicionarParametro("@var_descricao", material.descricao);
                acessoDados.AdicionarParametro("@var_altura", material.altura);
                acessoDados.AdicionarParametro("@var_largura", material.largura);
                acessoDados.AdicionarParametro("@var_comprimento", material.comprimento);
                acessoDados.AdicionarParametro("@var_valor_unitario", material.valor_unitario);
                acessoDados.AdicionarParametro("@var_ativo_inativo", material.ativo_inativo);
                acessoDados.AdicionarParametro("@var_cod_Unidade", material.Unidade.codigo);
                acessoDados.AdicionarParametro("@var_cod_Grupo", material.Grupo.codigo);
                acessoDados.AdicionarParametro("@var_cod_Fornecedor", material.Fornecedor.codigo);

                return acessoDados.ExecutarScalar("sp_ManterMaterial",
                    CommandType.StoredProcedure).ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ListaMaterial ConsultarRegistro(Material material, bool pesquisarTodos)
        {
            try
            {
                acessoDados.LimparParametros();

                acessoDados.AdicionarParametro("@var_pesquisarTodos", pesquisarTodos);
                acessoDados.AdicionarParametro("@var_codigo", material.codigo);
                acessoDados.AdicionarParametro("@var_ultima_atualizacao", material.ultima_atualizacao);
                acessoDados.AdicionarParametro("@var_imagem", material.imagem);
                acessoDados.AdicionarParametro("@var_descricao", material.descricao);
                acessoDados.AdicionarParametro("@var_altura", material.altura);
                acessoDados.AdicionarParametro("@var_largura", material.largura);
                acessoDados.AdicionarParametro("@var_comprimento", material.comprimento);
                acessoDados.AdicionarParametro("@var_valor_unitario", material.valor_unitario);
                acessoDados.AdicionarParametro("@var_ativo_inativo", material.ativo_inativo);
                acessoDados.AdicionarParametro("@var_cod_Unidade", material.Unidade.codigo);
                acessoDados.AdicionarParametro("@var_cod_Grupo", material.Grupo.codigo);
                acessoDados.AdicionarParametro("@var_cod_Fornecedor", material.Fornecedor.codigo);

                DataTable tabela = acessoDados.ObterDataTable("sp_ConsultarMaterial",
                    CommandType.StoredProcedure);

                ListaMaterial lista = new ListaMaterial();
                foreach (DataRow linha in tabela.Rows)
                {
                    material = new Material();

                    material.codigo = Convert.ToInt32(linha["codigo"]);
                    material.ultima_atualizacao = Convert.ToDateTime(linha["ultima_atualizacao"]);
                    material.imagem = linha["imagem"].ToString();
                    material.descricao = linha["descricao_Material"].ToString();
                    material.altura = Convert.ToDecimal(linha["altura"]);
                    material.largura = Convert.ToDecimal(linha["largura"]);
                    material.comprimento = Convert.ToDecimal(linha["comprimento"]);
                    material.valor_unitario = Convert.ToDecimal(linha["valor_unitario"]);
                    material.ativo_inativo = Convert.ToBoolean(linha["ativo_inativo_Material"]);

                    material.Unidade = new Grupo_Unidade()
                    {
                        codigo = Convert.ToInt32(linha["cod_Unidade"]),
                        sigla = linha["sigla"].ToString(),
                        descricao = linha["descricao_Unidade"].ToString(),
                        ativo_inativo = Convert.ToBoolean(linha["ativo_inativo_Unidade"])
                    };

                    material.Grupo = new Grupo_Unidade()
                    {
                        codigo = Convert.ToInt32(linha["cod_Grupo"]),
                        descricao = linha["descricao_Grupo"].ToString(),
                        material_ou_produto = Convert.ToChar(linha["material_ou_produto"]),
                        ativo_inativo = Convert.ToBoolean(linha["ativo_inativo_Grupo"])
                    };

                    material.Fornecedor = new Pessoa()
                    {
                        codigo = Convert.ToInt32(linha["cod_Fornecedor"]),
                        tipo_pessoa = linha["tipo_pessoa"].ToString(),
                        nome_razao_social = linha["nome_razao_social"].ToString(),
                        nome_fantasia = linha["nome_fantasia"].ToString(),
                        cpf_cnpj = linha["cpf_cnpj"].ToString(),
                        inscricao_estadual = linha["inscricao_estadual"].ToString(),
                        email = linha["email"].ToString(),
                        observacao = linha["observacao"].ToString(),
                        ativo_inativo = Convert.ToBoolean(linha["ativo_inativo_Pessoa"])
                    };

                    lista.Add(material);
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
