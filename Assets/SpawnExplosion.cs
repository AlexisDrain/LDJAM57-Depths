using UnityEngine;

public class SpawnExplosion : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Spawn()
    {
        GameManager.pool_Explosion.Spawn(transform.position);
    }
}
