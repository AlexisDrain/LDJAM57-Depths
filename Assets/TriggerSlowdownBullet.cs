using UnityEditor.DeviceSimulation;
using UnityEngine;

public class TriggerSlowdownBullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision col) {
        if (col.collider.GetComponent<BulletStats>()) {
            col.collider.GetComponent<BulletStats>().SlowdownBullet(true);
        }
    }
    void OnTriggerEnter(Collider col) {
        if (col.GetComponent<BulletStats>()) {
            col.GetComponent<BulletStats>().SlowdownBullet(true);
        }
    }

    void OnCollisionExit(Collision col) {
        if (col.collider.GetComponent<BulletStats>()) {
            col.collider.GetComponent<BulletStats>().SlowdownBullet(false);
        }
    }
    void OnTriggerExit(Collider col) {
        if (col.GetComponent<BulletStats>()) {
            col.GetComponent<BulletStats>().SlowdownBullet(false);
        }
    }

}
