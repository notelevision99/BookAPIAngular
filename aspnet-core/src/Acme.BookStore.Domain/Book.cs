    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Volo.Abp.Domain.Entities.Auditing;

    namespace Acme.BookStore
    {
        public class Book : AuditedAggregateRoot<Guid>
        {
            public string BookName { get; set; }
            public string BookType { get; set; }
            public string Description { get; set; }
        }
    }
