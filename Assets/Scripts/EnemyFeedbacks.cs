using MoreMountains.Feedbacks;
using UnityEngine;

public class EnemyFeedbacks : MonoBehaviour
{
    [SerializeField] public Enemy enemy;

    [SerializeField] MMF_Player hitFeedback;
    private void Start()
    {
        enemy.onHit.AddListener(Hit);
        enemy.onDied.AddListener(Died);
    }

    private void Hit()
    {
        hitFeedback.PlayFeedbacks();
        AudioManager.Instance.PlaySfx(1f, SFX.EnemyDamaged, 1f);
    }
    
    private void Died()
    {

    }
}
