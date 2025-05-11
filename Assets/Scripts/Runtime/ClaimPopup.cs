using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Threading.Tasks;
using UnityEngine;

public class ClaimPopup : MonoBehaviour
{
    public float animationDuration = 0.5f;

    public float shineSpeed = 30;

    [SerializeField] private Transform rewardPosition;
    [SerializeField] private Transform shine1, shine2;
    private Tween shineTween1, shineTween2;

    private Transform originalParent;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Vector2 originalSize;
    private Tween moveTween;
    private Tween rotationTween;
    private Tween scaleTween;

    private Transform rewardTransform;

    private TaskCompletionSource<object> tcs;

    private const float desiredSize = 300; 


    private void Awake()
    {
        shineTween1 = shine1.DORotate(new Vector3(0, 0, 360 * 5), shineSpeed, RotateMode.FastBeyond360)
            .SetSpeedBased()
            .SetEase(Ease.Linear)
            .SetLoops(-1)
            .Pause();

        shineTween2 = shine2.DORotate(new Vector3(0, 0, -360 * 5), shineSpeed, RotateMode.FastBeyond360)
            .SetSpeedBased()
            .SetEase(Ease.Linear)
            .SetLoops(-1)
            .Pause();
    }


    private void SaveState()
    {
        originalParent = rewardTransform.parent;
        originalPosition = rewardTransform.position;
        originalRotation = rewardTransform.rotation;
        originalSize = (rewardTransform as RectTransform).sizeDelta;
    } 
    private void LoadState()
    {
        rewardTransform.SetParent(originalParent);
        rewardTransform.SetPositionAndRotation(originalPosition, originalRotation);
        (rewardTransform as RectTransform).sizeDelta = originalSize;
    }


    public Task Show(Transform reward)
    {
        gameObject.SetActive(true);
        tcs = new TaskCompletionSource<object>();

        rewardTransform = reward;
        SaveState();

        shineTween1.Play();
        shineTween2.Play();

        reward.SetParent(transform);
        moveTween = reward.DORotate(Vector3.zero, animationDuration).SetEase(Ease.OutBack);
        rotationTween = reward.DOMove(rewardPosition.position, animationDuration).SetEase(Ease.OutBack);
        scaleTween = (reward as RectTransform).DOSizeDelta (Vector3.one * desiredSize, animationDuration).SetEase(Ease.OutBack);
        return tcs.Task;
    }

    public void ClaimReward()
    {
        moveTween.Kill();
        rotationTween.Kill();
        scaleTween.Kill();

        shineTween1.Pause();
        shineTween2.Pause();

        LoadState();
        gameObject.SetActive(false);
        tcs.SetResult(null);
    }

}
