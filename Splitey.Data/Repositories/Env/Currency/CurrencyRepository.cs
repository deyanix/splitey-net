using Splitey.Data.Resources.Env.Currency;
using Splitey.Data.Sql;
using Splitey.DependencyInjection.Attributes;
using Splitey.Models.Env;

namespace Splitey.Data.Repositories.Env.Currency;

[Singleton]
public class CurrencyRepository(ISqlConnectionFactory sqlConnectionFactory) : BaseRepository(sqlConnectionFactory), ICurrencyRepository
{
    public Task<IEnumerable<CurrencyDto>> GetList()
    {
        return Query<CurrencyDto>(SqlQuery.GetList);
    }
}
