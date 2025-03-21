using Company.G02.BLL.Interfaces;
using Company.G02.DAL.Data.Contexts;
using Company.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G02.BLL.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private CompanyDbContext Context { get; }

<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< Updated upstream
        public EmployeeRepository(CompanyDbContext context)
=======
        public EmployeeRepository(CompanyDbContext context) : base(context)//ASK CLR Create Object From CompanyDbContext    
>>>>>>> Stashed changes
=======
        public EmployeeRepository(CompanyDbContext context)
>>>>>>> 9dcd77fcc30c7140568edbaa33281339ddfaf8bf
=======
        public EmployeeRepository(CompanyDbContext context)
>>>>>>> 9dcd77fcc30c7140568edbaa33281339ddfaf8bf
        {
            Context = context;
        }

        public IEnumerable<Employee> GetALL()
        {
            return Context.Employees.ToList();
        }
        public Employee? Get(int id)
        {
            return Context.Employees.Find(id);

        }


        public int Add(Employee model)
        {
            Context.Employees.Add(model);
            return Context.SaveChanges();
        }

        public int Delete(Employee model)
        {
            Context.Employees.Remove(model);
            return Context.SaveChanges();
        }


        public int Update(Employee model)
        {
            Context.Employees.Update(model);
            return Context.SaveChanges();
        }

    }
}
