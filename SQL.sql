USE PreRegistro

--TABELA USUARIO
CREATE TABLE Usuario (
    idUsuario INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    nome VARCHAR (90) NOT NULL,
    CPF VARCHAR (11) UNIQUE NOT NULL,
	email VARCHAR(300) NULL,
	senha VARCHAR (15) NULL,
	ativo BIT NULL
);

--TABELA LOGIN
CREATE TABLE Administrador (
	id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    usuario VARCHAR (10) NOT NULL,
	senha VARCHAR (15) NULL,
	ativo BIT NULl
);

/**** Criar ADM ****/
INSERT INTO Administrador Values('admin','adm@2019',1)

/**** PROCEDURE - INSERÇÃO DE USUARIO ****/
CREATE PROCEDURE InserirUsuario (
	@nome VARCHAR (90),
	@CPF VARCHAR (11),
	@email VARCHAR(300),
	@senha VARCHAR (15),
	@ativo BIT
) AS
BEGIN
	INSERT INTO Usuario (nome, CPF, email, senha, ativo)
	VALUES (@nome, @CPF ,@email ,@senha ,@ativo);
END

/**** PROCEDURE - ATUALIZAÇÃO DE USUARIO ****/
CREATE PROCEDURE AtualizarUsuario (
	@idUsuario INT,
	@nome VARCHAR (90),
	@CPF VARCHAR (11),
	@email VARCHAR(300),
	@ativo BIT
) AS
BEGIN
	UPDATE Usuario
	SET nome = @nome, CPF = @CPF , email = @email , ativo = @ativo
	WHERE idUsuario = @idUsuario
END

/**** PROCEDURE - CARREGAR USUARI ****/
CREATE PROCEDURE CarregarUsuario (
	@idUsuario varchar(11)
) AS
BEGIN
	SELECT
		idUsuario = u.idUsuario,
		nome = u.nome,
		CPF = u.CPF,
		email = u.email,
		ativo = u.ativo
	FROM 
		Usuario u
	WHERE 
		idUsuario = @idUsuario
END
GO

/**** PROCEDURE - CARREGAR TODOS OS USUARIOS ****/
CREATE PROCEDURE dbo.CarregarTodosUsuarios
AS
BEGIN
	SELECT
		idUsuario = u.idUsuario,
		nome = u.nome,
		CPF = u.CPF,
		email = u.email,
		ativo = u.ativo
	FROM 
		Usuario u
END
GO

/**** PROCEDURE - DELETAR USUÁRIO ****/
CREATE PROCEDURE DeletarUsuario (
	@idUsuario varchar(11)
) AS
BEGIN
	DELETE FROM 
		Usuario 
	WHERE 
		idUsuario = @idUsuario
END
GO

/**** PROCEDURE - LOGIN ****/
CREATE PROCEDURE Logar (
	@usuario VARCHAR (10),
	@senha VARCHAR (15)
) AS
BEGIN
	SELECT 
		id = u.id,
		usuario = u.usuario,
		ativo = u.ativo
	FROM 
		Administrador u
	WHERE 
		usuario = @usuario AND senha = @senha;
END
GO



--exec InserirUsuario 'Gustavo','11110121111','gustavo@cebraspe.org.br','12345',0
--exec CarregarTodosUsuarios
--EXEC AtualizarUsuario '"Teste"','6666666666', 'teste@teste.com', 0
--exec DeletarUsuario 4
-- exec Logar 'admin','adm@2019'
