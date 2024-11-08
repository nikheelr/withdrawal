using Application.Services;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Requests;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BankAccountController : ControllerBase
{
    private readonly IBankAccountService _bankAccountService;
    private readonly ILogger<BankAccountController> _logger;

    public BankAccountController(ILogger<BankAccountController> logger, IBankAccountService bankAccountService)
    {
        _logger = logger;
        _bankAccountService = bankAccountService;
    }

    [HttpPost("withdraw")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Withdraw([FromForm] WithdrawRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        try
        {
            await _bankAccountService.Withdraw(request.AccountId, request.Amount);
            return Ok("Withdrawal successful");

        }
        catch (AccountNotFoundException e)
        {
            _logger.LogWarning("No account found for {0}", request.AccountId);
            return NotFound();
        }
        catch (InsufficientFundsException e)
        {
            _logger.LogWarning("Insufficient Funds for {0}", request.AccountId);
            return BadRequest();
        }
    }
}