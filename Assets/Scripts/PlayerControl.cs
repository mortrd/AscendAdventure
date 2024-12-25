using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Health Settings")]
    public static float health;


    [Header("Movement Settings")]
    [SerializeField] float maxSpeed = 10f; 
    [SerializeField] float acceleration = 20f; 
    [SerializeField] float deceleration = 25f;

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 25f; 
    [SerializeField] private float dashDistance = 5f; 
    [SerializeField] private float dashDuration = 0.15f; 
    [SerializeField] private float dashCooldown = 0.5f; 
    [SerializeField] private int maxDashes = 1;

    private int currentDashes;
    private bool isDashing = false;
    private bool canDash = true;
    private Vector2 dashDirection;
    private Vector2 startDashPosition;

    [Header("Debug Info")]
    [SerializeField] Vector2 currentVelocity;



    Camera cam;

    private Rigidbody2D rb;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (cam == null) cam = Camera.main;
        currentDashes = maxDashes;
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody2D found on the GameObject.");
        }
    }

    void Update()
    {
        float input = Input.GetAxisRaw("Horizontal");
        ApplyMovement(input);
        if (Input.GetKeyDown(KeyCode.Mouse1) && canDash && currentDashes > 0)
        {
            rb.velocity = Vector2.zero;
            Dash();
        }
    }

    void ApplyMovement(float input)
    {
        float targetSpeed = input * maxSpeed; 
        float speedDifference = targetSpeed - rb.velocity.x;

        float accelerationRate = (Mathf.Abs(targetSpeed) > 0.1f) ? acceleration : deceleration;
        

        
        float movement = Mathf.Clamp(speedDifference, -accelerationRate * Time.deltaTime, accelerationRate * Time.deltaTime);
        rb.velocity = new Vector2(rb.velocity.x + movement, rb.velocity.y);

        
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y);

        
        currentVelocity = rb.velocity;
    }
    void Dash()
    {
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        dashDirection = (mousePosition - (Vector2)transform.position).normalized;

        startDashPosition = transform.position;
        isDashing = true;
        canDash = false;
        currentDashes--;

        rb.isKinematic = true;

        StartCoroutine(PerformDash());

    }
    private System.Collections.IEnumerator PerformDash()
    {
        float elapsed = 0f;

        while (elapsed < dashDuration)
        {
            elapsed += Time.deltaTime;

            // Calculate distance traveled based on time
            float distanceTraveled = Mathf.Lerp(0, dashDistance, elapsed / dashDuration);

            
            rb.MovePosition(startDashPosition + dashDirection * distanceTraveled);

            yield return null;
        }

        EndDash();
    }
    private void EndDash()
    {
        
        isDashing = false;
        rb.isKinematic = false;

        
        Invoke(nameof(ResetDash), dashCooldown);
    }

    private void ResetDash()
    {
        canDash = true;
        if (currentDashes < maxDashes)
        {
            currentDashes++;
        }
    }

}
