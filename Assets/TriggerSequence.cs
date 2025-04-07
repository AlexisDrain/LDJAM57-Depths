using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerSequence : MonoBehaviour
{
    [Header("Read only. Calculated automatically")]
    public int sequenceTotal = 0;
    public int current_sequence = 0;

    [Header("Deprecated")]
    public UnityEvent sequenceEvent;
    void OnEnable()
    {
        current_sequence = 0;
        sequenceTotal = 0;
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(true);
            sequenceTotal += 1;
        }
        /* include inActive objects after we deactivate them
        for (int i = 0; i < children.Count; i++) {
            children[i].SetActive(true);
            sequenceTotal += 1;
        }
        */
    }

    // Update is called once per frame
    public void IncrementSequence()
    {

        current_sequence += 1;
        GameManager.totalKills += 1;

        GameManager.text_totalKills.text = $"Kills: {GameManager.totalKills}";
        float ratio = (float)current_sequence / (float)sequenceTotal;
        GameManager.bottomBarFill.GetComponent<BottomBarFill>().target = ratio;
        // no lerp
        //GameManager.bottomBarFill.fillAmount = (float)current_sequence / (float)sequenceTotal;

        if (current_sequence >= sequenceTotal) {
            GameManager.currentWave += 1;
            GameManager.myGameManager.SetNewWave(GameManager.currentWave);
            // sequenceEvent.Invoke();
        }
    }
}
