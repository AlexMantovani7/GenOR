-- --------------------------------CRIANDO PROCEDURES DE CONSULTA DE DADOS USANDO AS VIEWs----------------------------------
USE GenOR_BD;

-- LOG -------------------------------------------------------------------------------------------------------------------------------------------------------------
DELIMITER $$
CREATE PROCEDURE sp_ConsultarLOG(
    IN var_pesquisarTodos BOOLEAN,
    IN var_codigo INT,
    IN var_data_registro DATETIME,
    IN var_operacao CHAR(11),                   -- CADASTRO, ALTERAÇÃO, INATIVAÇÃO, ATUALIZAÇÃO
    IN var_registro CHAR(32),
    IN var_informacoes_registro VARCHAR(1000)
)
BEGIN
	DECLARE var_controle_commit TINYINT DEFAULT TRUE;
	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET var_controle_commit = FALSE;
    
    SET @@AUTOCOMMIT = OFF;
    
    IF (var_pesquisarTodos) THEN
		
        START TRANSACTION;
				
		SELECT * FROM vw_LOG AS l
        ORDER BY l.codigo DESC;
		
        IF ( var_controle_commit ) THEN
			COMMIT;
		ELSE
			ROLLBACK;
			SELECT 'Não foi possivel realizar a consulta do registro.' AS 'ALERTA';
		END IF;
	
    ELSE
        
        START TRANSACTION;
        
		SELECT * FROM vw_LOG AS l
		WHERE
        ((l.codigo = var_codigo) OR (var_codigo IS NULL)) AND
        ((l.data_registro = var_data_registro) OR (var_data_registro IS NULL)) AND
        ((l.operacao = var_operacao) OR (var_operacao IS NULL)) AND
        ((l.registro = var_registro) OR (var_registro IS NULL)) AND
        ((l.informacoes_registro LIKE CONCAT(var_informacoes_registro, '%')) OR (var_informacoes_registro IS NULL))
        ORDER BY l.codigo DESC;
		
        IF ( var_controle_commit ) THEN
			COMMIT;
		ELSE
			ROLLBACK;
			SELECT 'Não foi possivel realizar a consulta do registro.' AS 'ALERTA';
		END IF;
    
    END IF;
            
END $$
DELIMITER ;

-- PESSOA -------------------------------------------------------------------------------------------------------------------------------------------------------------
DELIMITER $$
CREATE PROCEDURE sp_ConsultarPessoa(
    IN var_pesquisarTodos BOOLEAN,
    IN var_codigo INT,
    IN var_tipo_pessoa CHAR(10),                   -- USUÁRIO | CLIENTE | FORNECEDOR
    IN var_nome_razao_social VARCHAR(150),        -- nome de uma pessoa fisica ou razao social de um pessoa juridica
    IN var_nome_fantasia VARCHAR(150),
    IN var_cpf_cnpj CHAR(14),
    IN var_inscricao_estadual CHAR(12),
    IN var_email VARCHAR(256),
    IN var_observacao VARCHAR(150),
    IN var_ativo_inativo BOOLEAN
)
BEGIN
	DECLARE var_controle_commit TINYINT DEFAULT TRUE;
	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET var_controle_commit = FALSE;
    
    SET @@AUTOCOMMIT = OFF;
    
    IF (var_pesquisarTodos) THEN
		
        START TRANSACTION;
				
		SELECT * FROM vw_Pessoa AS p
        WHERE p.tipo_pessoa = var_tipo_pessoa AND p.ativo_inativo = var_ativo_inativo
        ORDER BY p.nome_razao_social ASC;
		
        IF ( var_controle_commit ) THEN
			COMMIT;
		ELSE
			ROLLBACK;
			SELECT 'Não foi possivel realizar a consulta do registro.' AS 'ALERTA';
		END IF;
	
    ELSE
        START TRANSACTION;
				
		SELECT * FROM vw_Pessoa AS p
		WHERE
        ((p.codigo = var_codigo) OR (var_codigo IS NULL)) AND
        ((p.tipo_pessoa = var_tipo_pessoa) OR (var_tipo_pessoa IS NULL)) AND
        ((p.nome_razao_social LIKE CONCAT(var_nome_razao_social, '%')) OR (var_nome_razao_social IS NULL)) AND
        ((p.nome_fantasia LIKE CONCAT(var_nome_fantasia, '%')) OR (var_nome_fantasia IS NULL)) AND
        ((p.cpf_cnpj LIKE CONCAT(var_cpf_cnpj, '%')) OR (var_cpf_cnpj IS NULL)) AND
        ((p.inscricao_estadual LIKE CONCAT(var_inscricao_estadual, '%')) OR (var_inscricao_estadual IS NULL)) AND
        ((p.email LIKE CONCAT(var_email, '%')) OR (var_email IS NULL)) AND
        ((p.observacao LIKE CONCAT(var_observacao, '%')) OR (var_observacao IS NULL)) AND
        ((p.ativo_inativo = var_ativo_inativo) OR (var_ativo_inativo IS NULL))
        ORDER BY p.nome_razao_social ASC;
		
        IF ( var_controle_commit ) THEN
			COMMIT;
		ELSE
			ROLLBACK;
			SELECT 'Não foi possivel realizar a consulta do registro.' AS 'ALERTA';
		END IF;
    
    END IF;
            
END $$
DELIMITER ;

-- LOGIN -------------------------------------------------------------------------------------------------------------------------------------------------------------
DELIMITER $$
CREATE PROCEDURE sp_ConsultarLogin(
    IN var_pesquisarPorUsu BOOLEAN,
    IN var_codigo INT,
    IN var_nome_usuario VARCHAR(256),
    IN var_senha VARCHAR(50),
    IN var_cod_Usuario INT
)
BEGIN
	DECLARE var_controle_commit TINYINT DEFAULT TRUE;
	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET var_controle_commit = FALSE;
    
    SET @@AUTOCOMMIT = OFF;
	
    IF (var_pesquisarPorUsu) THEN
		
        START TRANSACTION;
		
        SELECT * FROM vw_Login AS lg
		WHERE
		lg.cod_Usuario = var_cod_Usuario
		ORDER BY lg.codigo ASC;
		
        IF ( var_controle_commit ) THEN
			COMMIT;
		ELSE
			ROLLBACK;
			SELECT 'Não foi possivel realizar a consulta do registro.' AS 'ALERTA';
		END IF;
	
    ELSE
        
        START TRANSACTION;
		
        SELECT * FROM vw_Login AS lg
		WHERE
		BINARY lg.nome_usuario = var_nome_usuario AND
		BINARY lg.senha = var_senha
		ORDER BY lg.codigo ASC;
		
        IF ( var_controle_commit ) THEN
			COMMIT;
		ELSE
			ROLLBACK;
			SELECT 'Não foi possivel realizar a consulta do registro.' AS 'ALERTA';
		END IF;
    
    END IF;
    
END $$
DELIMITER ;

-- ENDERECO -------------------------------------------------------------------------------------------------------------------------------------------------------------
DELIMITER $$
CREATE PROCEDURE sp_ConsultarEndereco(
    IN var_pesquisarTodos BOOLEAN,
    IN var_codigo INT,
    IN var_endereco VARCHAR(150),
    IN var_complemento VARCHAR(150),
    IN var_numero CHAR (12),
	IN var_bairro VARCHAR(150),
    IN var_cidade VARCHAR(31),
	IN var_estado CHAR(2),
	IN var_cep CHAR(8),
    IN var_observacao VARCHAR(150),
	IN var_ativo_inativo BOOLEAN,
    IN var_cod_Pessoa INT
)
BEGIN
	DECLARE var_controle_commit TINYINT DEFAULT TRUE;
	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET var_controle_commit = FALSE;
    
    SET @@AUTOCOMMIT = OFF;
    
    IF (var_pesquisarTodos) THEN
		
        START TRANSACTION;
				
		SELECT * FROM vw_Endereco AS e
        WHERE e.cod_Pessoa = var_cod_Pessoa AND e.ativo_inativo_Endereco = var_ativo_inativo
        ORDER BY e.codigo ASC;
		
        IF ( var_controle_commit ) THEN
			COMMIT;
		ELSE
			ROLLBACK;
			SELECT 'Não foi possivel realizar a consulta do registro.' AS 'ALERTA';
		END IF;
	
    ELSE
        START TRANSACTION;
				
		SELECT * FROM vw_Endereco AS e
		WHERE
        ((e.codigo = var_codigo) OR (var_codigo IS NULL)) AND
		((e.endereco LIKE CONCAT(var_endereco, '%')) OR (var_endereco IS NULL)) AND
		((e.complemento LIKE CONCAT(var_complemento, '%')) OR (var_complemento IS NULL)) AND
		((e.numero LIKE CONCAT(var_numero, '%')) OR (var_numero IS NULL)) AND
		((e.bairro LIKE CONCAT(var_bairro, '%')) OR (var_bairro IS NULL)) AND
		((e.cidade LIKE CONCAT(var_cidade, '%')) OR (var_cidade IS NULL)) AND
		((e.estado LIKE CONCAT(var_estado, '%')) OR (var_estado IS NULL)) AND
		((e.cep LIKE CONCAT(var_cep, '%')) OR (var_cep IS NULL)) AND
		((e.observacao LIKE CONCAT(var_observacao, '%')) OR (var_observacao IS NULL)) AND
		((e.ativo_inativo_Endereco = var_ativo_inativo) OR (var_ativo_inativo IS NULL)) AND
		((e.cod_Pessoa = var_cod_Pessoa) OR (var_cod_Pessoa IS NULL))
        ORDER BY e.codigo ASC;
		
        IF ( var_controle_commit ) THEN
			COMMIT;
		ELSE
			ROLLBACK;
			SELECT 'Não foi possivel realizar a consulta do registro.' AS 'ALERTA';
		END IF;
    
    END IF;
            
END $$
DELIMITER ;

-- TELEFONE -------------------------------------------------------------------------------------------------------------------------------------------------------------
DELIMITER $$
CREATE PROCEDURE sp_ConsultarTelefone(
    IN var_pesquisarTodos BOOLEAN,
    IN var_codigo INT,
    IN var_ddd CHAR(2),                              -- 0800 não tem DDD
    IN var_numero CHAR(11),                          -- tamanho 0800
    IN var_observacao VARCHAR(150),
	IN var_ativo_inativo BOOLEAN,
    IN var_cod_Pessoa INT
)
BEGIN
	DECLARE var_controle_commit TINYINT DEFAULT TRUE;
	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET var_controle_commit = FALSE;
    
    SET @@AUTOCOMMIT = OFF;
    
    IF (var_pesquisarTodos) THEN
		
        START TRANSACTION;
				
		SELECT * FROM vw_Telefone AS t
        WHERE t.cod_Pessoa = var_cod_Pessoa AND t.ativo_inativo_Telefone = var_ativo_inativo
        ORDER BY t.codigo ASC;
		
        IF ( var_controle_commit ) THEN
			COMMIT;
		ELSE
			ROLLBACK;
			SELECT 'Não foi possivel realizar a consulta do registro.' AS 'ALERTA';
		END IF;
	
    ELSE
        START TRANSACTION;
				
		SELECT * FROM vw_Telefone AS t
		WHERE
        ((t.codigo = var_codigo) OR (var_codigo IS NULL)) AND
		((t.ddd LIKE CONCAT(var_ddd, '%')) OR (var_ddd IS NULL)) AND
		((t.numero LIKE CONCAT(var_numero, '%')) OR (var_numero IS NULL)) AND
		((t.observacao LIKE CONCAT(var_observacao, '%')) OR (var_observacao IS NULL)) AND
		((t.ativo_inativo_Telefone = var_ativo_inativo) OR (var_ativo_inativo IS NULL)) AND
		((t.cod_Pessoa = var_cod_Pessoa) OR (var_cod_Pessoa IS NULL))
        ORDER BY t.codigo ASC;
		
        IF ( var_controle_commit ) THEN
			COMMIT;
		ELSE
			ROLLBACK;
			SELECT 'Não foi possivel realizar a consulta do registro.' AS 'ALERTA';
		END IF;
    
    END IF;
            
END $$
DELIMITER ;

-- GRUPO -------------------------------------------------------------------------------------------------------------------------------------------------------------
DELIMITER $$
CREATE PROCEDURE sp_ConsultarGrupo(
    IN var_pesquisarTodos BOOLEAN,
    IN var_codigo INT,
    IN var_descricao VARCHAR(150),
    IN var_material_ou_produto CHAR(1),		-- M - Material | P - Produto,
	IN var_ativo_inativo BOOLEAN
)
BEGIN
	DECLARE var_controle_commit TINYINT DEFAULT TRUE;
	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET var_controle_commit = FALSE;
    
    SET @@AUTOCOMMIT = OFF;
    
    IF (var_pesquisarTodos) THEN
		
        START TRANSACTION;
				
		SELECT * FROM vw_Grupo AS g
        WHERE
        ((g.material_ou_produto = var_material_ou_produto) OR (var_material_ou_produto IS NULL)) AND
        g.ativo_inativo = var_ativo_inativo
        ORDER BY g.descricao ASC;
		
        IF ( var_controle_commit ) THEN
			COMMIT;
		ELSE
			ROLLBACK;
			SELECT 'Não foi possivel realizar a consulta do registro.' AS 'ALERTA';
		END IF;
	
    ELSE
        START TRANSACTION;
				
		SELECT * FROM vw_Grupo AS g
		WHERE
        ((g.codigo = var_codigo) OR (var_codigo IS NULL)) AND
        ((g.descricao LIKE CONCAT(var_descricao, '%')) OR (var_descricao IS NULL)) AND
		((g.material_ou_produto = var_material_ou_produto) OR (var_material_ou_produto IS NULL)) AND
		((g.ativo_inativo = var_ativo_inativo) OR (var_ativo_inativo IS NULL))
        ORDER BY g.descricao ASC;
		
        IF ( var_controle_commit ) THEN
			COMMIT;
		ELSE
			ROLLBACK;
			SELECT 'Não foi possivel realizar a consulta do registro.' AS 'ALERTA';
		END IF;
    
    END IF;
            
END $$
DELIMITER ;

-- UNIDADE -------------------------------------------------------------------------------------------------------------------------------------------------------------
DELIMITER $$
CREATE PROCEDURE sp_ConsultarUnidade(
    IN var_pesquisarTodos BOOLEAN,
    IN var_codigo INT,
    IN var_sigla CHAR(8),
    IN var_descricao VARCHAR(150),
	IN var_ativo_inativo BOOLEAN
)
BEGIN
	DECLARE var_controle_commit TINYINT DEFAULT TRUE;
	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET var_controle_commit = FALSE;
    
    SET @@AUTOCOMMIT = OFF;
    
    IF (var_pesquisarTodos) THEN
		
        START TRANSACTION;
				
		SELECT * FROM vw_Unidade AS u
        WHERE u.ativo_inativo = var_ativo_inativo
        ORDER BY u.descricao ASC;
		
        IF ( var_controle_commit ) THEN
			COMMIT;
		ELSE
			ROLLBACK;
			SELECT 'Não foi possivel realizar a consulta do registro.' AS 'ALERTA';
		END IF;
	
    ELSE
        START TRANSACTION;
				
		SELECT * FROM vw_Unidade AS u
		WHERE
        ((u.codigo = var_codigo) OR (var_codigo IS NULL)) AND
        ((u.sigla LIKE CONCAT(var_sigla, '%')) OR (var_sigla IS NULL)) AND
        ((u.descricao LIKE CONCAT(var_descricao, '%')) OR (var_descricao IS NULL)) AND
        ((u.ativo_inativo = var_ativo_inativo) OR (var_ativo_inativo IS NULL))
        ORDER BY u.descricao ASC;
		
        IF ( var_controle_commit ) THEN
			COMMIT;
		ELSE
			ROLLBACK;
			SELECT 'Não foi possivel realizar a consulta do registro.' AS 'ALERTA';
		END IF;
    
    END IF;
            
END $$
DELIMITER ;

-- MATERIAL -------------------------------------------------------------------------------------------------------------------------------------------------------------
DELIMITER $$
CREATE PROCEDURE sp_ConsultarMaterial(
    IN var_pesquisarTodos BOOLEAN,
    IN var_codigo INT,
    IN var_ultima_atualizacao DATETIME,
    IN var_imagem VARCHAR(260),                               -- salvar a imagem em bytes
    IN var_descricao VARCHAR(150),
    IN var_altura DECIMAL(13,2),
    IN var_largura DECIMAL(13,2),
    IN var_comprimento DECIMAL(13,2),
    IN var_valor_unitario DECIMAL(13,2),
    IN var_ativo_inativo BOOLEAN,
    IN var_cod_Unidade INT,
    IN var_cod_Grupo INT,
    IN var_cod_Fornecedor INT
)
BEGIN
	DECLARE var_controle_commit TINYINT DEFAULT TRUE;
	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET var_controle_commit = FALSE;
    
    SET @@AUTOCOMMIT = OFF;
    
    IF (var_pesquisarTodos) THEN
		
        START TRANSACTION;
				
		SELECT * FROM vw_Material AS m
        WHERE m.ativo_inativo_Material = var_ativo_inativo
        ORDER BY m.descricao_Material ASC;
		
        IF ( var_controle_commit ) THEN
			COMMIT;
		ELSE
			ROLLBACK;
			SELECT 'Não foi possivel realizar a consulta do registro.' AS 'ALERTA';
		END IF;
	
    ELSE
        START TRANSACTION;
				
		SELECT * FROM vw_Material AS m
		WHERE
        ((m.codigo = var_codigo) OR (var_codigo IS NULL)) AND
        ((m.ultima_atualizacao = var_ultima_atualizacao) OR (var_ultima_atualizacao IS NULL)) AND
		((m.imagem = var_imagem) OR (var_imagem IS NULL)) AND
		((m.descricao_Material LIKE CONCAT(var_descricao, '%')) OR (var_descricao IS NULL)) AND
		((m.altura = var_altura) OR (var_altura IS NULL)) AND
		((m.largura = var_largura) OR (var_largura IS NULL)) AND
		((m.comprimento = var_comprimento) OR (var_comprimento IS NULL)) AND
		((m.valor_unitario = var_valor_unitario) OR (var_valor_unitario IS NULL)) AND
		((m.ativo_inativo_Material = var_ativo_inativo) OR (var_ativo_inativo IS NULL)) AND
		((m.cod_Unidade = var_cod_Unidade) OR (var_cod_Unidade IS NULL)) AND
		((m.cod_Grupo = var_cod_Grupo) OR (var_cod_Grupo IS NULL)) AND
		((m.cod_Fornecedor = var_cod_Fornecedor) OR (var_cod_Fornecedor IS NULL))
        ORDER BY m.descricao_Material ASC;
		
        IF ( var_controle_commit ) THEN
			COMMIT;
		ELSE
			ROLLBACK;
			SELECT 'Não foi possivel realizar a consulta do registro.' AS 'ALERTA';
		END IF;
    
    END IF;
            
END $$
DELIMITER ;

-- PRODUTO OU SERVIÇO -------------------------------------------------------------------------------------------------------------------------------------------------------------
DELIMITER $$
CREATE PROCEDURE sp_ConsultarProduto_Servico(
    IN var_pesquisarTodos BOOLEAN,
    IN var_codigo INT,
    IN var_ultima_atualizacao DATETIME,
    IN var_imagem VARCHAR(260),                               -- salvar a imagem em bytes
    IN var_descricao VARCHAR(150),
    IN var_altura DECIMAL(13,2),
    IN var_largura DECIMAL(13,2),
    IN var_comprimento DECIMAL(13,2),
    IN var_valor_total_materiais DECIMAL(13,2),
    IN var_maoObra DECIMAL(13,2),
    IN var_valor_maoObra DECIMAL(13,2),
    IN var_valor_total DECIMAL(13,2),
    IN var_ativo_inativo BOOLEAN,
    IN var_cod_Unidade INT,
    IN var_cod_Grupo INT
)
BEGIN
	DECLARE var_controle_commit TINYINT DEFAULT TRUE;
	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET var_controle_commit = FALSE;
    
    SET @@AUTOCOMMIT = OFF;
    
    IF (var_pesquisarTodos) THEN
		
        START TRANSACTION;
				
		SELECT * FROM vw_Produto_Servico AS ps
        WHERE ps.ativo_inativo_Produto_Servico = var_ativo_inativo
        ORDER BY ps.descricao_Produto_Servico ASC;
		
        IF ( var_controle_commit ) THEN
			COMMIT;
		ELSE
			ROLLBACK;
			SELECT 'Não foi possivel realizar a consulta do registro.' AS 'ALERTA';
		END IF;
	
    ELSE
        START TRANSACTION;
				
		SELECT * FROM vw_Produto_Servico AS ps
		WHERE
        ((ps.codigo = var_codigo) OR (var_codigo IS NULL)) AND
        ((ps.ultima_atualizacao = var_ultima_atualizacao) OR (var_ultima_atualizacao IS NULL)) AND
		((ps.imagem = var_imagem) OR (var_imagem IS NULL)) AND
		((ps.descricao_Produto_Servico LIKE CONCAT(var_descricao, '%')) OR (var_descricao IS NULL)) AND
		((ps.altura = var_altura) OR (var_altura IS NULL)) AND
		((ps.largura = var_largura) OR (var_largura IS NULL)) AND
		((ps.comprimento = var_comprimento) OR (var_comprimento IS NULL)) AND
        ((ps.valor_total_materiais = var_valor_total_materiais) OR (var_valor_total_materiais IS NULL)) AND
		((ps.maoObra = var_maoObra) OR (var_maoObra IS NULL)) AND
		((ps.valor_maoObra = var_valor_maoObra) OR (var_valor_maoObra IS NULL)) AND
		((ps.valor_total = var_valor_total) OR (var_valor_total IS NULL)) AND
		((ps.ativo_inativo_Produto_Servico = var_ativo_inativo) OR (var_ativo_inativo IS NULL)) AND
		((ps.cod_Unidade = var_cod_Unidade) OR (var_cod_Unidade IS NULL)) AND
		((ps.cod_Grupo = var_cod_Grupo) OR (var_cod_Grupo IS NULL))
        ORDER BY ps.descricao_Produto_Servico ASC;
		
        IF ( var_controle_commit ) THEN
			COMMIT;
		ELSE
			ROLLBACK;
			SELECT 'Não foi possivel realizar a consulta do registro.' AS 'ALERTA';
		END IF;
    
    END IF;
            
END $$
DELIMITER ;

-- MATERIAIS DO PRODUTO OU SERVIÇO  -------------------------------------------------------------------------------------------------------------------------------------------------------------
DELIMITER $$
CREATE PROCEDURE sp_ConsultarMateriais_Produto_Servico(
    IN var_pesquisarTodos BOOLEAN,
    IN var_codigo INT,
    IN var_quantidade DECIMAL(13,2),
	IN var_valor_total DECIMAL(13,2),
    IN var_ativo_inativo BOOLEAN,
    IN var_cod_Material INT,
    IN var_cod_Produto_Servico INT
)
BEGIN
	DECLARE var_controle_commit TINYINT DEFAULT TRUE;
	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET var_controle_commit = FALSE;
    
    SET @@AUTOCOMMIT = OFF;
    
    IF (var_pesquisarTodos) THEN
		
        START TRANSACTION;
				
		SELECT * FROM vw_Materiais_Produto_Servico AS mps
        WHERE mps.ativo_inativo_Materiais_Produto_Servico = var_ativo_inativo
        ORDER BY mps.descricao_Material ASC;
		
        IF ( var_controle_commit ) THEN
			COMMIT;
		ELSE
			ROLLBACK;
			SELECT 'Não foi possivel realizar a consulta do registro.' AS 'ALERTA';
		END IF;
	
    ELSE
        START TRANSACTION;
				
		SELECT * FROM vw_Materiais_Produto_Servico AS mps
		WHERE
        ((mps.codigo = var_codigo) OR (var_codigo IS NULL)) AND
        ((mps.quantidade = var_quantidade) OR (var_quantidade IS NULL)) AND
		((mps.valor_total = var_valor_total) OR (var_valor_total IS NULL)) AND
		((mps.ativo_inativo_Materiais_Produto_Servico = var_ativo_inativo) OR (var_ativo_inativo IS NULL)) AND
		((mps.cod_Material = var_cod_Material) OR (var_cod_Material IS NULL)) AND
		((mps.cod_Produto_Servico = var_cod_Produto_Servico) OR (var_cod_Produto_Servico IS NULL))
        ORDER BY mps.descricao_Material ASC;
		
        IF ( var_controle_commit ) THEN
			COMMIT;
		ELSE
			ROLLBACK;
			SELECT 'Não foi possivel realizar a consulta do registro.' AS 'ALERTA';
		END IF;
    
    END IF;
            
END $$
DELIMITER ;

-- ANDÇAMENTO -------------------------------------------------------------------------------------------------------------------------------------------------------------
DELIMITER $$
CREATE PROCEDURE sp_ConsultarOrcamento(
    IN var_pesquisarTodos BOOLEAN,
    IN var_codigo INT,
    IN var_ultima_atualizacao DATETIME,
    IN var_validade DATETIME,
    IN var_prazo_entrega VARCHAR(150),
    IN var_observacao VARCHAR(150),
    IN var_total_produtos_servicos DECIMAL(13,2),
    IN var_desconto DECIMAL(13,2),                                 -- Desconto em %
    IN var_total_orcamento DECIMAL(13,2),
    IN var_descricao_pagamento VARCHAR(150),
    IN var_valor_entrada DECIMAL(13,2),
    IN var_quantidade_parcelas INT,
    IN var_valor_parcela DECIMAL(13,2),
    IN var_juros DECIMAL(13, 2),                                     -- Juros em %
    IN var_valor_juros DECIMAL(13,2),                     -- Valor juros por parcela
    IN var_ativo_inativo BOOLEAN,
    IN var_cod_Usuario INT,
	IN var_cod_Cliente INT
)
BEGIN
	DECLARE var_controle_commit TINYINT DEFAULT TRUE;
	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET var_controle_commit = FALSE;
    
    SET @@AUTOCOMMIT = OFF;
    
    IF (var_pesquisarTodos) THEN
		
        START TRANSACTION;
				
		SELECT * FROM vw_Orcamento AS o
        WHERE o.ativo_inativo_Orcamento = var_ativo_inativo
        ORDER BY o.ultima_atualizacao DESC;
		
        IF ( var_controle_commit ) THEN
			COMMIT;
		ELSE
			ROLLBACK;
			SELECT 'Não foi possivel realizar a consulta do registro.' AS 'ALERTA';
		END IF;
	
    ELSE
        START TRANSACTION;
				
		SELECT * FROM vw_Orcamento AS o
		WHERE
        ((o.codigo = var_codigo) OR (var_codigo IS NULL)) AND
        ((o.ultima_atualizacao = var_ultima_atualizacao) OR (var_ultima_atualizacao IS NULL)) AND
		((o.validade = var_validade) OR (var_validade IS NULL)) AND
		((o.prazo_entrega LIKE CONCAT(var_prazo_entrega, '%')) OR (var_prazo_entrega IS NULL)) AND
		((o.observacao_Orcamento LIKE CONCAT(var_observacao, '%')) OR (var_observacao IS NULL)) AND
		((o.total_produtos_servicos = var_total_produtos_servicos) OR (var_total_produtos_servicos IS NULL)) AND
		((o.desconto = var_desconto) OR (var_desconto IS NULL)) AND
		((o.total_orcamento = var_total_orcamento) OR (var_total_orcamento IS NULL)) AND
		((o.descricao_pagamento LIKE CONCAT(var_descricao_pagamento, '%')) OR (var_descricao_pagamento IS NULL)) AND
		((o.valor_entrada = var_valor_entrada) OR (var_valor_entrada IS NULL)) AND
		((o.quantidade_parcelas = var_quantidade_parcelas) OR (var_quantidade_parcelas IS NULL)) AND
		((o.valor_parcela = var_valor_parcela) OR (var_valor_parcela IS NULL)) AND
		((o.juros = var_juros) OR (var_juros IS NULL)) AND
		((o.valor_juros = var_valor_juros) OR (var_valor_juros IS NULL)) AND
		((o.ativo_inativo_Orcamento = var_ativo_inativo) OR (var_ativo_inativo IS NULL)) AND
		((o.cod_Usuario = var_cod_Usuario) OR (var_cod_Usuario IS NULL)) AND
		((o.cod_Cliente = var_cod_Cliente) OR (var_cod_Cliente IS NULL))
        ORDER BY o.ultima_atualizacao DESC;
		
        IF ( var_controle_commit ) THEN
			COMMIT;
		ELSE
			ROLLBACK;
			SELECT 'Não foi possivel realizar a consulta do registro.' AS 'ALERTA';
		END IF;
    
    END IF;
            
END $$
DELIMITER ;

-- PRODUTOS OU SERVIÇOS DO ANDÇAMENTO  -------------------------------------------------------------------------------------------------------------------------------------------------------------
DELIMITER $$
CREATE PROCEDURE sp_ConsultarProdutos_Servicos_Orcamento(
    IN var_pesquisarTodos BOOLEAN,
    IN var_codigo INT,
    IN var_quantidade DECIMAL(13,2),
    IN var_valor_total DECIMAL(13,2),
    IN var_cod_Produto_Servico INT,
	IN var_ultima_atualizacao_Produto_Servico DATETIME,
    IN var_imagem_Produto_Servico VARCHAR(260),                               -- salvar a imagem em bytes
    IN var_descricao_Produto_Servico VARCHAR(150),
    IN var_altura_Produto_Servico DECIMAL(13,2),
    IN var_largura_Produto_Servico DECIMAL(13,2),
    IN var_comprimento_Produto_Servico DECIMAL(13,2),
    IN var_valor_total_materiais_Produto_Servico DECIMAL(13,2),
    IN var_maoObra_Produto_Servico DECIMAL(13,2),
    IN var_valor_maoObra_Produto_Servico DECIMAL(13,2),
    IN var_valor_unitario_Produto_Servico DECIMAL(13,2),
    IN var_ativo_inativo_Produto_Servico BOOLEAN,
    IN var_cod_Unidade INT,
    IN var_sigla_Unidade CHAR(8),
    IN var_descricao_Unidade VARCHAR(150),
    IN var_ativo_inativo_Unidade BOOLEAN,
    IN var_cod_Grupo INT,
    IN var_descricao_Grupo VARCHAR(150),
    IN var_ativo_inativo_Grupo BOOLEAN,
    IN var_cod_Orcamento INT
)
BEGIN
	DECLARE var_controle_commit TINYINT DEFAULT TRUE;
	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET var_controle_commit = FALSE;
    
    SET @@AUTOCOMMIT = OFF;
    
    IF (var_pesquisarTodos) THEN
		
        START TRANSACTION;
				
		SELECT * FROM vw_Produtos_Servicos_Orcamento AS pso
        WHERE pso.cod_Orcamento = var_cod_Orcamento
        ORDER BY pso.descricao_Produto_Servico ASC;
		
        IF ( var_controle_commit ) THEN
			COMMIT;
		ELSE
			ROLLBACK;
			SELECT 'Não foi possivel realizar a consulta do registro.' AS 'ALERTA';
		END IF;
	
    ELSE
        START TRANSACTION;
				
		SELECT * FROM vw_Produtos_Servicos_Orcamento AS pso
		WHERE
        ((pso.codigo = var_codigo) OR (var_codigo IS NULL)) AND
        ((pso.quantidade = var_quantidade) OR (var_quantidade IS NULL)) AND
		((pso.valor_total = var_valor_total) OR (var_valor_total IS NULL)) AND
		((pso.cod_Produto_Servico = var_cod_Produto_Servico) OR (var_cod_Produto_Servico IS NULL)) AND
			((pso.ultima_atualizacao_Produto_Servico = var_ultima_atualizacao_Produto_Servico) OR (var_ultima_atualizacao_Produto_Servico IS NULL)) AND
			((pso.imagem_Produto_Servico = var_imagem_Produto_Servico) OR (var_imagem_Produto_Servico IS NULL)) AND
			((pso.descricao_Produto_Servico LIKE CONCAT(var_descricao_Produto_Servico, '%')) OR (var_descricao_Produto_Servico IS NULL)) AND
			((pso.altura_Produto_Servico = var_altura_Produto_Servico) OR (var_altura_Produto_Servico IS NULL)) AND
			((pso.largura_Produto_Servico = var_largura_Produto_Servico) OR (var_largura_Produto_Servico IS NULL)) AND
			((pso.comprimento_Produto_Servico = var_comprimento_Produto_Servico) OR (var_comprimento_Produto_Servico IS NULL)) AND
            ((pso.valor_total_materiais_Produto_Servico = var_valor_total_materiais_Produto_Servico) OR (var_valor_total_materiais_Produto_Servico IS NULL)) AND
			((pso.maoObra_Produto_Servico = var_maoObra_Produto_Servico) OR (var_maoObra_Produto_Servico IS NULL)) AND
			((pso.valor_maoObra_Produto_Servico = var_valor_maoObra_Produto_Servico) OR (var_valor_maoObra_Produto_Servico IS NULL)) AND
			((pso.valor_unitario_Produto_Servico = var_valor_unitario_Produto_Servico) OR (var_valor_unitario_Produto_Servico IS NULL)) AND
			((pso.ativo_inativo_Produto_Servico = var_ativo_inativo_Produto_Servico) OR (var_ativo_inativo_Produto_Servico IS NULL)) AND
		((pso.cod_Unidade = var_cod_Unidade) OR (var_cod_Unidade IS NULL)) AND
			((pso.sigla_Unidade LIKE CONCAT(var_sigla_Unidade, '%')) OR (var_sigla_Unidade IS NULL)) AND
            ((pso.descricao_Unidade LIKE CONCAT(var_descricao_Unidade, '%')) OR (var_descricao_Unidade IS NULL)) AND
			((pso.ativo_inativo_Unidade = var_ativo_inativo_Unidade) OR (var_ativo_inativo_Unidade IS NULL)) AND
		((pso.cod_Grupo = var_cod_Grupo) OR (var_cod_Grupo IS NULL)) AND
			((pso.descricao_Grupo LIKE CONCAT(var_descricao_Grupo, '%')) OR (var_descricao_Grupo IS NULL)) AND
			((pso.ativo_inativo_Grupo = var_ativo_inativo_Grupo) OR (var_ativo_inativo_Grupo IS NULL)) AND
		((pso.cod_Orcamento = var_cod_Orcamento) OR (var_cod_Orcamento IS NULL))
        ORDER BY pso.descricao_Produto_Servico ASC;
        
        IF ( var_controle_commit ) THEN
			COMMIT;
		ELSE
			ROLLBACK;
			SELECT 'Não foi possivel realizar a consulta do registro.' AS 'ALERTA';
		END IF;
    
    END IF;
            
END $$
DELIMITER ;

-- MATERIAIS DO ORÇAMENTO  -------------------------------------------------------------------------------------------------------------------------------------------------------------
DELIMITER $$
CREATE PROCEDURE sp_ConsultarMateriais_Orcamento(
    IN var_pesquisarTodos BOOLEAN,
    IN var_codigo INT,
    IN var_quantidade_total DECIMAL(13,2),
    IN var_valor_total DECIMAL(13,2),
    IN var_cod_Produtos_Servicos_Orcamento INT,
    IN var_cod_Materiais_Produto_Servico INT,
		IN var_quantidade_Materiais_Produto_Servico DECIMAL(13,2),
		IN var_valor_unitario_Materiais_Produto_Servico DECIMAL(13,2),
    IN var_cod_Material INT,
		IN var_ultima_atualizacao_Material DATETIME,
		IN var_imagem_Material VARCHAR(260),                               -- salvar a imagem em bytes
		IN var_descricao_Material VARCHAR(150),
		IN var_altura_Material DECIMAL(13,2),
		IN var_largura_Material DECIMAL(13,2),
		IN var_comprimento_Material DECIMAL(13,2),
        IN var_valor_unitario_Material DECIMAL(13,2),
		IN var_ativo_inativo_Material BOOLEAN,
    IN var_cod_Unidade INT,
		IN var_sigla_Unidade CHAR(8),
		IN var_descricao_Unidade VARCHAR(150),
		IN var_ativo_inativo_Unidade BOOLEAN,
    IN var_cod_Grupo INT,
		IN var_descricao_Grupo VARCHAR(150),
		IN var_ativo_inativo_Grupo BOOLEAN,
	IN var_cod_Fornecedor INT,
		IN var_nome_razao_social_Fornecedor VARCHAR(150),
		IN var_nome_fantasia_Fornecedor VARCHAR(150),
		IN var_ativo_inativo_Fornecedor BOOLEAN,
	IN var_cod_Orcamento INT
)
BEGIN
	DECLARE var_controle_commit TINYINT DEFAULT TRUE;
	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET var_controle_commit = FALSE;
    
    SET @@AUTOCOMMIT = OFF;
    
    IF (var_pesquisarTodos) THEN
		
        START TRANSACTION;
				
		SELECT * FROM vw_Materiais_Orcamento AS mo
        WHERE mo.cod_Orcamento = var_cod_Orcamento
        ORDER BY mo.descricao_Material ASC;
		
        IF ( var_controle_commit ) THEN
			COMMIT;
		ELSE
			ROLLBACK;
			SELECT 'Não foi possivel realizar a consulta do registro.' AS 'ALERTA';
		END IF;
	
    ELSE
        START TRANSACTION;
				
		SELECT * FROM vw_Materiais_Orcamento AS mo
		WHERE
        ((mo.codigo = var_codigo) OR (var_codigo IS NULL)) AND
        ((mo.quantidade_total = var_quantidade_total) OR (var_quantidade_total IS NULL)) AND
		((mo.valor_total = var_valor_total) OR (var_valor_total IS NULL)) AND
        ((mo.cod_Produtos_Servicos_Orcamento = var_cod_Produtos_Servicos_Orcamento) OR (var_cod_Produtos_Servicos_Orcamento IS NULL)) AND
		((mo.cod_Materiais_Produto_Servico = var_cod_Materiais_Produto_Servico) OR (var_cod_Materiais_Produto_Servico IS NULL)) AND
			((mo.quantidade_Materiais_Produto_Servico = var_quantidade_Materiais_Produto_Servico) OR (var_quantidade_Materiais_Produto_Servico IS NULL)) AND
			((mo.valor_unitario_Materiais_Produto_Servico = var_valor_unitario_Materiais_Produto_Servico) OR (var_valor_unitario_Materiais_Produto_Servico IS NULL)) AND
		((mo.cod_Produto_Servico = var_cod_Produto_Servico) OR (var_cod_Produto_Servico IS NULL)) AND
			((mo.ultima_atualizacao_Material = var_ultima_atualizacao_Material) OR (var_ultima_atualizacao_Material IS NULL)) AND
			((mo.imagem_Material = var_imagem_Material) OR (var_imagem_Material IS NULL)) AND
			((mo.descricao_Material LIKE CONCAT(var_descricao_Material, '%')) OR (var_descricao_Material IS NULL)) AND
			((mo.altura_Material = var_altura_Material) OR (var_altura_Material IS NULL)) AND
			((mo.largura_Material = var_largura_Material) OR (var_largura_Material IS NULL)) AND
			((mo.comprimento_Material = var_comprimento_Material) OR (var_comprimento_Material IS NULL)) AND
            ((mo.valor_unitario_Material = var_valor_unitario_Material) OR (var_valor_unitario_Material IS NULL)) AND
			((mo.ativo_inativo_Material = var_ativo_inativo_Material) OR (var_ativo_inativo_Material IS NULL)) AND
		((mo.cod_Unidade = var_cod_Unidade) OR (var_cod_Unidade IS NULL)) AND
			((mo.sigla_Unidade LIKE CONCAT(var_sigla_Unidade, '%')) OR (var_sigla_Unidade IS NULL)) AND
            ((mo.descricao_Unidade LIKE CONCAT(var_descricao_Unidade, '%')) OR (var_descricao_Unidade IS NULL)) AND
			((mo.ativo_inativo_Unidade = var_ativo_inativo_Unidade) OR (var_ativo_inativo_Unidade IS NULL)) AND
		((mo.cod_Grupo = var_cod_Grupo) OR (var_cod_Grupo IS NULL)) AND
			((mo.descricao_Grupo LIKE CONCAT(var_descricao_Grupo, '%')) OR (var_descricao_Grupo IS NULL)) AND
			((mo.ativo_inativo_Grupo = var_ativo_inativo_Grupo) OR (var_ativo_inativo_Grupo IS NULL)) AND
		((mo.cod_Fornecedor = var_cod_Fornecedor) OR (var_cod_Fornecedor IS NULL)) AND
			((mo.nome_razao_social_Fornecedor = var_nome_razao_social_Fornecedor) OR (var_nome_razao_social_Fornecedor IS NULL)) AND
			((mo.nome_fantasia_Fornecedor = var_nome_fantasia_Fornecedor) OR (var_nome_fantasia_Fornecedor IS NULL)) AND
			((mo.ativo_inativo_Fornecedor = var_ativo_inativo_Fornecedor) OR (var_ativo_inativo_Fornecedor IS NULL)) AND
		((mo.cod_Orcamento = var_cod_Orcamento) OR (var_cod_Orcamento IS NULL))
        ORDER BY mo.descricao_Material ASC;
        
        IF ( var_controle_commit ) THEN
			COMMIT;
		ELSE
			ROLLBACK;
			SELECT 'Não foi possivel realizar a consulta do registro.' AS 'ALERTA';
		END IF;
    
    END IF;
            
END $$
DELIMITER ;