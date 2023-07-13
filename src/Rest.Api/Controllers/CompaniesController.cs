using Microsoft.AspNetCore.Mvc;
using Rest.Application.Entities;
using Rest.Application.Repositories;
using Rest.Infrastructure.Repositories;

namespace Rest.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompaniesController : ControllerBase
{
    public CompaniesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<Company[]>> GetAll([FromHeader(Name = "Continuation-Token")] byte[]? rawContinuationToken, CancellationToken cancellationToken)
    {
        var continuationToken = rawContinuationToken != null ? ContinuationToken.Deserialize(rawContinuationToken) : null;
        var pagedResult = await _unitOfWork.CompanyRepository.GetPaged(continuationToken, cancellationToken);

        Response.Headers.Add(pagedResult);

        return Ok(pagedResult.Data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Company>> GetById([FromRoute] Id<Company> id, CancellationToken cancellationToken)
    {
        var company = await _unitOfWork.CompanyRepository.GetById(id, cancellationToken);

        if (company != null)
        {
            return Ok(company);
        }
        else
        {
            return NotFound();
        }
    }

    private readonly IUnitOfWork _unitOfWork;
}
