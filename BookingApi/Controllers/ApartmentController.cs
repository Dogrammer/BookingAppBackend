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
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentService _apartmentService;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public ApartmentController(
            IApartmentService apartmentService, 
            IMapper mapper,
            IImageService imageService)
        {
            _apartmentService = apartmentService;
            _mapper = mapper;
            _imageService = imageService;
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
        [Route("getApartmentByApartmentGroupId/{id}")]
        public async Task<IActionResult> GetApartmentById(long id)
        {
            var apartments = await _apartmentService
                .Queryable()
                .AsNoTracking()
                .Where(c => !c.IsDeleted && c.ApartmentGroupId == id)
                .ToListAsync();

            return Ok(apartments);
        }

        

        [HttpGet]
        [Route("getApartment/{id}")]
        public async Task<IActionResult> GetApartment(long id)
        {
            var apartment = await _apartmentService
                .Queryable()
                .Include(a => a.ApartmentGroup)
                .Include(a => a.ApartmentType)
                .Include(a => a.Location)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => !c.IsDeleted && c.Id == id);

            var apartmentImages = _imageService.Queryable().Where(a => a.ApartmentId == id && !a.IsDeleted).Select(b => b.FilePath).ToList();

            var returnView = new ApartmentDetailViewModel()
            {
                ApartmentGroup = apartment.ApartmentGroup,
                ApartmentType = apartment.ApartmentType,
                Location = apartment.Location,
                Capacity = apartment.Capacity,
                Description = apartment.Description,
                Name = apartment.Name,
                Size = apartment.Size,
                Images = new List<string>(apartmentImages),

            };

            return Ok(returnView);
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