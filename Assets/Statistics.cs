using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour
{
    private SpriteRenderer spriteRenderer => GetComponent<SpriteRenderer>();
    private CircleCollider2D collider2D => GetComponent<CircleCollider2D>();

    [SerializeField] private Menu menu;

    public Text text;

    public int lives = 4;
    public int score = 0;

    public bool vulnerability;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Lives"))
        {
            lives = PlayerPrefs.GetInt("Lives");
            score = PlayerPrefs.GetInt("Score");
        }
        UpdateInf();//обвновляем информацию
    }
    private void Update()
    {
        if (lives == 0 && Time.timeScale!=0)
        {
            menu.NewGame();
        }
    }
    private async void OnTriggerEnter2D(Collider2D collision)//хитрый трюк но по сути здесь не используется многопоточность т.к возвращаем null у delay
    {
        if (collision.name != "Bullet(Clone)")
        {
            collider2D.enabled = false;
            vulnerability = true;

            lives--;
            transform.position = new Vector3(0, 0);
            gameObject.SetActive(false);

            await Task.Delay(2000);

            gameObject.SetActive(true);

            UpdateInf();

            StartCoroutine(Flashing());
        }
    }

       
    public void UpdateInf()//обновляем информацию на канвасе
    {
        text.text = "lives : " + lives + "\n" + "Score : " + score;
        PlayerPrefs.SetInt("Lives", lives);//сохраняем значения
        PlayerPrefs.SetInt("Score", score);//сохраняем значения
    }
    public IEnumerator Flashing()
    {
        for (int i = 0;i<3;i++)
        {
           
            while (spriteRenderer.color.a >= 0)
            {
                spriteRenderer.color = new Color(255, 255, 255, spriteRenderer.color.a - 0.013f);//интерполируем значение прозрачности до 0 
                yield return null;
            }

            while (spriteRenderer.color.a < 1)
            {
                spriteRenderer.color = new Color(255, 255, 255, spriteRenderer.color.a + 0.013f);//интерполируем значение прозрачности до 1
                yield return null;
            }
          
        }

        collider2D.enabled = true;
        vulnerability = false;
    }
}