using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace ConsoleApp5
{
    internal class Program
    {
        static bool ValidarNomeCompleto(string nome)
        {
            string pattern = @"^[A-Za-zÀ-ú]+(?:\s[A-Za-zÀ-ú]+)+$";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(nome);
        }

        static bool ValidarCPF(String documento)
        {
            string pattern = @"^\d{3}\.\d{3}\.\d{3}-\d{2}$";
            Regex regex = new Regex(@pattern);

            return regex.IsMatch(documento);
        }

        static bool ValidarCNPJ(String documento)
        {
            string pattern = @"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$";
            Regex regex = new Regex(@pattern);

            return regex.IsMatch(documento);
        }
        static bool ValidarTelefone(String telefone)
        {
            string pattern = @"^\(?\d{2}\)?[\s-]?\d{4,5}-?\d{4}$";
            Regex regex = new Regex(@pattern);

            return regex.IsMatch(telefone);
        }

        static bool ValidarEmail(String email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(@pattern);

            return regex.IsMatch(email);
        }

        static bool ValidarSenha(String senha)
        {
            string pattern = @"^(?=.*[!@#$%^&*()-+=])(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,}$";
            Regex regex = new Regex(@pattern);

            return regex.IsMatch(senha);
        }

        static void Main(string[] args)
        {

            Console.WriteLine(@"
███╗░░░███╗██╗░░░██╗███╗░░██╗██████╗░░█████╗░  ██╗░░░██╗███████╗██████╗░██████╗░███████╗
████╗░████║██║░░░██║████╗░██║██╔══██╗██╔══██╗  ██║░░░██║██╔════╝██╔══██╗██╔══██╗██╔════╝
██╔████╔██║██║░░░██║██╔██╗██║██║░░██║██║░░██║  ╚██╗░██╔╝█████╗░░██████╔╝██║░░██║█████╗░░
██║╚██╔╝██║██║░░░██║██║╚████║██║░░██║██║░░██║  ░╚████╔╝░██╔══╝░░██╔══██╗██║░░██║██╔══╝░░
██║░╚═╝░██║╚██████╔╝██║░╚███║██████╔╝╚█████╔╝  ░░╚██╔╝░░███████╗██║░░██║██████╔╝███████╗
╚═╝░░░░░╚═╝░╚═════╝░╚═╝░░╚══╝╚═════╝░░╚════╝░  ░░░╚═╝░░░╚══════╝╚═╝░░╚═╝╚═════╝░╚══════╝");
            string nome;
            string tipoDocumento;
            string documento;
            string telefone;
            string email;
            string senha;
            while (true)
            {
                while(true)
                {
                    Console.WriteLine("Digite seu nome completo");
                    nome = Convert.ToString(Console.ReadLine());


                    if (!ValidarNomeCompleto(nome.Trim()))
                    {
                        Console.WriteLine("ERRO: Nome inválido!");
                        continue;
                    }
                    break;
                }

                while(true)
                {
                    Console.WriteLine("Digite o tipo de documento que deseja cadastrar CPF ou CNPJ com pontuação");
                    tipoDocumento = Convert.ToString(Console.ReadLine());

                    if (!tipoDocumento.Trim().Equals("CPF", StringComparison.InvariantCultureIgnoreCase) &&
                        !tipoDocumento.Trim().Equals("CNPJ", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Console.WriteLine("ERRO: Tipo de documento inválido!");
                        continue;
                    }
                    break;
                }

                while(true)
                {
                    Console.WriteLine("Digite o número do documento escolhido");
                    documento = Convert.ToString(Console.ReadLine());

                    if (!ValidarCPF(documento.Trim()) && !ValidarCNPJ(documento.Trim()))
                    {
                        Console.WriteLine("ERRO: Documento informado inválido!");
                        continue;
                    }
                    break;
                }

                while(true)
                {
                    Console.WriteLine("Digite seu telefone utilizando a formatação (xx)xxxxx-xxxx");
                    telefone = Convert.ToString(Console.ReadLine());

                    if (!ValidarTelefone(telefone.Trim()))
                    {
                        Console.WriteLine("ERRO: Telefone inválido!");
                        continue;
                    }
                    break;
                }

                while(true)
                {
                    Console.WriteLine("Digite seu e-mail");
                    email = Convert.ToString(Console.ReadLine());

                    if (!ValidarEmail(email.Trim()))
                    {
                        Console.WriteLine("ERRO: E-mail inválido!");
                        continue;
                    }
                    break;
                }

                while (true)
                {
                    Console.WriteLine("Digite uma senha com 8 caracteres utilizando um caracter especial e uma letra maiúscula");
                    senha = Convert.ToString(Console.ReadLine());

                    if (!ValidarSenha(senha.Trim()))
                    {
                        Console.WriteLine("ERRO: Senha inválido!");
                        continue;
                    }
                    break;
                }
                string stringConexao = @"Data Source=localhost;Initial Catalog=Mundo_verde;Integrated Security=True";
                string consulta = "INSERT INTO [dbo].[usuario] (nome, tipo_documento, documento, telefone, email, senha)" +
                    " VALUES (@nome, @tipoDocumento, @documento,@telefone, @email, @senha)";

                using (SqlConnection conexao = new SqlConnection(stringConexao))
                {
                    SqlCommand comando = new SqlCommand(consulta, conexao);
                    comando.Parameters.AddWithValue("@nome", nome);
                    comando.Parameters.AddWithValue("@tipoDocumento", tipoDocumento);
                    comando.Parameters.AddWithValue("@documento", documento);
                    comando.Parameters.AddWithValue("@telefone", telefone);
                    comando.Parameters.AddWithValue("@email", email);
                    comando.Parameters.AddWithValue("@senha", senha);
                    conexao.Open();
                    int resultado = comando.ExecuteNonQuery();
                    if (resultado > 0)
                    {
                        Console.WriteLine("\nCADASTRO REALIZADO COM SUCESSO!");
                        Console.ReadLine();
                    }

                    Console.WriteLine(resultado);
                    Console.ReadLine();
                    break;
                }
            }
        }
    }
}