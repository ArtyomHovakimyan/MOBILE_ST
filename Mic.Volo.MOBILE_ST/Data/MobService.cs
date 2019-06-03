using Mic.Volo.MOBILE_ST.Data.AppDbCont;
using Mic.Volo.MOBILE_ST.Data.Models;
using Mic.Volo.MOBILE_ST.Data.Services;
using Mic.Volo.MOBILE_ST.Data.Smod;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mic.Volo.MOBILE_ST.Data
{
    public class MobService : IMobService
    {
        private readonly ApplicationDbContext _context;

        public MobService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Phone> GetPhoneById(int phoneId)
        {
            return await _context.Phones.FirstAsync(e => e.Id == phoneId);
        }
        public async Task<IEnumerable<Phone>> GetPhones(string company = null)
        {
            var query = _context.Phones.Include(c => c.Company)
                .AsQueryable();
            if(!string.IsNullOrWhiteSpace(company))
            {
                query = query.Where(c => c.Company.Name == company);
            }
            return await query.ToListAsync();
        }
        public async Task AddPhoneAsync(Phone phone)
        {
            await _context.Phones.AddAsync(phone);
        }

        public void Delete(int id)
        {
            var phone = new Phone { Id = id };
            _context.Entry(phone).State = EntityState.Deleted;
        }

        public async Task<IEnumerable<PhoneNameIdSMod>> GetAllPhonesNameId()
        {
            return await _context.Phones.
                Select(e => new PhoneNameIdSMod
                {
                    Id = e.Id,
                    Name = e.Name
                }).ToListAsync();
        }



        public async Task<IEnumerable<Phone>> GetPhonesOfTheWeek()
        {
            return await _context.Phones.
                Where(e => e.IsPhoneOfTheWeek)
                .Include(e => e.Company)
                .ToListAsync();
        }

        public void UpdatePhone(Phone phone)
        {
            _context.Phones.Update(phone);
        }
    }
}
