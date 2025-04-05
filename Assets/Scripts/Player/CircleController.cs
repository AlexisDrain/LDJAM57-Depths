using UnityEngine;
using UnityEngine.InputSystem;

public class CircleController : MonoBehaviour
{
    public float dashImpulse = 10f;

    private Rigidbody playerRigidbody;
    void Start()
    {
        playerRigidbody = GameManager.playerTrans.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 18f));

        transform.localPosition = Vector3.ClampMagnitude(transform.localPosition, 7.5f);

        // rotation
        Vector3 vec1 = GameManager.playerTrans.position;
        Vector3 vec2 = transform.position;
        float angle = Mathf.Atan2(vec2.y - vec1.y, vec2.x - vec1.x) * 180 / Mathf.PI;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));


        if (Input.GetButtonDown("Dash")
            && GameManager.playerTrans.GetComponent<PlayerControllerShark>()._aboveWater == false) {

            if (Cursor.visible == true) {
                Cursor.visible = false;
            }
            // GameManager.playerTrans.position = transform.position;
            playerRigidbody.AddForce((transform.position - GameManager.playerTrans.position).normalized * dashImpulse, ForceMode.Impulse);
        }

        // transform.localPosition = Vector3.ClampMagnitude(transform.position - GameManager.playerTrans.position, 15f);
    }
}
