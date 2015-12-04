﻿using System;

namespace Employee.Model
{
    public class Employee
    {
        public int Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime? Birthdate { get; set; }

        public int? DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        public byte[] RowVersion { get; set; }
    }
}