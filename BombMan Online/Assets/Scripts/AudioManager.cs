using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip explosion;
    public AudioClip pickup;
    public AudioClip spawn;
    public AudioClip death;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudioWithName(string name)
    {
        switch (name)
        {
            case "explosion":
                {
                    GetComponent<AudioSource>().clip = explosion;
                    GetComponent<AudioSource>().Play();
                    break;
                }
            case "pickup":
                {
                    GetComponent<AudioSource>().clip = pickup;
                    GetComponent<AudioSource>().Play();
                    break;
                }
            case "spawn":
                {
                    GetComponent<AudioSource>().clip = spawn;
                    GetComponent<AudioSource>().Play();
                    break;
                }
            case "death":
                {
                    GetComponent<AudioSource>().clip = death;
                    GetComponent<AudioSource>().Play();
                    break;
                }
        }
        
    }
}
