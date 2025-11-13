using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class ItemBase : MonoBehaviour
{
    [SerializeField]
    protected float speed_ = 3f;
    protected Camera camera_;
    protected Collider2D collider_;

    private void Awake()
    {
        camera_ = Camera.main;
        collider_ = GetComponent<Collider2D>();
    }

    protected virtual void Update()
    {
        // 右方向に移動
        transform.Translate(Vector3.right * speed_ * Time.deltaTime);

        // 画面外に出たら削除
        float worldScreenRight = camera_.orthographicSize * camera_.aspect;
        float boundSize = collider_.bounds.size.x;
        if (transform.position.x > worldScreenRight + boundSize)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion"))
        {
            Get();
        }
    }

    // 各アイテム固有の効果を定義
    public abstract void Get();
}
