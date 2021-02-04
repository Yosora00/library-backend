using library_backend.Entities;
using System.Collections.Generic;

namespace library_backend.Results
{
    public class BookSearchResult : ResultBase
    {
        public List<book> books { get; set; }
    }
}