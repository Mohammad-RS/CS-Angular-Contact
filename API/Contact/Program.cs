using Contact.Business;
using Contact.Data;
using Contact.Model;
using Contact.Model.User;
using Contact.Utility;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Contact
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //// Get connectionString: View --> SQL Server Object Explorer --> Add DB --> DB properties --> Add Database=Contact



            //string query = "INSERT INTO dbo.[User] (Username, Email, [Password], Fullname) VALUES (@Username, @Email, @Password, @Fullname)";

            //object param = new
            //{
            //    Username = "Alireza",
            //    Email = "Alireza@gmail.com",
            //    Password = Array.Empty<byte>(),
            //    Fullname = "Alireza"
            //};

            //conn.Execute(query, param);



            //query = "UPDATE dbo.[User] SET Fullname = @New_Fullname WHERE Username = @Username";

            //conn.Execute(query, new { Username = "Alireza", New_Fullname = "Alireza Dehghan" });



            //query = "SELECT * FROM dbo.[User] WHERE Id IN @Ids";

            //IEnumerable<UserTable> users = conn.Query<UserTable>(query, new { Ids = new int[] { 1, 2 } });

            //foreach (UserTable user in users)
            //{
            //    Console.WriteLine($"Username: {user.Username}, Fullname: {user.Fullname}");
            //}



            //string file = @$"C:\Users\USER\Desktop\Contact\API\Contact\static\avatar\SyncSphere.png";

            //string data = Convert.ToBase64String(File.ReadAllBytes(file));

            //UserAddModel model = new UserAddModel()
            //{
            //    Username = "newUser1",
            //    Email = "newUser1@gmail.com",
            //    Password1 = "1234Ty7890",
            //    Password2 = "1234Ty7890",
            //    Fullname = "",
            //    AvatarData = data,
            //};

            //BusinessResult<int> result = new UserBusiness().Register(model);
            //Console.WriteLine(result.Data);



            //BusinessResult<int> result = new UserBusiness().Login(new UserLoginModel { Username = "newUser1", Password = "1234Ty7890" });
            //Console.WriteLine(result.Data);



            //BusinessResult<UserProfileModel> result = new UserBusiness().Profile(25);
            //Console.WriteLine(result.Data.Fullname + " " + result.Data.Date_Created);



            //UserProfileModel profile = new UserData().GetUserProfileById(25);
            //Console.WriteLine(profile.Username);



            //BusinessResult<VisitUserProfileModel> result = new UserBusiness().VisitProfile("newUser1");
            //Console.WriteLine(result.Data.Fullname + " " + result.Data.Date_Created);



            //UserChangePasswordModel model = new UserChangePasswordModel()
            //{
            //    Id = 25,
            //    // CurrentPassword = "1234Ty7890",
            //    // Password1 = "Aa11111111",
            //    // Password2 = "Aa11111111",

            //    CurrentPassword = "Aa11111111",
            //    Password1 = "1234Ty7890",
            //    Password2 = "1234Ty7890",
            //};

            //BusinessResult<bool> result = new UserBusiness().ChangePassword(model);
            //Console.WriteLine(result.ErrorCode + " " + result.Data);



            //UserUpdateModel data = new UserUpdateModel()
            //{
            //    Id = 25,
            //    Username = "newUser1",
            //    Fullname = "updated full name with update model",
            //};

            //BusinessResult<bool> result = new UserBusiness().EditProfile(data);
            //Console.WriteLine(result.Data);



            //BusinessResult<bool> result = new UserBusiness().DeleteProfile(8);
            //Console.WriteLine(result.Data + " " + result.Success);



            //string file = @$"C:\Users\USER\Desktop\Contact\API\Contact\Static\Avatar\SyncSphere.png";
            //string avatarData = "data:image/png;base64," + Convert.ToBase64String(File.ReadAllBytes(file));
            //Console.WriteLine(avatarData);



            //var builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddControllers();

            //string cs = builder.Configuration.GetSection("Connection").Value;

            //builder.Services.AddTransient<SqlConnection>(x => new SqlConnection(cs));

            //builder.Services.AddTransient<Crud>();

            //builder.Services.AddTransient<UserBusiness>();
            //builder.Services.AddTransient<ContactBusiness>();

            //builder.Services.AddTransient<UserData>();
            //builder.Services.AddTransient<ContactData>();

            //builder.Services.AddAuthorization();
            //builder.Services.AddAuthentication().AddJwtBearer(x =>
            //{
            //    x.TokenValidationParameters = Token.Params;
            //});

            //var app = builder.Build();

            //app.UseCors(x =>
            //{
            //    x.AllowAnyHeader();
            //    x.AllowAnyMethod();
            //    x.AllowAnyOrigin();
            //});

            //app.UseAuthorization();
            ////app.UseAuthentication();

            //app.MapControllers();

            //app.Run();



            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            var app = builder.Build();

            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
