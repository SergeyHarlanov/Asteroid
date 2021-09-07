using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    private float _time;

    public GameObject bullet;

    public Transform player;

    public Statistics statistics => player.GetComponent<Statistics>();

    private Rigidbody2D _rb;

    private float _rangetimeshoot;//время для каждого выстрела
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name != "UFOBullet(Clone)")
        {
            statistics.score += 200;
            statistics.UpdateInf();
            Destroy(gameObject);
        }

    }
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rangetimeshoot = Random.Range(2, 5);//указываем значение
    }

    void Update()
    {
        // перемещение за экран
        _time += Time.deltaTime;

        BorderCalculating borderCalculating = new BorderCalculating();

        if (_time > 1 && borderCalculating.Newpos(transform.position) != transform.position)
        {
            transform.position = borderCalculating.Newpos(transform.position);
            _time = 0;
        }

        Shoot();

        _rb.AddForce(transform.up * 1);

        if (_rb.velocity.magnitude >= 4)//Ограничиваем скорость
        {
            _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, 4);
        }
    }
    public void Shoot()
    {
        Vector2 dir = player.position - transform.position;

        if (_time >= 1 && !player.GetComponent<Statistics>().vulnerability)//проверяем заряжено ли оружие и на состояние игрока
        {

            GameObject bull = Instantiate(bullet);

            bull.transform.position = transform.position;

            bull.GetComponent<Rigidbody2D>().rotation = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90);

            _time = 0;

            bull.GetComponent<Bullet>().myUfo = transform;
            bull.GetComponent<Bullet>()._ufo = true;
            //_rangetimeshoot = Random.Range(2, 5);//указываем значение снова
        }
    }
    
}
