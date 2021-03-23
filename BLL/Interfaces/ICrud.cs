using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICrud<TModel, ReadModel, UModel> where TModel : class where ReadModel : class where UModel : class
    {
        IEnumerable<ReadModel> GetAll();
        Task<ReadModel> GetByIdAsync(int id);
        Task<ReadModel> AddAsync(TModel model);
        Task<string> UpdateAsync(UModel model, int id);
        Task<string> DeleteByIdAsync(int modelId);

    }
}
