using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

//This is supposed to be a Facade for all action player can do.
//Like every button in the game(except navigation) should be called from here.
//usually it has less than 15 functions and works as a bridge between UI and game logic
public interface IGameController
{
    //for lack of proper DI, we are using a singleton pattern
    public static IGameController Instance { get; set; } = null;

    public ICurrencyRepository CurrencyRepository { get;}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="optionToWin">-1 is random </param>
    /// <returns>option number that player has won</returns>
    Task<int> SpinBonusWheel(List<WheelRewardSO> options, int optionToWin = -1);

    //dummy function to show that this is a facade
    public void StartMission(int missionNum);
    public void ExitToMainMenu();
    public void BuyItem(int itemId);
    public void ShowRewardedAd();
}
