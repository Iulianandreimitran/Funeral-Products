using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data;
using System.Data.SqlClient;

namespace PompeFunebre.Pages.ListaFurnizoritotalComenzi
{
    public class IndexModel : PageModel
    {
        public List<ListaFurnizoritotalComenzi> angajatiDepartamenteList1 = new List<ListaFurnizoritotalComenzi>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Server=DESKTOP-5I0R9P2\\SQLEXPRESS;Database=PompeFunebre;User Id=sa;Password=Admin;TrustServerCertificate=True;MultipleActiveResultSets=true";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //Interogarea simpla nr1
                    String sql = "SELECT f.Nume, SUM(p.Pret) AS TotalComenzi  FROM Furnizori f  JOIN Produse p ON f.FurnizoriID = p.FurnizoriID GROUP BY f.Nume";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ListaFurnizoritotalComenzi info = new ListaFurnizoritotalComenzi();
                                info.Nume = reader.GetString(0);
                                info.Pret = reader.GetDecimal(1);

                                angajatiDepartamenteList1.Add(info);
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

    public class ListaFurnizoritotalComenzi
    {
        public String Nume;
        public Decimal Pret;
    }
}