using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlaneController : MonoBehaviour
{
    public Joystick joystick;

    [Header("Speed UI")]
    public Slider speedSlider;
    public TextMeshProUGUI speedField;

    [Header("Buttons")]
    public GameObject sucessRestartButton;
    public GameObject failRestartButton;

    [Header("Plane Speed")]
    public float forwardSpeed = 15f;
    public float horizontalSpeed = 4f;
    public float verticalSpeed = 4f;

    [Header("Plane Max Rotation")]
    public float maxHorizontalRotation = 0.1f;
    public float maxVerticalRotation = 0.06f;

    [Header("Smoothness Settings")]
    public float smoothness = 5f;
    public float rotationSmoothness = 5f;

    private Rigidbody rb;

    private bool canMove = true;

    private float horizontalInput;
    private float verticalInput;

    private float forwardSpeedMultiplier = 100f;
    private float speedMultiplier = 1000f;

    [Header("Score UI")]
    public TextMeshProUGUI scoreText;

    [Header("Explosion VFX")]
    public GameObject explosion;

    [Header("Landingspot Markers")]
    public GameObject spot1;
    public GameObject spot2;
    private bool isLanding;
    private bool secondLanding;
    private float landingTime;

    private static int playerScore;
    public int PlayerScore
    {
        get
        {
            return playerScore;
        }
        set
        {
            playerScore = value;
            UpdateScoreText();
        }
    }

    private void Start()
    {
        PlayerScore = 0;
        sucessRestartButton.SetActive(false);
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(canMove)
        {
            if (Input.GetMouseButton(0) || Input.touches.Length != 0)
            {
                horizontalInput = joystick.Horizontal;
                verticalInput = joystick.Vertical;
            }

            HandlePlaneRotation();
        }
        if(isLanding)
        {
            var step = 100f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, spot1.transform.position, step);
            rb.useGravity = false;
            landingTime += Time.deltaTime;
            gameObject.GetComponent<BoxCollider>().isTrigger = true;

            if(Vector3.Distance(transform.position, spot1.transform.position)< 0.001f)
            {
                isLanding = false;
                secondLanding = true;
            }
        }
        if(secondLanding)
        {
            var step = 75f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, spot2.transform.position, step);
            if (Vector3.Distance(transform.position, spot2.transform.position) < 0.001f)
            {
                sucessRestartButton.SetActive(true);

                secondLanding = false;

                rb.constraints = RigidbodyConstraints.FreezePosition;
            }

        }
    }

    private void FixedUpdate()
    {
        if(canMove)
        {
            HandlePlaneSpeed();
            HandlePlaneMovement();

            speedField.text = "Speed: " + (int)rb.velocity.magnitude;
        }
    }

    public void Landing()
    {
        canMove = false;

        gameObject.transform.position = new Vector3(265.23f, 121.25f, 2135.06f);

        isLanding = true;

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

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + playerScore;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            Instantiate(explosion).transform.position = gameObject.transform.position;
            failRestartButton.SetActive(true);
            Destroy(gameObject);
        }
    }
}
