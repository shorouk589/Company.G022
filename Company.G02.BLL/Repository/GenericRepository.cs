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
        public async Task<IEnumerable<T>> GetALLAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>)await Context.Employees.Include(e => e.Department).ToListAsync();
            }

            return await Context.Set<T>().ToListAsync();
        }
        public async Task<T?> GetAsync(int id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return await Context.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.Id == id) as T;
            }
            return Context.Set<T>().Find(id);
        }
        public async Task AddAsync(T model)
        {
            await Context.Set<T>().AddAsync(model);
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
