﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPizza.ApplicationCore.Interfaces
{
    public interface ICartQueryService
    {
        Task<int> CountCartProductsAsync(Guid? userId);
    }
}
