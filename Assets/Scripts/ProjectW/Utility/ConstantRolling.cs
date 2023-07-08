using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRolling : MonoBehaviour
{
    public Vector3 Rotation = new Vector3(0,0,1);
    public float RotationSpeed = 2f;

    private void Update()
    {
        transform.Rotate(Rotation * (RotationSpeed * Time.deltaTime));
    }
}
