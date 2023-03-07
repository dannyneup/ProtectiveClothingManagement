using Pcm.Application.Interfaces.RequestModels;
using Pcm.Application.Interfaces.ResponseModels;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;

namespace Pcm.Application.Interfaces.Repositories;

public interface ITrainingRepository : IRepository<ITrainingResponseModel, ITrainingRequestModel>
{
    Task<IEnumerable<IPersonInfoResponseModel>> GetTrainees(int id);
    Task<IEnumerable<ILoadOutPartResponseModel>> GetLoadOut(int id);
    Task<IEnumerable<ILoadOutPartResponseModel>> InsertLoadOut(int trainingId,
        IEnumerable<ILoadOutPartRequestModel> loadOut);
}