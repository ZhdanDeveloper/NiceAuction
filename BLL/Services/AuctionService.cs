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
        private readonly IAuctionRepository _auctionRepository;
        private readonly IMapper _mapper;

        public AuctionService(IAuctionRepository auctionRepository, IMapper mapper)
        {
            _auctionRepository = auctionRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(AuctionDTO model)
        {

       
            await _auctionRepository.AddAsync(_mapper.Map<Auction>(model));
            _auctionRepository.Save();
        }

        public Task DeleteByIdAsync(int modelId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AuctionDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<AuctionDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(AuctionDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
