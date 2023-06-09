using Microsoft.AspNetCore.Identity;
using System;

namespace GasolinerasVIP.API.Models
{
    public class UserServiceRepository : IUserServiceRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;

        public UserServiceRepository(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            this._userManager = userManager;
            this._db = db;
        }

        public UserRefreshToken AddUserRefreshToken(UserRefreshToken user)
        {
            _db.UserRefreshToken.Add(user);
            return user;
        }

        public void DeleteUserRefreshToken(string username, string refreshToken)
        {
            var item = _db.UserRefreshToken.FirstOrDefault(x => x.UserName == username && x.RefreshToken == refreshToken);
            if (item != null)
            {
                _db.UserRefreshToken.Remove(item);
            }
        }

        public UserRefreshToken GetSavedRefreshToken(string username, string refreshToken)
        {
            return _db.UserRefreshToken.FirstOrDefault(x => x.UserName == username && x.RefreshToken == refreshToken && x.IsActive == true);
        }

        public int SaveCommit()
        {
            return _db.SaveChanges();
        }

        public async Task<bool> IsValidUserAsync(UserLogin user)
        {
            var u = _userManager.Users.FirstOrDefault(o => o.UserName == user.username);
            var result = await _userManager.CheckPasswordAsync(u, user.password);
            return result;

        }
    }
}
