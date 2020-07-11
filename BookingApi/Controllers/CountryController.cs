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

        [HttpPost]
        [Route("countries")]
        public async Task<ActionResult> AddCountry(CreateCountryRequest request)
        {
            var existing = await _countryService
                .Queryable()
                .Where(x => x.Name == request.Name && !x.IsDeleted)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            if (existing == null)
            {
                var domain = _mapper.Map<Country>(request);
                _countryService.Insert(domain);

                await _countryService.Save();
            }

            var newlyAdded = await _countryService
                .Queryable()
                .AsNoTracking()
                .Where(py => py.Name.ToUpper() == request.Name.ToUpper())
                .SingleOrDefaultAsync();

            return Ok(newlyAdded);

        }
    }
}