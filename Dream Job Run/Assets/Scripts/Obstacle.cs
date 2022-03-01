using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Obstacle : MonoBehaviour
{

    private GameObject player;
    private bool flag;
    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        flag = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if((gameObject.transform.position - player.transform.position).magnitude < 5 && gameObject.activeSelf)
        {

            GetComponent<Animator>().Play("Dance");

        }

        if((gameObject.transform.position.z - player.transform.position.z) < -3 && flag)
        {

            gameObject.SetActive(false);
            flag = false;

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player")
        {

            GameManager.Instance.AScore -= 20;
            GameManager.Instance.FScore += 10;

            other.transform.DOMoveZ(other.transform.position.z - 2,0.5f);

        }

    }

}
