using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{

    public Transform PlayerTransform;

    private Vector3 _cameraOffset;

    [Range(0.01f, 1.0f)] public float SmoothFactor = 0.5f;

    public float RotationSpeed = 5.0f;

    //public bool LookAtPlayer = false;

    //public bool RotateAroundPlayer = true;

    // Start is called before the first frame update
    void Start()
    {
        _cameraOffset = this.transform.position - PlayerTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Quaternion camTurnAngle = Quaternion.AngleAxis(
            Input.GetAxis("Mouse X") * RotationSpeed,
            Vector3.up);

        _cameraOffset = camTurnAngle * _cameraOffset;

        Vector3 newPos = PlayerTransform.position + _cameraOffset;

        this.transform.position = Vector3.Slerp(
            this.transform.position,
            newPos,
            SmoothFactor);


        this.transform.LookAt(PlayerTransform);


    }
}
