using AutoMapper;
using BLL.DTOs;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductService _productService;

        public OrderService(IMapper mapper, IOrderRepository orderRepository, IProductService productService)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _productService = productService;
        }

        public async Task<ReadOrderDTO> AddAsync(CreateOrderDTO model)
        {
           var order = _mapper.Map<Order>(model);
           var product = await _productService.GetByIdAsync(model.ProductId);
           if (product == null)
           {
                throw new AuctionException("Product not found", System.Net.HttpStatusCode.NotFound);
           }
           await _orderRepository.AddAsync(order);
           await _orderRepository.Save();

           var ordertToReturn = _mapper.Map<ReadOrderDTO>(order);
            ordertToReturn.TotalPrice = product.Price * ordertToReturn.Amount;
            return ordertToReturn;


        }

        public Task<string> DeleteByIdAsync(int modelId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReadOrderDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ReadOrderDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateAsync(CreateOrderDTO model, int id)
        {
            throw new NotImplementedException();
        }
    }
}
