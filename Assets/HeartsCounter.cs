using UnityEngine;
using UnityEngine.UI;

public class HeartsCounter : MonoBehaviour
{
    public Sprite heart_full;
    public Sprite heart_empty;

    public Image heart1;
    public Image heart2;
    public Image heart3;
    public Image heart4;
    public Image heart5;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void SetCurrentHealth(int newHealth)
    {
        // yes. I know this is silly.
        if (newHealth == 0) {
            heart1.sprite = heart_empty;
            heart2.sprite = heart_empty;
            heart3.sprite = heart_empty;
            heart4.sprite = heart_empty;
            heart5.sprite = heart_empty;
        }
        if (newHealth == 1) {
            heart1.sprite = heart_full;
            heart2.sprite = heart_empty;
            heart3.sprite = heart_empty;
            heart4.sprite = heart_empty;
            heart5.sprite = heart_empty;
        }
        if (newHealth == 2) {
            heart1.sprite = heart_full;
            heart2.sprite = heart_full;
            heart3.sprite = heart_empty;
            heart4.sprite = heart_empty;
            heart5.sprite = heart_empty;
        }
        if (newHealth == 3) {
            heart1.sprite = heart_full;
            heart2.sprite = heart_full;
            heart3.sprite = heart_full;
            heart4.sprite = heart_empty;
            heart5.sprite = heart_empty;
        }
        if (newHealth == 4) {
            heart1.sprite = heart_full;
            heart2.sprite = heart_full;
            heart3.sprite = heart_full;
            heart4.sprite = heart_full;
            heart5.sprite = heart_empty;
        }
        if (newHealth == 5) {
            heart1.sprite = heart_full;
            heart2.sprite = heart_full;
            heart3.sprite = heart_full;
            heart4.sprite = heart_full;
            heart5.sprite = heart_full;
        }
    }
}
