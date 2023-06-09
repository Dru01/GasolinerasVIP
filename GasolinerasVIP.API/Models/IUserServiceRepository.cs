namespace GasolinerasVIP.API.Models
{
    public interface IUserServiceRepository
    {
        Task<bool> IsValidUserAsync(UserLogin user);

        UserRefreshToken AddUserRefreshToken(UserRefreshToken user);

        UserRefreshToken GetSavedRefreshToken(string username, string refreshtoken);

        void DeleteUserRefreshToken(string username, string refreshToken);

        int SaveCommit();
    }
}
