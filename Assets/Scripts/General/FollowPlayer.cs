using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    void LateUpdate()
    {
        transform.position = PlayerController.playerController.transform.position;
    }
}
