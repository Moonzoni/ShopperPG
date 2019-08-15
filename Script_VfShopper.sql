
CREATE TABLE PERFIL
(
    COD_PERFIL INT IDENTITY(1,1),
    NOME VARCHAR(100) NOT NULL UNIQUE,
    PRIMARY KEY(COD_PERFIL)
)

CREATE TABLE CATEGORIA
(
	COD_CATEGORIA INT IDENTITY (1,1),
	NOME VARCHAR(150) NOT NULL UNIQUE,
    PRIMARY KEY(COD_CATEGORIA)
)

CREATE TABLE STATUSS
(	
	COD_STATUS INT IDENTITY (1,1),
	NOME VARCHAR (150) NOT NULL UNIQUE,
	PRIMARY KEY (COD_STATUS)
)


CREATE TABLE USUARIO
(
	COD_USUARIO INT IDENTITY (1,1),
	COD_PERFIL INT NOT NULL,
	NOME VARCHAR (150) NOT NULL,
	EMAIL VARCHAR (300) NOT NULL UNIQUE,
	PRIMARY KEY (COD_USUARIO),
	FOREIGN KEY (COD_PERFIL) REFERENCES PERFIL (COD_PERFIL)
)

CREATE TABLE COMPRAS
(
	COD_COMPRA INT IDENTITY (1,1),
	TITULO VARCHAR (100) NOT NULL,
	DESCRICAO VARCHAR (500) NOT NULL,
	COD_STATUS INT NOT NULL  DEFAULT 1,
	COD_CATEGORIA INT,
	COD_USUARIO INT,
	DATA_ABERTURA DATETIME DEFAULT GETDATE(),
	DATA_FINALIZADA DATETIME DEFAULT ('1900-01-01T00:00:00.000'),
	PRIMARY KEY (COD_COMPRA),
	FOREIGN KEY (COD_STATUS) REFERENCES STATUSS (COD_STATUS),
	FOREIGN KEY (COD_CATEGORIA) REFERENCES CATEGORIA (COD_CATEGORIA),
	FOREIGN KEY (COD_USUARIO) REFERENCES USUARIO (COD_USUARIO)
)

CREATE TABLE ORCAMENTO
(
	COD_ORCAMENTO INT IDENTITY (1,1),
	COD_COMPRA INT,
	NOME VARCHAR (100) NOT NULL,
	LINK VARCHAR (300) NOT NULL,
	OBSERVACAO VARCHAR(350),
	PRIMARY KEY (COD_ORCAMENTO),
	FOREIGN KEY (COD_COMPRA)
		REFERENCES COMPRAS (COD_COMPRA)
)


INSERT INTO STATUSS (NOME)
VALUES
    ('Aguardando aprovação'),
    ('Aprovado'),
    ('Reprovado'),
	('Finalizado')

INSERT INTO CATEGORIA(NOME)
VALUES
	('Periféricos'),
	('Hardware'),
	('Software')


INSERT INTO PERFIL (NOME)
VALUES
	('Admin'),
	('Gerente'),
	('Analista de Compras')


INSERT INTO USUARIO (NOME, EMAIL, COD_PERFIL)
VALUES
	('Pedro', 'ped.321@hotmail.com', 1),
	('Grazielle', 'grazi.lemoss@gmail.com',2),
	('João', 'joao.schmidt@viaflow.com.br', 3)


/*
DROP TABLE ORCAMENTO
DROP TABLE COMPRAS
DROP TABLE USUARIO
DROP TABLE PERFIL
DROP TABLE CATEGORIA
DROP TABLE STATUSS


SELECT * FROM CATEGORIA

SELECT * FROM PERFIL
SELECT * FROM STATUSS
select * from compras
select * from usuario



*/
