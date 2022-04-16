using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{
    public bool isPlane;
    public bool isHelicopter;
    private float m_spinSpeed = 20.0f;

    private void Update()
    {
        if (isPlane && !GameManager.isPlaneCrashed) {
            transform.Rotate(Vector3.right * m_spinSpeed, Space.Self);
        }
        if (isHelicopter && !GameManager.isPlaneCrashed) {
            transform.Rotate(Vector3.up * m_spinSpeed, Space.Self);
        }
    }
}
