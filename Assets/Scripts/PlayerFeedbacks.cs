using MoreMountains.Feedbacks;
using UnityEngine;

public class PlayerFeedbacks : MonoBehaviour
{
    [SerializeField] MMF_Player slashHit;
    [SerializeField] MMF_Player slashStab;
    [SerializeField] MMF_Player PlayerHitf;

    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerStats player;

    private void Start()
    {
        playerController.onSlash.AddListener(Slash);
        playerController.onStabbed.AddListener(Stab);
        player.onHitPlayer.AddListener(PlayerHit);
        player.onDied.AddListener(PlayerHit);
    }

    private void Slash()
    {
        slashHit.PlayFeedbacks();
    }

    private void Stab()
    {
        slashStab.PlayFeedbacks();
    }

    private void PlayerDied() { AudioManager.Instance.PlaySfx(1f, SFX.Death, 1f); }

    private void PlayerHit()
    {
        AudioManager.Instance.PlaySfx(1f, SFX.PlayerHit, 1f);

        PlayerHitf.PlayFeedbacks();
    }
}
