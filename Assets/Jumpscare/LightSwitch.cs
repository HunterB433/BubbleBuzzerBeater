using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LightSwitch : MonoBehaviour
{
    // ui stuff
    public GameObject openDoor;
    public GameObject closedDoor;
    public GameObject lighting;
    public GameObject darkness;
    public GameObject offSwitch;
    public GameObject onSwitch;
    public GameObject monsterDownstairs;
    public GameObject monsterUpstairs;

    // sounds
    public AudioSource audioSource;
    public AudioClip doorKnock;
    public AudioClip doorOpen;
    public AudioClip scarySound;

    // randomization stuff
    private bool monsterDownstairsAppeared;
    private bool showMonsterUpstairs = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // make sure light is on
        onSwitch.SetActive(true);
        lighting.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LightSwitched()
    {
        // check if switch is on or off
        if (onSwitch.activeSelf)
        {
            onSwitch.SetActive(false);
            offSwitch.SetActive(true);
            lighting.SetActive(false);
            darkness.SetActive(true);

            // check if monster has appeared downstairs
            if (monsterDownstairsAppeared)
            {
                // decide if we want to show jumpscare
                if (Random.value > 0.3)
                {
                    showMonsterUpstairs = true;
                    
                }
                // otherwise, endgame
                else
                {
                    EndGame();
                }
            }
            // otherwise, check if door has been opened
            else if (openDoor.activeSelf)
            {
                // randomize monster appearing
                if (Random.value > 0.4)
                {
                    monsterDownstairs.SetActive(true);
                    monsterDownstairsAppeared = true;
                }
                // otherwise, end game
                else
                {
                    EndGame();
                }
            }
            // otherwise, decide if we want to open door
            else if (Random.value > 0.5)
            {
                // play open door sound
                audioSource.PlayOneShot(doorOpen);

                // open door
                openDoor.SetActive(true);
                closedDoor.SetActive(false);
            }
            // otherwise, end game
            else
            {
                audioSource.PlayOneShot(doorKnock);
                EndGame();
            }
        }
        else
        {
            onSwitch.SetActive(true);
            offSwitch.SetActive(false);
            lighting.SetActive(true);
            darkness.SetActive(false);

            // check if monster has appeared downstairs
            if (monsterDownstairs.activeSelf)
            {
                // deactivate when light turns on
                monsterDownstairs.SetActive(false);
            }
            
            // check if we are a go to jumpscare
            if (showMonsterUpstairs)
            {
                audioSource.PlayOneShot(scarySound);
                monsterUpstairs.SetActive(true);

                // take picture here
                EndGame();
            }
        }
    }

    public void EndGame()
    {
        onSwitch.GetComponent<Button>().enabled = false;
        offSwitch.GetComponent<Button>().enabled = false;

        StartCoroutine(EndGameScene());
        Debug.Log("Game End!");
    }

    IEnumerator EndGameScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("LeadIn");
    }
}
