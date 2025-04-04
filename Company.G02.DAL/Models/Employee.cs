﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G02.DAL.Models
{
    public class Employee :BaseEntity
    {


        public string Name { get; set; }
        public int? Age { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [DisplayName("Hiring Date")]
        public DateTime DateOfBirth { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [DisplayName("Department")]
        public int? DepartmentId { get; set; }

        public Department Department { get; set; }

        public string? ImageName { get; set; }


        
    }
}
