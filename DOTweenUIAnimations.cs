using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro; // Thêm thư viện TextMeshPro

public class DOTweenUIAnimations : MonoBehaviour
{
    public RectTransform target; // Đối tượng UI
    public CanvasGroup canvasGroup; // Sử dụng để tween độ trong suốt của UI
    public Image image; // Dùng để đổi màu hoặc alpha cho hình ảnh

    public TextMeshProUGUI effectNameText; // TextMeshPro cho tên hiệu ứng
    public TextMeshProUGUI effectDescriptionText; // TextMeshPro cho giải thích hiệu ứng

    private Vector2 initialPosition; // Lưu vị trí ban đầu
    private Vector3 initialScale; // Lưu kích thước ban đầu
    private float initialAlpha; // Lưu độ trong suốt ban đầu

    private Tweener currentTween; // Biến lưu tween hiện tại

    // Start is called before the first frame update
    void Start()
    {
        // Lưu lại trạng thái ban đầu của RectTransform và các thuộc tính khác
        initialPosition = target.anchoredPosition;
        initialScale = target.localScale;
        if (canvasGroup != null)
        {
            initialAlpha = canvasGroup.alpha;
        }
    }

    // Cập nhật tên hiệu ứng và giải thích
    private void UpdateEffectUI(string effectName, string effectDescription)
    {
        if (effectNameText != null)
        {
            effectNameText.text = effectName; // Cập nhật tên hiệu ứng
        }

        if (effectDescriptionText != null)
        {
            effectDescriptionText.text = effectDescription; // Cập nhật giải thích hiệu ứng
        }
    }

    // Dừng hiệu ứng hiện tại nếu có
    private void KillCurrentTween()
    {
        if (currentTween != null)
        {
            currentTween.Kill(); // Dừng tween cũ
        }
    }

    // Các hiệu ứng animation

    public void Bounce()
    {
        KillCurrentTween(); // Dừng hiệu ứng cũ trước khi chạy hiệu ứng mới
        UpdateEffectUI("Bounce", "Đối tượng nảy lên và xuống.");
        currentTween = target.DOAnchorPos(new Vector2(0, 100f), 1f, false)
              .From(initialPosition)
              .SetEase(Ease.OutBounce)
              .SetLoops(-1, LoopType.Restart); // Lặp vô hạn từ vị trí ban đầu
    }

    public void FadeIn()
    {
        KillCurrentTween(); // Dừng hiệu ứng cũ trước khi chạy hiệu ứng mới
        UpdateEffectUI("Fade In", "Hiệu ứng làm đối tượng hiện ra dần.");
        if (canvasGroup != null)
        {
            currentTween = canvasGroup.DOFade(1f, 1f)
                       .SetLoops(-1, LoopType.Restart); // Lặp vô hạn với alpha
        }
    }

    public void FadeOut()
    {
        KillCurrentTween(); // Dừng hiệu ứng cũ trước khi chạy hiệu ứng mới
        UpdateEffectUI("Fade Out", "Hiệu ứng làm đối tượng biến mất dần.");
        if (canvasGroup != null)
        {
            currentTween = canvasGroup.DOFade(0f, 1f)
                       .SetLoops(-1, LoopType.Restart); // Lặp vô hạn với alpha
        }
    }

    public void SlideIn()
    {
        KillCurrentTween(); // Dừng hiệu ứng cũ trước khi chạy hiệu ứng mới
        UpdateEffectUI("Slide In", "Hiệu ứng trượt đối tượng từ bên trái vào.");
        target.anchoredPosition = new Vector2(-500f, target.anchoredPosition.y); // Set initial position
        currentTween = target.DOAnchorPosX(0f, 1f)
              .SetEase(Ease.OutCubic)
              .SetLoops(-1, LoopType.Restart); // Lặp vô hạn từ vị trí ban đầu
    }

    public void SlideOut()
    {
        KillCurrentTween(); // Dừng hiệu ứng cũ trước khi chạy hiệu ứng mới
        UpdateEffectUI("Slide Out", "Hiệu ứng trượt đối tượng từ bên phải ra.");
        currentTween = target.DOAnchorPosX(500f, 1f)
              .SetEase(Ease.OutCubic)
              .SetLoops(-1, LoopType.Restart); // Lặp vô hạn từ vị trí ban đầu
    }

    public void Rotate()
    {
        KillCurrentTween(); // Dừng hiệu ứng cũ trước khi chạy hiệu ứng mới
        UpdateEffectUI("Rotate", "Hiệu ứng xoay đối tượng quanh trục Z.");
        currentTween = target.DOLocalRotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360)
              .SetEase(Ease.OutElastic)
              .SetLoops(-1, LoopType.Restart); // Lặp vô hạn và quay lại vị trí ban đầu
    }

    public void Scale()
    {
        KillCurrentTween(); // Dừng hiệu ứng cũ trước khi chạy hiệu ứng mới
        UpdateEffectUI("Scale", "Hiệu ứng phóng to/thu nhỏ đối tượng.");
        currentTween = target.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 1f)
              .SetEase(Ease.OutBack)
              .From(initialScale)
              .SetLoops(-1, LoopType.Restart); // Lặp vô hạn và quay lại trạng thái ban đầu
    }

    public void Shake()
    {
        KillCurrentTween(); // Dừng hiệu ứng cũ trước khi chạy hiệu ứng mới
        UpdateEffectUI("Shake", "Hiệu ứng rung đối tượng.");
        currentTween = target.DOShakeAnchorPos(1f, new Vector2(10f, 10f), 10, 90, false, true)
              .SetLoops(-1, LoopType.Restart); // Lặp vô hạn với hiệu ứng shake
    }

    public void ChangeColor()
    {
        KillCurrentTween(); // Dừng hiệu ứng cũ trước khi chạy hiệu ứng mới
        UpdateEffectUI("Change Color", "Hiệu ứng thay đổi màu của đối tượng.");
        if (image != null)
        {
            currentTween = image.DOColor(Color.red, 1f)
                 .SetEase(Ease.Linear)
                 .SetLoops(-1, LoopType.Restart); // Lặp vô hạn và quay lại màu gốc
        }
    }
    public void FlipX()
    {
        KillCurrentTween();
        UpdateEffectUI("Flip X", "Hiệu ứng lật đối tượng theo trục X.");
        target.DOLocalRotate(new Vector3(0, 180f, 0), 1f, RotateMode.LocalAxisAdd)
              .SetEase(Ease.InOutQuad)
              .SetLoops(-1, LoopType.Restart);
    }

    public void FlipY()
    {
        KillCurrentTween();
        UpdateEffectUI("Flip Y", "Hiệu ứng lật đối tượng theo trục Y.");
        target.DOLocalRotate(new Vector3(180f, 0, 0), 1f, RotateMode.LocalAxisAdd)
              .SetEase(Ease.InOutQuad)
              .SetLoops(-1, LoopType.Restart);
    }
    public void Swing()
    {
        KillCurrentTween();
        UpdateEffectUI("Swing", "Hiệu ứng đung đưa đối tượng qua lại.");
        target.DORotate(new Vector3(0, 0, 15f), 0.5f)
              .SetEase(Ease.InOutQuad)
              .SetLoops(-1, LoopType.Yoyo);
    }
    public void BounceIn()
    {
        KillCurrentTween();
        UpdateEffectUI("Bounce In", "Hiệu ứng đối tượng nảy vào khi xuất hiện.");
        target.localScale = Vector3.zero;
        target.DOScale(Vector3.one, 0.6f)
              .SetEase(Ease.OutBounce);
    }
    public void Heartbeat()
    {
        KillCurrentTween();
        UpdateEffectUI("Heartbeat", "Hiệu ứng nhịp tim đập.");
        target.DOScale(new Vector3(1.2f, 1.2f, 1f), 0.2f)
              .SetEase(Ease.OutQuad)
              .SetLoops(-1, LoopType.Yoyo)
              .OnStepComplete(() =>
              {
                  target.DOScale(Vector3.one, 0.2f).SetEase(Ease.InQuad);
              });
    }
    public void RubberBand()
    {
        KillCurrentTween();
        UpdateEffectUI("Rubber Band", "Hiệu ứng co giãn như dây cao su.");
        target.DOScale(new Vector3(1.25f, 0.75f, 1f), 0.2f)
              .SetEase(Ease.OutQuad)
              .SetLoops(1, LoopType.Yoyo)
              .OnComplete(() =>
              {
                  target.DOScale(Vector3.one, 0.2f).SetEase(Ease.InQuad);
              });
    }
    public void LightSpeedOut()
    {
        KillCurrentTween();
        UpdateEffectUI("Light Speed Out", "Hiệu ứng chuyển động nhanh ra khỏi màn hình.");
        target.DOAnchorPosX(500f, 0.3f)
              .SetEase(Ease.InBack)
              .OnComplete(() => target.anchoredPosition = initialPosition);
    }
    public void Spin()
    {
        KillCurrentTween();
        UpdateEffectUI("Spin", "Hiệu ứng xoay liên tục.");
        target.DOLocalRotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360)
              .SetEase(Ease.Linear)
              .SetLoops(-1, LoopType.Incremental);
    }

}
