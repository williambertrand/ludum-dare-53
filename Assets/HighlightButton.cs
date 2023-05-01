using DG.Tweening;
using OTBG.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HighlightButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{

    public UnityEvent OnRelease;

    public GameObject gunIcon;

    private void Start()
    {
        gunIcon.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gunIcon.SetActive(true);
        transform.DOScale(Vector3.one * 1.2f, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one, 0.2f);
        gunIcon.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one, 0.2f);
        OnRelease?.Invoke();
    }

    public void PlayShotSound()
    {
        AudioManager.Instance.PlaySoundEffect(SFXIDs.ENEMY_2_GUNSHOT, true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one * 0.8f, 0.2f);

    }
}
