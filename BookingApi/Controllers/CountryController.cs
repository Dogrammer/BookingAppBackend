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
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;

        public CountryController(ICountryService countryService, IMapper mapper)
        {
            _countryService = countryService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("getCountries")]
        public async Task<IActionResult> GetCountriesAsync()
        {
            var countries = await _countryService
                .Queryable()
                .AsNoTracking()
                .Where(c => !c.IsDeleted)
                .ToListAsync();

            return Ok(countries);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("countries")]
        public async Task<ActionResult> AddCountry(CreateCountryRequest request)
        {
            //var existing = await _apartmentGroupService
            //    .Queryable()
            //    .Where(x => !x.IsDeleted && x.Id == request.Id)
            //    .AsNoTracking()
            //    .SingleOrDefaultAsync();

            var domain = _mapper.Map<Country>(request);
            _countryService.Insert(domain);

            await _countryService.Save();

            return Ok(domain);

        }

        

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("editCountry/{id}")]
        public async Task<ActionResult> EditCountry(long id, CreateCountryRequest request)
        {
            var existing = _countryService.Queryable().FirstOrDefault(a => a.Id == id);

            if (existing != null)
            {
                existing.Name = request.Name;

                await _countryService.Save();

                return Ok();
            }

            return BadRequest("Does not exist");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("deleteCountry/{id}")]
        public async Task<ActionResult> DeleteCountry(long id)
        {
            var existing = _countryService.Queryable().FirstOrDefault(a => a.Id == id);

            if (existing != null)
            {
                existing.IsDeleted = true;
                existing.DateDeleted = DateTimeOffset.UtcNow;

                await _countryService.Save();

                return Ok();
            }

            return BadRequest("Does not exist");
        }

    }
}