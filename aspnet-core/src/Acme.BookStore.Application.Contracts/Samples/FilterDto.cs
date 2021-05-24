using System;
using System.Collections.Generic;
using System.Text;

namespace Acme.BookStore.Samples
{
    public class FilterDto
    {
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public string searchString { get; set; }
    }
}
