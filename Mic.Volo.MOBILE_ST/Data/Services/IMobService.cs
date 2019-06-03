using Mic.Volo.MOBILE_ST.Data.Models;
using Mic.Volo.MOBILE_ST.Data.Smod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mic.Volo.MOBILE_ST.Data.Services
{
    public interface IMobService
    {
        Task<IEnumerable<Phone>> GetPhones(string company = null);
        Task<IEnumerable<Phone>> GetPhonesOfTheWeek();

        Task<Phone> GetPhoneById(int phoneId);

        Task<IEnumerable<PhoneNameIdSMod>> GetAllPhonesNameId();


        void UpdatePhone(Phone phone);
        Task AddPhoneAsync(Phone phone);
        void Delete(int id);

    }
}
