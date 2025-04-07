using UnityEngine;
using UnityEngine.InputSystem;

public class CircleController : MonoBehaviour
{
    public float dashImpulse = 10f;

    public AudioClip clip_dash;
    private Rigidbody playerRigidbody;
    private SpriteRenderer SpriteRenderer;

    public Color regularDashColor;
    public Color cannotDashColor;
    void Start()
    {
        playerRigidbody = GameManager.playerTrans.GetComponent<Rigidbody>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
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

        if(GameManager.playerTrans.GetComponent<PlayerControllerShark>()._aboveWater) {
            SpriteRenderer.color = cannotDashColor;
        } else {
            SpriteRenderer.color = regularDashColor;
        }

        if (Input.GetButtonDown("Dash")
            && GameManager.playerTrans.GetComponent<PlayerControllerShark>()._aboveWater == false
            && GameManager.playerIsAlive == true
            && GameManager.playerInUpgradeMenu == false) {

            if (Cursor.visible == true) {
                Cursor.visible = false;
            }
            // GameManager.playerTrans.position = transform.position;
            GameManager.SpawnLoudAudio(clip_dash);
            playerRigidbody.AddForce((transform.position - GameManager.playerTrans.position).normalized * dashImpulse, ForceMode.Impulse);
        }

        // transform.localPosition = Vector3.ClampMagnitude(transform.position - GameManager.playerTrans.position, 15f);
    }
}
