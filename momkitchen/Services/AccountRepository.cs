using momkitchen.Mapper;
using momkitchen.Models;
using System.Security.Cryptography;
using System.Text;

namespace momkitchen.Services
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MomkitchenContext ctx;

        private static int PAGE_SIZE { get; set; } = 5;

        public AccountRepository(MomkitchenContext context)
        {
            ctx = context;
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

        public Task<Account> Login(LoginDto account)
        {
            var user = ctx.Accounts.Select(x => new Account()
            {
                Email = x.Email,
                Password = x.Password,
                AccountStatus = x.AccountStatus,
                Role = x.Role
            }).Where(x => x.Email == account.Email).FirstOrDefault();
            bool isHashed = false;
            if (user != null)
            {
                isHashed = HashPassword(account.Password) == user.Password;
                user.Password = null;
            }
            
            return isHashed ? Task.FromResult(user) : null;
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
                    Id = Guid.NewGuid().GetHashCode(),
                    Email = account.Email,
                    Name = account.Email,
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
                    Id = Guid.NewGuid().GetHashCode(),
                    Email = account.Email,
                    Name = account.Email,
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
                    Id = Guid.NewGuid().GetHashCode(),
                    Email = account.Email,
                    Name = account.Email,
                };

                ctx.Add(shipper);
                await ctx.SaveChangesAsync();
            }


        }

    }
}
