using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Department
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public List<Subdepartment> Subdepartments { get; set; }
        public Department(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("El nombre no puede ser vacio");

            Name = name;
        }
    }
}
