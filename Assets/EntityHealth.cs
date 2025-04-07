using UnityEngine;
using UnityEngine.Events;

public class EntityHealth : MonoBehaviour
{
    public int health = 1;
    public UnityEvent onDamage;
    public UnityEvent onKill;
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
            if(transform.position.y > 29) {
                GameManager.particles_BloodAboveWater.transform.position = transform.position;
                GameManager.particles_BloodAboveWater.Play();
            } else {
                GameManager.particles_Blood.transform.position = transform.position;
                GameManager.particles_Blood.Play();
            }
            transform.parent.GetComponent<TriggerSequence>().IncrementSequence();
            onKill.Invoke();
            Destroy(gameObject);
        } else {
            onDamage.Invoke();
        }
    }
}
