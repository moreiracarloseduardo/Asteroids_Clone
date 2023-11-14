using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public GameObject accelerationEffect;

    public float rotationSpeed = 180f;
    public float initialThrustSpeed = 1f;
    public float maxThrustSpeed = 5f;
    public float accelerationRate = 0.1f;


    float currentThrustSpeed;

    void Start()
    {
        currentThrustSpeed = initialThrustSpeed;
    }

    void Update()
    {
        HandleRotationInput();
        HandleThrustInput();
        accelerationEffect.SetActive(Input.GetKey(KeyCode.UpArrow));    
    }

    void HandleRotationInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float rotationAmount = -horizontalInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward, rotationAmount);
    }

    void HandleThrustInput()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (Input.GetKey(KeyCode.UpArrow))
        {
            currentThrustSpeed += accelerationRate * Time.deltaTime;
            currentThrustSpeed = Mathf.Clamp(currentThrustSpeed, initialThrustSpeed, maxThrustSpeed);

            // Verifique se a nave está se movendo na direção oposta à aceleração
            if (Vector2.Dot(rb.velocity, transform.up) < 0)
            {
                // Desacelere a nave suavemente
                rb.velocity *= 0.99f;
            }

            // Adicione uma força oposta à direção atual da nave para simular atrito
            rb.AddForce(-rb.velocity * 0.1f);

            rb.AddForce(transform.up * currentThrustSpeed);

            // Verifique se a velocidade excede a velocidade máxima após a força ser aplicada
            if (rb.velocity.magnitude > maxThrustSpeed)
            {
                // Defina a velocidade para a velocidade máxima
                rb.velocity = rb.velocity.normalized * maxThrustSpeed;
            }
        }
        else
        {
            currentThrustSpeed = initialThrustSpeed;
        }
    }

}
/* Here is the explanation for the code above:
1. If the player is pressing the Up Arrow key, increase the thrust speed by the acceleration rate over time.
2. Make sure the thrust speed doesn't exceed the maximum thrust speed.
3. If the player is pressing the Up Arrow key, and the ship is moving in the opposite direction of the acceleration, slow the ship down.
4. Add a force opposite to the current direction of the ship to simulate friction.
5. Add a force in the direction of the ship's current rotation.
6. If the velocity exceeds the maximum thrust speed after the force is applied, set the velocity to the maximum thrust speed.
7. If the player is not pressing the Up Arrow key, reset the thrust speed to the initial thrust speed. */