﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thesis.Domain.Commons
{
    public abstract class AuditableEntity : BaseEntity
    {
        public DateTime Created { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public int LastModifiedBy { get; set; }

        protected void Create(int userId)
        {
            Created = DateTime.UtcNow;
            CreatedBy = userId;
        }

        protected void Update(int userId)
        {
            LastModified = DateTime.UtcNow;
            LastModifiedBy = userId;
        }
    }
}
