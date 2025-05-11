1) I have a scriptableObject for WheelReward (WheelRewardSO) that has data for Reward, wheighted probability and icon
2) I have separtion of logic and view. All player actions are going through a facade(GameController)
3) Animation is done via Dotween
4) Outcome is decided at the moment of button press everything else is just visual.
(you can theoreticlly restrict visual updates on other widgets like topPanel untill claim button is pressed)
5) For manual testing there is custom Editor for BonusWheel(can test every sector) 
6) 1000_spinstest  tests only logic part
7) To scale to more sectors you would need to add more WheelRewardSO in the BonusWheel.RewardData array
Editor Button Init will automaticlly create icons and position them
8) Everything scales reasonably well but only for Portrait orientation
9)This took a whole a day     
