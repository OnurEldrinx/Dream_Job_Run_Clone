using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Podium : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject einstein;
    public GameObject judy;
    public GameObject dali;

    private bool flag = true;

    public ParticleSystem capParticles;

    // Update is called once per frame
    void Update()
    {
        
        if(GameManager.Instance.characterType == "Artist" && flag)
        {

            dali.SetActive(true);
            flag = false;
            GetComponent<Podium>().enabled = false;

        }else if (GameManager.Instance.characterType == "Scientist" && flag)
        {

            einstein.SetActive(true);
            flag = false;
            GetComponent<Podium>().enabled = false;


        }
        else if (GameManager.Instance.characterType == "Lawyer" && flag)
        {

            judy.SetActive(true);
            flag = false;
            GetComponent<Podium>().enabled = false;

        }

    }
}
