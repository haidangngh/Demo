﻿using System;
using System.Collections.Generic;

namespace AppData.Models
{
    public partial class Facility
    {
        public Facility()
        {
            DepartmentFacilities = new HashSet<DepartmentFacility>();
        }

        public byte? Status { get; set; }
        public long? CreatedDate { get; set; }
        public long? LastModifiedDate { get; set; }
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<DepartmentFacility> DepartmentFacilities { get; set; }
    }
}
