using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    public GameObject[] collectables; // 0:Science 1:Law 2:Art
    private bool ready;
    public int value;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!ready && GameManager.Instance.characterType != null)
        {

            switch (GameManager.Instance.characterType)
            {

                case "Artist":
                    collectables[2].gameObject.SetActive(true);
                    ready = true;
                    break;

                case "Lawyer":
                    collectables[1].gameObject.SetActive(true);
                    ready = true;
                    break;

                case "Scientist":
                    collectables[0].gameObject.SetActive(true);
                    ready = true;
                    break;
            }



        }

    }
}
