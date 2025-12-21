namespace CustomerManagement.WebApp
{
    public static class AppState
    {
        // Indicates if the database is available. Set to false when DB init fails.
        public static bool IsDatabaseAvailable { get; set; } = true;
    }
}
