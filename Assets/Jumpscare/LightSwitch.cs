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

    // camera stuff
    public GameObject image;
    public RawImage rawImage;
    private WebCamTexture webcamTexture;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // make sure light is on
        onSwitch.SetActive(true);
        lighting.SetActive(true);

        // camera stuff
        // Find the default webcam (can be adjusted if you have multiple webcams)
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length > 0)
        {
            // Create a new WebCamTexture with the selected webcam device
            webcamTexture = new WebCamTexture(devices[0].name);

            // Assign the webcam feed to the RawImage texture
            rawImage.texture = webcamTexture;

            // Optionally, flip the image if needed (for proper orientation)
            rawImage.rectTransform.localScale = new Vector3(-1, 1, 1);

            // Start the webcam
            webcamTexture.Play();

            image.SetActive(false);
        }
        else
        {
            Debug.LogError("No webcam found.");
        }
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
                if (Random.value > 0.2)
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
                if (Random.value > 0.3)
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
            else if (Random.value > 0.4)
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
                StartCoroutine(TakePicture());

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

    private IEnumerator SaveImage()
    {
        //Create a Texture2D with the size of the rendered image on the screen.
        Texture2D texture = new Texture2D(rawImage.texture.width, rawImage.texture.height, TextureFormat.ARGB32, false);
        //Save the image to the Texture2D
        texture.SetPixels(webcamTexture.GetPixels());
        //texture = RotateTexture(texture, -90);
        texture.Apply();
        yield return new WaitForEndOfFrame();

        // save picture here ?
        AllManager.Instance.rollerCoasterImage = texture;

        // To avoid memory leaks
        Destroy(texture);

        OnDisable();
    }

    public void Capture()
    {
        StartCoroutine(SaveImage());
    }

    void OnDisable()
    {
        // Stop the webcam when the script is disabled or the scene is changed
        if (webcamTexture != null && webcamTexture.isPlaying)
        {
            webcamTexture.Stop();
        }
    }

    IEnumerator TakePicture()
    {
        yield return new WaitForSeconds(1);

        image.SetActive(true);

        Capture();
    }
}
