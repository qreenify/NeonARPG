using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{

    // Variables
    public Transform player;
    public float smooth = 0.3f;

    public float height;

    private Vector3 velocity = Vector3.zero;



    //Methods

    void Update()
    {

        Vector3 pos = new Vector3(player.position.x - 10f, player.position.y + height, player.position.z - 10f);
        
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);
        
    }


}
