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
    public class ApartmentTypeController : ControllerBase
    {
        private readonly IApartmentTypeService _apartmentTypeService;
        private readonly IMapper _mapper;

        public ApartmentTypeController(IApartmentTypeService apartmentTypeService, IMapper mapper)
        {
            _apartmentTypeService = apartmentTypeService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("getApartmentTypes")]
        public async Task<IActionResult> GetApartmentTypes()
        {
            var apartmentTypes = await _apartmentTypeService
                .Queryable()
                .AsNoTracking()
                .Where(c => !c.IsDeleted)
                .ToListAsync();

            return Ok(apartmentTypes);
        }

        [HttpPost]
        [Route("apartmentTypes")]
        public async Task<ActionResult> AddApartmentTypes(CreateApartmentTypeRequest request)
        {
            //var existing = await _apartmentGroupService
            //    .Queryable()
            //    .Where(x => !x.IsDeleted && x.Id == request.Id)
            //    .AsNoTracking()
            //    .SingleOrDefaultAsync();

            var domain = _mapper.Map<ApartmentType>(request);
            _apartmentTypeService.Insert(domain);

            await _apartmentTypeService.Save();

            return Ok(domain);

        }
    }
}