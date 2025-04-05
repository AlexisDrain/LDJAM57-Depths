using UnityEngine;
using UnityEngine.Events;

public class TriggerSequence : MonoBehaviour
{
    public int sequenceTotal = 0;
    public int current_sequence = 0;

    public UnityEvent sequenceEvent;
    void Start()
    {
        sequenceTotal = 0;
        for (int i = 0; i < transform.childCount; i++) {
            sequenceTotal += 1;
        }
    }

    // Update is called once per frame
    public void IncrementSequence()
    {
        current_sequence += 1;

        GameManager.bottomBarFill.fillAmount = (float)current_sequence / (float)sequenceTotal;

        if (current_sequence >= sequenceTotal) {
            sequenceEvent.Invoke();
        }
    }
}
