using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBack : MonoBehaviour
{
    private void Update()
    {
        if (!GameManager.isGameStarted) {
            transform.Translate(Vector3.left * 2.5f * Time.deltaTime, Space.World);
        }
        if (transform.position.x <= -2) {
            transform.position = new Vector3(24, transform.position.y, transform.position.z);
        }
    }
}
