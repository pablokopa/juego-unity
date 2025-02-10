using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour {
    // Rigidbody del jugador.
    private Rigidbody rb; 

    // Variable para llevar el registro de los objetos "PickUp" recolectados.
    private int count;

    // Movimiento a lo largo de los ejes X y Y.
    private float movementX;
    private float movementY;

    // Velocidad a la que se mueve el jugador.
    public float speed = 0;

    // Fuerza del salto.
    public float jumpForce = 5f; 

    // Para verificar si el jugador está en el suelo.
    private bool isGrounded = true; 

    // Componente de texto UI para mostrar la cantidad de objetos "PickUp" recolectados.
    public TextMeshProUGUI countText;

    // Objeto UI para mostrar el texto de victoria.
    public GameObject winTextObject;

    // Start se llama antes de la primera actualización del frame.
    void Start() {
        rb = GetComponent<Rigidbody>();
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
        // Salto con la tecla SPACE
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Evita saltos dobles
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    void SetCountText() {
        countText.text = "Puntos: " + count.ToString();
        if (count >= 7) {
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    private void OnCollisionEnter(Collision collision) {
        // Detectar si toca el suelo para permitir otro salto
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Enemy")) {
            Destroy(gameObject);
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "¡Has perdido!";
        }
    }
}