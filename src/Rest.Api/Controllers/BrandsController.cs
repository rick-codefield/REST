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
    public async Task<ActionResult<Brand[]>> GetAll([FromHeader(Name = "Continuation-Token")] byte[]? rawContinuationToken, CancellationToken cancellationToken)
    {
        var continuationToken = rawContinuationToken != null ? ContinuationToken.Deserialize(rawContinuationToken) : null;
        var pagedResult = await _unitOfWork.BrandRepository.GetPaged(continuationToken, cancellationToken);

        Response.Headers.Add(pagedResult);

        return Ok(pagedResult.Data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Brand>> GetById([FromRoute] Id<Brand> id, CancellationToken cancellationToken)
    {
        var brand = await _unitOfWork.BrandRepository.GetById(id, cancellationToken);

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
