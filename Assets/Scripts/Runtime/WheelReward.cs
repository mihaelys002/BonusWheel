using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WheelReward : MonoBehaviour
{

    public Image Icon;
    public TextMeshProUGUI AmountText;
    private const string amountFormat = "x{0}"; // Format for the amount text
    private const string lifeFormat = "{0}\n  min"; // Format for the amount text

    public void Initialize(WheelRewardSO rewardData)
    {
        // Set the icon and amount text based on the reward data
        Icon.sprite = rewardData.Icon;
        if (rewardData.RewardCurrency == Currency.Life)
            AmountText.text = string.Format(lifeFormat, rewardData.Amount);
        else
            AmountText.text = string.Format(amountFormat, rewardData.Amount);


    }

}
