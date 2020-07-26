using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BookingCore.RequestModels;
using BookingCore.Services;
using BookingCore.ViewModels;
using BookingDomain.Domain;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IUserService _userService;
        private readonly IImageService _imageService;

        public ApartmentController(
            IApartmentService apartmentService, 
            IMapper mapper,
            IUserService userService,
            IImageService imageService)
        {
            _apartmentService = apartmentService;
            _mapper = mapper;
            _userService = userService;
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

        [Authorize]
        [HttpGet]
        [Route("getApartmentByApartmentGroupIdForAdmins/{id}")]
        public async Task<IActionResult> GetApartmentByIdForAdmins(long id)
        {
            var returnValues = new List<Apartment>();
            var apartmentsQuery = _apartmentService
                .Queryable()
                .AsNoTracking().Where(a => !a.IsDeleted);

            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _userService.GetUser(currentUserId);

            if (user != null)
            {
                if (user.Role == "ApartmentManager")
                {
                    var userApartments = apartmentsQuery.Where(a => a.ApartmentGroup.User.Id == user.Id).Select(b => b.ApartmentGroupId).ToList();

                    if (!userApartments.Contains(id))
                    {
                        return BadRequest("Authorization Fail");
                    }

                    apartmentsQuery = apartmentsQuery.Where(a => a.ApartmentGroup.UserId == user.Id);
                    returnValues = apartmentsQuery.ToList();
                    return Ok(returnValues);
                }

                returnValues = apartmentsQuery.ToList();
                return Ok(returnValues);
            }

            return BadRequest("Authorization Fail");
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

        [Authorize]
        [HttpGet]
        [Route("getApartmentForAdmin/{id}")]
        public async Task<IActionResult> GetApartmentForAdmin(long id)
        {
            var returnValues = new ApartmentDetailViewModel();

            var apartmentsQuery =  _apartmentService
                .Queryable()
                .Include(a => a.ApartmentGroup)
                .Include(a => a.ApartmentType)
                .Include(a => a.Location)
                .AsNoTracking();

            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _userService.GetUser(currentUserId);
            var apartmentImages = _imageService.Queryable().Where(a => a.ApartmentId == id && !a.IsDeleted).Select(b => b.FilePath).ToList();

            if (user != null)
            {
                if (user.Role == "ApartmentManager")
                {
                    var userApartments = apartmentsQuery.Where(a => a.ApartmentGroup.User.Id == user.Id).Select(b => b.Id).ToList();

                    if (!userApartments.Contains(id))
                    {
                        return BadRequest("Authorization Fail");
                    }


                    var returnObj = apartmentsQuery.FirstOrDefault(a => a.Id == id);
                    returnValues = _mapper.Map<ApartmentDetailViewModel>(returnObj);
                    returnValues.Images = new List<string>(apartmentImages);

                    
                    return Ok(returnValues);
                }

                var returnObjAdmin = apartmentsQuery.FirstOrDefault(a => a.Id == id);
                returnValues = _mapper.Map<ApartmentDetailViewModel>(returnObjAdmin);
                returnValues.Images = new List<string>(apartmentImages);

                return Ok(returnObjAdmin);
            }

            return BadRequest("Authorization Fail");
           
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