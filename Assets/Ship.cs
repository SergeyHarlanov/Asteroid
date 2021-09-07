using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private Rigidbody2D _rb;
    [Header("Ускорение")]

    public float acceleration;
    [Header("Макс скорость")]

    public float maxspeed;
    [Header("поворот корабля")]

    public float TurningShip;
    private float _time;
    public float sensivityKeyboard = 1.5f;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
    }
    void Update()
    {

        //Ограничиваем скорость
        if (_rb.velocity.magnitude >= maxspeed)
        {
            _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, maxspeed);
        }

        //Acceleration

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
                _rb.AddForce(transform.up * acceleration);
        }


        //direction ship проверяем какое управление использовать
        if (PlayerPrefs.GetInt("Control") == 0) RotationOnKeyboard();
        else RotationShip(transform, Camera.main.ScreenToWorldPoint(Input.mousePosition));

        //border screen

        Borderscreen();


    }
    public void Borderscreen()//переелет за границу действие
    {
        _time += Time.deltaTime;

        BorderCalculating borderCalculating = new BorderCalculating();

        if (_time > 1 && borderCalculating.Newpos(transform.position) != transform.position)//проверяем поменялась ли позицию и смотрим что бы в один фрейм не поменяли позицию много раз
        {
            transform.position = borderCalculating.Newpos(transform.position);
            _time = 0;
        }
    }
    public void RotationShip(Transform tr, Vector3 target)//поворот с пмоощью мыши
    {
        Vector3 mousepos = target;
        Vector2 dirfrome = (mousepos - tr.position);

        Quaternion torotation = Quaternion.LookRotation(Vector3.forward, dirfrome);

       transform.rotation = Quaternion.Lerp(tr.rotation, torotation, TurningShip);
    }
    public void RotationOnKeyboard()//поворот с помощью клавиратуры
    {
        if (Input.GetKey(KeyCode.A))
            _rb.rotation += sensivityKeyboard;
        if (Input.GetKey(KeyCode.D))
            _rb.rotation -= sensivityKeyboard;
    }
 
}