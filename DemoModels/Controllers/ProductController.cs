using DemoModels.Dto;
using DemoModels.Infrastrucutre;
using Microsoft.AspNetCore.Mvc;
using Models.Commands;
using Models.Entities;
using Models.Queries;
using System.Net;
using Tools.CQRS;

using CQRS = Tools.CQRS.Commands;

namespace DemoModels.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IDisptacher _disptacher;

        public ProductController(IDisptacher disptacher)
        {
            _disptacher = disptacher;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_disptacher.Dispatch(new GetProductsQuery()).ToList());
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Product? product = _disptacher.Dispatch(new GetProductQuery(id));

            if(product is null)
                return NotFound();

            return Ok(product);
        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateProductDto dto)
        {
            CQRS.IResult result = _disptacher.Dispatch(new CreateProductCommand(dto.Name, dto.Price));

            if(result.IsSuccess)
                return NoContent();

            return BadRequest(new { result.Message });
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public CallResult Put(int id, [FromBody] UpdateProductDto dto)
        {
            CQRS.IResult result = _disptacher.Dispatch(new UpdateProductCommand(id, dto.Price));

            if (result.IsSuccess)
                return CallResult.Success();

            return CallResult.Failure(HttpStatusCode.BadRequest, result.Message!, new { Id = id, dto.Price });
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            CQRS.IResult result = _disptacher.Dispatch(new DeleteProductCommand(id));

            if (result.IsSuccess)
                return NoContent();

            return BadRequest(new { result.Message, Data = new { Id = id }});
        }
    }
}
