using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
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
    }

    void HandleRotationInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float rotationAmount = -horizontalInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward, rotationAmount);
    }

    void HandleThrustInput()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            currentThrustSpeed += accelerationRate * Time.deltaTime;

            // Limita a velocidade ao máximo
            currentThrustSpeed = Mathf.Clamp(currentThrustSpeed, initialThrustSpeed, maxThrustSpeed);

            // Aplica o impulso
            GetComponent<Rigidbody2D>().AddForce(transform.up * currentThrustSpeed);
        }
        else
        {
            // Se não estiver pressionando, redefine a velocidade para o valor inicial
            currentThrustSpeed = initialThrustSpeed;
        }
    }
}
