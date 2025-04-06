using Company.G02.BLL.Interfaces;
using Company.G02.DAL.Data.Contexts;
using Company.G02.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G02.BLL.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        /////private CompanyDbContext Context { get; }
        ///
        /////public EmployeeRepository(CompanyDbContext context)
        /////{
        /////    Context = context;
        /////}
        ///
        /////public IEnumerable<Employee> GetALL()
        /////{
        /////    return Context.Employees.ToList();
        /////}
        /////public Employee? Get(int id)
        /////{
        /////    return Context.Employees.Find(id);
        ///
        /////}
        ///
        ///
        /////public int Add(Employee model)
        /////{
        /////    Context.Employees.Add(model);
        /////    return Context.SaveChanges();
        /////}
        ///
        /////public int Delete(Employee model)
        /////{
        /////    Context.Employees.Remove(model);
        /////    return Context.SaveChanges();
        /////}
        ///
        ///
        /////public int Update(Employee model)
        /////{
        /////    Context.Employees.Update(model);
        /////    return Context.SaveChanges();
        /////}
        ///


        private CompanyDbContext _Context { get; }
        public EmployeeRepository(CompanyDbContext context) : base(context)//ASK CLR Create Object From CompanyDbContext    
        {
            _Context = context;
        }



        public async Task<List<Employee>> GetByNameAsync(string name)
        {
            return await _Context.Employees.Include(E => E.Department)
                                      .Where(E => E.Name
                                      .ToLower()
                                      .Contains(name.ToLower()))
                                      .ToListAsync();

        }


    }
}
