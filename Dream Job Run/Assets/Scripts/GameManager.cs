using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool isLevelStarted;
    public bool isLevelFinished;
    public bool isFailed;
    public bool isSucceed;
    public string characterType;
    public int score;

    public static GameManager Instance;

    private void Awake()
    {
        characterType = null;

        if (Instance == null)
        {

            Instance = this;

        }

    }

    // Start is called before the first frame update
    void Start()
    {

        UIManager.Instance.levelNameText.text = "LEVEL " + (SceneManager.GetActiveScene().buildIndex + 1).ToString(); 

    }

    // Update is called once per frame
    void Update()
    {

        UIManager.Instance.scoreText.text = score.ToString();

    }
}
