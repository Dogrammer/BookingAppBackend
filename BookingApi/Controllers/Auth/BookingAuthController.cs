using AutoMapper;
using BookingCore.Enums;
using BookingCore.JwtConstants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Linq;

namespace BookingApi.Controllers.Auth
{
    //[ProducesResponseType(401)]
    //[ProducesResponseType(403)]
    [Authorize]
    [Route("api/[controller]")]
    public class BookingAuthController : ControllerBase
    {
        protected RoleEnum _jwtUserRole { get { return jwtUserRole; } private set { } }
        //protected ApplicationRightEnum _jwtUserRight { get { return jwtUserRight; } private set { } }
        //protected ApplicationTypeEnum _jwtApplicationType { get { return jwtApplicationType; } private set { } }
        protected string _jwtUsername { get { return jwtUsername; } private set { } }
        //protected string _jwtFullname { get { return jwtFullname; } private set { } }
        //protected string _jwtFirstname { get { return jwtFirstname; } private set { } }
        //protected string _jwtLastname { get { return jwtLastname; } private set { } }
        //protected string _jwtOIB { get { return jwtOIB; } private set { } }
        protected long _jwtUserId { get { return jwtUserId; } private set { } }

        //protected readonly IStringLocalizer<LocalizationResources> _localizer;
        protected readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;
        //protected readonly IUnitOfWork _unitOfWork;

        private RoleEnum jwtUserRole;
        private string jwtUsername;
        //private string jwtFullname;
        //private string jwtFirstname;
        //private string jwtLastname;
        //private string jwtOIB;
        private long jwtUserId;

        public BookingAuthController(IDistributedCache distributedCache, IHttpContextAccessor contextAccessor)
        {
            _distributedCache = distributedCache;

            ReadJWTData(contextAccessor);
        }

        public BookingAuthController(IDistributedCache distributedCache, IMapper mapper)
        {
            _mapper = mapper;
            _distributedCache = distributedCache;
        }

        //public BookingAuthController(IDistributedCache distributedCache, IUnitOfWork unitOfWork)
        //{
        //    _distributedCache = distributedCache;
        //    _unitOfWork = unitOfWork;
        //}

        //public BookingAuthController(IDistributedCache distributedCache, IStringLocalizer<LocalizationResources> localizer)
        //{
        //    _distributedCache = distributedCache;
        //    _localizer = localizer;

        //}

        public BookingAuthController(IDistributedCache distributedCache, IHttpContextAccessor contextAccessor, IMapper mapper)
        {
            _distributedCache = distributedCache;
            _mapper = mapper;

            ReadJWTData(contextAccessor);

        }

        //public BookingAuthController(IDistributedCache distributedCache, IHttpContextAccessor contextAccessor, IUnitOfWork unitOfWork)
        //{
        //    _distributedCache = distributedCache;
        //    _unitOfWork = unitOfWork;

        //    ReadJWTData(contextAccessor);

        //}

        //public BookingAuthController(IDistributedCache distributedCache, IHttpContextAccessor contextAccessor, IStringLocalizer<LocalizationResources> localizer)
        //{
        //    _distributedCache = distributedCache;
        //    _localizer = localizer;

        //    ReadJWTData(contextAccessor);

        //}

        //public BookingAuthController(IDistributedCache distributedCache, IHttpContextAccessor contextAccessor, IUnitOfWork unitOfWork, IStringLocalizer<LocalizationResources> localizer)
        //{
        //    _localizer = localizer;
        //    _distributedCache = distributedCache;
        //    _unitOfWork = unitOfWork;

        //    ReadJWTData(contextAccessor);

        //}

        //public BookingAuthController(IDistributedCache distributedCache, IHttpContextAccessor contextAccessor, IMapper mapper, IUnitOfWork unitOfWork)
        //{
        //    _distributedCache = distributedCache;
        //    _mapper = mapper;
        //    _unitOfWork = unitOfWork;

        //    ReadJWTData(contextAccessor);

        //}

        //public BookingAuthController(IDistributedCache distributedCache, IHttpContextAccessor contextAccessor, IMapper mapper, IUnitOfWork unitOfWork, IStringLocalizer<LocalizationResources> localizer)
        //{
        //    _distributedCache = distributedCache;
        //    _mapper = mapper;
        //    _unitOfWork = unitOfWork;
        //    _localizer = localizer;

        //    ReadJWTData(contextAccessor);

        //}

        private void ReadJWTData(IHttpContextAccessor contextAccessor)
        {
            if (contextAccessor.HttpContext.User != null)
            {
                var role = contextAccessor.HttpContext.User.Claims.FirstOrDefault(a => a.Type == JwtClaimNameConstants.ROLE_CLAIM_NAME);
                var username = contextAccessor.HttpContext.User.Claims.FirstOrDefault(a => a.Type == JwtClaimNameConstants.USERNAME_CLAIM_NAME);
                var userId = contextAccessor.HttpContext.User.Claims.FirstOrDefault(a => a.Type == JwtClaimNameConstants.USER_ID_CLAIM_NAME);

               
                
                if (role != null)
                {
                    Enum.TryParse(role.Value, out jwtUserRole);
                }
                if (userId != null)
                {
                    jwtUserId = Convert.ToInt64(userId.Value);
                }
                if (username != null)
                {
                    jwtUsername = username.Value;
                }

                //if (oib != null)
                //{
                //    jwtOIB = oib.Value;
                //}
                //else
                //{
                //    jwtOIB = "";
                //}

                //if (subject_id != null && sessionIndex != null && !string.IsNullOrEmpty(subject_id.Value) && !string.IsNullOrEmpty(sessionIndex.Value))
                //{
                //    var someValue = _distributedCache.GetString($"{subject_id.Value}_{sessionIndex.Value}");
                //    if (!string.IsNullOrEmpty(someValue))
                //    {
                //        throw new LogoutException("Logout from application");
                //    }
                //}


            }


        }

        //protected PagedResult<TDto> MapPagedResult<TEntity, TDto>(PagedResult<TEntity> pagedResult)
        //{
        //    PagedResult<TDto> mappedPagedResult = new PagedResult<TDto>
        //    {
        //        Count = pagedResult.Count,
        //        PageCount = pagedResult.PageCount,
        //    };
        //    mappedPagedResult.Data = _mapper.Map<IEnumerable<TEntity>, IEnumerable<TDto>>(pagedResult.Data);
        //    return mappedPagedResult;
        //}
    }
}