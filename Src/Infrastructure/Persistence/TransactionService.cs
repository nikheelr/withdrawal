using Application.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence;

public class TransactionService : ITransactionService
{
    private readonly ILogger<TransactionService> _logger;
    private readonly BankContext _context;

    public TransactionService(ILogger<TransactionService> logger, BankContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    public async Task Execute(Func<Task> task)
    {
        var executionStrategy = _context.Database.CreateExecutionStrategy();
        await executionStrategy.ExecuteAsync(async () => await ExecuteTransaction(task));
    }

    private async Task ExecuteTransaction(Func<Task> task)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            await task();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            _logger.LogError("{message}", e.Message);
        }
    }
}
