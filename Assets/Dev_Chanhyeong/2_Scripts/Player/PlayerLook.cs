using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private WallRun wallRun;

    [SerializeField] private float sensX = 100f;
    [SerializeField] private float sensY = 100f;

    [SerializeField] private Transform cam = null;
    [SerializeField] private Transform orientation = null;

    private float _mouseX;
    private float _mouseY;

    private float _multiplier = 0.01f;

    private float _xRotation;
    private float _yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        _mouseX = Input.GetAxisRaw("Mouse X");
        _mouseY = Input.GetAxisRaw("Mouse Y");
         
        _yRotation += _mouseX * sensX * _multiplier;
        _xRotation -= _mouseY * sensY * _multiplier;

        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        cam.transform.rotation = Quaternion.Euler(_xRotation, _yRotation, wallRun.GetCameraRoll(cam.rotation.eulerAngles.z));
        orientation.transform.rotation = Quaternion.Euler(0, _yRotation, 0);
    }
}
