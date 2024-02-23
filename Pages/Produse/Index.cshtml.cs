using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace PompeFunebre.Pages.Produse
{
    public class IndexModel : PageModel
    {
        public List<ProduseInfo> listProduse = new List<ProduseInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Server=DESKTOP-5I0R9P2\\SQLEXPRESS;Database=PompeFunebre;User Id=sa;Password=Admin;TrustServerCertificate=True;MultipleActiveResultSets=true";
            

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Produse ";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProduseInfo produseInfo = new ProduseInfo();
                                produseInfo.ProduseID ="" + reader.GetInt32(0);
                                produseInfo.FurnizoriID = reader.GetInt32(1);
                                produseInfo.Nume = reader.GetString(2);
                                produseInfo.Pret = reader.GetDecimal(3);
                                produseInfo.Material = reader.GetString(4);
                                produseInfo.Stoc = reader.GetString(5);

                                listProduse.Add(produseInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class ProduseInfo
    {
        public String ProduseID;
        public int FurnizoriID;
        public String Nume;
        public Decimal Pret;
        public String Material;
        public String Stoc;
    }
}
