using DG.Tweening;
using TMPro;
using UnityEngine;

public class HitCounter : MonoBehaviour
{
    public TextMeshProUGUI counterText;

    public void Initialise(float damage, float travelDistance)
    {
        counterText.text = Mathf.RoundToInt(damage).ToString();
        Movement(travelDistance);
    }

    public void Movement(float travelDistance)
    {
        transform.DOMove(transform.position + Vector3.up * travelDistance, 1f);
        counterText.DOFade(0, 0.5f).SetDelay(0.5f).OnComplete(() => Destroy(this.gameObject));
    }
}
