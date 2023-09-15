using TrabalhoBimestral_Estremote.conexao;
using TrabalhoBimestral_Estremote.controler;
using TrabalhoBimestral_Estremote.model;

using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using TrabalhoBimestral_Estremote.controller;
using TrabalhoBimestral_Estremote.view;

namespace TrabalhoBimestral_Estremote
{
    public partial class frmCadastroDisciplina : Form
    {
        string connectionString = @"Server=.;Database=BD_DISCIPLINA; Trusted_Connection=True;";
        bool novo;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable disciplinas;
        SqlDataReader tabDisciplina;
        DataRow[] linhaAtual;
        int posicao = 0;

        public frmCadastroDisciplina()
        {
            InitializeComponent();
            carregarTabela();
        }

        public void carregarTabela()
        {
            string strSql = "SELECT * FROM disciplina order by nomedisciplina";

            ConectaBanco conectaBanco = new ConectaBanco();
            con = conectaBanco.conectaSqlServer();

            cmd = new SqlCommand(strSql, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            da = new SqlDataAdapter(cmd);
            disciplinas = new DataTable();
            da.Fill(disciplinas);
            dataGridView1.DataSource = disciplinas;

            linhaAtual = disciplinas.Select();

            posicao = Int32.Parse(TotalRegistros()) - 1;
            if (posicao == -1)
            {
                MessageBox.Show("Não Existem Registros!");
            }
            else
            {
                txtId.Text = linhaAtual[0]["coddisciplina"].ToString();
                txtNomeDisc.Text = linhaAtual[0]["nomedisciplina"].ToString();
                txtEmenta.Text = linhaAtual[0]["ementa"].ToString();
                txtCargaHoraria.Text = linhaAtual[0]["cargahoraria"].ToString();
                txtBibliografia.Text = linhaAtual[0]["bibliografia"].ToString();
                byte[] imagemBytes = linhaAtual[0]["fotodisciplina"] as byte[];
                if (imagemBytes != null)
                {
                    using (MemoryStream stream = new MemoryStream(imagemBytes))
                    {
                        pictureBox_Foto.Image = Image.FromStream(stream);
                    }
                }
            }
        }

        string TotalRegistros()
        {
            string sqlBuscarId = "select count(nomedisciplina) as total from disciplina";
            con = new SqlConnection(connectionString);
            cmd = new SqlCommand(sqlBuscarId, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            string total = "";
            try
            {
                tabDisciplina = cmd.ExecuteReader();
                if (tabDisciplina.Read())
                {
                    total = (tabDisciplina[0].ToString());
                }
                else
                {
                    MessageBox.Show("disciplina não Encontrada!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Buscar!!!");
            }
            finally
            {
                con.Close();
            }
            return total;
        }

        private void frmCadastroDisciplina_Load(object sender, EventArgs e)
        {
            tsbNovo.Enabled = true;
            tsbSalvar.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbExcluir.Enabled = false;
            btnBuscar.Enabled = true;

            txtNomeDisc.Enabled = false;
            txtEmenta.Enabled = false;
            txtCargaHoraria.Enabled = false;
            txtBibliografia.Enabled = false;
            pictureBox_Foto.Enabled = false;

            lblTotal.Text = TotalRegistros();
        }

        private void tsbNovo_Click(object sender, EventArgs e)
        {
            limpaCampos();

            tsbNovo.Enabled = false;
            tsbSalvar.Enabled = true;
            tsbCancelar.Enabled = true;
            tsbExcluir.Enabled = false;
            btnBuscar.Enabled = false;

            txtNomeDisc.Enabled = true;
            txtEmenta.Enabled = true;
            txtCargaHoraria.Enabled = true;
            txtBibliografia.Enabled = true;
            pictureBox_Foto.Enabled = true;

            txtNomeDisc.Focus();

            novo = true;
        }

        private void limpaCampos()
        {
            txtNomeDisc.Text = "";
            txtEmenta.Text = "";
            txtCargaHoraria.Text = "";
            txtBibliografia.Text = "";
            txtId.Text = "";
            pictureBox_Foto.Image = null;
        }

        private void tsbSalvar_Click(object sender, EventArgs e)
        {
            if (novo)
            {
                byte[] fotoByte = ImageJpegToByte(pictureBox_Foto.Image);

                disciplina disciplina = new disciplina();
                disciplina.nomedisciplina = txtNomeDisc.Text;
                disciplina.ementa = txtEmenta.Text;
                disciplina.cargahoraria = txtCargaHoraria.Text;
                disciplina.bibliografia = txtBibliografia.Text;
                disciplina.fotodisciplina = fotoByte;


                C_disciplina C_Disciplina = new C_disciplina();
                C_Disciplina.inserirDados(disciplina);
            }
            else
            {
                disciplina disciplina = new disciplina();
                disciplina.coddisciplina = Int32.Parse(txtId.Text);
                disciplina.nomedisciplina = txtNomeDisc.Text;
                disciplina.ementa = txtEmenta.Text;
                disciplina.cargahoraria = txtCargaHoraria.Text;
                disciplina.bibliografia = txtBibliografia.Text;

                byte[] fotoByte = ImageJpegToByte(pictureBox_Foto.Image);
                if (fotoByte != null)
                {
                    disciplina.fotodisciplina = fotoByte;

                    C_disciplina c_Disciplina = new C_disciplina();
                    c_Disciplina.editarDados(disciplina);
                }
                else
                {
                    C_disciplina c_Disciplina = new C_disciplina();
                    c_Disciplina.editarDados2(disciplina);
                }
            }

            carregarTabela();

            tsbNovo.Enabled = true;
            tsbSalvar.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbExcluir.Enabled = false;
            btnBuscar.Enabled = true;

            txtNomeDisc.Enabled = false;
            txtEmenta.Enabled = false;
            txtCargaHoraria.Enabled = false;
            txtBibliografia.Enabled = false;
            pictureBox_Foto.Enabled = false;

            limpaCampos();

            lblTotal.Text = TotalRegistros();
        }

        private byte[] ImageJpegToByte(Image image)
        {
            MemoryStream ms = new MemoryStream();
            try
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] img = ms.ToArray();
                return img;
            }catch (Exception ex)
            {
                return null;
            }
        }

        private void tsbCancelar_Click(object sender, EventArgs e)
        {
            tsbNovo.Enabled = true;
            tsbSalvar.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbExcluir.Enabled = false;
            btnBuscar.Enabled = true;

            txtNomeDisc.Enabled = false;
            txtEmenta.Enabled = false;
            txtCargaHoraria.Enabled = false;
            txtBibliografia.Enabled = false;
            pictureBox_Foto.Enabled = false;

            limpaCampos();

            lblTotal.Text = TotalRegistros();
        }

        private void tsbExcluir_Click(object sender, EventArgs e)
        {
            C_disciplina c_Disciplina = new C_disciplina();
            c_Disciplina.apagarDados(Int32.Parse(txtId.Text));

            tsbNovo.Enabled = true;
            tsbSalvar.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbExcluir.Enabled = false;
            btnBuscar.Enabled = true;

            txtNomeDisc.Enabled = false;
            txtEmenta.Enabled = false;
            txtCargaHoraria.Enabled = false;
            txtBibliografia.Enabled = false;
            pictureBox_Foto.Enabled = false;

            carregarTabela();
            lblTotal.Text = TotalRegistros();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string sqlBuscarId = "select * from disciplina where nomedisciplina LIKE @nome order by nomedisciplina";
            con = new SqlConnection(connectionString);
            cmd = new SqlCommand(sqlBuscarId, con);

            cmd.Parameters.AddWithValue("@nome", txtBuscar.Text + "%");
            cmd.CommandType = CommandType.Text;

            SqlDataReader tabDisciplina;
            con.Open();

            da = new SqlDataAdapter(cmd);
            disciplinas = new DataTable();
            da.Fill(disciplinas);
            dataGridView1.DataSource = disciplinas;

            try
            {
                tabDisciplina = cmd.ExecuteReader();
                if (tabDisciplina.Read())
                {
                    txtId.Text = tabDisciplina["coddisciplina"].ToString();
                    txtNomeDisc.Text = tabDisciplina["nomedisciplina"].ToString();
                    txtEmenta.Text = tabDisciplina["ementa"].ToString();
                    txtCargaHoraria.Text = tabDisciplina["cargahoraria"].ToString();
                    txtBibliografia.Text = tabDisciplina["bibliografia"].ToString();
                    byte[] imagemBytes = tabDisciplina["fotodisciplina"] as byte[];
                    if (imagemBytes != null)
                    {
                        using (MemoryStream stream = new MemoryStream(imagemBytes))
                        {
                            pictureBox_Foto.Image = Image.FromStream(stream);
                        }
                    }

                    tsbNovo.Enabled = false;
                    tsbSalvar.Enabled = true;
                    tsbCancelar.Enabled = true;
                    tsbExcluir.Enabled = true;

                    txtNomeDisc.Enabled = true;
                    txtEmenta.Enabled = true;
                    txtCargaHoraria.Enabled = true;
                    txtBibliografia.Enabled = true;
                    pictureBox_Foto.Enabled = true;
                    txtNomeDisc.Focus();
                    novo = false;
                }
                else
                {
                    MessageBox.Show("disciplina não Encontrada!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Buscar!!!");
            }

            finally
            {
                con.Close();
            }
            txtBuscar.Text = string.Empty;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];

            txtId.Text = selectedRow.Cells[0].Value.ToString();
            txtNomeDisc.Text = selectedRow.Cells[1].Value.ToString();
            txtEmenta.Text = selectedRow.Cells[2].Value.ToString();
            txtCargaHoraria.Text = selectedRow.Cells[3].Value.ToString();
            txtBibliografia.Text = selectedRow.Cells[4].Value.ToString();
            byte[] imagemBytes = selectedRow.Cells[5].Value as byte[];
            if (imagemBytes != null)
            {
                using (MemoryStream stream = new MemoryStream(imagemBytes))
                {
                    pictureBox_Foto.Image = Image.FromStream(stream);
                }
            }
        }

        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            posicao = Int32.Parse(TotalRegistros()) - 1;

            if (posicao > 0)
            {
                posicao = 0;
                txtId.Text = linhaAtual[posicao]["coddisciplina"].ToString();
                txtNomeDisc.Text = linhaAtual[posicao]["nomedisciplina"].ToString();
                txtEmenta.Text = linhaAtual[posicao]["ementa"].ToString();
                txtCargaHoraria.Text = linhaAtual[posicao]["cargahoraria"].ToString();
                txtBibliografia.Text = linhaAtual[posicao]["bibliografia"].ToString();
                byte[] imagemBytes = linhaAtual[posicao]["fotodisciplina"] as byte[];
                if (imagemBytes != null)
                {
                    using (MemoryStream stream = new MemoryStream(imagemBytes))
                    {
                        pictureBox_Foto.Image = Image.FromStream(stream);
                    }
                }

                dataGridView1.Rows[posicao].Selected = true;
            }
            else
            {
                MessageBox.Show("Não Existem Registros!");
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            posicao = (Int32.Parse(TotalRegistros())) - 1;
            if (posicao == -1)
            {
                MessageBox.Show("Não Existem Registros");
            }
            else
            {
                txtId.Text = linhaAtual[posicao]["coddisciplina"].ToString();
                txtNomeDisc.Text = linhaAtual[posicao]["nomedisciplina"].ToString();
                txtEmenta.Text = linhaAtual[posicao]["ementa"].ToString();
                txtCargaHoraria.Text = linhaAtual[posicao]["cargahoraria"].ToString();
                txtBibliografia.Text = linhaAtual[posicao]["bibliografia"].ToString();
                byte[] imagemBytes = linhaAtual[posicao]["fotodisciplina"] as byte[];
                if (imagemBytes != null)
                {
                    using (MemoryStream stream = new MemoryStream(imagemBytes))
                    {
                        pictureBox_Foto.Image = Image.FromStream(stream);
                    }
                }

                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (posicao > 0)
            {
                posicao--;
                txtId.Text = linhaAtual[posicao]["coddisciplina"].ToString();
                txtNomeDisc.Text = linhaAtual[posicao]["nomedisciplina"].ToString();
                txtEmenta.Text = linhaAtual[posicao]["ementa"].ToString();
                txtCargaHoraria.Text = linhaAtual[posicao]["cargahoraria"].ToString();
                txtBibliografia.Text = linhaAtual[posicao]["bibliografia"].ToString();
                byte[] imagemBytes = linhaAtual[posicao]["fotodisciplina"] as byte[];
                if (imagemBytes != null)
                {
                    using (MemoryStream stream = new MemoryStream(imagemBytes))
                    {
                        pictureBox_Foto.Image = Image.FromStream(stream);
                    }
                }

                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (posicao < Int32.Parse(TotalRegistros()) - 1)
            {
                posicao++;
                txtId.Text = linhaAtual[posicao]["coddisciplina"].ToString();
                txtNomeDisc.Text = linhaAtual[posicao]["nomedisciplina"].ToString();
                txtEmenta.Text = linhaAtual[posicao]["ementa"].ToString();
                txtCargaHoraria.Text = linhaAtual[posicao]["cargahoraria"].ToString();
                txtBibliografia.Text = linhaAtual[posicao]["bibliografia"].ToString();
                byte[] imagemBytes = linhaAtual[posicao]["fotodisciplina"] as byte[];
                if (imagemBytes != null)
                {
                    using (MemoryStream stream = new MemoryStream(imagemBytes))
                    {
                        pictureBox_Foto.Image = Image.FromStream(stream);
                    }
                }

                dataGridView1.Rows[posicao].Selected = true;
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|AllFiles(*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foto = dialog.FileName.ToString();
                pictureBox_Foto.ImageLocation = foto;
            }
            /*  private void btnRelatorio_Click(object sender, EventArgs e)
    {
    //clientes é datatable
    FrmRelCliente frmRelCliente = new FrmRelCliente(clientes);
    frmRelCliente.ShowDialog();
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
    FrmRelProfessor frmrelprof = new FrmRelProfessor();
    frmrelprof.ShowDialog();
    }*/
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //clientes é datatable
            FrmDisc frmRel = new FrmDisc(disciplinas);
            frmRel.ShowDialog();
        }

        private void pictureBox_Foto_Click(object sender, EventArgs e)
        {

        }

        private void lblBuscaId_Click(object sender, EventArgs e)
        {

        }

        private void frmCadastroDisciplina_Load_1(object sender, EventArgs e)
        {

        }
    }

}