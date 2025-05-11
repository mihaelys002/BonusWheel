using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameController : MonoBehaviour, IGameController
{
    public ICurrencyRepository CurrencyRepository => currencyRepository;

    [SerializeField] private CurrencyPlayerPrefs currencyRepository;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        currencyRepository = new CurrencyPlayerPrefs();
        // For lack of proper DI, we are using a singleton pattern
        IGameController.Instance = this;
    }

    public async Task<int> SpinBonusWheel(List<WheelRewardSO> options, int optionToWin = -1)
    {
        //if option to win is not specified
        if (optionToWin < 0 || optionToWin >= options.Count)
        {
            float totalProbability = 0;
            options.ForEach(x => totalProbability += x.WeightedProbability);
            float randomResult = Random.Range(0, totalProbability);

            for (int i = 0; i < options.Count; i++)
            {
                if (randomResult < options[i].WeightedProbability)
                {
                    optionToWin = i;
                    break;
                }
                //In case someone set negative probability
                randomResult -= Mathf.Max(0,options[i].WeightedProbability);
            }
        }

        await options[optionToWin].GiveReward(CurrencyRepository);

        return optionToWin;
    }







    //These are dummy functions
    public void BuyItem(int itemId) => throw new System.NotImplementedException();

    public void ExitToMainMenu() => throw new System.NotImplementedException();

    public void ShowRewardedAd() => throw new System.NotImplementedException();

    public void StartMission(int missionNum) => throw new System.NotImplementedException();
}
