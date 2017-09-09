using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    private Rigidbody body;

    private Vector3 _velocity;
    private Vector3 _rotation;
    private float jumpForce;
    private float _cameraRotation;

    public Camera cam;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 velocity)
    {
        _velocity = velocity;
    }

    public void Rotate(Vector3 rotation)
    {
        _rotation = rotation;
    }

    public void cameraRotation(float cameraRotation)
    {
        _cameraRotation = cameraRotation;
    }

    public void Jump(float force)
    {
        jumpForce = force;
    }

    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
        PerformJump();
        performCameraRotation();
    }

    private void PerformMovement()
    {
        if(_velocity != Vector3.zero)
        {
            body.MovePosition(body.position + _velocity * Time.fixedDeltaTime);
        }
    }

    private void PerformRotation()
    {
        if(_rotation != Vector3.zero)
        {
            body.MoveRotation(body.rotation * Quaternion.Euler(_rotation));
        }
    }

    private void PerformJump()
    {
        if (GetComponent<PlayerController>().isGrounded)
        {
            jumpForce = jumpForce * Time.fixedDeltaTime;
            body.AddForce(transform.up * jumpForce);
        }
    }

    private void performCameraRotation()
    {
        cam.transform.localRotation = Quaternion.Euler(_cameraRotation, 0, 0);
    }

   
}
