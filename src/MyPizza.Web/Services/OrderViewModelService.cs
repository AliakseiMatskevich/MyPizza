using AutoMapper;
using MyPizza.ApplicationCore.Entities;
using MyPizza.ApplicationCore.Interfaces;
using MyPizza.Infrastructure.Interfaces;
using MyPizza.Web.Interfaces;
using MyPizza.Web.Models;

namespace MyPizza.Web.Services
{
    public class OrderViewModelService : IOrderViewModelService
    {
        private readonly IUoWRepository _repository;
        private readonly IMapper _mapper;
        public OrderViewModelService(IUoWRepository repository,
                                    IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<OrderViewModel> GetLastUserOrderAsync(Guid userId)
        {
            var order = await _repository.Orders.FirstOrDefaultAsync(
                predicate: x => x.UserId == userId,
                orderBy: x => x.OrderByDescending(x => x.Date));
            if (order == null)
            {
                return new OrderViewModel() { UserId = userId};
            }

            var orderModel = _mapper.Map<OrderViewModel>(order);
            return orderModel;
        }

        public async Task<OrderViewModel> CreateOrderAsync(OrderViewModel model)
        {
            var newOrder = _mapper.Map<Order>(model);
            newOrder = await _repository.Orders.CreateAsync(newOrder);
            var newModel = _mapper.Map<OrderViewModel>(newOrder);
            return newModel;
        }
    }
}
