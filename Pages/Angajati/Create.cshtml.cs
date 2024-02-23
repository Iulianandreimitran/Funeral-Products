using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace PompeFunebre.Pages.Angajati
{
    public class CreateModel : PageModel
    {
        public AngajatiInfo angajatiInfo = new AngajatiInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            angajatiInfo.DepartamentID = Int32.Parse(Request.Form["departament"]);
            angajatiInfo.Nume = Request.Form["nume"];
            angajatiInfo.Prenume = Request.Form["prenume"];
            angajatiInfo.DataNasterii = DateTime.Parse(Request.Form["datanasterii"]);
            angajatiInfo.CNP = Request.Form["cnp"];
            angajatiInfo.Strada = Request.Form["strada"];
            angajatiInfo.Numar = Request.Form["numar"];
            angajatiInfo.Sex = Request.Form["sex"];
            angajatiInfo.Telefon = Request.Form["telefon"];
            angajatiInfo.Oras = Request.Form["oras"];
            angajatiInfo.Judet = Request.Form["judet"];
            angajatiInfo.Salariu = Decimal.Parse(Request.Form["salariu"]);
            angajatiInfo.Email = Request.Form["email"];
        

            if(angajatiInfo.DepartamentID == 0 || angajatiInfo.Nume.Length == 0 ||
               angajatiInfo.Prenume.Length == 0 ||
               angajatiInfo.CNP.Length == 0 || angajatiInfo.Strada.Length == 0 ||
               angajatiInfo.Numar.Length == 0 || angajatiInfo.Sex.Length == 0 ||
               angajatiInfo.Telefon.Length == 0 || angajatiInfo.Oras.Length == 0 ||
               angajatiInfo.Judet.Length == 0 ||
               angajatiInfo.Salariu == 0 || angajatiInfo.Email.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;

            }
            //save the new client into database
            try
            {
                String connectionString = "Server=DESKTOP-5I0R9P2\\SQLEXPRESS;Database=PompeFunebre;User Id=sa;Password=Admin;TrustServerCertificate=True;MultipleActiveResultSets=true";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Angajati" +
                                "(DepartamentID,Nume,Prenume,DataNasterii,CNP,Strada,Numar,Sex,Telefon,Oras,Judet,Salariu,Email) VALUES " +
                                "(@departamentID,@nume,@prenume,@datanasterii,@cnp,@strada,@numar,@sex,@telefon,@oras,@judet,@salariu,@email);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@departamentID", angajatiInfo.DepartamentID);
                        command.Parameters.AddWithValue("@nume", angajatiInfo.Nume);
                        command.Parameters.AddWithValue("@prenume", angajatiInfo.Prenume);
                        command.Parameters.AddWithValue("@datanasterii", angajatiInfo.DataNasterii);
                        command.Parameters.AddWithValue("@cnp", angajatiInfo.CNP);
                        command.Parameters.AddWithValue("@strada", angajatiInfo.Strada);
                        command.Parameters.AddWithValue("@numar", angajatiInfo.Numar);
                        command.Parameters.AddWithValue("@sex", angajatiInfo.Sex);
                        command.Parameters.AddWithValue("@telefon", angajatiInfo.Telefon);
                        command.Parameters.AddWithValue("@oras", angajatiInfo.Oras);
                        command.Parameters.AddWithValue("@judet", angajatiInfo.Judet);
                        command.Parameters.AddWithValue("@salariu", angajatiInfo.Salariu);
                        command.Parameters.AddWithValue("@email", angajatiInfo.Email);

                        command.ExecuteNonQuery();

                    }
                
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            angajatiInfo.Nume = "";angajatiInfo.Prenume = "";angajatiInfo.CNP = "";
            angajatiInfo.Strada = "";angajatiInfo.Numar = "";angajatiInfo.Sex = "";
            angajatiInfo.Telefon = ""; angajatiInfo.Oras = ""; angajatiInfo.Judet = "";
            angajatiInfo.Email = "";
            successMessage = "Angajat Nou Adaugat Corect";

            Response.Redirect("/Angajati/Index");
        }

    }
}
