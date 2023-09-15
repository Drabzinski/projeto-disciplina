using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TrabalhoBimestral_Estremote.conexao
{
    internal class ConectaBanco
    {
        SqlConnection con;

        string connectionString = @"Server=.;Database=BD_DISCIPLINA;
                                    Trusted_Connection=True;";

        public SqlConnection conectaSqlServer()
        {
            //cria a conexão com o banco de dados
            con = new SqlConnection(connectionString);

            return con;
        }
    }
}

