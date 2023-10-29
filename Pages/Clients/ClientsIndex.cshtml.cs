using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace UsersRolesStatus.Pages.Clients
{
    public class ClientsIndexModel : PageModel
    {

        public List<ClientInfo> listClients = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=ALIMPC;Initial Catalog=URSdatabase;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT users.userId, users.username, users.email, users.created_at, userStatus.statusId, userRoles.roleId, status.statusName, roles.roleName FROM users INNER JOIN userStatus ON users.userId=userStatus.userId INNER JOIN userRoles ON users.userId=userRoles.userId INNER JOIN status ON userStatus.statusId=status.statusId INNER JOIN roles ON userRoles.roleId=roles.roleId ORDER BY userId DESC";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.created_at = reader.GetDateTime(3).ToString();
                                clientInfo.user_status_id = "" + reader.GetInt32(4);
                                clientInfo.user_role_id = "" + reader.GetInt32(5);
                                clientInfo.user_status = reader.GetString(6);
                                clientInfo.user_role = reader.GetString(7);

                                listClients.Add(clientInfo);
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

    public class ClientInfo
    {
        public string id;
        public string name;
        public string email;
        public string created_at;
        public string user_status_id;
        public string user_role_id;
        public string user_status;
        public string user_role;

    }
}
