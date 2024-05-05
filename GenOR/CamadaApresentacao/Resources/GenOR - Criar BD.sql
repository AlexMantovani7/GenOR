-- MySqlBackup.NET 2.3.8.0
-- Dump Time: 2024-02-02 17:39:04
-- --------------------------------------
-- Server version 8.0.31 MySQL Community Server - GPL


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- 
-- Definition of tbl_grupo
-- 

DROP TABLE IF EXISTS `tbl_grupo`;
CREATE TABLE IF NOT EXISTS `tbl_grupo` (
  `codigo` int NOT NULL AUTO_INCREMENT,
  `descricao` varchar(150) NOT NULL,
  `material_ou_produto` char(1) NOT NULL,
  `ativo_inativo` tinyint(1) NOT NULL,
  PRIMARY KEY (`codigo`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table tbl_grupo
-- 

/*!40000 ALTER TABLE `tbl_grupo` DISABLE KEYS */;
INSERT INTO `tbl_grupo`(`codigo`,`descricao`,`material_ou_produto`,`ativo_inativo`) VALUES(1,'PADRÃO','M',1),(2,'PADRÃO','P',1);
/*!40000 ALTER TABLE `tbl_grupo` ENABLE KEYS */;

-- 
-- Definition of tbl_log
-- 

DROP TABLE IF EXISTS `tbl_log`;
CREATE TABLE IF NOT EXISTS `tbl_log` (
  `codigo` int NOT NULL AUTO_INCREMENT,
  `data_registro` datetime NOT NULL,
  `operacao` char(11) NOT NULL,
  `registro` char(32) NOT NULL,
  `informacoes_registro` varchar(1000) NOT NULL,
  PRIMARY KEY (`codigo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table tbl_log
-- 

/*!40000 ALTER TABLE `tbl_log` DISABLE KEYS */;

/*!40000 ALTER TABLE `tbl_log` ENABLE KEYS */;

-- 
-- Definition of tbl_pessoa
-- 

DROP TABLE IF EXISTS `tbl_pessoa`;
CREATE TABLE IF NOT EXISTS `tbl_pessoa` (
  `codigo` int NOT NULL AUTO_INCREMENT,
  `tipo_pessoa` char(10) NOT NULL,
  `nome_razao_social` varchar(150) NOT NULL,
  `nome_fantasia` varchar(150) NOT NULL,
  `cpf_cnpj` char(14) NOT NULL,
  `inscricao_estadual` char(12) NOT NULL,
  `email` varchar(256) NOT NULL,
  `observacao` varchar(150) NOT NULL,
  `ativo_inativo` tinyint(1) NOT NULL,
  PRIMARY KEY (`codigo`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table tbl_pessoa
-- 

/*!40000 ALTER TABLE `tbl_pessoa` DISABLE KEYS */;
INSERT INTO `tbl_pessoa`(`codigo`,`tipo_pessoa`,`nome_razao_social`,`nome_fantasia`,`cpf_cnpj`,`inscricao_estadual`,`email`,`observacao`,`ativo_inativo`) VALUES(1,'USUARIO','USUÁRIO PADRÃO','USUARIO PADRÃO','','','','',1);
/*!40000 ALTER TABLE `tbl_pessoa` ENABLE KEYS */;

-- 
-- Definition of tbl_endereco
-- 

DROP TABLE IF EXISTS `tbl_endereco`;
CREATE TABLE IF NOT EXISTS `tbl_endereco` (
  `codigo` int NOT NULL AUTO_INCREMENT,
  `endereco` varchar(150) NOT NULL,
  `complemento` varchar(150) NOT NULL,
  `numero` char(12) NOT NULL,
  `bairro` varchar(150) NOT NULL,
  `cidade` varchar(31) NOT NULL,
  `estado` char(2) NOT NULL,
  `cep` char(8) NOT NULL,
  `observacao` varchar(150) NOT NULL,
  `ativo_inativo` tinyint(1) NOT NULL,
  `cod_Pessoa` int NOT NULL,
  PRIMARY KEY (`codigo`),
  KEY `cod_Pessoa` (`cod_Pessoa`),
  CONSTRAINT `tbl_endereco_ibfk_1` FOREIGN KEY (`cod_Pessoa`) REFERENCES `tbl_pessoa` (`codigo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table tbl_endereco
-- 

/*!40000 ALTER TABLE `tbl_endereco` DISABLE KEYS */;

/*!40000 ALTER TABLE `tbl_endereco` ENABLE KEYS */;

-- 
-- Definition of tbl_login
-- 

DROP TABLE IF EXISTS `tbl_login`;
CREATE TABLE IF NOT EXISTS `tbl_login` (
  `codigo` int NOT NULL AUTO_INCREMENT,
  `nome_usuario` varchar(256) NOT NULL,
  `senha` varchar(50) NOT NULL,
  `cod_Usuario` int NOT NULL,
  PRIMARY KEY (`codigo`),
  KEY `cod_Usuario` (`cod_Usuario`),
  CONSTRAINT `tbl_login_ibfk_1` FOREIGN KEY (`cod_Usuario`) REFERENCES `tbl_pessoa` (`codigo`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table tbl_login
-- 

/*!40000 ALTER TABLE `tbl_login` DISABLE KEYS */;
INSERT INTO `tbl_login`(`codigo`,`nome_usuario`,`senha`,`cod_Usuario`) VALUES(1,'admin','admin',1);
/*!40000 ALTER TABLE `tbl_login` ENABLE KEYS */;

-- 
-- Definition of tbl_material
-- 

DROP TABLE IF EXISTS `tbl_material`;
CREATE TABLE IF NOT EXISTS `tbl_material` (
  `codigo` int NOT NULL AUTO_INCREMENT,
  `ultima_atualizacao` datetime NOT NULL,
  `imagem` varchar(260) NOT NULL,
  `descricao` varchar(150) NOT NULL,
  `altura` decimal(13,2) NOT NULL,
  `largura` decimal(13,2) NOT NULL,
  `comprimento` decimal(13,2) NOT NULL,
  `valor_unitario` decimal(13,2) NOT NULL,
  `ativo_inativo` tinyint(1) NOT NULL,
  `cod_Unidade` int NOT NULL,
  `cod_Grupo` int NOT NULL,
  `cod_Fornecedor` int NOT NULL,
  PRIMARY KEY (`codigo`),
  KEY `cod_Unidade` (`cod_Unidade`),
  KEY `cod_Grupo` (`cod_Grupo`),
  KEY `cod_Fornecedor` (`cod_Fornecedor`),
  CONSTRAINT `tbl_material_ibfk_1` FOREIGN KEY (`cod_Unidade`) REFERENCES `tbl_unidade` (`codigo`),
  CONSTRAINT `tbl_material_ibfk_2` FOREIGN KEY (`cod_Grupo`) REFERENCES `tbl_grupo` (`codigo`),
  CONSTRAINT `tbl_material_ibfk_3` FOREIGN KEY (`cod_Fornecedor`) REFERENCES `tbl_pessoa` (`codigo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table tbl_material
-- 

/*!40000 ALTER TABLE `tbl_material` DISABLE KEYS */;

/*!40000 ALTER TABLE `tbl_material` ENABLE KEYS */;

-- 
-- Definition of tbl_orcamento
-- 

DROP TABLE IF EXISTS `tbl_orcamento`;
CREATE TABLE IF NOT EXISTS `tbl_orcamento` (
  `codigo` int NOT NULL AUTO_INCREMENT,
  `ultima_atualizacao` datetime NOT NULL,
  `validade` datetime NOT NULL,
  `prazo_entrega` varchar(150) NOT NULL,
  `observacao` varchar(150) NOT NULL,
  `total_produtos_servicos` decimal(13,2) NOT NULL,
  `desconto` decimal(13,2) NOT NULL,
  `total_orcamento` decimal(13,2) NOT NULL,
  `descricao_pagamento` varchar(150) NOT NULL,
  `valor_entrada` decimal(13,2) NOT NULL,
  `quantidade_parcelas` int NOT NULL,
  `valor_parcela` decimal(13,2) NOT NULL,
  `juros` decimal(13,2) NOT NULL,
  `valor_juros` decimal(13,2) NOT NULL,
  `ativo_inativo` tinyint(1) NOT NULL,
  `cod_Usuario` int NOT NULL,
  `cod_Cliente` int NOT NULL,
  PRIMARY KEY (`codigo`),
  KEY `cod_Usuario` (`cod_Usuario`),
  KEY `cod_Cliente` (`cod_Cliente`),
  CONSTRAINT `tbl_orcamento_ibfk_1` FOREIGN KEY (`cod_Usuario`) REFERENCES `tbl_pessoa` (`codigo`),
  CONSTRAINT `tbl_orcamento_ibfk_2` FOREIGN KEY (`cod_Cliente`) REFERENCES `tbl_pessoa` (`codigo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table tbl_orcamento
-- 

/*!40000 ALTER TABLE `tbl_orcamento` DISABLE KEYS */;

/*!40000 ALTER TABLE `tbl_orcamento` ENABLE KEYS */;

-- 
-- Definition of tbl_materiais_orcamento
-- 

DROP TABLE IF EXISTS `tbl_materiais_orcamento`;
CREATE TABLE IF NOT EXISTS `tbl_materiais_orcamento` (
  `codigo` int NOT NULL AUTO_INCREMENT,
  `quantidade_total` decimal(13,2) NOT NULL,
  `valor_total` decimal(13,2) NOT NULL,
  `cod_Produtos_Servicos_Orcamento` int NOT NULL,
  `cod_Materiais_Produto_Servico` int NOT NULL,
  `quantidade_Materiais_Produto_Servico` decimal(13,2) NOT NULL,
  `valor_unitario_Materiais_Produto_Servico` decimal(13,2) NOT NULL,
  `cod_Material` int NOT NULL,
  `ultima_atualizacao_Material` datetime NOT NULL,
  `imagem_Material` varchar(260) NOT NULL,
  `descricao_Material` varchar(150) NOT NULL,
  `altura_Material` decimal(13,2) NOT NULL,
  `largura_Material` decimal(13,2) NOT NULL,
  `comprimento_Material` decimal(13,2) NOT NULL,
  `valor_unitario_Material` decimal(13,2) NOT NULL,
  `ativo_inativo_Material` tinyint(1) NOT NULL,
  `cod_Unidade` int NOT NULL,
  `sigla_Unidade` char(8) NOT NULL,
  `descricao_Unidade` varchar(150) NOT NULL,
  `ativo_inativo_Unidade` tinyint(1) NOT NULL,
  `cod_Grupo` int NOT NULL,
  `descricao_Grupo` varchar(150) NOT NULL,
  `ativo_inativo_Grupo` tinyint(1) NOT NULL,
  `cod_Fornecedor` int NOT NULL,
  `nome_razao_social_Fornecedor` varchar(150) NOT NULL,
  `nome_fantasia_Fornecedor` varchar(150) NOT NULL,
  `ativo_inativo_Fornecedor` tinyint(1) NOT NULL,
  `cod_Orcamento` int NOT NULL,
  PRIMARY KEY (`codigo`),
  KEY `cod_Produtos_Servicos_Orcamento` (`cod_Produtos_Servicos_Orcamento`),
  KEY `cod_Materiais_Produto_Servico` (`cod_Materiais_Produto_Servico`),
  KEY `cod_Material` (`cod_Material`),
  KEY `cod_Unidade` (`cod_Unidade`),
  KEY `cod_Grupo` (`cod_Grupo`),
  KEY `cod_Fornecedor` (`cod_Fornecedor`),
  KEY `cod_Orcamento` (`cod_Orcamento`),
  CONSTRAINT `tbl_materiais_orcamento_ibfk_1` FOREIGN KEY (`cod_Produtos_Servicos_Orcamento`) REFERENCES `tbl_produtos_servicos_orcamento` (`codigo`),
  CONSTRAINT `tbl_materiais_orcamento_ibfk_2` FOREIGN KEY (`cod_Materiais_Produto_Servico`) REFERENCES `tbl_materiais_produto_servico` (`codigo`),
  CONSTRAINT `tbl_materiais_orcamento_ibfk_3` FOREIGN KEY (`cod_Material`) REFERENCES `tbl_material` (`codigo`),
  CONSTRAINT `tbl_materiais_orcamento_ibfk_4` FOREIGN KEY (`cod_Unidade`) REFERENCES `tbl_unidade` (`codigo`),
  CONSTRAINT `tbl_materiais_orcamento_ibfk_5` FOREIGN KEY (`cod_Grupo`) REFERENCES `tbl_grupo` (`codigo`),
  CONSTRAINT `tbl_materiais_orcamento_ibfk_6` FOREIGN KEY (`cod_Fornecedor`) REFERENCES `tbl_pessoa` (`codigo`),
  CONSTRAINT `tbl_materiais_orcamento_ibfk_7` FOREIGN KEY (`cod_Orcamento`) REFERENCES `tbl_orcamento` (`codigo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table tbl_materiais_orcamento
-- 

/*!40000 ALTER TABLE `tbl_materiais_orcamento` DISABLE KEYS */;

/*!40000 ALTER TABLE `tbl_materiais_orcamento` ENABLE KEYS */;

-- 
-- Definition of tbl_produto_servico
-- 

DROP TABLE IF EXISTS `tbl_produto_servico`;
CREATE TABLE IF NOT EXISTS `tbl_produto_servico` (
  `codigo` int NOT NULL AUTO_INCREMENT,
  `ultima_atualizacao` datetime NOT NULL,
  `imagem` varchar(260) NOT NULL,
  `descricao` varchar(150) NOT NULL,
  `altura` decimal(13,2) NOT NULL,
  `largura` decimal(13,2) NOT NULL,
  `comprimento` decimal(13,2) NOT NULL,
  `valor_total_materiais` decimal(13,2) NOT NULL,
  `maoObra` decimal(13,2) NOT NULL,
  `valor_maoObra` decimal(13,2) NOT NULL,
  `valor_total` decimal(13,2) NOT NULL,
  `ativo_inativo` tinyint(1) NOT NULL,
  `cod_Unidade` int NOT NULL,
  `cod_Grupo` int NOT NULL,
  PRIMARY KEY (`codigo`),
  KEY `cod_Unidade` (`cod_Unidade`),
  KEY `cod_Grupo` (`cod_Grupo`),
  CONSTRAINT `tbl_produto_servico_ibfk_1` FOREIGN KEY (`cod_Unidade`) REFERENCES `tbl_unidade` (`codigo`),
  CONSTRAINT `tbl_produto_servico_ibfk_2` FOREIGN KEY (`cod_Grupo`) REFERENCES `tbl_grupo` (`codigo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table tbl_produto_servico
-- 

/*!40000 ALTER TABLE `tbl_produto_servico` DISABLE KEYS */;

/*!40000 ALTER TABLE `tbl_produto_servico` ENABLE KEYS */;

-- 
-- Definition of tbl_materiais_produto_servico
-- 

DROP TABLE IF EXISTS `tbl_materiais_produto_servico`;
CREATE TABLE IF NOT EXISTS `tbl_materiais_produto_servico` (
  `codigo` int NOT NULL AUTO_INCREMENT,
  `quantidade` decimal(13,2) NOT NULL,
  `valor_total` decimal(13,2) NOT NULL,
  `ativo_inativo` tinyint(1) NOT NULL,
  `cod_Material` int NOT NULL,
  `cod_Produto_Servico` int NOT NULL,
  PRIMARY KEY (`codigo`),
  KEY `cod_Material` (`cod_Material`),
  KEY `cod_Produto_Servico` (`cod_Produto_Servico`),
  CONSTRAINT `tbl_materiais_produto_servico_ibfk_1` FOREIGN KEY (`cod_Material`) REFERENCES `tbl_material` (`codigo`),
  CONSTRAINT `tbl_materiais_produto_servico_ibfk_2` FOREIGN KEY (`cod_Produto_Servico`) REFERENCES `tbl_produto_servico` (`codigo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table tbl_materiais_produto_servico
-- 

/*!40000 ALTER TABLE `tbl_materiais_produto_servico` DISABLE KEYS */;

/*!40000 ALTER TABLE `tbl_materiais_produto_servico` ENABLE KEYS */;

-- 
-- Definition of tbl_produtos_servicos_orcamento
-- 

DROP TABLE IF EXISTS `tbl_produtos_servicos_orcamento`;
CREATE TABLE IF NOT EXISTS `tbl_produtos_servicos_orcamento` (
  `codigo` int NOT NULL AUTO_INCREMENT,
  `quantidade` decimal(13,2) NOT NULL,
  `valor_total` decimal(13,2) NOT NULL,
  `cod_Produto_Servico` int NOT NULL,
  `ultima_atualizacao_Produto_Servico` datetime NOT NULL,
  `imagem_Produto_Servico` varchar(260) NOT NULL,
  `descricao_Produto_Servico` varchar(150) NOT NULL,
  `altura_Produto_Servico` decimal(13,2) NOT NULL,
  `largura_Produto_Servico` decimal(13,2) NOT NULL,
  `comprimento_Produto_Servico` decimal(13,2) NOT NULL,
  `valor_total_materiais_Produto_Servico` decimal(13,2) NOT NULL,
  `maoObra_Produto_Servico` decimal(13,2) NOT NULL,
  `valor_maoObra_Produto_Servico` decimal(13,2) NOT NULL,
  `valor_unitario_Produto_Servico` decimal(13,2) NOT NULL,
  `ativo_inativo_Produto_Servico` tinyint(1) NOT NULL,
  `cod_Unidade` int NOT NULL,
  `sigla_Unidade` char(8) NOT NULL,
  `descricao_Unidade` varchar(150) NOT NULL,
  `ativo_inativo_Unidade` tinyint(1) NOT NULL,
  `cod_Grupo` int NOT NULL,
  `descricao_Grupo` varchar(150) NOT NULL,
  `ativo_inativo_Grupo` tinyint(1) NOT NULL,
  `cod_Orcamento` int NOT NULL,
  PRIMARY KEY (`codigo`),
  KEY `cod_Produto_Servico` (`cod_Produto_Servico`),
  KEY `cod_Unidade` (`cod_Unidade`),
  KEY `cod_Grupo` (`cod_Grupo`),
  KEY `cod_Orcamento` (`cod_Orcamento`),
  CONSTRAINT `tbl_produtos_servicos_orcamento_ibfk_1` FOREIGN KEY (`cod_Produto_Servico`) REFERENCES `tbl_produto_servico` (`codigo`),
  CONSTRAINT `tbl_produtos_servicos_orcamento_ibfk_2` FOREIGN KEY (`cod_Unidade`) REFERENCES `tbl_unidade` (`codigo`),
  CONSTRAINT `tbl_produtos_servicos_orcamento_ibfk_3` FOREIGN KEY (`cod_Grupo`) REFERENCES `tbl_grupo` (`codigo`),
  CONSTRAINT `tbl_produtos_servicos_orcamento_ibfk_4` FOREIGN KEY (`cod_Orcamento`) REFERENCES `tbl_orcamento` (`codigo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table tbl_produtos_servicos_orcamento
-- 

/*!40000 ALTER TABLE `tbl_produtos_servicos_orcamento` DISABLE KEYS */;

/*!40000 ALTER TABLE `tbl_produtos_servicos_orcamento` ENABLE KEYS */;

-- 
-- Definition of tbl_telefone
-- 

DROP TABLE IF EXISTS `tbl_telefone`;
CREATE TABLE IF NOT EXISTS `tbl_telefone` (
  `codigo` int NOT NULL AUTO_INCREMENT,
  `ddd` char(2) NOT NULL,
  `numero` char(11) NOT NULL,
  `observacao` varchar(150) NOT NULL,
  `ativo_inativo` tinyint(1) NOT NULL,
  `cod_Pessoa` int NOT NULL,
  PRIMARY KEY (`codigo`),
  KEY `cod_Pessoa` (`cod_Pessoa`),
  CONSTRAINT `tbl_telefone_ibfk_1` FOREIGN KEY (`cod_Pessoa`) REFERENCES `tbl_pessoa` (`codigo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table tbl_telefone
-- 

/*!40000 ALTER TABLE `tbl_telefone` DISABLE KEYS */;

/*!40000 ALTER TABLE `tbl_telefone` ENABLE KEYS */;

-- 
-- Definition of tbl_unidade
-- 

DROP TABLE IF EXISTS `tbl_unidade`;
CREATE TABLE IF NOT EXISTS `tbl_unidade` (
  `codigo` int NOT NULL AUTO_INCREMENT,
  `sigla` char(8) NOT NULL,
  `descricao` varchar(150) NOT NULL,
  `ativo_inativo` tinyint(1) NOT NULL,
  PRIMARY KEY (`codigo`)
) ENGINE=InnoDB AUTO_INCREMENT=53 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table tbl_unidade
-- 

/*!40000 ALTER TABLE `tbl_unidade` DISABLE KEYS */;
INSERT INTO `tbl_unidade`(`codigo`,`sigla`,`descricao`,`ativo_inativo`) VALUES(1,'PADRÃO','PADRÃO',1),(2,'un','Unidade',1),(3,'pç','Peça',1),(4,'gp','Grupo',1),(5,'jog','Jogo',1),(6,'cx','Caixa',1),(7,'env','Envelope',1),(8,'lt','Lata',1),(9,'conj','Conjunto',1),(10,'rem','Remessa',1),(11,'mm','Milimetro',1),(12,'cm','Centimetro',1),(13,'m','Metro',1),(14,'m²','Metro quadrado',1),(15,'m³','Metro cúbico',1),(16,'m/seg','Metro por segundo',1),(17,'m/min','Metro por minuto',1),(18,'m/h','Metro por hora',1),(19,'Km','Kilometro',1),(20,'Km/seg','Kilometro por segundo',1),(21,'Km/min','Kilometro por minuto',1),(22,'Km/h','Kilometro por hora',1),(23,'ft','Feet',1),(24,'pol','Polegada',1),(25,'r','Raio',1),(26,'Φ','Diametro',1),(27,'π','pi',1),(28,'ml','Mililitro',1),(29,'ml/m²','Mililitro por metro quadrado',1),(30,'ml/m³','Mililitro por metro cúbico',1),(31,'L','Litro',1),(32,'L/m²','Litro por metro quadrado',1),(33,'L/m³','Litro por metro cúbico',1),(34,'g','Grama',1),(35,'Kg','Quilograma',1),(36,'Kg/m²','Quilograma por metro quadrado',1),(37,'Kg/m³','Quilograma por metro cúbico',1),(38,'ms','Milisegundo',1),(39,'seg','Segundo',1),(40,'min','Minuto',1),(41,'h','Hora',1),(42,'A','Ampère',1),(43,'W','Watt',1),(44,'kW','Kilowatt',1),(45,'J','Joule',1),(46,'V','Volts',1),(47,'kVA','Quilovolt ampère',1),(48,'kWh','Quilowatt por hora',1),(49,'kvarh','Quilo ampère reativo por hora',1),(50,'°C','Celsius',1),(51,'°F','Fahrenheit',1),(52,'°K','Kelvin',1);
/*!40000 ALTER TABLE `tbl_unidade` ENABLE KEYS */;

-- 
-- Dumping procedures
-- 

DROP PROCEDURE IF EXISTS `sp_AlterarValores_Orcamento_Sem_Output`;
DELIMITER |
CREATE PROCEDURE `sp_AlterarValores_Orcamento_Sem_Output`(
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
    
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `sp_ConsultarEndereco`;
DELIMITER |
CREATE PROCEDURE `sp_ConsultarEndereco`(
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
            
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `sp_ConsultarGrupo`;
DELIMITER |
CREATE PROCEDURE `sp_ConsultarGrupo`(
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
            
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `sp_ConsultarLOG`;
DELIMITER |
CREATE PROCEDURE `sp_ConsultarLOG`(
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
            
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `sp_ConsultarLogin`;
DELIMITER |
CREATE PROCEDURE `sp_ConsultarLogin`(
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
    
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `sp_ConsultarMateriais_Orcamento`;
DELIMITER |
CREATE PROCEDURE `sp_ConsultarMateriais_Orcamento`(
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
            
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `sp_ConsultarMateriais_Produto_Servico`;
DELIMITER |
CREATE PROCEDURE `sp_ConsultarMateriais_Produto_Servico`(
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
            
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `sp_ConsultarMaterial`;
DELIMITER |
CREATE PROCEDURE `sp_ConsultarMaterial`(
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
            
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `sp_ConsultarOrcamento`;
DELIMITER |
CREATE PROCEDURE `sp_ConsultarOrcamento`(
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
            
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `sp_ConsultarPessoa`;
DELIMITER |
CREATE PROCEDURE `sp_ConsultarPessoa`(
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
            
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `sp_ConsultarProdutos_Servicos_Orcamento`;
DELIMITER |
CREATE PROCEDURE `sp_ConsultarProdutos_Servicos_Orcamento`(
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
            
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `sp_ConsultarProduto_Servico`;
DELIMITER |
CREATE PROCEDURE `sp_ConsultarProduto_Servico`(
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
            
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `sp_ConsultarTelefone`;
DELIMITER |
CREATE PROCEDURE `sp_ConsultarTelefone`(
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
            
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `sp_ConsultarUnidade`;
DELIMITER |
CREATE PROCEDURE `sp_ConsultarUnidade`(
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
            
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `sp_GerarLOG`;
DELIMITER |
CREATE PROCEDURE `sp_GerarLOG`(
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
    
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `sp_ManterEndereco`;
DELIMITER |
CREATE PROCEDURE `sp_ManterEndereco`(
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
    
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `sp_ManterGrupo`;
DELIMITER |
CREATE PROCEDURE `sp_ManterGrupo`(
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
    
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `sp_ManterLogin`;
DELIMITER |
CREATE PROCEDURE `sp_ManterLogin`(
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
    
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `sp_ManterMateriais_Orcamento`;
DELIMITER |
CREATE PROCEDURE `sp_ManterMateriais_Orcamento`(
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
    
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `sp_ManterMateriais_Produto_Servico`;
DELIMITER |
CREATE PROCEDURE `sp_ManterMateriais_Produto_Servico`(
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
    
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `sp_ManterMaterial`;
DELIMITER |
CREATE PROCEDURE `sp_ManterMaterial`(
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
    
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `sp_ManterOrcamento`;
DELIMITER |
CREATE PROCEDURE `sp_ManterOrcamento`(
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
    
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `sp_ManterPessoa`;
DELIMITER |
CREATE PROCEDURE `sp_ManterPessoa`(
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
    
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `sp_ManterProdutos_Servicos_Orcamento`;
DELIMITER |
CREATE PROCEDURE `sp_ManterProdutos_Servicos_Orcamento`(
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
    
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `sp_ManterProduto_Servico`;
DELIMITER |
CREATE PROCEDURE `sp_ManterProduto_Servico`(
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
    
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `sp_ManterTelefone`;
DELIMITER |
CREATE PROCEDURE `sp_ManterTelefone`(
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
    
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `sp_ManterUnidade`;
DELIMITER |
CREATE PROCEDURE `sp_ManterUnidade`(
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
    
END |
DELIMITER ;

-- 
-- Dumping views
-- 

DROP TABLE IF EXISTS `vw_endereco`;
DROP VIEW IF EXISTS `vw_endereco`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `vw_endereco` AS select `e`.`codigo` AS `codigo`,`e`.`endereco` AS `endereco`,`e`.`complemento` AS `complemento`,`e`.`numero` AS `numero`,`e`.`bairro` AS `bairro`,`e`.`cidade` AS `cidade`,`e`.`estado` AS `estado`,`e`.`cep` AS `cep`,`e`.`observacao` AS `observacao`,`e`.`ativo_inativo` AS `ativo_inativo_Endereco`,`e`.`cod_Pessoa` AS `cod_Pessoa`,`p`.`tipo_pessoa` AS `tipo_pessoa`,`p`.`nome_razao_social` AS `nome_razao_social`,`p`.`nome_fantasia` AS `nome_fantasia`,`p`.`cpf_cnpj` AS `cpf_cnpj`,`p`.`inscricao_estadual` AS `inscricao_estadual`,`p`.`email` AS `email`,`p`.`observacao` AS `observacao_Pessoa`,`p`.`ativo_inativo` AS `ativo_inativo_Pessoa` from (`tbl_endereco` `e` left join `tbl_pessoa` `p` on((`e`.`cod_Pessoa` = `p`.`codigo`)));

DROP TABLE IF EXISTS `vw_grupo`;
DROP VIEW IF EXISTS `vw_grupo`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `vw_grupo` AS select `g`.`codigo` AS `codigo`,`g`.`descricao` AS `descricao`,`g`.`material_ou_produto` AS `material_ou_produto`,`g`.`ativo_inativo` AS `ativo_inativo` from `tbl_grupo` `g`;

DROP TABLE IF EXISTS `vw_log`;
DROP VIEW IF EXISTS `vw_log`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `vw_log` AS select `l`.`codigo` AS `codigo`,`l`.`data_registro` AS `data_registro`,`l`.`operacao` AS `operacao`,`l`.`registro` AS `registro`,`l`.`informacoes_registro` AS `informacoes_registro` from `tbl_log` `l`;

DROP TABLE IF EXISTS `vw_login`;
DROP VIEW IF EXISTS `vw_login`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `vw_login` AS select `lg`.`codigo` AS `codigo`,`lg`.`nome_usuario` AS `nome_usuario`,`lg`.`senha` AS `senha`,`lg`.`cod_Usuario` AS `cod_Usuario`,`usu`.`tipo_pessoa` AS `tipo_pessoa`,`usu`.`nome_razao_social` AS `nome_razao_social`,`usu`.`nome_fantasia` AS `nome_fantasia`,`usu`.`cpf_cnpj` AS `cpf_cnpj`,`usu`.`inscricao_estadual` AS `inscricao_estadual`,`usu`.`email` AS `email`,`usu`.`observacao` AS `observacao`,`usu`.`ativo_inativo` AS `ativo_inativo` from (`tbl_login` `lg` left join `tbl_pessoa` `usu` on((`lg`.`cod_Usuario` = `usu`.`codigo`)));

DROP TABLE IF EXISTS `vw_materiais_orcamento`;
DROP VIEW IF EXISTS `vw_materiais_orcamento`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `vw_materiais_orcamento` AS select `mo`.`codigo` AS `codigo`,`mo`.`quantidade_total` AS `quantidade_total`,`mo`.`valor_total` AS `valor_total`,`mo`.`cod_Produtos_Servicos_Orcamento` AS `cod_Produtos_Servicos_Orcamento`,`mo`.`cod_Materiais_Produto_Servico` AS `cod_Materiais_Produto_Servico`,`mo`.`quantidade_Materiais_Produto_Servico` AS `quantidade_Materiais_Produto_Servico`,`mo`.`valor_unitario_Materiais_Produto_Servico` AS `valor_unitario_Materiais_Produto_Servico`,`mo`.`cod_Material` AS `cod_Material`,`mo`.`ultima_atualizacao_Material` AS `ultima_atualizacao_Material`,`mo`.`imagem_Material` AS `imagem_Material`,`mo`.`descricao_Material` AS `descricao_Material`,`mo`.`altura_Material` AS `altura_Material`,`mo`.`largura_Material` AS `largura_Material`,`mo`.`comprimento_Material` AS `comprimento_Material`,`mo`.`valor_unitario_Material` AS `valor_unitario_Material`,`mo`.`ativo_inativo_Material` AS `ativo_inativo_Material`,`mo`.`cod_Unidade` AS `cod_Unidade`,`mo`.`sigla_Unidade` AS `sigla_Unidade`,`mo`.`descricao_Unidade` AS `descricao_Unidade`,`mo`.`ativo_inativo_Unidade` AS `ativo_inativo_Unidade`,`mo`.`cod_Grupo` AS `cod_Grupo`,`mo`.`descricao_Grupo` AS `descricao_Grupo`,`mo`.`ativo_inativo_Grupo` AS `ativo_inativo_Grupo`,`mo`.`cod_Fornecedor` AS `cod_Fornecedor`,`mo`.`nome_razao_social_Fornecedor` AS `nome_razao_social_Fornecedor`,`mo`.`nome_fantasia_Fornecedor` AS `nome_fantasia_Fornecedor`,`mo`.`ativo_inativo_Fornecedor` AS `ativo_inativo_Fornecedor`,`mo`.`cod_Orcamento` AS `cod_Orcamento` from (((((((`tbl_materiais_orcamento` `mo` left join `tbl_produtos_servicos_orcamento` `pso` on((`mo`.`cod_Produtos_Servicos_Orcamento` = `pso`.`codigo`))) left join `tbl_materiais_produto_servico` `mps` on((`mo`.`cod_Materiais_Produto_Servico` = `mps`.`codigo`))) left join `tbl_material` `m` on((`mo`.`cod_Material` = `m`.`codigo`))) left join `tbl_unidade` `u` on((`mo`.`cod_Unidade` = `u`.`codigo`))) left join `tbl_grupo` `g` on((`mo`.`cod_Grupo` = `g`.`codigo`))) left join `tbl_pessoa` `f` on((`mo`.`cod_Fornecedor` = `f`.`codigo`))) left join `tbl_orcamento` `o` on((`mo`.`cod_Orcamento` = `o`.`codigo`)));

DROP TABLE IF EXISTS `vw_materiais_produto_servico`;
DROP VIEW IF EXISTS `vw_materiais_produto_servico`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `vw_materiais_produto_servico` AS select `mps`.`codigo` AS `codigo`,`mps`.`quantidade` AS `quantidade`,`mps`.`valor_total` AS `valor_total`,`mps`.`ativo_inativo` AS `ativo_inativo_Materiais_Produto_Servico`,`mps`.`cod_Material` AS `cod_Material`,`m`.`ultima_atualizacao` AS `ultima_atualizacao_Material`,`m`.`imagem` AS `imagem_Material`,`m`.`descricao` AS `descricao_Material`,`m`.`altura` AS `altura_Material`,`m`.`largura` AS `largura_Material`,`m`.`comprimento` AS `comprimento_Material`,`m`.`valor_unitario` AS `valor_unitario_Material`,`m`.`ativo_inativo` AS `ativo_inativo_Material`,`m`.`cod_Unidade` AS `cod_Unidade_Material`,`um`.`sigla` AS `sigla_Unidade_Material`,`um`.`descricao` AS `descricao_Unidade_Material`,`um`.`ativo_inativo` AS `ativo_inativo_Unidade_Material`,`m`.`cod_Grupo` AS `cod_Grupo_Material`,`gm`.`descricao` AS `descricao_Grupo_Material`,`gm`.`material_ou_produto` AS `material_ou_produto_Grupo_Material`,`gm`.`ativo_inativo` AS `ativo_inativo_Grupo_Material`,`m`.`cod_Fornecedor` AS `cod_Fornecedor_Material`,`fm`.`tipo_pessoa` AS `tipo_pessoa`,`fm`.`nome_razao_social` AS `nome_razao_social`,`fm`.`nome_fantasia` AS `nome_fantasia`,`fm`.`cpf_cnpj` AS `cpf_cnpj`,`fm`.`inscricao_estadual` AS `inscricao_estadual`,`fm`.`email` AS `email`,`fm`.`observacao` AS `observacao`,`fm`.`ativo_inativo` AS `ativo_inativo_Fornecedor_Material`,`mps`.`cod_Produto_Servico` AS `cod_Produto_Servico`,`ps`.`ultima_atualizacao` AS `ultima_atualizacao_Produto_Servico`,`ps`.`imagem` AS `imagem_Produto_Servico`,`ps`.`descricao` AS `descricao_Produto_Servico`,`ps`.`altura` AS `altura_Produto_Servico`,`ps`.`largura` AS `largura_Produto_Servico`,`ps`.`comprimento` AS `comprimento_Produto_Servico`,`ps`.`valor_total_materiais` AS `valor_total_materiais_Produto_Servico`,`ps`.`maoObra` AS `maoObra_Produto_Servico`,`ps`.`valor_maoObra` AS `valor_maoObra_Produto_Servico`,`ps`.`valor_total` AS `valor_total_Produto_Servico`,`ps`.`ativo_inativo` AS `ativo_inativo_Produto_Servico`,`ps`.`cod_Unidade` AS `cod_Unidade_Produto_Servico`,`up`.`sigla` AS `sigla_Unidade_Produto_Servico`,`up`.`descricao` AS `descricao_Unidade_Produto_Servico`,`up`.`ativo_inativo` AS `ativo_inativo_Unidade_Produto_Servico`,`ps`.`cod_Grupo` AS `cod_Grupo_Produto_Servico`,`gp`.`descricao` AS `descricao_Grupo_Produto_Servico`,`gp`.`material_ou_produto` AS `material_ou_produto_Grupo_Produto_Servico`,`gp`.`ativo_inativo` AS `ativo_inativo_Grupo_Produto_Servico` from (((((((`tbl_materiais_produto_servico` `mps` left join `tbl_material` `m` on((`mps`.`cod_Material` = `m`.`codigo`))) left join `tbl_unidade` `um` on((`m`.`cod_Unidade` = `um`.`codigo`))) left join `tbl_grupo` `gm` on((`m`.`cod_Grupo` = `gm`.`codigo`))) left join `tbl_pessoa` `fm` on((`m`.`cod_Fornecedor` = `fm`.`codigo`))) left join `tbl_produto_servico` `ps` on((`mps`.`cod_Produto_Servico` = `ps`.`codigo`))) left join `tbl_unidade` `up` on((`ps`.`cod_Unidade` = `up`.`codigo`))) left join `tbl_grupo` `gp` on((`ps`.`cod_Grupo` = `gp`.`codigo`)));

DROP TABLE IF EXISTS `vw_material`;
DROP VIEW IF EXISTS `vw_material`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `vw_material` AS select `m`.`codigo` AS `codigo`,`m`.`ultima_atualizacao` AS `ultima_atualizacao`,`m`.`imagem` AS `imagem`,`m`.`descricao` AS `descricao_Material`,`m`.`altura` AS `altura`,`m`.`largura` AS `largura`,`m`.`comprimento` AS `comprimento`,`m`.`valor_unitario` AS `valor_unitario`,`m`.`ativo_inativo` AS `ativo_inativo_Material`,`m`.`cod_Unidade` AS `cod_Unidade`,`u`.`sigla` AS `sigla`,`u`.`descricao` AS `descricao_Unidade`,`u`.`ativo_inativo` AS `ativo_inativo_Unidade`,`m`.`cod_Grupo` AS `cod_Grupo`,`g`.`descricao` AS `descricao_Grupo`,`g`.`material_ou_produto` AS `material_ou_produto`,`g`.`ativo_inativo` AS `ativo_inativo_Grupo`,`m`.`cod_Fornecedor` AS `cod_Fornecedor`,`f`.`tipo_pessoa` AS `tipo_pessoa`,`f`.`nome_razao_social` AS `nome_razao_social`,`f`.`nome_fantasia` AS `nome_fantasia`,`f`.`cpf_cnpj` AS `cpf_cnpj`,`f`.`inscricao_estadual` AS `inscricao_estadual`,`f`.`email` AS `email`,`f`.`observacao` AS `observacao`,`f`.`ativo_inativo` AS `ativo_inativo_Pessoa` from (((`tbl_material` `m` left join `tbl_unidade` `u` on((`m`.`cod_Unidade` = `u`.`codigo`))) left join `tbl_grupo` `g` on((`m`.`cod_Grupo` = `g`.`codigo`))) left join `tbl_pessoa` `f` on((`m`.`cod_Fornecedor` = `f`.`codigo`)));

DROP TABLE IF EXISTS `vw_orcamento`;
DROP VIEW IF EXISTS `vw_orcamento`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `vw_orcamento` AS select `o`.`codigo` AS `codigo`,`o`.`ultima_atualizacao` AS `ultima_atualizacao`,`o`.`validade` AS `validade`,`o`.`prazo_entrega` AS `prazo_entrega`,`o`.`observacao` AS `observacao_Orcamento`,`o`.`total_produtos_servicos` AS `total_produtos_servicos`,`o`.`desconto` AS `desconto`,`o`.`total_orcamento` AS `total_orcamento`,`o`.`descricao_pagamento` AS `descricao_pagamento`,`o`.`valor_entrada` AS `valor_entrada`,`o`.`quantidade_parcelas` AS `quantidade_parcelas`,`o`.`valor_parcela` AS `valor_parcela`,`o`.`juros` AS `juros`,`o`.`valor_juros` AS `valor_juros`,`o`.`ativo_inativo` AS `ativo_inativo_Orcamento`,`o`.`cod_Usuario` AS `cod_Usuario`,`usu`.`tipo_pessoa` AS `tipo_pessoa_Usuario`,`usu`.`nome_razao_social` AS `nome_razao_social_Usuario`,`usu`.`nome_fantasia` AS `nome_fantasia_Usuario`,`usu`.`cpf_cnpj` AS `cpf_cnpj_Usuario`,`usu`.`inscricao_estadual` AS `inscricao_estadual_Usuario`,`usu`.`email` AS `email_Usuario`,`usu`.`observacao` AS `observacao_Usuario`,`usu`.`ativo_inativo` AS `ativo_inativo_Usuario`,`o`.`cod_Cliente` AS `cod_Cliente`,`cli`.`tipo_pessoa` AS `tipo_pessoa_Cliente`,`cli`.`nome_razao_social` AS `nome_razao_social_Cliente`,`cli`.`nome_fantasia` AS `nome_fantasia_Cliente`,`cli`.`cpf_cnpj` AS `cpf_cnpj_Cliente`,`cli`.`inscricao_estadual` AS `inscricao_estadual_Cliente`,`cli`.`email` AS `email_Cliente`,`cli`.`observacao` AS `observacao_Cliente`,`cli`.`ativo_inativo` AS `ativo_inativo_Cliente` from ((`tbl_orcamento` `o` left join `tbl_pessoa` `usu` on((`o`.`cod_Usuario` = `usu`.`codigo`))) left join `tbl_pessoa` `cli` on((`o`.`cod_Cliente` = `cli`.`codigo`)));

DROP TABLE IF EXISTS `vw_pessoa`;
DROP VIEW IF EXISTS `vw_pessoa`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `vw_pessoa` AS select `p`.`codigo` AS `codigo`,`p`.`tipo_pessoa` AS `tipo_pessoa`,`p`.`nome_razao_social` AS `nome_razao_social`,`p`.`nome_fantasia` AS `nome_fantasia`,`p`.`cpf_cnpj` AS `cpf_cnpj`,`p`.`inscricao_estadual` AS `inscricao_estadual`,`p`.`email` AS `email`,`p`.`observacao` AS `observacao`,`p`.`ativo_inativo` AS `ativo_inativo` from `tbl_pessoa` `p`;

DROP TABLE IF EXISTS `vw_produto_servico`;
DROP VIEW IF EXISTS `vw_produto_servico`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `vw_produto_servico` AS select `ps`.`codigo` AS `codigo`,`ps`.`ultima_atualizacao` AS `ultima_atualizacao`,`ps`.`imagem` AS `imagem`,`ps`.`descricao` AS `descricao_Produto_Servico`,`ps`.`altura` AS `altura`,`ps`.`largura` AS `largura`,`ps`.`comprimento` AS `comprimento`,`ps`.`valor_total_materiais` AS `valor_total_materiais`,`ps`.`maoObra` AS `maoObra`,`ps`.`valor_maoObra` AS `valor_maoObra`,`ps`.`valor_total` AS `valor_total`,`ps`.`ativo_inativo` AS `ativo_inativo_Produto_Servico`,`ps`.`cod_Unidade` AS `cod_Unidade`,`u`.`sigla` AS `sigla`,`u`.`descricao` AS `descricao_Unidade`,`u`.`ativo_inativo` AS `ativo_inativo_Unidade`,`ps`.`cod_Grupo` AS `cod_Grupo`,`g`.`descricao` AS `descricao_Grupo`,`g`.`material_ou_produto` AS `material_ou_produto`,`g`.`ativo_inativo` AS `ativo_inativo_Grupo` from ((`tbl_produto_servico` `ps` left join `tbl_unidade` `u` on((`ps`.`cod_Unidade` = `u`.`codigo`))) left join `tbl_grupo` `g` on((`ps`.`cod_Grupo` = `g`.`codigo`)));

DROP TABLE IF EXISTS `vw_produtos_servicos_orcamento`;
DROP VIEW IF EXISTS `vw_produtos_servicos_orcamento`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `vw_produtos_servicos_orcamento` AS select `pso`.`codigo` AS `codigo`,`pso`.`quantidade` AS `quantidade`,`pso`.`valor_total` AS `valor_total`,`pso`.`cod_Produto_Servico` AS `cod_Produto_Servico`,`pso`.`ultima_atualizacao_Produto_Servico` AS `ultima_atualizacao_Produto_Servico`,`pso`.`imagem_Produto_Servico` AS `imagem_Produto_Servico`,`pso`.`descricao_Produto_Servico` AS `descricao_Produto_Servico`,`pso`.`altura_Produto_Servico` AS `altura_Produto_Servico`,`pso`.`largura_Produto_Servico` AS `largura_Produto_Servico`,`pso`.`comprimento_Produto_Servico` AS `comprimento_Produto_Servico`,`pso`.`valor_total_materiais_Produto_Servico` AS `valor_total_materiais_Produto_Servico`,`pso`.`maoObra_Produto_Servico` AS `maoObra_Produto_Servico`,`pso`.`valor_maoObra_Produto_Servico` AS `valor_maoObra_Produto_Servico`,`pso`.`valor_unitario_Produto_Servico` AS `valor_unitario_Produto_Servico`,`pso`.`ativo_inativo_Produto_Servico` AS `ativo_inativo_Produto_Servico`,`pso`.`cod_Unidade` AS `cod_Unidade`,`pso`.`sigla_Unidade` AS `sigla_Unidade`,`pso`.`descricao_Unidade` AS `descricao_Unidade`,`pso`.`ativo_inativo_Unidade` AS `ativo_inativo_Unidade`,`pso`.`cod_Grupo` AS `cod_Grupo`,`pso`.`descricao_Grupo` AS `descricao_Grupo`,`g`.`material_ou_produto` AS `material_ou_produto_Grupo`,`pso`.`ativo_inativo_Grupo` AS `ativo_inativo_Grupo`,`pso`.`cod_Orcamento` AS `cod_Orcamento`,`o`.`ultima_atualizacao` AS `ultima_atualizacao`,`o`.`validade` AS `validade`,`o`.`prazo_entrega` AS `prazo_entrega`,`o`.`observacao` AS `observacao`,`o`.`total_produtos_servicos` AS `total_produtos_servicos`,`o`.`desconto` AS `desconto`,`o`.`total_orcamento` AS `total_orcamento`,`o`.`descricao_pagamento` AS `descricao_pagamento`,`o`.`valor_entrada` AS `valor_entrada`,`o`.`quantidade_parcelas` AS `quantidade_parcelas`,`o`.`valor_parcela` AS `valor_parcela`,`o`.`juros` AS `juros`,`o`.`valor_juros` AS `valor_juros`,`o`.`ativo_inativo` AS `ativo_inativo_Orcamento`,`o`.`cod_Usuario` AS `cod_Usuario`,`usu`.`tipo_pessoa` AS `tipo_pessoa_Usuario`,`usu`.`nome_razao_social` AS `nome_razao_social_Usuario`,`usu`.`nome_fantasia` AS `nome_fantasia_Usuario`,`usu`.`cpf_cnpj` AS `cpf_cnpj_Usuario`,`usu`.`inscricao_estadual` AS `inscricao_estadual_Usuario`,`usu`.`email` AS `email_Usuario`,`usu`.`observacao` AS `observacao_Usuario`,`usu`.`ativo_inativo` AS `ativo_inativo_Usuario`,`o`.`cod_Cliente` AS `cod_Cliente`,`cli`.`tipo_pessoa` AS `tipo_pessoa_Cliente`,`cli`.`nome_razao_social` AS `nome_razao_social_Cliente`,`cli`.`nome_fantasia` AS `nome_fantasia_Cliente`,`cli`.`cpf_cnpj` AS `cpf_cnpj_Cliente`,`cli`.`inscricao_estadual` AS `inscricao_estadual_Cliente`,`cli`.`email` AS `email_Cliente`,`cli`.`observacao` AS `observacao_Cliente`,`cli`.`ativo_inativo` AS `ativo_inativo_Cliente` from ((((((`tbl_produtos_servicos_orcamento` `pso` left join `tbl_produto_servico` `ps` on((`pso`.`cod_Produto_Servico` = `ps`.`codigo`))) left join `tbl_unidade` `u` on((`pso`.`cod_Unidade` = `u`.`codigo`))) left join `tbl_grupo` `g` on((`pso`.`cod_Grupo` = `g`.`codigo`))) left join `tbl_orcamento` `o` on((`pso`.`cod_Orcamento` = `o`.`codigo`))) left join `tbl_pessoa` `usu` on((`o`.`cod_Usuario` = `usu`.`codigo`))) left join `tbl_pessoa` `cli` on((`o`.`cod_Cliente` = `cli`.`codigo`)));

DROP TABLE IF EXISTS `vw_telefone`;
DROP VIEW IF EXISTS `vw_telefone`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `vw_telefone` AS select `t`.`codigo` AS `codigo`,`t`.`ddd` AS `ddd`,`t`.`numero` AS `numero`,`t`.`observacao` AS `observacao`,`t`.`ativo_inativo` AS `ativo_inativo_Telefone`,`t`.`cod_Pessoa` AS `cod_Pessoa`,`p`.`tipo_pessoa` AS `tipo_pessoa`,`p`.`nome_razao_social` AS `nome_razao_social`,`p`.`nome_fantasia` AS `nome_fantasia`,`p`.`cpf_cnpj` AS `cpf_cnpj`,`p`.`inscricao_estadual` AS `inscricao_estadual`,`p`.`email` AS `email`,`p`.`observacao` AS `observacao_Pessoa`,`p`.`ativo_inativo` AS `ativo_inativo_Pessoa` from (`tbl_telefone` `t` left join `tbl_pessoa` `p` on((`t`.`cod_Pessoa` = `p`.`codigo`)));

DROP TABLE IF EXISTS `vw_unidade`;
DROP VIEW IF EXISTS `vw_unidade`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `vw_unidade` AS select `u`.`codigo` AS `codigo`,`u`.`sigla` AS `sigla`,`u`.`descricao` AS `descricao`,`u`.`ativo_inativo` AS `ativo_inativo` from `tbl_unidade` `u`;



/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;


-- Dump completed on 2024-02-02 17:39:05
-- Total time: 0:0:0:0:139 (d:h:m:s:ms)
