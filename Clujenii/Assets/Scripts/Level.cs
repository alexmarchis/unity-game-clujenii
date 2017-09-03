using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public float levelStartDelay = 2f;
    public string LevelStartText;
    public string LevelEndText;

    private Text levelText;
    private GameObject textImage;

    protected void Awake()
    {
        textImage = GameObject.Find("TextImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();

        levelText.text = LevelStartText;
        textImage.SetActive(true);

        Invoke("HideLevelImage", levelStartDelay);
    }

    void HideLevelImage()
    { 
        textImage.SetActive(false);
    }

    public virtual void EndGame()
    {
        levelText.text = LevelEndText;
        textImage.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            EndGame();
        }
    }
}
