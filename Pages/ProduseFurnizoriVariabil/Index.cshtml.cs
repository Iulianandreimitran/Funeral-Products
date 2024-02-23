using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace PompeFunebre.Pages.ProduseurnizoriVariabil
{
    public class IndexModel : PageModel
    {
        public List<ProduseurnizoriVariabil> ProduseurnizoriVariabilList = new List<ProduseurnizoriVariabil>();
        public List<SelectListItem> ProductNames = new List<SelectListItem>();
        public void OnGet(string productName)
        {
            try
            {
                String connectionString = "Server=DESKTOP-5I0R9P2\\SQLEXPRESS;Database=PompeFunebre;User Id=sa;Password=Admin;TrustServerCertificate=True;MultipleActiveResultSets=true";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //Interogarea simpla cu parametru variabil
                    String sql = @"
                    SELECT p.Nume, p.Pret, f.Nume
                    FROM Produse p
                    JOIN Furnizori f ON p.FurnizoriID = f.FurnizoriID
                    WHERE p.Material = @productName";
                    String sqlProductNames = "SELECT DISTINCT Material FROM Produse";
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
                                ProduseurnizoriVariabil info = new ProduseurnizoriVariabil();

                                info.Nume = reader.GetString(0);
                                info.Pret = reader.GetDecimal(1);
                                info.NumeFurnizor = reader.GetString(2);

                                ProduseurnizoriVariabilList.Add(info);
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

    public class ProduseurnizoriVariabil
    {
        public String Nume;
        public Decimal Pret;
        public String NumeFurnizor;
    }
    public class SelectListItem
    {
        public String Value;
        public String Text;
    }
}