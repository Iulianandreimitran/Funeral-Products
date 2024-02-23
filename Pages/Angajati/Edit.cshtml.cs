using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;     

namespace PompeFunebre.Pages.Angajati
{
    public class EditModel : PageModel
    {   
        public AngajatiInfo angajatiInfo = new AngajatiInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String AngajatiID = Request.Query["AngajatiID"];
            
            try
            {
                String connectionString = "Server=DESKTOP-5I0R9P2\\SQLEXPRESS;Database=PompeFunebre;User Id=sa;Password=Admin;TrustServerCertificate=True;MultipleActiveResultSets=true";
                using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        String sql = "SELECT * FROM Angajati WHERE AngajatiID=@AngajatiID ";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@AngajatiID", AngajatiID);
                            using(SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    angajatiInfo.AngajatiID = "" + reader.GetInt32(0);
                                    angajatiInfo.DepartamentID = reader.GetInt32(1);
                                    angajatiInfo.Nume = reader.GetString(2);
                                    angajatiInfo.Prenume = reader.GetString(3);
                                    angajatiInfo.DataNasterii = reader.GetDateTime(4);
                                    angajatiInfo.CNP = reader.GetString(5);
                                    angajatiInfo.Strada = reader.GetString(6);
                                    angajatiInfo.Numar = reader.GetString(7);
                                    angajatiInfo.Sex = reader.GetString(8);
                                    angajatiInfo.Telefon = reader.GetString(9);
                                    angajatiInfo.Oras = reader.GetString(10);
                                    angajatiInfo.Judet = reader.GetString(11);
                                    angajatiInfo.Salariu = reader.GetDecimal(12);
                                    angajatiInfo.Email = reader.GetString(13);
                                
                                }
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            angajatiInfo.AngajatiID = Request.Form["AngajatiID"];
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


            if (angajatiInfo.AngajatiID.Length == 0 ||
               angajatiInfo.DepartamentID == 0 || angajatiInfo.Nume.Length == 0 ||
              angajatiInfo.Prenume.Length == 0 ||
              angajatiInfo.CNP.Length == 0 || angajatiInfo.Strada.Length == 0 ||
              angajatiInfo.Numar.Length == 0 || angajatiInfo.Sex.Length == 0 ||
              angajatiInfo.Telefon.Length == 0 || angajatiInfo.Oras.Length == 0 ||
              angajatiInfo.Judet.Length == 0 ||
              angajatiInfo.Salariu == 0 || angajatiInfo.Email.Length == 0)
            {
                    errorMessage = "All fields are required";
                    return;
                }

            try
            {
                String connectionString = "Server=DESKTOP-5I0R9P2\\SQLEXPRESS;Database=PompeFunebre;User Id=sa;Password=Admin;TrustServerCertificate=True;MultipleActiveResultSets=true";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Angajati " +
                                "SET DepartamentID=@departamentID, Nume=@nume, Prenume=@prenume, DataNasterii=@datanasterii, CNP=@cnp, Strada=@strada, Numar=@numar, Sex=@sex, Telefon=@telefon, Oras=@oras, Judet=@judet, Salariu=@salariu, Email=@email " +
                                "WHERE AngajatiID=@AngajatiID";
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
                        command.Parameters.AddWithValue("@AngajatiID", angajatiInfo.AngajatiID);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Angajati/Index");
        }
        
    }

}
