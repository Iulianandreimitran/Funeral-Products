using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace PompeFunebre.Pages.SalariuPesteMedie
{
    public class IndexModel : PageModel
    {
        public List<SalariuPesteMedie> SalariuPesteMedieList = new List<SalariuPesteMedie>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Server=DESKTOP-5I0R9P2\\SQLEXPRESS;Database=PompeFunebre;User Id=sa;Password=Admin;TrustServerCertificate=True;MultipleActiveResultSets=true";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //Interogarea complexa nr3
                    String sql = "SELECT a.Nume, a.Prenume, a.Salariu FROM Angajati a WHERE a.Salariu > (SELECT AVG(a2.Salariu) FROM Angajati a2 WHERE a2.DepartamentID = a.DepartamentID)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SalariuPesteMedie info = new SalariuPesteMedie();

                                info.Nume = reader.GetString(0);
                                info.Prenume = reader.GetString(1);
                                info.Salariu = reader.GetDecimal(2);

                                SalariuPesteMedieList.Add(info);
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

    public class SalariuPesteMedie
    {
        public String Nume;
        public String Prenume;
        public Decimal Salariu;
    }
}
