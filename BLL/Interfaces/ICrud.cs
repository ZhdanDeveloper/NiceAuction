using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICrud<CreateModel, ReadModel, UpdateModel> where CreateModel : class where ReadModel : class where UpdateModel : class
    {
        IEnumerable<ReadModel> GetAll();
        Task<ReadModel> GetByIdAsync(int id);
        Task<ReadModel> AddAsync(CreateModel model);
        Task<string> UpdateAsync(UpdateModel model, int id);
        Task<string> DeleteByIdAsync(int modelId);
    }
}
