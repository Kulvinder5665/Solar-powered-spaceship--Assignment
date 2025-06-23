
using Solar.Utilities;
using UnityEngine;

public class FollowPlayer :MonoBehaviour
{
    // public Transform playerPrefab;
    public Vector3 offset;
    

    private void LateUpdate() {
        
        if(GameService.Instance.playerTrans!= null)
       transform.position =GameService.Instance.playerTrans.position+ offset;
    }
}
