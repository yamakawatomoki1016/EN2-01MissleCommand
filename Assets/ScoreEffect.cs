using UnityEngine;
using TMPro; // �� �ǉ��iTextMeshPro���g�����߁j

// TMP_Text�iTextMeshPro - Text�j��K�{�ɂ���
[RequireComponent(typeof(TMP_Text))]
public class ScoreEffect : MonoBehaviour
{
    // �㏸���x
    [SerializeField]
    float upSpeed_ = 1f;

    // ���ł܂ł̎��ԁi�b�j
    [SerializeField]
    float aliveTime_ = 1f;

    // �o�ߎ��ԃJ�E���^
    float aliveTimer_ = 0f;

    // �X�R�A���󂯎���ĕ\������֐�
    public void SetScore(int score)
    {
        // RequireComponent������̂�GetComponent��null�ɂȂ�Ȃ�
        GetComponent<TMP_Text>().text = score.ToString();
    }

    // Update�͖��t���[���Ă΂��
    void Update()
    {
        // �o�ߎ��Ԃ��J�E���g
        aliveTimer_ += Time.deltaTime;

        // �\�����Ԃ��߂�����폜
        if (aliveTimer_ >= aliveTime_)
        {
            Destroy(gameObject);
        }

        // ������Ɉړ�
        transform.Translate(Vector3.up * upSpeed_ * Time.deltaTime);
    }
}
