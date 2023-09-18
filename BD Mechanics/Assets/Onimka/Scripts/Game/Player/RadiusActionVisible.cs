using UnityEngine;
using DG.Tweening;

public class RadiusActionVisible : MonoBehaviour
{
    [HideInInspector] public bool isDisable;
    public void EnableVisible(float newRadius)
    {
        Vector3 newScale = new Vector3(newRadius * 2, transform.localScale.y, newRadius * 2);
        isDisable = false;
        transform.localScale = new Vector3(0, transform.localScale.y, 0) ;
        gameObject.SetActive(true);
        transform.DOKill();
        DOTween.Sequence().AppendInterval(0.2f).
                        Append(transform.DOScale(newScale, 0.5f).SetEase(Ease.OutQuad));
    }

    public void DisableVisible()
    {
        Vector3 newScale = new Vector3(0, transform.localScale.y, 0);
        isDisable = true;
        transform.DOKill();
        DOTween.Sequence().AppendInterval(0.2f).
                        Append(transform.DOScale(newScale, 0.5f).SetEase(Ease.OutQuad)).OnComplete(() => transform.gameObject.SetActive(false));
    }
}
