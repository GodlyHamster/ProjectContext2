using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    public Transform spriteTransform;
    public SpriteRenderer spriteRenderer;

    public Sprite frontSprite;
    public Sprite backSprite;

    public float hopHeight = 0.1f;
    public float hopSpeed = 10f;

    private Rigidbody rb;
    private Vector3 moveInput;
    private Vector3 originalSpritePosition;
    private float hopTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.None;

        originalSpritePosition = spriteTransform.localPosition;

        StartCoroutine(EnableInterpolationAfterDelay(0.5f));
    }

    private IEnumerator EnableInterpolationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        moveInput = new Vector3(moveX, 0, moveZ).normalized * speed;

        if (moveInput.magnitude > 0.1f)
        {
            if (spriteRenderer != null)
            {
                if (moveZ < 0)
                {
                    if (frontSprite != null)
                        spriteRenderer.sprite = frontSprite;
                }
                else if (moveZ > 0)
                {
                    if (backSprite != null)
                        spriteRenderer.sprite = backSprite;
                }
            }
            hopTimer += Time.deltaTime * hopSpeed;
            float hopOffset = Mathf.Sin(hopTimer) * hopHeight;
            spriteTransform.localPosition = originalSpritePosition + new Vector3(0, hopOffset, 0);
        }
        else
        {
            spriteTransform.localPosition = originalSpritePosition;
            hopTimer = 0;
        }
    }

    private void FixedUpdate()
    {
        if (moveInput.magnitude > 0.1f)
        {
            rb.linearVelocity = new Vector3(moveInput.x, rb.linearVelocity.y, moveInput.z);
        }
        else
        {
            Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            Vector3 smoothVelocity = Vector3.Lerp(horizontalVelocity, Vector3.zero, 0.2f);
            rb.linearVelocity = new Vector3(smoothVelocity.x, rb.linearVelocity.y, smoothVelocity.z);
        }
    }
}