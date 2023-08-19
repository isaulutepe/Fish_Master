using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fish : MonoBehaviour
{

    public Fish.FishType type;
    private CircleCollider2D _coll;
    private SpriteRenderer _spriteRenderer;
    private float _screenLeft;
    private Tweener _tweener;

    public Fish.FishType Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
            _coll.radius = type.colliderRadius;
            _spriteRenderer.sprite = type.sprite;
        }
    }


    private void Awake()
    {
        _coll = GetComponent<CircleCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _screenLeft = Camera.main.ScreenToWorldPoint(Vector3.zero).x;
        ResetFish();
    }
    public void ResetFish()
    {
        if (_tweener != null)
        {
            _tweener.Kill(false);
        }
        float num = UnityEngine.Random.Range(type.minLength, type.maxLength);
        _coll.enabled = true;
        Vector3 position = transform.position;
        position.y = num;
        position.x = _screenLeft;
        transform.position = position;

        float num2 = 1;
        float y = UnityEngine.Random.Range(num - num2, num + num2);
        Vector2 v = new Vector2(-position.x, y);

        float num3 = 3;
        float delay = UnityEngine.Random.Range(0, 2 * num3);
        _tweener = transform.DOMove(v, num3, false).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear).SetDelay(delay).OnStepComplete(
            delegate
            {
                Vector3 localScale = transform.localScale;
                localScale.x = -localScale.x;
                transform.localScale = localScale;
            });
    }
    public void Hooked() //Balýk oltaya takýldýðýnda tween öldürülür ve kapanýr.
    {
        _coll.enabled = false;
        _tweener.Kill(false);
    }

    [Serializable]
    public class FishType
    {
        public float price;
        public float fishCount;
        public float minLength;
        public float maxLength;
        public float colliderRadius;
        public Sprite sprite; //Oluþturulacak balýklarýn görsellerini dðeiþtirmek için.
    }
}
