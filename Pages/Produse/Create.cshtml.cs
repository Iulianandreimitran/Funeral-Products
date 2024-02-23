using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace PompeFunebre.Pages.Produse
{
    public class CreateModel : PageModel
    {
        public ProduseInfo produseInfo = new ProduseInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            produseInfo.FurnizoriID = Int32.Parse(Request.Form["furnizoriID"]);
            produseInfo.Nume = Request.Form["nume"];
            produseInfo.Pret = Decimal.Parse(Request.Form["pret"]);
            produseInfo.Material = Request.Form["material"]; ;
            produseInfo.Stoc = Request.Form["stoc"];
        

            if(produseInfo.FurnizoriID == 0 || produseInfo.Nume.Length == 0 ||
               produseInfo.Pret == 0 || produseInfo.Material.Length == 0 || 
               produseInfo.Stoc.Length == 0)
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
                    String sql = "INSERT INTO Produse" +
                                "(FurnizoriID,Nume,Pret,Material,Stoc) VALUES " +
                                "(@furnizoriId,@nume,@pret,@material,@stoc);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@furnizoriID", produseInfo.FurnizoriID);
                        command.Parameters.AddWithValue("@nume", produseInfo.Nume);
                        command.Parameters.AddWithValue("@pret", produseInfo.Pret);
                        command.Parameters.AddWithValue("@material", produseInfo.Material);
                        command.Parameters.AddWithValue("@stoc", produseInfo.Stoc);

                        command.ExecuteNonQuery();

                    }
                
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            } 
            produseInfo.Nume = "";
            produseInfo.Material = "";
            successMessage = "Produs Nou Adaugat Corect";

            Response.Redirect("/Produse/Index");
        }

    }
}
