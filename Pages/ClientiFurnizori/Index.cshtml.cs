using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace PompeFunebre.Pages.ClientiFurnizori
{
    public class IndexModel : PageModel
    {
        public List<ClientiFurnizori> ClientiFurnizoriList = new List<ClientiFurnizori>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Server=DESKTOP-5I0R9P2\\SQLEXPRESS;Database=PompeFunebre;User Id=sa;Password=Admin;TrustServerCertificate=True;MultipleActiveResultSets=true";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //Interogarea complexa nr3
                    String sql = "SELECT c.Nume, c.Prenume FROM Clienti c WHERE (SELECT COUNT(DISTINCT p.FurnizoriID) FROM Facturi f JOIN ProduseFacturi pf ON f.FacturiID = pf.FacturiID JOIN Produse p ON pf.ProduseID = p.ProduseID WHERE f.ClientiID = c.ClientiID) = 1";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientiFurnizori info = new ClientiFurnizori();
                                info.Nume = reader.GetString(0);
                                info.Prenume = reader.GetString(1);

                                ClientiFurnizoriList.Add(info);
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

    public class ClientiFurnizori
    {
        public String Nume;
        public String Prenume;
    }
}