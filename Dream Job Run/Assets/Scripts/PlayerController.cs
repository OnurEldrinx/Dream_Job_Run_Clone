using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private PlayerActions PlayerInputActions;
    private Vector3 movement;
    private Vector2 inputVector;
    public float speed = 3;
    private bool flag = true;

    public GameObject[] characters; // 0:Default 1:Scientist 2:Judge 3:Artist
    public GameObject activeCharacter;

    private void Awake()
    {

        PlayerInputActions = new PlayerActions();
        activeCharacter = characters[0];
    }

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        TakeInput();

        //Is Game Started
        if (movement.x != 0 && flag)
        {

            GameManager.Instance.isLevelStarted = true;
            activeCharacter.GetComponent<Animator>().SetBool("isWalking", true);
            UIManager.Instance.swipeImg.gameObject.SetActive(false);
            flag = false;

        }


        if (GameManager.Instance.isLevelStarted && !GameManager.Instance.isLevelFinished)
            Move();





    }

    private void TakeInput()
    {

        inputVector = PlayerInputActions.Movement.Swerve.ReadValue<Vector2>();
        movement = new Vector3(inputVector.x, 0, 0);

    }

    public void Move()
    {

        transform.Translate(movement * speed * Time.deltaTime);

        AutoMoveForward();

        ClampPosition();


    }

    private void AutoMoveForward()
    {

        transform.Translate(Vector3.forward * (speed / 2) * Time.deltaTime);

    }

    private void ClampPosition()
    {


        float c = Mathf.Clamp(transform.position.x, -1.5f, 1.5f);

        transform.position = new Vector3(c, transform.position.y, transform.position.z);


    }

    private void OnTriggerEnter(Collider other)
    {

        switch (other.tag)
        {

            case "ArtistGate":

                activeCharacter.SetActive(false);
                activeCharacter = characters[3];
                activeCharacter.SetActive(true); 
                activeCharacter.GetComponent<Animator>().Play("Walking");
                GameManager.Instance.characterType = "Artist";
                UIManager.Instance.paintTubeImg.gameObject.SetActive(true);
                break;

            case "LawyerGate":

                activeCharacter.SetActive(false);
                activeCharacter = characters[2];
                activeCharacter.SetActive(true);
                activeCharacter.GetComponent<Animator>().Play("Walking");
                GameManager.Instance.characterType = "Lawyer";
                UIManager.Instance.libraImg.gameObject.SetActive(true);
                break;

            case "ScientistGate":

                activeCharacter.SetActive(false);
                activeCharacter = characters[1];
                activeCharacter.SetActive(true);
                activeCharacter.GetComponent<Animator>().Play("Walking");
                GameManager.Instance.characterType = "Scientist";
                UIManager.Instance.scienceTubeImg.gameObject.SetActive(true);
                break;




        }

    }


    private void OnEnable()
    {

        PlayerInputActions.Enable();

    }

    private void OnDisable()
    {

        PlayerInputActions.Disable();

    }


}
