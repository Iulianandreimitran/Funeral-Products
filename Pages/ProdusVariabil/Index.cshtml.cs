using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

namespace PompeFunebre.Pages.ProdusVariabil
{
    public class IndexModel : PageModel
    {
        public List<ProdusVariabil> ProdusVariabilList = new List<ProdusVariabil>();
        public List<SelectListItem> ProductNames = new List<SelectListItem>();

        public void OnGet(string productName)
        {
            try
            {


                String connectionString = "Server=DESKTOP-5I0R9P2\\SQLEXPRESS;Database=PompeFunebre;User Id=sa;Password=Admin;TrustServerCertificate=True;MultipleActiveResultSets=true";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //Interogarea complexa cu parametru variabil
                    String sql = @"
                    SELECT p.Nume, p.Pret
                    FROM Produse p
                    WHERE (SELECT AVG(pf.CantitateProdus)
                           FROM ProduseFacturi pf
                           JOIN Produse p2 ON pf.ProduseID = p2.ProduseID
                           WHERE p2.Nume = p.Nume) < (SELECT SUM(pf.CantitateProdus)
                                                      FROM ProduseFacturi pf
                                                      JOIN Produse p2 ON pf.ProduseID = p2.ProduseID
                                                      WHERE p2.Nume = @productName)
                ";

                    String sqlProductNames = "SELECT DISTINCT Nume FROM Produse";
                    using (SqlCommand command = new SqlCommand(sqlProductNames, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductNames.Add(new SelectListItem
                                {
                                    Value = reader.GetString(0),
                                    Text = reader.GetString(0)
                                });
                            }
                        }
                    }
            
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ProductName", productName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProdusVariabil info = new ProdusVariabil();

                                info.Nume = reader.GetString(0);
                                info.Pret = reader.GetDecimal(1);

                                ProdusVariabilList.Add(info);

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

    public class ProdusVariabil
    {
        public String Nume;
        public Decimal Pret;
    }
    public class SelectListItem
    {
        public String Value;
        public String Text;
    }
}