using UnityEngine;
using UnityEngine.UI;

public class BottomBarFill : MonoBehaviour
{
    private Image myImage;
    public float target;
    void Start()
    {
        myImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(target == 1f) {
            myImage.fillAmount = 0f;
        }
        else if(target >= 0.01f) {
            myImage.fillAmount = Mathf.Lerp(myImage.fillAmount, target, 3f * Time.deltaTime);
            
        } else {
            myImage.fillAmount = 0f;
        }
    }
}
