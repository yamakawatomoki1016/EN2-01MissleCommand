using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Meteor : MonoBehaviour
{
    [SerializeField] private float fallSpeedMin_ = 1;
    [SerializeField] private float fallSpeedMax_ = 3;
    [SerializeField] private Explosion explosionPrefab_;
    [SerializeField] private ScoreEffect scoreEffectPrefab_;
    private BoxCollider2D groundCollider_;
    private Rigidbody2D rb_;
    private GameManager gameManager_;

    void Start()
    {
        rb_ = GetComponent<Rigidbody2D>();
        SetupVelocity();
    }

    public void Setup(BoxCollider2D ground, GameManager gameManager, Explosion explosionPrefab)
    {
        groundCollider_ = ground;
        gameManager_ = gameManager;
        explosionPrefab_ = explosionPrefab;
    }

    private void SetupVelocity()
    {
        float left = groundCollider_.bounds.center.x - groundCollider_.bounds.size.x / 2;
        float right = groundCollider_.bounds.center.x + groundCollider_.bounds.size.x / 2;
        float top = groundCollider_.bounds.center.y + groundCollider_.bounds.size.y / 2;

        float targetX = Mathf.Lerp(left, right, Random.Range(0f, 1f));

        Vector3 target = new Vector3(targetX, top, 0);
        Vector3 direction = (target - transform.position).normalized;
        float fallSpeed = Random.Range(fallSpeedMin_, fallSpeedMax_);

        rb_.linearVelocity = direction * fallSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion") && collision.TryGetComponent(out Explosion explosion))
        {
            TriggerExplosion(explosion);
        }

        if (collision.CompareTag("Ground"))
        {
            Fall();
        }
    }

    private void TriggerExplosion(Explosion otherExplosion)
    {
        // 連鎖数を加算
        int chainNum = otherExplosion.chainNum + 1;

        // スコア計算
        int score = chainNum * 100;
        gameManager_.AddScore(score);

        // スコアエフェクト生成
        if (scoreEffectPrefab_ != null)
        {
            // ここでインスタンスを生成して変数に代入
            ScoreEffect effect = Instantiate(scoreEffectPrefab_, transform.position, Quaternion.identity);

            // スコアを渡す
            effect.SetScore(score);
        }

        // 新しい爆発生成
        Explosion newExplosion = Instantiate(explosionPrefab_, transform.position, Quaternion.identity);
        newExplosion.chainNum = chainNum;

        // 隕石削除
        Destroy(gameObject);
    }

    private void Fall()
    {
        gameManager_.Damage(1);
        Destroy(gameObject);
    }
}
