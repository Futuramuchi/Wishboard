using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [Space]
    [SerializeField] private AudioClip clipAutograph;
    [SerializeField] private AudioClip clipAroma;
    [SerializeField] private AudioClip clipLegalBusiness;
    [SerializeField] private AudioClip clipDocumentalCinema;
    [SerializeField] private AudioClip clipClubJoin;
    [SerializeField] private AudioClip clipWatch;
    [SerializeField] private AudioClip clipLegalOlympic;
    [SerializeField] private AudioClip clipSuccessfulBuild;

    public static AudioPlayer Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySfx(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlaySuccessfulBuild() => PlaySfx(clipSuccessfulBuild);
}