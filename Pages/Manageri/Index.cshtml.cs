using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace PompeFunebre.Pages.Manageri
{
    public class IndexModel : PageModel
    {
        public List<Manageri> ManageriList = new List<Manageri>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Server=DESKTOP-5I0R9P2\\SQLEXPRESS;Database=PompeFunebre;User Id=sa;Password=Admin;TrustServerCertificate=True;MultipleActiveResultSets=true";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //Interogarea simpla nr5
                    String sql = "SELECT Angajati.Nume AS NumeAngajat,Angajati.Prenume AS PrenumeAngajat, Manager.Nume AS NumeManager, Manager.Prenume AS PrenumeManager FROM Angajati JOIN Departamente ON Angajati.DepartamentID = Departamente.DepartamenteID JOIN Angajati AS Manager ON Departamente.ManagerID = Manager.AngajatiID\r\n";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Manageri info = new Manageri();
                                info.AngajatNume = reader.GetString(0);
                                info.AngajatPrenume = reader.GetString(1);
                                info.ManagerNume = reader.GetString(2);
                                info.ManagerPrenume = reader.GetString(3);

                                ManageriList.Add(info);
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

    public class Manageri
    {
        public String AngajatNume;
        public String AngajatPrenume;
        public String ManagerNume;
        public String ManagerPrenume;
    }
}
