using Microsoft.AspNetCore.Mvc;
using Rest.Application.Entities;
using Rest.Application.Repositories;
using Rest.Infrastructure.Repositories;

namespace Rest.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    public ProductsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Product[]>> GetAll([FromHeader(Name = "Continuation-Token")] IContinuationToken? continuationToken, ProductExpansion? expand, CancellationToken cancellationToken)
    {
        var pagedResult = await _unitOfWork.ProductRepository.GetPaged(
            continuationToken: continuationToken,
            expansion: expand,
            cancellationToken: cancellationToken);

        Response.Headers.Add(pagedResult);

        return Ok(pagedResult.Data);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> GetById(Id<Product> id, ProductExpansion? expand, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetById(id, expand, cancellationToken);

        if (product != null)
        {
            return Ok(product);
        }
        else
        {
            return NotFound();
        }
    }

    private readonly IUnitOfWork _unitOfWork;
}
