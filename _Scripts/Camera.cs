using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform player;

    public Vector3 cameraOffset;

    private void LateUpdate()
    {
        transform.position = new Vector3(player.position.x + cameraOffset.x, player.position.y + cameraOffset.y, transform.position.z);
    }
}
