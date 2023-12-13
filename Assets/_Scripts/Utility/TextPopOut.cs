using DG.Tweening;
using UnityEngine;

public class TextPopOut : MonoBehaviour
{
    [SerializeField] private Vector3 _originalScale;
    [SerializeField] private Vector3 _scaleTo;
    
    void Start()
    {
        _originalScale = transform.localScale;
        _scaleTo = _originalScale * 1.5f;

        ScaleText();
    }

    private void ScaleText()
    {
        transform.DOScale(_scaleTo, 1.0f)
            .SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {
                transform.DOScale(_originalScale, 1.0f)
                    .SetEase(Ease.InOutSine)
                    .SetDelay(0f)
                    .OnComplete(ScaleText);
            });
    }
}
