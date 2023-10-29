using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

namespace UsersRolesStatus.Pages.Clients
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public List<SelectListItem> StatusList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> RoleList { get; set; } = new List<SelectListItem>();
        public EditModel()
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
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
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
                            while (reader2.Read())
                            {
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
            string id = Request.Query["id"];

            try
            {
                string connectionString = "Data Source=ALIMPC;Initial Catalog=URSdatabase;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT users.userId, users.username, users.email, users.created_at, userStatus.statusId, userRoles.roleId, status.statusName, roles.roleName FROM users INNER JOIN userStatus ON users.userId=userStatus.userId INNER JOIN userRoles ON users.userId=userRoles.userId INNER JOIN status ON userStatus.statusId=status.statusId INNER JOIN roles ON userRoles.roleId=roles.roleId WHERE users.userId=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.created_at = reader.GetDateTime(3).ToString();
                                clientInfo.user_status_id = "" + reader.GetInt32(4);
                                clientInfo.user_role_id = "" + reader.GetInt32(5);
                                clientInfo.user_status = reader.GetString(6);
                                clientInfo.user_role = reader.GetString(7);
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
            clientInfo.id = Request.Form["id"];
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.user_status_id = Request.Form["user_status_id"];
            clientInfo.user_role_id = Request.Form["user_role_id"];

            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0)
            {
                errorMessage = "All fields are required to be filled!";
                return;
            }

            try
            {
                string connectionString = "Data Source=ALIMPC;Initial Catalog=URSdatabase;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE users " + "SET username=@name, email=@email " + "WHERE users.userId=@id " + "UPDATE userStatus " + "SET statusId=@user_status_id " + "WHERE userStatus.userId=@id " + "UPDATE userRoles " + "SET roleId=@user_role_id " + "WHERE userRoles.userId=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@id", clientInfo.id);
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

            Response.Redirect("/Clients/ClientsIndex");
        }
    }
}
