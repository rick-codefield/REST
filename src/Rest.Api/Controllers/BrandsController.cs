using Microsoft.AspNetCore.Mvc;
using Rest.Application.Entities;
using Rest.Application.Repositories;
using Rest.Infrastructure.Repositories;

namespace Rest.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandsController : ControllerBase
{
    public BrandsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<Brand[]>> GetAll(BrandExpansion? expand, [FromHeader(Name = "Continuation-Token")] IContinuationToken? continuationToken, CancellationToken cancellationToken)
    {
        var pagedResult = await _unitOfWork.BrandRepository.GetPaged(
            continuationToken: continuationToken,
            expansion: expand,
            cancellationToken: cancellationToken);

        Response.Headers.Add(pagedResult);

        return Ok(pagedResult.Data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Brand>> GetById(Id<Brand> id, BrandExpansion? expand, CancellationToken cancellationToken)
    {
        var brand = await _unitOfWork.BrandRepository.GetById(id, expand, cancellationToken);

        if (brand != null)
        {
            return Ok(brand);
        }
        else
        {
            return NotFound();
        }
    }

    private readonly IUnitOfWork _unitOfWork;
}
