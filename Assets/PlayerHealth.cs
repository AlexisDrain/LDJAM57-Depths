using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth = 3;

    public UnityEvent onDamageEvent;
    public UnityEvent onDeathEvent;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void DamagePlayer(int newDamage)
    {
        if(newDamage < 0) {
            newDamage = -newDamage;
        }
        currentHealth -= newDamage;

        if (currentHealth <= 0) {
            onDeathEvent.Invoke();
            GameManager.playerIsAlive = false;
        } else {
            onDamageEvent.Invoke();
        }
    }
}
