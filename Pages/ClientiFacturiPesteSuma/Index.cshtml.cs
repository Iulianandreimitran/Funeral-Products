using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace PompeFunebre.Pages.ClientiFacturiPesteSuma
{
    public class IndexModel : PageModel
    {
        public List<ClientiFacturiPesteSuma> ClientiFacturiPesteSumaList = new List<ClientiFacturiPesteSuma>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Server=DESKTOP-5I0R9P2\\SQLEXPRESS;Database=PompeFunebre;User Id=sa;Password=Admin;TrustServerCertificate=True;MultipleActiveResultSets=true";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Interogarea complexa nr1
                    String sql = "SELECT c.Nume, c.Prenume FROM Clienti c WHERE (SELECT SUM(pf.CantitateProdus * p.Pret) FROM ProduseFacturi pf JOIN Produse p ON pf.ProduseID = p.ProduseID JOIN Facturi f ON pf.FacturiID = f.FacturiID WHERE f.ClientiID = c.ClientiID) > 1000";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientiFacturiPesteSuma info = new ClientiFacturiPesteSuma();

                                info.Nume = reader.GetString(0);
                                info.Prenume = reader.GetString(1);

                                ClientiFacturiPesteSumaList.Add(info);
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

    public class ClientiFacturiPesteSuma
    {
        public String Nume;
        public String Prenume;
    }
}
