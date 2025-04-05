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
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private CompanyDbContext Context { get; }

        public GenericRepository(CompanyDbContext context)
        {
            Context = context;
        }
        public IEnumerable<T> GetALL()
        {
            if (typeof(T) == typeof(Employee)) { return (IEnumerable<T>)Context.Employees.Include(e => e.Department).ToList(); }

            return Context.Set<T>().ToList();
        }
        public T? Get(int id)
        {
            return Context.Set<T>().Find(id);
        }
        public void Add(T model)
        {
            Context.Set<T>().Add(model);
            //return Context.SaveChanges();
        }

        public void Delete(T model)
        {
            Context.Set<T>().Remove(model);

        }

        public void Update(T model)
        {
            Context.Set<T>().Update(model);

        }
    }
}
