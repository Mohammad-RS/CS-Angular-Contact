using Contact.Data;
using Contact.Model;
using Contact.Model.User;
using Contact.Utility;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Contact.Business
{
    // Add email verificaiton, forget pw, delete(or deactivate) profile ...
    // Use relative path for file handling instead
    public class UserBusiness
    {
        private UserData userData;

        public UserBusiness() 
        { 
            this.userData = new UserData();
        }

        // ...
        public BusinessResult<int> RegisterBusiness(UserAddModel addModel)
        {
            BusinessResult<int> result = new BusinessResult<int>();

            Validation validator = new Validation();

            string username = addModel.Username;
            string email = addModel.Email;
            string pw = addModel.Password1;


            string passwordError = validator.PasswordValidation(pw);

            if (pw != addModel.Password2)
            {
                result.SetError(400, "Passwords do not match.");

                return result;
            }

            if (passwordError != "")
            {
                result.SetError(400, passwordError);
            }

            byte[] hashedPW = MD5.HashData(Encoding.UTF8.GetBytes(pw));


            string usernameError = validator.UsernameValidation(username);

            if (usernameError != "")
            {
                result.SetError(400, usernameError);

                return result;
            }


            string emailError = validator.EmailValidation(email);

            if (emailError != "")
            {
                result.SetError(400, emailError);

                return result;
            }


            if (!userData.CheckUsernameAndEmailUniqueness(username, email))
            {
                result.SetError(400, "Username or email already taken.");

                return result;
            }


            if (string.IsNullOrEmpty(addModel.Fullname))
            {
                addModel.Fullname = username;
            }


            string Avatar;

            if (string.IsNullOrEmpty(addModel.AvatarData))
            {
                Avatar = "default_avatar.png";
            }
            else
            {
                string file = @$"{Path.Combine(Directory.GetCurrentDirectory(), @"Static\Avatar\")}{addModel.Username.ToLower()}.png";

                addModel.AvatarData = addModel.AvatarData.Replace("data:image/png;base64,", "");

                byte[] avatarData = Convert.FromBase64String(addModel.AvatarData);

                File.WriteAllBytes(file, avatarData);

                Avatar = $"{addModel.Username.ToLower()}.png";
            }


            UserTable user = new UserTable()
            {
                Username = addModel.Username,
                Email = addModel.Email,
                Password = hashedPW,
                Fullname = addModel.Fullname,
                Avatar = Avatar,
                Date_Created = DateTime.Now,
                Date_Modified = DateTime.Now,
            };


            int Id = userData.Insert(user);

            if (Id == -1)
            {
                result.SetError(500, "Something went wrong.");
            }
            else
            {
                result.SetData(Id);
            }

            return result;
        }

        // ...
        public BusinessResult<int> LoginBusiness(UserLoginModel loginModel)
        {
            BusinessResult<int> result = new BusinessResult<int>();

            byte[] hashedPW = MD5.HashData(Encoding.UTF8.GetBytes(loginModel.Password));

            int Id = userData.GetUserId(loginModel.Username, hashedPW);


            if (Id == -1)
            {
                result.SetError(404, "Incorrect username or password.");
            }
            else
            {
                result.SetData(Id);
            }

            return result;
        }

        // ...
        public BusinessResult<UserProfileModel> ProfileBusiness(int Id)
        {
            BusinessResult<UserProfileModel> result = new BusinessResult<UserProfileModel>();

            UserProfileModel profile = userData.GetUserProfileById(Id);


            if (profile.Username == "")
            {
                result.SetError(404, "Profile not found.");

                return result;
            }


            string file = @$"{Path.Combine(Directory.GetCurrentDirectory(), @"Static\Avatar\")}{profile.Avatar}";

            string avatarData = "data:image/png;base64," + Convert.ToBase64String(File.ReadAllBytes(file));

            profile.Avatar = avatarData;


            result.SetData(profile);

            return result;
        }

        // ...
        public BusinessResult<VisitUserProfileModel> VisitProfileBusiness(string username)
        {
            BusinessResult<VisitUserProfileModel> result = new BusinessResult<VisitUserProfileModel>();

            VisitUserProfileModel profile = userData.GetUserProfileByUsername(username);


            if (profile.Username == "")
            {
                result.SetError(404, "Profile not found.");

                return result;
            }


            string file = @$"{Path.Combine(Directory.GetCurrentDirectory(), @"Static\Avatar\")}{profile.Avatar}";

            string avatarData = "data:image/png;base64," + Convert.ToBase64String(File.ReadAllBytes(file));

            profile.Avatar = avatarData;


            result.SetData(profile);

            return result;
        }

        // ...
        public BusinessResult<bool> ChangePasswordBusiness(UserChangePasswordModel changePWModel)
        {
            BusinessResult<bool> result = new BusinessResult<bool>();

            Validation validator = new Validation();

            UserTable user = userData.GetUser(changePWModel.Id);


            if (user.Id == 0)
            {
                result.SetError(404, "User  not found.");
                return result;
            }


            string pw1 = changePWModel.Password1;
            string pw2 = changePWModel.Password2;

            byte[] hashedcurrentPW = MD5.HashData(Encoding.UTF8.GetBytes(changePWModel.CurrentPassword));
            byte[] hashedPW1 = MD5.HashData(Encoding.UTF8.GetBytes(pw1));


            string passwordError = validator.PasswordValidation(pw1);

            if (pw1 != pw2)
            {
                result.SetError(400, "Passwords do not match.");

                return result;
            }

            if (passwordError != "")
            {
                result.SetError(400, passwordError);

                return result;
            }

            if (!user.Password.SequenceEqual(hashedcurrentPW))
            {
                result.SetError(400, "Current password is incorrect.");

                return result;
            }

            if (hashedPW1.SequenceEqual(hashedcurrentPW))
            {
                result.SetError(400, "New password cannot be the same as the current password.");

                return result;
            }

            user.Password = hashedPW1;


            if (!userData.Update(user))
            {
                result.SetError(500, "Something went wrong.");
            }
            else
            {
                result.SetData(true);
            }

            return result;
        }

        // ...
        public BusinessResult<bool> EditProfileBusiness(UserUpdateModel updateModel)
        {
            BusinessResult<bool> result = new BusinessResult<bool>();

            Validation validator = new Validation();

            UserTable user = userData.GetUser(updateModel.Id);

            string username = updateModel.Username;
            string email = updateModel.Email;


            if (user.Id == 0)
            {
                result.SetError(404, "User  not found.");
                return result;
            }


            if (!userData.CheckUsernameAndEmailUniqueness(username, updateModel.Email))
            {
                result.SetError(400, "Username or email already taken.");

                return result;
            }


            if (!string.IsNullOrEmpty(username))
            {
                string usernameError = validator.UsernameValidation(username);

                if (usernameError != "")
                {
                    result.SetError(400, usernameError);

                    return result;
                }
            }
            else
            {
                username = user.Username;
            }


            if (!string.IsNullOrEmpty(email))
            {
                string emailError = validator.EmailValidation(email);

                if (emailError != "")
                {
                    result.SetError(400, emailError);

                    return result;
                }

                if (email.Length > 100)
                {
                    result.SetError(400, "Email must be less than 100 characters.");

                    return result;
                }
            }
            else
            {
                updateModel.Email = user.Email;
            }


            if (string.IsNullOrEmpty(updateModel.Fullname))
            {
                updateModel.Fullname = user.Fullname;
            }
            else if (updateModel.Fullname.Length > 50)
            {
                result.SetError(400, "Fullname must be less than 50 characters.");

                return result;
            }


            string Avatar;
            string file;

            if (!string.IsNullOrEmpty(updateModel.AvatarData))
            {
                if (!string.IsNullOrEmpty(updateModel.Username))
                {
                    file = @$"{Path.Combine(Directory.GetCurrentDirectory(), @"Static\Avatar\")}{updateModel.Username.ToLower()}.png";
                    Avatar = $"{updateModel.Username.ToLower()}.png";
                }
                else
                {
                    file = @$"{Path.Combine(Directory.GetCurrentDirectory(), @"Static\Avatar\")}{user.Username.ToLower()}.png";
                    Avatar = $"{user.Username.ToLower()}.png";
                }


                updateModel.AvatarData = updateModel.AvatarData.Replace("data:image/png;base64,", "");

                byte[] avatarData = Convert.FromBase64String(updateModel.AvatarData);

                File.WriteAllBytes(file, avatarData);
            }
            else
            {
                Avatar = user.Avatar;
            }


            UserTable updatedUser = new UserTable()
            {
                Id = user.Id,
                Username = username,
                Email = updateModel.Email,
                Password = user.Password,
                Fullname = updateModel.Fullname,
                Avatar = Avatar,
                Date_Created = user.Date_Created,
                Date_Modified = DateTime.Now
            };


            if (!userData.Update(updatedUser))
            {
                result.SetError(500, "Womething went wrong.");
            }
            else
            {
                result.SetData(true);
            }

            return result;
        }

        // ...
        public BusinessResult<bool> DeleteProfileBusiness(int Id)
        {
            BusinessResult<bool> result = new BusinessResult<bool>();

            UserTable user = userData.GetUser(Id);


            if (user.Id == 0)
            {
                result.SetError(404, "User  not found.");
                return result;
            }


            if (userData.Delete(Id))
            {
                string file = @$"{Path.Combine(Directory.GetCurrentDirectory(), @"Static\Avatar\")}{user.Username.ToLower()}.png";

                if (File.Exists(file))
                {
                    File.Delete(file);
                }

                result.SetData(true);
            }
            else
            {
                result.SetError(500, "Something went wrong.");
            }

            return result;
        }

        // ...
    }
}
