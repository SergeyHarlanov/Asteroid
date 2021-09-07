using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [Range(0, 100)] [SerializeField] private float _speedasteroids;

    public TypeAsteroids type = TypeAsteroids.Large;
    public SpawnAsteroids spawnAsteroids;
    public Statistics statistics => spawnAsteroids.GetComponent<Statistics>();//статистика игрока туда будем записывать его награду

    

    private float _time;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (type != TypeAsteroids.Small)
        {
            
            Large ast = new Large();

            GameObject pr = gameObject;

            if (type == TypeAsteroids.Large)
            {
                  statistics.score += 20;
                  statistics.UpdateInf();
            

                ast = new Middle();
                type = TypeAsteroids.Middle;
            }
            else if (type == TypeAsteroids.Middle)
            {
                statistics.score += 50;
                statistics.UpdateInf();

                ast = new Small();
                type = TypeAsteroids.Small;
            }

            for (int i = 0; i < ast.breaking; i++)
            {
                GameObject smallerasteroid = Instantiate(pr);

                ast.Fault(smallerasteroid, Random.Range(_speedasteroids/2, _speedasteroids), i, transform.position);

                spawnAsteroids.Asteroids.Add(smallerasteroid.transform);
            }
        }
        else
        {
            statistics.score += 100;
            statistics.UpdateInf();
        }
        Destroy(gameObject);

    }
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(_rb.transform.up * _speedasteroids, ForceMode2D.Force);
    }
    private void Update()
    {
        _time += Time.deltaTime;

        BorderCalculating borderCalculating = new BorderCalculating();

        if (_time > 1 && borderCalculating.Newpos(transform.position) != transform.position)
        {
            transform.position = borderCalculating.Newpos(transform.position);
            _time = 0;
        }

    }
    
}

public enum TypeAsteroids
{
    Large,
    Middle,
    Small
}

interface IDataAsteroids
{
    public void Set();
}

public class Large
{
    public int breaking = 2;
    public float size = 0.478034f;

    public virtual void Set() { }

    public void Fault(GameObject insprefab, float speed, int i, Vector3 mypos)
    {
        Set();

        insprefab.transform.localScale = new Vector3(size, size);

        Rigidbody2D rb = insprefab.GetComponent<Rigidbody2D>();
        insprefab.transform.position = mypos;

        rb.AddForce((insprefab.transform.up * Random.Range(speed/2, speed)) + new Vector3(Random.Range(i == 0 ? -45 : 0, i == 1 ? 45 : 0), Random.Range(i == 0 ? -45 : 0, i == 1 ? 45 : 0)), ForceMode2D.Force);
    }
}

public class Middle : Large, IDataAsteroids
{
    public override void Set()
    {
        breaking = 2;
        size = 0.378034f;
    }
}
public class Small : Large, IDataAsteroids
{
    public override void Set()
    {
        size = 0.278034f;
        breaking = 2;
    }
}


