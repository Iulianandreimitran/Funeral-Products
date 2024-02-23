using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace PompeFunebre.Pages.AngajatiDepartamente
{
    public class IndexModel : PageModel
    {
        public List<AngajatiDepartamente> angajatiDepartamenteList = new List<AngajatiDepartamente>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Server=DESKTOP-5I0R9P2\\SQLEXPRESS;Database=PompeFunebre;User Id=sa;Password=Admin;TrustServerCertificate=True;MultipleActiveResultSets=true";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //Interogarea simpla nr 3
                    String sql = "SELECT a.Nume, a.Prenume, d.NumeDepartament FROM Angajati a JOIN Departamente d ON a.DepartamentID = d.DepartamenteID ";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AngajatiDepartamente info = new AngajatiDepartamente();

                                info.Nume = reader.GetString(0);
                                info.Prenume = reader.GetString(1);
                                info.NumeDepartament = reader.GetString(2);

                                angajatiDepartamenteList.Add(info);
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

    public class AngajatiDepartamente
    {
        public String Nume;
        public String Prenume;
        public String NumeDepartament;
    }
}