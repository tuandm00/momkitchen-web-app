﻿using AutoMapper;
using momkitchen.Mapper;
using momkitchen.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

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
            accounts = accounts.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);
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
                bool isHashed = HashPassword1(account.Password) == user.Password;
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

        public string HashPassword1(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        public async Task Register(Account account)
        {
            var existedAccount = ctx.Accounts.Where(x => x.Email == account.Email).FirstOrDefault();
            if (existedAccount != null) throw new Exception("Email is existed");

            account.Password = HashPassword1(account.Password);
            account.RoleId = 2;
            account.AccountStatus = "true";



            ctx.Add(account);
            ctx.SaveChangesAsync();
            if (await ctx.SaveChangesAsync() == 1)
            {
                var customer = new Customer
                {
                    Email = account.Email,
                };

                ctx.Add(customer);
                await ctx.SaveChangesAsync();
            }




        }

        public async Task UpdateAccount(string email, AccountDto accountDto)
        {
            var accounts = await ctx.Accounts.FindAsync(email);
            if (accountDto != null)
            {
                accounts.Password = HashPassword1(accountDto.Password);
                accounts.RoleId = accountDto.RoleId;
                accounts.AccountStatus = "true";
                await ctx.SaveChangesAsync();
            }
        }

        public async Task RegisterChefAndShipper(Account account)
        {
            var existedAccount = ctx.Accounts.Where(x => x.Email == account.Email).FirstOrDefault();
            if (existedAccount != null) throw new Exception("Email is existed");

            account.Password = HashPassword1(account.Password);
            account.RoleId = account.RoleId;
            account.AccountStatus = "true";


            ctx.Add(account);
            if (account.RoleId == 3)
            {
                if (await ctx.SaveChangesAsync() == 1)
                {
                    var chef = new Chef
                    {
                        Email = account.Email,
                        Phone = "0",
                    };

                    ctx.Add(chef);
                    await ctx.SaveChangesAsync();
                }
            }

            if (account.RoleId == 4)
            {
                
                    var shipper = new Shipper
                    {
                        Email = account.Email,
                        Phone = "0",
                    };

                    ctx.Add(shipper);
                    await ctx.SaveChangesAsync();
            }
        }

        public async Task RegisterShipperByBcryt(Account account)
        {
            var existedAccount = ctx.Accounts.Where(x => x.Email == account.Email).FirstOrDefault();
            if (existedAccount != null) throw new Exception("Email is existed");


            account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            account.RoleId = account.RoleId;
            account.AccountStatus = "true";

            ctx.Add(account);
            if (account.RoleId == 4)
            {
                    var shipper = new Shipper
                    {
                        Email = account.Email,
                        Phone = "0",
                    };

                    ctx.Add(shipper);
                    await ctx.SaveChangesAsync();

            }
        }

        public Task UpdateCustomerDetail(CustomerDto customerDto)
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

        public List<CustomerDto> GetInfoCusByID(string email)
        {
            var accounts = ctx.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(email))
            {
                accounts = accounts.Where(x => x.Email.Contains(email));
            }
            var result = accounts.Select(x => new CustomerDto
            {
                Id = x.Id,
                Name = x.Name,
                Phone = x.Phone,
                Image = x.Image,
                Email = email,
                DefaultBuilding = x.DefaultBuilding,

            });

            return result.ToList();
        }

        public Customer GetAllCustomerByEmail(string email)
        {
            var result = ctx.Customers.Where(x => x.Email == email).Select(x => new Customer()
            {
                Id=x.Id,
                Name=x.Name,
                Phone=x.Phone,
                Image=x.Image,
                Email=email,
                DefaultBuilding=x.DefaultBuilding,
            }).FirstOrDefault();
            return result;
        }

        public Account GetAccountByEmail(string email)
        {
            var result = ctx.Accounts.Where(x => x.Email == email).SingleOrDefault();
            return result;
        }

        public List<GetAllUserDto> GetAllUserDetail()
        {
            var customers = ctx.Customers.Select(c => new GetAllUserDto
            {
                Id = c.Id,
                Name = c.Name,
                DefaultBuilding = c.DefaultBuilding,
                Email = c.Email,
                Image = c.Image,
                Phone = c.Phone,
            }).ToList();

            var chefs = ctx.Chefs.Select(ch => new GetAllUserDto
            {
                Id = ch.Id,
                Address = ch.Address,
                BuildingId = ch.BuildingId,
                Email = ch.Email,
                Image = ch.Image,
                Name = ch.Name,
                Phone = ch.Phone,
            }).ToList();

            var shippers = ctx.Shippers.Select(s => new GetAllUserDto
            {
                Id = s.Id,
                Address = s.Address,
                BatchId = s.BatchId,
                Email = s.Email,
                Image = s.Image,
                Name = s.Name,
                Phone = s.Phone,
            }).ToList();

            var allUsers = new List<GetAllUserDto>();
            allUsers.AddRange(customers);
            allUsers.AddRange(chefs);
            allUsers.AddRange(shippers);

            return allUsers;
        }

        public int CountCustomer()
        {
            var result = ctx.Customers.Count();
            return result;
        }
    }
}
