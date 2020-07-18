using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookingCore.Services;
using BookingDomain.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingPeriodController : ControllerBase
    {
        private readonly IPricingPeriodService _pricingPeriodService;
        private readonly IMapper _mapper;

        public PricingPeriodController(IPricingPeriodService pricingPeriodService, IMapper mapper)
        {
            _pricingPeriodService = pricingPeriodService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("getPricingPeriods")]
        public async Task<IActionResult> GetPricingPeriods()
        {
            var pricingPeriods = await _pricingPeriodService
                .Queryable()
                .AsNoTracking()
                .Where(c => !c.IsDeleted)
                .ToListAsync();

            return Ok(pricingPeriods);
        }

        [HttpPost]
        [Route("pricingPeriods")]
        public async Task<ActionResult> AddPricingPeriod(CreatePricingPeriodRequest request)
        {
            //var existing = await _apartmentGroupService
            //    .Queryable()
            //    .Where(x => !x.IsDeleted && x.Id == request.Id)
            //    .AsNoTracking()
            //    .SingleOrDefaultAsync();

            var domain = _mapper.Map<PricingPeriod>(request);
            _pricingPeriodService.Insert(domain);

            await _pricingPeriodService.Save();

            return Ok(domain);

        }
    }
}