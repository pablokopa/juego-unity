using UnityEngine;

public class CameraController : MonoBehaviour {
    // Referencia al GameObject del jugador.
    public GameObject player;

    // La distancia entre la cámara y el jugador.
    private Vector3 offset;

    // Start se llama antes de la primera actualización del frame.
    void Start() {
        // Calcular el desplazamiento inicial entre la posición de la cámara y la posición del jugador.
        offset = transform.position - player.transform.position; 
    }

    // LateUpdate se llama una vez por frame después de que se hayan completado todas las funciones Update.
    void LateUpdate() {
        // Mantener el mismo desplazamiento entre la cámara y el jugador durante todo el juego.
        transform.position = player.transform.position + offset;  
    }
}