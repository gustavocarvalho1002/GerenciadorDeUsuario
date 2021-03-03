ALTER PROCEDURE inscricao.Logar (
	@usuario VARCHAR (10),
	@senha VARCHAR (15)
) AS
BEGIN

	SELECT 
		id,
		Login as  usuario,
		ativo
	FROM 
		sql1.sipreregistro.dbo.UsuarioColaboradorPreRegistro 
	WHERE 
		Login = @usuario AND senha = @senha;
END

ALTER PROCEDURE inscricao.CarregarUsuario (
	@id varchar(11)
) AS
BEGIN
	SELECT
		id,
		nome,
		[login] as CPF,
		email,
		ativo
	FROM 
		UsuarioColaboradorPreRegistro u
	WHERE 
		id = @id
END
GO

ALTER PROCEDURE inscricao.CarregarTodosUsuarios
AS
BEGIN
	SELECT
		id,
		nome,
		login as CPF,
		email,
		ativo
	FROM 
		UsuarioColaboradorPreRegistro
	WHERE
		nome != '' AND Login NOT LIKE '%[^0-9]%'
	ORDER BY nome

END
GO

ALTER PROCEDURE inscricao.DeletarUsuario (
	@id varchar(11)
) AS
BEGIN
	DELETE FROM 
		UsuarioColaboradorPreRegistro 
	WHERE 
		id = @id
END
GO

ALTER PROCEDURE Inscricao.InserirUsuario (
	@idPerfil INT,
	@nome VARCHAR (90),
	@CPF VARCHAR (11),
	@email VARCHAR(300),
	@senha VARCHAR (15),
	@ativo BIT
) AS
BEGIN
	INSERT INTO UsuarioColaboradorPreRegistro (idPerfil,nome, Login, email, senha, ativo)
	VALUES (@idPerfil,@nome, @CPF ,@email ,@senha ,@ativo);
END

ALTER PROCEDURE Inscricao.AtualizarUsuario (
	@id INT,
	@nome VARCHAR (90),
	@CPF VARCHAR (11),
	@email VARCHAR(300),
	@ativo BIT
) AS
BEGIN
	UPDATE UsuarioColaboradorPreRegistro
	SET nome = @nome, Login = @CPF , email = @email , ativo = @ativo
	WHERE id = @id
END

ALTER PROCEDURE Inscricao.AtualizarSenha (
	@id INT,
	@senha VARCHAR(36)	
) AS
BEGIN
	UPDATE UsuarioColaboradorPreRegistro
	SET senha = @senha
	WHERE id = @id 
END

ALTER PROCEDURE Inscricao.MudarStatusDeTodosUsuarios (
	@ativo bit
) AS
BEGIN
	UPDATE UsuarioColaboradorPreRegistro
	SET ativo = @ativo
	WHERE nome != '' AND Login NOT LIKE '%[^0-9]%'
END

ALTER PROCEDURE inscricao.BuscarUsuarios(
@parametro varchar(80)
)AS
BEGIN
	SELECT
		id,
		nome,
		login as CPF,
		email,
		ativo
	FROM 
		UsuarioColaboradorPreRegistro
	WHERE
		(nome != '' AND Login NOT LIKE '%[^0-9]%') AND nome LIKE '%'+@parametro+'%' OR login = @parametro
	ORDER BY nome
END
GO

ALTER PROCEDURE inscricao.CarregarTodosUsuariosAtivos
AS
BEGIN
	SELECT
		id,
		nome,
		email
	FROM 
		UsuarioColaboradorPreRegistro
	WHERE
		(nome != '' AND Login NOT LIKE '%[^0-9]%') and ativo = 1;

END
GO

select * from dbo.UsuarioColaboradorPreRegistro where nome like '%Roberto%'
exec inscricao.BuscarUsuarios @parametro = 'Gu'