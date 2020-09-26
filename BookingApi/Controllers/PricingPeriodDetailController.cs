using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookingCore.RequestModels;
using BookingCore.Services;
using BookingCore.ViewModels;
using BookingDomain.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingPeriodDetailController : ControllerBase
    {
        private readonly IPricingPeriodDetailService _pricingPeriodDetailService;
        private readonly IMapper _mapper;

        public PricingPeriodDetailController(IPricingPeriodDetailService pricingPeriodDetailService, IMapper mapper)
        {
            _pricingPeriodDetailService = pricingPeriodDetailService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("getPricingPeriodDetails")]
        public async Task<IActionResult> GetPricingPeriodDetails()
        {
            var pricingPeriodDetails = await _pricingPeriodDetailService
                .Queryable()
                .AsNoTracking()
                .Where(c => !c.IsDeleted)
                .ToListAsync();

            return Ok(pricingPeriodDetails);
        }

        [HttpPost]
        [Route("pricingPeriodDetails")]
        public async Task<ActionResult> AddPricingPeriodDetail(CreatePricingPeriodDetailRequest request)
        {

            var existing =  _pricingPeriodDetailService
                .Queryable()
                .Where(a => a.ApartmentId == request.ApartmentId).ToList();
            if (existing.Count > 0 && existing != null)
            {
                foreach (var deleteItem in existing)
                {
                    _pricingPeriodDetailService.Delete(deleteItem);
                }
            }

            

            foreach(var detail in request.PricingPeriodDetails)
            {
                detail.ApartmentId = request.ApartmentId;
                _pricingPeriodDetailService.Insert(detail);
            }



            
                
            
            //var existing = await _apartmentGroupService
            //    .Queryable()
            //    .Where(x => !x.IsDeleted && x.Id == request.Id)
            //    .AsNoTracking()
            //    .SingleOrDefaultAsync();

            //var domain = _mapper.Map<PricingPeriodDetail>(request);
            //_pricingPeriodDetailService.Insert(domain);

            await _pricingPeriodDetailService.Save();

            return Ok();

        }

        [HttpGet]
        [Route("getPricingPeriodDetailsForApartment/{id}")]
        public async Task<IActionResult> GetPricingPeriodDetailsForApartment(long id)
        {
            var pricingPeriodDetails = await _pricingPeriodDetailService
                .Queryable()
                .AsNoTracking()
                .Where(c => !c.IsDeleted && c.ApartmentId == id)
                .ToListAsync();

            //var retVal = _mapper.Map<List<PricingPeriodDetailViewModel>>(pricingPeriodDetails);

            return Ok(pricingPeriodDetails);
        }

        //getPricingPeriodDetailsForApartment
    }
}