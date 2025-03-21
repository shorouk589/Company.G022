﻿using Company.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G02.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        public IEnumerable<T> GetALL();

        T? Get(int id);

        int Add(T model);

        int Update(T model);


        int Delete(T model);

    }
}
