using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; //Do tewin kullanmak i�in ekledi�imiz k�t�phanedir.
using UnityEngine.UIElements;
using Unity.VisualScripting;
using System;

public class Hook : MonoBehaviour
{
    public Transform hookTransform;
    private Camera _mainCamera;
    private Collider2D _coll;

    private int _length;
    private int _strength;
    private int _fishCount;

    private bool _canMove;

    //List<Fish> -->Fish scripti eklendi�inde d�zeltilecek.
    private Tweener _cameraTween;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _coll = GetComponent<Collider2D>();

    }
    private void Start()
    {

    }
    private void Update()
    {
        if (_canMove && Input.GetMouseButton(0))
        {
            Vector3 vector = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 position = transform.position;
            position.x = vector.x;
            transform.position = position; //Yanl�zca x posizyonunda hareket olmas� i�in bu �ekilde yapt�m

        }

    }
    public void StartFishing()
    {
        _length = -50;//IdleManager dan gelecek.Kanca uzunlugu s�rekli negatif olaml� ��nk� kanca hep a�a�� inecek.
        _strength = 3; //G�� IdleManager dan gelecek sonra g�ncellenecek
        _fishCount = 0;
        float time = (-_length) * 0.1f;
        //DoTween ile kamera hareket kontrol�n� sa�lamak i�in.
        _cameraTween = _mainCamera.transform.DOMoveY(_length, 1 + time * 0.25f, false).OnUpdate(delegate
        {
            if (_mainCamera.transform.position.y < -11)
            {
                transform.SetParent(_mainCamera.transform);
            }
        }).OnComplete(delegate
        {
            _coll.enabled = true;
            _cameraTween = _mainCamera.transform.DOMoveY(0, time * 5, false).OnUpdate(delegate
            {
                if (_mainCamera.transform.position.y > -25)
                {
                    StopFishing();
                }
            });
        });
        _coll.enabled = false;
        _canMove = true;
    }

    private void StopFishing()
    {
        _canMove = false;
        _cameraTween.Kill(false);
        _cameraTween = _mainCamera.transform.DOMoveY(0, 2, false).OnUpdate(delegate
        {
            if (_mainCamera.transform.position.y >= -11)
            {
                transform.SetParent(null);
                transform.position=new Vector2(transform.position.x, -6);
            }
        }).OnComplete(delegate
        {
            transform.position = Vector2.down * 6;
            _coll.enabled = true;
            int num = 0; //Bal�klar� s�f�rlamak i�in.
        });
    }
}
