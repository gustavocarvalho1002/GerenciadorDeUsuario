using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace PreRegistro_CadastroUsuario.Models
{
    public class SendMail
    {
        public void EnviarEmailCadastro(Usuario usuario, String senha)
        {
            string mensagem = "";

            mensagem =
             @"<html>
                      <meta http - equiv = 'Content-Type' content = 'text/html; charset=iso-8859-1'>
                      <body bgcolor='#FFFFFF' text='#000033'>
                        <font size = '2' face = 'Arial'>
                                Olá, " + usuario.nome.ToUpper() + @", <br /><br />
                                Segue abaixo suas credenciais para acesso no sistema do registro acadêmico online:
                            <p>
                                    
                                        Senha: <strong> " + senha + @"
                                    </strong><br />
                            </p>
                            <br>
                            <br>
                            <h2><strong>17/02 - UNB_20_2_ACESSOENEM</strong></h2>
                            <ul>                  
                                <li><p>Link: <a href='https://security.cebraspe.org.br/Preregistro/App/UNB_20_ACESSOENEM'> Avaliação do RECURSO da 2ª chamada - <strong>UNB_20_2_ACESSOENEM</strong> </a>  </p></li>

                            </ul>
                            <br>
                            <br>
                            <h2><strong>17/02 - VESTUNB_20_1_REMANESCENTES</strong></h2>
                            <ul>                  
                                <li><p>Link: <a href='https://security.cebraspe.org.br/Preregistro/App/UAB_20_LICENCIATURA'> Avaliação do RECURSO da 2ª chamada - <strong>VESTUNB_20_1_REMANESCENTES</strong> </a>  </p></li>

                            </ul>
                            <br>
                            <br>
                            <h2><strong>17/02 - PAS_19</strong></h2>
                            <ul>                  
                                <li><p>Link: <a href='https://security.cebraspe.org.br/Preregistro/App/PAS_19'> Avaliação do RECURSO da 4ª chamada - <strong>PAS_19</strong> </a>  </p></li>

                            </ul>

                        </font>
                    </body>
              </html> ";




            try
            {
                MailMessage message = new MailMessage();
                message.To.Add(usuario.email);
                message.Subject = "Crendenciais de acesso - Registro acadêmico online";
                message.From = new MailAddress("sac@cebraspe.org.br");
                message.IsBodyHtml = true;
                message.Body = mensagem;
                SmtpClient smtp = new SmtpClient();
                smtp.Timeout = 5000;
                smtp.Host = "smtp.cebraspe.org.br";
                smtp.Port = 25;
                smtp.Send(message);
                //int obs = 1;
                //return obs;
            }
            catch (Exception ex)
            {
                //obs = 0;
                //cont++;
                //if (cont < 6)
                //{
                //    Sendmail(emailRemetente, nomeRemetente, token);
                //    return obs;
                //}
                //else
                //{
                //    return obs;
                //}
            }


        }

        public void EnviarEmailNovaSenha(String nomeUsuario, String email, String senha)
        {
            string mensagem = "";

            mensagem =
             @"<html>
                      <meta http - equiv = 'Content-Type' content = 'text/html; charset=iso-8859-1'>
                      <body bgcolor='#FFFFFF' text='#000033'>
                        <font size = '2' face = 'Arial'>
                                Olá, " + nomeUsuario + @", <br /><br />
                                Segue abaixo suas credenciais para acesso no sistema do registro acadêmico online:
                            <p>
                                    
                                        Senha: <strong> " + senha + @"
                                    </strong><br />
                            </p>
                            <br>
                            <br>
                            <h2><strong>17/02 - UNB_20_2_ACESSOENEM</strong></h2>
                            <ul>                  
                                <li><p>Link: <a href='https://security.cebraspe.org.br/Preregistro/App/UNB_20_ACESSOENEM'> Avaliação do RECURSO da 2ª chamada - <strong>UNB_20_2_ACESSOENEM</strong> </a>  </p></li>

                            </ul>
                            <br>
                            <br>
                            <h2><strong>17/02 - VESTUNB_20_1_REMANESCENTES</strong></h2>
                            <ul>                  
                                <li><p>Link: <a href='https://security.cebraspe.org.br/Preregistro/App/UAB_20_LICENCIATURA'> Avaliação do RECURSO da 2ª chamada - <strong>VESTUNB_20_1_REMANESCENTES</strong> </a>  </p></li>

                            </ul>
                            <br>
                            <br>
                            <h2><strong>17/02 - PAS_19</strong></h2>
                            <ul>                  
                                <li><p>Link: <a href='https://security.cebraspe.org.br/Preregistro/App/PAS_19'> Avaliação do RECURSO da 4ª chamada - <strong>PAS_19</strong> </a>  </p></li>

                            </ul>
                        </font>
                    </body>
              </html> ";





            try
            {
                MailMessage message = new MailMessage();
                message.To.Add(email);
                message.Subject = "Redefinição de senha - Registro acadêmico online";
                message.From = new MailAddress("sac@cebraspe.org.br");
                message.IsBodyHtml = true;
                message.Body = mensagem;
                SmtpClient smtp = new SmtpClient();
                smtp.Timeout = 5000;
                smtp.Host = "smtp.cebraspe.org.br";
                smtp.Port = 25;
                smtp.Send(message);
                //int obs = 1;
                //return obs;
            }
            catch (Exception ex)
            {
                //obs = 0;
                //cont++;
                //if (cont < 6)
                //{
                //    Sendmail(emailRemetente, nomeRemetente, token);
                //    return obs;
                //}
                //else
                //{
                //    return obs;
                //}
            }


        }
    }
}