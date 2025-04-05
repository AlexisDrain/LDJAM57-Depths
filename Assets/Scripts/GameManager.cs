using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager myGameManager;
    public static Transform playerTrans;

    void Awake()
    {
        playerTrans = GameObject.Find("Player").transform;
    }

    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
