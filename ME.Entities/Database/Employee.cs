using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ME.Entities.Database
{
    public class Employee
    {
        public long Id { get; set; }
        [StringLength(30)]
        public string? Name { get; set; }
        [StringLength(50)]
        public string? LastName { get; set; }
        [StringLength(20)]
        public string Username { get; set; } = null!;
        [StringLength(64)]
        public string? Password { get; set; }
        [StringLength(13)]
        public string? Mobile { get; set; }

        [StringLength(320)]
        public string? Gmail { get; set; }

        public virtual ICollection<EmployeeRole> Roles { get; set; } = new List<EmployeeRole>();
    }
}
