﻿namespace Company.G02.PL.Services
{
    public interface IScopedService
    {
        public Guid Guid { get; set; }
        public string GetGuid();

    }
}
