using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EntityHealth : MonoBehaviour
{
    public Renderer flickerGraphics;
    public int defaultHealth = 1;
    private int currentHealth = 1;
    public UnityEvent onDamage;
    public UnityEvent onKill;
    void OnEnable() {
        if (flickerGraphics) {
            flickerGraphics.enabled = true;
        }
        currentHealth = defaultHealth;
    }
    private IEnumerator InvunrablePattern() {
        // _invunrable = true;
        flickerGraphics.enabled = false;
        yield return new WaitForSeconds(0.1f);
        flickerGraphics.enabled = true;
        yield return new WaitForSeconds(0.1f);
        flickerGraphics.enabled = false;
        yield return new WaitForSeconds(0.1f);
        flickerGraphics.enabled = true;
        yield return new WaitForSeconds(0.1f);
        flickerGraphics.enabled = false;
        yield return new WaitForSeconds(0.1f);
        flickerGraphics.enabled = true;
        // _invunrable = false;
    }

    // Update is called once per frame
    public void DamageEntity(int newDamage)
    {
        if (newDamage < 0) {
            newDamage = -newDamage;
        }

        currentHealth -= newDamage;

        if (currentHealth <= 0) {
            if(transform.position.y > 29) {
                GameManager.particles_BloodAboveWater.transform.position = transform.position;
                GameManager.particles_BloodAboveWater.Play();
            } else {
                GameManager.particles_Blood.transform.position = transform.position;
                GameManager.particles_Blood.Play();
            }
            transform.parent.GetComponent<TriggerSequence>().IncrementSequence();
            onKill.Invoke();
            gameObject.SetActive(false); // this will allow it to be revived
            // Destroy(gameObject);
        } else {
            if (flickerGraphics) {
                StartCoroutine("InvunrablePattern");
            }
            onDamage.Invoke();
        }
    }
}
