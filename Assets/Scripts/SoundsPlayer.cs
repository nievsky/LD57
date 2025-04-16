using UnityEngine;
using UnityEngine.SceneManagement;
public class SoundsPlayer : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        // Debug checks
        if (audioSource == null)
            Debug.LogError("No AudioSource component found!");
        
        if (audioClips == null || audioClips.Length == 0)
            Debug.LogError("No AudioClips assigned!");
            
        Debug.Log("SoundsPlayer initialized with " + audioClips.Length + " clips");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision detected with: " + other.gameObject.name + " (Tag: " + other.gameObject.tag + ")");
        
        if (other.gameObject.CompareTag("BlackTag"))
        {
            if (audioClips.Length > 0 && audioClips[0] != null)
            {
                audioSource.PlayOneShot(audioClips[0]);
                Debug.Log("Playing BlackTag sound");
            }
        }
        else
        {
            int index = Random.Range(1, audioClips.Length);
            if (index < audioClips.Length && audioClips[index] != null)
            {
                audioSource.PlayOneShot(audioClips[index]);
                Debug.Log("Playing random sound: " + audioClips[index].name);
            }
        }
    }
}
