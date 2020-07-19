using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookingCore.RequestModels;
using BookingCore.Services;
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
            //var existing = await _apartmentGroupService
            //    .Queryable()
            //    .Where(x => !x.IsDeleted && x.Id == request.Id)
            //    .AsNoTracking()
            //    .SingleOrDefaultAsync();

            var domain = _mapper.Map<PricingPeriodDetail>(request);
            _pricingPeriodDetailService.Insert(domain);

            await _pricingPeriodDetailService.Save();

            return Ok(domain);

        }
    }
}