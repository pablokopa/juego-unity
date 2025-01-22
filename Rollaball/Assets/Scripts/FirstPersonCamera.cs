using UnityEngine;

public class FirstPersonCamera : MonoBehaviour {
    public Transform player; // Referencia al jugador (la bola)
    public Rigidbody playerRigidbody; // Rigidbody del jugador para aplicar movimiento
    public Vector3 offset = new Vector3(0, 1.5f, 0); // Posición de la cámara relativa a la bola
    public float mouseSensitivity = 100f; // Sensibilidad del ratón
    public float movementSpeed = 5f; // Velocidad de movimiento de la bola
    private float xRotation = 0f; // Rotación en el eje X (vertical)
    private float yRotation = 0f; // Rotación en el eje Y (horizontal)

    void Start() {
        // Bloquea el cursor en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate() {
        // Leer el movimiento del ratón
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Ajustar la rotación en el eje X (vertical)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limita la rotación vertical

        // Ajustar la rotación en el eje Y (horizontal)
        yRotation += mouseX;

        // Combinar las rotaciones para aplicarlas juntas
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

        // Actualizar la posición de la cámara para que siga al jugador con un offset
        transform.position = player.position + offset;
    }

    void FixedUpdate() {
        // Leer la entrada del jugador para movimiento
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calcular las direcciones de movimiento basadas en la rotación de la cámara
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        // Ignorar la componente vertical de las direcciones (para que la bola no se mueva hacia arriba/abajo)
        forward.y = 0;
        right.y = 0;

        // Normalizar las direcciones
        forward.Normalize();
        right.Normalize();

        // Calcular el vector de movimiento deseado
        Vector3 movement = (forward * moveVertical + right * moveHorizontal).normalized * movementSpeed;

        // Aplicar el movimiento al Rigidbody del jugador
        playerRigidbody.velocity = new Vector3(movement.x, playerRigidbody.velocity.y, movement.z);
    }
}
