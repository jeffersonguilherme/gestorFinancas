
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;

namespace Dima.Core.Handlers;

public interface ITransactionHandler
{
    Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request);
    Task<Response<List<Transaction>>> GetPeriodAsync(GetTransactionsByPeriodRequest request);
    Task<Response<Transaction>> CreateAsync(CreateTransactionRequest request);
    Task<Response<Transaction>> UpdateAsync(UpdateTransactionRequest request);
    Task<Response<Transaction>> DeleteAsync(DeleteTransactionRequest request);
}