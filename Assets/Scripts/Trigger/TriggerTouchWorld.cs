using UnityEngine;
using UnityEngine.Events;

public class TriggerTouchWorld : MonoBehaviour
{
    public UnityEvent onTouchEvent;
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision col)
    {
        if(col.collider.gameObject.layer == GameManager.worldMask) {
            onTouchEvent.Invoke();
        }
    }
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.layer == GameManager.worldMask) {
            onTouchEvent.Invoke();
        }
    }
}
