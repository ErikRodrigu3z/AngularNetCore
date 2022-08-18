using Microsoft.AspNetCore.Mvc;
using Northwin.Models;
using Northwind.UnitOfWork;

namespace Northwind.Api.Controllers
{
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
            try
            {
                return Ok(_unitOfWork.Customer.GetById(id));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]       
        public IActionResult Post(Customer customer)
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


    }
}
