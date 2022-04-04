using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesWebAPI.Data.Models;
using MoviesWebAPI.Data.Requests.Users;
using MoviesWebAPI.Data.Responses.Users;
using MoviesWebAPI.Services.Users;
using MoviesWebAPI.Utils;

namespace MoviesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public IUserService _service { get; set; }

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                AllUsersResponse response = await _service.GetAllUsers();

                return Ok(response);
            }
            catch (Exception ex)
            {
                AllUsersResponse error = new AllUsersResponse
                {
                    Code = 400,
                    Message = APIErrorCodes.GET_ALL_USERS_REQUEST_EXCEPTION_MESSAGE + ex.Message
                };

                return BadRequest(error);
            }
        }

        [HttpGet]
        [Route("get-user-by-id")]
        public async Task<IActionResult> GetUserByID([FromHeader] int? userID)
        {
            if (userID == null)
            {
                UserResponse response = new UserResponse
                {
                    Code = 300,
                    Message = APIErrorCodes.HEADER_MISSING_MESSAGE + "userID"
                };

                return BadRequest(response);
            }

            try
            {
                UserResponse response = await _service.GetUserByID(userID);

                if (response.User != null)
                {
                    return Ok(response);
                }
                else
                {
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                UserResponse error = new UserResponse
                {
                    Code = 400,
                    Message = APIErrorCodes.GET_USER_BY_ID_RESPONSE_EXCEPTION_MESSAGE + ex.Message
                };

                return BadRequest(error);
            }
        }

        [HttpGet, Route("get-user-by-name")]
        public async Task<IActionResult> GetUserByName([FromHeader] string? userName)
        {
            if (userName == null)
            {
                UserResponse noHeaderResponse = new UserResponse
                {
                    Code = 301,
                    Message = APIErrorCodes.HEADER_MISSING_MESSAGE + "userName"
                };

                return BadRequest(noHeaderResponse);
            }

            try
            {

                AllUsersResponse allUsersResponse = await _service.GetUserByName(userName);

                return Ok(allUsersResponse);

            }
            catch (Exception ex)
            {
                UserResponse error = new UserResponse { Code = 400, Message = APIErrorCodes.GET_USER_BY_NAME_RESPONSE_EXCEPTION_MESSAGE + ex.Message };

                return BadRequest(error);
            }
        }

        [HttpPost, Route("add-new-user")]
        public async Task<IActionResult> AddNewUser([FromBody] UserRequest userRequest)
        {

            //username mezo ures,visszateritem a hibat
            if (String.IsNullOrEmpty(userRequest.UserName))
            {
                UserResponse userResponse = new UserResponse
                {
                    Code = 300,
                    Message = APIErrorCodes.BODY_MISSING_MESSAGE + "Username is missing from body!"
                };

                return BadRequest(userResponse);
            }

            //jelszo mezo ures,visszateritem a hibat
            if (String.IsNullOrEmpty(userRequest.Password))
            {
                UserResponse userResponse = new UserResponse
                {
                    Code = 301,
                    Message = APIErrorCodes.BODY_MISSING_MESSAGE + "Password field is missing from body!"
                };

                return BadRequest(userResponse);
            }

            //email mezo ures => visszateritem a hibat
            if (String.IsNullOrEmpty(userRequest.Email))
            {
                UserResponse userResponse = new UserResponse
                {
                    Code = 302,
                    Message = APIErrorCodes.BODY_MISSING_MESSAGE + "Email field is missing from body!"
                };

                return BadRequest(userResponse);
            }


            if (String.IsNullOrEmpty(userRequest.FirstName))
            {
                userRequest.FirstName = null;
            }

            if (String.IsNullOrEmpty(userRequest.LastName))
            {
                userRequest.LastName = null;
            }

            if (String.IsNullOrEmpty(userRequest.Gender))
            {
                userRequest.Gender = null;
            }

            try
            {
                UserResponse userResponse = await _service.AddNewUser(userRequest);

                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                UserResponse error = new UserResponse { Code = 400, Message = APIErrorCodes.ADD_NEW_USER_EXCEPTION_MESSAGE + ex.Message };

                return BadRequest(error);
            }
        }

        /// <summary>
        /// Updates user profile data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userRequest"></param>
        /// <response code="200">Updates user profile data in database and returns it</response>
        /// <response code="300">UserID wasn't given in the request's header</response>
        /// <response code="301">Username wasn't given in the request's body</response>
        /// <response code="302">Email wasn't given in the request's body</response>
        /// <response code="303">Update method gave us an null, meaning the backend couldn't update user's profile</response>
        /// <response code="400">Some error happened at backend level and returned an exception</response>
        /// <returns></returns>
        [HttpPut, Route("update-user-profile")]
        public async Task<IActionResult> UpdateUserProfile([FromHeader] int? userId, [FromBody] UserRequest userRequest)
        {
            if (userId == null)
            {
                UserResponse response = new UserResponse
                {
                    Code = 300,
                    Message = APIErrorCodes.HEADER_MISSING_MESSAGE + "userID"
                };

                return BadRequest(response);
            }

            if (String.IsNullOrEmpty(userRequest.UserName))
            {
                UserResponse response = new UserResponse
                {
                    Code = 301,
                    Message = APIErrorCodes.BODY_MISSING_MESSAGE + "Username is missing from body"
                };

                return BadRequest(response);
            }

            if (String.IsNullOrEmpty(userRequest.Email))
            {
                UserResponse response = new UserResponse
                {
                    Code = 302,
                    Message = APIErrorCodes.BODY_MISSING_MESSAGE + "Email is missing from body"
                };

                return BadRequest(response);
            }

            if (String.IsNullOrEmpty(userRequest.FirstName))
            {
                userRequest.FirstName = null;
            }

            if (String.IsNullOrEmpty(userRequest.LastName))
            {
                userRequest.LastName = null;
            }

            if (String.IsNullOrEmpty(userRequest.Gender))
            {
                userRequest.Gender = null;
            }

            try
            {
                UserResponse userResponse = await _service.UpdateUserProfile(userId, userRequest);

                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                UserResponse error = new UserResponse { Code = 400, Message = APIErrorCodes.ADD_NEW_USER_EXCEPTION_MESSAGE + ex.Message };

                return BadRequest(error);
            }
        }
    }
}
