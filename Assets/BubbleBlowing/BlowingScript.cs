using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlowingScript : MonoBehaviour
{
    [SerializeField]
    private Slider blowingMeter;
    [SerializeField]
    private Image thresholdUI;
    [SerializeField]
    private GameObject bubble;
    [SerializeField]
    private GameObject wellDoneText;
    public float threshold;

    // microphone stuff
    private AudioSource audioSource;
    private AudioClip microphoneClip;
    private int sampleRate = 44100;
    private float[] samples;
    private float loudness, sum;

    // Start is called before the first frame update
    void Start()
    {
        // set default value
        blowingMeter.value = 0;

        thresholdUI.color = Color.red;

        audioSource = GetComponent<AudioSource>();
        samples = new float[1024]; // Number of samples you want to analyze
        
        // Start recording from the default microphone
        microphoneClip = Microphone.Start(null, true, 1, sampleRate);
        audioSource.clip = microphoneClip;
        audioSource.loop = true;
        while (Microphone.GetPosition(null) <= 0) {} // Wait until the microphone is initialized
        audioSource.Play();
    }

    void Update()
    {   
        AnalyzeMicrophoneInput();
    }

    void AnalyzeMicrophoneInput()
    {
        if (Microphone.devices[0] != null
            && Microphone.GetPosition(Microphone.devices[0]) > 0
            && Microphone.GetPosition(Microphone.devices[0]) > samples.Length)
        {
        // Get a portion of the microphone audio to analyze
        microphoneClip.GetData(samples, Microphone.GetPosition(Microphone.devices[0]) - samples.Length);

        // Measure the loudness (volume) of the audio input
        loudness = GetLoudness();
        //Debug.Log("Loudness: " + loudness);
        blowingMeter.value = loudness;

        // Here you can trigger events based on the loudness value (blowing strength)
            if (loudness > threshold) // Adjust threshold as needed
            {
                thresholdUI.color = Color.green;
                Debug.Log("Threshold reached for the required time!");
                audioSource.Play();
                // Trigger whatever event you want here
                bubble.SetActive(true);
                StartCoroutine(SayWellDone());
            }
        }
    }

    float GetLoudness()
    {
        sum = 0f;
        foreach (float sample in samples)
        {
            sum += Mathf.Abs(sample); // Sum up the absolute value of each sample
        }
        loudness = sum / samples.Length; // Calculate the average loudness

        // Clamp the loudness to ensure it's between 0 and 1
        loudness = Mathf.Clamp(loudness, 0f, 1f);

        // Scale down the loudness to make it less sensitive
        //loudness *= 0.5f; // Adjust this factor as needed

        return loudness;
    }

    IEnumerator SayWellDone()
    {
        yield return new WaitForSeconds(2);

        wellDoneText.SetActive(true);
    }
}
