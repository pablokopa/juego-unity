using UnityEngine;

public class Rotator : MonoBehaviour {

 // Llama a update una vez por frame
 void Update() {
 // Rota el objeto unas cantidades especificas en los ejes X, Y y Z 
        transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
    }
}