using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Northwin.Models;
using Northwind.UnitOfWork;

namespace Northwind.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            var res = Request.Headers.ToArray();
            //TODO: revisar los ambientes
            //throw new Exception("error inesperado");  
            return Ok(_unitOfWork.Order.GetById(id));

        }

        [HttpGet]
        [Route("GetPaginatedOrder/{page:int}/{rows:int}")]
        public IActionResult GetPaginatedOrder(int page, int rows)
        {
            try
            {
                return Ok(_unitOfWork.Order.OrderPagedList(page, rows));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Order order)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                return Ok(_unitOfWork.Order.Insert(order));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Order order)
        {
            try
            {
                if (!ModelState.IsValid && _unitOfWork.Order.Update(order))
                {
                    return Ok(new { Message = "ok" });

                }
                return BadRequest();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] Order order)
        {
            try
            {
                if (order.Id > 0)
                {
                    return Ok(_unitOfWork.Order.Delete(order));
                  
                }
                return BadRequest();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
