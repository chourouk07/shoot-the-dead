using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] skeltons;
    private bool isRising = false;
    private bool isFalling = false;
    private int activeSkeltIndex = 0;
    private Vector2 startPos;
    public int riseSpeed = 1;
    private int score;
    private int lives;
    private bool game;
    public Image life1;
    public Image life2;
    public Image life3;
    public Text scoreText;
    public Button gameOverButton;
    public int mark = 5; 


    // Start is called before the first frame update
    void Start()
    {
        PickSkelton();
        score = 0;
        scoreText.text = score.ToString();
        lives = 3;
        game = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (game)
        {
            if (isRising)
            {
                if (skeltons[activeSkeltIndex].transform.position.y - startPos.y >= 3.5f)
                {
                    isRising = false;
                    isFalling = true;
                }
                else
                {
                    skeltons[activeSkeltIndex].transform.Translate(Vector2.up * Time.deltaTime * riseSpeed);
                }
            }
            else if (isFalling)
            {
                if (skeltons[activeSkeltIndex].transform.position.y - startPos.y <= 0)
                {
                    isFalling = false;
                    lives--;
                    UpdateLife();
                }
                else
                {
                    skeltons[activeSkeltIndex].transform.Translate(Vector2.down * Time.deltaTime * riseSpeed);
                }
            }
            else
            {
                skeltons[activeSkeltIndex].transform.position = startPos;
                PickSkelton();
            }
        }
    }

    void UpdateLife()
    {
        if (lives == 3)
        {
            life1.gameObject.SetActive(true);
            life2.gameObject.SetActive(true);
            life3.gameObject.SetActive(true);
        }
        if (lives == 2)
        {
            life1.gameObject.SetActive(true);
            life2.gameObject.SetActive(true);
            life3.gameObject.SetActive(false);
        }
        if (lives == 1)
        {
            life1.gameObject.SetActive(true);
            life2.gameObject.SetActive(false);
            life3.gameObject.SetActive(false);
        }
        if (lives == 0)
        {
            life1.gameObject.SetActive(false);
            life2.gameObject.SetActive(false);
            life3.gameObject.SetActive(false);
            game = false;
            gameOverButton.gameObject.SetActive(true);
        }

    }
    void PickSkelton()
    {
        isRising = true;
        isFalling = false;
        activeSkeltIndex = UnityEngine.Random.Range(0, skeltons.Length);
        startPos = skeltons[activeSkeltIndex].transform.position;
    }
    public void KillSkelton()
    {
        skeltons[activeSkeltIndex].transform.position = startPos;
        score++;
        Difficulty();
        scoreText.text = score.ToString();
        PickSkelton();
    }
    
    void Difficulty()
    {
        if (score >= mark)
        {
            riseSpeed++;
            mark += 5;
        }
    }
    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
