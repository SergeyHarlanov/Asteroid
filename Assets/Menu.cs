using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _Buttons;
    [SerializeField] private TMP_Dropdown _tMP;

    [SerializeField] private Statistics statistics;

    [SerializeField] private Transform ship => statistics.transform;
    private void Start()
    {
        Time.timeScale = 0;
    }
    public void Escape()
    {
        
            if (!_Buttons.activeSelf)
            {
                _Buttons.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                _Buttons.SetActive(false);
                Time.timeScale = 1;
            }
       
    }
    public void NewGame()
    {
        statistics.lives = 4;
        statistics.score = 0;
        statistics.UpdateInf();
        SceneManager.LoadScene(0);
       // ship.position = new Vector3(0,0);
      //  ship.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
      //  Escape();
    }
    public void Quit()
    {
        Application.Quit();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//�����
        {
            Escape();
        }

        Control();

    }
    public void Control()
    {
        if (_tMP.transform.Find("Dropdown List")) PlayerPrefs.SetInt("Control", _tMP.value);//��������� �������� ������� �� ������� ��� ����������

        if (PlayerPrefs.HasKey("Control")) _tMP.value = PlayerPrefs.GetInt("Control");//���� �������� ��� ���� ����� ������� ����� ����������
    }
}
