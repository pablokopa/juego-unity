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

    // Componente de texto UI para mostrar la cantidad de objetos "PickUp" recolectados.
    public TextMeshProUGUI countText;

    // Objeto UI para mostrar el texto de victoria.
    public GameObject winTextObject;

    // Start se llama antes de la primera actualización del frame.
    void Start() {
        // Obtener y almacenar el componente Rigidbody adjunto al jugador.
        rb = GetComponent<Rigidbody>();

        // Inicializar el contador a cero.
        count = 0;

        // Actualizar la visualización del contador.
        SetCountText();

        // Inicialmente establecer el texto de victoria como inactivo.
        winTextObject.SetActive(false);
    }
 
    // Esta función se llama cuando se detecta un input de movimiento.
    void OnMove(InputValue movementValue) {
        // Convertir el valor de entrada en un Vector2 para el movimiento.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Almacenar los componentes X e Y del movimiento.
        movementX = movementVector.x; 
        movementY = movementVector.y; 
    }

    // FixedUpdate se llama una vez por cada frame de tasa de actualización fija.
    private void FixedUpdate() {
        // Crear un vector de movimiento 3D utilizando las entradas X e Y.
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

        // Aplicar fuerza al Rigidbody para mover al jugador.
        rb.AddForce(movement * speed); 
    }

    void OnTriggerEnter(Collider other) {
        // Verificar si el objeto con el que colisionó el jugador tiene la etiqueta "PickUp".
        if (other.gameObject.CompareTag("PickUp")) {
            // Desactivar el objeto colisionado (haciéndolo desaparecer).
            other.gameObject.SetActive(false);

            // Incrementar el contador de objetos "PickUp" recolectados.
            count = count + 1;

            // Actualizar la visualización del contador.
            SetCountText();
        }
    }

    // Función para actualizar el contador mostrado de objetos "PickUp" recolectados.
    void SetCountText() {
        // Actualizar el texto del contador con el valor actual.
        countText.text = "Puntos: " + count.ToString();

        // Verificar si el contador ha alcanzado o superado la condición de victoria.
        if (count >= 7) {
            // Mostrar el texto de victoria.
            winTextObject.SetActive(true);

            // Destruir el GameObject enemigo.
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    private void OnCollisionEnter(Collision collision) {
        // Verificar si el objeto con el que colisionó el jugador tiene la etiqueta "Enemy".
        if (collision.gameObject.CompareTag("Enemy")) {
            // Destruir el objeto actual.
            Destroy(gameObject); 
 
            // Actualizar el texto de winText para mostrar "¡Has perdido!".
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "¡Has perdido!";
        }
    }
}