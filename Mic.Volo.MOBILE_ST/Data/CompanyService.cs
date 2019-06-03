using Mic.Volo.MOBILE_ST.Data.AppDbCont;
using Mic.Volo.MOBILE_ST.Data.Models;
using Mic.Volo.MOBILE_ST.Data.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mic.Volo.MOBILE_ST.Data
{
    public class CompanyService : ICompanyService
    {
        private readonly ApplicationDbContext _context;

        public CompanyService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Company>> GetCompanies()
        {
            return await _context.Companies.ToListAsync();
        }
    }
}
