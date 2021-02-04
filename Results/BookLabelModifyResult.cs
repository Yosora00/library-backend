using library_backend.Entities;
using System.Collections.Generic;

namespace library_backend.Results
{
    public class BookLabelModifyResult : ResultBase
    {
        public List<string> errorlist { get; set; }
    }
}
