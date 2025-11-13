using UnityEngine;

public class ClusterBombItem : ItemBase
{
    [SerializeField]
    private Explosion explosionPrefab_;
    // 取得して爆発状態かどうかを判断する
    bool isGet = false;
    // 爆発し続ける時間
    private float explosionEmmitionTimer_ = 3;
    // 細かな爆発を生成する間隔
    private float explosionInterval_ = 0.2f;
    private float explosionTimer_ = 0.0f;
    private SpriteRenderer renderer_;
    public override void Get()
    {
        if (TryGetComponent(out renderer_))
        {
            renderer_.enabled = false;
        }
        collider_.enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        isGet = true;
    }
    // 右移動だけでない処理をUpdateで行なうためoverrideする。
    protected override void Update() {
        if (!isGet)
        {
            base.Update();
            return;
        }
        explosionTimer_ -= Time.deltaTime;
        if(explosionEmmitionTimer_ <= 0)
        {
            Destroy(gameObject);
        }
        UpdateClusterExplosion();
    }
    // 小さな爆発を起こす
    private void UpdateClusterExplosion() {
        explosionEmmitionTimer_ -= Time.deltaTime;
        if(explosionTimer_ > 0) { return; }
        float randomWidth = 2;
        Vector3 offset = new Vector3(
            Random.Range(-randomWidth,randomWidth),
            Random.Range(-randomWidth,randomWidth),
            0
            );
        Instantiate(
            explosionPrefab_,
            transform.position + offset,
            Quaternion.identity
            );
        explosionTimer_ += explosionInterval_;
    }

 

}