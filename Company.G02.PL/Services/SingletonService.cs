﻿
namespace Company.G02.PL.Services
{
    public class SingletonService : ISingletonService
    {
        public Guid Guid { get; set; }

        public SingletonService()
        {
            Guid = Guid.NewGuid();

        }
        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
