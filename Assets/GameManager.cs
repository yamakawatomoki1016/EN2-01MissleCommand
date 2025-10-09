using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField, Header("Prefabs")]
    private Explosion explosionPrefab_;
    [SerializeField]
    private Meteor meteorPrefab_;
    [SerializeField, Header("MeteorSpawner")]
    private BoxCollider2D ground_;
    [SerializeField]
    private float meteorInterval_ = 1;
    private float meteorTimer_;
    private Camera mainCamera_;
    [SerializeField]
    private List<Transform> spawnPositions_;
    [SerializeField, Header("ScoreUISettings")]
    private ScoreText scoreText_;
    private int score_;
    [SerializeField, Header("LifeUISettings")]
    private LifeBar lifeBar_;
    [SerializeField]
    private float maxLife_ = 10;
    private float life_;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void AddScore(int point)
    {
        score_ += point;
        scoreText_.SetScore(score_);
    }
    private void ResetLife() {
        life_ = maxLife_;
        UpdateLifeBar();
    }

    private void UpdateLifeBar() {
        float lifeRatio = Mathf.Clamp01(life_ / maxLife_);
        lifeBar_.SetGaugeRatio(lifeRatio);
    }
    public void Damage(int point) { 
        life_ -= point;
        UpdateLifeBar();
    }
    void Start()
    {
        GameObject mainCameraObject =
            GameObject.FindGameObjectWithTag("MainCamera");
        bool isGetComponent = mainCameraObject.TryGetComponent(out mainCamera_);
        Assert.IsTrue(isGetComponent, "MainCamera�R���|�[�����g������܂���");
        Assert.IsTrue(spawnPositions_.Count > 0, "spawnPositions_�ɗv�f��1������܂���B");
        foreach (Transform t in spawnPositions_)
        {
            Assert.IsNotNull(t, "spawnPositions_��Null���܂܂�Ă��܂��B");
        }
        ResetLife();
    }

    private void GenerateExplosion()
    {
        Vector3 clickPosition = mainCamera_.ScreenToWorldPoint(Input.mousePosition);
        clickPosition.z = 0;
        Explosion explosion = Instantiate(explosionPrefab_, clickPosition, Quaternion.identity);
    }

    private void UpdateMeteorTimer()
    {
        meteorTimer_ -= Time.deltaTime;
        if (meteorTimer_ > 0) { return; }
        meteorTimer_ += meteorInterval_;
        GenerateMeteor();
    }
    private void GenerateMeteor()
    {
        //Vector3 spawnPosition = new Vector3(0,6,0);
        int max = spawnPositions_.Count;
        int posIndex = Random.Range(0, max);
        Vector3 spawnPosition = spawnPositions_[posIndex].position;
        Meteor meteor = Instantiate(meteorPrefab_, spawnPosition, Quaternion.identity);
        meteor.Setup(ground_, this, explosionPrefab_);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GenerateExplosion();
        }
        UpdateMeteorTimer();
    }
}