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

        /// <summary>
        /// Injecting repositories
        /// </summary>
        public OrderService(IMapper mapper, IOrderRepository orderRepository, IProductService productService)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _productService = productService;
        }

        /// <summary>
        /// this method adds the order to the database
        /// throws an exception on failure
        /// </summary>
        /// <param name="model">order model</param>
        public async Task<ReadOrderDTO> AddAsync(CreateOrderDTO model)
        {
           var order = _mapper.Map<Order>(model);
           var product = await _productService.GetByIdAsync(model.ProductId);
           if (product == null)
           {
                throw new AuctionException("Product not found", System.Net.HttpStatusCode.NotFound);
           }
           await _orderRepository.AddAsync(order);
           await _orderRepository.SaveAsync();

           var orderToReturn = _mapper.Map<ReadOrderDTO>(order);
           return orderToReturn;
        }

        /// <summary>
        /// this method delets the order by id from the database, and checks if current user is owner of order or product owner
        /// throws an exception on failure
        /// </summary>
        /// <param name="modelId">order id</param>
        /// <param name="currentUserId">user id</param>
        public async Task<string> DeleteAsUserByIdAsync(int modelId, string currentUserId)
        {
            var order = _orderRepository.FindAllWithDetails().FirstOrDefault(x => x.Id == modelId);
            if (order == null || (order.Product.UserId != currentUserId && order.UserId != currentUserId))
            {
                throw new AuctionException("Order not found", System.Net.HttpStatusCode.NotFound);
            }
           
            await _orderRepository.DeleteByIdAsync(modelId);
            await _orderRepository.SaveAsync();
            return $"Order {order.Product.Name} has been deleted succesfully"; 
        }

        /// <summary>
        /// this method delets the order by id from the database
        /// throws an exception on failure
        /// </summary>
        /// <param name="modelId">order id</param>
        public async Task<string> DeleteByIdAsync(int modelId)
        {
            var order = _orderRepository.FindAllWithDetails().FirstOrDefault(x => x.Id == modelId);
            if (order == null)
            {
                throw new AuctionException("Order not found", System.Net.HttpStatusCode.NotFound);
            }
            await _orderRepository.DeleteByIdAsync(modelId);
            await _orderRepository.SaveAsync();
            return $"Order {order.Product.Name} has been deleted succesfully";
        }

        /// <summary>
        /// this method returns all orders from the database
        /// </summary>
        public IEnumerable<ReadOrderDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<ReadOrderDTO>>(_orderRepository.FindAllWithDetails());
        }

        /// <summary>
        /// this method returns order from the database by id 
        /// </summary>
        /// <param name="id">order id</param>
        public async Task<ReadOrderDTO> GetByIdAsync(int id)
        {
            return _mapper.Map<ReadOrderDTO>(await _orderRepository.GetByIdWithDetailsAsync(id));
        }

        /// <summary>
        /// this method returns the products that were ordered from the current user
        /// </summary>
        /// <param name="currentUserId">current user id</param>
        public IEnumerable<ReadOrderDTO> IncomingUserOrders(string currentUserId)
        {
            return _mapper.Map<IEnumerable<ReadOrderDTO>>(_orderRepository.FindAllWithDetails().Where(x => x.Product.UserId == currentUserId));
        }

        /// <summary>
        /// this method returns the products that were ordered from the current user by product name
        /// </summary>
        /// <param name="currentUserId">current user id</param>
        /// <param name="name">current user name</param>
        public IEnumerable<ReadOrderDTO> IncomingUserOrdersByProductName(string currentUserId, string name)
        {
            return _mapper.Map<IEnumerable<ReadOrderDTO>>(_orderRepository.FindAllWithDetails().Where(x => x.Product.UserId == currentUserId && x.Product.Name.Contains(name)));
        }

        /// <summary>
        /// this method returns products that have been ordered by the current user
        /// </summary>
        /// <param name="currentUserId">current user id</param>
        public IEnumerable<ReadOrderDTO> OutcomingUserOrders(string currentUserId)
        {
            return _mapper.Map<IEnumerable<ReadOrderDTO>>(_orderRepository.FindAllWithDetails().Where(x => x.UserId == currentUserId));
        }

        /// <summary>
        /// this method returns products that have been ordered by the current user by product name
        /// </summary>
        /// <param name="currentUserId">current user id</param>
        /// <param name="name">current user name</param>
        public IEnumerable<ReadOrderDTO> OutcomingUserOrdersByProductName(string currentUserId, string name)
        {
            return _mapper.Map<IEnumerable<ReadOrderDTO>>(_orderRepository.FindAllWithDetails().Where(x => x.UserId == currentUserId && x.Product.Name.Contains(name)));
        }

        public Task<string> UpdateAsync(CreateOrderDTO model, int id)
        {
            throw new NotImplementedException();
        }
    }
}
