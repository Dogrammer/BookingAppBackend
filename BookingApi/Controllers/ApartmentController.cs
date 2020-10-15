using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BookingCore.Repository;
using BookingCore.RequestModels;
using BookingCore.Services;
using BookingCore.ViewModels;
using BookingDomain.Domain;
using BookingInfrastructure;
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
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IApartmentService _apartmentService;
        private readonly IMapper _mapper;
        private readonly IApartmentGroupService _apartmentGroupService;
        private readonly IUserService _userService;
        //private readonly ITrackableRepository<Address> _addressRepository;
        private readonly IImageService _imageService;

        public ApartmentController(
            ApplicationDbContext applicationDbContext,
            IApartmentService apartmentService, 
            IMapper mapper,
            IApartmentGroupService apartmentGroupService,
            IUserService userService,
            //BookingCore.Repository.ITrackableRepository<Address> addressRepository,
            IImageService imageService)
        {
            _applicationDbContext = applicationDbContext;
            _apartmentService = apartmentService;
            _mapper = mapper;
            _apartmentGroupService = apartmentGroupService;
            _userService = userService;
            //_addressRepository = addressRepository;
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
                .Queryable().Include(a => a.ApartmentGroup)
                .AsNoTracking().Where(a => !a.IsDeleted && a.ApartmentGroupId == id);

            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _userService.GetUser(currentUserId);
            var apartmentGroups = _apartmentGroupService.Queryable().Where(a => a.UserId == user.Id && id == a.Id).ToList();

            if (apartmentGroups.Count <= 0 && user.Role == "ApartmentManager")
            {
                return BadRequest("You are not admin of that apartment group");
            }

            if (user != null)
            {
                if (user.Role == "ApartmentManager")
                {
                    //var userApartments = apartmentsQuery.Where(a => a.ApartmentGroup.User.Id == user.Id).Select(b => b.ApartmentGroupId).ToList();
                    //var ifUserIsAdminOnRequestedApartmentGroup = apartmentsQuery.FirstOrDefault(a => a.ApartmentGroup.UserId == user.Id);
                    //if (ifUserIsAdminOnRequestedApartmentGroup != null && !userApartments.Contains(id))
                    //{
                        //returnValues = apartmentsQuery.Where(a => a.ApartmentGroup.UserId == user.Id).ToList();
                        //return Ok(returnValues);
                    //}
                    //if (!userApartments.Contains(id))
                    //{
                    //    return BadRequest("Authorization Fail");
                    //}

                    apartmentsQuery = apartmentsQuery.Where(a => a.ApartmentGroup.UserId == user.Id);
                    returnValues = apartmentsQuery.ToList();
                    return Ok(returnValues);
                }

                returnValues = apartmentsQuery.Where(a => a.ApartmentGroupId == id).ToList();

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
                .Include(a => a.City).ThenInclude(a => a.Country)
                //.Include(a => a.Location)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => !c.IsDeleted && c.Id == id);

            var apartmentImages = _imageService.Queryable().Where(a => a.ApartmentId == id && !a.IsDeleted).Select(b => b.FilePath).ToList();

            var returnView = new ApartmentDetailViewModel()
            {
                ApartmentGroup = apartment.ApartmentGroup,
                ApartmentType = apartment.ApartmentType,
                //Location = apartment.Location,
                Capacity = apartment.Capacity,
                Description = apartment.Description,
                Name = apartment.Name,
                Size = apartment.Size,
                Images = new List<string>(apartmentImages),
                BbqTools = apartment.BbqTools,
                City = apartment.City,
                ClimateControl = apartment.ClimateControl,
                ClosestBeachDistance = apartment.ClosestBeachDistance,
                ClosestMarketDistance = apartment.ClosestMarketDistance,
                FullAddress = apartment.FullAddress,
                KitchenTool = apartment.KitchenTool,
                NumberOfBedrooms = apartment.NumberOfBedrooms,
                SportTool = apartment.SportTool,
                Wifi = apartment.Wifi,
                WorkSpace = apartment.WorkSpace
                

            };

            return Ok(returnView);
        }

        [Authorize]
        [HttpGet]
        [Route("getApartmentForAdmin/{id}")]
        public async Task<IActionResult> GetApartmentForAdmin(long id)
        {
            var returnValues = new ApartmentDetailViewModel();

            var apartmentsQuery = _apartmentService
                .Queryable()
                .Include(a => a.ApartmentGroup)
                .Include(a => a.ApartmentType)
                .Include(a => a.City).ThenInclude(b => b.Country).Where(x => !x.IsDeleted);
                
                //.Include(a => a.Location)

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
        //public async Task<ActionResult> AddApartment(object request)

        {

            var newApartment = new Apartment();
            newApartment.ApartmentGroupId = request.ApartmentGroupId;
            newApartment.ApartmentTypeId = request.ApartmentTypeId;
            newApartment.BbqTools = request.BbqTools;
            newApartment.Capacity = request.Capacity;
            newApartment.ClimateControl = request.ClimateControl;
            newApartment.ClosestBeachDistance = request.ClosestBeachDistance;
            newApartment.ClosestMarketDistance = request.ClosestMarketDistance;
            newApartment.Description = request.Description;
            newApartment.KitchenTool = request.KitchenTool;
            newApartment.Name = request.Name;
            newApartment.NumberOfBedrooms = request.NumberOfBedrooms;
            newApartment.Size = request.Size;
            newApartment.Wifi = request.Wifi;
            newApartment.WorkSpace = request.WorkSpace;
            newApartment.FullAddress = request.Address;
            newApartment.CityId = request.CityId;
            //newApartment.Address = new Address();
            //newApartment.Address.StreetNameAndNumber = request.Address;
            //newApartment.Address.CityId = request.CityId;
            //newApartment.Address.CountryId = request.CountryId;
            //newApartment.AddressId = 



            _apartmentService.Insert(newApartment);
            await _apartmentService.Save();

            //var domain = _mapper.Map<Apartment>(request);
            //_applicationDbContext.Apartments.Add(newApartment);
            //_addressRepository.Insert(newApartment.Address);
            //var apps = _applicationDbContext.SaveChangesAsync();
            //await _applicationDbContext.SaveChangesAsync();
            //await _apartmentService.Save();
            //await _addressRepository.SaveChangesAsync();

            return Ok();

        }

        //[HttpGet]
        //[Route("getImagesForApartment/{id}")]
        //public async Task<IActionResult> GetImagesForApartment(long id)
        //{
        //    var images = await _
        //        .Queryable()
        //        .AsNoTracking()
        //        .Where(c => !c.IsDeleted && c.ApartmentId == id)
        //        .ToListAsync();

        //    var retVal = _mapper.Map<List<PricingPeriodDetailViewModel>>(pricingPeriodDetails);

        //    return Ok(retVal);
        //}

        

        [Authorize]
        [HttpGet]
        [Route("getImagesForApartment/{id}")]
        public async Task<IActionResult> GetImagesForApartment(long id)
        {
            var returnValues = new ApartmentDetailViewModel();


            var apartmentsQuery = _apartmentService
                .Queryable()
                .Include(a => a.ApartmentGroup.User).AsNoTracking();

            //dohvati sve slike
            var imagesQuery = _imageService
                .Queryable()
                .AsNoTracking();

            //dohvati koji je user slao request
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _userService.GetUser(currentUserId);

            //dohvati samo filepathove od slika 
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


                    var returnObj = imagesQuery.Where(a => a.ApartmentId == id).ToList();
                    //returnValues = _mapper.Map<ApartmentDetailViewModel>(returnObj);
                    //returnValues.Images = new List<string>(apartmentImages);


                    return Ok(returnObj);
                }

                var returnObjAdmin = imagesQuery.Where(a => a.ApartmentId == id).ToList();
                //returnValues = _mapper.Map<ApartmentDetailViewModel>(returnObjAdmin);
                //returnValues.Images = new List<string>(apartmentImages);

                return Ok(returnObjAdmin);
            }

            return BadRequest("Authorization Fail");

        }

        //deleteImage

        [Authorize]
        [HttpDelete]
        [Route("deleteImage/{id}")]
        public async Task<ActionResult> DeleteImage(long id)
        {
            var imageToDelete = _imageService.Queryable().FirstOrDefault(x => x.Id == id);

            if (imageToDelete != null)
            {
                _imageService.Delete(imageToDelete);

                await _imageService.Save();

                return NoContent();
            }

            return BadRequest("Does not exist");
        }

        [HttpPut]
        [Route("editApartment/{id}")]
        public async Task<ActionResult> EditApartment(long id, [FromBody]CreateApartmentRequest request)
        {

            var existing = _apartmentService.Queryable().FirstOrDefault(x => x.Id == id);

            if (existing != null)
            {
                existing.ApartmentGroupId = request.ApartmentGroupId;
                existing.ApartmentTypeId = request.ApartmentTypeId;
                existing.BbqTools = request.BbqTools;
                existing.Capacity = request.Capacity;
                existing.ClimateControl = request.ClimateControl;
                existing.ClosestBeachDistance = request.ClosestBeachDistance;
                existing.ClosestMarketDistance = request.ClosestMarketDistance;
                existing.Description = request.Description;
                existing.KitchenTool = request.KitchenTool;
                existing.Name = request.Name;
                existing.NumberOfBedrooms = request.NumberOfBedrooms;
                existing.Size = request.Size;
                existing.Wifi = request.Wifi;
                existing.WorkSpace = request.WorkSpace;
                existing.FullAddress = request.Address;
                existing.CityId = request.CityId;
                existing.SportTool = request.SportTool;
            }

            await _apartmentService.Save();

            return Ok();

        }

        [Authorize]
        [HttpDelete]
        [Route("deleteApartment/{id}")]
        public async Task<ActionResult> DeleteApartment(long id)
        {
            var apartmentToDelete = _apartmentService.Queryable().FirstOrDefault(x => x.Id == id);

            if (apartmentToDelete != null)
            {
                apartmentToDelete.IsDeleted = true;
                apartmentToDelete.DateDeleted = DateTimeOffset.UtcNow;
                //_apartmentService.Delete(apartmentToDelete);

                await _apartmentService.Save();

                return NoContent();
            }

            return BadRequest("Does not exist");
        }
    }
}