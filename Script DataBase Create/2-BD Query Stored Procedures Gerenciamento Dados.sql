-- --------------------------------CRIANDO PROCEDURES----------------------------------
USE GenOR_BD;

-- LOG -------------------------------------------------------------------------------------------------------------------------------------------------------------
DELIMITER $$
CREATE PROCEDURE sp_GerarLOG(
    IN var_operacao CHAR(11),                   -- CADASTRO, ALTERAÇÃO, INATIVAÇÃO, ATUALIZAÇÃO
    IN var_registro CHAR(32),					-- Pessoa, Endereco, Telefone, Grupo, Unidade, Material, Produto ou Serviço, Materiais do Produto ou Serviço, Orçamento
    IN var_informacoes_registro VARCHAR(1000)
)
BEGIN
	DECLARE var_controle_commit TINYINT DEFAULT TRUE;		-- Cria uma var_controle_commit que vai ser usada para check se o commit pode ser realizado, ou dar rollback em um erro.
	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET var_controle_commit = FALSE;		-- Caso haja um erro na execução, a var_controle_commit receberá FALSE para acionar o rollback.
    
    SET @@AUTOCOMMIT = OFF;	-- Desliga o commit automatico para que o "Try, Catch e rollback" possa funcionar corretamente e commitar manualmente.
    
    IF ( (var_operacao IS NULL) OR (var_registro IS NULL) OR (var_informacoes_registro IS NULL) ) THEN
		SELECT CONCAT('A informação está NULL ou Inválida. \r\n ', 
        '\r\n CAMPO Operação: ( ', IFNULL(var_operacao, 'NULL'), ' )',
        '\r\n CAMPO Registro: ( ', IFNULL(var_registro, 'NULL'), ' )',
        '\r\n CAMPO Informações do Registro: ( ', IFNULL(var_informacoes_registro, 'NULL'), ' )' ) AS 'ALERTA';
        
    ELSE
    
        IF ( (var_operacao = 'CADASTRO') OR (var_operacao = 'ALTERAÇÃO') OR (var_operacao = 'INATIVAÇÃO') OR (var_operacao = 'ATUALIZAÇÃO') ) THEN
			
            START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
            
            INSERT INTO tbl_LOG
            (data_registro, operacao, registro, informacoes_registro)
            VALUES
            (localtimestamp(0), var_operacao, var_registro, var_informacoes_registro);
            
            -- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
            IF ( var_controle_commit ) THEN
				COMMIT;
            ELSE
				ROLLBACK;
                SELECT 'Erro na transação de dados de LOG, operação de registro de LOG não pode ser realizada.' AS 'ALERTA';
			END IF;
        
        ELSE
			SELECT CONCAT('A operação informada não é válida: ( ', IFNULL(var_operacao, 'NULL'), ' ).' ) AS 'ALERTA';
		END IF;
        
	END IF;
    
END $$
DELIMITER ;

-- PESSOA -------------------------------------------------------------------------------------------------------------------------------------------------------------
DELIMITER $$
CREATE PROCEDURE sp_ManterPessoa(
	IN var_operacao CHAR(11), -- CADASTRO, ALTERAÇÃO, INATIVAÇÃO, ATUALIZAÇÃO(caso possua)
	IN var_codigo INT,
    IN var_tipo_pessoa CHAR(10),                 -- USUARIO | CLIENTE | FORNECEDOR
    IN var_nome_razao_social VARCHAR(150),
    IN var_nome_fantasia VARCHAR(150),
    IN var_cpf_cnpj CHAR(14),
    IN var_inscricao_estadual CHAR(12),
    IN var_email VARCHAR(256),
    IN var_observacao VARCHAR(150),
    IN var_ativo_inativo BOOLEAN
)
BEGIN
	DECLARE var_resgistro_LOG CHAR(32);
	DECLARE var_informacoes_registro VARCHAR(1000);
    
	DECLARE var_controle_commit TINYINT DEFAULT TRUE;		-- Cria uma var_controle_commit que vai ser usada para check se o commit pode ser realizado, ou dar rollback em um erro.
	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET var_controle_commit = FALSE;		-- Caso haja um erro na execução, a var_controle_commit receberá FALSE para acionar o rollback.
    
    SET var_resgistro_LOG = 'PESSOA';
    SET @@AUTOCOMMIT = OFF;	-- Desliga o commit automatico para que o "Try, Catch e rollback" possa funcionar corretamente e commitar manualmente.
    
    IF ( (var_codigo IS NULL) OR (var_tipo_pessoa IS NULL) OR (var_nome_razao_social IS NULL) OR (var_nome_fantasia IS NULL) OR (var_cpf_cnpj IS NULL) OR (var_inscricao_estadual IS NULL) OR (var_email IS NULL) OR (var_observacao IS NULL) OR (var_ativo_inativo IS NULL) ) THEN
		SELECT CONCAT('A informação está NULL ou Inválida. \r\n ', 
        '\r\n CAMPO Código: ( ', IFNULL(var_codigo, 'NULL'), ' )',
        '\r\n CAMPO Tipo: ( ', IFNULL(var_tipo_pessoa, 'NULL'), ' )',
        '\r\n CAMPO Nome/Razão Social: ( ', IFNULL(var_nome_razao_social, 'NULL'), ' )',
        '\r\n CAMPO Nome Fantasia: ( ', IFNULL(var_nome_fantasia, 'NULL'), ' )',
        '\r\n CAMPO CPF/CNPJ: ( ', IFNULL(var_cpf_cnpj, 'NULL'), ' )',
        '\r\n CAMPO Inscrição Estadual: ( ', IFNULL(var_inscricao_estadual, 'NULL'), ' )',
        '\r\n CAMPO Email: ( ', IFNULL(var_email, 'NULL'), ' )',
        '\r\n CAMPO Observação: ( ', IFNULL(var_observacao, 'NULL'), ' )',
        '\r\n CAMPO Registro Ativo: ( ', IFNULL(var_ativo_inativo, 'NULL'), ' )'
        ) AS 'ALERTA';
            
    ELSE
    
		IF ( (var_operacao = 'CADASTRO') OR (var_operacao = 'ALTERAÇÃO') OR (var_operacao = 'INATIVAÇÃO') ) THEN			-- OR (var_operacao = 'ATUALIZAÇÃO') para os que permitem.
        
-- -----CADASTRO ------------------------------------------------------------------------------------------------------------------------------------------------
			IF (var_operacao = 'CADASTRO') THEN
            
                SET var_ativo_inativo = TRUE;
                
                START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
                
                INSERT INTO tbl_Pessoa
				(tipo_pessoa, nome_razao_social, nome_fantasia, cpf_cnpj, inscricao_estadual, email, observacao, ativo_inativo)
				VALUES
				(var_tipo_pessoa, var_nome_razao_social, var_nome_fantasia, var_cpf_cnpj, var_inscricao_estadual, var_email, var_observacao, var_ativo_inativo);
                
				SELECT MAX(p.codigo)
                INTO var_codigo
				FROM tbl_Pessoa AS p LIMIT 1;
                
				SET var_informacoes_registro = CONCAT('Código: ( ', IFNULL(var_codigo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                'Tipo: ( ', IFNULL(var_tipo_pessoa, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                'Nome/Razão Social: ( ', IFNULL(var_nome_razao_social, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                'Nome Fantasia: ( ', IFNULL(var_nome_fantasia, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                'CPF/CNPJ: ( ', IFNULL(var_cpf_cnpj, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                'Inscricao Estadual: ( ', IFNULL(var_inscricao_estadual, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                'Email: ( ', IFNULL(var_email, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                'Observação: ( ', IFNULL(var_observacao, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                'Registro Ativo: ( ', IFNULL(var_ativo_inativo, 'ERROR: INFORMAÇÃO NULL'), ' ).');
                
				CALL sp_GerarLOG(var_operacao, var_resgistro_LOG, var_informacoes_registro);
                
                -- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
                IF ( var_controle_commit ) THEN
					COMMIT;
                    SELECT var_codigo AS 'SUCESSO';
				ELSE
					ROLLBACK;
                    SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
				END IF;
		
-- -----ALTERAÇÃO OU INATIVAÇÃO ------------------------------------------------------------------------------------------------------------------------------------------------
			ELSEIF ( (var_operacao = 'ALTERAÇÃO') OR (var_operacao = 'INATIVAÇÃO') ) THEN
				
                IF( var_ativo_inativo ) THEN
					
                    IF (var_operacao = 'INATIVAÇÃO') THEN
						SET var_ativo_inativo = FALSE;
					END IF;
                    
                    IF( var_codigo > 0 ) THEN
						
                        START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
                        
                        UPDATE tbl_Pessoa
                        SET
                        tipo_pessoa = var_tipo_pessoa,
                        nome_razao_social = var_nome_razao_social,
                        nome_fantasia = var_nome_fantasia,
                        cpf_cnpj = var_cpf_cnpj,
                        inscricao_estadual = var_inscricao_estadual,
                        email = var_email,
                        observacao = var_observacao,
                        ativo_inativo = var_ativo_inativo
                        WHERE
                        codigo = var_codigo AND ativo_inativo = TRUE;
                        
                        SET var_informacoes_registro = CONCAT('Código: ( ', IFNULL(var_codigo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                        'Tipo: ( ', IFNULL(var_tipo_pessoa, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                        'Nome/Razão Social: ( ', IFNULL(var_nome_razao_social, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                        'Nome Fantasia: ( ', IFNULL(var_nome_fantasia, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                        'CPF/CNPJ: ( ', IFNULL(var_cpf_cnpj, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                        'Inscricao Estadual: ( ', IFNULL(var_inscricao_estadual, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                        'Email: ( ', IFNULL(var_email, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                        'Observação: ( ', IFNULL(var_observacao, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                        'Registro Ativo: ( ', IFNULL(var_ativo_inativo, 'ERROR: INFORMAÇÃO NULL'), ' ).');
                        
                        CALL sp_GerarLOG(var_operacao, var_resgistro_LOG, var_informacoes_registro);
                        
                        -- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
                        IF ( var_controle_commit ) THEN
							COMMIT;
                            SELECT var_codigo AS 'SUCESSO';
						ELSE
							ROLLBACK;
                            SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
						END IF;
                
                    ELSE
						SELECT 'A operação requisitada não pode ser realizada pois o Código informado não é válido.' AS 'ALERTA';
					END IF;
                    
				ELSE
					SELECT 'A operação requisitada não pode ser realizada pois o registro não está mais ativo.' AS 'ALERTA';
				END IF;
		
-- -----CASO NÃO TENHA INSERIDO NENHUMA OPERAÇÃO ----------------------------------------------------------------------------------------------------------------
			ELSE
				SELECT CONCAT('A operação ( ', IFNULL(var_operacao, 'NULL'), ' ) não é VÁLIDA.') AS 'ALERTA';
			END IF;
        
        ELSE
            SELECT CONCAT( 'A operação informada não é válida: ( ', IFNULL(var_operacao, 'NULL'), ' ).' ) AS 'ALERTA';
		END IF;
		
	END IF;
    
END $$
DELIMITER ;

-- LOGIN -------------------------------------------------------------------------------------------------------------------------------------------------------------
DELIMITER $$
CREATE PROCEDURE sp_ManterLogin(
	IN var_operacao CHAR(11), -- CADASTRO, ALTERAÇÃO, INATIVAÇÃO, ATUALIZAÇÃO(caso possua)
	IN var_codigo INT,
    IN var_nome_usuario VARCHAR(256),
    IN var_senha VARCHAR(50),
    IN var_cod_Usuario INT
)
BEGIN
    
	DECLARE var_controle_commit TINYINT DEFAULT TRUE;		-- Cria uma var_controle_commit que vai ser usada para check se o commit pode ser realizado, ou dar rollback em um erro.
	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET var_controle_commit = FALSE;		-- Caso haja um erro na execução, a var_controle_commit receberá FALSE para acionar o rollback.
    
    SET @@AUTOCOMMIT = OFF;	-- Desliga o commit automatico para que o "Try, Catch e rollback" possa funcionar corretamente e commitar manualmente.
    
    IF ( (var_codigo IS NULL) OR (var_nome_usuario IS NULL) OR (var_senha IS NULL) OR (var_cod_Usuario IS NULL) ) THEN
		SELECT CONCAT('A informação está NULL ou Inválida. \r\n ', 
        '\r\n CAMPO Código: ( ', IFNULL(var_codigo, 'NULL'), ' )',
        '\r\n CAMPO Nome Usuário: ( ', IFNULL(var_nome_usuario, 'NULL'), ' )',
        '\r\n CAMPO Senha: ( ', IFNULL(var_senha, 'NULL'), ' )',
        '\r\n CAMPO Código Pessoa: ( ', IFNULL(var_cod_Usuario, 'NULL'), ' )'
        ) AS 'ALERTA';
            
    ELSE
    
		IF ( (var_operacao = 'CADASTRO') OR (var_operacao = 'ALTERAÇÃO') OR (var_operacao = 'INATIVAÇÃO') ) THEN			-- OR (var_operacao = 'ATUALIZAÇÃO') para os que permitem.
        
-- -----CADASTRO ------------------------------------------------------------------------------------------------------------------------------------------------
			IF (var_operacao = 'CADASTRO') THEN
				
                IF( var_cod_Usuario > 0 ) THEN
					
					START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
					
					INSERT INTO tbl_Login
					(nome_usuario, senha, cod_Usuario)
					VALUES
					(var_nome_usuario, var_senha, var_cod_Usuario);
					
                    SELECT MAX(l.codigo)
                    INTO var_codigo
                    FROM tbl_Login AS l LIMIT 1;
                    
					-- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
					IF ( var_controle_commit ) THEN
						COMMIT;
                        SELECT var_codigo AS 'SUCESSO';
					ELSE
						ROLLBACK;
						SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
					END IF;
					
                ELSE
					SELECT 'A operação requisitada não pode ser realizada pois o Código do USUARIO informado não é válido.' AS 'ALERTA';
				END IF;
		
-- -----ALTERAÇÃO OU INATIVAÇÃO ------------------------------------------------------------------------------------------------------------------------------------------------
			ELSEIF (var_operacao = 'ALTERAÇÃO') THEN
				
				IF( var_codigo > 0 ) THEN
                
					IF( var_cod_Usuario > 0 ) THEN
						
						START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
                        
						UPDATE tbl_Login
						SET
						nome_usuario = var_nome_usuario,
						senha = var_senha
						WHERE
						codigo = var_codigo;
						
						-- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
						IF ( var_controle_commit ) THEN
							COMMIT;
                            SELECT var_codigo AS 'SUCESSO';
						ELSE
							ROLLBACK;
							SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
						END IF;
					
					ELSE
						SELECT 'A operação requisitada não pode ser realizada pois o Código do USUARIO informado não é válido.' AS 'ALERTA';
					END IF;
                
                ELSE
					SELECT 'A operação requisitada não pode ser realizada pois o Código informado não é válido.' AS 'ALERTA';
				END IF;

-- -----INATIVAÇÃO ------------------------------------------------------------------------------------------------------------------------------------------------
			ELSEIF (var_operacao = 'INATIVAÇÃO') THEN
				
                IF( var_codigo > 0 ) THEN
					
                    IF( var_cod_Usuario > 0 ) THEN
						
						START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
                        
						DELETE FROM tbl_Login
						WHERE
						codigo = var_codigo AND cod_Usuario = var_cod_Usuario;
						
						-- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
						IF ( var_controle_commit ) THEN
							COMMIT;
                            SELECT var_codigo AS 'SUCESSO';
						ELSE
							ROLLBACK;
							SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
						END IF;
                        
					ELSE
						SELECT 'A operação requisitada não pode ser realizada pois o Código do USUARIO informado não é válido.' AS 'ALERTA';
					END IF;
                    
				ELSE
					SELECT 'A operação requisitada não pode ser realizada pois o Código informado não é válido.' AS 'ALERTA';
				END IF;
                
-- -----CASO NÃO TENHA INSERIDO NENHUMA OPERAÇÃO ----------------------------------------------------------------------------------------------------------------
			ELSE
				SELECT CONCAT('A operação ( ', IFNULL(var_operacao, 'NULL'), ' ) não é VÁLIDA.') AS 'ALERTA';
			END IF;
        
        ELSE
            SELECT CONCAT( 'A operação informada não é válida: ( ', IFNULL(var_operacao, 'NULL'), ' ).' ) AS 'ALERTA';
		END IF;
		
	END IF;
    
END $$
DELIMITER ;

-- ENDERECO -------------------------------------------------------------------------------------------------------------------------------------------------------------
DELIMITER $$
CREATE PROCEDURE sp_ManterEndereco(
	IN var_operacao CHAR(11), -- CADASTRO, ALTERAÇÃO, INATIVAÇÃO, ATUALIZAÇÃO(caso possua)
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
	DECLARE var_resgistro_LOG CHAR(32);
	DECLARE var_informacoes_registro VARCHAR(1000);
    
	DECLARE var_controle_commit TINYINT DEFAULT TRUE;		-- Cria uma var_controle_commit que vai ser usada para check se o commit pode ser realizado, ou dar rollback em um erro.
	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET var_controle_commit = FALSE;		-- Caso haja um erro na execução, a var_controle_commit receberá FALSE para acionar o rollback.
    
    SET var_resgistro_LOG = 'ENDEREÇO';
    SET @@AUTOCOMMIT = OFF;	-- Desliga o commit automatico para que o "Try, Catch e rollback" possa funcionar corretamente e commitar manualmente.
    
    IF ( (var_codigo IS NULL) OR (var_endereco IS NULL) OR (var_complemento IS NULL) OR (var_numero IS NULL) OR (var_bairro IS NULL) OR (var_cidade IS NULL) OR (var_estado IS NULL) OR (var_cep IS NULL) OR (var_observacao IS NULL) OR (var_ativo_inativo IS NULL) OR (var_cod_Pessoa IS NULL) ) THEN
		SELECT CONCAT('A informação está NULL ou Inválida. \r\n ', 
        '\r\n CAMPO Código: ( ', IFNULL(var_codigo, 'NULL'), ' )',
        '\r\n CAMPO Endereco: ( ', IFNULL(var_endereco, 'NULL'), ' )',
        '\r\n CAMPO Complemento: ( ', IFNULL(var_complemento, 'NULL'), ' )',
        '\r\n CAMPO Número: ( ', IFNULL(var_numero, 'NULL'), ' )',
        '\r\n CAMPO Bairro: ( ', IFNULL(var_bairro, 'NULL'), ' )',
        '\r\n CAMPO Cidade: ( ', IFNULL(var_cidade, 'NULL'), ' )',
        '\r\n CAMPO Estado: ( ', IFNULL(var_estado, 'NULL'), ' )',
        '\r\n CAMPO CEP: ( ', IFNULL(var_cep, 'NULL'), ' )',
        '\r\n CAMPO Observação: ( ', IFNULL(var_observacao, 'NULL'), ' )',
        '\r\n CAMPO Registro Ativo: ( ', IFNULL(var_ativo_inativo, 'NULL'), ' )',
        '\r\n \r\n CAMPO Código Pessoa: ( ', IFNULL(var_cod_Pessoa, 'NULL'), ' )'
        ) AS 'ALERTA';
	
    ELSE
    
        IF ( (var_operacao = 'CADASTRO') OR (var_operacao = 'ALTERAÇÃO') OR (var_operacao = 'INATIVAÇÃO') ) THEN
        
-- -----CADASTRO ------------------------------------------------------------------------------------------------------------------------------------------------
			IF (var_operacao = 'CADASTRO') THEN
            
                IF (var_cod_Pessoa > 0) THEN
                
					SET var_ativo_inativo = TRUE;
                
					START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
                
					INSERT INTO tbl_Endereco
					(endereco, complemento, numero, bairro, cidade, estado, cep, observacao, ativo_inativo, cod_Pessoa)
					VALUES
					(var_endereco, var_complemento, var_numero, var_bairro, var_cidade, var_estado, var_cep, var_observacao, var_ativo_inativo, var_cod_Pessoa);
                    
					SELECT MAX(e.codigo)
					INTO var_codigo
					FROM tbl_Endereco AS e LIMIT 1;
                    
					SET var_informacoes_registro = CONCAT('Código: ( ', IFNULL(var_codigo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
					'Endereco: ( ', IFNULL(var_endereco, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
					'Complemento: ( ', IFNULL(var_complemento, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
					'Número: ( ', IFNULL(var_numero, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
					'Bairro: ( ', IFNULL(var_bairro, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
					'Cidade: ( ', IFNULL(var_cidade, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
					'Estado: ( ', IFNULL(var_estado, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
					'CEP: ( ', IFNULL(var_cep, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
					'Observação: ( ', IFNULL(var_observacao, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
					'Registro Ativo: ( ', IFNULL(var_ativo_inativo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n \r\n',
					'Código Pessoa: ( ', IFNULL(var_cod_Pessoa, 'ERROR: INFORMAÇÃO NULL'), ' ).');
                    
					CALL sp_GerarLOG(var_operacao, var_resgistro_LOG, var_informacoes_registro);
                
					-- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
					IF ( var_controle_commit ) THEN
						COMMIT;
						SELECT var_codigo AS 'SUCESSO';
					ELSE
						ROLLBACK;
						SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
					END IF;
				
                ELSE
					SELECT 'A operação requisitada não pode ser realizada pois o Código da PESSOA informado não é válido.' AS 'ALERTA';
                END IF;
		
-- -----ALTERAÇÃO OU INATIVAÇÃO ------------------------------------------------------------------------------------------------------------------------------------------------
			ELSEIF ( (var_operacao = 'ALTERAÇÃO') OR (var_operacao = 'INATIVAÇÃO') ) THEN
				
                IF( var_ativo_inativo ) THEN
					
                    IF (var_operacao = 'INATIVAÇÃO') THEN
						SET var_ativo_inativo = FALSE;
					END IF;
                
                    IF( var_codigo > 0 ) THEN
                    
                        IF (var_cod_Pessoa > 0) THEN
                        
							START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
                        
							UPDATE tbl_Endereco
							SET
							endereco = var_endereco,
							complemento = var_complemento,
							numero = var_numero,
							bairro = var_bairro,
							cidade = var_cidade,
							estado = var_estado,
							cep = var_cep,
							observacao = var_observacao,
							ativo_inativo = var_ativo_inativo
							WHERE
							codigo = var_codigo AND cod_Pessoa = var_cod_Pessoa AND ativo_inativo = TRUE;
                            
							SET var_informacoes_registro = CONCAT('Código: ( ', IFNULL(var_codigo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
							'Endereco: ( ', IFNULL(var_endereco, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
							'Complemento: ( ', IFNULL(var_complemento, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
							'Número: ( ', IFNULL(var_numero, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
							'Bairro: ( ', IFNULL(var_bairro, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
							'Cidade: ( ', IFNULL(var_cidade, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
							'Estado: ( ', IFNULL(var_estado, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
							'CEP: ( ', IFNULL(var_cep, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
							'Observação: ( ', IFNULL(var_observacao, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
							'Registro Ativo: ( ', IFNULL(var_ativo_inativo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n \r\n',
							'Código Pessoa: ( ', IFNULL(var_cod_Pessoa, 'ERROR: INFORMAÇÃO NULL'), ' ).');
                            
							CALL sp_GerarLOG(var_operacao, var_resgistro_LOG, var_informacoes_registro);
                        
							-- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
							IF ( var_controle_commit ) THEN
								COMMIT;
								SELECT var_codigo AS 'SUCESSO';
							ELSE
								ROLLBACK;
								SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
							END IF;
						
                        ELSE
                            SELECT 'A operação requisitada não pode ser realizada pois o Código da PESSOA informado não é válido.' AS 'ALERTA';
						END IF;
                
                    ELSE
						SELECT 'A operação requisitada não pode ser realizada pois o Código informado não é válido.' AS 'ALERTA';
					END IF;
                    
				ELSE
					SELECT 'A operação requisitada não pode ser realizada pois o registro não está mais ativo.' AS 'ALERTA';
				END IF;
                
-- -----CASO NÃO TENHA INSERIDO NENHUMA OPERAÇÃO ----------------------------------------------------------------------------------------------------------------
			ELSE
				SELECT CONCAT('A operação ( ', IFNULL(var_operacao, 'NULL'), ' ) não é VÁLIDA.') AS 'ALERTA';
			END IF;
        
        ELSE
            SELECT CONCAT( 'A operação informada não é válida: ( ', IFNULL(var_operacao, 'NULL'), ' ).' ) AS 'ALERTA';
		END IF;
		
	END IF;
    
END $$
DELIMITER ;

-- TELEFONE -------------------------------------------------------------------------------------------------------------------------------------------------------------
DELIMITER $$
CREATE PROCEDURE sp_ManterTelefone(
	IN var_operacao CHAR(11), -- CADASTRO, ALTERAÇÃO, INATIVAÇÃO, ATUALIZAÇÃO(caso possua)
	IN var_codigo INT,
    IN var_ddd CHAR(2),
    IN var_numero CHAR(11),
    IN var_observacao VARCHAR(150),
    IN var_ativo_inativo BOOLEAN,
    IN var_cod_Pessoa INT
)
BEGIN
	DECLARE var_resgistro_LOG CHAR(32);
	DECLARE var_informacoes_registro VARCHAR(1000);
    
	DECLARE var_controle_commit TINYINT DEFAULT TRUE;		-- Cria uma var_controle_commit que vai ser usada para check se o commit pode ser realizado, ou dar rollback em um erro.
	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET var_controle_commit = FALSE;		-- Caso haja um erro na execução, a var_controle_commit receberá FALSE para acionar o rollback.
    
    SET var_resgistro_LOG = 'TELEFONE';
    SET @@AUTOCOMMIT = OFF;	-- Desliga o commit automatico para que o "Try, Catch e rollback" possa funcionar corretamente e commitar manualmente.
    
    IF ( (var_codigo IS NULL) OR (var_ddd IS NULL) OR (var_numero IS NULL) OR (var_observacao IS NULL) OR (var_ativo_inativo IS NULL) OR (var_cod_Pessoa IS NULL) ) THEN
		SELECT CONCAT('A informação está NULL ou Inválida. \r\n ', 
        '\r\n CAMPO Código: ( ', IFNULL(var_codigo, 'NULL'), ' )',
        '\r\n CAMPO DDD: ( ', IFNULL(var_ddd, 'NULL'), ' )',
        '\r\n CAMPO Número: ( ', IFNULL(var_numero, 'NULL'), ' )',
        '\r\n CAMPO Observação: ( ', IFNULL(var_observacao, 'NULL'), ' )',
        '\r\n CAMPO Registro Ativo: ( ', IFNULL(var_ativo_inativo, 'NULL'), ' )',
        '\r\n \r\n CAMPO Código Pessoa: ( ', IFNULL(var_cod_Pessoa, 'NULL'), ' )'
        ) AS 'ALERTA';
	
    ELSE
    
        IF ( (var_operacao = 'CADASTRO') OR (var_operacao = 'ALTERAÇÃO') OR (var_operacao = 'INATIVAÇÃO') ) THEN
        
-- -----CADASTRO ------------------------------------------------------------------------------------------------------------------------------------------------
			IF (var_operacao = 'CADASTRO') THEN
            
                IF (var_cod_Pessoa > 0) THEN
                
					SET var_ativo_inativo = TRUE;
                
					START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
                
					INSERT INTO tbl_Telefone
					(ddd, numero, observacao, ativo_inativo, cod_Pessoa)
					VALUES
					(var_ddd, var_numero, var_observacao, var_ativo_inativo, var_cod_Pessoa);
                    
					SELECT MAX(t.codigo)
					INTO var_codigo
					FROM tbl_Telefone AS t LIMIT 1;
                    
					SET var_informacoes_registro = CONCAT('Código: ( ', IFNULL(var_codigo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
					'DDD: ( ', IFNULL(var_ddd, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                    'Número: ( ', IFNULL(var_numero, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
					'Observação: ( ', IFNULL(var_observacao, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
					'Registro Ativo: ( ', IFNULL(var_ativo_inativo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n \r\n',
					'Código Pessoa: ( ', IFNULL(var_cod_Pessoa, 'ERROR: INFORMAÇÃO NULL'), ' ).');
                    
					CALL sp_GerarLOG(var_operacao, var_resgistro_LOG, var_informacoes_registro);
                
					-- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
					IF ( var_controle_commit ) THEN
						COMMIT;
						SELECT var_codigo AS 'SUCESSO';
					ELSE
						ROLLBACK;
						SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
					END IF;
				
                ELSE
					SELECT 'A operação requisitada não pode ser realizada pois o Código da PESSOA informado não é válido.' AS 'ALERTA';
                END IF;
		
-- -----ALTERAÇÃO OU INATIVAÇÃO ------------------------------------------------------------------------------------------------------------------------------------------------
			ELSEIF ( (var_operacao = 'ALTERAÇÃO') OR (var_operacao = 'INATIVAÇÃO')) THEN
				
                IF( var_ativo_inativo ) THEN
					
                    IF (var_operacao = 'INATIVAÇÃO') THEN
						SET var_ativo_inativo = FALSE;
					END IF;
                
                    IF( var_codigo > 0 ) THEN
                    
                        IF (var_cod_Pessoa > 0) THEN
                        
							START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
                        
							UPDATE tbl_Telefone
							SET
							ddd = var_ddd,
							numero = var_numero,
							observacao = var_observacao,
							ativo_inativo = var_ativo_inativo
							WHERE
							codigo = var_codigo AND cod_Pessoa = var_cod_Pessoa AND ativo_inativo = TRUE;
                            
							SET var_informacoes_registro = CONCAT('Código: ( ', IFNULL(var_codigo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                            'DDD: ( ', IFNULL(var_ddd, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                            'Número: ( ', IFNULL(var_numero, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                            'Observação: ( ', IFNULL(var_observacao, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                            'Registro Ativo: ( ', IFNULL(var_ativo_inativo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n \r\n',
                            'Código Pessoa: ( ', IFNULL(var_cod_Pessoa, 'ERROR: INFORMAÇÃO NULL'), ' ).');
                            
							CALL sp_GerarLOG(var_operacao, var_resgistro_LOG, var_informacoes_registro);
                        
							-- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
							IF ( var_controle_commit ) THEN
								COMMIT;
								SELECT var_codigo AS 'SUCESSO';
							ELSE
								ROLLBACK;
								SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
							END IF;
						
                        ELSE
                            SELECT 'A operação requisitada não pode ser realizada pois o Código da PESSOA informado não é válido.' AS 'ALERTA';
						END IF;
                
                    ELSE
						SELECT 'A operação requisitada não pode ser realizada pois o Código informado não é válido.' AS 'ALERTA';
					END IF;
                    
				ELSE
					SELECT 'A operação requisitada não pode ser realizada pois o registro não está mais ativo.' AS 'ALERTA';
				END IF;

-- -----INATIVAÇÃO ------------------------------------------------------------------------------------------------------------------------------------------------
			ELSEIF (var_operacao = 'INATIVAÇÃO') THEN
            
				IF(var_ativo_inativo = TRUE) THEN
                
                    IF( var_codigo > 0 ) THEN
                    
                        IF (var_cod_Pessoa > 0) THEN
                        
							SET var_ativo_inativo = FALSE;
                        
							START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
                        
							UPDATE tbl_Telefone
							SET
							ativo_inativo = var_ativo_inativo
							WHERE
							codigo = var_codigo AND cod_Pessoa = var_cod_Pessoa AND ativo_inativo = TRUE;
                            
							SET var_informacoes_registro = CONCAT('Código: ( ', IFNULL(var_codigo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                            'DDD: ( ', IFNULL(var_ddd, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                            'Número: ( ', IFNULL(var_numero, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                            'Observação: ( ', IFNULL(var_observacao, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                            'Registro Ativo: ( ', IFNULL(var_ativo_inativo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n \r\n',
                            'Código Pessoa: ( ', IFNULL(var_cod_Pessoa, 'ERROR: INFORMAÇÃO NULL'), ' ).');
                            
							CALL sp_GerarLOG(var_operacao, var_resgistro_LOG, var_informacoes_registro);
                        
							-- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
							IF ( var_controle_commit ) THEN
								COMMIT;
								SELECT var_codigo AS 'SUCESSO';
							ELSE
								ROLLBACK;
								SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
							END IF;
                        
                        ELSE
                            SELECT 'A operação requisitada não pode ser realizada pois o Código da PESSOA informado não é válido.' AS 'ALERTA';
						END IF;
                        
                    ELSE
						SELECT 'A operação requisitada não pode ser realizada pois o Código informado não é válido.' AS 'ALERTA';
					END IF;
                    
                ELSE
					SELECT 'A operação requisitada não pode ser realizada pois o registro não está mais ativo.' AS 'ALERTA';
				END IF;
		
-- -----CASO NÃO TENHA INSERIDO NENHUMA OPERAÇÃO ----------------------------------------------------------------------------------------------------------------
			ELSE
				SELECT CONCAT('A operação ( ', IFNULL(var_operacao, 'NULL'), ' ) não é VÁLIDA.') AS 'ALERTA';
			END IF;
        
        ELSE
            SELECT CONCAT( 'A operação informada não é válida: ( ', IFNULL(var_operacao, 'NULL'), ' ).' ) AS 'ALERTA';
		END IF;
		
	END IF;
    
END $$
DELIMITER ;

-- GRUPO -------------------------------------------------------------------------------------------------------------------------------------------------------------
DELIMITER $$
CREATE PROCEDURE sp_ManterGrupo(
	IN var_operacao CHAR(11), -- CADASTRO, ALTERAÇÃO, INATIVAÇÃO, ATUALIZAÇÃO(caso possua)
	IN var_codigo INT,
    IN var_descricao VARCHAR(150),
    IN var_material_ou_produto CHAR(1), -- M - Material | P - Produto
    IN var_ativo_inativo BOOLEAN
)
BEGIN
	DECLARE var_resgistro_LOG CHAR(32);
	DECLARE var_informacoes_registro VARCHAR(1000);
    
	DECLARE var_controle_commit TINYINT DEFAULT TRUE;		-- Cria uma var_controle_commit que vai ser usada para check se o commit pode ser realizado, ou dar rollback em um erro.
	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET var_controle_commit = FALSE;		-- Caso haja um erro na execução, a var_controle_commit receberá FALSE para acionar o rollback.
    
    SET var_resgistro_LOG = 'GRUPO';
    SET @@AUTOCOMMIT = OFF;	-- Desliga o commit automatico para que o "Try, Catch e rollback" possa funcionar corretamente e commitar manualmente.
    
    IF ( (var_codigo IS NULL)OR (var_descricao IS NULL) OR (var_material_ou_produto IS NULL)  OR (var_ativo_inativo IS NULL) ) THEN
		SELECT CONCAT('A informação está NULL ou Inválida. \r\n ', 
        '\r\n CAMPO Código: ( ', IFNULL(var_codigo, 'NULL'), ' )',
        '\r\n CAMPO Descrição: ( ', IFNULL(var_descricao, 'NULL'), ' )',
        '\r\n CAMPO Material ou Produto: ( ', IFNULL(var_material_ou_produto, 'NULL'), ' )',
        '\r\n CAMPO Registro Ativo: ( ', IFNULL(var_ativo_inativo, 'NULL'), ' )'
        ) AS 'ALERTA';
            
    ELSE
    
        IF ( (var_operacao = 'CADASTRO') OR (var_operacao = 'ALTERAÇÃO') OR (var_operacao = 'INATIVAÇÃO') ) THEN
        
-- -----CADASTRO ------------------------------------------------------------------------------------------------------------------------------------------------
			IF (var_operacao = 'CADASTRO') THEN
            
                SET var_ativo_inativo = TRUE;
                
                START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
				
                INSERT INTO tbl_Grupo
				(descricao, material_ou_produto, ativo_inativo)
				VALUES
				(var_descricao, var_material_ou_produto, var_ativo_inativo);
                
				SELECT MAX(g.codigo)
                INTO var_codigo
				FROM tbl_Grupo AS g LIMIT 1;
                
				SET var_informacoes_registro = CONCAT('Código: ( ', IFNULL(var_codigo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                'Descrição: ( ', IFNULL(var_descricao, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                'Material ou Produto: ( ', IFNULL(var_material_ou_produto, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                'Registro Ativo: ( ', IFNULL(var_ativo_inativo, 'ERROR: INFORMAÇÃO NULL'), ' ).');
                
				CALL sp_GerarLOG(var_operacao, var_resgistro_LOG, var_informacoes_registro);
                
                -- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
                IF ( var_controle_commit ) THEN
					COMMIT;
                    SELECT var_codigo AS 'SUCESSO';
				ELSE
					ROLLBACK;
                    SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
				END IF;
		
-- -----ALTERAÇÃO OU INATIVAÇÃO ------------------------------------------------------------------------------------------------------------------------------------------------
			ELSEIF ( (var_operacao = 'ALTERAÇÃO') OR (var_operacao = 'INATIVAÇÃO') ) THEN
				
                IF( var_ativo_inativo ) THEN
					
                    IF (var_operacao = 'INATIVAÇÃO') THEN
						SET var_ativo_inativo = FALSE;
					END IF;
                
                    IF( var_codigo > 0 ) THEN
						
                        START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
                        
                        UPDATE tbl_Grupo
                        SET
                        descricao = var_descricao,
                        material_ou_produto = var_material_ou_produto,
						ativo_inativo = var_ativo_inativo
                        WHERE
                        codigo = var_codigo AND ativo_inativo = TRUE;
                        
                        SET var_informacoes_registro = CONCAT('Código: ( ', IFNULL(var_codigo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                        'Descrição: ( ', IFNULL(var_descricao, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                        'Material ou Produto: ( ', IFNULL(var_material_ou_produto, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                        'Registro Ativo: ( ', IFNULL(var_ativo_inativo, 'ERROR: INFORMAÇÃO NULL'), ' ).');
                        
                        CALL sp_GerarLOG(var_operacao, var_resgistro_LOG, var_informacoes_registro);
                        
                        -- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
                        IF ( var_controle_commit ) THEN
							COMMIT;
                            SELECT var_codigo AS 'SUCESSO';
						ELSE
							ROLLBACK;
                            SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
						END IF;
                
                    ELSE
						SELECT 'A operação requisitada não pode ser realizada pois o Código informado não é válido.' AS 'ALERTA';
					END IF;
                    
				ELSE
					SELECT 'A operação requisitada não pode ser realizada pois o registro não está mais ativo.' AS 'ALERTA';
				END IF;
		
-- -----CASO NÃO TENHA INSERIDO NENHUMA OPERAÇÃO ----------------------------------------------------------------------------------------------------------------
			ELSE
				SELECT CONCAT('A operação ( ', IFNULL(var_operacao, 'NULL'), ' ) não é VÁLIDA.') AS 'ALERTA';
			END IF;
        
        ELSE
            SELECT CONCAT( 'A operação informada não é válida: ( ', IFNULL(var_operacao, 'NULL'), ' ).' ) AS 'ALERTA';
		END IF;
		
	END IF;
    
END $$
DELIMITER ;

-- UNIDADE -------------------------------------------------------------------------------------------------------------------------------------------------------------
DELIMITER $$
CREATE PROCEDURE sp_ManterUnidade(
	IN var_operacao CHAR(11), -- CADASTRO, ALTERAÇÃO, INATIVAÇÃO, ATUALIZAÇÃO(caso possua)
	IN var_codigo INT,
    IN var_sigla CHAR(8),
    IN var_descricao VARCHAR(150),
    IN var_ativo_inativo BOOLEAN
)
BEGIN
	DECLARE var_resgistro_LOG CHAR(32);
	DECLARE var_informacoes_registro VARCHAR(1000);
    
	DECLARE var_controle_commit TINYINT DEFAULT TRUE;		-- Cria uma var_controle_commit que vai ser usada para check se o commit pode ser realizado, ou dar rollback em um erro.
	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET var_controle_commit = FALSE;		-- Caso haja um erro na execução, a var_controle_commit receberá FALSE para acionar o rollback.
    
    SET var_resgistro_LOG = 'UNIDADE';
    SET @@AUTOCOMMIT = OFF;	-- Desliga o commit automatico para que o "Try, Catch e rollback" possa funcionar corretamente e commitar manualmente.
    
    IF ( (var_codigo IS NULL) OR (var_sigla IS NULL) OR (var_descricao IS NULL) OR (var_ativo_inativo IS NULL) ) THEN
		SELECT CONCAT('A informação está NULL ou Inválida. \r\n ', 
        '\r\n CAMPO Código: ( ', IFNULL(var_codigo, 'NULL'), ' )',
        '\r\n CAMPO Sigla: ( ', IFNULL(var_sigla, 'NULL'), ' )',
        '\r\n CAMPO Descrição: ( ', IFNULL(var_descricao, 'NULL'), ' )',
        '\r\n CAMPO Registro Ativo: ( ', IFNULL(var_ativo_inativo, 'NULL'), ' )'
        ) AS 'ALERTA';
            
    ELSE
    
        IF ( (var_operacao = 'CADASTRO') OR (var_operacao = 'ALTERAÇÃO') OR (var_operacao = 'INATIVAÇÃO') ) THEN
        
-- -----CADASTRO ------------------------------------------------------------------------------------------------------------------------------------------------
			IF (var_operacao = 'CADASTRO') THEN
            
                SET var_ativo_inativo = TRUE;
                
                START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
				
                INSERT INTO tbl_Unidade
				(sigla, descricao, ativo_inativo)
				VALUES
				(var_sigla, var_descricao, var_ativo_inativo);
                
				SELECT MAX(uni.codigo)
                INTO var_codigo
				FROM tbl_Unidade AS uni LIMIT 1;
                
				SET var_informacoes_registro = CONCAT('Código: ( ', IFNULL(var_codigo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                'Sigla: ( ', IFNULL(var_sigla, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                'Descrição: ( ', IFNULL(var_descricao, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                'Registro Ativo: ( ', IFNULL(var_ativo_inativo, 'ERROR: INFORMAÇÃO NULL'), ' ).');
                
				CALL sp_GerarLOG(var_operacao, var_resgistro_LOG, var_informacoes_registro);
                
                -- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
                IF ( var_controle_commit ) THEN
					COMMIT;
                    SELECT var_codigo AS 'SUCESSO';
				ELSE
					ROLLBACK;
                    SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
				END IF;
		
-- -----ALTERAÇÃO OU INATIVAÇÃO ------------------------------------------------------------------------------------------------------------------------------------------------
			ELSEIF ( (var_operacao = 'ALTERAÇÃO') OR (var_operacao = 'INATIVAÇÃO') ) THEN
				
                IF( var_ativo_inativo ) THEN
					
                    IF (var_operacao = 'INATIVAÇÃO') THEN
						SET var_ativo_inativo = FALSE;
					END IF;
                
                    IF( var_codigo > 0 ) THEN
						
                        START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
                        
                        UPDATE tbl_Unidade
                        SET
                        sigla = var_sigla,
                        descricao = var_descricao,
						ativo_inativo = var_ativo_inativo
                        WHERE
                        codigo = var_codigo AND ativo_inativo = TRUE;
                        
                        SET var_informacoes_registro = CONCAT('Código: ( ', IFNULL(var_codigo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                        'Sigla: ( ', IFNULL(var_sigla, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                        'Descrição: ( ', IFNULL(var_descricao, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                        'Registro Ativo: ( ', IFNULL(var_ativo_inativo, 'ERROR: INFORMAÇÃO NULL'), ' ).');
                        
                        CALL sp_GerarLOG(var_operacao, var_resgistro_LOG, var_informacoes_registro);
                        
                        -- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
                        IF ( var_controle_commit ) THEN
							COMMIT;
                            SELECT var_codigo AS 'SUCESSO';
						ELSE
							ROLLBACK;
                            SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
						END IF;
                
                    ELSE
						SELECT 'A operação requisitada não pode ser realizada pois o Código informado não é válido.' AS 'ALERTA';
					END IF;
                    
				ELSE
					SELECT 'A operação requisitada não pode ser realizada pois o registro não está mais ativo.' AS 'ALERTA';
				END IF;
		
-- -----CASO NÃO TENHA INSERIDO NENHUMA OPERAÇÃO ----------------------------------------------------------------------------------------------------------------
			ELSE
				SELECT CONCAT('A operação ( ', IFNULL(var_operacao, 'NULL'), ' ) não é VÁLIDA.') AS 'ALERTA';
			END IF;
        
        ELSE
            SELECT CONCAT( 'A operação informada não é válida: ( ', IFNULL(var_operacao, 'NULL'), ' ).' ) AS 'ALERTA';
		END IF;
		
	END IF;
    
END $$
DELIMITER ;

-- MATERIAL -------------------------------------------------------------------------------------------------------------------------------------------------------------
DELIMITER $$
CREATE PROCEDURE sp_ManterMaterial(
	IN var_operacao CHAR(11), -- CADASTRO, ALTERAÇÃO, INATIVAÇÃO, ATUALIZAÇÃO(caso possua)
    IN var_codigo INT,
    IN var_imagem VARCHAR(260),
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
	DECLARE var_resgistro_LOG CHAR(32);
	DECLARE var_informacoes_registro VARCHAR(1000);
    
    DECLARE var_ultima_atualizacao DATETIME; -- Variavel de Tempo que o sistema irá gerenciar sua entrada de dados.
    
	DECLARE var_controle_commit TINYINT DEFAULT TRUE;		-- Cria uma var_controle_commit que vai ser usada para check se o commit pode ser realizado, ou dar rollback em um erro.
	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET var_controle_commit = FALSE;		-- Caso haja um erro na execução, a var_controle_commit receberá FALSE para acionar o rollback.
    
    SET var_resgistro_LOG = 'MATERIAL';
    SET @@AUTOCOMMIT = OFF;	-- Desliga o commit automatico para que o "Try, Catch e rollback" possa funcionar corretamente e commitar manualmente.
    
    IF ( (var_codigo IS NULL) OR (var_imagem IS NULL) OR (var_descricao IS NULL) OR (var_altura IS NULL) OR (var_largura IS NULL) OR (var_comprimento IS NULL) OR (var_valor_unitario IS NULL) OR (var_ativo_inativo IS NULL) OR (var_cod_Unidade IS NULL) OR (var_cod_Grupo IS NULL) OR (var_cod_Fornecedor IS NULL) ) THEN
		SELECT CONCAT('A informação está NULL ou Inválida. \r\n ', 
        '\r\n CAMPO Código: ( ', IFNULL(var_codigo, 'NULL'), ' )',
        '\r\n CAMPO Imagem: ( ', IFNULL(var_imagem, 'NULL'), ' )',
        '\r\n CAMPO Descrição: ( ', IFNULL(var_descricao, 'NULL'), ' )',
        '\r\n CAMPO Altura: ( ', IFNULL(var_altura, 'NULL'), ' )',
        '\r\n CAMPO Largura: ( ', IFNULL(var_largura, 'NULL'), ' )',
        '\r\n CAMPO Comprimento: ( ', IFNULL(var_comprimento, 'NULL'), ' )',
        '\r\n CAMPO Valor Unitário: ( ', IFNULL(var_valor_unitario, 'NULL'), ' )',
        '\r\n CAMPO Registro Ativo: ( ', IFNULL(var_ativo_inativo, 'NULL'), ' )',
        '\r\n CAMPO Código Unidade: ( ', IFNULL(var_cod_Unidade, 'NULL'), ' )',
        '\r\n CAMPO Código Grupo: ( ', IFNULL(var_cod_Grupo, 'NULL'), ' )',
        '\r\n \r\n CAMPO Código Fornecedor: ( ', IFNULL(var_cod_Fornecedor, 'NULL'), ' )'
        ) AS 'ALERTA';
	
    ELSE
    
        SET var_ultima_atualizacao = localtimestamp(0);
        
        IF ( (var_operacao = 'CADASTRO') OR (var_operacao = 'ALTERAÇÃO') OR (var_operacao = 'INATIVAÇÃO') ) THEN
        
-- -----CADASTRO ------------------------------------------------------------------------------------------------------------------------------------------------
			IF (var_operacao = 'CADASTRO') THEN
            
                IF (var_cod_Unidade > 0) THEN
                
                    IF (var_cod_Grupo > 0) THEN
                    
						IF (var_cod_Fornecedor > 0) THEN
                        
							SET var_ativo_inativo = TRUE;
                            
                            START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
                            
                            INSERT INTO tbl_Material
							(ultima_atualizacao, imagem, descricao, altura, largura, comprimento, valor_unitario, ativo_inativo, cod_Unidade, cod_Grupo, cod_Fornecedor)
							VALUES
							(var_ultima_atualizacao, var_imagem, var_descricao, var_altura, var_largura, var_comprimento, var_valor_unitario, var_ativo_inativo, var_cod_Unidade, var_cod_Grupo, var_cod_Fornecedor);
                            
							SELECT MAX(m.codigo)
							INTO var_codigo
							FROM tbl_Material AS m LIMIT 1;
                            
							SET var_informacoes_registro = CONCAT('Código: ( ', IFNULL(var_codigo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
							'Ultima Atualização: ( ', IFNULL(var_ultima_atualizacao, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
							'Descrição: ( ', IFNULL(var_descricao, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
							'Altura: ( ', IFNULL(var_altura, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                            'Largura: ( ', IFNULL(var_largura, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                            'Comprimento: ( ', IFNULL(var_comprimento, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                            'Valor Unitario: ( ', IFNULL(var_valor_unitario, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                            'Registro Ativo: ( ', IFNULL(var_ativo_inativo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n \r\n',
                            'Código Unidade: ( ', IFNULL(var_cod_Unidade, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                            'Código Grupo: ( ', IFNULL(var_cod_Grupo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                            'Código Fornecedor: ( ', IFNULL(var_cod_Fornecedor, 'ERROR: INFORMAÇÃO NULL'), ' ).');
                            
							CALL sp_GerarLOG(var_operacao, var_resgistro_LOG, var_informacoes_registro);
                
							-- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
							IF ( var_controle_commit ) THEN
								COMMIT;
								SELECT var_codigo AS 'SUCESSO';
							ELSE
								ROLLBACK;
								SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
							END IF;
						
                        ELSE
							SELECT 'A operação requisitada não pode ser realizada pois o Código da FORNECEDOR informado não é válido.' AS 'ALERTA';
						END IF;
                        
					ELSE
						SELECT 'A operação requisitada não pode ser realizada pois o Código da GRUPO informado não é válido.' AS 'ALERTA';
					END IF;
				
                ELSE
					SELECT 'A operação requisitada não pode ser realizada pois o Código da UNIDADE informado não é válido.' AS 'ALERTA';
                END IF;
		
-- -----ALTERAÇÃO ------------------------------------------------------------------------------------------------------------------------------------------------
			ELSEIF ( (var_operacao = 'ALTERAÇÃO') ) THEN
				
                IF( var_ativo_inativo ) THEN
					
                    IF( var_codigo > 0 ) THEN
                    
						IF (var_cod_Unidade > 0) THEN
                        
							IF (var_cod_Grupo > 0) THEN
                            
								IF (var_cod_Fornecedor > 0) THEN
                        
									START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
                                    
                                    UPDATE tbl_Material
									SET
									ultima_atualizacao = var_ultima_atualizacao,
									imagem = var_imagem,
									descricao = var_descricao,
									altura = var_altura,
									largura = var_largura,
									comprimento = var_comprimento,
									valor_unitario = var_valor_unitario,
									cod_Unidade = var_cod_Unidade,
									cod_Grupo = var_cod_Grupo,
									cod_Fornecedor = var_cod_Fornecedor
									WHERE
									codigo = var_codigo AND ativo_inativo = var_ativo_inativo;
                                    
                                    SET var_informacoes_registro = CONCAT('Código: ( ', IFNULL(var_codigo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
									'Ultima Atualização: ( ', IFNULL(var_ultima_atualizacao, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
									'Descrição: ( ', IFNULL(var_descricao, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
									'Altura: ( ', IFNULL(var_altura, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
									'Largura: ( ', IFNULL(var_largura, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
									'Comprimento: ( ', IFNULL(var_comprimento, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
									'Valor Unitario: ( ', IFNULL(var_valor_unitario, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
									'Registro Ativo: ( ', IFNULL(var_ativo_inativo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n \r\n',
									'Código Unidade: ( ', IFNULL(var_cod_Unidade, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
									'Código Grupo: ( ', IFNULL(var_cod_Grupo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
									'Código Fornecedor: ( ', IFNULL(var_cod_Fornecedor, 'ERROR: INFORMAÇÃO NULL'), ' ).');
                                    
									CALL sp_GerarLOG(var_operacao, var_resgistro_LOG, var_informacoes_registro);
                                    
                                    -- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
									IF ( var_controle_commit ) THEN
										COMMIT;
										SELECT var_codigo AS 'SUCESSO';
									ELSE
										ROLLBACK;
										SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
									END IF;
						
								ELSE
									SELECT 'A operação requisitada não pode ser realizada pois o Código da FORNECEDOR informado não é válido.' AS 'ALERTA';
								END IF;
                        
							ELSE
								SELECT 'A operação requisitada não pode ser realizada pois o Código da GRUPO informado não é válido.' AS 'ALERTA';
							END IF;
				
						ELSE
							SELECT 'A operação requisitada não pode ser realizada pois o Código da UNIDADE informado não é válido.' AS 'ALERTA';
						END IF;
                        
                    ELSE
						SELECT 'A operação requisitada não pode ser realizada pois o Código informado não é válido.' AS 'ALERTA';
					END IF;
                    
				ELSE
					SELECT 'A operação requisitada não pode ser realizada pois o registro não está mais ativo.' AS 'ALERTA';
				END IF;

-- -----INATIVAÇÃO ------------------------------------------------------------------------------------------------------------------------------------------------
			ELSEIF (var_operacao = 'INATIVAÇÃO') THEN
            
				IF(var_ativo_inativo = TRUE) THEN
                
                    IF( var_codigo > 0 ) THEN
                    
						IF (var_cod_Unidade > 0) THEN
                        
							IF (var_cod_Grupo > 0) THEN
                            
								IF (var_cod_Fornecedor > 0) THEN
                                
									SET var_ativo_inativo = FALSE;
                        
									START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
                        
									UPDATE tbl_Material
									SET
									ativo_inativo = var_ativo_inativo
									WHERE
									codigo = var_codigo AND ativo_inativo = TRUE;
                                    
									SET var_informacoes_registro = CONCAT('Código: ( ', IFNULL(var_codigo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
									'Ultima Atualização: ( ', IFNULL(var_ultima_atualizacao, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
									'Descrição: ( ', IFNULL(var_descricao, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
									'Altura: ( ', IFNULL(var_altura, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
									'Largura: ( ', IFNULL(var_largura, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
									'Comprimento: ( ', IFNULL(var_comprimento, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
									'Valor Unitario: ( ', IFNULL(var_valor_unitario, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
									'Registro Ativo: ( ', IFNULL(var_ativo_inativo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n \r\n',
									'Código Unidade: ( ', IFNULL(var_cod_Unidade, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
									'Código Grupo: ( ', IFNULL(var_cod_Grupo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
									'Código Fornecedor: ( ', IFNULL(var_cod_Fornecedor, 'ERROR: INFORMAÇÃO NULL'), ' ).');
                                    
									CALL sp_GerarLOG(var_operacao, var_resgistro_LOG, var_informacoes_registro);
                        
									-- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
									IF ( var_controle_commit ) THEN
										COMMIT;
										SELECT var_codigo AS 'SUCESSO';
									ELSE
										ROLLBACK;
										SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
									END IF;
						
								ELSE
									SELECT 'A operação requisitada não pode ser realizada pois o Código da FORNECEDOR informado não é válido.' AS 'ALERTA';
								END IF;
                        
							ELSE
								SELECT 'A operação requisitada não pode ser realizada pois o Código da GRUPO informado não é válido.' AS 'ALERTA';
							END IF;
				
						ELSE
							SELECT 'A operação requisitada não pode ser realizada pois o Código da UNIDADE informado não é válido.' AS 'ALERTA';
						END IF;
                        
                    ELSE
						SELECT 'A operação requisitada não pode ser realizada pois o Código informado não é válido.' AS 'ALERTA';
					END IF;
                    
                ELSE
					SELECT 'A operação requisitada não pode ser realizada pois o registro não está mais ativo.' AS 'ALERTA';
				END IF;
		
-- -----CASO NÃO TENHA INSERIDO NENHUMA OPERAÇÃO ----------------------------------------------------------------------------------------------------------------
			ELSE
				SELECT CONCAT('A operação ( ', IFNULL(var_operacao, 'NULL'), ' ) não é VÁLIDA.') AS 'ALERTA';
			END IF;
        
        ELSE
            SELECT CONCAT( 'A operação informada não é válida: ( ', IFNULL(var_operacao, 'NULL'), ' ).' ) AS 'ALERTA';
		END IF;
		
	END IF;
    
END $$
DELIMITER ;

-- PRODUTO OU SERVIÇO -------------------------------------------------------------------------------------------------------------------------------------------------------------
DELIMITER $$
CREATE PROCEDURE sp_ManterProduto_Servico(
	IN var_operacao CHAR(11), -- CADASTRO, ALTERAÇÃO, INATIVAÇÃO, ATUALIZAÇÃO(caso possua)
    IN var_codigo INT,
    IN var_imagem VARCHAR(260),
    IN var_descricao VARCHAR(150),
    IN var_altura DECIMAL(13,2),
    IN var_largura DECIMAL(13,2),
    IN var_comprimento DECIMAL(13,2),
    IN var_valor_total_materiais DECIMAL(13,2),
    IN var_maoObra DECIMAL(13,2),
    -- IN var_valor_maoObra DECIMAL(13,2),
    -- IN var_valor_total DECIMAL(13,2),
    IN var_ativo_inativo BOOLEAN,
    IN var_cod_Unidade INT,
    IN var_cod_Grupo INT
)
BEGIN
	DECLARE var_resgistro_LOG CHAR(32);
	DECLARE var_informacoes_registro VARCHAR(1000);
    
    DECLARE var_ultima_atualizacao DATETIME;
    DECLARE var_valor_maoObra DECIMAL(13,2);
    DECLARE var_valor_total DECIMAL(13,2);
    
	DECLARE var_controle_commit TINYINT DEFAULT TRUE;		-- Cria uma var_controle_commit que vai ser usada para check se o commit pode ser realizado, ou dar rollback em um erro.
	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET var_controle_commit = FALSE;		-- Caso haja um erro na execução, a var_controle_commit receberá FALSE para acionar o rollback.
    
    SET var_resgistro_LOG = 'PRODUTO OU SERVIÇO';
    SET @@AUTOCOMMIT = OFF;	-- Desliga o commit automatico para que o "Try, Catch e rollback" possa funcionar corretamente e commitar manualmente.
    
    IF ( (var_codigo IS NULL) OR (var_imagem IS NULL) OR (var_descricao IS NULL) OR (var_altura IS NULL) OR (var_largura IS NULL) OR (var_comprimento IS NULL) OR (var_valor_total_materiais IS NULL) OR (var_maoObra IS NULL) OR (var_ativo_inativo IS NULL) OR (var_cod_Unidade IS NULL) OR (var_cod_Grupo IS NULL) ) THEN
		SELECT CONCAT('A informação está NULL ou Inválida. \r\n ', 
        '\r\n CAMPO Código: ( ', IFNULL(var_codigo, 'NULL'), ' )',
        '\r\n CAMPO Imagem: ( ', IFNULL(var_imagem, 'NULL'), ' )',
        '\r\n CAMPO Descrição: ( ', IFNULL(var_descricao, 'NULL'), ' )',
        '\r\n CAMPO Altura: ( ', IFNULL(var_altura, 'NULL'), ' )',
        '\r\n CAMPO Largura: ( ', IFNULL(var_largura, 'NULL'), ' )',
        '\r\n CAMPO Comprimento: ( ', IFNULL(var_comprimento, 'NULL'), ' )',
        '\r\n CAMPO Valor Total Materiais: ( ', IFNULL(var_valor_total_materiais, 'NULL'), ' )',
        '\r\n CAMPO Mão de Obra: ( ', IFNULL(var_maoObra, 'NULL'), ' )',
        '\r\n CAMPO Registro Ativo: ( ', IFNULL(var_ativo_inativo, 'NULL'), ' )',
        '\r\n CAMPO Código Unidade: ( ', IFNULL(var_cod_Unidade, 'NULL'), ' )',
        '\r\n CAMPO Código Grupo: ( ', IFNULL(var_cod_Grupo, 'NULL'), ' )'
        ) AS 'ALERTA';
	
    ELSE
		
        SET var_ultima_atualizacao = localtimestamp(0);
        SET var_valor_maoObra = 0.00;
        SET var_valor_total = 0.00;
        
        IF ( (var_operacao = 'CADASTRO') OR (var_operacao = 'ALTERAÇÃO') OR (var_operacao = 'INATIVAÇÃO') ) THEN
        
-- -----CADASTRO ------------------------------------------------------------------------------------------------------------------------------------------------
			IF (var_operacao = 'CADASTRO') THEN
            
                IF (var_cod_Unidade > 0) THEN
                
                    IF (var_cod_Grupo > 0) THEN
                    
						SET var_ativo_inativo = TRUE;
                            
						START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
                        
						INSERT INTO tbl_Produto_Servico
						(ultima_atualizacao, imagem, descricao, altura, largura, comprimento, valor_total_materiais, maoObra, valor_maoObra, valor_total, ativo_inativo, cod_Unidade, cod_Grupo)
						VALUES
						(var_ultima_atualizacao, var_imagem, var_descricao, var_altura, var_largura, var_comprimento, var_valor_total_materiais, var_maoObra, var_valor_maoObra, var_valor_total, var_ativo_inativo, var_cod_Unidade, var_cod_Grupo);
                        
						SELECT MAX(ps.codigo)
						INTO var_codigo
						FROM tbl_Produto_Servico AS ps LIMIT 1;
                        
						SET var_informacoes_registro = CONCAT('Código: ( ', IFNULL(var_codigo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
						'Ultima Atualização: ( ', IFNULL(var_ultima_atualizacao, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
						'Descrição: ( ', IFNULL(var_descricao, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
						'Altura: ( ', IFNULL(var_altura, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
						'Largura: ( ', IFNULL(var_largura, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
						'Comprimento: ( ', IFNULL(var_comprimento, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
						'Valor Total Materiais: ( ', IFNULL(var_valor_total_materiais, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
						'Mão de Obra: ( ', IFNULL(var_maoObra, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
						'Valor Mão de Obra: ( ', IFNULL(var_valor_maoObra, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
						'Valor Total: ( ', IFNULL(var_valor_total, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
						'Registro Ativo: ( ', IFNULL(var_ativo_inativo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n \r\n',
						'Código Unidade: ( ', IFNULL(var_cod_Unidade, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
						'Código Grupo: ( ', IFNULL(var_cod_Grupo, 'ERROR: INFORMAÇÃO NULL'), ' ).');
                        
						CALL sp_GerarLOG(var_operacao, var_resgistro_LOG, var_informacoes_registro);
                
						-- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
						IF ( var_controle_commit ) THEN
							COMMIT;
							SELECT var_codigo AS 'SUCESSO';
						ELSE
							ROLLBACK;
							SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
						END IF;
					
                    ELSE
						SELECT 'A operação requisitada não pode ser realizada pois o Código da GRUPO informado não é válido.' AS 'ALERTA';
					END IF;
				
                ELSE
					SELECT 'A operação requisitada não pode ser realizada pois o Código da UNIDADE informado não é válido.' AS 'ALERTA';
                END IF;
		
-- -----ALTERAÇÃO OU INATIVAÇÃO ------------------------------------------------------------------------------------------------------------------------------------------------
			ELSEIF ( (var_operacao = 'ALTERAÇÃO') OR (var_operacao = 'INATIVAÇÃO') ) THEN
				
                IF( var_ativo_inativo ) THEN
					
                    IF (var_operacao = 'INATIVAÇÃO') THEN
						SET var_ativo_inativo = FALSE;
					END IF;
                
                    IF( var_codigo > 0 ) THEN
                    
						IF (var_cod_Unidade > 0) THEN
                        
							IF (var_cod_Grupo > 0) THEN
                                
                                SET var_valor_maoObra = var_valor_total_materiais * (var_maoObra / 100);
                                
                                SET var_valor_total = var_valor_total_materiais + var_valor_maoObra;
                                
								START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
                        
								UPDATE tbl_Produto_Servico
								SET
								ultima_atualizacao = var_ultima_atualizacao,
								imagem = var_imagem,
								descricao = var_descricao,
								altura = var_altura,
								largura = var_largura,
								comprimento = var_comprimento,
								valor_total_materiais = var_valor_total_materiais,  
								maoObra = var_maoObra,
								valor_maoObra = var_valor_maoObra,
                                valor_total = var_valor_total,
								cod_Unidade = var_cod_Unidade,
								cod_Grupo = var_cod_Grupo,
								ativo_inativo = var_ativo_inativo
								WHERE
								codigo = var_codigo AND ativo_inativo = TRUE;
                                
								SET var_informacoes_registro = CONCAT('Código: ( ', IFNULL(var_codigo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
								'Ultima Atualização: ( ', IFNULL(var_ultima_atualizacao, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
								'Descrição: ( ', IFNULL(var_descricao, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
								'Altura: ( ', IFNULL(var_altura, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
								'Largura: ( ', IFNULL(var_largura, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
								'Comprimento: ( ', IFNULL(var_comprimento, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
								'Valor Total Materiais: ( ', IFNULL(var_valor_total_materiais, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
								'Mão de Obra: ( ', IFNULL(var_maoObra, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
								'Valor Mão de Obra: ( ', IFNULL(var_valor_maoObra, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
								'Valor Total: ( ', IFNULL(var_valor_total, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
								'Registro Ativo: ( ', IFNULL(var_ativo_inativo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n \r\n',
								'Código Unidade: ( ', IFNULL(var_cod_Unidade, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
								'Código Grupo: ( ', IFNULL(var_cod_Grupo, 'ERROR: INFORMAÇÃO NULL'), ' ).');
                                
								CALL sp_GerarLOG(var_operacao, var_resgistro_LOG, var_informacoes_registro);
                        
								-- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
								IF ( var_controle_commit ) THEN
									COMMIT;
									SELECT var_codigo AS 'SUCESSO';
								ELSE
									ROLLBACK;
									SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
								END IF;
							
                            ELSE
								SELECT 'A operação requisitada não pode ser realizada pois o Código da GRUPO informado não é válido.' AS 'ALERTA';
							END IF;
				
						ELSE
							SELECT 'A operação requisitada não pode ser realizada pois o Código da UNIDADE informado não é válido.' AS 'ALERTA';
						END IF;
                        
                    ELSE
						SELECT 'A operação requisitada não pode ser realizada pois o Código informado não é válido.' AS 'ALERTA';
					END IF;
                    
				ELSE
					SELECT 'A operação requisitada não pode ser realizada pois o registro não está mais ativo.' AS 'ALERTA';
				END IF;
		
-- -----CASO NÃO TENHA INSERIDO NENHUMA OPERAÇÃO ----------------------------------------------------------------------------------------------------------------
			ELSE
				SELECT CONCAT('A operação ( ', IFNULL(var_operacao, 'NULL'), ' ) não é VÁLIDA.') AS 'ALERTA';
			END IF;
        
        ELSE
            SELECT CONCAT( 'A operação informada não é válida: ( ', IFNULL(var_operacao, 'NULL'), ' ).' ) AS 'ALERTA';
		END IF;
		
	END IF;
    
END $$
DELIMITER ;

-- MATERIAIS DO PRODUTO OU SERVIÇO  -------------------------------------------------------------------------------------------------------------------------------------------------------------
DELIMITER $$
CREATE PROCEDURE sp_ManterMateriais_Produto_Servico(
	IN var_operacao CHAR(11), -- CADASTRO, ALTERAÇÃO, INATIVAÇÃO, ATUALIZAÇÃO(caso possua)
    IN var_codigo INT,
    IN var_quantidade DECIMAL(13,2),
    IN var_valor_total DECIMAL(13,2),
    IN var_ativo_inativo BOOLEAN,
    IN var_cod_Material INT,
    IN var_cod_Produto_Servico INT
)
BEGIN
	DECLARE var_resgistro_LOG CHAR(32);
	DECLARE var_informacoes_registro VARCHAR(1000);
    
    DECLARE var_antigo_valor_total DECIMAL(13,2);
    DECLARE var_codigo_retornavelDesse_MPS INT;
    
    DECLARE var_imagem_Produto_Servico VARCHAR(260);
    DECLARE var_descricao_Produto_Servico VARCHAR(150);
    DECLARE var_altura_Produto_Servico DECIMAL(13,2);
    DECLARE var_largura_Produto_Servico DECIMAL(13,2);
    DECLARE var_comprimento_Produto_Servico DECIMAL(13,2);
	DECLARE var_valor_total_materiais_Produto_Servico DECIMAL(13,2);
    DECLARE var_maoObra_Produto_Servico DECIMAL(13,2);
    -- DECLARE var_valor_maoObra_Produto_Servico DECIMAL(13,2);
    -- DECLARE var_valor_total_Produto_Servico DECIMAL(13,2);
    DECLARE var_ativo_inativo_Produto_Servico BOOLEAN;
    DECLARE var_cod_Unidade_Produto_Servico INT;
    DECLARE var_cod_Grupo_Produto_Servico INT;
    
	DECLARE var_controle_commit TINYINT DEFAULT TRUE;		-- Cria uma var_controle_commit que vai ser usada para check se o commit pode ser realizado, ou dar rollback em um erro.
	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET var_controle_commit = FALSE;		-- Caso haja um erro na execução, a var_controle_commit receberá FALSE para acionar o rollback.
    
    SET var_resgistro_LOG = 'MATERIAIS DO PRODUTO OU SERVIÇO';
    SET @@AUTOCOMMIT = OFF;	-- Desliga o commit automatico para que o "Try, Catch e rollback" possa funcionar corretamente e commitar manualmente.
    
    IF ( (var_codigo IS NULL) OR (var_quantidade IS NULL) OR (var_valor_total IS NULL) OR (var_ativo_inativo IS NULL) OR (var_cod_Material IS NULL) OR (var_cod_Produto_Servico IS NULL) ) THEN
		SELECT CONCAT('A informação está NULL ou Inválida. \r\n ', 
        '\r\n CAMPO Código: ( ', IFNULL(var_codigo, 'NULL'), ' )',
        '\r\n CAMPO Quantidade: ( ', IFNULL(var_quantidade, 'NULL'), ' )',
        '\r\n CAMPO Valor Total: ( ', IFNULL(var_valor_total, 'NULL'), ' )',
        '\r\n CAMPO Registro Ativo: ( ', IFNULL(var_ativo_inativo, 'NULL'), ' )',
        '\r\n CAMPO Código Material: ( ', IFNULL(var_cod_Material, 'NULL'), ' )',
        '\r\n CAMPO Código Produto ou Serviço: ( ', IFNULL(var_cod_Produto_Servico, 'NULL'), ' )'
        ) AS 'ALERTA';
	
    ELSE
        
        IF ( (var_operacao = 'CADASTRO') OR (var_operacao = 'ALTERAÇÃO') OR (var_operacao = 'INATIVAÇÃO') ) THEN
        
-- -----CADASTRO ------------------------------------------------------------------------------------------------------------------------------------------------
			IF (var_operacao = 'CADASTRO') THEN
            
                IF (var_cod_Material > 0) THEN
                
                    IF (var_cod_Produto_Servico > 0) THEN
                    
						SET var_ativo_inativo = TRUE;
                        
						START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
                        
						INSERT INTO tbl_Materiais_Produto_Servico
						(quantidade, valor_total, ativo_inativo, cod_Material, cod_Produto_Servico)
						VALUES
						(var_quantidade, var_valor_total, var_ativo_inativo, var_cod_Material, var_cod_Produto_Servico);
                        
						SELECT MAX(mps.codigo)
						INTO var_codigo_retornavelDesse_MPS
						FROM tbl_Materiais_Produto_Servico AS mps LIMIT 1;
                        
                        SELECT
						ps.imagem,
						ps.descricao,
						ps.altura,
						ps.largura,
						ps.comprimento,
						ps.valor_total_materiais,
						ps.maoObra,
						ps.ativo_inativo,
						ps.cod_Unidade,
						ps.cod_Grupo
						INTO
                        var_imagem_Produto_Servico,
						var_descricao_Produto_Servico,
						var_altura_Produto_Servico,
						var_largura_Produto_Servico,
						var_comprimento_Produto_Servico,
						var_valor_total_materiais_Produto_Servico,
						var_maoObra_Produto_Servico,
						var_ativo_inativo_Produto_Servico,
						var_cod_Unidade_Produto_Servico,
						var_cod_Grupo_Produto_Servico
						FROM tbl_Produto_Servico AS ps
						WHERE ps.codigo = var_cod_Produto_Servico;
						
						CALL sp_ManterProduto_Servico("ALTERAÇÃO", var_cod_Produto_Servico, var_imagem_Produto_Servico, var_descricao_Produto_Servico, var_altura_Produto_Servico, var_largura_Produto_Servico, var_comprimento_Produto_Servico, ( var_valor_total_materiais_Produto_Servico + var_valor_total ), var_maoObra_Produto_Servico, var_ativo_inativo_Produto_Servico, var_cod_Unidade_Produto_Servico, var_cod_Grupo_Produto_Servico);
                        
						-- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
						IF ( var_controle_commit ) THEN
							COMMIT;
							SELECT var_codigo_retornavelDesse_MPS AS 'SUCESSO';
						ELSE
							ROLLBACK;
							SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
						END IF;
					
                    ELSE
						SELECT 'A operação requisitada não pode ser realizada pois o Código da PRODUTO OU SERVIÇO informado não é válido.' AS 'ALERTA';
					END IF;
				
                ELSE
					SELECT 'A operação requisitada não pode ser realizada pois o Código da MATERIAL informado não é válido.' AS 'ALERTA';
                END IF;
		
-- -----ALTERAÇÃO ------------------------------------------------------------------------------------------------------------------------------------------------
			ELSEIF (var_operacao = 'ALTERAÇÃO') THEN
            
				IF(var_ativo_inativo = TRUE) THEN
                
                    IF( var_codigo > 0 ) THEN
                    
						IF (var_cod_Material > 0) THEN
                        
							IF (var_cod_Produto_Servico > 0) THEN
								
                                SET var_codigo_retornavelDesse_MPS = var_codigo;
                                
                                START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
                                
                                SELECT valor_total
								INTO var_antigo_valor_total
								FROM tbl_Materiais_Produto_Servico
                                WHERE 
                                codigo = var_codigo AND cod_Material = var_cod_Material AND cod_Produto_Servico = var_cod_Produto_Servico AND ativo_inativo = var_ativo_inativo
                                LIMIT 1;
                                
                                UPDATE tbl_Materiais_Produto_Servico
								SET
                                quantidade = var_quantidade,
                                valor_total = var_valor_total
								WHERE
								codigo = var_codigo AND cod_Material = var_cod_Material AND cod_Produto_Servico = var_cod_Produto_Servico AND ativo_inativo = var_ativo_inativo;
                                
                                SELECT
								ps.imagem,
								ps.descricao,
								ps.altura,
								ps.largura,
								ps.comprimento,
								ps.valor_total_materiais,
								ps.maoObra,
								ps.ativo_inativo,
								ps.cod_Unidade,
								ps.cod_Grupo
								INTO
								var_imagem_Produto_Servico,
								var_descricao_Produto_Servico,
								var_altura_Produto_Servico,
								var_largura_Produto_Servico,
								var_comprimento_Produto_Servico,
								var_valor_total_materiais_Produto_Servico,
								var_maoObra_Produto_Servico,
								var_ativo_inativo_Produto_Servico,
								var_cod_Unidade_Produto_Servico,
								var_cod_Grupo_Produto_Servico
								FROM tbl_Produto_Servico AS ps
								WHERE ps.codigo = var_cod_Produto_Servico;
								
								CALL sp_ManterProduto_Servico("ALTERAÇÃO", var_cod_Produto_Servico, var_imagem_Produto_Servico, var_descricao_Produto_Servico, var_altura_Produto_Servico, var_largura_Produto_Servico, var_comprimento_Produto_Servico, ( ((var_valor_total_materiais_Produto_Servico - var_antigo_valor_total) + var_valor_total ) ), var_maoObra_Produto_Servico, var_ativo_inativo_Produto_Servico, var_cod_Unidade_Produto_Servico, var_cod_Grupo_Produto_Servico);
                                
								-- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
								IF ( var_controle_commit ) THEN
									COMMIT;
									SELECT var_codigo_retornavelDesse_MPS AS 'SUCESSO';
								ELSE
									ROLLBACK;
									SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
								END IF;
							
                            ELSE
								SELECT 'A operação requisitada não pode ser realizada pois o Código da PRODUTO OU SERVIÇO informado não é válido.' AS 'ALERTA';
							END IF;
				
						ELSE
							SELECT 'A operação requisitada não pode ser realizada pois o Código da MATERIAL informado não é válido.' AS 'ALERTA';
						END IF;
                        
                    ELSE
						SELECT 'A operação requisitada não pode ser realizada pois o Código informado não é válido.' AS 'ALERTA';
					END IF;
                    
				ELSE
					SELECT 'A operação requisitada não pode ser realizada pois o registro não está mais ativo.' AS 'ALERTA';
				END IF;

-- -----INATIVAÇÃO ------------------------------------------------------------------------------------------------------------------------------------------------
			ELSEIF (var_operacao = 'INATIVAÇÃO') THEN
            
				IF(var_ativo_inativo = TRUE) THEN
                
                    IF( var_codigo > 0 ) THEN
                    
						IF (var_cod_Material > 0) THEN
                        
							IF (var_cod_Produto_Servico > 0) THEN
                            
								SET var_ativo_inativo = FALSE;
                                SET var_codigo_retornavelDesse_MPS = var_codigo;
                                
								START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
                                
                                UPDATE tbl_Materiais_Produto_Servico
								SET
								ativo_inativo = var_ativo_inativo
								WHERE
                                codigo = var_codigo AND cod_Material = var_cod_Material AND cod_Produto_Servico = var_cod_Produto_Servico AND ativo_inativo = TRUE;
                                
                                SELECT
								ps.imagem,
								ps.descricao,
								ps.altura,
								ps.largura,
								ps.comprimento,
								ps.valor_total_materiais,
								ps.maoObra,
								ps.ativo_inativo,
								ps.cod_Unidade,
								ps.cod_Grupo
								INTO
								var_imagem_Produto_Servico,
								var_descricao_Produto_Servico,
								var_altura_Produto_Servico,
								var_largura_Produto_Servico,
								var_comprimento_Produto_Servico,
								var_valor_total_materiais_Produto_Servico,
								var_maoObra_Produto_Servico,
								var_ativo_inativo_Produto_Servico,
								var_cod_Unidade_Produto_Servico,
								var_cod_Grupo_Produto_Servico
								FROM tbl_Produto_Servico AS ps
								WHERE ps.codigo = var_cod_Produto_Servico;
								
								CALL sp_ManterProduto_Servico("ALTERAÇÃO", var_cod_Produto_Servico, var_imagem_Produto_Servico, var_descricao_Produto_Servico, var_altura_Produto_Servico, var_largura_Produto_Servico, var_comprimento_Produto_Servico, ( var_valor_total_materiais_Produto_Servico - var_valor_total ), var_maoObra_Produto_Servico, var_ativo_inativo_Produto_Servico, var_cod_Unidade_Produto_Servico, var_cod_Grupo_Produto_Servico);
                                
								-- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
								IF ( var_controle_commit ) THEN
									COMMIT;
									SELECT var_codigo_retornavelDesse_MPS AS 'SUCESSO';
								ELSE
									ROLLBACK;
									SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
								END IF;
							
                            ELSE
								SELECT 'A operação requisitada não pode ser realizada pois o Código da PRODUTO OU SERVIÇO informado não é válido.' AS 'ALERTA';
							END IF;
				
						ELSE
							SELECT 'A operação requisitada não pode ser realizada pois o Código da MATERIAL informado não é válido.' AS 'ALERTA';
						END IF;
                        
                    ELSE
						SELECT 'A operação requisitada não pode ser realizada pois o Código informado não é válido.' AS 'ALERTA';
					END IF;
                    
                ELSE
					SELECT 'A operação requisitada não pode ser realizada pois o registro não está mais ativo.' AS 'ALERTA';
				END IF;
		
-- -----CASO NÃO TENHA INSERIDO NENHUMA OPERAÇÃO ----------------------------------------------------------------------------------------------------------------
			ELSE
				SELECT CONCAT('A operação ( ', IFNULL(var_operacao, 'NULL'), ' ) não é VÁLIDA.') AS 'ALERTA';
			END IF;
        
        ELSE
            SELECT CONCAT( 'A operação informada não é válida: ( ', IFNULL(var_operacao, 'NULL'), ' ).' ) AS 'ALERTA';
		END IF;
		
	END IF;
    
END $$
DELIMITER ;

-- ORÇAMENTO -------------------------------------------------------------------------------------------------------------------------------------------------------------
DELIMITER $$
CREATE PROCEDURE sp_ManterOrcamento(
	IN var_operacao CHAR(11), -- CADASTRO, ALTERAÇÃO, INATIVAÇÃO, ATUALIZAÇÃO(caso possua)
    IN var_codigo INT,
    IN var_validade DATETIME,
    IN var_prazo_entrega VARCHAR(150),
    IN var_observacao VARCHAR(150),
    IN var_total_produtos_servicos DECIMAL(13,2),
    IN var_desconto DECIMAL(13,2),
    -- IN var_total_orcamento DECIMAL(13,2),
    IN var_descricao_pagamento VARCHAR(150),
    IN var_valor_entrada DECIMAL(13,2),
    IN var_quantidade_parcelas INT,
    -- IN var_valor_parcela DECIMAL(13,2),
    IN var_juros DECIMAL(13, 2),
    -- IN var_valor_juros DECIMAL(13,2),
    IN var_ativo_inativo BOOLEAN,
    IN var_cod_Usuario INT,
    IN var_cod_Cliente INT
)
BEGIN
	DECLARE var_resgistro_LOG CHAR(32);
	DECLARE var_informacoes_registro VARCHAR(1000);
    
    DECLARE var_ultima_atualizacao DATETIME;
    DECLARE var_total_orcamento DECIMAL(13,2);
    DECLARE var_valor_parcela DECIMAL(13,2);
    DECLARE var_valor_juros DECIMAL(13,2);
    
	DECLARE var_controle_commit TINYINT DEFAULT TRUE;		-- Cria uma var_controle_commit que vai ser usada para check se o commit pode ser realizado, ou dar rollback em um erro.
	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET var_controle_commit = FALSE;		-- Caso haja um erro na execução, a var_controle_commit receberá FALSE para acionar o rollback.
    
    SET var_resgistro_LOG = 'ORÇAMENTO';
    SET @@AUTOCOMMIT = OFF;	-- Desliga o commit automatico para que o "Try, Catch e rollback" possa funcionar corretamente e commitar manualmente.
    
    IF ( (var_codigo IS NULL) OR (var_validade IS NULL) OR (var_prazo_entrega IS NULL) OR (var_observacao IS NULL) OR (var_total_produtos_servicos IS NULL) OR (var_desconto IS NULL) OR (var_descricao_pagamento IS NULL) OR (var_valor_entrada IS NULL) OR (var_quantidade_parcelas IS NULL) OR (var_juros IS NULL) OR (var_ativo_inativo IS NULL) OR (var_cod_Usuario IS NULL) OR (var_cod_Cliente IS NULL) ) THEN
		SELECT CONCAT('A informação está NULL ou Inválida. \r\n ', 
        '\r\n CAMPO Código: ( ', IFNULL(var_codigo, 'NULL'), ' )',
        '\r\n CAMPO Válidade: ( ', IFNULL(var_validade, 'NULL'), ' )',
        '\r\n CAMPO Prazo Entrega: ( ', IFNULL(var_prazo_entrega, 'NULL'), ' )',
        '\r\n CAMPO Observação: ( ', IFNULL(var_observacao, 'NULL'), ' )',
        '\r\n CAMPO Valor Total Produtos e Serviços: ( ', IFNULL(var_total_produtos_servicos, 'NULL'), ' )',
        '\r\n CAMPO Desconto: ( ', IFNULL(var_desconto, 'NULL'), ' )',
        '\r\n CAMPO Descrição Pagamento: ( ', IFNULL(var_descricao_pagamento, 'NULL'), ' )',
        '\r\n CAMPO Valor Entrada: ( ', IFNULL(var_valor_entrada, 'NULL'), ' )',
        '\r\n CAMPO Quantidade Parcelas: ( ', IFNULL(var_quantidade_parcelas, 'NULL'), ' )',
        '\r\n CAMPO Juros: ( ', IFNULL(var_juros, 'NULL'), ' )',
        '\r\n CAMPO Registro Ativo: ( ', IFNULL(var_ativo_inativo, 'NULL'), ' )',
        '\r\n CAMPO Código Usuario: ( ', IFNULL(var_cod_Usuario, 'NULL'), ' )',
        '\r\n CAMPO Código Cliente: ( ', IFNULL(var_cod_Cliente, 'NULL'), ' )'
        ) AS 'ALERTA';
	
    ELSE
    
        SET var_ultima_atualizacao = localtimestamp(0);
        SET var_total_orcamento = 0.00;
        SET var_valor_parcela =  0.00;
        SET var_valor_juros =  0.00;
        
        IF ( (var_operacao = 'CADASTRO') OR (var_operacao = 'ALTERAÇÃO') OR (var_operacao = 'INATIVAÇÃO') ) THEN
        
-- -----CADASTRO ------------------------------------------------------------------------------------------------------------------------------------------------
			IF (var_operacao = 'CADASTRO') THEN
            
                IF (var_cod_Usuario > 0) THEN
                
                    IF (var_cod_Cliente > 0) THEN
                    
						SET var_ativo_inativo = TRUE;
                        
						START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
                        
						INSERT INTO tbl_Orcamento
						(ultima_atualizacao, validade, prazo_entrega, observacao, total_produtos_servicos, desconto, total_orcamento, descricao_pagamento, valor_entrada, quantidade_parcelas, valor_parcela, juros, valor_juros, ativo_inativo, cod_Usuario, cod_Cliente)
						VALUES
						(var_ultima_atualizacao, var_validade, var_prazo_entrega, var_observacao, var_total_produtos_servicos, var_desconto, var_total_orcamento, var_descricao_pagamento, var_valor_entrada, var_quantidade_parcelas, var_valor_parcela, var_juros, var_valor_juros, var_ativo_inativo, var_cod_Usuario, var_cod_Cliente);
                        
						SELECT MAX(o.codigo)
						INTO var_codigo
						FROM tbl_Orcamento AS o LIMIT 1;
                        
						SET var_informacoes_registro = CONCAT('Código: ( ', IFNULL(var_codigo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
						'Ultima Atualização: ( ', IFNULL(var_ultima_atualizacao, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
						'Válidade: ( ', IFNULL(var_validade, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
						'Prazo Entrega: ( ', IFNULL(var_prazo_entrega, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                        'Observação: ( ', IFNULL(var_observacao, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                        'Valor Total Produtos e Serviços: ( ', IFNULL(var_total_produtos_servicos, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                        'Valor Desconto: ( ', IFNULL(var_desconto, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                        'Valor Total Orçamento: ( ', IFNULL(var_total_orcamento, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                        'Descrição Pagamento: ( ', IFNULL(var_descricao_pagamento, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                        'Valor Entrada: ( ', IFNULL(var_valor_entrada, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                        'Quantidade Parcelas: ( ', IFNULL(var_quantidade_parcelas, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                        'Valor Parcela: ( ', IFNULL(var_valor_parcela, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                        'Juros: ( ', IFNULL(var_juros, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                        'Valor Juros: ( ', IFNULL(var_valor_juros, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
                        'Registro Ativo: ( ', IFNULL(var_ativo_inativo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n \r\n',
                        'Código Usuario: ( ', IFNULL(var_cod_Usuario, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
						'Código Cliente: ( ', IFNULL(var_cod_Cliente, 'ERROR: INFORMAÇÃO NULL'), ' ).');
                        
						CALL sp_GerarLOG(var_operacao, var_resgistro_LOG, var_informacoes_registro);
                
						-- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
						IF ( var_controle_commit ) THEN
							COMMIT;
							SELECT var_codigo AS 'SUCESSO';
						ELSE
							ROLLBACK;
							SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
						END IF;
					
                    ELSE
						SELECT 'A operação requisitada não pode ser realizada pois o Código da CLIENTE informado não é válido.' AS 'ALERTA';
					END IF;
				
                ELSE
					SELECT 'A operação requisitada não pode ser realizada pois o Código da USUARIO informado não é válido.' AS 'ALERTA';
                END IF;
		
-- -----ALTERAÇÃO OU INATIVAÇÃO ------------------------------------------------------------------------------------------------------------------------------------------------
			ELSEIF ( (var_operacao = 'ALTERAÇÃO') OR (var_operacao = 'INATIVAÇÃO') ) THEN
				
                IF( var_ativo_inativo ) THEN
					
                    IF (var_operacao = 'INATIVAÇÃO') THEN
						SET var_ativo_inativo = FALSE;
					END IF;
                
                    IF( var_codigo > 0 ) THEN
                    
						IF (var_cod_Usuario > 0) THEN
                        
							IF (var_cod_Cliente > 0) THEN
								
                                SET var_total_orcamento = var_total_produtos_servicos - (var_total_produtos_servicos * (var_desconto / 100));
								SET var_valor_parcela = (var_total_orcamento - var_valor_entrada) / var_quantidade_parcelas;
								SET var_valor_juros = var_valor_parcela + (var_valor_parcela * (var_juros / 100));

								START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
                                
                                UPDATE tbl_Orcamento
								SET
								ultima_atualizacao = var_ultima_atualizacao,
								validade = var_validade,
                                prazo_entrega = var_prazo_entrega,
                                observacao = var_observacao,
                                total_produtos_servicos = var_total_produtos_servicos,
                                desconto = var_desconto,
                                total_orcamento = var_total_orcamento,
                                descricao_pagamento = var_descricao_pagamento,
                                valor_entrada = var_valor_entrada,
                                quantidade_parcelas = var_quantidade_parcelas,
                                valor_parcela = var_valor_parcela,
                                juros = var_juros,
                                valor_juros = var_valor_juros,
                                cod_Usuario = var_cod_Usuario,
                                cod_Cliente = var_cod_Cliente,
                                ativo_inativo = var_ativo_inativo
								WHERE
								codigo = var_codigo;
                                
								SET var_informacoes_registro = CONCAT('Código: ( ', IFNULL(var_codigo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
								'Ultima Atualização: ( ', IFNULL(var_ultima_atualizacao, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
								'Válidade: ( ', IFNULL(var_validade, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
								'Prazo Entrega: ( ', IFNULL(var_prazo_entrega, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
								'Observação: ( ', IFNULL(var_observacao, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
								'Valor Total Produtos e Serviços: ( ', IFNULL(var_total_produtos_servicos, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
								'Valor Desconto: ( ', IFNULL(var_desconto, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
								'Valor Total Orçamento: ( ', IFNULL(var_total_orcamento, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
								'Descrição Pagamento: ( ', IFNULL(var_descricao_pagamento, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
								'Valor Entrada: ( ', IFNULL(var_valor_entrada, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
								'Quantidade Parcelas: ( ', IFNULL(var_quantidade_parcelas, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
								'Valor Parcela: ( ', IFNULL(var_valor_parcela, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
								'Juros: ( ', IFNULL(var_juros, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
								'Valor Juros: ( ', IFNULL(var_valor_juros, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
								'Registro Ativo: ( ', IFNULL(var_ativo_inativo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n \r\n',
								'Código Usuario: ( ', IFNULL(var_cod_Usuario, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
								'Código Cliente: ( ', IFNULL(var_cod_Cliente, 'ERROR: INFORMAÇÃO NULL'), ' ).');
                                
								CALL sp_GerarLOG(var_operacao, var_resgistro_LOG, var_informacoes_registro);
								
								-- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
								IF ( var_controle_commit ) THEN
									COMMIT;
									SELECT var_codigo AS 'SUCESSO';
								ELSE
									ROLLBACK;
									SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
								END IF;
							
                            ELSE
								SELECT 'A operação requisitada não pode ser realizada pois o Código da CLIENTE informado não é válido.' AS 'ALERTA';
							END IF;
				
						ELSE
							SELECT 'A operação requisitada não pode ser realizada pois o Código da USUARIO informado não é válido.' AS 'ALERTA';
						END IF;
                        
                    ELSE
						SELECT 'A operação requisitada não pode ser realizada pois o Código informado não é válido.' AS 'ALERTA';
					END IF;
                    
				ELSE
					SELECT 'A operação requisitada não pode ser realizada pois o registro não está mais ativo.' AS 'ALERTA';
				END IF;
		
-- -----CASO NÃO TENHA INSERIDO NENHUMA OPERAÇÃO ----------------------------------------------------------------------------------------------------------------
			ELSE
				SELECT CONCAT('A operação ( ', IFNULL(var_operacao, 'NULL'), ' ) não é VÁLIDA.') AS 'ALERTA';
			END IF;
        
        ELSE
            SELECT CONCAT( 'A operação informada não é válida: ( ', IFNULL(var_operacao, 'NULL'), ' ).' ) AS 'ALERTA';
		END IF;
		
	END IF;
    
END $$
DELIMITER ;

-- PRODUTOS OU SERVIÇOS DO ORÇAMENTO  -------------------------------------------------------------------------------------------------------------------------------------------------------------
DELIMITER $$
CREATE PROCEDURE sp_ManterProdutos_Servicos_Orcamento(
	IN var_operacao CHAR(11), -- CADASTRO, ALTERAÇÃO, INATIVAÇÃO, ATUALIZAÇÃO(caso possua)
    IN var_codigo INT,
    IN var_quantidade DECIMAL(13,2),
    IN var_valor_total DECIMAL(13,2),            -- Essa é a quantidade de produtos * valor unitario do Produto ou Serviço
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
    DECLARE var_antigo_valor_total DECIMAL(13,2);
    DECLARE var_reter_codigo_retornado_Orcamento VARCHAR(1000);
    
    DECLARE var_validade_Orcamento DATETIME;
    DECLARE var_prazo_entrega_Orcamento VARCHAR(150);
    DECLARE var_observacao_Orcamento VARCHAR(150);
    DECLARE var_total_produtos_servicos_Orcamento DECIMAL(13,2);
    DECLARE var_desconto_Orcamento DECIMAL(13,2);
    -- DECLARE var_total_orcamento_Orcamento DECIMAL(13,2);
    DECLARE var_descricao_pagamento_Orcamento VARCHAR(150);
    DECLARE var_valor_entrada_Orcamento DECIMAL(13,2);
    DECLARE var_quantidade_parcelas_Orcamento INT;
    -- DECLARE var_valor_parcela_Orcamento DECIMAL(13,2);
    DECLARE var_juros_Orcamento DECIMAL(13,2);
    -- DECLARE var_valor_juros_Orcamento DECIMAL(13,2);
    DECLARE var_ativo_inativo_Orcamento BOOLEAN;
    DECLARE var_cod_Usuario_Orcamento INT;
	DECLARE var_cod_Cliente_Orcamento INT;
    
    DECLARE var_controle_commit TINYINT DEFAULT TRUE;		-- Cria uma var_controle_commit que vai ser usada para check se o commit pode ser realizado, ou dar rollback em um erro.
	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET var_controle_commit = FALSE;		-- Caso haja um erro na execução, a var_controle_commit receberá FALSE para acionar o rollback.
    
    SET @@AUTOCOMMIT = OFF;	-- Desliga o commit automatico para que o "Try, Catch e rollback" possa funcionar corretamente e commitar manualmente.
    
    IF ( (var_codigo IS NULL) OR (var_quantidade IS NULL) OR (var_valor_total IS NULL) OR (var_cod_Produto_Servico IS NULL) OR (var_ultima_atualizacao_Produto_Servico IS NULL) OR (var_imagem_Produto_Servico IS NULL) OR (var_descricao_Produto_Servico IS NULL) OR (var_altura_Produto_Servico IS NULL) OR (var_largura_Produto_Servico IS NULL) OR (var_comprimento_Produto_Servico IS NULL) OR (var_valor_total_materiais_Produto_Servico IS NULL) OR (var_maoObra_Produto_Servico IS NULL) OR (var_valor_maoObra_Produto_Servico IS NULL) OR (var_valor_unitario_Produto_Servico IS NULL) OR (var_ativo_inativo_Produto_Servico IS NULL) OR (var_cod_Unidade IS NULL) OR (var_sigla_Unidade IS NULL) OR (var_descricao_Unidade IS NULL) OR (var_ativo_inativo_Unidade IS NULL) OR (var_cod_Grupo IS NULL) OR (var_descricao_Grupo IS NULL) OR (var_ativo_inativo_Grupo IS NULL) OR (var_cod_Orcamento IS NULL) ) THEN
		SELECT CONCAT('A informação está NULL ou Inválida. \r\n ', 
        '\r\n CAMPO Código: ( ', IFNULL(var_codigo, 'NULL'), ' )',
        '\r\n CAMPO Quantidade: ( ', IFNULL(var_quantidade, 'NULL'), ' )',
        '\r\n CAMPO Valor Total: ( ', IFNULL(var_valor_total, 'NULL'), ' )',
        '\r\n CAMPO Código Produto ou Serviço: ( ', IFNULL(var_cod_Produto_Servico, 'NULL'), ' )',
        '\r\n CAMPO Ultima AtualizaçãoProduto ou Serviço: ( ', IFNULL(var_ultima_atualizacao_Produto_Servico, 'NULL'), ' )',
        '\r\n CAMPO Descrição Produto ou Serviço: ( ', IFNULL(var_descricao_Produto_Servico, 'NULL'), ' )',
        '\r\n CAMPO Altura Produto ou Serviço: ( ', IFNULL(var_altura_Produto_Servico, 'NULL'), ' )',
        '\r\n CAMPO Largura Produto ou Serviço: ( ', IFNULL(var_largura_Produto_Servico, 'NULL'), ' )',
        '\r\n CAMPO Comprimento Produto ou Serviço: ( ', IFNULL(var_comprimento_Produto_Servico, 'NULL'), ' )',
		'\r\n CAMPO Valor Total Materiais: ( ', IFNULL(var_valor_total_materiais_Produto_Servico, 'NULL'), ' )',
		'\r\n CAMPO Mão de Obra: ( ', IFNULL(var_maoObra_Produto_Servico, 'NULL'), ' )',
		'\r\n CAMPO Valor Mão de Obra: ( ', IFNULL(var_valor_maoObra_Produto_Servico, 'NULL'), ' )',
        '\r\n CAMPO Valor Unitário Produto ou Serviço: ( ', IFNULL(var_valor_unitario_Produto_Servico, 'NULL'), ' )',
        '\r\n CAMPO Produto ou Serviço Ativo: ( ', IFNULL(var_ativo_inativo_Produto_Servico, 'NULL'), ' )',
        '\r\n CAMPO Código Unidade: ( ', IFNULL(var_cod_Unidade, 'NULL'), ' )',
        '\r\n CAMPO Sigla Unidade: ( ', IFNULL(var_sigla_Unidade, 'NULL'), ' )',
        '\r\n CAMPO Descrição Unidade: ( ', IFNULL(var_descricao_Unidade, 'NULL'), ' )',
        '\r\n CAMPO Unidade Ativo: ( ', IFNULL(var_ativo_inativo_Unidade, 'NULL'), ' )',
        '\r\n CAMPO Código Grupo: ( ', IFNULL(var_cod_Grupo, 'NULL'), ' )',
        '\r\n CAMPO Descrição Grupo: ( ', IFNULL(var_descricao_Grupo, 'NULL'), ' )',
        '\r\n CAMPO Grupo Ativo: ( ', IFNULL(var_ativo_inativo_Grupo, 'NULL'), ' )',
        '\r\n CAMPO Código Orçamento: ( ', IFNULL(var_cod_Orcamento, 'NULL'), ' )'
        ) AS 'ALERTA';
	
    ELSE
        
        IF ( (var_operacao = 'CADASTRO') OR (var_operacao = 'ALTERAÇÃO') OR (var_operacao = 'INATIVAÇÃO') ) THEN
        
-- -----CADASTRO ------------------------------------------------------------------------------------------------------------------------------------------------
			IF (var_operacao = 'CADASTRO') THEN
            
                IF (var_cod_Produto_Servico > 0) THEN
					
                    IF (var_cod_Unidade > 0) THEN
                    
						IF (var_cod_Grupo > 0) THEN
                        
							IF (var_cod_Orcamento > 0) THEN
								
                                START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
								
								INSERT INTO tbl_Produtos_Servicos_Orcamento
								(quantidade, valor_total, cod_Produto_Servico, ultima_atualizacao_Produto_Servico, imagem_Produto_Servico, descricao_Produto_Servico, altura_Produto_Servico, largura_Produto_Servico, comprimento_Produto_Servico, valor_total_materiais_Produto_Servico, maoObra_Produto_Servico, valor_maoObra_Produto_Servico, valor_unitario_Produto_Servico, ativo_inativo_Produto_Servico, cod_Unidade, sigla_Unidade, descricao_Unidade, ativo_inativo_Unidade, cod_Grupo, descricao_Grupo, ativo_inativo_Grupo, cod_Orcamento)
								VALUES
								(var_quantidade, var_valor_total, var_cod_Produto_Servico, var_ultima_atualizacao_Produto_Servico, var_imagem_Produto_Servico, var_descricao_Produto_Servico, var_altura_Produto_Servico, var_largura_Produto_Servico, var_comprimento_Produto_Servico, var_valor_total_materiais_Produto_Servico, var_maoObra_Produto_Servico, var_valor_maoObra_Produto_Servico, var_valor_unitario_Produto_Servico, var_ativo_inativo_Produto_Servico, var_cod_Unidade, var_sigla_Unidade, var_descricao_Unidade, var_ativo_inativo_Unidade, var_cod_Grupo, var_descricao_Grupo, var_ativo_inativo_Grupo, var_cod_Orcamento);
								
								SELECT MAX(pso.codigo)
								INTO var_codigo
								FROM tbl_Produtos_Servicos_Orcamento AS pso LIMIT 1;
								
                                SELECT
								o.validade,
								o.prazo_entrega,
								o.observacao,
								o.total_produtos_servicos,
								o.desconto,
								o.descricao_pagamento,
								o.valor_entrada,
								o.quantidade_parcelas,
								o.juros,
								o.ativo_inativo,
								o.cod_Usuario,
								o.cod_Cliente
								INTO
								var_validade_Orcamento,
								var_prazo_entrega_Orcamento,
								var_observacao_Orcamento,
								var_total_produtos_servicos_Orcamento,
								var_desconto_Orcamento,
								var_descricao_pagamento_Orcamento,
								var_valor_entrada_Orcamento,
								var_quantidade_parcelas_Orcamento,
								var_juros_Orcamento,
								var_ativo_inativo_Orcamento,
								var_cod_Usuario_Orcamento,
								var_cod_Cliente_Orcamento
								FROM tbl_Orcamento AS o
								WHERE o.codigo = var_cod_Orcamento;
                                
								CALL sp_AlterarValores_Orcamento_Sem_Output(var_cod_Orcamento, var_validade_Orcamento, var_prazo_entrega_Orcamento, var_observacao_Orcamento, ( var_total_produtos_servicos_Orcamento + var_valor_total ), var_desconto_Orcamento, var_descricao_pagamento_Orcamento, var_valor_entrada_Orcamento, var_quantidade_parcelas_Orcamento, var_juros_Orcamento, var_ativo_inativo_Orcamento, var_cod_Usuario_Orcamento, var_cod_Cliente_Orcamento);
                                
								-- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
								IF ( var_controle_commit ) THEN
									COMMIT;
									SELECT var_codigo AS 'SUCESSO';
								ELSE
									ROLLBACK;
									SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
								END IF;
							
                            ELSE
								SELECT 'A operação requisitada não pode ser realizada pois o Código da ORÇAMENTO informado não é válido.' AS 'ALERTA';
							END IF;
                        
                        ELSE
							SELECT 'A operação requisitada não pode ser realizada pois o Código da GRUPO informado não é válido.' AS 'ALERTA';
						END IF;
                    
                    ELSE
						SELECT 'A operação requisitada não pode ser realizada pois o Código da UNIDADE informado não é válido.' AS 'ALERTA';
					END IF;
				
                ELSE
					SELECT 'A operação requisitada não pode ser realizada pois o Código da PRODUTO OU SERVIÇO informado não é válido.' AS 'ALERTA';
                END IF;
		
-- -----ALTERAÇÃO ------------------------------------------------------------------------------------------------------------------------------------------------
			ELSEIF (var_operacao = 'ALTERAÇÃO') THEN
            
				IF( var_codigo > 0 ) THEN
                    
					IF (var_cod_Produto_Servico > 0) THEN
							
						IF (var_cod_Unidade > 0) THEN
                            
							IF (var_cod_Grupo > 0) THEN
                                
								IF (var_cod_Orcamento > 0) THEN
                                    
									START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
                                    
                                    SELECT valor_total
									INTO var_antigo_valor_total
									FROM tbl_Produtos_Servicos_Orcamento
									WHERE 
									codigo = var_codigo AND cod_Produto_Servico = var_cod_Produto_Servico AND cod_Orcamento = var_cod_Orcamento
									LIMIT 1;
										
									UPDATE tbl_Produtos_Servicos_Orcamento
									SET
									quantidade = var_quantidade,
									valor_total = var_valor_total,
                                    cod_Produto_Servico = var_cod_Produto_Servico,
										ultima_atualizacao_Produto_Servico = var_ultima_atualizacao_Produto_Servico,
										imagem_Produto_Servico = var_imagem_Produto_Servico,
										descricao_Produto_Servico = var_descricao_Produto_Servico,
										altura_Produto_Servico = var_altura_Produto_Servico,
										largura_Produto_Servico = var_largura_Produto_Servico,
										comprimento_Produto_Servico = var_comprimento_Produto_Servico,
                                        valor_total_materiais_Produto_Servico = var_valor_total_materiais_Produto_Servico,
                                        maoObra_Produto_Servico = var_maoObra_Produto_Servico,
                                        valor_maoObra_Produto_Servico = var_valor_maoObra_Produto_Servico,
										valor_unitario_Produto_Servico = var_valor_unitario_Produto_Servico,
										ativo_inativo_Produto_Servico = var_ativo_inativo_Produto_Servico,
                                    cod_Unidade = var_cod_Unidade,
										sigla_Unidade = var_sigla_Unidade,
										descricao_Unidade = var_descricao_Unidade,
										ativo_inativo_Unidade = var_ativo_inativo_Unidade,
                                    cod_Grupo = var_cod_Grupo,
										descricao_Grupo = var_descricao_Grupo,
										ativo_inativo_Grupo = var_ativo_inativo_Grupo,
                                    cod_Orcamento = var_cod_Orcamento
									WHERE
									codigo = var_codigo AND cod_Produto_Servico = var_cod_Produto_Servico AND cod_Orcamento = var_cod_Orcamento;
									
                                    SELECT
									o.validade,
									o.prazo_entrega,
									o.observacao,
									o.total_produtos_servicos,
									o.desconto,
									o.descricao_pagamento,
									o.valor_entrada,
									o.quantidade_parcelas,
									o.juros,
									o.ativo_inativo,
									o.cod_Usuario,
									o.cod_Cliente
									INTO
									var_validade_Orcamento,
									var_prazo_entrega_Orcamento,
									var_observacao_Orcamento,
									var_total_produtos_servicos_Orcamento,
									var_desconto_Orcamento,
									var_descricao_pagamento_Orcamento,
									var_valor_entrada_Orcamento,
									var_quantidade_parcelas_Orcamento,
									var_juros_Orcamento,
									var_ativo_inativo_Orcamento,
									var_cod_Usuario_Orcamento,
									var_cod_Cliente_Orcamento
									FROM tbl_Orcamento AS o
									WHERE o.codigo = var_cod_Orcamento;
									
									CALL sp_AlterarValores_Orcamento_Sem_Output(var_cod_Orcamento, var_validade_Orcamento, var_prazo_entrega_Orcamento, var_observacao_Orcamento, ( (var_total_produtos_servicos_Orcamento - var_antigo_valor_total) + var_valor_total ), var_desconto_Orcamento, var_descricao_pagamento_Orcamento, var_valor_entrada_Orcamento, var_quantidade_parcelas_Orcamento, var_juros_Orcamento, var_ativo_inativo_Orcamento, var_cod_Usuario_Orcamento, var_cod_Cliente_Orcamento);
                                    
									-- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
									IF ( var_controle_commit ) THEN
										COMMIT;
										SELECT var_codigo AS 'SUCESSO';
									ELSE
										ROLLBACK;
										SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
									END IF;
									
								ELSE
									SELECT 'A operação requisitada não pode ser realizada pois o Código da ORÇAMENTO informado não é válido.' AS 'ALERTA';
								END IF;
								
							ELSE
								SELECT 'A operação requisitada não pode ser realizada pois o Código da GRUPO informado não é válido.' AS 'ALERTA';
							END IF;
							
						ELSE
							SELECT 'A operação requisitada não pode ser realizada pois o Código da UNIDADE informado não é válido.' AS 'ALERTA';
						END IF;
						
					ELSE
						SELECT 'A operação requisitada não pode ser realizada pois o Código da PRODUTO OU SERVIÇO informado não é válido.' AS 'ALERTA';
					END IF;
								
				ELSE
					SELECT 'A operação requisitada não pode ser realizada pois o Código informado não é válido.' AS 'ALERTA';
				END IF;

-- -----INATIVAÇÃO ------------------------------------------------------------------------------------------------------------------------------------------------
			ELSEIF (var_operacao = 'INATIVAÇÃO') THEN
            
				IF( var_codigo > 0 ) THEN
                    
					IF (var_cod_Produto_Servico > 0) THEN
							
						IF (var_cod_Unidade > 0) THEN
                            
							IF (var_cod_Grupo > 0) THEN
                                
								IF (var_cod_Orcamento > 0) THEN
                                    
                                    START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
									
									DELETE FROM tbl_Produtos_Servicos_Orcamento
                                    WHERE
                                    codigo = var_codigo AND cod_Produto_Servico = var_cod_Produto_Servico AND cod_Orcamento = var_cod_Orcamento;
									
                                    SELECT
									o.validade,
									o.prazo_entrega,
									o.observacao,
									o.total_produtos_servicos,
									o.desconto,
									o.descricao_pagamento,
									o.valor_entrada,
									o.quantidade_parcelas,
									o.juros,
									o.ativo_inativo,
									o.cod_Usuario,
									o.cod_Cliente
									INTO
									var_validade_Orcamento,
									var_prazo_entrega_Orcamento,
									var_observacao_Orcamento,
									var_total_produtos_servicos_Orcamento,
									var_desconto_Orcamento,
									var_descricao_pagamento_Orcamento,
									var_valor_entrada_Orcamento,
									var_quantidade_parcelas_Orcamento,
									var_juros_Orcamento,
									var_ativo_inativo_Orcamento,
									var_cod_Usuario_Orcamento,
									var_cod_Cliente_Orcamento
									FROM tbl_Orcamento AS o
									WHERE o.codigo = var_cod_Orcamento;
									
									CALL sp_AlterarValores_Orcamento_Sem_Output(var_cod_Orcamento, var_validade_Orcamento, var_prazo_entrega_Orcamento, var_observacao_Orcamento, ( var_total_produtos_servicos_Orcamento - var_valor_total ), var_desconto_Orcamento, var_descricao_pagamento_Orcamento, var_valor_entrada_Orcamento, var_quantidade_parcelas_Orcamento, var_juros_Orcamento, var_ativo_inativo_Orcamento, var_cod_Usuario_Orcamento, var_cod_Cliente_Orcamento);
									
									-- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
									IF ( var_controle_commit ) THEN
										COMMIT;
										SELECT var_codigo AS 'SUCESSO';
									ELSE
										ROLLBACK;
										SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
									END IF;
									
								ELSE
									SELECT 'A operação requisitada não pode ser realizada pois o Código da ORÇAMENTO informado não é válido.' AS 'ALERTA';
								END IF;
								
							ELSE
								SELECT 'A operação requisitada não pode ser realizada pois o Código da GRUPO informado não é válido.' AS 'ALERTA';
							END IF;
							
						ELSE
							SELECT 'A operação requisitada não pode ser realizada pois o Código da UNIDADE informado não é válido.' AS 'ALERTA';
						END IF;
						
					ELSE
						SELECT 'A operação requisitada não pode ser realizada pois o Código da PRODUTO OU SERVIÇO informado não é válido.' AS 'ALERTA';
					END IF;
								
				ELSE
					SELECT 'A operação requisitada não pode ser realizada pois o Código informado não é válido.' AS 'ALERTA';
				END IF;
		
-- -----CASO NÃO TENHA INSERIDO NENHUMA OPERAÇÃO ----------------------------------------------------------------------------------------------------------------
			ELSE
				SELECT CONCAT('A operação ( ', IFNULL(var_operacao, 'NULL'), ' ) não é VÁLIDA.') AS 'ALERTA';
			END IF;
        
        ELSE
            SELECT CONCAT( 'A operação informada não é válida: ( ', IFNULL(var_operacao, 'NULL'), ' ).' ) AS 'ALERTA';
		END IF;
		
	END IF;
    
END $$
DELIMITER ;

-- MATERIAIS DO ORÇAMENTO  -------------------------------------------------------------------------------------------------------------------------------------------------------------
DELIMITER $$
CREATE PROCEDURE sp_ManterMateriais_Orcamento(
	IN var_operacao CHAR(11), -- CADASTRO, ALTERAÇÃO, INATIVAÇÃO, ATUALIZAÇÃO(caso possua)
    IN var_codigo INT,
    IN var_quantidade_total  DECIMAL(13,2),
    IN var_valor_total DECIMAL(13,2),            -- Essa é a quantidade de produtos * valor unitario do Produto ou Serviço
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
	
    DECLARE var_controle_commit TINYINT DEFAULT TRUE;		-- Cria uma var_controle_commit que vai ser usada para check se o commit pode ser realizado, ou dar rollback em um erro.
	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET var_controle_commit = FALSE;		-- Caso haja um erro na execução, a var_controle_commit receberá FALSE para acionar o rollback.
    
    SET @@AUTOCOMMIT = OFF;	-- Desliga o commit automatico para que o "Try, Catch e rollback" possa funcionar corretamente e commitar manualmente.
    
    IF ( (var_codigo IS NULL) OR (var_quantidade_total IS NULL) OR (var_valor_total IS NULL)  OR (var_cod_Produtos_Servicos_Orcamento IS NULL)  OR (var_cod_Materiais_Produto_Servico IS NULL)  OR (var_quantidade_Materiais_Produto_Servico IS NULL)  OR (var_valor_unitario_Materiais_Produto_Servico IS NULL) OR (var_cod_Material IS NULL) OR (var_ultima_atualizacao_Material IS NULL) OR (var_imagem_Material IS NULL) OR (var_descricao_Material IS NULL) OR (var_altura_Material IS NULL) OR (var_largura_Material IS NULL) OR (var_comprimento_Material IS NULL) OR (var_ativo_inativo_Material IS NULL) OR (var_cod_Unidade IS NULL) OR (var_sigla_Unidade IS NULL) OR (var_descricao_Unidade IS NULL) OR (var_ativo_inativo_Unidade IS NULL) OR (var_cod_Grupo IS NULL) OR (var_descricao_Grupo IS NULL) OR (var_ativo_inativo_Grupo IS NULL) OR (var_cod_Fornecedor IS NULL) OR (var_nome_razao_social_Fornecedor IS NULL) OR (var_nome_fantasia_Fornecedor IS NULL) OR (var_ativo_inativo_Fornecedor IS NULL) OR (var_cod_Orcamento IS NULL)) THEN
		SELECT CONCAT('A informação está NULL ou Inválida. \r\n ', 
        '\r\n CAMPO Código: ( ', IFNULL(var_codigo, 'NULL'), ' )',
        '\r\n CAMPO Quantidade: ( ', IFNULL(var_quantidade_total, 'NULL'), ' )',
        '\r\n CAMPO Valor Total: ( ', IFNULL(var_valor_total, 'NULL'), ' )',
        '\r\n CAMPO Código Produto ou Serviço do Orçamento: ( ', IFNULL(var_cod_Produtos_Servicos_Orcamento, 'NULL'), ' )',
        '\r\n CAMPO Código Materiais Produto Servico: ( ', IFNULL(var_cod_Materiais_Produto_Servico, 'NULL'), ' )',
        '\r\n CAMPO Quantidade Materiais Produto Servico: ( ', IFNULL(var_quantidade_Materiais_Produto_Servico, 'NULL'), ' )',
        '\r\n CAMPO Valor Unitário Materiais Produto Servico: ( ', IFNULL(var_valor_unitario_Materiais_Produto_Servico, 'NULL'), ' )',
        '\r\n CAMPO Código Material: ( ', IFNULL(var_cod_Material, 'NULL'), ' )',
        '\r\n CAMPO Ultima Atualização Material: ( ', IFNULL(var_ultima_atualizacao_Material, 'NULL'), ' )',
        '\r\n CAMPO Descrição Material: ( ', IFNULL(var_descricao_Material, 'NULL'), ' )',
        '\r\n CAMPO Altura Material: ( ', IFNULL(var_altura_Material, 'NULL'), ' )',
        '\r\n CAMPO Largura Material: ( ', IFNULL(var_largura_Material, 'NULL'), ' )',
        '\r\n CAMPO Comprimento Material: ( ', IFNULL(var_comprimento_Material, 'NULL'), ' )',
        '\r\n CAMPO Valor Unitário Material: ( ', IFNULL(var_valor_unitario_Material, 'NULL'), ' )',
        '\r\n CAMPO Material Ativo: ( ', IFNULL(var_ativo_inativo_Material, 'NULL'), ' )',
        '\r\n CAMPO Código Unidade: ( ', IFNULL(var_cod_Unidade, 'NULL'), ' )',
        '\r\n CAMPO Sigla Unidade: ( ', IFNULL(var_sigla_Unidade, 'NULL'), ' )',
        '\r\n CAMPO Descrição Unidade: ( ', IFNULL(var_descricao_Unidade, 'NULL'), ' )',
        '\r\n CAMPO Unidade Ativo: ( ', IFNULL(var_ativo_inativo_Unidade, 'NULL'), ' )',
        '\r\n CAMPO Código Grupo: ( ', IFNULL(var_cod_Grupo, 'NULL'), ' )',
        '\r\n CAMPO Descrição Grupo: ( ', IFNULL(var_descricao_Grupo, 'NULL'), ' )',
        '\r\n CAMPO Grupo Ativo: ( ', IFNULL(var_ativo_inativo_Grupo, 'NULL'), ' )',
        '\r\n CAMPO Código Fornecedor: ( ', IFNULL(var_cod_Fornecedor, 'NULL'), ' )',
        '\r\n CAMPO Razão Social: ( ', IFNULL(var_nome_razao_social_Fornecedor, 'NULL'), ' )',
        '\r\n CAMPO Nome Fantasia: ( ', IFNULL(var_nome_fantasia_Fornecedor, 'NULL'), ' )',
        '\r\n CAMPO Fornecedor Ativo: ( ', IFNULL(var_ativo_inativo_Fornecedor, 'NULL'), ' )',
        '\r\n CAMPO Código Orçamento: ( ', IFNULL(var_cod_Orcamento, 'NULL'), ' )'
        ) AS 'ALERTA';
        
	ELSE
    
        IF ( (var_operacao = 'CADASTRO') OR (var_operacao = 'ALTERAÇÃO') OR (var_operacao = 'INATIVAÇÃO') ) THEN
        
-- -----CADASTRO ------------------------------------------------------------------------------------------------------------------------------------------------
			IF (var_operacao = 'CADASTRO') THEN
                
                IF (var_cod_Produtos_Servicos_Orcamento > 0) THEN
                
                    IF (var_cod_Materiais_Produto_Servico > 0) THEN
                    
						IF (var_cod_Material > 0) THEN
                        
							IF (var_cod_Unidade > 0) THEN
                            
								IF (var_cod_Grupo > 0) THEN
                                
									IF (var_cod_Fornecedor > 0) THEN
                                    
                                        IF (var_cod_Orcamento > 0) THEN
                                        
											START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
                                            
											INSERT INTO tbl_Materiais_Orcamento
											(quantidade_total, valor_total, cod_Produtos_Servicos_Orcamento, cod_Materiais_Produto_Servico, quantidade_Materiais_Produto_Servico, valor_unitario_Materiais_Produto_Servico, cod_Material, ultima_atualizacao_Material, imagem_Material, descricao_Material, altura_Material, largura_Material, comprimento_Material, valor_unitario_Material, ativo_inativo_Material, cod_Unidade, sigla_Unidade, descricao_Unidade, ativo_inativo_Unidade, cod_Grupo, descricao_Grupo, ativo_inativo_Grupo, cod_Fornecedor, nome_razao_social_Fornecedor, nome_fantasia_Fornecedor, ativo_inativo_Fornecedor, cod_Orcamento)
											VALUES
											(var_quantidade_total, var_valor_total, var_cod_Produtos_Servicos_Orcamento, var_cod_Materiais_Produto_Servico, var_quantidade_Materiais_Produto_Servico, var_valor_unitario_Materiais_Produto_Servico, var_cod_Material, var_ultima_atualizacao_Material, var_imagem_Material, var_descricao_Material, var_altura_Material, var_largura_Material, var_comprimento_Material, var_valor_unitario_Material, var_ativo_inativo_Material, var_cod_Unidade, var_sigla_Unidade, var_descricao_Unidade, var_ativo_inativo_Unidade, var_cod_Grupo, var_descricao_Grupo, var_ativo_inativo_Grupo, var_cod_Fornecedor, var_nome_razao_social_Fornecedor, var_nome_fantasia_Fornecedor, var_ativo_inativo_Fornecedor, var_cod_Orcamento);
											
											SELECT MAX(mo.codigo)
											INTO var_codigo
											FROM tbl_Materiais_Orcamento AS mo LIMIT 1;
                                            
											-- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
											IF ( var_controle_commit ) THEN
												COMMIT;
												SELECT var_codigo AS 'SUCESSO';
											ELSE
												ROLLBACK;
												SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
											END IF;
									
										ELSE
											SELECT 'A operação requisitada não pode ser realizada pois o Código da ORÇAMENTO informado não é válido.' AS 'ALERTA';
										END IF;
                                    
									ELSE
										SELECT 'A operação requisitada não pode ser realizada pois o Código da FORNECEDOR informado não é válido.' AS 'ALERTA';
									END IF;
								
								ELSE
									SELECT 'A operação requisitada não pode ser realizada pois o Código da GRUPO informado não é válido.' AS 'ALERTA';
								END IF;
							
							ELSE
								SELECT 'A operação requisitada não pode ser realizada pois o Código da UNIDADE informado não é válido.' AS 'ALERTA';
							END IF;
						
						ELSE
							SELECT 'A operação requisitada não pode ser realizada pois o Código da MATERIAL informado não é válido.' AS 'ALERTA';
						END IF;
						
					ELSE
						SELECT 'A operação requisitada não pode ser realizada pois o Código da MATERIAIS DO PRODUTO OU SERVIÇO informado não é válido.' AS 'ALERTA';
					END IF;
                        
				ELSE
					SELECT 'A operação requisitada não pode ser realizada pois o Código da PRODUTOS OU SERVIÇOS DO ORÇAMENTO informado não é válido.' AS 'ALERTA';
				END IF;
		
-- -----ALTERAÇÃO ------------------------------------------------------------------------------------------------------------------------------------------------
			ELSEIF (var_operacao = 'ALTERAÇÃO') THEN
            
				IF( var_codigo > 0 ) THEN
					
                    IF (var_cod_Produtos_Servicos_Orcamento > 0) THEN
						
                        IF (var_cod_Materiais_Produto_Servico > 0) THEN
							
							IF (var_cod_Material > 0) THEN
									
								IF (var_cod_Unidade > 0) THEN
									
									IF (var_cod_Grupo > 0) THEN
										
										IF (var_cod_Fornecedor > 0) THEN
											
                                            IF (var_cod_Orcamento > 0) THEN
												
												START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
												
												UPDATE tbl_Materiais_Orcamento
												SET
												quantidade_total = var_quantidade_total,
												valor_total = var_valor_total,
												cod_Produtos_Servicos_Orcamento = var_cod_Produtos_Servicos_Orcamento,
												cod_Materiais_Produto_Servico = var_cod_Materiais_Produto_Servico,
													quantidade_Materiais_Produto_Servico = var_quantidade_Materiais_Produto_Servico,
													valor_unitario_Materiais_Produto_Servico = var_valor_unitario_Materiais_Produto_Servico,
												cod_Material = var_cod_Material,
													ultima_atualizacao_Material = var_ultima_atualizacao_Material,
													imagem_Material = var_imagem_Material,
													descricao_Material = var_descricao_Material,
													altura_Material = var_altura_Material,
													largura_Material = var_largura_Material,
													comprimento_Material = var_comprimento_Material,
                                                    valor_unitario_Material = var_valor_unitario_Material,
													ativo_inativo_Material = var_ativo_inativo_Material,
												cod_Unidade = var_cod_Unidade,
													sigla_Unidade = var_sigla_Unidade,
													descricao_Unidade = var_descricao_Unidade,
													ativo_inativo_Unidade = var_ativo_inativo_Unidade,
												cod_Grupo = var_cod_Grupo,
													descricao_Grupo = var_descricao_Grupo,
													ativo_inativo_Grupo = var_ativo_inativo_Grupo,
												cod_Fornecedor = var_cod_Fornecedor,
													nome_razao_social_Fornecedor = var_nome_razao_social_Fornecedor,
													nome_fantasia_Fornecedor = var_nome_fantasia_Fornecedor,
													ativo_inativo_Fornecedor = var_ativo_inativo_Fornecedor,
												 cod_Orcamento = var_cod_Orcamento
												WHERE
												codigo = var_codigo AND cod_Produtos_Servicos_Orcamento = var_cod_Produtos_Servicos_Orcamento AND cod_Material = var_cod_Material AND cod_Orcamento = var_cod_Orcamento;
													
												-- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
												IF ( var_controle_commit ) THEN
													COMMIT;
													SELECT var_codigo AS 'SUCESSO';
												ELSE
													ROLLBACK;
													SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
												END IF;
											
                                            ELSE
												SELECT 'A operação requisitada não pode ser realizada pois o Código da ORÇAMENTO informado não é válido.' AS 'ALERTA';
											END IF;
                                            
										ELSE
											SELECT 'A operação requisitada não pode ser realizada pois o Código da FORNECEDOR informado não é válido.' AS 'ALERTA';
										END IF;
										
									ELSE
										SELECT 'A operação requisitada não pode ser realizada pois o Código da GRUPO informado não é válido.' AS 'ALERTA';
									END IF;
									
								ELSE
									SELECT 'A operação requisitada não pode ser realizada pois o Código da UNIDADE informado não é válido.' AS 'ALERTA';
								END IF;
								
							ELSE
								SELECT 'A operação requisitada não pode ser realizada pois o Código da MATERIAL informado não é válido.' AS 'ALERTA';
							END IF;
						
                        ELSE
							SELECT 'A operação requisitada não pode ser realizada pois o Código da MATERIAIS DO PRODUTO OU SERVIÇO informado não é válido.' AS 'ALERTA';
						END IF;
                        
                    ELSE
						SELECT 'A operação requisitada não pode ser realizada pois o Código da PRODUTOS OU SERVIÇOS DO ORÇAMENTO informado não é válido.' AS 'ALERTA';
					END IF;        
				
                ELSE
					SELECT 'A operação requisitada não pode ser realizada pois o Código informado não é válido.' AS 'ALERTA';
				END IF;

-- -----INATIVAÇÃO ------------------------------------------------------------------------------------------------------------------------------------------------
			ELSEIF (var_operacao = 'INATIVAÇÃO') THEN
            
				IF( var_codigo > 0 ) THEN
					
                    IF (var_cod_Produtos_Servicos_Orcamento > 0) THEN
						
                        IF (var_cod_Materiais_Produto_Servico > 0) THEN
                    
							IF (var_cod_Material > 0) THEN
									
								IF (var_cod_Unidade > 0) THEN
									
									IF (var_cod_Grupo > 0) THEN
										
										IF (var_cod_Fornecedor > 0) THEN
											
                                            IF (var_cod_Orcamento > 0) THEN
                                            
												START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
												
												DELETE FROM tbl_Materiais_Orcamento
												WHERE
												codigo = var_codigo AND cod_Produtos_Servicos_Orcamento = var_cod_Produtos_Servicos_Orcamento AND cod_Material = var_cod_Material AND cod_Orcamento = var_cod_Orcamento;
												
												-- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
												IF ( var_controle_commit ) THEN
													COMMIT;
													SELECT var_codigo AS 'SUCESSO';
												ELSE
													ROLLBACK;
													SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
												END IF;
											
                                            ELSE
												SELECT 'A operação requisitada não pode ser realizada pois o Código da ORÇAMENTO informado não é válido.' AS 'ALERTA';
											END IF;
                                            
										ELSE
											SELECT 'A operação requisitada não pode ser realizada pois o Código da FORNECEDOR informado não é válido.' AS 'ALERTA';
										END IF;
										
									ELSE
										SELECT 'A operação requisitada não pode ser realizada pois o Código da GRUPO informado não é válido.' AS 'ALERTA';
									END IF;
									
								ELSE
									SELECT 'A operação requisitada não pode ser realizada pois o Código da UNIDADE informado não é válido.' AS 'ALERTA';
								END IF;
								
							ELSE
								SELECT 'A operação requisitada não pode ser realizada pois o Código da MATERIAL informado não é válido.' AS 'ALERTA';
							END IF;
						
                        ELSE
							SELECT 'A operação requisitada não pode ser realizada pois o Código da MATERIAIS DO PRODUTO OU SERVIÇO informado não é válido.' AS 'ALERTA';
						END IF;
                        
					ELSE
						SELECT 'A operação requisitada não pode ser realizada pois o Código da PRODUTOS OU SERVIÇOS DO ORÇAMENTO informado não é válido.' AS 'ALERTA';
					END IF;
                
				ELSE
					SELECT 'A operação requisitada não pode ser realizada pois o Código informado não é válido.' AS 'ALERTA';
				END IF;
		
-- -----CASO NÃO TENHA INSERIDO NENHUMA OPERAÇÃO ----------------------------------------------------------------------------------------------------------------
			ELSE
				SELECT CONCAT('A operação ( ', IFNULL(var_operacao, 'NULL'), ' ) não é VÁLIDA.') AS 'ALERTA';
			END IF;
        
        ELSE
            SELECT CONCAT( 'A operação informada não é válida: ( ', IFNULL(var_operacao, 'NULL'), ' ).' ) AS 'ALERTA';
		END IF;
		
	END IF;
    
END $$
DELIMITER ;

-- PROCEDURE EXTRA, ALTERAR ORÇAMENTO PÓS PRODUTO OU SERVIÇO VINCULADO ----------------------------------------------------------------------------------------------------------------
DELIMITER $$
CREATE PROCEDURE sp_AlterarValores_Orcamento_Sem_Output(
    IN var_codigo INT,
    IN var_validade DATETIME,
    IN var_prazo_entrega VARCHAR(150),
    IN var_observacao VARCHAR(150),
    IN var_total_produtos_servicos DECIMAL(13,2),
    IN var_desconto DECIMAL(13,2),
    IN var_descricao_pagamento VARCHAR(150),
    IN var_valor_entrada DECIMAL(13,2),
    IN var_quantidade_parcelas INT,
    IN var_juros DECIMAL(13, 2),
    IN var_ativo_inativo BOOLEAN,
    IN var_cod_Usuario INT,
    IN var_cod_Cliente INT
)
BEGIN
	DECLARE var_resgistro_LOG CHAR(32);
	DECLARE var_informacoes_registro VARCHAR(1000);
    
    DECLARE var_ultima_atualizacao DATETIME;
    DECLARE var_total_orcamento DECIMAL(13,2);
    DECLARE var_valor_parcela DECIMAL(13,2);
    DECLARE var_valor_juros DECIMAL(13,2);
    
	DECLARE var_controle_commit TINYINT DEFAULT TRUE;		-- Cria uma var_controle_commit que vai ser usada para check se o commit pode ser realizado, ou dar rollback em um erro.
	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET var_controle_commit = FALSE;		-- Caso haja um erro na execução, a var_controle_commit receberá FALSE para acionar o rollback.
    
    SET var_resgistro_LOG = 'ORÇAMENTO';
    SET @@AUTOCOMMIT = OFF;	-- Desliga o commit automatico para que o "Try, Catch e rollback" possa funcionar corretamente e commitar manualmente.
    
    SET var_ultima_atualizacao = localtimestamp(0);
    SET var_total_orcamento = 0.00;
    SET var_valor_parcela =  0.00;
    SET var_valor_juros =  0.00;
    
    SET var_total_orcamento = var_total_produtos_servicos - (var_total_produtos_servicos * (var_desconto / 100));
    SET var_valor_parcela = (var_total_orcamento - var_valor_entrada) / var_quantidade_parcelas;
    SET var_valor_juros = var_valor_parcela + (var_valor_parcela * (var_juros / 100));
    
    START TRANSACTION;	-- Inicia as transações que receberam rollback caso haja algum erro que prejudique a sintax.
    
	UPDATE tbl_Orcamento
	SET
	ultima_atualizacao = var_ultima_atualizacao,
	validade = var_validade,
    prazo_entrega = var_prazo_entrega,
    observacao = var_observacao,
    total_produtos_servicos = var_total_produtos_servicos,
    desconto = var_desconto,
    total_orcamento = var_total_orcamento,
    descricao_pagamento = var_descricao_pagamento,
    valor_entrada = var_valor_entrada,
    quantidade_parcelas = var_quantidade_parcelas,
    valor_parcela = var_valor_parcela,
    juros = var_juros,
    valor_juros = var_valor_juros,
    cod_Usuario = var_cod_Usuario,
    cod_Cliente = var_cod_Cliente,
    ativo_inativo = var_ativo_inativo
	WHERE
	codigo = var_codigo;
    
	SET var_informacoes_registro = CONCAT('Código: ( ', IFNULL(var_codigo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
		'Ultima Atualização: ( ', IFNULL(var_ultima_atualizacao, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
		'Válidade: ( ', IFNULL(var_validade, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
		'Prazo Entrega: ( ', IFNULL(var_prazo_entrega, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
		'Observação: ( ', IFNULL(var_observacao, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
		'Valor Total Produtos e Serviços: ( ', IFNULL(var_total_produtos_servicos, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
		'Valor Desconto: ( ', IFNULL(var_desconto, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
		'Valor Total Orçamento: ( ', IFNULL(var_total_orcamento, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
		'Descrição Pagamento: ( ', IFNULL(var_descricao_pagamento, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
		'Valor Entrada: ( ', IFNULL(var_valor_entrada, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
		'Quantidade Parcelas: ( ', IFNULL(var_quantidade_parcelas, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
		'Valor Parcela: ( ', IFNULL(var_valor_parcela, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
		'Juros: ( ', IFNULL(var_juros, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
		'Valor Juros: ( ', IFNULL(var_valor_juros, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
		'Registro Ativo: ( ', IFNULL(var_ativo_inativo, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n \r\n',
		'Código Usuario: ( ', IFNULL(var_cod_Usuario, 'ERROR: INFORMAÇÃO NULL'), ' ). \r\n',
		'Código Cliente: ( ', IFNULL(var_cod_Cliente, 'ERROR: INFORMAÇÃO NULL'), ' ).');
        
	CALL sp_GerarLOG('ALTERAÇÃO', var_resgistro_LOG, var_informacoes_registro);
    
	-- Se o processo foi um sucesso, a variavel var_controle_commit estará TRUE e poderá realizar o commit;
	IF ( var_controle_commit ) THEN
		COMMIT;
	ELSE
		ROLLBACK;
        SELECT 'Erro na transação de dados, operação não pode ser realizada.' AS 'ALERTA';
	END IF;
    
END $$
DELIMITER ;