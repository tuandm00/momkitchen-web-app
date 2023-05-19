using AutoMapper;
using momkitchen.Mapper;
using momkitchen.Models;
using System.Security.Cryptography;
using System.Text;

namespace momkitchen.Services
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MomkitchenContext ctx;
        private readonly IMapper _mapper;

        private static int PAGE_SIZE { get; set; } = 5;

        public AccountRepository(MomkitchenContext context, IMapper mapper)
        {
            ctx = context;
            _mapper = mapper;
        }

        public List<Account> GetAll(String email, int page = 1)
        {
            var accounts = ctx.Accounts.AsQueryable();

            if (!string.IsNullOrEmpty(email))
            {
                accounts = accounts.Where(x => x.Email.Contains(email));
            }
            accounts = accounts.Skip((page-1) * PAGE_SIZE).Take(PAGE_SIZE);
            var result = accounts.Select(x => new Account
            {
                Password = x.Password,
                Email = x.Email,
                RoleId = x.RoleId,
                Role = x.Role,
            });

            return result.ToList();
        }

        public AuthenticationResult Login(LoginDto account)
        {
            var user = ctx.Accounts.Select(x => new Account()
            {
                Email = x.Email,
                Password = x.Password,
                AccountStatus = x.AccountStatus,
                Role = x.Role
            }).FirstOrDefault(x => x.Email == account.Email);

            if (user != null)
            {
                bool isHashed = HashPassword(account.Password) == user.Password;
                user.Password = null;

                if (isHashed)
                {
                    return new AuthenticationResult
                    {
                        IsAuthenticated = true,
                        User = user
                    };
                }
            }

            return new AuthenticationResult
            {
                IsAuthenticated = false
            };
        }

        public async Task UpdateAccountStatus(string email, bool status)
        {
            var account = await ctx.Accounts.FindAsync(email);
            if (account != null)
            {
                account.AccountStatus = status.ToString();
                await ctx.SaveChangesAsync();
            }
        }

        public string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        public async Task Register(Account account)
        {
            var existedAccount = ctx.Accounts.Where(x => x.Email == account.Email).FirstOrDefault();
            if (existedAccount != null) throw new Exception("Email is existed");

            account.Password = HashPassword(account.Password);
            account.RoleId = 2;
            account.AccountStatus = "true";



            ctx.Add(account);
            if(await ctx.SaveChangesAsync() == 1)
            {
                var customer = new Customer
                {
                    Email = account.Email,
                };

                ctx.Add(customer);
                await ctx.SaveChangesAsync();
            }

            


        }

        public async Task UpdateAccount(string email,Account account)
        {
            var accounts = await ctx.Accounts.FindAsync(email);
            if(account != null)
            {
                accounts.Email = account.Email;
                accounts.Password = HashPassword(account.Password);
                accounts.RoleId = account.RoleId;
                accounts.AccountStatus = "true";
                await ctx.SaveChangesAsync();
            }
        }

        public async Task RegisterChef(Account account)
        {
            var existedAccount = ctx.Accounts.Where(x => x.Email == account.Email).FirstOrDefault();
            if (existedAccount != null) throw new Exception("Email is existed");

            account.Password = HashPassword(account.Password);
            account.RoleId = 3;
            account.AccountStatus = "true";



            ctx.Add(account);
            if (await ctx.SaveChangesAsync() == 1)
            {
                var chef = new Chef
                {
                    Email = account.Email,
                };

                ctx.Add(chef);
                await ctx.SaveChangesAsync();
            }


        }
        public async Task RegisterShipper(Account account)
        {
            var existedAccount = ctx.Accounts.Where(x => x.Email == account.Email).FirstOrDefault();
            if (existedAccount != null) throw new Exception("Email is existed");

            account.Password = HashPassword(account.Password);
            account.RoleId = 4;
            account.AccountStatus = "true";



            ctx.Add(account);
            if (await ctx.SaveChangesAsync() == 1)
            {
                var shipper = new Shipper
                {
                    Email = account.Email,
                };

                ctx.Add(shipper);
                await ctx.SaveChangesAsync();
            }


        }

        public  Task UpdateCustomerDetail(CustomerDto customerDto)
        {
            var currentCustomer = ctx.Customers.Where(x => x.Email == customerDto.Email).FirstOrDefault();
            if (currentCustomer != null)
            {
                currentCustomer.Id = customerDto.Id;
                currentCustomer.Email = customerDto.Email;
                currentCustomer.Name = customerDto.Name;
                currentCustomer.Phone = customerDto.Phone;
                currentCustomer.Image = customerDto.Image;
                currentCustomer.DefaultBuilding = customerDto.DefaultBuilding;

                ctx.SaveChangesAsync();
            }
            return null;
        }

    }
}
