using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace PompeFunebre.Pages.FacturiClienti
{
    public class IndexModel : PageModel
    {
        public List<FacturiClienti> FacturiClientiList = new List<FacturiClienti>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Server=DESKTOP-5I0R9P2\\SQLEXPRESS;Database=PompeFunebre;User Id=sa;Password=Admin;TrustServerCertificate=True;MultipleActiveResultSets=true";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //Interogarea simpla nr4
                    String sql = "SELECT c.Nume, c.Prenume, COUNT(f.FacturiID) as NumarFacturi\r\nFROM Clienti c\r\nLEFT JOIN Facturi f ON c.ClientiID = f.ClientiID\r\nGROUP BY c.Nume, c.Prenume, c.ClientiID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                FacturiClienti info = new FacturiClienti();
                                info.Nume = reader.GetString(0);
                                info.Prenume = reader.GetString(1);
                                info.NumarFacturi ="" + reader.GetInt32(2);

                                FacturiClientiList.Add(info);
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

    public class FacturiClienti
    {
        public String Nume;
        public String Prenume;
        public String NumarFacturi;
    }
}