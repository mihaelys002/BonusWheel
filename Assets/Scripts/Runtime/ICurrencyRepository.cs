using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface ICurrencyRepository
{
    //This probably should be done differently so that UI doesn't have to have reference to this(like centralized event system)
    event System.Action<Currency, int> OnCurrencyChanged;
    Task InitializeAsync();
    Task<int> GetBalanceAsync(Currency currency);
    Task AddAsync(Currency currency, int amount);
    Task<bool> TrySpendAsync(Currency currency, int amount);
}

public enum Currency
{
    Brush,
    Coin,
    Gem,
    Hammer,
    Life,
}
