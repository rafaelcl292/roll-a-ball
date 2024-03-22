using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Rigidbody of the player.
    private Rigidbody rb;

    // Variable to keep track of collected "PickUp" objects.
    private int count;

    // Movement along X and Y axes.
    private float movementX;
    private float movementY;

    // Speed at which the player moves.
    public float speed = 0;

    // UI text component to display count of "PickUp" objects collected.
    public TextMeshProUGUI countText;

    // UI object to display winning text.
    public GameObject winTextObject;
    public GameObject loseTextObject;

    public AudioSource coleta;
    public AudioClip clip;
    private bool isGameOver = false;

    // declare playgame scene
    public void Initial()
    {
        SceneManager.LoadSceneAsync(0);
    }


    // Start is called before the first frame update.
    void Start()
    {
        // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();

        // Initialize count to zero.
        count = 0;

        // Update the count display.
        SetCountText();

        // Initially set the win text to be inactive.
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
    }

    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate()
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Apply force to the Rigidbody to move the player.
        rb.AddForce(movement * speed);
    }


    void OnTriggerEnter(Collider other)
    {
        // Check if the object the player collided with has the "PickUp" tag.
        if (other.gameObject.CompareTag("PickUp"))
        {
            // Deactivate the collided object (making it disappear).
            other.gameObject.SetActive(false);

            // Increment the count of "PickUp" objects collected.
            count = count + 1;

            // Update the count display.
            SetCountText();
            coleta.PlayOneShot(clip);
        }
    }

    // Function to update the displayed count of "PickUp" objects collected.
    void SetCountText()
    {
        // Update the count text with the current count.
        countText.text = "Count: " + count.ToString();

        // Check if the count has reached or exceeded the win condition.
        if (count >= 14 && !isGameOver)
        {
            // Display the win text.
            winTextObject.SetActive(true);
            // delay 2 seconds
            Invoke("Initial", 2);
            isGameOver = true;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isGameOver)
        {
            loseTextObject.SetActive(true);
            Invoke("Initial", 2);
            isGameOver = true;
        }
    }

    // timer
    private int initialTime = 90;
    [SerializeField] TextMeshProUGUI timerText;
    float ellapsedTime;
    void Update()
    {
        ellapsedTime += Time.deltaTime;
        int remainingTime = initialTime - (int)ellapsedTime;
        int minutes = remainingTime / 60;
        int seconds = remainingTime % 60;
        if (remainingTime <= 0 && !isGameOver)
        {
            timerText.text = "00:00";
            loseTextObject.SetActive(true);
            Invoke("Initial", 2);
            isGameOver = true;
            return;
        }
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");

        // game over if player fall off the platform
        if (transform.position.y < -1 && !isGameOver)
        {
            loseTextObject.SetActive(true);
            Invoke("Initial", 2);
            isGameOver = true;
        }
    }
}