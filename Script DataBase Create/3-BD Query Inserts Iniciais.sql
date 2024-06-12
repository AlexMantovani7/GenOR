-- --------------------------------INSERINDO VALORES INICIAIS----------------------------------
USE GenOR_BD;

-- PESSOA -------------------------------------------------------------------------------------------------------------------------------------------------------------
INSERT INTO tbl_Pessoa (tipo_pessoa, nome_razao_social, nome_fantasia, cpf_cnpj, inscricao_estadual, email, observacao, ativo_inativo)
VALUES ('USUARIO', 'USUÁRIO PADRÃO', 'USUARIO PADRÃO', '', '', '', '', TRUE);
-- CALL sp_ManterPessoa('CADASTRO', 0, 'USUARIO', 'USUÁRIO PADRÃO', 'USUARIO PADRÃO', '', '', '', '', TRUE);

-- LOGIN -------------------------------------------------------------------------------------------------------------------------------------------------------------
INSERT INTO tbl_Login (nome_usuario, senha, cod_Usuario) VALUES ('admin', 'admin', 1);
-- CALL sp_ManterLogin('CADASTRO', 0, 'admin', 'admin', 1);

-- GRUPO -------------------------------------------------------------------------------------------------------------------------------------------------------------
INSERT INTO tbl_Grupo (descricao, material_ou_produto, ativo_inativo) VALUES ('PADRÃO', 'M', TRUE);
-- CALL sp_ManterGrupo('CADASTRO', 0, 'PADRÃO', 'M', TRUE);
INSERT INTO tbl_Grupo (descricao, material_ou_produto, ativo_inativo) VALUES ('PADRÃO', 'P', TRUE);
-- CALL sp_ManterGrupo('CADASTRO', 0, 'PADRÃO', 'P', TRUE);

-- UNIDADE -------------------------------------------------------------------------------------------------------------------------------------------------------------
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('PADRÃO', 'PADRÃO', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'PADRÃO', 'PADRÃO', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('un', 'Unidade', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'un', 'Unidade', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('pç', 'Peça', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'pç', 'Peça', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('gp', 'Grupo', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'gp', 'Grupo', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('jog', 'Jogo', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'jog', 'Jogo', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('cx', 'Caixa', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'cx', 'Caixa', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('env', 'Envelope', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'env', 'Envelope', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('lt', 'Lata', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'lt', 'Lata', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('conj', 'Conjunto', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'conj', 'Conjunto', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('rem', 'Remessa', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'rem', 'Remessa', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('mm', 'Milimetro', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'mm', 'Milimetro', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('cm', 'Centimetro', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'cm', 'Centimetro', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('m', 'Metro', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'm', 'Metro', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('m²', 'Metro quadrado', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'm²', 'Metro quadrado', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('m³', 'Metro cúbico', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'm³', 'Metro cúbico', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('m/seg', 'Metro por segundo', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'm/seg', 'Metro por segundo', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('m/min', 'Metro por minuto', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'm/min', 'Metro por minuto', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('m/h', 'Metro por hora', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'm/h', 'Metro por hora', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('Km', 'Kilometro', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'Km', 'Kilometro', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('Km/seg', 'Kilometro por segundo', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'Km/seg', 'Kilometro por segundo', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('Km/min', 'Kilometro por minuto', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'Km/min', 'Kilometro por minuto', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('Km/h', 'Kilometro por hora', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'Km/h', 'Kilometro por hora', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('ft', 'Feet', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'ft', 'Feet', TRUE);

INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('pol', 'Polegada', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'pol', 'Polegada', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('r', 'Raio', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'r', 'Raio', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('Φ', 'Diametro', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'Φ', 'Diametro', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('π', 'pi', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'π', 'pi', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('ml', 'Mililitro', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'ml', 'Mililitro', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('ml/m²', 'Mililitro por metro quadrado', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'ml/m²', 'Mililitro por metro quadrado', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('ml/m³', 'Mililitro por metro cúbico', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'ml/m³', 'Mililitro por metro cúbico', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('L', 'Litro', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'L', 'Litro', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('L/m²', 'Litro por metro quadrado', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'L/m²', 'Litro por metro quadrado', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('L/m³', 'Litro por metro cúbico', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'L/m³', 'Litro por metro cúbico', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('g', 'Grama', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'g', 'Grama', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('Kg', 'Quilograma', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'Kg', 'Quilograma', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('Kg/m²', 'Quilograma por metro quadrado', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'Kg/m²', 'Quilograma por metro quadrado', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('Kg/m³', 'Quilograma por metro cúbico', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'Kg/m³', 'Quilograma por metro cúbico', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('ms', 'Milisegundo', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'ms', 'Milisegundo', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('seg', 'Segundo', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'seg', 'Segundo', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('min', 'Minuto', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'min', 'Minuto', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('h', 'Hora', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'h', 'Hora', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('A', 'Ampère', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'A', 'Ampère', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('W', 'Watt', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'W', 'Watt', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('kW', 'Kilowatt', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'kW', 'Kilowatt', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('J', 'Joule', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'J', 'Joule', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('V', 'Volts', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'V', 'Volts', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('kVA', 'Quilovolt ampère', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'kVA', 'Quilovolt ampère', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('kWh', 'Quilowatt por hora', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'kWh', 'Quilowatt por hora', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('kvarh', 'Quilo ampère reativo por hora', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, 'kvarh', 'Quilo ampère reativo por hora', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('°C', 'Celsius', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, '°C', 'Celsius', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('°F', 'Fahrenheit', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, '°F', 'Fahrenheit', TRUE);
INSERT INTO tbl_Unidade (sigla, descricao, ativo_inativo) VALUES ('°K', 'Kelvin', TRUE);
-- CALL sp_ManterUnidade('CADASTRO', 0, '°K', 'Kelvin', TRUE);
-- --------------------------------------------------------------------------------------------------------------------------------------------------------------------