using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace PompeFunebre.Pages.ProduseFacturi
{
    public class IndexModel : PageModel
    {
        public List<ProduseFacturi> produseFacturiList = new List<ProduseFacturi>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Server=DESKTOP-5I0R9P2\\SQLEXPRESS;Database=PompeFunebre;User Id=sa;Password=Admin;TrustServerCertificate=True;MultipleActiveResultSets=true";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //Interogarea simpla nr2
                    String sql = "SELECT f.Numar, p.Nume, p.Pret FROM ProduseFacturi pf JOIN Facturi f ON pf.FacturiID = f.FacturiID JOIN Produse p ON pf.ProduseID = p.ProduseID ";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProduseFacturi profuseInfo = new ProduseFacturi();

                                profuseInfo.Numar = reader.GetInt32(0);
                                profuseInfo.Nume = reader.GetString(1);
                                profuseInfo.Pret = reader.GetDecimal(2);

                                produseFacturiList.Add(profuseInfo);
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

    public class ProduseFacturi
    {
        public Int32 Numar;
        public String Nume;
        public Decimal Pret;
    }
}