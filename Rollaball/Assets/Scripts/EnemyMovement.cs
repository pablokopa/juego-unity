using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {
    // Referencia al transform del jugador.
    public Transform player;

    // Referencia al componente NavMeshAgent para la búsqueda de rutas.
    private NavMeshAgent navMeshAgent;

    // Start se llama antes de la primera actualización del frame.
    void Start() {
        // Obtener y almacenar el componente NavMeshAgent adjunto a este objeto.
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update se llama una vez por frame.
    void Update() {
        // Si hay una referencia al jugador...
        if (player != null) {    
            // Establecer el destino del enemigo a la posición actual del jugador.
            navMeshAgent.SetDestination(player.position);
        }
    }
}
