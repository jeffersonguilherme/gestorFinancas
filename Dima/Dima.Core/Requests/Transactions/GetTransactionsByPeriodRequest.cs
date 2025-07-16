namespace Dima.Core.Requests.Transactions;

public class GetTransactionsByPeriodRequest : PagedRequest
{
    public DateTime? StarDate { get; set; }
    public DateTime? EndDate { get; set; }
}