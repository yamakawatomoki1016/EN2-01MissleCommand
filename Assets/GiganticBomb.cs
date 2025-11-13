using UnityEngine;

public class GiganticBomb : ItemBase
{
    [SerializeField]
    private Explosion giganticExplosionPrefab_;

    public override void Get()
    {
        Instantiate(giganticExplosionPrefab_, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
