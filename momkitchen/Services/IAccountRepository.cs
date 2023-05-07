using momkitchen.Mapper;
using momkitchen.Models;

namespace momkitchen.Services
{
    public interface IAccountRepository
    {
        List<Account> GetAll(String email, int page = 1);
        Task<Account> Login(LoginDto account);
        Task UpdateAccountStatus(string email, bool status);
        string HashPassword(string password);
        Task Register(Account account);

        Task UpdateAccount(string email, Account account);
    }
}
