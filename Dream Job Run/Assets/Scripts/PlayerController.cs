using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerController : MonoBehaviour
{

    private PlayerActions PlayerInputActions;
    private Vector3 movement;
    private Vector2 inputVector;
    public float horizontalSpeed = 3;
    public float forwardSpeed = 1.5f;
    private bool flag = true;

    public GameObject[] characters; // 0:Default 1:Scientist 2:Judge 3:Artist
    public GameObject activeCharacter;

    //Science Fail
    public GameObject cleanJacket;
    public GameObject dirtyJacket;
    public GameObject glasses;
    
    //Artist Fail
    public GameObject paintBucket;
    public Material artistBodySkin;
    public Texture paintedSkinTexture;
    public Texture normalSkinTexture;

    private bool upgradeNeed;
    private bool downgradeNeed;

    public GameObject finishPos;
    public GameObject photoCam;

    private CharacterController characterController;
    private void Awake()
    {

        PlayerInputActions = new PlayerActions();
        activeCharacter = characters[0];
    }

    // Start is called before the first frame update
    void Start()
    {

        characterController = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {

        if(GameManager.Instance.FScore >= GameManager.Instance.AScore)
        {

            downgradeNeed = true;

        }
        else
        {

            upgradeNeed = true;

        }

        CharacterAppearanceUpdate();

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

        //transform.Translate(movement * speed * Time.deltaTime);

        characterController.SimpleMove(movement * horizontalSpeed);

        AutoMoveForward();

        ClampPosition();


    }

    private void AutoMoveForward()
    {

        //transform.Translate(Vector3.forward * (speed / 2) * Time.deltaTime);
        characterController.SimpleMove(Vector3.forward * (forwardSpeed));
    }

    private void ClampPosition()
    {


        float c = Mathf.Clamp(transform.position.x, -1.5f, 1.5f);

        transform.position = new Vector3(c, transform.position.y, transform.position.z);


    }

    private void CharacterAppearanceUpdate()
    {

        if(downgradeNeed)
        {


            if(activeCharacter == characters[1])
            {

                glasses.SetActive(true);
                cleanJacket.SetActive(false);
                dirtyJacket.SetActive(true);

            }else if(activeCharacter == characters[3])
            {

                paintBucket.SetActive(true);
                artistBodySkin.SetTexture("_BaseMap", paintedSkinTexture);

            }

            downgradeNeed = false;

        }
        else if(upgradeNeed)
        {

            if (activeCharacter == characters[1])
            {

                glasses.SetActive(false);
                cleanJacket.SetActive(true);
                dirtyJacket.SetActive(false);

            }
            else if (activeCharacter == characters[3])
            {

                paintBucket.SetActive(false);
                artistBodySkin.SetTexture("_BaseMap", normalSkinTexture);

            }

            upgradeNeed = false;

        }

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

            case "Collectable":

                other.gameObject.SetActive(false);
                GameManager.Instance.score += other.transform.parent.gameObject.GetComponent<Collectable>().value;
                break;

            case "A+":
                GameManager.Instance.AScore += 10;
                other.gameObject.SetActive(false);
                break;
            case "F-":
                GameManager.Instance.FScore += 10;
                other.gameObject.SetActive(false);
                break;

            

        }

        if (other.tag.Contains("Gate"))
        {

            other.gameObject.SetActive(false);

        }

        if((int.TryParse(other.tag,out int r) && r == GameManager.Instance.AScore) && GameManager.Instance.AScore < 100)
        {

            GameManager.Instance.isLevelFinished = true;
            UIManager.Instance.nextLevelButton.gameObject.SetActive(true);
            activeCharacter.GetComponent<Animator>().Play("Sad");


        }else if ((GameManager.Instance.AScore > 50 && other.tag == "50") && GameManager.Instance.AScore < 100)
        {

            GameManager.Instance.isLevelFinished = true;
            UIManager.Instance.nextLevelButton.gameObject.SetActive(true);
            activeCharacter.GetComponent<Animator>().Play("Sad");

        }else if (GameManager.Instance.AScore >= 100 && other.tag == "100")
        {

            photoCam.SetActive(true);
            this.transform.parent = finishPos.transform;
            transform.DOLocalMove(Vector3.zero,1.5f).OnComplete(()=>Pose());
            

        }

    }

    private void Pose()
    {

        GameManager.Instance.isLevelFinished = true;
        //transform.DOLocalRotate(new Vector3(0, 180, 0), 1).OnComplete(() => activeCharacter.GetComponent<Animator>().Play("Pose"));
        StartCoroutine(PhotoShot(1));
        
    }

    IEnumerator PhotoShot(float t)
    {


        transform.DOLocalRotate(new Vector3(0, 180, 0), 1).OnComplete(() => activeCharacter.GetComponent<Animator>().Play("Pose"));
        yield return new WaitForSeconds(t);
        UIManager.Instance.photoframe.gameObject.SetActive(true);
        UIManager.Instance.dreamJobImg.gameObject.SetActive(true);
        UIManager.Instance.nextLevelButton.gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("Podium").GetComponent<Podium>().capParticles.Play();

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
