using UnityEngine;
using UnityEngine.InputSystem;

public class CircleController : MonoBehaviour
{
    public float dashImpulse = 10f;

    public AudioClip clip_dash;
    private Rigidbody playerRigidbody;
    private SpriteRenderer SpriteRenderer;
    public GameObject slowdownCollider;
    public AudioSource myAudioSource;
    public AudioClip clip_dashHold;
    public ParticleSystem orbitParticles;

    public Color regularDashColor;
    public Color cannotDashColor;

    public bool _isSlowingDown = false;
    void Start()
    {
        playerRigidbody = GameManager.playerTrans.GetComponent<Rigidbody>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        slowdownCollider.SetActive(false);
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


        if (GameManager.playerTrans.GetComponent<PlayerControllerShark>()._aboveWater == false
            && GameManager.playerIsAlive == true
            && GameManager.playerInUpgradeMenu == false) {

            if (Input.GetButtonDown("Dash")) {
                if (Cursor.visible == true) {
                    Cursor.visible = false;
                }
                // GameManager.playerTrans.position = transform.position;
                GameManager.SpawnLoudAudio(clip_dash);
                // not normalized
                playerRigidbody.AddForce((transform.position - GameManager.playerTrans.position) * dashImpulse, ForceMode.Impulse);
                // normalized
                //playerRigidbody.AddForce((transform.position - GameManager.playerTrans.position).normalized * dashImpulse, ForceMode.Impulse);

            }
            /* OLD. slowdown mechanics
            if (Input.GetButtonDown("Dash")) {
                myAudioSource.clip = clip_dashHold;
                myAudioSource.PlayWebGL();
                orbitParticles.Play();
                slowdownCollider.SetActive(true);
                _isSlowingDown = true;
            }

            //if (Input.GetButton("Dash")) {
            //    slowdownCollider.SetActive(true);
            //}


            if (Input.GetButtonUp("Dash") && _isSlowingDown == true) {
                if (Cursor.visible == true) {
                    Cursor.visible = false;
                }
                slowdownCollider.SetActive(false);
                myAudioSource.StopWebGL();
                orbitParticles.Stop();
                orbitParticles.Clear();
                _isSlowingDown = false;
                // GameManager.playerTrans.position = transform.position;
                GameManager.SpawnLoudAudio(clip_dash);
                playerRigidbody.AddForce((transform.position - GameManager.playerTrans.position).normalized * dashImpulse, ForceMode.Impulse);

            }
            */
        }
        /*
        if (Input.GetButtonUp("Dash")) {
            slowdownCollider.SetActive(false);
            orbitParticles.Stop();
            orbitParticles.Clear();
            myAudioSource.StopWebGL();
        }
        */
        // transform.localPosition = Vector3.ClampMagnitude(transform.position - GameManager.playerTrans.position, 15f);
    }
}
