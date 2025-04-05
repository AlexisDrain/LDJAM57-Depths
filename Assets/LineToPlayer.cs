using UnityEngine;

public class LineToPlayer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void LateUpdate() {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, GameManager.playerTrans.position);
    }
}
