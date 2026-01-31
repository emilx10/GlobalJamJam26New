using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [Header("Settings")]
    [SerializeField] public AudioMixer audioMixer; //Unused yet
    [SerializeField] public AudioPool sfxPool;

    [Header("Sounds")]
    [Header("SFX")]
    [SerializeField] public AudioClip sfx_Death;
    [SerializeField] public AudioClip sfx_EnemyDamaged;
    [SerializeField] public AudioClip sfx_PlayerHit;
    [SerializeField] public AudioClip sfx_SoulCollect1;
    [SerializeField] public AudioClip sfx_SoulCollect2;
    [SerializeField] public AudioClip sfx_SoulDrop;
    [SerializeField] public AudioClip sfx_Thrust;
    [SerializeField] public AudioClip music;
    public void Awake()
    {
        if (!Instance.IsUnityNull())
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void PlaySfx(float volume, AudioClip audio, float pitch)
    {
        sfxPool.PlaySound(volume, audio, pitch);
    }

    private void PlaySfxInWorld(float volume, SFX sfx, float pitch, Transform position, float lifeTime)
    {

    }

    public void PlaySfx(float volume, SFX sfx, float pitch)
    {
        switch (sfx)
        {
            case SFX.Death:
                PlaySfx(volume, sfx_Death, pitch);
                break;
            case SFX.EnemyDamaged:
                PlaySfx(volume, sfx_EnemyDamaged, pitch);
                break;
            case SFX.PlayerHit:
                PlaySfx(volume, sfx_PlayerHit, pitch);
                break;
            case SFX.SoulCollect1:
                PlaySfx(volume, sfx_SoulCollect1, pitch);
                break;
            case SFX.SoulCollect2:
                PlaySfx(volume, sfx_SoulCollect2, pitch);
                break;
            case SFX.SoulDrop:
                PlaySfx(volume, sfx_SoulDrop, pitch);
                break;
            case SFX.Thrust:
                PlaySfx(volume, sfx_Thrust, pitch);
                break;
            case SFX.Music:
                PlayMusic(volume, music, pitch);
                break;
            default:
                break;
        }
    }
    public void PlayMusic(float volume, AudioClip audio, float pitch)
    {
        sfxPool.PlayMusic(volume, audio, pitch);
    }
    public float GetRandomPitch(float min, float max)
    {
        return Random.Range(min, max);
    }
}
