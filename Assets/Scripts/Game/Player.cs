using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Rigidbody2D rb;

    private float fireTimer = 0f;
    private Vector2 moveDirection = Vector2.zero;
    private Vector2 touchStartPos = Vector2.zero;
    private bool isTouching = false;

    private void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleInput();
        UpdateFireTimer();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void HandleInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    isTouching = true;
                    break;

                case TouchPhase.Moved:
                    if (isTouching)
                    {
                        Vector2 dragDelta = touch.position - touchStartPos;
                        moveDirection = new Vector2(dragDelta.x, dragDelta.y).normalized;
                    }
                    break;

                case TouchPhase.Ended:
                    isTouching = false;
                    moveDirection = Vector2.zero;
                    break;
            }

            // Tap to shoot
            if (touch.tapCount > 0)
            {
                Shoot();
            }
        }

        // Fallback for editor testing
        #if UNITY_EDITOR
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Input.GetKeyDown(KeyCode.Space))
            Shoot();
        #endif
    }

    private void Move()
    {
        rb.velocity = moveDirection * moveSpeed;
    }

    private void UpdateFireTimer()
    {
        if (fireTimer > 0)
            fireTimer -= Time.deltaTime;
    }

    private void Shoot()
    {
        if (fireTimer <= 0 && GameManager.Instance.IsGameActive)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            if (bulletRb != null)
            {
                bulletRb.velocity = Vector2.up * 10f;
            }
            fireTimer = fireRate;
            AudioManager.Instance.PlaySFX("shoot");
        }
    }
}
