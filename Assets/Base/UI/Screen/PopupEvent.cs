using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Unity.VisualScripting;

[CreateAssetMenu(menuName = "Hung/SO Singleton/Popup Event")]
public class PopupEvent : SOSingleton<PopupEvent>
{
    [Range(0f, 1f)][SerializeField] internal float popUpDuration;
    [Range(0f, 1f)][SerializeField] internal float fadeDuration;
    [Range(0f, 1f)][SerializeField] internal float fadeValue;
    [SerializeField] internal float wipeDuration;
    [SerializeField] internal float popDuration;

    public void OnOpenClick(HomeScreen screenName)
    {
        PopUp_On(UIScreenManager_Obsolete.Instance.GetScreen(screenName));
    }

    public void OnOpenClick(TravelScreen screenName)
    {
        PopUp_On(UIScreenManager_Obsolete.Instance.GetScreen(screenName));
    }

    public void OnExitClick(TravelScreen screenName)
    {
        PopUp_Off(UIScreenManager_Obsolete.Instance.GetScreen(screenName));
    }


    public void OnOpenClick(UIScreen_Obsolete screen)
    {
        PopUp_On(screen);
    }

    public void OnExitClick(UIScreen_Obsolete screen)
    {
        PopUp_Off(screen);
    }

    public void InstantExit(UIScreen_Obsolete screen)
    {
        screen.BgMask.DOFade(0, fadeDuration).SetUpdate(true);
        screen.BgMask.transform.SetParent(screen.transform);
        screen.transform.localScale = Vector3.zero;
        screen.OnClosed();
    }

    private void PopUp_On(UIScreen_Obsolete screen)
    {
        screen.OnStartOpening();

        Transform popUpTransform = screen.transform;
        popUpTransform.gameObject.SetActive(true);
        screen.BgMask.color = Color.black * fadeValue;
        screen.BgMask.transform.SetParent(screen.transform.parent);
        screen.BgMask.transform.localScale = Vector3.one;
        screen.BgMask.transform.SetSiblingIndex(screen.transform.GetSiblingIndex());

        popUpTransform.DOScale(Vector3.one, popUpDuration).SetEase(Ease.OutBack).SetUpdate(true).OnComplete(() =>
        {
            screen.OnOpened();
            for (int i = 0; i < screen.WipedList.Count; i++)
            {
                screen.WipedList[i].DOAnchorPos(Vector2.zero, wipeDuration).SetUpdate(true).SetEase(Ease.OutBack);
            }
            float stepTime = screen.PopStepTime;
            for (int i = 0; i < screen.PoppedList.Count; i++)
            {
                screen.PoppedList[i].DOScale(1, popDuration).SetUpdate(true).SetEase(Ease.OutBack).SetDelay(wipeDuration + i * stepTime);
            }
        });
    }

    private Action closedCommittee;

    public float PopDuration { get => popDuration; }

    internal void OnNextCloseCommit(Action action)
    {
        closedCommittee += action;
    }

    private void PopUp_Off(UIScreen_Obsolete screen)
    {
        Transform popUpTransform = screen.transform;
        screen.BgMask.DOFade(0, fadeDuration).SetUpdate(true);

        for (int i = 0; i < screen.PoppedList.Count; i++)
        {
            screen.PoppedList[i].localScale = Vector3.zero;
        }

        popUpTransform.DOScale(Vector3.zero, popUpDuration).SetEase(Ease.InBack).SetUpdate(true).OnComplete(() =>
        {
            screen.OnClosed();
            screen.BgMask.transform.SetParent(screen.transform);

            if (closedCommittee != null)
            {
                closedCommittee();
                closedCommittee = null;
            }
        });
    }
}
