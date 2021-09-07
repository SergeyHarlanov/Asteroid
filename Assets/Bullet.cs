using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] private float _force;

    public Transform ship;

    public Transform myUfo;

    public bool _ufo = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_ufo && collision.gameObject != myUfo.gameObject || !_ufo && collision.gameObject != ship.gameObject)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

    }
    void Update()
    {
        //border screen

        BorderCalculating borderCalculating = new BorderCalculating();

        //проверяем нашу позицию относитльно границы

        if (transform.position != borderCalculating.Newpos(transform.position)) Destroy(gameObject);

        //ускорение

        _rb.AddForce(transform.up * _force);

    }
}
