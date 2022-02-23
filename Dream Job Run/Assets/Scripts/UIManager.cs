using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text scoreText;
    public Text levelNameText;
    public Image swipeImg;
    public Image emptyBarImg;
    public Image greenBarImg;
    public Image redBarImg;
    public Image paintTubeImg;
    public Image scienceTubeImg;
    public Image libraImg;
    public Button nextLevelButton;
    public Image dreamJobImg;


    public static UIManager Instance;

    private void Awake()
    {
        
        if(Instance == null)
        {

            Instance = this;

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
