# Roll a Ball Game - Unity 🏐

Este proyecto es una versión personalizada del tutorial de "Roll a Ball" de Unity. He seguido el curso completo de "Roll a Ball" para crear este juego y agregarle mis propios toques y características adicionales.

<details>
<summary> <b> Características 📜 </b> </summary>

- **Superficies y Obstáculos**: El juego cuenta con tres superficies diferentes, cada una con obstáculos que dificultan el movimiento. Todo el entorno está diseñado en tonos de rosa para dar un estilo único al juego. También cuenta con un plano invisible bajo el mapa para detectar cuando el jugador se cae y el juego debe terminar.

  ![obstaculos.gif](gifs/obstaculos.gif)

- **Recogida de Monedas**: He añadido monedas por todo el mapa. El jugador debe recoger un número específico de monedas para ganar. El marcador de puntos en la interfaz indica cuántas monedas se han recogido hasta el momento.

  ![monedas.gif](gifs/monedas.gif)

- **Enemigo (Fantasma Rosa)**: Un enemigo en forma de fantasma rosa persigue al jugador. Si el fantasma toca al jugador, se muestra un mensaje en pantalla indicando que se ha perdido la partida.

  ![enemigo.gif](gifs/enemigo.gif)

- He creado un script (*RampTrigger*) que genera un **impulso** cuando el jugador pasa por encima de una rampa. Esto permite al jugador saltar y superar obstáculos.

  ![salto_rampa.gif](gifs/salto_rampa.gif)

- He modificado el script (*PlayerController.cs*) para que el jugador pueda hacer un **salto hacia arriba**, añadiendo así más variedad en los movimientos del juego.

  ![salto_recto.gif](gifs/salto_recto.gif)

- He añadido también efectos visuales por todo el mapa (**lluvia, rayos y fuego**) para hacer que el escenario sea más llamativo y divertido. También he actualizado el cielo y las texturas de las monedas.

  ![efectos.gif](gifs/efectos.gif)

</details>


<details>
<summary> <b> Materiales Utilizados 🎨 </b> </summary>

- Los **modelos y texturas** del entorno, así como el modelo del **fantasma rosa**, fueron descargados desde la **Asset Store de Unity**.

  ![imagen](https://github.com/user-attachments/assets/698fa18b-a6d9-47e6-8d1e-6f8bd6f1a925)

- Se utilizaron **texturas personalizadas** para las superficies y los obstáculos, las cuales se diseñaron con un estilo de color rosa. *Se ven las texturas bug por un error en Unity*

  ![imagen](https://github.com/user-attachments/assets/3d5593bd-9022-4bbc-9496-be5c457ee36b)

- A medida que he ido avanzando con el juego, he ido utilizando más assets, como por ejemplo para las texturas de las **monedas, el cielo, los efectos del mapa**, etc.

  ![assets.png](gifs/assets.png)

</details>

<details>
<summary> <b> Scripts ⚙️ </b> </summary>

## Cámaras
### FirstPersonCamera.cs
Este código implementa una cámara en primera persona y el movimiento:

- **Cámara**: Sigue al jugador con un desplazamiento (offset), rota en función del movimiento del ratón. La rotación vertical está limitada entre -90 y 90 grados para evitar giros excesivos.
- **Movimiento del jugador**: Controlado por las teclas de dirección (*W A S D*), el movimiento se ajusta según la rotación de la cámara. Se aplica al Rigidbody del jugador para interactuar con la física del mapa.

Métodos:

- **LateUpdate()**: Actualiza la rotación de la cámara y la posición relativa al jugador.
- **FixedUpdate()**: Aplica el movimiento del jugador en base a la entrada del teclado y la rotación de la cámara.

![frist_person.gif](gifs/frist_person.gif)

### ThirdPersonController.cs
Este código implementa una cámara en tercera persona que sigue al jugador:

- **Cámara**: Mantiene una distancia constante del jugador usando un desplazamiento (offset).

Métodos:

- **Start()**: Calcula el desplazamiento inicial entre la cámara y el jugador al inicio del juego.
- **LateUpdate()**: Actualiza la posición de la cámara para que siga al jugador, manteniendo el mismo desplazamiento.

![third_person.gif](gifs/third_person.gif)

</details>

<details>
<summary> <b> Estados 🔧 </b> </summary>

### Invulnerabilidad
He añadido un estado de **invulnerabilidad** al jugador que se activa cuando el jugador recoge una moneda. Durante este estado, el jugador es inmune y no puede ser tocado por el fantasma. El estado de invulnerabilidad dura 1 segundo por defecto (aunque se puede modificar) y se indica con un efecto visual de destello y cambio de color.

Para hacer este estado he modificado ***PlayerCOntroller.cs*** para añadir el efecto de invulnerabilidad.

También he añadido un Animator Controller que cambia cada vez que paso sobre una moneda:
![estadosgif.gif](gifs/estadosgif.gif)

Este se encarga de gestionar cuando el jugador coge una moneda y pasa al estado de invulnerabilidad. Y también controla cuando dicho estado se acaba y debe volver al estado normal.

![invulnerable.gif](gifs/invulnerable.gif)
</details>