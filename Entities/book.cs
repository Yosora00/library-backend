using SqlSugar;

namespace library_backend.Entities
{
    [SugarTable("book")]
    public class book
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string id { get; set; }
        public string name { get; set; }
        public string isbn { get; set; }
        public string press { get; set; }
        public string author { get; set; }
        public int number { get; set; }
        public string image { get; set; }
        public string ebook { get; set; }
    }
}
