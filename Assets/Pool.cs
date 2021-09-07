using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;

    private float _time;
    private int numbershots;

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1))
        {
            if (numbershots != 3 && _time < 1 || numbershots < 3)
            {
                GameObject bl = Instantiate(_bullet);

                bl.transform.position = transform.position;
                bl.transform.rotation = transform.rotation;
                bl.GetComponent<Bullet>().ship = transform;
                numbershots++;
            }
            else
            {
                numbershots = 0;
                _time = 0;
            }
           
        }
       
    }
}
