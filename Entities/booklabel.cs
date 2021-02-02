using SqlSugar;

namespace library_backend.Entities
{
    [SugarTable("booklabel")]
    public class booklabel
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string id { get; set; }
        public string bookId { get; set; }
        public string labelId { get; set; }
    }
}
