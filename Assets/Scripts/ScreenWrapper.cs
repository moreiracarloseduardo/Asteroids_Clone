using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    void LateUpdate()
    {
        if (gameObject.activeSelf)
        {
            WrapAroundScreen();
        }
    }

    void WrapAroundScreen()
    {
        Vector3 currentPosition = transform.position;
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(currentPosition);

        float margin = 0.1f; // Ajuste este valor conforme necessário

        if (viewportPosition.x > 1 + margin)
            currentPosition.x = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;

        else if (viewportPosition.x < 0 - margin)
            currentPosition.x = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        if (viewportPosition.y > 1 + margin)
            currentPosition.y = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;

        else if (viewportPosition.y < 0 - margin)
            currentPosition.y = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

        transform.position = currentPosition;
    }
}
