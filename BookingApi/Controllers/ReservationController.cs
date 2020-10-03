using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BookingCore.Enums;
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
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public ReservationController(IReservationService reservationService,
            IMapper mapper,
            IUserService userService
            )
        {
            _reservationService = reservationService;
            _mapper = mapper;
            _userService = userService;
        }
        [Authorize]
        [HttpGet]
        [Route("getReservationsForAdmin")]
        public async Task<IActionResult> GetReservations()
        {
            var reservations = _reservationService
               .Queryable()
               .Include(x => x.Apartment)
               .Include(x => x.ReservationStatus)
               .Include(x => x.User)
               .AsNoTracking()
               .Where(c => !c.IsDeleted);
               //.ToListAsync();


            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (currentUserId > 0)
            {
                var user = await _userService.GetUser(currentUserId);
                
                if (user.Role == "ApartmentManager")
                {
                    reservations = reservations.Where(x => x.Apartment.ApartmentGroup.UserId == user.Id);
                }
            }
            else
            {
                return Unauthorized();
            }

            var retVal = reservations.ToList();

            return Ok(retVal);
        }


        [Authorize]
        [HttpPost]
        [Route("reservations")]
        public async Task<ActionResult> AddReservation(CreateReservationRequest request)
        {

            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (currentUserId > 0)
            {
                var domain = _mapper.Map<Reservation>(request);
                domain.UserId = currentUserId;
                domain.ReservationStatusId = 3;
                _reservationService.Insert(domain);

                await _reservationService.Save();

                return Ok(domain);
            }

            return Unauthorized();

        }

        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("rejectReservation/{reservationId}")]
        public async Task<ActionResult> RejectReservation(long reservationId)
        {
            var existing = _reservationService
                .Queryable()
                .FirstOrDefault(x => !x.IsDeleted && x.Id == reservationId);

            if (existing != null)
            {
                // status: canceled
                existing.ReservationStatusId = 5;
            }

            await _reservationService.Save();

            return Ok(existing);

        }

        [Authorize]
        [HttpGet]
        [Route("acceptReservation/{reservationId}")]
        public async Task<ActionResult> AcceptReservation(long reservationId)
        {
            var existing = _reservationService
                .Queryable()
                .FirstOrDefault(x => !x.IsDeleted && x.Id == reservationId);

            if (existing != null)
            {
                // status: reserved
                existing.ReservationStatusId = 4;
            }

            await _reservationService.Save();

            return Ok(existing);

        }

        [Authorize]
        [HttpDelete]
        [Route("deleteReservation/{id}")]
        public async Task<ActionResult> DeleteApartmentGroups(long id)
        {
            var reservationToDelete = _reservationService.Queryable().FirstOrDefault(x => x.Id == id);

            if (reservationToDelete != null)
            {
                _reservationService.Delete(reservationToDelete);

                await _reservationService.Save();

                return NoContent();
            }

            return BadRequest("Does not exist");
        }


    }
}