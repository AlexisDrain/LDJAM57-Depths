using UnityEngine;

public class BulletStats : MonoBehaviour
{
    public float shootForce = 1f;
    public float maxSpeedFull = 1f;
    public float maxSpeedSlowdown = 1f;
    public float rotateTowardsTargetSpeed = 0f;
    private Rigidbody myRigidbody;

    [Header("Read only")]
    public Vector3 direction;
    public Collider ignoreShooter;

    public bool isSlowDown = false;

    public void Start() {
        myRigidbody = GetComponent<Rigidbody>();
    }
    public void OnEnable() {
        SlowdownBullet(false);
    }

    public void SlowdownBullet(bool newState)
    {
        isSlowDown = newState;
    }
    public void FixedUpdate() {
        myRigidbody.AddForce(direction * shootForce, ForceMode.Force); // direction is set by other entities. Like EntityShooter;

        if (rotateTowardsTargetSpeed > 0f) {
            Vector3 newDirection = Vector3.RotateTowards(direction, (GameManager.playerTrans.position - transform.position).normalized, rotateTowardsTargetSpeed, 0f);
            direction = newDirection;
        }

        if(isSlowDown) {
            myRigidbody.linearVelocity = Vector3.ClampMagnitude(myRigidbody.linearVelocity, maxSpeedSlowdown);

            if(GameManager.playerCircle._isSlowingDown == false) {
                SlowdownBullet(false);
            }
        } else {
            myRigidbody.linearVelocity = Vector3.ClampMagnitude(myRigidbody.linearVelocity, maxSpeedFull);
        }
    }
}
