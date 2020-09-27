using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookingCore.RequestModels;
using BookingCore.Services;
using BookingDomain.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public CityController(ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("getCities")]
        public async Task<IActionResult> GetCitiesAsync()
        {
            var cities = await _cityService
                .Queryable()
                .Include(x => x.Country)
                .AsNoTracking()
                .Where(c => !c.IsDeleted)
                .ToListAsync();

            return Ok(cities);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        [Route("cities")]
        public async Task<ActionResult> AddCity(CreateCityRequest request)
        {
            //var existing = await _apartmentGroupService
            //    .Queryable()
            //    .Where(x => !x.IsDeleted && x.Id == request.Id)
            //    .AsNoTracking()
            //    .SingleOrDefaultAsync();

            var domain = _mapper.Map<City>(request);
            _cityService.Insert(domain);

            await _cityService.Save();

            return Ok(domain);

        }

        [Authorize (Roles ="Admin")]
        [HttpPut]
        [Route("editCity/{id}")]
        public async Task<ActionResult> EditCity(long id, CreateCityRequest request)
        {
            var existing = _cityService.Queryable().FirstOrDefault(a => a.Id == id);

            if (existing != null)
            {
                existing.Name = request.Name;
                existing.Description = request.Description;
                existing.CountryId = request.CountryId;

                await _cityService.Save();

                return Ok();
            }

            return BadRequest("Does not exist");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("deleteCity/{id}")]
        public async Task<ActionResult> DeleteCity(long id)
        {
            var existing = _cityService.Queryable().FirstOrDefault(a => a.Id == id);

            if (existing != null)
            {
                existing.IsDeleted = true;
                existing.DateDeleted = DateTimeOffset.UtcNow;

                await _cityService.Save();

                return Ok();
            }

            return BadRequest("Does not exist");
        }


    }
}