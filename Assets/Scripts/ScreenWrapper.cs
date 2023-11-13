using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    void Update()
    {
        WrapAroundScreen();
    }

    void WrapAroundScreen()
    {
        Vector3 currentPosition = transform.position;
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(currentPosition);

        if (viewportPosition.x > 1)
            currentPosition.x = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;

        else if (viewportPosition.x < 0)
            currentPosition.x = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        if (viewportPosition.y > 1)
            currentPosition.y = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;

        else if (viewportPosition.y < 0)
            currentPosition.y = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

        transform.position = currentPosition;
    }
}
