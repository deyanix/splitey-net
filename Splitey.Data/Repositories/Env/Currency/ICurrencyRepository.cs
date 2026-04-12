using Splitey.Models.Env;

namespace Splitey.Data.Repositories.Env.Currency;

public interface ICurrencyRepository
{
    Task<IEnumerable<CurrencyDto>> GetList();
}
