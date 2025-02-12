using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour {
    // Rigidbody del jugador
    private Rigidbody rb;

    // Animator del jugador
    private Animator animator;

    // Renderer para efectos visuales
    private Renderer playerRenderer;

    // Registro de objetos "PickUp" recolectados
    private int count;

    // Movimiento en los ejes X y Y
    private float movementX;
    private float movementY;

    // Velocidad de movimiento
    public float speed = 0;

    // Fuerza del salto
    public float jumpForce = 5f;

    // Verificar si el jugador está en el suelo
    private bool isGrounded = true;

    // UI para mostrar el conteo de PickUps
    public TextMeshProUGUI countText;

    // UI para mostrar el texto de victoria o derrota
    public GameObject winTextObject;

    // Invulnerabilidad
    private bool isInvulnerable = false;
    [Header("Configuración de Invulnerabilidad")]
    public float invulnerabilityDuration = 1f; // Configurable desde Unity
    private float invulnerabilityTimer = 0f;

    // Configuración del parpadeo
    public float blinkSpeed = 0.2f; // Velocidad del parpadeo
    private float blinkTimer = 0f;

    // Color original e invulnerable
    private Color originalColor;
    public Color invulnerableColor = Color.yellow;

    void Start() {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerRenderer = GetComponent<Renderer>();

        originalColor = playerRenderer.material.color; // Guardar el color original

        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue) {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate() {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if (isInvulnerable) {
            invulnerabilityTimer -= Time.deltaTime;
            HandleBlinkEffect(); // Parpadeo

            if (invulnerabilityTimer <= 0) {
                EndInvulnerability();
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
            ActivateInvulnerability();
        }
    }

    void SetCountText() {
        countText.text = "Puntos: " + count.ToString();
        if (count >= 7) {
            winTextObject.SetActive(true);
            Destroy(gameObject);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Enemy")) {
            if (!isInvulnerable) {
                Destroy(gameObject);
                winTextObject.gameObject.SetActive(true);
                winTextObject.GetComponent<TextMeshProUGUI>().text = "¡Has perdido!";
            }
        }

        if (collision.gameObject.CompareTag("Invisible")) {
            Destroy(gameObject);
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "¡Has perdido!";
        }
    }

    private void ActivateInvulnerability() {
        isInvulnerable = true;
        invulnerabilityTimer = invulnerabilityDuration;

        animator.SetBool("isInvulnerable", true);
        playerRenderer.material.color = invulnerableColor; // Cambiar color

        blinkTimer = 0f;

        // Ignorar colisiones con el enemigo
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
            Physics.IgnoreCollision(GetComponent<Collider>(), enemy.GetComponent<Collider>(), true);
        }
    }

    private void HandleBlinkEffect() {
        blinkTimer += Time.deltaTime;
        if (blinkTimer >= blinkSpeed) {
            playerRenderer.enabled = !playerRenderer.enabled;
            blinkTimer = 0f;
        }
    }

    private void EndInvulnerability() {
        isInvulnerable = false;
        animator.SetBool("isInvulnerable", false);
        playerRenderer.enabled = true;
        playerRenderer.material.color = originalColor; // Restaurar color original

        // Rehabilitar colisiones con el enemigo
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
            Physics.IgnoreCollision(GetComponent<Collider>(), enemy.GetComponent<Collider>(), false);
        }
    }
}