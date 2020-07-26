using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BookingApi.Controllers.Auth;
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
    public class ApartmentGroupController : ControllerBase
    {
        private readonly IApartmentGroupService _apartmentGroupService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public ApartmentGroupController(
            IApartmentGroupService apartmentGroupService, 
            IMapper mapper,
            IUserService userService)
        {
            _apartmentGroupService = apartmentGroupService;
            _mapper = mapper;
            _userService = userService;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        [Route("getApartmentGroupsForAdmins")]
        public async Task<IActionResult> GetApartmentGroupsForAdmins()
        {
            var returnValues = new List<ApartmentGroup>();
            var apartmentGroupsQuery =  _apartmentGroupService
                .Queryable()
                .AsNoTracking().Where(a => !a.IsDeleted);
                
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _userService.GetUser(currentUserId);

            if (user != null)
            {
                if (user.Role == "ApartmentManager")
                {
                    apartmentGroupsQuery = apartmentGroupsQuery.Where(a => a.UserId == user.Id);
                    returnValues = apartmentGroupsQuery.ToList();
                    return Ok(returnValues);
                }

                returnValues = apartmentGroupsQuery.ToList();
                return Ok(returnValues);
            }

            return BadRequest("Authorization Fail");
        }

        [HttpGet]
        [Route("getApartmentGroups")]
        public async Task<IActionResult> GetApartmentGroups()
        {
            var apartmentGroups = await _apartmentGroupService
                .Queryable()
                .AsNoTracking().Where(a => !a.IsDeleted)
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