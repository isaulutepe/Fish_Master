using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; //Do tewin kullanmak i�in ekledi�imiz k�t�phanedir.

public class Hook : MonoBehaviour
{
    public Transform hookTransform;
    private Camera _mainCamera;
    private Collider2D _coll;

    private int _length;
    private int _strength;
    private int _fishCount;

    private bool _canMove=true;

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
}
