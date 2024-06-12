CREATE DATABASE IF NOT EXISTS GenOR_BD;

USE GenOR_BD;

-- --------------------------------CRIANDO AS TABELAS BASE----------------------------------
CREATE TABLE tbl_LOG(
	codigo INT AUTO_INCREMENT PRIMARY KEY,
    data_registro DATETIME NOT NULL,
    operacao CHAR(11) NOT NULL,                   -- CADASTRO, ALTERAÇÃO, INATIVAÇÃO, ATUALIZAÇÃO
    registro CHAR(32) NOT NULL,					  -- Pessoa, Endereco, Telefone, Grupo, Unidade, Material, Produto ou Serviço, Materiais do Produto ou Serviço, Orçamento
    informacoes_registro VARCHAR(1000) NOT NULL
);

CREATE TABLE tbl_Pessoa(
	codigo INT AUTO_INCREMENT PRIMARY KEY,
    tipo_pessoa CHAR(10) NOT NULL,                   -- USUARIO | CLIENTE | FORNECEDOR
    nome_razao_social VARCHAR(150) NOT NULL,        -- nome de uma pessoa fisica ou razao social de um pessoa juridica
    nome_fantasia VARCHAR(150) NOT NULL,
    cpf_cnpj CHAR(14) NOT NULL,
    inscricao_estadual CHAR(12) NOT NULL,
    email VARCHAR(256) NOT NULL,
    observacao VARCHAR(150) NOT NULL,
    ativo_inativo BOOLEAN NOT NULL
);

CREATE TABLE tbl_Login(
	codigo INT AUTO_INCREMENT PRIMARY KEY,
	nome_usuario VARCHAR(256) NOT NULL,
    senha VARCHAR(50) NOT NULL,
    
    cod_Usuario INT NOT NULL,
		FOREIGN KEY (cod_Usuario) REFERENCES tbl_Pessoa(codigo)
);

CREATE TABLE tbl_Endereco(
	codigo INT AUTO_INCREMENT PRIMARY KEY,
    endereco VARCHAR(150) NOT NULL,
    complemento VARCHAR(150) NOT NULL,
    numero CHAR (12) NOT NULL,
	bairro VARCHAR(150) NOT NULL,
    cidade VARCHAR(31) NOT NULL,
	estado CHAR(2) NOT NULL,
	cep CHAR(8) NOT NULL,
    observacao VARCHAR(150) NOT NULL,
	ativo_inativo BOOLEAN NOT NULL,
    
    cod_Pessoa INT NOT NULL,
		FOREIGN KEY (cod_Pessoa) REFERENCES tbl_Pessoa(codigo)
);

CREATE TABLE tbl_Telefone(
	codigo INT AUTO_INCREMENT PRIMARY KEY,
    ddd CHAR(2) NOT NULL,                              -- 0800 não tem DDD
    numero CHAR(11) NOT NULL,                          -- tamanho 0800
    observacao VARCHAR(150) NOT NULL,
	ativo_inativo BOOLEAN NOT NULL,
    
    cod_Pessoa INT NOT NULL,
		FOREIGN KEY (cod_Pessoa) REFERENCES tbl_Pessoa(codigo)
);

CREATE TABLE tbl_Grupo(
	codigo INT AUTO_INCREMENT PRIMARY KEY,
    descricao VARCHAR(150) NOT NULL,
    material_ou_produto CHAR(1) NOT NULL, -- M - Material | P - Produto
    ativo_inativo BOOLEAN NOT NULL
);

CREATE TABLE tbl_Unidade(
	codigo INT AUTO_INCREMENT PRIMARY KEY,
    sigla CHAR(8) NOT NULL,
    descricao VARCHAR(150) NOT NULL,
    ativo_inativo BOOLEAN NOT NULL
);

CREATE TABLE tbl_Material(
	codigo INT AUTO_INCREMENT PRIMARY KEY,
    ultima_atualizacao DATETIME NOT NULL,
    imagem VARCHAR(260) NOT NULL,                               -- salvar a imagem em bytes
    descricao VARCHAR(150) NOT NULL,
    altura DECIMAL(13,2) NOT NULL,
    largura DECIMAL(13,2) NOT NULL,
    comprimento DECIMAL(13,2) NOT NULL,
    valor_unitario DECIMAL(13,2) NOT NULL,
    ativo_inativo BOOLEAN NOT NULL,
    
    cod_Unidade INT NOT NULL,
		FOREIGN KEY (cod_Unidade) REFERENCES tbl_Unidade(codigo),
    cod_Grupo INT NOT NULL,
		FOREIGN KEY (cod_Grupo) REFERENCES tbl_Grupo(codigo),
    cod_Fornecedor INT NOT NULL,
		FOREIGN KEY (cod_Fornecedor) REFERENCES tbl_Pessoa(codigo)
);

CREATE TABLE tbl_Produto_Servico(
	codigo INT AUTO_INCREMENT PRIMARY KEY,
    ultima_atualizacao DATETIME NOT NULL,
    imagem VARCHAR(260) NOT NULL,                               -- salvar a imagem em bytes
    descricao VARCHAR(150) NOT NULL,
    altura DECIMAL(13,2) NOT NULL,
    largura DECIMAL(13,2) NOT NULL,
    comprimento DECIMAL(13,2) NOT NULL,
    valor_total_materiais DECIMAL(13,2) NOT NULL,
    maoObra DECIMAL(13,2) NOT NULL,
    valor_maoObra DECIMAL(13,2) NOT NULL,
    valor_total DECIMAL(13,2) NOT NULL,
    ativo_inativo BOOLEAN NOT NULL,
    
    cod_Unidade INT NOT NULL,
		FOREIGN KEY (cod_Unidade) REFERENCES tbl_Unidade(codigo),
    cod_Grupo INT NOT NULL,
		FOREIGN KEY (cod_Grupo) REFERENCES tbl_Grupo(codigo)
);

CREATE TABLE tbl_Materiais_Produto_Servico(
	codigo INT AUTO_INCREMENT PRIMARY KEY,
    quantidade DECIMAL(13,2) NOT NULL,
    valor_total DECIMAL(13,2) NOT NULL,                 -- Valor unitario do material * quantidade de material desse produto ou serviço
    ativo_inativo BOOLEAN NOT NULL,
    
    cod_Material INT NOT NULL,
		FOREIGN KEY (cod_Material) REFERENCES tbl_Material(codigo),
    cod_Produto_Servico INT NOT NULL,
		FOREIGN KEY (cod_Produto_Servico) REFERENCES tbl_Produto_Servico(codigo)
);

CREATE TABLE tbl_Orcamento(
	codigo INT AUTO_INCREMENT PRIMARY KEY,
    ultima_atualizacao DATETIME NOT NULL,
    validade DATETIME NOT NULL,
    prazo_entrega VARCHAR(150) NOT NULL,
    observacao VARCHAR(150) NOT NULL,
    total_produtos_servicos DECIMAL(13,2) NOT NULL,
    desconto DECIMAL(13,2) NOT NULL,                                 -- Desconto em %
    total_orcamento DECIMAL(13,2) NOT NULL,
    descricao_pagamento VARCHAR(150) NOT NULL,
    valor_entrada DECIMAL(13,2) NOT NULL,
    quantidade_parcelas INT NOT NULL,
    valor_parcela DECIMAL(13,2) NOT NULL,
    juros DECIMAL(13, 2) NOT NULL,                                     -- Juros em %
    valor_juros DECIMAL(13,2) NOT NULL,                     -- Valor juros por parcela
    ativo_inativo BOOLEAN NOT NULL,
    
    cod_Usuario INT NOT NULL,
		FOREIGN KEY (cod_Usuario) REFERENCES tbl_Pessoa(codigo),
	cod_Cliente INT NOT NULL,
		FOREIGN KEY (cod_Cliente) REFERENCES tbl_Pessoa(codigo)
);

CREATE TABLE tbl_Produtos_Servicos_Orcamento(
	codigo INT AUTO_INCREMENT PRIMARY KEY,
    quantidade DECIMAL(13,2) NOT NULL,
    valor_total DECIMAL(13,2) NOT NULL,            -- Essa é a quantidade de produtos * valor unitario do Produto ou Serviço
	
    -- Salvando novamente as informações desse produto, para que elas se mantenham nessa tabela sem alterações
    cod_Produto_Servico INT NOT NULL,
		FOREIGN KEY (cod_Produto_Servico) REFERENCES tbl_Produto_Servico(codigo),
	ultima_atualizacao_Produto_Servico DATETIME NOT NULL,
    imagem_Produto_Servico VARCHAR(260) NOT NULL,                               -- salvar a imagem em bytes
    descricao_Produto_Servico VARCHAR(150) NOT NULL,
    altura_Produto_Servico DECIMAL(13,2) NOT NULL,
    largura_Produto_Servico DECIMAL(13,2) NOT NULL,
    comprimento_Produto_Servico DECIMAL(13,2) NOT NULL,
    valor_total_materiais_Produto_Servico DECIMAL(13,2) NOT NULL,
    maoObra_Produto_Servico DECIMAL(13,2) NOT NULL,
    valor_maoObra_Produto_Servico DECIMAL(13,2) NOT NULL,
    valor_unitario_Produto_Servico DECIMAL(13,2) NOT NULL,
    ativo_inativo_Produto_Servico BOOLEAN NOT NULL,
    
    cod_Unidade INT NOT NULL,
		FOREIGN KEY (cod_Unidade) REFERENCES tbl_Unidade(codigo),
	sigla_Unidade CHAR(8) NOT NULL,
    descricao_Unidade VARCHAR(150) NOT NULL,
    ativo_inativo_Unidade BOOLEAN NOT NULL,
    
    cod_Grupo INT NOT NULL,
		FOREIGN KEY (cod_Grupo) REFERENCES tbl_Grupo(codigo),
    descricao_Grupo VARCHAR(150) NOT NULL,
    ativo_inativo_Grupo BOOLEAN NOT NULL,
    
    cod_Orcamento INT NOT NULL,
		FOREIGN KEY (cod_Orcamento) REFERENCES tbl_Orcamento(codigo)
);

CREATE TABLE tbl_Materiais_Orcamento(
	codigo INT AUTO_INCREMENT PRIMARY KEY,
    quantidade_total DECIMAL(13,2) NOT NULL,
    valor_total DECIMAL(13,2) NOT NULL,
    
    cod_Produtos_Servicos_Orcamento INT NOT NULL,
		FOREIGN KEY (cod_Produtos_Servicos_Orcamento) REFERENCES tbl_Produtos_Servicos_Orcamento(codigo),
	
    -- Salvando novamente as informações desse Materiais usados pelos Produtos ou Serviços desse orcamento, para que elas se mantenham nessa tabela sem alterações
    cod_Materiais_Produto_Servico INT NOT NULL,
		FOREIGN KEY (cod_Materiais_Produto_Servico) REFERENCES tbl_Materiais_Produto_Servico(codigo),
	quantidade_Materiais_Produto_Servico DECIMAL(13,2) NOT NULL,                          -- Essa vai ser a quantidade do Materiais_Produto_Servico
    valor_unitario_Materiais_Produto_Servico DECIMAL(13,2) NOT NULL,                      -- Essa vai ser a valor unitario do Materiais_Produto_Servico    
    
    cod_Material INT NOT NULL,
		FOREIGN KEY (cod_Material) REFERENCES tbl_Material(codigo),
    ultima_atualizacao_Material DATETIME NOT NULL,
    imagem_Material VARCHAR(260) NOT NULL,                               -- salvar a imagem em bytes
    descricao_Material VARCHAR(150) NOT NULL,
    altura_Material DECIMAL(13,2) NOT NULL,
    largura_Material DECIMAL(13,2) NOT NULL,
    comprimento_Material DECIMAL(13,2) NOT NULL,
    valor_unitario_Material DECIMAL(13,2) NOT NULL,
    ativo_inativo_Material BOOLEAN NOT NULL,
        
    cod_Unidade INT NOT NULL,
		FOREIGN KEY (cod_Unidade) REFERENCES tbl_Unidade(codigo),
	sigla_Unidade CHAR(8) NOT NULL,
    descricao_Unidade VARCHAR(150) NOT NULL,
    ativo_inativo_Unidade BOOLEAN NOT NULL,
    
    cod_Grupo INT NOT NULL,
		FOREIGN KEY (cod_Grupo) REFERENCES tbl_Grupo(codigo),
    descricao_Grupo VARCHAR(150) NOT NULL,
    ativo_inativo_Grupo BOOLEAN NOT NULL,
    
    cod_Fornecedor INT NOT NULL,
		FOREIGN KEY (cod_Fornecedor) REFERENCES tbl_Pessoa(codigo),
	nome_razao_social_Fornecedor VARCHAR(150) NOT NULL,
	nome_fantasia_Fornecedor VARCHAR(150) NOT NULL,
	ativo_inativo_Fornecedor BOOLEAN NOT NULL,
    
    cod_Orcamento INT NOT NULL,
		FOREIGN KEY (cod_Orcamento) REFERENCES tbl_Orcamento(codigo)
);
