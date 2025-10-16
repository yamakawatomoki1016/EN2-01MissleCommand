using UnityEngine;
using TMPro; // ← 追加（TextMeshProを使うため）

// TMP_Text（TextMeshPro - Text）を必須にする
[RequireComponent(typeof(TMP_Text))]
public class ScoreEffect : MonoBehaviour
{
    // 上昇速度
    [SerializeField]
    float upSpeed_ = 1f;

    // 消滅までの時間（秒）
    [SerializeField]
    float aliveTime_ = 1f;

    // 経過時間カウンタ
    float aliveTimer_ = 0f;

    // スコアを受け取って表示する関数
    public void SetScore(int score)
    {
        // RequireComponentがあるのでGetComponentはnullにならない
        GetComponent<TMP_Text>().text = score.ToString();
    }

    // Updateは毎フレーム呼ばれる
    void Update()
    {
        // 経過時間をカウント
        aliveTimer_ += Time.deltaTime;

        // 表示時間を過ぎたら削除
        if (aliveTimer_ >= aliveTime_)
        {
            Destroy(gameObject);
        }

        // 上方向に移動
        transform.Translate(Vector3.up * upSpeed_ * Time.deltaTime);
    }
}
