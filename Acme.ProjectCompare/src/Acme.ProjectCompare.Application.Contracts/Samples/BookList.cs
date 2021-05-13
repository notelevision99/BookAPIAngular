using System;
using System.Collections.Generic;
using System.Text;

    namespace Acme.ProjectCompare.Samples
    {
        public class BookList
        {
            public int TotalPage { get; set; }
            
            public int CurrentPage { get; set; }

            public int PageSize { get; set; }

            public int TotalCount { get; set; }

            public int PreviousPage { get; set; }

            public int NextPage { get; set; }
            public List<BookDto> Books { get; set; }
        }
    }
