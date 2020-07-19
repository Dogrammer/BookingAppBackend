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
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentService _apartmentService;
        private readonly IMapper _mapper;

        public ApartmentController(IApartmentService apartmentService, IMapper mapper)
        {
            _apartmentService = apartmentService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("getApartments")]
        public async Task<IActionResult> getApartment()
        {
            var apartmentGroups = await _apartmentService
                .Queryable()
                .AsNoTracking()
                .Where(c => !c.IsDeleted)
                .ToListAsync();

            return Ok(apartmentGroups);
        }

        [HttpGet]
        [Route("getApartmentById/{id}")]
        public async Task<IActionResult> GetApartmentById(long id)
        {
            var apartments = await _apartmentService
                .Queryable()
                .AsNoTracking()
                .Where(c => !c.IsDeleted && c.ApartmentGroupId == id)
                .ToListAsync();

            return Ok(apartments);
        }

        [HttpPost]
        [Route("apartments")]
        public async Task<ActionResult> AddApartment(CreateApartmentRequest request)
        {
            //var existing = await _apartmentGroupService
            //    .Queryable()
            //    .Where(x => !x.IsDeleted && x.Id == request.Id)
            //    .AsNoTracking()
            //    .SingleOrDefaultAsync();

            var domain = _mapper.Map<Apartment>(request);
            _apartmentService.Insert(domain);

            await _apartmentService.Save();

            return Ok(domain);

        }
    }
}