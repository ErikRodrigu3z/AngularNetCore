using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Northwin.Models;
using Northwind.UnitOfWork;

namespace Northwind.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
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
            return Ok(_unitOfWork.Product.GetById(id));

        }

        [HttpGet]
        [Route("GetPaginatedProduct/{page:int}/{rows:int}")]
        public IActionResult GetPaginatedProduct(int page, int rows)
        {
            try
            {
                return Ok(_unitOfWork.Product.ProductPagedList(page, rows));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                return Ok(_unitOfWork.Product.Insert(product));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Product product)
        {
            try
            {
                if (!ModelState.IsValid && _unitOfWork.Product.Update(product))
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
        public IActionResult Delete([FromBody] Product product)
        {
            try
            {
                if (product.Id > 0)
                {
                    return Ok(_unitOfWork.Product.Delete(product));

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
