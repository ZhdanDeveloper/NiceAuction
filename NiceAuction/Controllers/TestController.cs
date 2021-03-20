using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NiceAuction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        public static IWebHostEnvironment _environment;
        private readonly IAuctionService _auctionService;

        public TestController(IWebHostEnvironment environment, IAuctionService auctionService)
        {
            _environment = environment;
            _auctionService = auctionService;
        }

        [HttpPost]
        public IActionResult AddAuction(AuctionDTO auction)
        {

         


            return Ok();




        }



    }
}
