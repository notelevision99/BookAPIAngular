using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.ProjectCompare.Model
{
    public class Book : AuditedAggregateRoot<Guid>
    {
        public string BookName { get; set; }
        public string BookType { get; set; }
        
        public string Description { get; set; }
    }
}
