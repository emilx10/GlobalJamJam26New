using UnityEngine;

public class ChaosOrb : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayRandomSFX();
            CurrencyManager.Instance.Add(1);
            Destroy(gameObject);
        }
    }

    public void PlayRandomSFX()
    {
        int randomChoice = Random.Range(0, 2);

        SFX selectedSound = (randomChoice == 0) ? SFX.SoulCollect1 : SFX.SoulCollect2;

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySfx(1f, selectedSound, 1f);
        }
    }
}