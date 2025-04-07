using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth = 5;

    public SpriteRenderer playerSprite;
    public UnityEvent onDamageEvent;
    public UnityEvent onDeathEvent;
    private bool _invunrable = false;
    void OnEnable()
    {
        _invunrable = false;
    }

    private IEnumerator InvunrablePattern() {
        _invunrable = true;
        playerSprite.enabled = false;
        yield return new WaitForSeconds(0.1f);
        playerSprite.enabled = true;
        yield return new WaitForSeconds(0.1f);
        playerSprite.enabled = false;
        yield return new WaitForSeconds(0.1f);
        playerSprite.enabled = true;
        yield return new WaitForSeconds(0.1f);
        playerSprite.enabled = false;
        yield return new WaitForSeconds(0.1f);
        playerSprite.enabled = true;
        _invunrable = false;
    }
    public void HealPlayer(int newHeal) {
        currentHealth += currentHealth;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        GameManager.heartsCounter.SetCurrentHealth(currentHealth);
    }
    public void DamagePlayer(int newDamage)
    {
        if(newDamage < 0) {
            newDamage = -newDamage;
        }
        if (_invunrable || currentHealth <= 0) {
            print("player can't be damaged");
            return;
        }
        currentHealth -= newDamage;
        GameManager.heartsCounter.SetCurrentHealth(currentHealth);

        if (currentHealth <= 0) {
            onDeathEvent.Invoke();
            GameManager.playerIsAlive = false;
            GameManager.pressR.SetActive(true);
        } else {
            StartCoroutine("InvunrablePattern");
            onDamageEvent.Invoke();
        }
    }
}
