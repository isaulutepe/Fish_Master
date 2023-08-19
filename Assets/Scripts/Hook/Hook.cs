using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; //Do tewin kullanmak için eklediðimiz kütüphanedir.
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

    //List<Fish> -->Fish scripti eklendiðinde düzeltilecek.
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
            transform.position = position; //Yanlýzca x posizyonunda hareket olmasý için bu þekilde yaptým

        }

    }
    public void StartFishing()
    {
        _length = -50;//IdleManager dan gelecek.Kanca uzunlugu sürekli negatif olamlý çünkü kanca hep aþaðý inecek.
        _strength = 3; //Güç IdleManager dan gelecek sonra güncellenecek
        _fishCount = 0;
        float time = (-_length) * 0.1f;
        //DoTween ile kamera hareket kontrolünü saðlamak için.
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
            int num = 0; //Balýklarý sýfýrlamak için.
        });
    }
}
