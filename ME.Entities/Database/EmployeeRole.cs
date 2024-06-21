using Models.UserControls.Enums;

namespace ME.Entities.Database
{
    public class EmployeeRole 
    {
        public long Id { get; set; }

        public long EmployeeId { get; set; }

        public EmployeeRoleEnum Role { get; set; }
    }
}
