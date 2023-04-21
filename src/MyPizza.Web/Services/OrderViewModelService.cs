using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        private readonly ILogger<OrderViewModelService> _logger;
        public OrderViewModelService(IUoWRepository repository,
                                    IMapper mapper,
                                    ILogger<OrderViewModelService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<OrderViewModel> GetLastUserOrderAsync(Guid userId)
        {
            _logger.LogInformation($"User with id {userId} got his last order");
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
            _logger.LogInformation($"User with id {model.UserId} created an order");
            var newOrder = _mapper.Map<Order>(model);
            newOrder = await _repository.Orders.CreateAsync(newOrder);
            var newModel = _mapper.Map<OrderViewModel>(newOrder);
            return newModel;
        }

        public async Task<IList<OrderViewModel>> GetUserOrdersAsync(Guid userId)
        {
            _logger.LogInformation($"User with id {userId} got orders list");
            var orders = await _repository.Orders.GetListAsync(
                predicate: x => x.UserId == userId,
                orderBy: x => x.OrderByDescending(x => x.Date),
                includes: x => x.Include(p => p.Products)
                                .ThenInclude(x => x.Product)!
                                .ThenInclude(x => x!.ProductType!)
                                .ThenInclude(x => x.Category!));
            if (orders == null)
            {
                return new List<OrderViewModel>();
            }

            var orderModels = _mapper.Map<List<OrderViewModel>>(orders);
            return orderModels;
        }
    }
}
