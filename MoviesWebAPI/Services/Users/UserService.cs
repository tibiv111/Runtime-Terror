﻿using MoviesWebAPI.Data.Requests.Users;
using MoviesWebAPI.Data.Responses.Users;
using MoviesWebAPI.Repositories.Users;
using MoviesWebAPI.Utils;

namespace MoviesWebAPI.Services.Users
{
    public class UserService : IUserService
    {
        private IUserRepository _repository { get; }

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<AllUsersResponse> GetAllUsers()
        {
            AllUsersResponse response = new AllUsersResponse();

            try
            {
                response.UserList = await _repository.GetAllUsers();

                if (response.UserList != null)
                {
                    response.Code = 200;
                    response.Message = APISuccessCodes.GET_ALL_USERS_SUCCESS_MESSAGE;
                }

                return response;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserResponse> GetUserByID(int? id)
        {
            UserResponse response = new UserResponse();

            try
            {
                response.User = await _repository.GetUserByID(id);

                if (response.User != null)
                {
                    response.Code = 200;
                    response.Message = APISuccessCodes.GET_USER_BY_ID_SUCCES_MESSAGE;
                }
                else
                {
                    response.Code = 404;
                    response.Message = APIErrorCodes.GET_USER_BY_ID_NOT_FOUND_MESSAGE;
                }

                return response;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AllUsersResponse> GetUserByName(string? userName)
        {
            AllUsersResponse response = new AllUsersResponse();

            try
            {
                response.UserList = await _repository.GetUserByName(userName);


                if (response.UserList != null && response.UserList.Count() > 0)
                {
                    response.Code = 200;
                    response.Message = APISuccessCodes.GET_USER_BY_NAME_SUCCES_MESSAGE;
                }
                else
                {
                    response.Code = 404;
                    response.Message = APIErrorCodes.GET_USER_BY_NAME_NOT_FOUND_MESSAGE;
                }

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserResponse> AddNewUser(UserRequest request)
        {
            Data.Models.Users NewUser = new Data.Models.Users
            {
                UserName = request.UserName,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Gender = request.Gender
            };

            UserResponse userResponse = new UserResponse();

            try
            {
                userResponse.User = await _repository.AddNewUser(NewUser);

                if (userResponse.User != null)
                {
                    //ha van Username,Pass es Emailt a valaszban => nem letezett a user a tablaban => sikeres valaszt adok vissza
                    if (!String.IsNullOrEmpty(userResponse.User.UserName) && !String.IsNullOrEmpty(userResponse.User.Password)
                        && !String.IsNullOrEmpty(userResponse.User.Email))
                    {
                        userResponse.Code = 200;
                        userResponse.Message = APISuccessCodes.ADD_NEW_USER_SUCCES_MESSAGE;
                    }

                    //van mar egy ilyen jelszavunk
                    if (String.IsNullOrEmpty(userResponse.User.UserName) && String.IsNullOrEmpty(userResponse.User.Email) && !String.IsNullOrEmpty(userResponse.User.Password))
                    {
                        userResponse.Code = 300;
                        userResponse.Message = APIErrorCodes.ADD_NEW_USER_PASSWORD_EXISTS_MESSAGE;
                    }

                    //van mar egy ilyen emailunk
                    if (String.IsNullOrEmpty(userResponse.User.UserName) && !String.IsNullOrEmpty(userResponse.User.Email) && String.IsNullOrEmpty(userResponse.User.Password))
                    {
                        userResponse.Code = 301;
                        userResponse.Message = APIErrorCodes.ADD_NEW_USER_EMAIL_EXISTS_MESSAGE;
                    }


                }
                else
                {
                    userResponse.Code = 302;
                    userResponse.Message = APIErrorCodes.ADD_NEW_USER_NULL_MESSAGE;
                }

                return userResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserResponse> UpdateUserProfile(int? userID, UserRequest request)
        {
            Data.Models.Users NewUser = new Data.Models.Users
            {
                UserName = request.UserName,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Gender = request.Gender
            };

            UserResponse userResponse = new UserResponse();

            try
            {
                userResponse.User = await _repository.UpdateUserProfile(userID, NewUser);

                if (userResponse.User != null)
                {
                    userResponse.Code = 200;
                    userResponse.Message = APISuccessCodes.UPDATE_USER_SUCCES_MESSAGE;
                }
                else
                {
                    userResponse.Code = 303;
                    userResponse.Message = APIErrorCodes.UPDATE_USER_NULL_MESSAGE;
                }

                return userResponse;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
