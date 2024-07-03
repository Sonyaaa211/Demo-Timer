using Hung.Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorldUI : MonoBehaviour, ICanvas, IPoolable
{
    [field: SerializeField] public Vector3 Offset { get; set; }

    public Canvas targetCanvas => Ref.Instance.WorldCanvas;

    protected RectTransform m_rect;

    public Transform Storage { get; set; }

    public void BackToPool()
    {
        gameObject.SetActive(false);
        if (Storage != null)
        {
            transform.SetParent(Storage);
        }
    }

    public void AttachToCanvas()
    {
        transform.SetParent(targetCanvas.transform, true);
        transform.localScale = Vector3.one;
        transform.localEulerAngles = Vector3.zero;
        transform.localPosition = Vector3.zero;

        m_rect = (RectTransform)transform;
        m_rect.anchorMax = Vector2.zero;
        m_rect.anchorMin = Vector2.zero;
    }

    protected void Awake()
    {
        AttachToCanvas();
    }

    Vector2 _v;
    [Range(0.0000001f, 1f)][SerializeField] protected float smoothTime; 
    protected void LocateAt(Vector3 worldPos)
    {
        m_rect.anchoredPosition = Vector2.SmoothDamp(m_rect.anchoredPosition, Camera.main.WorldToScreenPoint(worldPos) / targetCanvas.scaleFactor + Offset, ref _v, Time.deltaTime * smoothTime);
        //m_rect.anchoredPosition = Camera.main.WorldToScreenPoint(worldPos) / targetCanvas.scaleFactor + Offset;
    }

    public abstract void ToggleOn();
    public abstract void ToggleOff();
}