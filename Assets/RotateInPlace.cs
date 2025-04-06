using UnityEngine;

public class RotateInPlace : MonoBehaviour
{

    public Vector3 rotationAxis = new Vector3(0f, 1f, 0f);
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.Rotate(rotationAxis.x * Time.deltaTime,
                        rotationAxis.y * Time.deltaTime,
                        rotationAxis.z * Time.deltaTime);
    }
}
