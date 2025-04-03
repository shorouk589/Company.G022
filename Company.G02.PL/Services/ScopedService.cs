﻿
namespace Company.G02.PL.Services
{
    public class ScopedService : IScopedService
    {
        public Guid Guid { get; set; }

        public ScopedService()
        {
            Guid = Guid.NewGuid();

        }
        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
