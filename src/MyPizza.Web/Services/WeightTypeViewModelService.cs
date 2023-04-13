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
        public WeightTypeViewModelService(IUoWRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<WeightTypeViewModel>> GetWeihgtTypesAsync(Guid? categoryId)
        {
            var weights = await _repository.WeightTypes.GetListAsync(predicate: x => x.CategoryId.Equals(categoryId));
            var mapedWeights = _mapper.Map<List<WeightTypeViewModel>>(weights);
            return mapedWeights;
        }
    }
}
