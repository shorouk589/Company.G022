using Company.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G02.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetALLAsync();

        Task<T?> GetAsync(int id);

        //int Add(T model);
        Task AddAsync(T model);

        //int Update(T model);
        void Update(T model);

        //int Delete(T model);
        void Delete(T model);

    }
}
