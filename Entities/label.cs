using SqlSugar;

namespace library_backend.Entities
{
    [SugarTable("label")]
    public class label
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string id { get; set; }
        public string name { get; set; }
    }
}
