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
    internal class DepartmentRepository : IDepartmentRepository
    {
        private CompanyDbContext _context;
        public DepartmentRepository()
        {
            _context = new CompanyDbContext();
        }
        public IEnumerable<Department> GetALL()
        {

            return _context.Departments.ToList();
        }


        public Department? Get(int id)
        {

            return _context.Departments.Find(id);
        }

        public int Add(Department model)
        {
            _context.Departments.Add(model);
            return _context.SaveChanges();
        }

        public int Delete(Department model)
        {

            _context.Departments.Update(model);
            return _context.SaveChanges();
        }



        public int Update(Department model)
        {

            _context.Departments.Remove(model);
            return _context.SaveChanges();
        }
    }
}
