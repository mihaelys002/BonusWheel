using TMPro;
using UnityEngine;

public class TopPanelCurrencyCount : MonoBehaviour
{
    public Currency Currency;

    public TextMeshProUGUI currencyText;
    private int amount;
    public int Amount
    {
        get => amount;
        set
        {
            amount = value;
            currencyText.text = amount.ToString();
        }
    }

}
