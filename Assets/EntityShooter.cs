using System.Collections;
using System.Reflection;
using UnityEngine;

public enum ShooterType {
    Spear,
    Submarine,
    Eyeball
}

public class EntityShooter : MonoBehaviour
{
    public ShooterType shootType;
    public Transform bulletStart;
    public Animator shootTimerSprite;

    public float shootDistance = 10f;
    public float shootImpulse = 1f;
    public float defaultReloadDelay = 1f;
    private float currentReloadDelay = 1f;
    public AudioClip powerUpSFX;
    public AudioClip shootSFX;
    public Collider overrideCollider;
    void Start()
    {
        currentReloadDelay = defaultReloadDelay;
        if(overrideCollider == null) {
            overrideCollider = GetComponent<Collider>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentReloadDelay > 0f) {
            currentReloadDelay -= Time.deltaTime;
        } else {
            currentReloadDelay = defaultReloadDelay;
            if (Vector2.Distance(bulletStart.position, GameManager.playerTrans.position) < shootDistance) {
                if (shootType == ShooterType.Spear) {
                    StartCoroutine("ShootPattern1");
                }
                if (shootType == ShooterType.Submarine) {
                    StartCoroutine("ShootPattern2");
                }
                if (shootType == ShooterType.Eyeball) {
                    StartCoroutine("ShootPattern3");
                }
            }
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
    private IEnumerator ShootPattern2() {
        shootTimerSprite.GetComponent<Animator>().SetTrigger("ShootWarning");
        GetComponent<AudioSource>().clip = powerUpSFX;
        GetComponent<AudioSource>().PlayWebGL();
        yield return new WaitForSeconds(1f);
        ShootMissile();
    }
    private IEnumerator ShootPattern3() {
        shootTimerSprite.GetComponent<Animator>().SetTrigger("ShootWarning");
        GetComponent<AudioSource>().clip = powerUpSFX;
        GetComponent<AudioSource>().PlayWebGL();
        yield return new WaitForSeconds(1f);
        ShootEyeball();
    }

    private void ShootMissile() {
        Vector3 direction = (GameManager.playerTrans.position - bulletStart.position).normalized;

        GameObject missile = GameManager.pool_EnemyMissile.Spawn(bulletStart.position);
        // missile.GetComponent<Rigidbody>().AddForce(direction * shootImpulse, ForceMode.Impulse);
        missile.GetComponent<BulletStats>().direction = direction;

        GetComponent<AudioSource>().clip = shootSFX;
        GetComponent<AudioSource>().PlayWebGL();

        // ignore shooter
        if (missile.GetComponent<BulletStats>().ignoreShooter) {
            Physics.IgnoreCollision(missile.GetComponent<Collider>(), missile.GetComponent<BulletStats>().ignoreShooter, false);
        }
        missile.GetComponent<BulletStats>().ignoreShooter = overrideCollider;
        Physics.IgnoreCollision(missile.GetComponent<Collider>(), missile.GetComponent<BulletStats>().ignoreShooter, true);
    }

    private void ShootSpear() {
        Vector3 direction = (GameManager.playerTrans.position - bulletStart.position).normalized;

        GameObject spear = GameManager.pool_EnemySpear.Spawn(bulletStart.position);
        // spear.GetComponent<Rigidbody>().AddForce(direction * shootImpulse, ForceMode.Impulse);
        spear.GetComponent<BulletStats>().direction = direction;

        GetComponent<AudioSource>().clip = shootSFX;
        GetComponent<AudioSource>().PlayWebGL();

        // ignore shooter
        if (spear.GetComponent<BulletStats>().ignoreShooter) {
            Physics.IgnoreCollision(spear.GetComponent<Collider>(), spear.GetComponent<BulletStats>().ignoreShooter, false);
        }
        spear.GetComponent<BulletStats>().ignoreShooter = overrideCollider;
        Physics.IgnoreCollision(spear.GetComponent<Collider>(), spear.GetComponent<BulletStats>().ignoreShooter, true);
    }
    private void ShootEyeball() {
        Vector3 direction = (GameManager.playerTrans.position - bulletStart.position).normalized;

        GameObject eyeball = GameManager.pool_EnemyEyeBullet.Spawn(bulletStart.position);
        // spear.GetComponent<Rigidbody>().AddForce(direction * shootImpulse, ForceMode.Impulse);
        eyeball.GetComponent<BulletStats>().direction = direction;

        GetComponent<AudioSource>().clip = shootSFX;
        GetComponent<AudioSource>().PlayWebGL();

        // ignore shooter
        if (eyeball.GetComponent<BulletStats>().ignoreShooter) {
            Physics.IgnoreCollision(eyeball.GetComponent<Collider>(), eyeball.GetComponent<BulletStats>().ignoreShooter, false);
        }
        eyeball.GetComponent<BulletStats>().ignoreShooter = overrideCollider;
        Physics.IgnoreCollision(eyeball.GetComponent<Collider>(), eyeball.GetComponent<BulletStats>().ignoreShooter, true);
    }
}
