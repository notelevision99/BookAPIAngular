    namespace Acme.ProjectCompare
    {
        public static class ProjectCompareDbProperties
        {
            public static string DbTablePrefix { get; set; } = "ProjectCompare";

            public static string DbSchema { get; set; } = null;
            
            public const string ConnectionStringName = "ProjectCompare";
        }
    }
