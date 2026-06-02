using AppLogin.Models;
using AppLogin.Models.Constant;
using AppLogin.Repository.Contract;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System.Data;
using System.Security.Cryptography;
using X.PagedList;

namespace AppLogin.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        //Propriedade Privada para injetar a coenxão com o banco de dados;
        private readonly string _conexaoMySQL;
        IConfiguration _config;
        //Metodo construtor da classe ClienteRepository
        public ClienteRepository(IConfiguration conf)
        {
            // Injeção de dependencia do banco de dados
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }
        public Cliente Login(string Email, string Senha)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("select * from Cliente where Email = @Email and Senha = @Senha", conexao);
                {
                    cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = Email;
                    cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = Senha;

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    MySqlDataReader dr;

                    Cliente cliente = new Cliente();
                    dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (dr.Read())
                    {
                        cliente.Id = Convert.ToInt32(dr["Id"]);
                        cliente.Nome = Convert.ToString(dr["Nome"]);
                        cliente.Nascimento = Convert.ToDateTime(dr["Nascimento"]);

                        cliente.Sexo = Convert.ToString(dr["Sexo"]);
                        cliente.CPF = Convert.ToString(dr["CPF"]);
                        cliente.Telefone = Convert.ToString(dr["Telefone"]);
                        cliente.Situacao = Convert.ToString(dr["Situacao"]);

                        cliente.Email = Convert.ToString(dr["Email"]);
                        cliente.Senha = Convert.ToString(dr["Senha"]);

                    }
                    return cliente;
                }
            }
         
        }
        public IEnumerable<Cliente> ObterTodosClientes()
        {
            List<Cliente> clilist = new List<Cliente>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("select * from Cliente", conexao);
                
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);

                conexao.Open();

                foreach (DataRow dr in dt.Rows)
                {
                    clilist.Add(
                        new Cliente
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = (string)(dr["Nome"]),
                            Nascimento = Convert.ToDateTime(dr["Nascimento"]),
                            Sexo = Convert.ToString(dr["Sexo"]),
                            CPF = Convert.ToString(dr["CPF"]),
                            Telefone = Convert.ToString(dr["Telefone"]),
                            Situacao = Convert.ToString(dr["Situacao"]),
                            Email = Convert.ToString(dr["Email"]),
                            Senha = Convert.ToString(dr["Senha"])
                        }
                    );
                }
                return clilist;
            } 
        }

        public void Atualizar(Cliente cliente)
        {
            string Situacao = SituacaoConstant.Ativo;

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("Update Cliente set (Nome=@Nome, Nascimento=@Nascimento, Sexo=@Sexo, CPF=@CPF,)" +
                    "Telefone=@Telefone, Email=@Email, Senha=@Senha, Situacao=@Situacao where Id=@Id", conexao);
                {
                    cmd.Parameters.Add("@Id", MySqlDbType.VarChar).Value = cliente.Id;
                    cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = cliente.Nome;
                    cmd.Parameters.Add("@Nascimento", MySqlDbType.VarChar).Value = cliente.Nascimento.ToString("yyyy/MM/dd");
                    cmd.Parameters.Add("@Sexo", MySqlDbType.VarChar).Value = cliente.Sexo;
                    cmd.Parameters.Add("@Telefone", MySqlDbType.VarChar).Value = cliente.Telefone;
                    cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = cliente.Email;
                    cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = cliente.Senha;
                    cmd.Parameters.Add("@Situacao", MySqlDbType.VarChar).Value = Situacao;
                    cmd.ExecuteNonQuery();
                    conexao.Close();
                }
            }
        }

        public void Cadastrar(Cliente cliente)
        {
            string Situacao = SituacaoConstant.Ativo;

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("insert into Cliente (Nome, Nascimento, Sexo, CPF, Telefone, Email, Senha, Situacao)" +
                    " values (@Nome, @Nascimento, @Sexo, @CPF, @Telefone, @Email, @Senha, @Situacao)", conexao);
                {
                    cmd.Parameters.Add("@CPF", MySqlDbType.VarChar).Value = cliente.CPF;
                    cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = cliente.Nome;
                    cmd.Parameters.Add("@Nascimento", MySqlDbType.VarChar).Value = cliente.Nascimento.ToString("yyyy/MM/dd");
                    cmd.Parameters.Add("@Sexo", MySqlDbType.VarChar).Value = cliente.Sexo;
                    cmd.Parameters.Add("@Telefone", MySqlDbType.VarChar).Value = cliente.Telefone;
                    cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = cliente.Email;
                    cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = cliente.Senha;
                    cmd.Parameters.Add("@Situacao", MySqlDbType.VarChar).Value = Situacao;
                    cmd.ExecuteNonQuery();
                    conexao.Close();
                }
            }
        }
        public void Excluir(int Id)
        {
            
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("delete from Cliente where Id=@Id", conexao);
                cmd.Parameters.AddWithValue("@Id", Id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

       

        public Cliente ObterCliente(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Cliente where Id=@Id", conexao);
                cmd.Parameters.AddWithValue("@Id", Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Cliente cliente = new Cliente();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    cliente.Id = (Int32)(dr["Id"]);
                    cliente.Nome = (string)(dr["Nome"]);
                    cliente.Nascimento = (DateTime)(dr["Nascimento"]);
                    cliente.Sexo = (string)(dr["Sexo"]);
                    cliente.CPF = (string)(dr["CPF"]);
                    cliente.Telefone = (string)(dr["Telefone"]);
                    cliente.Email = (string)(dr["Email"]);
                    cliente.Senha = (string)(dr["Senha"]);
                    cliente.Situacao = (string)(dr["Situacao"]);
                }
                return cliente;
            }
        }

     

        public IPagedList<Cliente> ObterTodosClientes(int? pagina, string pesquisa)
        {
            throw new NotImplementedException();
        }
    }
}
