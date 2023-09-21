namespace Shared.Contracts.Options
{
    public class MongoOptions
    {
        public const string SectionName = "Mongo";
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}