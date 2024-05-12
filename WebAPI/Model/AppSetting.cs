namespace WebAPI.Model
{
    /// <summary>
    /// Appsettings.json Mapping物件
    /// </summary>
    public class AppSetting
    {
        public string CoindeskApiUrl { get; set; }

        public ConnectionString ConnectionStrings { get; set; }

        public class ConnectionString
        {
            public string DbConnection { get; set; }
        }
    }
}
