using SqlSugar;

namespace library_backend.Entities
{
    [SugarTable("booklabel")]
    public class booklabel
    {
        public string bookId { get; set; }
        public string labelId { get; set; }
    }
}