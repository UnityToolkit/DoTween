using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// DOTweenUIAnimations - Manage and showcase various UI animations.
/// </summary>
public class DOTweenUIAnimations : MonoBehaviour
{
    [Header("UI Components")]
    [Tooltip("The RectTransform target for animations.")]
    public RectTransform target;

    [Tooltip("CanvasGroup for fade animations.")]
    public CanvasGroup canvasGroup;

    [Tooltip("Image component for color animations.")]
    public Image image;

    [Header("Effect Descriptions")]
    [Tooltip("Text to display the effect name.")]
    public TextMeshProUGUI effectNameText;

    [Tooltip("Text to display the effect description.")]
    public TextMeshProUGUI effectDescriptionText;

    private Vector2 initialPosition;
    private Vector3 initialScale;
    private float initialAlpha;

    private Tweener currentTween;

    private void Awake()
    {
        initialPosition = target.anchoredPosition;
        initialScale = target.localScale;
        initialAlpha = canvasGroup != null ? canvasGroup.alpha : 1f;
    }

    /// <summary>
    /// Resets the animation state and updates the effect UI.
    /// </summary>
    private void InitializeEffect(string effectName, string effectDescription)
    {
        currentTween?.Kill();
        UpdateEffectUI(effectName, effectDescription);
        ResetTargetState();
    }

    private void UpdateEffectUI(string effectName, string effectDescription)
    {
        if (effectNameText != null) effectNameText.text = effectName;
        if (effectDescriptionText != null) effectDescriptionText.text = effectDescription;
    }

    private void ResetTargetState()
    {
        target.anchoredPosition = initialPosition;
        target.localScale = initialScale;
        if (canvasGroup != null) canvasGroup.alpha = initialAlpha;
    }

    // Animation methods
    public void Bounce() => StartAnimation("Bounce", "The target bounces up and down.",
        () => target.DOAnchorPosY(initialPosition.y + 100f, 1f).SetEase(Ease.OutBounce).SetLoops(-1, LoopType.Yoyo));

    public void FadeIn() => StartAnimation("Fade In", "Gradually appears.",
        () => canvasGroup?.DOFade(1f, 1f).SetLoops(-1, LoopType.Yoyo));

    public void FadeOut() => StartAnimation("Fade Out", "Gradually disappears.",
        () => canvasGroup?.DOFade(0f, 1f).SetLoops(-1, LoopType.Yoyo));

    public void Rotate() => StartAnimation("Rotate", "Rotates around Z-axis.",
        () => target.DOLocalRotate(Vector3.forward * 360f, 1f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental));

    public void Scale() => StartAnimation("Scale", "Scales up and down.",
        () => target.DOScale(initialScale * 1.5f, 1f).SetEase(Ease.InOutBack).SetLoops(-1, LoopType.Yoyo));

    public void Shake() => StartAnimation("Shake", "Shakes the target.",
        () => target.DOShakeAnchorPos(1f, 10f, 10, 90, false, true).SetLoops(-1, LoopType.Yoyo));

    public void ChangeColor() => StartAnimation("Change Color", "Changes color to red.",
        () => image?.DOColor(Color.red, 1f).SetLoops(-1, LoopType.Yoyo));

    public void FlipX() => StartAnimation("Flip X", "Flips the target along the X-axis.",
        () => target.DOLocalRotate(new Vector3(0, 180f, 0), 1f, RotateMode.LocalAxisAdd)
            .SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Restart));

    public void FlipY() => StartAnimation("Flip Y", "Flips the target along the Y-axis.",
        () => target.DOLocalRotate(new Vector3(180f, 0, 0), 1f, RotateMode.LocalAxisAdd)
            .SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Restart));

    public void Swing() => StartAnimation("Swing", "Swings the target back and forth.",
        () => target.DORotate(new Vector3(0, 0, 15f), 0.5f)
            .SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo));

    public void Heartbeat() => StartAnimation("Heartbeat", "Pulses like a heartbeat.",
        () => target.DOScale(initialScale * 1.2f, 0.2f)
            .SetEase(Ease.OutQuad).SetLoops(-1, LoopType.Yoyo)
            .OnStepComplete(() => target.DOScale(initialScale, 0.2f).SetEase(Ease.InQuad)));

    public void RubberBand() => StartAnimation("Rubber Band", "Stretches and compresses like a rubber band.",
        () => target.DOScale(new Vector3(1.25f, 0.75f, 1f), 0.2f)
            .SetEase(Ease.OutQuad).SetLoops(1, LoopType.Yoyo)
            .OnComplete(() => target.DOScale(initialScale, 0.2f).SetEase(Ease.InQuad)));

    public void LightSpeedOut() => StartAnimation("Light Speed Out", "Quickly moves out of view.",
        () => target.DOAnchorPosX(500f, 0.3f).SetEase(Ease.InBack)
            .OnComplete(() => target.anchoredPosition = initialPosition));

    public void Spin() => StartAnimation("Spin", "Continuously spins around the Z-axis.",
        () => target.DOLocalRotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental));

    private void StartAnimation(string name, string description, System.Func<Tweener> animation)
    {
        InitializeEffect(name, description);
        if (animation != null) currentTween = animation.Invoke();
    }
}
