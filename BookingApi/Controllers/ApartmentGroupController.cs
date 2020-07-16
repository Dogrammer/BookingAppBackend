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
    public class ApartmentGroupController : ControllerBase
    {
        private readonly IApartmentGroupService _apartmentGroupService;
        private readonly IMapper _mapper;

        public ApartmentGroupController(IApartmentGroupService apartmentGroupService, IMapper mapper)
        {
            _apartmentGroupService = apartmentGroupService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("getApartmentGroups")]
        public async Task<IActionResult> getApartmentGroups()
        {
            var apartmentGroups = await _apartmentGroupService
                .Queryable()
                .AsNoTracking()
                .Where(c => !c.IsDeleted)
                .ToListAsync();

            return Ok(apartmentGroups);
        }

        [HttpPost]
        [Route("apartmentGroups")]
        public async Task<ActionResult> AddApartmentGroup(CreateApartmentGroupRequest request)
        {
            //var existing = await _apartmentGroupService
            //    .Queryable()
            //    .Where(x => !x.IsDeleted && x.Id == request.Id)
            //    .AsNoTracking()
            //    .SingleOrDefaultAsync();

            var domain = _mapper.Map<ApartmentGroup>(request);
            _apartmentGroupService.Insert(domain);

            await _apartmentGroupService.Save();

            return Ok(domain);

        }
    }
}