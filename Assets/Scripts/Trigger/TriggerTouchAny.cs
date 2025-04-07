using UnityEngine;
using UnityEngine.Events;

public class TriggerTouchAny : MonoBehaviour
{
    public UnityEvent onTouchEvent;
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision col)
    {
        onTouchEvent.Invoke();
    }
    void OnTriggerEnter(Collider col) {
        onTouchEvent.Invoke();
    }
}
