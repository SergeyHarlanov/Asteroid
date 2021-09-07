using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderVectorSpawn
{
    public float _rightBorder { get; private set; }
    public float _leftBorder { get; private set; }
    public float _topBorder { get; private set; }
    public float _bottomBorder { get; private set; }
    public BorderVectorSpawn(Transform transform)
    {
        //Вектора для границ экрана
        float dist = Vector3.Distance(transform.position, Camera.main.transform.position);
        _rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0, dist)).x;
        _leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(1.02f, 0, dist)).x;
        _topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1f, dist)).y;
        _bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0f, dist)).y;
    }
}
