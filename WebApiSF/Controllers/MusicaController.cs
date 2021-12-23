using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApiSF.Models;

namespace WebApiSF.Controllers
{
    public class MusicaController : Controller
    {
        string cs = "DATA SOURCE=RENAN-PC; INITIAL CATALOG=FTI; Trusted_Connection=True";
        [HttpGet]//suporta GET

        // GET: MusicaController
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(cs)) //FECHA A CONEXAO DEPOIS DE USAR
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SELECT*FROM Bands", conn);
                da.Fill(dt); //PREENCHE OS VALORES NA DT
                return (View(dt));
            }                                                
            
            return View();
        }

        // GET: MusicaController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new MusicModel());
        }

        // POST: MusicaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MusicModel musicmodel)
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
                return RedirectToAction("Index");

        }

        // GET: MusicaController/Edit/5
        public ActionResult Edit(int id)
        {
            MusicModel musicmodel = new MusicModel();
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();

                string comando = "SELECT*FROM Bands WHERE ID=@ID";
                SqlDataAdapter da = new SqlDataAdapter(comando, conn);
                da.SelectCommand.Parameters.AddWithValue("@ID", id);
                da.Fill(dt);
            }
            if (dt.Rows.Count == 1) //se encontrar um ID
            {
                musicmodel.ID = Convert.ToInt32(dt.Rows[0][0].ToString());
                musicmodel.Banda = dt.Rows[0][1].ToString();
                musicmodel.Musica = dt.Rows[0][2].ToString();
                return View(musicmodel);
            }
            else
            {
                return RedirectToAction("Index");
            }
                
        }

        // POST: MusicaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MusicModel musicmodel)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                string comando = "UPDATE Bands SET Banda=@Banda, Musica=@Musica WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(comando, conn);
                cmd.Parameters.AddWithValue("ID", musicmodel.ID);
                cmd.Parameters.AddWithValue("@Banda", musicmodel.Banda);
                cmd.Parameters.AddWithValue("@Musica", musicmodel.Musica);
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
           
            
        }

        // GET: MusicaController/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                string comando = "DELETE FROM Bands WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(comando, conn);
                cmd.Parameters.AddWithValue("ID", id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

    }
}
