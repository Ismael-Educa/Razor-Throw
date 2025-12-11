using UnityEngine;
using UnityEngine.InputSystem;

public class LaunchManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float disableSpringTime = 0.15f;
    [SerializeField] private float launchEndDelay = 3f; // tiempo para reiniciar bola

    private Camera mainCamera;
    private Rigidbody2D ballRb;
    private SpringJoint2D springJoint2D;
    private bool isDragging;

    private Vector3 startPosition; // posición inicial para reiniciar bola

    private void Start()
    {
        mainCamera = Camera.main;
        ballRb = GetComponent<Rigidbody2D>();
        springJoint2D = GetComponent<SpringJoint2D>();
        startPosition = transform.position;

        UnityEngine.InputSystem.EnhancedTouch.EnhancedTouchSupport.Enable();
    }

    private void Update()
    {
        if (ballRb == null) return;

        var touch = Touchscreen.current?.primaryTouch;
        if (touch == null) return;

        if (!touch.press.IsPressed())
        {
            if (isDragging) LaunchBall();
            isDragging = false;
            return;
        }

        isDragging = true;
        ballRb.bodyType = RigidbodyType2D.Kinematic;

        Vector2 screenPos = touch.position.ReadValue();
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(screenPos);
        worldPos.z = 0f;
        ballRb.position = worldPos;
    }

    private void LaunchBall()
    {
        ballRb.bodyType = RigidbodyType2D.Dynamic;
        ballRb = null;

        Invoke(nameof(CutSpringJoint), disableSpringTime);
    }

    private void CutSpringJoint()
    {
        if (springJoint2D != null)
        {
            springJoint2D.enabled = false;
            springJoint2D = null;
        }

        Invoke(nameof(HandleLaunchEnded), launchEndDelay);
    }

    private void HandleLaunchEnded()
    {
        GameManager.Instance.LaunchEnded();

        if (GameManager.Instance.attempts > 0 && GameManager.Instance.foamRemaining > 0)
        {
            ResetBall();
        }
    }

    private void ResetBall()
    {
        ballRb = GetComponent<Rigidbody2D>();
        ballRb.linearVelocity = Vector2.zero;
        ballRb.angularVelocity = 0f;
        ballRb.bodyType = RigidbodyType2D.Kinematic;

        // Reactivar SpringJoint2D
        springJoint2D = GetComponent<SpringJoint2D>();
        if (springJoint2D != null)
            springJoint2D.enabled = true;

        // Reiniciar posición
        transform.position = startPosition;
    }
}
