    using TrabalhoBimestral_Estremote.conexao;
    using TrabalhoBimestral_Estremote.model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using TrabalhoBimestral_Estremote.controler;


    namespace TrabalhoBimestral_Estremote.controller
    {
        internal class C_disciplina : I_metodogenerico
        {
            SqlConnection con;
            SqlCommand cmd;

            string sqlInsere = "insert into disciplina (nomedisciplina, ementa, cargahoraria, bibliografia, fotodisciplina) values (@NomeDisciplina, @Ementa, @CargaHoraria, @Bibliografia, @FotoDisciplina)";
            string sqlEditar = "update disciplina set nomedisciplina = @NomeDisciplina, " +
                        "ementa = @Ementa, cargahoraria = @CargaHoraria, bibliografia = @Bibliografia, fotodisciplina = @FotoDisciplina" +
                        " where coddisciplina = @CodigoDisciplina";
        string sqlEditar2 = "update disciplina set nomedisciplina = @NomeDisciplina, " +
                       "ementa = @Ementa, cargahoraria = @CargaHoraria, bibliografia = @Bibliografia" +
                       " where coddisciplina = @CodigoDisciplina";
        string sqlApagar = "delete from disciplina where coddisciplina = @CodigoDisciplina";

            string sqlBuscarId = "select * from disciplina where coddisciplina = @CodigoDisciplina";

            public void apagarDados(int codigoDisciplina)
            {
                ConectaBanco cb = new ConectaBanco();
                con = cb.conectaSqlServer();
                cmd = new SqlCommand(sqlApagar, con);

                // Passando parâmetros para a sentença SQL
                cmd.Parameters.AddWithValue("@CodigoDisciplina", codigoDisciplina);
                cmd.CommandType = CommandType.Text;
                con.Open();

                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Disciplina deletada com sucesso!!!\n" +
                            "Código: " + codigoDisciplina);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao Apagar!\nErro: " + ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }

            public object buscarId(int codigoDisciplina)
            {
                disciplina disciplina = new disciplina();

                ConectaBanco cb = new ConectaBanco();
                con = cb.conectaSqlServer();

                cmd = new SqlCommand(sqlBuscarId, con);

                // Passando parâmetros para a sentença SQL
                cmd.Parameters.AddWithValue("@CodigoDisciplina", codigoDisciplina);
                cmd.CommandType = CommandType.Text;

                SqlDataReader tabDisciplina;
                con.Open();

                try
                {
                    tabDisciplina = cmd.ExecuteReader();
                    if (tabDisciplina.Read())
                    {
                        disciplina.coddisciplina = Int32.Parse(tabDisciplina["coddisciplina"].ToString());
                        disciplina.nomedisciplina = tabDisciplina["nomedisciplina"].ToString();
                        disciplina.ementa = tabDisciplina["ementa"].ToString();
                        disciplina.cargahoraria = tabDisciplina["cargahoraria"].ToString();
                        disciplina.bibliografia = tabDisciplina["bibliografia"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.ToString());
                }
                finally
                {
                    con.Close();
                }

                return disciplina;
            }

            public void consultarTodos()
            {
                throw new NotImplementedException();
            }


        public void editarDados2(object obj)
        {
            disciplina disciplina = (disciplina)obj;

            ConectaBanco cb = new ConectaBanco();
            con = cb.conectaSqlServer();
            cmd = new SqlCommand(sqlEditar2, con);

            cmd.Parameters.AddWithValue("@CodigoDisciplina", disciplina.coddisciplina);
            cmd.Parameters.AddWithValue("@NomeDisciplina", disciplina.nomedisciplina);
            cmd.Parameters.AddWithValue("@Ementa", disciplina.ementa);
            cmd.Parameters.AddWithValue("@CargaHoraria", disciplina.cargahoraria);
            cmd.Parameters.AddWithValue("@Bibliografia", disciplina.bibliografia);
         

            cmd.CommandType = CommandType.Text;
            con.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Registro atualizado com sucesso");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }
        public void editarDados(object obj)
            {
                disciplina disciplina = (disciplina)obj;

                ConectaBanco cb = new ConectaBanco();
                con = cb.conectaSqlServer();
                cmd = new SqlCommand(sqlEditar, con);

                cmd.Parameters.AddWithValue("@CodigoDisciplina", disciplina.coddisciplina);
                cmd.Parameters.AddWithValue("@NomeDisciplina", disciplina.nomedisciplina);
                cmd.Parameters.AddWithValue("@Ementa", disciplina.ementa);
                cmd.Parameters.AddWithValue("@CargaHoraria", disciplina.cargahoraria);
                cmd.Parameters.AddWithValue("@Bibliografia", disciplina.bibliografia);
                cmd.Parameters.AddWithValue("@FotoDisciplina", disciplina.fotodisciplina);


            cmd.CommandType = CommandType.Text;
                con.Open();

                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Registro atualizado com sucesso");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }

            public void inserirDados(object obj)
            {
                disciplina disciplina = (disciplina)obj;

                ConectaBanco cb = new ConectaBanco();
                con = cb.conectaSqlServer();
                cmd = new SqlCommand(sqlInsere, con);

                cmd.Parameters.AddWithValue("@NomeDisciplina", disciplina.nomedisciplina);
                cmd.Parameters.AddWithValue("@Ementa", disciplina.ementa);
                cmd.Parameters.AddWithValue("@CargaHoraria", disciplina.cargahoraria);
                cmd.Parameters.AddWithValue("@Bibliografia", disciplina.bibliografia);
                cmd.Parameters.AddWithValue("@FotoDisciplina", disciplina.fotodisciplina);
                cmd.CommandType = CommandType.Text;
                con.Open();
                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Registro incluído com sucesso");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
