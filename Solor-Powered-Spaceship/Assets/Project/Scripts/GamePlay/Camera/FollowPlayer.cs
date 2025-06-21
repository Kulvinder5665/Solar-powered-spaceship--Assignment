
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject playerPrefab;
    public Vector3 offset;
    void Start()
    {
        if (playerPrefab == null)
        {
            playerPrefab = GameObject.FindGameObjectWithTag("Player");
        }


    }

    private void LateUpdate() {
        
       transform.position = playerPrefab.transform.position + offset;
    }
}
