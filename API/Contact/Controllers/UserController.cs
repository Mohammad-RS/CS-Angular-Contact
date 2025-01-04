using Contact.Business;
using Contact.Model;
using Contact.Model.User;
using Contact.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contact.Controller
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private UserBusiness userBusiness;

        public UserController() 
        {   
            this.userBusiness = new UserBusiness();
        }    

        // ...
        [HttpPost("register")]
        public BusinessResult<int> Register(UserAddModel addModel)
        {
            return userBusiness.RegisterBusiness(addModel);
        }

        // ...
        [HttpPost("login")]
        public BusinessResult<string> Login(UserLoginModel loginModel)
        {
            BusinessResult<int> result = userBusiness.LoginBusiness(loginModel);

            if (result.Success)
            {
                int Id = result.Data;
                string token = Token.Generate(Id);

                return new BusinessResult<string>()
                {
                    Success = true,
                    Data = token
                };
            }
            else
            {
                return new BusinessResult<string>()
                {
                    Success = false,
                    ErrorCode = result.ErrorCode,
                    ErrorMessage = result.ErrorMessage
                };
            }
        }

        // ...
        [HttpGet("{username}/profile")]
        public BusinessResult<VisitUserProfileModel> VisitProfile(string username)
        {
            return userBusiness.VisitProfileBusiness($"{username}");
        }

        // ...
        [Authorize]
        [HttpGet("profile")]
        public BusinessResult<UserProfileModel> Profile()
        {
            int Id = int.Parse(base.User.Identity.Name);

            return userBusiness.ProfileBusiness(Id);
        }

        // ...
        [Authorize]
        [HttpPut("change-password")]
        public BusinessResult<bool> ChangePassword(UserChangePasswordModel changePWModel)
        {
            int Id = int.Parse(base.User.Identity.Name);

            changePWModel.Id = Id;

            return userBusiness.ChangePasswordBusiness(changePWModel);
        }

        // ...
        [Authorize]
        [HttpPut("profile/edit")]
        public BusinessResult<bool> EditProfile(UserUpdateModel updateModel)
        {
            int Id = int.Parse(base.User.Identity.Name);

            updateModel.Id = Id;

            return userBusiness.EditProfileBusiness(updateModel);
        }

        // ...
        [Authorize]
        [HttpDelete("profile/delete")]
        public BusinessResult<bool> DeleteProfile()
        {
            int Id = int.Parse(base.User.Identity.Name);

            return userBusiness.DeleteProfileBusiness(Id);
        }

        // ...
    }
}
