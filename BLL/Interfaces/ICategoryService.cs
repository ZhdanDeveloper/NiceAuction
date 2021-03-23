using BLL.DTOs;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface ICategoryService : ICrud<CreateCategoryDTO, Category, CreateCategoryDTO>
    {
    }
}
