using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mic.Volo.MOBILE_ST.Data.Models;
using Mic.Volo.MOBILE_ST.Data.Services;
using Mic.Volo.MOBILE_ST.Data.Smod;
using Mic.Volo.MOBILE_ST.Data.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mic.Volo.MOBILE_ST.Controllers
{
    //[Authorize(Roles ="Admin")]
    [Authorize]
    [Route("/admin/managePhones")]
    public class AdminController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMobService _mobSerice;
        private readonly IMapper _mapper;
        private readonly IUWork _uWork;
        private readonly ICompanyService _companyService;

        public AdminController(
            IOrderService orderService,
            IMobService mobSerice,
            IMapper mapper,
            IUWork uWork,
            ICompanyService companyService)
        {
            _orderService = orderService;
            _mobSerice = mobSerice;
            _mapper = mapper;
            _uWork = uWork;
            _companyService = companyService;
            
        }
        [HttpGet("allOrders")]
        public async Task<IActionResult> AllOrders()
        {
            ViewBag.ActionTitle = "All Orders";
            var orders = await _orderService.GetAllOrdersAsync();
            return View(orders);
        }
        [HttpGet("")]
        public async Task<IActionResult> ManagePhones()
        {
            var phones = await _mobSerice.GetAllPhonesNameId();
            return View(phones);
        }
        [HttpGet("add")]
        public async Task<IActionResult> AddPhone()
        {
            var company = await _companyService.GetCompanies();
            return View(new PhoneCreateUpdateViewModel
            {
                Companies = company
            }) ;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddPhone(PhoneSMod phoneSMod)
        {
            if (!ModelState.IsValid)
            {
                var company = await _companyService.GetCompanies();
                return View(new PhoneCreateUpdateViewModel
                {
                    Companies = company,
                    PhoneSMod = phoneSMod
                });
            }
            var phone = _mapper.Map<PhoneSMod, Phone>(phoneSMod);
            await _mobSerice.AddPhoneAsync(phone);
            await _uWork.CompleteAsync();
            return RedirectToAction("ManagePhones");
        }
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> EditPhone(int id)
        {
            var phone = await _mobSerice.GetPhoneById(id);
            var phoneSMod = _mapper.Map<Phone, PhoneSMod>(phone);
            var company = await _companyService.GetCompanies();

            return View(new PhoneCreateUpdateViewModel
            {
                Companies = company,
                PhoneSMod = phoneSMod
            });
        }
        [HttpPost("edit/{id}")]
        public async Task<IActionResult> EditPhone(int id,[FromForm] PhoneSMod phoneSMod)
        {
            if(!ModelState.IsValid)
            {
                var company = await _companyService.GetCompanies();
                return View(new PhoneCreateUpdateViewModel
                {
                    Companies = company,
                    PhoneSMod = phoneSMod
                });

            }
            var phone = _mapper.Map<PhoneSMod, Phone>(phoneSMod);
            phone.Id = id;
            _mobSerice.UpdatePhone(phone);
            await _uWork.CompleteAsync();

            return RedirectToAction("ManagePhones");

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhone(int id)
        {
            _mobSerice.Delete(id);
            await _uWork.CompleteAsync();
            return Ok();
        }
    }
}