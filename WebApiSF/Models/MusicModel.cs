using System.ComponentModel;
namespace WebApiSF.Models
{
    public class MusicModel
    {
        public int ID { get; set; }
        [DisplayName("Nome da música")]
        public string Banda { get; set; }
        public string Musica { get; set; }

        /*public void wilian()
        {
            MusicModel model = new MusicModel();
            model.ID = asp;
        }*/
    }
    
}
