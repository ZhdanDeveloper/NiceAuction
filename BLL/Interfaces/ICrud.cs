﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICrud<TModel, ReadModel> where TModel : class where ReadModel : class
    {
        IEnumerable<ReadModel> GetAll();
        Task<ReadModel> GetByIdAsync(int id);
        Task<ReadModel> AddAsync(TModel model);
        Task<string> UpdateAsync(TModel model);
        Task<string> DeleteByIdAsync(int modelId);

    }
}
