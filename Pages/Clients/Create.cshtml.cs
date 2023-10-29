using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

namespace UsersRolesStatus.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public List<SelectListItem> StatusList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> RoleList { get; set; } = new List<SelectListItem>();
        public CreateModel()
        {
            try
            {
                string connectionString = "Data Source=ALIMPC;Initial Catalog=URSdatabase;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM status";
                    string sql2 = "SELECT * FROM roles";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read())
                            {
                                int statusId = reader.GetInt32(0);
                                string statusName = reader.GetString(1);
                                SelectListItem item = new SelectListItem(statusName, "" + statusId);
                                StatusList.Add(item);
                            }
                        }
                    }
                    using (SqlCommand command2 = new SqlCommand(sql2, connection))
                    {
                        using (SqlDataReader reader2 = command2.ExecuteReader())
                        {
                            while (reader2.Read()) {
                                int roleId = reader2.GetInt32(0);
                                string roleName = reader2.GetString(1);
                                SelectListItem item2 = new SelectListItem(roleName, "" + roleId);
                                RoleList.Add(item2);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
                return;
            }
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.user_status_id = Request.Form["user_status_id"];
            clientInfo.user_role_id = Request.Form["user_role_id"];

            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0 || clientInfo.user_status_id.Length == 0 || clientInfo.user_role_id.Length == 0)
            {
                errorMessage = "All fields are required to be filled!";
                    return;
            }


            //save the new client into the database
            try
            {
                string connectionString = "Data Source=ALIMPC;Initial Catalog=URSdatabase;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO users " + "(username, email) VALUES " + "(@name, @email);" + "DECLARE @newUserId INT; " + "SET @newUserId = SCOPE_IDENTITY(); " + "INSERT INTO userStatus " + "(userId, statusId) VALUES " + "(@newUserId, @user_status_id);" + "INSERT INTO userRoles " + "(userId, roleId) VALUES " + "(@newUserId, @user_role_id);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@user_status_id", clientInfo.user_status_id);
                        command.Parameters.AddWithValue("@user_role_id", clientInfo.user_role_id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
                return;
            }
            

            clientInfo.name = "";
            clientInfo.email = "";
            successMessage = "New client added successfully!";

            Response.Redirect("/Clients/ClientsIndex");

        }
    }
}
