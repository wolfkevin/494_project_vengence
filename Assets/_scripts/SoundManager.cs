using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    AudioSource musicSource;
    public static SoundManager instance = null;

    void Awake()
    {
			musicSource = GetComponent<AudioSource>();
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy (gameObject);

        DontDestroyOnLoad (gameObject);
    }

    public void PlayMusic()
    {
        musicSource.Play ();
    }

    public void StopMusic()
    {
        Destroy(this.gameObject);
        // musicSource.Stop ();
    }
}
