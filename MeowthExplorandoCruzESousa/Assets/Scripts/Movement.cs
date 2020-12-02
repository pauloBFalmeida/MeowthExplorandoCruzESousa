using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller = null;
    public Transform cam;

    public float speed = 5f;
    public float turnSmoothTime = 0.5f;
    private float turnSmoothVelocity;

    public float gravity = -9.8f;
    private Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;

    public float jumpHeight = 5f;
    private float jumpForce;

    public Animator anim = null;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        jumpForce = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y += jumpForce;
            isGrounded = false;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        // Gravidade
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Animacao
        if (isGrounded)
        {
            if (direction.magnitude >= 0.1f)
                anim.Play("Walking");
            else
                anim.Play("Standing");
        }
        else if (velocity.y >= 0f)
                anim.Play("Jumping");
    }

}
