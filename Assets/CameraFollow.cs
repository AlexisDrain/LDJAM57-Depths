using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector2 cameraCoords = new Vector2(0, 0);
    public Vector2 cameraBounds = new Vector2(32, 18);
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // if(GameManager.playerTrans.position.x > cameraCoords.x % cameraBounds.x) {
        //    cameraCoords.x = cameraBounds.x * cameraCoords.x;
        // }
        if (GameManager.playerTrans.position.y > (cameraCoords.y + 1f) * cameraBounds.y) {
            cameraCoords.y += 1;
        }
        if (GameManager.playerTrans.position.y < (cameraCoords.y) * cameraBounds.y) {
            cameraCoords.y -= 1;
        }

        if (GameManager.playerTrans.position.x > (cameraCoords.x + 1f) * cameraBounds.x) {
            cameraCoords.x += 1;
        }
        if (GameManager.playerTrans.position.x < (cameraCoords.x) * cameraBounds.x) {
            cameraCoords.x -= 1;
        }

        Camera.main.transform.position = new Vector3((cameraCoords.x * cameraBounds.x) + 16f,
                                                    (cameraCoords.y * cameraBounds.y) + 9f,
                                                    -18f);
    }
}
