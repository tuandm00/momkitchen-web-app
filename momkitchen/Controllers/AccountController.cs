using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using momkitchen.Mapper;
using momkitchen.Models;
using momkitchen.Services;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace momkitchen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly MomkitchenContext ctx;
        private readonly IAccountRepository _repository;

        public AccountController(MomkitchenContext context, IMapper mapper, IAccountRepository repository)
        {
            ctx = context;
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        [Route("getallaccount")]
        public async Task<IActionResult> GetAllAccount()
        {

            List<Account> accounts = await ctx.Accounts.Select(x => new Account()
            {
                Email = x.Email,
                Password = x.Password,
                AccountStatus = x.AccountStatus,
                RoleId = x.RoleId,
                Role = x.Role
            }).ToListAsync();
            return Ok(accounts);
        }



        [HttpPost]
        [Route("addaccount")]
        public async Task<IActionResult> AddUser(Account account)
        {
            var accounts = new Account()
            {
                Password = account.Password,
                Email = account.Email,
            };

            await ctx.Accounts.AddAsync(accounts);
            await ctx.SaveChangesAsync();
            return Ok(accounts);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAccount([FromQuery] string email, AccountDto accountDto)
        {
            await _repository.UpdateAccount(email, accountDto);
            return Ok();
        }
        private bool UserAvailable(string email)
        {
            return (ctx.Accounts?.Any(x => x.Email == email )).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            if (ctx.Accounts == null)
            {
                return NotFound();
            }

            var user = await ctx.Accounts.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ctx.Accounts.Remove(user);
            await ctx.SaveChangesAsync();

            return Ok();

        }

        [HttpPatch]
        public async Task<IActionResult> PatchAccount([FromQuery] string email, bool status)
        {
            await _repository.UpdateAccountStatus(email, status);
            return Ok();
        }

        [HttpPut]
        [Route("updateaccountdetail")]
        public async Task UpdateCustomerDetail(CustomerDto customerDto)
        {
            try
            {
                  await _repository.UpdateCustomerDetail(customerDto);
            }catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet]
        public List<CustomerDto> GetInfoCusByID(string email) => _repository.GetInfoCusByID(email);
        
    }
}
