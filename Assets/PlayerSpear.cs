using UnityEngine;
using UnityEngine.UIElements;

public class PlayerSpear : MonoBehaviour {

    public float charge = 0f;

    public float horizontalMoveForce = 10f;
    public float maxHorizontalSpeed = 10f;
    public float horizontalDrag = 0.9f;
    public float verticalMoveImpulse = 10f;
    public float gravityForce = 10f;
    private Rigidbody myRigidbody;
    private SpriteRenderer myRenderer;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myRenderer = transform.Find("Graphics").GetComponent<SpriteRenderer>();
    }


    void FixedUpdate()
    {
        myRigidbody.AddForce(Vector3.down * gravityForce, ForceMode.Force);

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h < -0.1f) {
            myRenderer.flipX = true;
        } else if (h > 0.1f) {
            myRenderer.flipX = false;
        }

        if (Mathf.Abs(h) > 0.1f) {
            // move
            myRigidbody.AddForce(Vector3.right * h * horizontalMoveForce, ForceMode.Force);
        } else {
            // drag while not moving
            myRigidbody.linearVelocity = new Vector3(myRigidbody.linearVelocity.x * horizontalDrag,
                                                 myRigidbody.linearVelocity.y,
                                                 myRigidbody.linearVelocity.z);
        }


            // clamp velocity
            myRigidbody.linearVelocity = new Vector3(Mathf.Clamp(myRigidbody.linearVelocity.x, -maxHorizontalSpeed, maxHorizontalSpeed),
                myRigidbody.linearVelocity.y,
                myRigidbody.linearVelocity.z);
        

    }
}
