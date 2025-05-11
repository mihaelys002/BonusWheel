using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TopPanel : MonoBehaviour
{
    [SerializeField]
    private List<TopPanelCurrencyCount> currencyIcons;
    public ICurrencyRepository currencyRepository;

    private bool canShowCurrencyUpdates = true;

    public void ShowCurrencyUpdates(bool shouldShow)
    {
        if (canShowCurrencyUpdates == false && shouldShow == true)
        {
            canShowCurrencyUpdates = shouldShow;
            foreach (Currency currency in Enum.GetValues(typeof(Currency)))
            {
                int balance  = currencyRepository.GetBalanceAsync(currency).Result;
                OnCurrencyChanged(currency, balance);
            }
        }
        canShowCurrencyUpdates = shouldShow;
    }


    public void OnEnable()
    {
        currencyRepository.OnCurrencyChanged += OnCurrencyChanged;
    }

    private void OnCurrencyChanged(Currency currency, int count)
    {
        if (canShowCurrencyUpdates == false)
            return;

        var currencyIcon = currencyIcons.Find(x => x.Currency == currency);
        if (currencyIcon != null)
            currencyIcon.Amount = count;
    }

    public void OnDisable()
    {
        currencyRepository.OnCurrencyChanged -= OnCurrencyChanged;
    }
}
