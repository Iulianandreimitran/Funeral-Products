using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace PompeFunebre.Pages.Angajati
{
    public class IndexModel : PageModel
    {
        public List<AngajatiInfo> listAngajati = new List<AngajatiInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Server=DESKTOP-5I0R9P2\\SQLEXPRESS;Database=PompeFunebre;User Id=sa;Password=Admin;TrustServerCertificate=True;MultipleActiveResultSets=true";
            

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Angajati";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AngajatiInfo angajatInfo = new AngajatiInfo();
                                angajatInfo.AngajatiID = "" + reader.GetInt32(0);
                                angajatInfo.DepartamentID = reader.GetInt32(1);
                                angajatInfo.Nume = reader.GetString(2);
                                angajatInfo.Prenume = reader.GetString(3);
                                angajatInfo.DataNasterii = reader.GetDateTime(4);
                                angajatInfo.CNP = reader.GetString(5);  
                                angajatInfo.Strada = reader.GetString(6);   
                                angajatInfo.Numar = reader.GetString(7);    
                                angajatInfo.Sex = reader.GetString(8);
                                angajatInfo.Telefon = reader.GetString(9);
                                angajatInfo.Oras = reader.GetString(10);    
                                angajatInfo.Judet = reader.GetString(11);
                                angajatInfo.Salariu = reader.GetDecimal(12);
                                angajatInfo.Email = reader.GetString(13); 
                                
                                listAngajati.Add(angajatInfo);



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

    public class AngajatiInfo
    {
        public String AngajatiID;
        public int DepartamentID;
        public String Nume;
        public String Prenume;
        public DateTime DataNasterii;
        public String CNP;
        public String Strada;
        public String Numar;
        public String Sex;
        public String Telefon;
        public String Oras;
        public String Judet;
        public Decimal Salariu;
        public String Email;
    }
}
