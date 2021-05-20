    namespace Acme.ProjectCompare
    {
        public static class ProjectCompareDbProperties
        {
            public static string DbTablePrefix { get; set; } = "BookDb";

            public static string DbSchema { get; set; } = null;
            
            public const string ConnectionStringName = "BookDb";
        }
    }
