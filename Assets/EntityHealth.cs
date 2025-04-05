using UnityEngine;
using UnityEngine.Events;

public class EntityHealth : MonoBehaviour
{
    public int health = 1;
    // public UnityEvent onKill;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void DamageEntity(int newDamage)
    {
        if (newDamage < 0) {
            newDamage = -newDamage;
        }

        health -= newDamage;

        if (health <= 0) {

        }
    }
}
