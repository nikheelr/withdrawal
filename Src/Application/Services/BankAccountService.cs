using Application.Notifications;
using Application.Repository;
using Domain.Enums;
using Domain.Events;
using Domain.Exceptions;
using Domain.Models;

namespace Application.Services;

public class BankAccountService : IBankAccountService
{
    private readonly IBankAccountRepository _bankAccountRepository;
    private readonly IBankAccountAuditRepository _bankAccountAuditRepository;
    private readonly INotificationService _notificationService;
    private readonly ITransactionService _transactionService;

    public BankAccountService(IBankAccountRepository bankAccountRepository, IBankAccountAuditRepository bankAccountAuditRepository, INotificationService notificationService, ITransactionService transactionService)
    {
        _bankAccountRepository = bankAccountRepository;
        _bankAccountAuditRepository = bankAccountAuditRepository;
        _notificationService = notificationService;
        _transactionService = transactionService;
    }
    
    public async Task Withdraw(long accountNumber, decimal amount)
    {
        var account = await _bankAccountRepository.GetAccount(accountNumber);

        if (account is null)
        {
            await _bankAccountAuditRepository.Add(new BankAccountAuditLogDTO(accountNumber, amount, ActionType.Withdrawal,Status.Failure,"Account not found"));
            throw new AccountNotFoundException(accountNumber);
        }

        if (amount > account.Balance)
        {
            await _bankAccountAuditRepository.Add(new BankAccountAuditLogDTO(accountNumber, amount, ActionType.Withdrawal,Status.Failure,"Insufficient funds"));
            throw new InsufficientFundsException(account.Balance);
        }

        await _transactionService.Execute(async () =>
        {
            var newBalance = account.Balance - amount;
            await _bankAccountRepository.UpdateBalance(accountNumber, newBalance);

            await _bankAccountAuditRepository.Add(new BankAccountAuditLogDTO(accountNumber, amount, ActionType.Withdrawal,Status.Success,"Withdrawal successful"));
        });

        await PublishWithdrawalNotification(accountNumber, amount);
    }

    private async Task PublishWithdrawalNotification(long accountNumber, decimal amount)
    {
        try
        {
            var withdrawalEvent = new WithdrawalEvent(amount, accountNumber, "Success");
            await _notificationService.Publish(withdrawalEvent);
        
            await _bankAccountAuditRepository.Add(new BankAccountAuditLogDTO(accountNumber, amount, ActionType.PublishEvent,Status.Success,"Published Withdrawal successful event"));
        }
        catch (NotificationException e)
        {
            await _bankAccountAuditRepository.Add(new BankAccountAuditLogDTO(accountNumber, amount, ActionType.PublishEvent,Status.Failure,"Failed to published Withdrawal successful event"));
        }
    }
}