using MoreMountains.Feedbacks;
using UnityEngine;

public class PlayerFeedbacks : MonoBehaviour
{
    [SerializeField] MMF_Player slashHit;

    [SerializeField] PlayerController playerController;

    private void Start()
    {
        playerController.onSlash.AddListener(Slash);
    }

    private void Slash()
    {
        slashHit.PlayFeedbacks();
    }
}
