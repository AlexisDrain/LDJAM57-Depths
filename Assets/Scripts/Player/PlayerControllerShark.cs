using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControllerShark : MonoBehaviour {

    public float horizontalMoveForce = 10f;
    public float maxVelocity = 10f;
    // public float horizontalDrag = 0.9f;
    public float drag = 0.9f;
    public float verticalMoveImpulse = 10f;
    public float gravityForce = 10f;
    private Rigidbody myRigidbody;

    public AudioClip clip_SplashUp;
    public AudioClip clip_SplashDown;

    public float waterHeight = 10f;
    [Header("Read only")]
    public bool _aboveWater = false;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();

    }

    //void Update() {
    //
    //}
    void FixedUpdate()
    {
        // player Dead body
        if(GameManager.playerIsAlive == false) {
            if (_aboveWater == false) {
                myRigidbody.AddForce(Vector3.up * gravityForce, ForceMode.Force);
            }
            if (transform.position.y > waterHeight) {
                _aboveWater = true;
                myRigidbody.linearVelocity = new Vector3(myRigidbody.linearVelocity.x * drag,
                                                     myRigidbody.linearVelocity.y * drag,
                                                     myRigidbody.linearVelocity.z);
            }
            return;
        }

        if(transform.position.y > waterHeight) {
            if(_aboveWater == false) {
                GameManager.SpawnLoudAudio(clip_SplashUp);
                _aboveWater = true;
                GameManager.particles_Water.transform.position = transform.position;
                GameManager.particles_Water.Play();
            }
            myRigidbody.AddForce(Vector3.down * gravityForce, ForceMode.Force);
        } else {
            if (_aboveWater == true) {
                GameManager.SpawnLoudAudio(clip_SplashDown);
                _aboveWater = false;
                GameManager.particles_Water.transform.position = transform.position;
                GameManager.particles_Water.Play();
            }
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(h) > 0.1f) {
            // move
            myRigidbody.AddForce(Vector3.right * h * horizontalMoveForce, ForceMode.Force);
        }
        if (Mathf.Abs(v) > 0.1f) {
            // move
            myRigidbody.AddForce(Vector3.up * v * horizontalMoveForce, ForceMode.Force);
        }

        myRigidbody.linearVelocity = new Vector3(myRigidbody.linearVelocity.x * drag,
                                             myRigidbody.linearVelocity.y * drag,
                                             myRigidbody.linearVelocity.z);


        // clamp velocity
        myRigidbody.linearVelocity = Vector3.ClampMagnitude(myRigidbody.linearVelocity, maxVelocity);
        

    }
}
