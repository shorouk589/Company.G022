﻿namespace Company.G02.PL.Services
{
    public interface ISingletonService
    {
        public Guid Guid { get; set; }
        public string GetGuid();

    }
}
