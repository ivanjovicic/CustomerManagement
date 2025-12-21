using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.Hosting;

namespace CustomerManagement.WebApp.Helpers
{
    public static class DatabaseInitializer
    {
        public static void RunInitScript()
        {
            try
            {
                var cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                
                var builder = new SqlConnectionStringBuilder(cs)
                {
                    InitialCatalog = string.Empty
                };
                var serverConnectionString = builder.ConnectionString;

               
                using (var conn = new SqlConnection(serverConnectionString))
                using (var checkCmd = new SqlCommand("SELECT DB_ID('CustomerTest')", conn))
                {
                    conn.Open();
                    var result = checkCmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        // Database already exists, no need to run init script
                        return;
                    }
                }

                var path = HostingEnvironment.MapPath("~/App_Data/Sql/CreateDatabaseAndCustomersTable.sql");
                if (string.IsNullOrEmpty(path) || !File.Exists(path))
                {
                    return;
                }

                var script = File.ReadAllText(path);

                // Split batches on GO separators (on their own line)
                var batches = script.Split(new[] { "\r\nGO\r\n", "\nGO\n", "\r\nGO\n", "\nGO\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                using (var conn = new SqlConnection(serverConnectionString))
                {
                    conn.Open();

                    foreach (var batch in batches)
                    {
                        using (var cmd = new SqlCommand(batch, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Mark app as running in demo/read-only mode
                AppState.IsDatabaseAvailable = false;
            }
        }
    }
}
