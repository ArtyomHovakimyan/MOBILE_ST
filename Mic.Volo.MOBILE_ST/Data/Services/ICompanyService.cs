﻿using Mic.Volo.MOBILE_ST.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mic.Volo.MOBILE_ST.Data.Services
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetCompanies();
    }
}
