using AutoMapper;
using MyPizza.Infrastructure.Interfaces;
using MyPizza.Web.Interfaces;
using MyPizza.Web.Models;

namespace MyPizza.Web.Services
{
    public class WeightTypeViewModelService : IWeightTypeViewModelService
    {
        private readonly IUoWRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<WeightTypeViewModelService> _logger;
        public WeightTypeViewModelService(IUoWRepository repository,
            IMapper mapper,
            ILogger<WeightTypeViewModelService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IList<WeightTypeViewModel>> GetWeihgtTypesAsync(Guid? categoryId)
        {
            _logger.LogInformation($"Weight type list with category {categoryId} has been recieved");
            var weights = await _repository.WeightTypes.GetListAsync(predicate: x => x.CategoryId.Equals(categoryId));
            var mapedWeights = _mapper.Map<List<WeightTypeViewModel>>(weights);
            return mapedWeights;
        }
    }
}
