using BLL.DTOs;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using AutoMapper;
using DAL.Entities;

namespace BLL.Services
{
    public class AuctionService : IAuctionService
    {
        private readonly IProductRepository _auctionRepository;
        private readonly IMapper _mapper;

        public AuctionService(IProductRepository auctionRepository, IMapper mapper)
        {
            _auctionRepository = auctionRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(ProductDTO model)
        {

       
            await _auctionRepository.AddAsync(_mapper.Map<Product>(model));
            _auctionRepository.Save();
        }

        public Task DeleteByIdAsync(int modelId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ProductDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ProductDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
