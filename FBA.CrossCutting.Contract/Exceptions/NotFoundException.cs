﻿using System;

namespace FBA.CrossCutting.Contract.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {}
        
        public NotFoundException()
        {}
    }
}