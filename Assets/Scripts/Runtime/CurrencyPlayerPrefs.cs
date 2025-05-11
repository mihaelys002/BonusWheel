using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CurrencyPlayerPrefs :  ICurrencyRepository
{
    public event System.Action<Currency, int> OnCurrencyChanged;
    public Task AddAsync(Currency currency, int amount)
    {
        int currentAmount = PlayerPrefs.GetInt(currency.ToString(),0);
        currentAmount += amount;
        PlayerPrefs.SetInt(currency.ToString(), currentAmount);
        OnCurrencyChanged?.Invoke(currency, currentAmount);
        return Task.CompletedTask;
    }

    public Task<int> GetBalanceAsync(Currency currency)
    {
        return Task.FromResult(PlayerPrefs.GetInt(currency.ToString(), 0));
    }

    public Task InitializeAsync()
    {
        //nothing to do here
        return Task.CompletedTask;
    }

    public Task<bool> TrySpendAsync(Currency currency, int amount)
    {
        throw new System.NotImplementedException();
    }
}
