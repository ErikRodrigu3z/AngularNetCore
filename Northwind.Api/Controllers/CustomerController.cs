using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Northwin.Models;
using Northwind.UnitOfWork;

namespace Northwind.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("{id:int}")]
        public  IActionResult GetById(int id)
        {
            var res = Request.Headers.ToArray();
            throw new Exception("error inesperado");
            return Ok(_unitOfWork.Customer.GetById(id));
            
        }

        [HttpGet]
        [Route("GetPaginatedCustomer/{page:int}/{rows:int}")]
        public IActionResult GetPaginatedCustomer(int page, int rows)
        {
            try
            {
                return Ok(_unitOfWork.Customer.CustomerPagedList(page, rows));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]       
        public IActionResult Post([FromBody] Customer customer)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                return Ok(_unitOfWork.Customer.Insert(customer));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Customer customer)
        {
            try
            {
                if (!ModelState.IsValid && _unitOfWork.Customer.Update(customer))
                {
                    return Ok(new { Message = "ok"});

                }
                return BadRequest();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] Customer customer)
        {
            try
            {
                if (customer.Id > 0)
                {
                    return Ok(_unitOfWork.Customer.Delete(customer));

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
