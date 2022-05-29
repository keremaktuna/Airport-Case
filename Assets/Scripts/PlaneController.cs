using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneController : MonoBehaviour
{
    public Joystick joystick;

    public Slider speedSlider;

    public float forwardSpeed = 15f;
    public float horizontalSpeed = 4f;
    public float verticalSpeed = 4f;

    public float maxHorizontalRotation = 0.1f;
    public float maxVerticalRotation = 0.06f;

    public float smoothness = 5f;
    public float rotationSmoothness = 5f;

    private Rigidbody rb;

    private float horizontalInput;
    private float verticalInput;

    private float forwardSpeedMultiplier = 100f;
    private float speedMultiplier = 1000f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) || Input.touches.Length != 0)
        {
            horizontalInput = joystick.Horizontal;
            verticalInput = joystick.Vertical;
        }

        HandlePlaneRotation();
    }

    private void FixedUpdate()
    {
        HandlePlaneSpeed();
        HandlePlaneMovement();
    }

    private void HandlePlaneMovement()
    {
        rb.velocity = new Vector3(
            rb.velocity.x,
            rb.velocity.y,
            forwardSpeed * forwardSpeedMultiplier * Time.deltaTime
            );

        float xVelocity = horizontalInput * speedMultiplier * horizontalSpeed * Time.deltaTime;
        float yVelocity = -verticalInput * speedMultiplier * verticalSpeed * Time.deltaTime;

        rb.velocity = Vector3.Lerp(
            rb.velocity,
            new Vector3(xVelocity, yVelocity, rb.velocity.z),
            Time.deltaTime * smoothness
            );
    }

    private void HandlePlaneRotation()
    {
        float horizontalRotation = -horizontalInput * maxHorizontalRotation;
        float verticalRotation  = verticalInput * maxVerticalRotation;

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            new Quaternion(
                verticalRotation,
                transform.rotation.y,
                horizontalRotation,
                transform.rotation.w
                ),
            Time.deltaTime * rotationSmoothness
            ) ;
    }

    public void HandlePlaneSpeed()
    {
        speedMultiplier = 1000f;
        forwardSpeedMultiplier = 100f;
        speedMultiplier = speedMultiplier * speedSlider.value;
        forwardSpeedMultiplier = forwardSpeedMultiplier * speedSlider.value;
    }
}
