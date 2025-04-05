using UnityEngine;
using UnityEngine.Events;

public class TriggerPlayerTouch : MonoBehaviour
{
    public UnityEvent onTouch;
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision col)
    {
        if(col.collider.CompareTag("Player")) {
            onTouch.Invoke();
        }
    }
    void OnTriggerEnter(Collider col) {
        if (col.CompareTag("Player")) {
            onTouch.Invoke();
        }
    }
}
