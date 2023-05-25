using momkitchen.Mapper;
using momkitchen.Models;

namespace momkitchen.Services
{
    public interface IAccountRepository
    {
        List<Account> GetAll(String email, int page = 1);
        AuthenticationResult Login(LoginDto account);

        Task UpdateAccountStatus(string email, bool status);
        string HashPassword(string password);
        Task Register(Account account);

        Task RegisterChefAndShipper(Account account);


        Task UpdateAccount(string email, AccountDto accountDto);

        Task UpdateCustomerDetail(CustomerDto customerDto);

        List<CustomerDto> GetInfoCusByID(string email);
    }
}
