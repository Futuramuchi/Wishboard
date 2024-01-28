using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WishPart : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action Dragged;
    public event Action Dropped;

    [SerializeField] private Image image; 

    private Vector2 _offset;

    private bool _dragged;

    public void Set(Sprite sprite, Vector2 position)
    {
        image.sprite = sprite;
        transform.position = position;
    }

    private void Update()
    {
        if (_dragged) 
            transform.position = (Vector2)Input.mousePosition - _offset;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _offset = eventData.position - (Vector2)transform.position;

        var temp = _offset;

        DOVirtual.Float(0, 1, 0.2f, (x) => _offset = Vector2.Lerp(temp, Vector2.zero, x));

        _dragged = true;

        Dragged?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _dragged = false;

        Dropped?.Invoke();
    }
}