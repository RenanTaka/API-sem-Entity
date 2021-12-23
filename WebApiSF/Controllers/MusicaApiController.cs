using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApiSF.Models;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiSF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicaApiController : ControllerBase
    {
        string cs = "DATA SOURCE=RENAN-PC; INITIAL CATALOG=FTI; Trusted_Connection=True";
        // GET: api/<MusicaApiController>
        [HttpGet]
        public IEnumerable<MusicModel> GetAllMusic()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(cs)) //FECHA A CONEXAO DEPOIS DE USAR
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SELECT*FROM Bands", conn);
                da.Fill(dt); //PREENCHE OS VALORES NA DT
                
        
            }
            //EXECUTA UMA FUNÇÃO PRA CADA ITEM ITERADO
            return dt.AsEnumerable().Select(row => new MusicModel
            {
                ID = Convert.ToInt32(row["ID"]),
                Banda = row["Banda"].ToString(),
                Musica = row["Musica"].ToString()

            });
    
        }

        // GET api/<MusicaApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MusicaApiController>
        [HttpPost]
        public string Post([FromBody] MusicModel musicmodel)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                string comando = "INSERT INTO Bands VALUES(@Banda, @Musica)";
                SqlCommand cmd = new SqlCommand(comando, conn);
                cmd.Parameters.AddWithValue("@Banda", musicmodel.Banda);
                cmd.Parameters.AddWithValue("@Musica", musicmodel.Musica);
                cmd.ExecuteNonQuery();
            }
            return "Musica adicionada com sucesso! \r\n" + "ID: " + musicmodel.ID + "\r\n" + "Banda: " + musicmodel.Banda + "\r\n" + "Musica: " + musicmodel.Musica;
        }

        // PUT api/<MusicaApiController>/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] MusicModel musicmodel)
        {
            
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(cs))
            {

                conn.Open();
                string comando = "UPDATE Bands SET Banda=@Banda, Musica=@Musica WHERE ID="+id;
                SqlCommand cmd = new SqlCommand(comando, conn);
                
                cmd.Parameters.AddWithValue("@Banda", musicmodel.Banda);
                cmd.Parameters.AddWithValue("@Musica", musicmodel.Musica);
                cmd.ExecuteNonQuery();
                int result = cmd.ExecuteNonQuery();

                if (result > 0) //se encontrar um ID
                {
                   /* musicmodel.ID = Convert.ToInt32(dt.Rows[0][0].ToString());
                    musicmodel.Banda = dt.Rows[0][1].ToString();
                    musicmodel.Musica = dt.Rows[0][2].ToString();*/
                    return "Musica atualizada com sucesso! \r\n" + "ID: " + musicmodel.ID + "\r\n" + "Banda: " + musicmodel.Banda + "\r\n" + "Musica: " + musicmodel.Musica; 
                }
                else
                {
                    return ("ID não encontrado !");
                }
            }
            
        }

        // DELETE api/<MusicaApiController>/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                string comando = "DELETE FROM Bands WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(comando, conn);
                cmd.Parameters.AddWithValue("ID", id);
                cmd.ExecuteNonQuery();
            }
            return ("Arquivo deletado com sucesso!");
        }
    }
}
