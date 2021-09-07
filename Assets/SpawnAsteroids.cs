using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroids : MonoBehaviour
{
    [SerializeField] private GameObject _prefabsteroid;
    [SerializeField] private int _count;
    [SerializeField] private float _radius;

    public List<Transform> Asteroids = new List<Transform>();
    void Start()
    {
        CutAsteroid(2);
    }

    private void Update()
    {
        Asteroids.RemoveAll(x => x == null);

        if (Asteroids.Count==0)
        {
            _count++;
            CutAsteroid(_count);
        }
    }

    public void CutAsteroid(int count)
    {
        for (float i = 0; i < count; i++)
        {
            GameObject pr = Instantiate(_prefabsteroid);

            pr.GetComponent<Asteroid>().enabled = true;
            pr.GetComponent<CircleCollider2D>().enabled = true;

            Rigidbody2D rb = pr.GetComponent<Rigidbody2D>();

            BorderVectorSpawn borderVectorSpawn = new BorderVectorSpawn(transform);

            //рандом для позиции
            int done = Random.Range(0, 2);

            //рандом для направления
            int dir = Random.Range(0, 2);

            Vector2 randompos = Vector2.zero;

            if (done == 0)
            {
                randompos = new Vector2(Random.Range(borderVectorSpawn._rightBorder, borderVectorSpawn._leftBorder), dir == 0 ? borderVectorSpawn._bottomBorder : borderVectorSpawn._topBorder);
            }
            if (done == 1)
            {
                randompos = new Vector2(dir == 0 ? borderVectorSpawn._leftBorder : borderVectorSpawn._rightBorder, Random.Range(borderVectorSpawn._bottomBorder, borderVectorSpawn._topBorder));
            }

            pr.transform.position = randompos;

            rb.rotation = dir == 0 ? Random.Range(0, 90) : Random.Range(-90, 0);

            Asteroids.Add(pr.transform);

            pr.GetComponent<Asteroid>().spawnAsteroids = this;
        }
    }
}


