using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

    [SerializeField] private AudioSource soundFxObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
    }
   
    public void PlaySound(AudioClip clip, Transform spawnTransform, float volume)
    {
        //spawn in GameObject to play the sound at the position of the spawnTransform
        AudioSource audioSource = Instantiate(soundFxObject, spawnTransform.position, Quaternion.identity);

        //assign the clip to the audio source and play it
        audioSource.clip = clip;

        //assign the volume to the audio source and play it
        audioSource.volume = volume;

        //play the sound
        audioSource.Play();

        //get the length of the clip and destroy the audio source after it has finished playing
        float clipLength = clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
}
