using Microsoft.AspNetCore.Mvc;
using Rest.Application.Entities;
using Rest.Application.Repositories;

namespace Rest.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    public ProductController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public IActionResult GetAll(CancellationToken cancellationToken)
    {
        return Ok(_unitOfWork.ProductRepository.GetAll(cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(int id, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetById(new (id), cancellationToken);

        if (product != null)
        {
            return Ok(product);
        }
        else
        {
            return Ok(new Product()
            {
                Id = new(12),
                Name = "TEST",
                Price = 4.99m,
                Brand = new Brand()
                {
                    Id = new(22),
                    Name = "ARG",
                    Company = new Id<Company>(123)
                }
            });

            return NotFound();
        }
    }

    private readonly IUnitOfWork _unitOfWork;
}
