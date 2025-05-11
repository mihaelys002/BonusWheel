using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "WheelReward", menuName = "Scriptable Objects/WheelReward")]
public class WheelRewardSO : ScriptableObject
{
    public float WeightedProbability;

    public int Amount;
    public Currency RewardCurrency;
    public Sprite Icon;


    /// <summary>
    /// This pattern allows us to extend wheel reward in future with things that are not currency
    /// or do smh not obvious (we would have to extend this class or add interface)
    /// </summary>
    /// <param name="currencyRepository"></param>
    public async Task GiveReward(ICurrencyRepository currencyRepository)
    {
        await currencyRepository.AddAsync(RewardCurrency, Amount);
    }
}


