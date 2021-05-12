using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thesis.Domain.Entities.Abstract;

namespace Thesis.Domain.Commons
{
    public abstract class AuditableEntity : BaseEntity
    {
        public DateTime Created { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public int LastModifiedBy { get; set; }
    }
}
