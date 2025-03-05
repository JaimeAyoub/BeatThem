
using UnityEngine;


//Efectos de sonidos sacados de :  https://shapeforms.itch.io/shapeforms-audio-free-sfx
public class AudioManager : MonoBehaviour
{
    [Header("---------Audio Source----------")] [SerializeField]
    AudioSource BGMSource;


    [SerializeField] AudioSource SFXsource;

    [Header("---------Audio Clip----------")] [SerializeField]
    AudioClip BGM;

    public AudioClip HitSFX;
    public AudioClip TakeDamageSFX;
    public AudioClip DieSFX;
    
    public static AudioManager instance; 

    void Awake()
    {
      
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        BGMSource.clip = BGM;
        BGMSource.Play();
        
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXsource.PlayOneShot(clip);
    }
}