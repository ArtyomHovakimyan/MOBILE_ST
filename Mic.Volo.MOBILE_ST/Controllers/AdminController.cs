using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mic.Volo.MOBILE_ST.Data.Services;
using Mic.Volo.MOBILE_ST.Data.Smod;
using Mic.Volo.MOBILE_ST.Data.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mic.Volo.MOBILE_ST.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("/admin/managePhones")]
    public class AdminController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMobSerice _mobSerice;
        private readonly IUWork _uWork;
        private readonly ICompanyService _companyService;

        public AdminController(
            IOrderService orderService,
            IMobSerice mobSerice,
            IUWork uWork,
            ICompanyService companyService)
        {
            _orderService = orderService;
            _mobSerice = mobSerice;
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
        //public async Task<IActionResult> AddPhone(PhoneSMod phoneSMod)
        //{
        //    if(!ModelState.IsValid)
        //    {
        //        var company = await _companyService.GetCompanies();
        //        return View(new PhoneCreateUpdateViewModel
        //        {
        //            Companies = company,
        //            PhoneSMod = phoneSMod
        //        });
        //    }
        //    var phone=
        //}
        public IActionResult Index()
        {
            return View();
        }
    }
}