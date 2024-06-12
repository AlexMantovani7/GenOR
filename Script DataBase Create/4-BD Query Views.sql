-- --------------------------------CRIANDO VIEWs----------------------------------
USE GenOR_BD;

-- LOG -------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE VIEW vw_LOG AS
SELECT
	l.codigo,
    l.data_registro,
    l.operacao,
    l.registro,
    l.informacoes_registro
FROM tbl_LOG AS l;

-- PESSOA -------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE VIEW vw_Pessoa AS
SELECT
	p.codigo,
    p.tipo_pessoa,
    p.nome_razao_social,
    p.nome_fantasia,
    p.cpf_cnpj,
    p.inscricao_estadual,
    p.email,
    p.observacao,
    p.ativo_inativo
FROM tbl_Pessoa AS p;

-- LOGIN -------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE VIEW vw_Login AS
SELECT
	lg.codigo,
    lg.nome_usuario,
    lg.senha,
    lg.cod_Usuario,
		usu.tipo_pessoa,
		usu.nome_razao_social,
		usu.nome_fantasia,
		usu.cpf_cnpj,
		usu.inscricao_estadual,
		usu.email,
		usu.observacao,
		usu.ativo_inativo
FROM tbl_Login AS lg
LEFT JOIN tbl_Pessoa AS usu ON lg.cod_Usuario = usu.codigo;

-- ENDERECO -------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE VIEW vw_Endereco AS
SELECT
	e.codigo,
    e.endereco,
    e.complemento,
    e.numero,
	e.bairro,
    e.cidade,
	e.estado,
	e.cep,
    e.observacao,
	e.ativo_inativo AS ativo_inativo_Endereco,
    e.cod_Pessoa,
		p.tipo_pessoa,
		p.nome_razao_social,
		p.nome_fantasia,
		p.cpf_cnpj,
		p.inscricao_estadual,
		p.email,
		p.observacao AS observacao_Pessoa,
		p.ativo_inativo AS ativo_inativo_Pessoa
FROM tbl_Endereco AS e
LEFT JOIN tbl_Pessoa AS p ON e.cod_Pessoa = p.codigo;

-- TELEFONE -------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE VIEW vw_Telefone AS
SELECT
	t.codigo,
    t.ddd,
    t.numero,
    t.observacao,
	t.ativo_inativo AS ativo_inativo_Telefone,
    t.cod_Pessoa,
		p.tipo_pessoa,
		p.nome_razao_social,
		p.nome_fantasia,
		p.cpf_cnpj,
		p.inscricao_estadual,
		p.email,
		p.observacao AS observacao_Pessoa,
		p.ativo_inativo AS ativo_inativo_Pessoa
FROM tbl_Telefone AS t
LEFT JOIN tbl_Pessoa AS p ON t.cod_Pessoa = p.codigo;

-- GRUPO -------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE VIEW vw_Grupo AS
SELECT
	g.codigo,
    g.descricao,
    g.material_ou_produto,
    g.ativo_inativo
FROM tbl_Grupo AS g;

-- UNIDADE -------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE VIEW vw_Unidade AS
SELECT
	u.codigo,
    u.sigla,
    u.descricao,
    u.ativo_inativo
FROM tbl_Unidade AS u;

-- MATERIAL -------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE VIEW vw_Material AS
SELECT
	m.codigo,
    m.ultima_atualizacao,
    m.imagem,
    m.descricao AS descricao_Material,
    m.altura,
    m.largura,
    m.comprimento,
    m.valor_unitario,
    m.ativo_inativo AS ativo_inativo_Material,
    m.cod_Unidade,
		u.sigla,
		u.descricao AS descricao_Unidade,
		u.ativo_inativo AS ativo_inativo_Unidade,
    m.cod_Grupo,
		g.descricao AS descricao_Grupo,
		g.material_ou_produto,
		g.ativo_inativo AS ativo_inativo_Grupo,
    m.cod_Fornecedor,
		f.tipo_pessoa,
		f.nome_razao_social,
		f.nome_fantasia,
		f.cpf_cnpj,
		f.inscricao_estadual,
		f.email,
		f.observacao,
		f.ativo_inativo AS ativo_inativo_Pessoa
FROM tbl_Material AS m
LEFT JOIN tbl_Unidade AS u ON m.cod_Unidade = u.codigo
LEFT JOIN tbl_Grupo AS g ON m.cod_Grupo = g.codigo
LEFT JOIN tbl_Pessoa AS f ON m.cod_Fornecedor = f.codigo;

-- PRODUTO OU SERVIÇO -------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE VIEW vw_Produto_Servico AS
SELECT
	ps.codigo,
	ps.ultima_atualizacao,
	ps.imagem,
	ps.descricao AS descricao_Produto_Servico,
	ps.altura,
	ps.largura,
	ps.comprimento,
    ps.valor_total_materiais,
    ps.maoObra,
    ps.valor_maoObra,
	ps.valor_total,
	ps.ativo_inativo AS ativo_inativo_Produto_Servico,
    ps.cod_Unidade,
		u.sigla,
		u.descricao AS descricao_Unidade,
		u.ativo_inativo AS ativo_inativo_Unidade,
    ps.cod_Grupo,
		g.descricao AS descricao_Grupo,
		g.material_ou_produto,
		g.ativo_inativo AS ativo_inativo_Grupo
FROM tbl_Produto_Servico AS ps
LEFT JOIN tbl_Unidade AS u ON ps.cod_Unidade = u.codigo
LEFT JOIN tbl_Grupo AS g ON ps.cod_Grupo = g.codigo;

-- MATERIAIS DO PRODUTO OU SERVIÇO  -------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE VIEW vw_Materiais_Produto_Servico AS
SELECT
	mps.codigo,
	mps.quantidade,
	mps.valor_total,
	mps.ativo_inativo AS ativo_inativo_Materiais_Produto_Servico,
    mps.cod_Material,
		m.ultima_atualizacao AS ultima_atualizacao_Material,
		m.imagem AS imagem_Material,
		m.descricao AS descricao_Material,
		m.altura AS altura_Material,
		m.largura AS largura_Material,
		m.comprimento AS comprimento_Material,
		m.valor_unitario AS valor_unitario_Material,
		m.ativo_inativo AS ativo_inativo_Material,
		m.cod_Unidade AS cod_Unidade_Material,
			um.sigla AS sigla_Unidade_Material,
			um.descricao AS descricao_Unidade_Material,
			um.ativo_inativo AS ativo_inativo_Unidade_Material,
		m.cod_Grupo AS cod_Grupo_Material,
			gm.descricao AS descricao_Grupo_Material,
			gm.material_ou_produto AS material_ou_produto_Grupo_Material,
			gm.ativo_inativo AS ativo_inativo_Grupo_Material,
		m.cod_Fornecedor AS cod_Fornecedor_Material,
			fm.tipo_pessoa,
			fm.nome_razao_social,
			fm.nome_fantasia,
			fm.cpf_cnpj,
			fm.inscricao_estadual,
			fm.email,
			fm.observacao,
			fm.ativo_inativo AS ativo_inativo_Fornecedor_Material,
	mps.cod_Produto_Servico,
		ps.ultima_atualizacao AS ultima_atualizacao_Produto_Servico,
		ps.imagem AS imagem_Produto_Servico,
		ps.descricao AS descricao_Produto_Servico,
		ps.altura AS altura_Produto_Servico,
		ps.largura AS largura_Produto_Servico,
		ps.comprimento AS comprimento_Produto_Servico,
		ps.valor_total_materiais AS valor_total_materiais_Produto_Servico,
		ps.maoObra AS maoObra_Produto_Servico,
		ps.valor_maoObra AS valor_maoObra_Produto_Servico,
		ps.valor_total AS valor_total_Produto_Servico,
		ps.ativo_inativo AS ativo_inativo_Produto_Servico,
		ps.cod_Unidade AS cod_Unidade_Produto_Servico,
			up.sigla AS sigla_Unidade_Produto_Servico,
			up.descricao AS descricao_Unidade_Produto_Servico,
			up.ativo_inativo AS ativo_inativo_Unidade_Produto_Servico,
		ps.cod_Grupo AS cod_Grupo_Produto_Servico,
			gp.descricao AS descricao_Grupo_Produto_Servico,
			gp.material_ou_produto AS material_ou_produto_Grupo_Produto_Servico,
			gp.ativo_inativo AS ativo_inativo_Grupo_Produto_Servico
FROM tbl_Materiais_Produto_Servico AS mps
LEFT JOIN tbl_Material AS m ON mps.cod_Material = m.codigo
	LEFT JOIN tbl_Unidade AS um ON m.cod_Unidade = um.codigo
	LEFT JOIN tbl_Grupo AS gm ON m.cod_Grupo = gm.codigo
	LEFT JOIN tbl_Pessoa AS fm ON m.cod_Fornecedor = fm.codigo
LEFT JOIN tbl_Produto_Servico AS ps ON mps.cod_Produto_Servico = ps.codigo
	LEFT JOIN tbl_Unidade AS up ON ps.cod_Unidade = up.codigo
	LEFT JOIN tbl_Grupo AS gp ON ps.cod_Grupo = gp.codigo;

-- ORÇAMENTO -------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE VIEW vw_Orcamento AS
SELECT
	o.codigo,
    o.ultima_atualizacao,
    o.validade,
    o.prazo_entrega,
    o.observacao AS observacao_Orcamento,
    o.total_produtos_servicos,
    o.desconto,
    o.total_orcamento,
    o.descricao_pagamento,
    o.valor_entrada,
    o.quantidade_parcelas,
    o.valor_parcela,
    o.juros,
    o.valor_juros,
    o.ativo_inativo AS ativo_inativo_Orcamento,
    o.cod_Usuario,
		usu.tipo_pessoa AS tipo_pessoa_Usuario,
		usu.nome_razao_social AS nome_razao_social_Usuario,
		usu.nome_fantasia AS nome_fantasia_Usuario,
		usu.cpf_cnpj AS cpf_cnpj_Usuario,
		usu.inscricao_estadual AS inscricao_estadual_Usuario,
		usu.email AS email_Usuario,
		usu.observacao AS observacao_Usuario,
		usu.ativo_inativo AS ativo_inativo_Usuario,
	o.cod_Cliente,
		cli.tipo_pessoa AS tipo_pessoa_Cliente,
		cli.nome_razao_social AS nome_razao_social_Cliente,
		cli.nome_fantasia AS nome_fantasia_Cliente,
		cli.cpf_cnpj AS cpf_cnpj_Cliente,
		cli.inscricao_estadual AS inscricao_estadual_Cliente,
		cli.email AS email_Cliente,
		cli.observacao AS observacao_Cliente,
		cli.ativo_inativo AS ativo_inativo_Cliente
FROM tbl_Orcamento AS o
LEFT JOIN tbl_Pessoa AS usu ON o.cod_Usuario = usu.codigo
LEFT JOIN tbl_Pessoa AS cli ON o.cod_Cliente = cli.codigo;

-- PRODUTOS OU SERVIÇOS DO ORÇAMENTO  -------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE VIEW vw_Produtos_Servicos_Orcamento AS
SELECT
	pso.codigo,
    pso.quantidade,
    pso.valor_total,
    pso.cod_Produto_Servico,
		pso.ultima_atualizacao_Produto_Servico,
		pso.imagem_Produto_Servico AS imagem_Produto_Servico,
		pso.descricao_Produto_Servico,
		pso.altura_Produto_Servico,
		pso.largura_Produto_Servico,
		pso.comprimento_Produto_Servico,
        pso.valor_total_materiais_Produto_Servico,
		pso.maoObra_Produto_Servico,
		pso.valor_maoObra_Produto_Servico,
		pso.valor_unitario_Produto_Servico,
		pso.ativo_inativo_Produto_Servico,
	pso.cod_Unidade,
		pso.sigla_Unidade,
		pso.descricao_Unidade,
		pso.ativo_inativo_Unidade,
	pso.cod_Grupo,
		pso.descricao_Grupo,
        g.material_ou_produto AS material_ou_produto_Grupo,
		pso.ativo_inativo_Grupo,
	pso.cod_Orcamento,
		o.ultima_atualizacao,
		o.validade,
		o.prazo_entrega,
		o.observacao,
		o.total_produtos_servicos,
		o.desconto,
		o.total_orcamento,
		o.descricao_pagamento,
		o.valor_entrada,
		o.quantidade_parcelas,
		o.valor_parcela,
		o.juros,
		o.valor_juros,
		o.ativo_inativo AS ativo_inativo_Orcamento,
		o.cod_Usuario,
			usu.tipo_pessoa AS tipo_pessoa_Usuario,
			usu.nome_razao_social AS nome_razao_social_Usuario,
			usu.nome_fantasia AS nome_fantasia_Usuario,
			usu.cpf_cnpj AS cpf_cnpj_Usuario,
			usu.inscricao_estadual AS inscricao_estadual_Usuario,
			usu.email AS email_Usuario,
			usu.observacao AS observacao_Usuario,
			usu.ativo_inativo AS ativo_inativo_Usuario,
		o.cod_Cliente,
			cli.tipo_pessoa AS tipo_pessoa_Cliente,
			cli.nome_razao_social AS nome_razao_social_Cliente,
			cli.nome_fantasia AS nome_fantasia_Cliente,
			cli.cpf_cnpj AS cpf_cnpj_Cliente,
			cli.inscricao_estadual AS inscricao_estadual_Cliente,
			cli.email AS email_Cliente,
			cli.observacao AS observacao_Cliente,
			cli.ativo_inativo AS ativo_inativo_Cliente
FROM tbl_Produtos_Servicos_Orcamento AS pso
LEFT JOIN tbl_Produto_Servico AS ps ON pso.cod_Produto_Servico = ps.codigo
LEFT JOIN tbl_Unidade AS u ON pso.cod_Unidade = u.codigo
LEFT JOIN tbl_Grupo AS g ON pso.cod_Grupo = g.codigo
LEFT JOIN tbl_Orcamento AS o ON pso.cod_Orcamento = o.codigo
LEFT JOIN tbl_Pessoa AS usu ON o.cod_Usuario = usu.codigo
LEFT JOIN tbl_Pessoa AS cli ON o.cod_Cliente = cli.codigo;

-- MATERIAIS DO ORÇAMENTO  -------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE VIEW vw_Materiais_Orcamento AS
SELECT
	mo.codigo,
    mo.quantidade_total,
    mo.valor_total,
    mo.cod_Produtos_Servicos_Orcamento,
    mo.cod_Materiais_Produto_Servico,
		mo.quantidade_Materiais_Produto_Servico,
		mo.valor_unitario_Materiais_Produto_Servico,
	mo.cod_Material,
		mo.ultima_atualizacao_Material,
		mo.imagem_Material AS imagem_Material,
		mo.descricao_Material,
		mo.altura_Material,
		mo.largura_Material,
		mo.comprimento_Material,
        mo.valor_unitario_Material,
		mo.ativo_inativo_Material,
	mo.cod_Unidade,
		mo.sigla_Unidade,
		mo.descricao_Unidade,
		mo.ativo_inativo_Unidade,
	mo.cod_Grupo,
		mo.descricao_Grupo,
		mo.ativo_inativo_Grupo,
	mo.cod_Fornecedor,
		mo.nome_razao_social_Fornecedor,
		mo.nome_fantasia_Fornecedor,
		mo.ativo_inativo_Fornecedor,
	mo.cod_Orcamento
FROM tbl_Materiais_Orcamento AS mo
LEFT JOIN tbl_Produtos_Servicos_Orcamento AS pso ON mo.cod_Produtos_Servicos_Orcamento = pso.codigo
LEFT JOIN tbl_Materiais_Produto_Servico AS mps ON mo.cod_Materiais_Produto_Servico = mps.codigo
LEFT JOIN tbl_Material AS m ON mo.cod_Material = m.codigo
LEFT JOIN tbl_Unidade AS u ON mo.cod_Unidade = u.codigo
LEFT JOIN tbl_Grupo AS g ON mo.cod_Grupo = g.codigo
LEFT JOIN tbl_Pessoa AS f ON mo.cod_Fornecedor = f.codigo
LEFT JOIN tbl_Orcamento AS o ON mo.cod_Orcamento = o.codigo;
