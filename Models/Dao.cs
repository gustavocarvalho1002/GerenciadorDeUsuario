using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace PreRegistro_CadastroUsuario.Models
{
    public class Dao
    {
        public SqlConnection oCon = new SqlConnection();


        /******** CONEXÃO COM O BANCO********/
        public void Conexao()
        {
            try
            {
                oCon.ConnectionString = "Data Source=sql5;Initial Catalog=SiPreRegistro;Trusted_Connection=true;";
                oCon.Open();
            }
            catch (Exception ex)
            {
                //Code
            }


        }

        /******** FECHAR CONEXÃO COM O BANCO********/
        public void FecharConexao()
        {
            oCon.Close();
        }

        /********  CADASTRAR USUÁRIO ********/
        public bool CadastrarUsuario(Usuario usuario,String senha)
        {
            int i = 0;

            try
            {
                Conexao();

                //Instanciando comando para procedure
                SqlCommand oCmd = new SqlCommand();
                oCmd.Connection = oCon;
                oCmd.CommandText = "Inscricao.InserirUsuario";
                oCmd.CommandType = CommandType.StoredProcedure;

                //Parametros da procedure
                oCmd.Parameters.AddWithValue("@idPerfil", SqlDbType.Int).Value = 2;
                oCmd.Parameters.AddWithValue("@nome", SqlDbType.VarChar).Value = usuario.nome;
                oCmd.Parameters.AddWithValue("@CPF", SqlDbType.VarChar).Value = usuario.CPF;
                oCmd.Parameters.AddWithValue("@email", SqlDbType.VarChar).Value = usuario.email;
                oCmd.Parameters.AddWithValue("@senha", SqlDbType.VarChar).Value = senha;
                oCmd.Parameters.AddWithValue("@ativo", SqlDbType.Bit).Value = usuario.ativo;

                i = oCmd.ExecuteNonQuery();

                FecharConexao();

            }
            catch (Exception ex)
            {
                FecharConexao();

            }

            finally
            {
                if (oCon.State == ConnectionState.Open)
                {
                    oCon.Close();
                    oCon.Dispose();
                }


            }

            if (i >= 1)
                return true;
            else
                return false;
        }

        /******** ATUALIZAR USUARIO********/
        public bool AtualizarUsuario(Usuario usuario)
        {
            int i = 0;

            try
            {
                Conexao();

                //Instanciando comando para procedure
                SqlCommand oCmd = new SqlCommand();
                oCmd.Connection = oCon;
                oCmd.CommandText = "Inscricao.AtualizarUsuario";
                oCmd.CommandType = CommandType.StoredProcedure;

                //Parametros da procedure
                oCmd.Parameters.AddWithValue("@id", SqlDbType.VarChar).Value = usuario.idUsuario;
                oCmd.Parameters.AddWithValue("@nome", SqlDbType.VarChar).Value = usuario.nome;
                oCmd.Parameters.AddWithValue("@CPF", SqlDbType.VarChar).Value = usuario.CPF;
                oCmd.Parameters.AddWithValue("@email", SqlDbType.VarChar).Value = usuario.email;
                oCmd.Parameters.AddWithValue("@ativo", SqlDbType.Bit).Value = usuario.ativo;

                i = oCmd.ExecuteNonQuery();

                FecharConexao();

            }
            catch (Exception ex)
            {
                FecharConexao();

            }

            finally
            {
                if (oCon.State == ConnectionState.Open)
                {
                    oCon.Close();
                    oCon.Dispose();
                }


            }

            if (i >= 1)
                return true;
            else
                return false;
        }

        /******** ATUALIZAR SENHA DO USUÁRIO********/
        public bool AtualizarSenha(int id, string novaSenha)
        {
            int i = 0;

            try
            {
                Conexao();

                //Instanciando comando para procedure
                SqlCommand oCmd = new SqlCommand();
                oCmd.Connection = oCon;
                oCmd.CommandText = "Inscricao.AtualizarSenha";
                oCmd.CommandType = CommandType.StoredProcedure;

                //Parametros da procedure
                oCmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                oCmd.Parameters.AddWithValue("@senha", SqlDbType.VarChar).Value = novaSenha;

                i = oCmd.ExecuteNonQuery();

                FecharConexao();

            }
            catch (Exception ex)
            {
                FecharConexao();

            }

            finally
            {
                if (oCon.State == ConnectionState.Open)
                {
                    oCon.Close();
                    oCon.Dispose();
                }


            }

            if (i >= 1)
                return true;
            else
                return false;
        }

        /******** CARREGAR TODOS USUARIOS********/
        public List<Usuario> CarregarTodosUsuarios()
        {
            //Lista de usuario
            List<Usuario> listaUsuarios = new List<Usuario>();

            try
            {
                Conexao();

                //Instanciando comando para procedure
                SqlCommand oCmd = new SqlCommand();
                oCmd.Connection = oCon;
                oCmd.CommandText = "Inscricao.CarregarTodosUsuarios";
                oCmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader odr = oCmd.ExecuteReader();

                while (odr.Read())
                {
                    listaUsuarios.Add(
                        new Usuario
                        {
                            idUsuario = Convert.ToInt32(odr["id"]),
                            nome = odr["nome"].ToString(),
                            CPF = odr["CPF"].ToString(),
                            email = odr["email"].ToString(),
                            ativo = Convert.ToBoolean(odr["ativo"])
                        }

                    );
                }

                FecharConexao();

            }
            catch (Exception ex)
            {
                FecharConexao();

            }

            finally
            {
                if (oCon.State == ConnectionState.Open)
                {
                    oCon.Close();
                    oCon.Dispose();
                }


            }

            return listaUsuarios;
        }

        /******** CARREGAR USUARIO POR ID********/
        public Usuario CarregarUsuarioPorId(int id)
        {
            //Objeto de usuário de usuario
            Usuario usuario = new Usuario();

            try
            {
                Conexao();

                //Instanciando comando para procedure
                SqlCommand oCmd = new SqlCommand();
                oCmd.Connection = oCon;
                oCmd.CommandText = "Inscricao.CarregarUsuario";
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.AddWithValue("@id", SqlDbType.VarChar).Value = id;

                SqlDataReader odr = oCmd.ExecuteReader();

                if (odr.Read())
                {
                    usuario.idUsuario = Convert.ToInt32(odr["id"]);
                    usuario.nome = odr["nome"].ToString();
                    usuario.CPF = odr["CPF"].ToString();
                    usuario.email = odr["email"].ToString();
                    usuario.ativo = Convert.ToBoolean(odr["ativo"]);
                }

                FecharConexao();

            }
            catch (Exception ex)
            {
                FecharConexao();

            }

            finally
            {
                if (oCon.State == ConnectionState.Open)
                {
                    oCon.Close();
                    oCon.Dispose();
                }


            }

            return usuario;
        }

        /******** DELETAR USUARIO POR ID********/
        public bool DeletarUsuario(int id)
        {
            int i = 0;
            try
            {
                Conexao();

                SqlCommand oCmd = new SqlCommand();
                oCmd.Connection = oCon;
                oCmd.CommandText = "Inscricao.DeletarUsuario";
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.AddWithValue("@id", SqlDbType.VarChar).Value = id;

                 i = oCmd.ExecuteNonQuery();
                FecharConexao();
            }
            catch (Exception ex)
            {
                FecharConexao();
            }

            if (i >= 1)
                return true;
            else
                return false;
        }

        /******** DESATIVAR TODOS USUARIOS ********/
        public bool DesativarTodosUsuarios()
        {
            int i = 0;
            try
            {
                Conexao();

                SqlCommand oCmd = new SqlCommand();
                oCmd.Connection = oCon;
                oCmd.CommandText = "Inscricao.MudarStatusDeTodosUsuarios";
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.AddWithValue("@ativo", SqlDbType.Bit).Value = 0;

                i = oCmd.ExecuteNonQuery();
                FecharConexao();
            }
            catch (Exception ex)
            {
                FecharConexao();
            }

            if (i >= 1)
                return true;
            else
                return false;
        }

        /******** ATIVAR TODOS USUARIOS ********/
        public bool AtivarTodosUsuarios()
        {
            int i = 0;
            try
            {
                Conexao();

                SqlCommand oCmd = new SqlCommand();
                oCmd.Connection = oCon;
                oCmd.CommandText = "Inscricao.MudarStatusDeTodosUsuarios";
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.AddWithValue("@ativo", SqlDbType.Bit).Value = 1;

                i = oCmd.ExecuteNonQuery();
                FecharConexao();
            }
            catch (Exception ex)
            {
                FecharConexao();
            }

            if (i >= 1)
                return true;
            else
                return false;
        }

        public List<Usuario> BuscarUsuarios(string paramatro)
        {
            //Lista de usuario
            List<Usuario> listaUsuarios = new List<Usuario>();

            try
            {
                Conexao();

                //Instanciando comando para procedure
                SqlCommand oCmd = new SqlCommand();
                oCmd.Connection = oCon;
                oCmd.CommandText = "Inscricao.BuscarUsuarios";
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.Parameters.AddWithValue("@parametro", SqlDbType.VarChar).Value = paramatro;

                SqlDataReader odr = oCmd.ExecuteReader();

                while (odr.Read())
                {
                    listaUsuarios.Add(
                        new Usuario
                        {
                            idUsuario = Convert.ToInt32(odr["id"]),
                            nome = odr["nome"].ToString(),
                            CPF = odr["CPF"].ToString(),
                            email = odr["email"].ToString(),
                            ativo = Convert.ToBoolean(odr["ativo"])
                        }

                    );
                }

                FecharConexao();

            }
            catch (Exception ex)
            {
                FecharConexao();

            }

            finally
            {
                if (oCon.State == ConnectionState.Open)
                {
                    oCon.Close();
                    oCon.Dispose();
                }


            }

            return listaUsuarios;
        }

        /******** LOGAR USUARIO POR ID********/
        public Administrador Logar(Administrador login)
        {

            try
            {
                Conexao();

                //Instanciando comando para procedure
                SqlCommand oCmd = new SqlCommand();
                oCmd.Connection = oCon;
                oCmd.CommandText = "Inscricao.Logar";
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.AddWithValue("@usuario", SqlDbType.VarChar).Value = login.usuario;
                oCmd.Parameters.AddWithValue("@senha", SqlDbType.VarChar).Value = login.senha ;

                SqlDataReader odr = oCmd.ExecuteReader();

                if (odr.Read())
                {
                    login.id = Convert.ToInt32(odr["id"]);
                    login.usuario = odr["usuario"].ToString();
                    login.ativo = Convert.ToBoolean(odr["ativo"]);

                } else
                {
                    login.id = 0;
                    login.usuario = "";
                }

                FecharConexao();

            }
            catch (Exception ex)
            {
                FecharConexao();

            }

            finally
            {
                if (oCon.State == ConnectionState.Open)
                {
                    oCon.Close();
                    oCon.Dispose();
                }
            }

            return login;
        }

        public void SalvarCredenciais(String usuario, int id)
        {
            Configuration webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
            webConfigApp.AppSettings.Settings["USUARIO"].Value = usuario;
            webConfigApp.AppSettings.Settings["ID_USUARIO"].Value = id.ToString();
            webConfigApp.Save();
        }

        public void LimparCredenciais()
        {
            Configuration webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
            webConfigApp.AppSettings.Settings["USUARIO"].Value = "null";
            webConfigApp.AppSettings.Settings["ID_USUARIO"].Value = "null";
            webConfigApp.Save();
        }

        public List<Usuario> CarregarTodosUsuariosAtivos()
        {
            //Lista de usuario
            List<Usuario> listaUsuarios = new List<Usuario>();

            try
            {
                Conexao();

                //Instanciando comando para procedure
                SqlCommand oCmd = new SqlCommand();
                oCmd.Connection = oCon;
                oCmd.CommandText = "Inscricao.CarregarTodosUsuariosAtivos";
                oCmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader odr = oCmd.ExecuteReader();

                while (odr.Read())
                {
                    listaUsuarios.Add(
                        new Usuario
                        {
                            idUsuario = Convert.ToInt32(odr["id"]),
                            nome = odr["nome"].ToString(),
                            email = odr["email"].ToString()
                        }

                    );
                }

                FecharConexao();

            }
            catch (Exception ex)
            {
                FecharConexao();

            }

            finally
            {
                if (oCon.State == ConnectionState.Open)
                {
                    oCon.Close();
                    oCon.Dispose();
                }


            }

            return listaUsuarios;
        }
    }


}
