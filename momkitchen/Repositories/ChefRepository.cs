using momkitchen.Models;

namespace momkitchen.Services
{
    public class ChefRepository : IChefRepository
    {
        private readonly MomkitchenContext _ctx;

        public ChefRepository(MomkitchenContext context)
        {
            _ctx = context;
        }

        //public List<Chef> GetAllChef()
        //{
        //    var result = _ctx.Chefs.Select(x => new Chef()
        //    {
        //        Id = x.Id,
        //        Name = x.Name,
        //        Phone = x.Phone,
        //    });
        //}
    }
}
