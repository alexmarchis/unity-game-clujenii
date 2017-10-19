using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public abstract class Level : MonoBehaviour
{
    public float levelStartDelay = 2f;
    public string LevelStartText;
    public string LevelEndText;

    private Text levelText;
    private GameObject textImage;
    private GameObject exitSign;

    private Music playMusic;

    private bool levelComplete = false;

    protected void Awake()
    {
        textImage = GameObject.Find("TextImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        exitSign = GameObject.Find("ExitSign");

        levelText.text = LevelStartText;
        textImage.SetActive(true);

        exitSign.SetActive(false);

        Invoke("StartLevel", levelStartDelay);

        playMusic = GetComponent<Music>();
    }

    private void Update()
    {
        if(LevelConditionsMet())
        {
            levelComplete = true;
            if (!exitSign.activeSelf)
            {
                exitSign.SetActive(true);
            }
        }
        else
        {
            levelComplete = false;
            exitSign.SetActive(false);
        }
    }

    void StartLevel()
    { 
        textImage.SetActive(false);
        playMusic.PlayLevelMusic();
    }

    public virtual void EndGame()
    {
        levelText.text = LevelEndText;
        textImage.SetActive(true);
        Invoke("BackToMainMenu", levelStartDelay);
    }

    private void BackToMainMenu()
    {
        playMusic.StopLevelMusic();
        SceneManager.LoadScene(0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"
          && levelComplete)
        {
            EndGame();
        }
    }

    protected abstract bool LevelConditionsMet();
}
