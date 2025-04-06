using System.Collections;
using UnityEngine;

public class EntityShooter : MonoBehaviour
{
    public Transform bulletStart;
    public Animator shootTimerSprite;

    public float shootImpulse = 1f;
    public float defaultReloadDelay = 1f;
    private float currentReloadDelay = 1f;
    public AudioClip powerUpSFX;
    public AudioClip shootSFX;
    void Start()
    {
        currentReloadDelay = defaultReloadDelay;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentReloadDelay > 0f) {
            currentReloadDelay -= Time.deltaTime;
        } else {
            currentReloadDelay = defaultReloadDelay;
            StartCoroutine("ShootPattern1");
        }
        /*
        if (GameManager.playerTrans.position.x < transform.position.x) {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } else {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        */
    }

    private IEnumerator ShootPattern1() {
        shootTimerSprite.GetComponent<Animator>().SetTrigger("ShootWarning");
        GetComponent<AudioSource>().clip = powerUpSFX;
        GetComponent<AudioSource>().PlayWebGL();
        yield return new WaitForSeconds(1f);
        ShootSpear();
        yield return new WaitForSeconds(0.2f);
        ShootSpear();
        yield return new WaitForSeconds(0.2f);
        ShootSpear();
    }

    private void ShootSpear() {
        Vector3 direction = (GameManager.playerTrans.position - bulletStart.position).normalized;

        GameObject spear = GameManager.pool_EnemySpear.Spawn(bulletStart.position);
        spear.GetComponent<Rigidbody>().AddForce(direction * shootImpulse, ForceMode.Impulse);

        GetComponent<AudioSource>().clip = shootSFX;
        GetComponent<AudioSource>().PlayWebGL();

        // ignore shooter
        if (spear.GetComponent<BulletStats>().ignoreShooter) {
            Physics.IgnoreCollision(spear.GetComponent<Collider>(), spear.GetComponent<BulletStats>().ignoreShooter, false);
        }
        spear.GetComponent<BulletStats>().ignoreShooter = GetComponent<Collider>();
        Physics.IgnoreCollision(spear.GetComponent<Collider>(), spear.GetComponent<BulletStats>().ignoreShooter, true);
    }
}
