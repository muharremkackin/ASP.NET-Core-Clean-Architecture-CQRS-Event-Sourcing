using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Commands;
using Shop.Application.Customer.Queries;
using Shop.Application.Customer.Responses;
using Shop.Domain.QueriesModel;

namespace Shop.PublicApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
        => _mediator = mediator;

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Result<CreatedCustomerResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CreatedCustomerResponse>> Create([FromBody] CreateCustomerCommand command)
        => (await _mediator.Send(command)).ToActionResult(this);

    [HttpGet("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Result<CustomerQueryModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CustomerQueryModel>> GetById(Guid id)
        => (await _mediator.Send(new GetCustomerByIdQuery(id))).ToActionResult(this);
}