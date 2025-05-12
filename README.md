1) I have a scriptableObject for WheelReward (WheelRewardSO), which has data for Reward, wheighted probability and icon
this SO handles reward by itslef, so we could extend it to give us a more complex reward
The probability of all sectors does not have to addup to 100(everything will work anyway)
3) I have separtion of logic and view. All player actions are going through a facade(GameController)
4) Animation is done via Dotween
5) Outcome is decided and applied at the moment of button press everything else is just visual.
(you can theoreticlly restrict visual updates on other widgets like topPanel untill claim button is pressed)
6) For manual testing there is custom Editor for BonusWheel(can test every sector) 
7) 1000_spinstest  tests only logic part
8) To scale to more sectors you would need to add more WheelRewardSO in the BonusWheel.RewardData array
We can have several identical sectors. 
Editor Button Init will automaticlly create icons and position them
9) Everything scales reasonably well but only for Portrait orientation
9)This took a whole a day
10) Performance: 
      a) Everything is in atlas there is minimal amount of drawcalls.
      b) Hierarchy is not deep
      c) Logic will scale without a problem
