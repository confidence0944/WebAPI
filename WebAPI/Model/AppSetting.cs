namespace WebAPI.Model
{
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
