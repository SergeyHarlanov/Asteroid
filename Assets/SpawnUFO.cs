using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUFO : MonoBehaviour
{
    [SerializeField] private GameObject _ufoleft;
    [SerializeField] private GameObject _uforight;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _player;
    void Start()
    {
        StartCoroutine(SpawnTime());
    }
    IEnumerator SpawnTime()
    {
        while (true)
        {
            int done = Random.Range(0, 2);

            GameObject ufo = Instantiate(done == 0 ? _ufoleft : _uforight);

            Rigidbody2D _rb = ufo.GetComponent<Rigidbody2D>();

            BorderVectorSpawn borderVectorSpawn = new BorderVectorSpawn(transform);

            Vector2 randompos = new Vector2(done == 0 ? borderVectorSpawn._rightBorder : borderVectorSpawn._leftBorder, Random.Range(borderVectorSpawn._bottomBorder + 2f, borderVectorSpawn._topBorder - 2f));

            ufo.transform.position = randompos;

            _rb.rotation = done == 0 ? -90 :  90;

            _rb.GetComponent<UFO>().player = _player;

            yield return new WaitForSeconds(Random.Range(20, 40));
        }
    }
}
