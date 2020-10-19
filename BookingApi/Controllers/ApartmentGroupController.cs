using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BookingApi.Controllers.Auth;
using BookingApi.Extensions;
using BookingApi.Helpers;
using BookingApi.Helpers.ApartmentGroup;
using BookingCore.RequestModels;
using BookingCore.RequestModels.FilterRequests;
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
        public async Task<IActionResult> GetApartmentGroupsForAdmins([FromQuery]ApartmentGroupParams apartmentGroupParams)
        {

            var returnValues = new List<ApartmentGroup>();
            var apartmentGroupsQuery =  _apartmentGroupService
                .Queryable()
                .Include(a => a.User)
                .AsNoTracking().Where(a => !a.IsDeleted);

            if (apartmentGroupParams.UserId > 0)
            {
                apartmentGroupsQuery = apartmentGroupsQuery.Where(x => x.UserId == apartmentGroupParams.UserId);
            }

            //var apartmentGroups = await PagedList<ApartmentGroup>.CreateAsync(apartmentGroupsQuery, apartmentGroupParams.PageNumber, apartmentGroupParams.PageSize);
            //Response.AddPaginationHeader(apartmentGroups.CurrentPage, apartmentGroups.PageSize, apartmentGroups.TotalCount, apartmentGroups.TotalPages);
                
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _userService.GetUser(currentUserId);

            if (user != null)
            {
                if (user.Role == "ApartmentManager")
                {
                    apartmentGroupsQuery = apartmentGroupsQuery.Where(a => a.UserId == user.Id);
                    
                    var apartmentGroups = await PagedList<ApartmentGroup>.CreateAsync(apartmentGroupsQuery, apartmentGroupParams.PageNumber, apartmentGroupParams.PageSize);
                    Response.AddPaginationHeader(apartmentGroups.CurrentPage, apartmentGroups.PageSize, apartmentGroups.TotalCount, apartmentGroups.TotalPages);

                    return Ok(apartmentGroups);
                }

                //returnValues = apartmentGroupsQuery.ToList();
                //returnValues = apartmentGroupsQuery;

                var apartmentGroupsAdmin = await PagedList<ApartmentGroup>.CreateAsync(apartmentGroupsQuery, apartmentGroupParams.PageNumber, apartmentGroupParams.PageSize);
                Response.AddPaginationHeader(apartmentGroupsAdmin.CurrentPage, apartmentGroupsAdmin.PageSize, apartmentGroupsAdmin.TotalCount, apartmentGroupsAdmin.TotalPages);

                return Ok(apartmentGroupsAdmin);
            }

            return BadRequest("Authorization Fail");
        }

        [HttpGet]
        [Route("getApartmentGroupsPagination")]
        public async Task<IActionResult> GetApartmentGroups([FromQuery]ApartmentGroupParams apartmentGroupParams)
        {
            var apartmentGroupsQuery = _apartmentGroupService
                .Queryable()
                .AsNoTracking().Where(a => !a.IsDeleted);

            //var apartmentsQuery = _apartment

            //if (apartmentGroupParams.CityId > 0)
            //{
            //    apartmentGroupsQuery = apartmentGroupsQuery.Where(x => x.)w
            //}

            // uzmi datume...dohvati dostupnost..

            var apartmentGroups = await PagedList<ApartmentGroup>.CreateAsync(apartmentGroupsQuery, apartmentGroupParams.PageNumber, apartmentGroupParams.PageSize);
            Response.AddPaginationHeader(apartmentGroups.CurrentPage, apartmentGroups.PageSize, apartmentGroups.TotalCount, apartmentGroups.TotalPages);

            return Ok(apartmentGroups);
        }


        [Authorize]
        [HttpPost]
        [Route("apartmentGroups")]
        public async Task<ActionResult> AddApartmentGroup(CreateApartmentGroupRequest request)
        {
            var domain = _mapper.Map<ApartmentGroup>(request);

            if (request.UserId == 0)
            {
                var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                domain.UserId = currentUserId;
            }
            else
            {
                domain.UserId = request.UserId;
            }

            _apartmentGroupService.Insert(domain);

            await _apartmentGroupService.Save();

            return Ok(domain);
        }

        [Authorize]
        [HttpPut]
        [Route("editApartmentGroup/{id}")]
        public async Task<ActionResult> EditApartmentGroup(long id, [FromForm]ImportApartmentGroupImage request)
        {
            var existingApartmentGroup = _apartmentGroupService.Queryable().FirstOrDefault(x => x.Id == id);
            
            if (existingApartmentGroup != null)
            {
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (request.File != null && request.File.Length > 0)
                {

                    // izbrisi staru sliku 
                    var fileToDelete = Directory.GetFiles(folderName);

                    foreach (var file in fileToDelete)
                    {
                        if (file == existingApartmentGroup.ImageFilePath)
                        {
                            System.IO.File.Delete(file);
                        }
                    }

                    var fileName = ContentDispositionHeaderValue.Parse(request.File.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        request.File.CopyTo(stream);
                    }

                    existingApartmentGroup.ImageFilePath = dbPath;
                    existingApartmentGroup.Name = request.Name;
                    existingApartmentGroup.Description = request.Description;
                    // fill the object for db


                    if (request.UserId == 0)
                    {
                        var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                        existingApartmentGroup.UserId = currentUserId;
                    }
                    else
                    {
                        existingApartmentGroup.UserId = request.UserId;
                    }

                    //return Ok();
                }

                await _apartmentGroupService.Save();

                return Ok(existingApartmentGroup);
            }

            return BadRequest("Apartment Group Invalid");
            
        }


        [Authorize]
        [HttpDelete]
        [Route("deleteApartmentGroups/{id}")]
        public async Task<ActionResult> DeleteApartmentGroups(long id)
        {
            var apartmentGroupToDelete = _apartmentGroupService.Queryable().FirstOrDefault(x => x.Id == id);

            if (apartmentGroupToDelete != null)
            {
                _apartmentGroupService.Delete(apartmentGroupToDelete);

                await _apartmentGroupService.Save();

                return NoContent();
            }

            return BadRequest("Does not exist");
        }


        [AllowAnonymous]
        [Route("addApartmentGroup")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload([FromForm]ImportApartmentGroupImage request)
        {
            //tu spremi file u file storage 
            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (request.File.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(request.File.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    request.File.CopyTo(stream);
                }

                // fill the object for db
                var newApartmentGroup = new ApartmentGroup();
                newApartmentGroup.DateCreated = DateTimeOffset.UtcNow;
                newApartmentGroup.Description = request.Description;
                newApartmentGroup.ImageFilePath = dbPath;
                newApartmentGroup.Name = request.Name;

                if (request.UserId == 0)
                {
                    var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    newApartmentGroup.UserId = currentUserId;
                }
                else
                {
                    newApartmentGroup.UserId = request.UserId;
                }

                _apartmentGroupService.Attach(newApartmentGroup);
                await _apartmentGroupService.Save();

                return Ok();
            }

            else
            {
                return BadRequest("File size is 0");
            }
        }
    }
}