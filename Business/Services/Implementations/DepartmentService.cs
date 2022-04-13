using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Business.Services.Abstractions;

namespace Business.Services.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private List<Department> DepartmentList = TestData.DepartmentList;

        public List<Department> GetDepartments()
        {
            return DepartmentList;
        }

        public List<Subdepartment> GetSubdepartments(int departmentID)
        {
            var result = DepartmentList.FirstOrDefault(department => department.Id.Equals(departmentID));

            if (result != null)
                return result.Subdepartments;

            return new List<Subdepartment>();
        }

        public List<Subdepartment> GetSubdepartments()
        {
            throw new NotImplementedException();
        }
    }
}
