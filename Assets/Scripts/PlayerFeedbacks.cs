using MoreMountains.Feedbacks;
using UnityEngine;

public class PlayerFeedbacks : MonoBehaviour
{
    [SerializeField] MMF_Player slashHit;
    [SerializeField] MMF_Player slashStab;

    [SerializeField] PlayerController playerController;

    private void Start()
    {
        playerController.onSlash.AddListener(Slash);
        playerController.onStabbed.AddListener(Stab);
    }

    private void Slash()
    {
        slashHit.PlayFeedbacks();
    }

    private void Stab()
    {
        slashStab.PlayFeedbacks();
    }
}
