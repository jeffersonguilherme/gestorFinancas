using Dima.Api.Data;
using Dima.Core.Common.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

public class TransactionHandler(AppDbContext context) : ITransactionHandler
{
    public async Task<Response<Transaction>> CreateAsync(CreateTransactionRequest request)
    {
        try
        {
            var transaction = new Transaction
            {
                UserId = request.UserId,
                CategoryId = request.CategoryId,
                Amount = request.Amount,
                PaidOrReceivedAt = request.PaidOrReceivedAt,
                Title = request.Title,
                Type = request.Type,

            };

            await context.Transactions.AddAsync(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transaction, 201, message: "Transação criada");
        } catch {
            return new Response<Transaction?>(null, 500, "Não foi possível crirar sua transação");
        }
        
    }

    public async Task<Response<Transaction>> DeleteAsync(DeleteTransactionRequest request)
    {
        try
        {
            var transaction = await context.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            if (transaction is null)
                return new Response<Transaction?>(null, 404, "Transação não encontrada");

            context.Transactions.Remove(transaction);
            await context.SaveChangesAsync();
            
            return new Response<Transaction>(transaction);
        }
        catch
        {
            return new Response<Transaction?>(null, 500, "Transação não encontrada");
        }
        
    }

    public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
    {
        try
        {
            var transaction = await context.Transactions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            if (transaction is null)
                return new Response<Transaction?>(null, 404, "Transação não encontrada");

            return new Response<Transaction?>(transaction);
        }
        catch
        {
            return new Response<Transaction?>(null, 500, "Transação não encontrada");
        }
        
    }

    public async Task<Response<List<Transaction>>> GetPeriodAsync(GetTransactionsByPeriodRequest request)
    {
        try
        {
            request.StarDate ??= DateTime.Now.GetFirstDay();
            request.EndDate ??= DateTime.Now.GetLastDay();
        }
        catch
        {
            return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possível determinar a data de início e termino");
        }

        try
        {
            var query = context
           .Transactions
           .AsNoTracking()
           .Where(x => x.CreatedAt >= request.StarDate && x.CreatedAt <= request.EndDate && x.UserId == request.UserId)
           .OrderBy(x => x.CreatedAt);

            var transactions = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<Transaction>>(
                transactions,
                count,
                request.PageNumber,
                request.PageSize
            );
        }
        catch
        {
            return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possível obter as transações");
        }
    }

    public async Task<Response<Transaction>> UpdateAsync(UpdateTransactionRequest request)
    {
        try
        {
            var transaction = await context.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            if (transaction is null)
                return new Response<Transaction?>(null, 404, "transação não encontrada");

            transaction.CategoryId = request.CategoryId;
            transaction.Amount = request.Amount;
            transaction.Title = request.Title;
            transaction.Type = request.Type;
            transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;

            context.Transactions.Update(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction>(transaction);
        }
        catch
        {
            return new Response<Transaction>(null, 500, "Não foi possível retorna a transação");
        }
        
        
    }
}
