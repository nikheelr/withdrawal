namespace Application.Repository;

public interface ITransactionService
{
    /// <summary>
    /// Executes the task in a transaction and rollbacks if any errors occur 
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
    Task Execute(Func<Task> task);
}