using System.Transactions;

namespace Splitey.Data.Transaction;

public class TransactionBuilder
{
    public static TransactionScope Default => new TransactionBuilder().Transaction;
    
    private IsolationLevel _isolationLevel = IsolationLevel.ReadCommitted;
    private TimeSpan _timeout = TimeSpan.FromSeconds(30);
    private TransactionScopeOption _scopeOption = TransactionScopeOption.Required;
    private TransactionScopeAsyncFlowOption _asyncFlow = TransactionScopeAsyncFlowOption.Enabled;

    public TransactionScope Transaction
    {
        get
        {
            var options = new TransactionOptions()
            {
                IsolationLevel = _isolationLevel, 
                Timeout = _timeout,
            };
            
            return new TransactionScope(_scopeOption, options, _asyncFlow);
        }
    }
    
    public TransactionBuilder Isolation(IsolationLevel isolationLevel)
    {
        _isolationLevel = isolationLevel;
        return this;
    }
    
    public TransactionBuilder Timeout(TimeSpan timeout)
    {
        _timeout = timeout;
        return this;
    }
    
    public TransactionBuilder Scope(TransactionScopeOption scopeOption)
    {
        _scopeOption = scopeOption;
        return this;
    }
    
    public TransactionBuilder Async(TransactionScopeAsyncFlowOption asyncFlow)
    {
        _asyncFlow = asyncFlow;
        return this;
    }
}