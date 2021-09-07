using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderCalculating : Border
{
    public Vector3 Newpos(Vector3 pos)
    {
        BorderValue(pos);

        float x = Mathf.Clamp(pos.x, RightBorder, LeftBorder);
        float y = Mathf.Clamp(pos.y, BottomBorder, TopBorder);

        if (pos != new Vector3(x, y, pos.z))
        {
            return new Vector3(-pos.x, -pos.y, pos.z);
        }
        return pos;
    }
}
public abstract class Border
{
    public float LeftBorder { private set; get; }
    public float RightBorder { private set; get; }
    public float TopBorder { private set; get; }
    public float BottomBorder { private set; get; }

    public void BorderValue(Vector3 mypos)
    {
        float dist = Vector3.Distance(mypos, Camera.main.transform.position);
        RightBorder = Camera.main.ViewportToWorldPoint(new Vector3(-0.04f, 0, dist)).x;
        LeftBorder = Camera.main.ViewportToWorldPoint(new Vector3(1.04f, 0, dist)).x;
        TopBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1.04f, dist)).y;
        BottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, -0.04f, dist)).y;
    }
}