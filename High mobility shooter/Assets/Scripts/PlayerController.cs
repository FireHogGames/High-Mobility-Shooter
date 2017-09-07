using UnityEngine;
using System;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    public float runSpeed;
    public float sprintSpeed;
    public float jumpSpeed;
    public float sensitivity;


    private bool jump, wasGrounded, isJumping;
    public bool isGrounded;
    private CapsuleCollider m_Capsule;

    [Serializable]
    public class AdvancedSettings
    {
        public float groundCheckDistance = 0.01f;
        [Tooltip("set it to 0.1 or more if you get stuck in wall")]
        public float shellOffset; //reduce the radius by that ratio to avoid getting stuck in wall (a value of 0.1f is nice)
    }
    public AdvancedSettings advancedSettings;

    private float speed;
    private float _verticalRotation;
    private PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        speed = runSpeed;
    }

    private void Update()
    {
        float _horizontal = Input.GetAxisRaw("Horizontal");
        float _vertical = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _horizontal;
        Vector3 _moveVertical = transform.forward * _vertical;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * speed;

        motor.Move(_velocity);

        if (Input.GetButtonDown("Jump"))
        {
            motor.Jump(jumpSpeed);
        }
        else
        {
            motor.Jump(0);
        }

        float _horizontalRotation = Input.GetAxis("Mouse X") * sensitivity;
        Vector3 rotation = new Vector3(0, _horizontalRotation, 0);
        motor.Rotate(rotation);

        _verticalRotation -= Input.GetAxis("Mouse Y") * sensitivity;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -90, 90);
        motor.cameraRotation(_verticalRotation);
    }

    private void GroundCheck()
    {
        wasGrounded = isGrounded;
        RaycastHit hitInfo;
        if (Physics.SphereCast(transform.position, m_Capsule.radius * (1.0f - advancedSettings.shellOffset), Vector3.down, out hitInfo,
                               ((m_Capsule.height / 2f) - m_Capsule.radius) + advancedSettings.groundCheckDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        if (!wasGrounded && isGrounded && isJumping)
        {
            isJumping = false;
        }
    }
}
