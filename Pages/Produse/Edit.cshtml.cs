using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;     

namespace PompeFunebre.Pages.Produse
{
    public class EditModel : PageModel
    {   
        public ProduseInfo produseInfo = new ProduseInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String ProduseID = Request.Query["ProduseID"];
            
            try
            {
                String connectionString = "Server=DESKTOP-5I0R9P2\\SQLEXPRESS;Database=PompeFunebre;User Id=sa;Password=Admin;TrustServerCertificate=True;MultipleActiveResultSets=true";
                using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        String sql = "SELECT * FROM Produse WHERE ProduseID=@ProduseID";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@ProduseID", ProduseID);
                            using(SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    produseInfo.ProduseID = "" + reader.GetInt32(0);
                                    produseInfo.FurnizoriID = reader.GetInt32(1);
                                    produseInfo.Nume = reader.GetString(2);
                                    produseInfo.Pret = reader.GetDecimal(3);
                                    produseInfo.Material = reader.GetString(4);
                                    produseInfo.Stoc = reader.GetString(5);

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
            produseInfo.ProduseID = Request.Form["ProduseID"];
            produseInfo.FurnizoriID = Int32.Parse(Request.Form["furnizoriID"]);
            produseInfo.Nume = Request.Form["nume"];
            produseInfo.Pret = Decimal.Parse(Request.Form["pret"]);
            produseInfo.Material = Request.Form["material"]; ;
            produseInfo.Stoc = Request.Form["stoc"];


            if (produseInfo.FurnizoriID == 0 || produseInfo.Nume.Length == 0 ||
               produseInfo.Pret == 0 || produseInfo.Material.Length == 0 ||
               produseInfo.Stoc.Length == 0)
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
                    String sql = "UPDATE Produse " +
                                "SET FurnizoriID=@furnizoriID, Nume=@nume, Pret=@pret, Material=@material, Stoc=@stoc " +
                                "WHERE ProduseID=@ProduseID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@furnizoriID", produseInfo.FurnizoriID);
                        command.Parameters.AddWithValue("@nume", produseInfo.Nume);
                        command.Parameters.AddWithValue("@pret", produseInfo.Pret);
                        command.Parameters.AddWithValue("@material", produseInfo.Material);
                        command.Parameters.AddWithValue("@stoc", produseInfo.Stoc);
                        command.Parameters.AddWithValue("@ProduseID", produseInfo.ProduseID);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Produse/Index");
        }
        
    }

}
