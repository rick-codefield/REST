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
    public async Task<ActionResult<Product[]>> GetAll([FromHeader(Name = "Continuation-Token")] byte[]? rawContinuationToken, CancellationToken cancellationToken)
    {
        var continuationToken = rawContinuationToken != null ? ContinuationToken.Deserialize(rawContinuationToken) : null;
        var pagedResult = await _unitOfWork.ProductRepository.GetPaged(continuationToken, cancellationToken);

        Response.Headers.Add(pagedResult);

        return Ok(pagedResult.Data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById([FromRoute] Id<Product> id, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetById(id, cancellationToken);

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
