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
        [Route("getApartmentGroups")]
        public async Task<IActionResult> getApartmentGroups()
        {

            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            //var userId = User.FindFirst("sub").Value;

            var user = await _userService.GetUser(currentUserId);





            var apartmentGroups = await _apartmentGroupService
                .Queryable()
                .AsNoTracking()
                .Where(c => !c.IsDeleted && c.UserId == user.Id)
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