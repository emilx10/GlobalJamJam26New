using UnityEngine;

public class AmbientDustEmitter : MonoBehaviour
{
    public Sprite dustSprite;
    public float spawnRate = 0.5f;
    public Vector2 spawnAreaSize = new Vector2(500, 500);
    public Color dustColor = new Color(1, 1, 1, 0.5f);

    private float timer;

    void Update()
    {
        timer += Time.unscaledDeltaTime;

        if (timer >= spawnRate)
        {
            SpawnAmbientDust();
            timer = 0;
        }
    }

    void SpawnAmbientDust()
    {
        if (DustPooler.Instance == null) return;

        CanvasDustParticle p = DustPooler.Instance.GetParticle();

        Vector2 randomPos = new Vector2(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
        );

        p.Initialize(dustSprite, randomPos, dustColor, Random.Range(0.2f, 0.5f));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, spawnAreaSize.y, 0));
    }
}