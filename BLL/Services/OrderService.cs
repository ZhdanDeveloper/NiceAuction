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

           var orderToReturn = _mapper.Map<ReadOrderDTO>(order);
           return orderToReturn;
        }

        public async Task<string> DeleteAsUserByIdAsync(int modelId, string CurrentUserId)
        {
            var order = _orderRepository.FindAllWithDetails().FirstOrDefault(x => x.Id == modelId);
            if (order == null || (order.Product.UserId != CurrentUserId && order.UserId != CurrentUserId))
            {
                throw new AuctionException("Order not found", System.Net.HttpStatusCode.NotFound);
            }
           
            await _orderRepository.DeleteByIdAsync(modelId);
            await _orderRepository.Save();
            return $"Order {order.Product.Name} has been deleted succesfully"; 
        }

        public async Task<string> DeleteByIdAsync(int modelId)
        {
            var order = _orderRepository.FindAllWithDetails().FirstOrDefault(x => x.Id == modelId);
            if (order == null)
            {
                throw new AuctionException("Order not found", System.Net.HttpStatusCode.NotFound);
            }
            await _orderRepository.DeleteByIdAsync(modelId);
            await _orderRepository.Save();
            return $"Order {order.Product.Name} has been deleted succesfully";
        }

        public IEnumerable<ReadOrderDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<ReadOrderDTO>>(_orderRepository.FindAllWithDetails());

        }

        public async Task<ReadOrderDTO> GetByIdAsync(int id)
        {
            return _mapper.Map<ReadOrderDTO>(await _orderRepository.GetByIdWithDetailsAsync(id));
        }

        public IEnumerable<ReadOrderDTO> IncomingUserOrders(string CurrentUserId)
        {
            return _mapper.Map<IEnumerable<ReadOrderDTO>>(_orderRepository.FindAllWithDetails().Where(x => x.Product.UserId == CurrentUserId));
        }

        public IEnumerable<ReadOrderDTO> OutcomingUserOrders(string CurrentUserId)
        {
            return _mapper.Map<IEnumerable<ReadOrderDTO>>(_orderRepository.FindAllWithDetails().Where(x => x.UserId == CurrentUserId));
        }

        public Task<string> UpdateAsync(CreateOrderDTO model, int id)
        {
            throw new NotImplementedException();
        }
    }
}
