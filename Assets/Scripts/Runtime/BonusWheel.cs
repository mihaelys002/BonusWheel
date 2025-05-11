using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BonusWheel : MonoBehaviour
{
    public List<WheelRewardSO> RewardData;

    public float SpinSpeed = 30f;

    [SerializeField]
    private GameObject wheelRewardPrefab;
    [SerializeField, HideInInspector]
    private WheelReward[] wheelRewards;
    [SerializeField]
    private Transform rotationRoot;
    [SerializeField]
    private Button spinButton;
    private const float rewardDistance = 340f;
    private const float angleHalfOffset = 0.5f;
    private Tween spinTween;


    [SerializeField] private ClaimPopup claimPopup;

    private float AngleOfOption(int option)
    {
        return -360f / RewardData.Count * (option + angleHalfOffset);
    }


    public void Init()
    {
        if (wheelRewards != null)
            foreach (var reward in wheelRewards)
                if (reward != null)
#if UNITY_EDITOR
                    GameObject.DestroyImmediate(reward.gameObject);
#else
                GameObject.Destroy(reward.gameObject);
#endif

        // Initialize the wheel rewards
        wheelRewards = new WheelReward[RewardData.Count];
        for (int i = 0; i < RewardData.Count; i++)
        {
            GameObject rewardObject = Instantiate(wheelRewardPrefab, rotationRoot);
            WheelReward wheelReward = rewardObject.GetComponent<WheelReward>();
            wheelReward.Initialize(RewardData[i]);
            wheelRewards[i] = wheelReward;
            float angle = AngleOfOption(i);
            Vector3 position = Quaternion.AngleAxis(angle, Vector3.forward) * new Vector3(0, rewardDistance, 0);
            (wheelRewards[i].transform as RectTransform).anchoredPosition = position;
            wheelReward.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public async void OnSpinButtonPressed(int option = -1)
    {
        spinButton.gameObject.SetActive(false);
        try
        {
            await SpinWheelAsync(option);
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex);
        }
        finally
        {
            spinButton.gameObject.SetActive(true);
        }
    }




    public async Task SpinWheelAsync(int option  = -1)
    {
        Task<int> logicSpin = IGameController.Instance.SpinBonusWheel(RewardData, option);

        spinTween = rotationRoot.DORotate(new Vector3(0, 0, -360 * 5), SpinSpeed, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear).SetLoops(-1).SetSpeedBased();
        await logicSpin;
        spinTween.Kill();
        option = logicSpin.Result;
        float angleWidth = 360f/ RewardData.Count;
        float desiredAngle = AngleOfOption(option) + Random.Range(-angleWidth/2, angleWidth/2);
        //angle should be negative because we are rotating the root
        desiredAngle = -desiredAngle;
        //guarnteed extra spin
        desiredAngle -= 360 * 3;

        spinTween = rotationRoot.DORotate(new Vector3(0, 0, desiredAngle), SpinSpeed, RotateMode.FastBeyond360)
            .SetEase(Ease.OutCubic).SetSpeedBased();
        await spinTween.AsyncWaitForCompletion();
        gameObject.SetActive(false);
        await claimPopup.Show(wheelRewards[option].transform);
        gameObject.SetActive(true);
    }

}

