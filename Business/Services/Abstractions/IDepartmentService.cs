using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Business.Services.Abstractions
{
    public interface IDepartmentService
    {
        public List<Department> GetDepartments();
        public List<Subdepartment> GetSubdepartments();
    }
}
