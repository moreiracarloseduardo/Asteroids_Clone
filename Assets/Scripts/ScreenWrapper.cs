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

    if (viewportPosition.x > 1)
    {
        currentPosition.x = Camera.main.ViewportToWorldPoint(new Vector3(0, viewportPosition.y, viewportPosition.z)).x;
        currentPosition.y = Camera.main.ViewportToWorldPoint(new Vector3(viewportPosition.x, 1 - viewportPosition.y, viewportPosition.z)).y;
    }

    else if (viewportPosition.x < 0)
    {
        currentPosition.x = Camera.main.ViewportToWorldPoint(new Vector3(1, viewportPosition.y, viewportPosition.z)).x;
        currentPosition.y = Camera.main.ViewportToWorldPoint(new Vector3(viewportPosition.x, 1 - viewportPosition.y, viewportPosition.z)).y;
    }

    if (viewportPosition.y > 1)
    {
        currentPosition.y = Camera.main.ViewportToWorldPoint(new Vector3(viewportPosition.x, 0, viewportPosition.z)).y;
        currentPosition.x = Camera.main.ViewportToWorldPoint(new Vector3(1 - viewportPosition.x, viewportPosition.y, viewportPosition.z)).x;
    }

    else if (viewportPosition.y < 0)
    {
        currentPosition.y = Camera.main.ViewportToWorldPoint(new Vector3(viewportPosition.x, 1, viewportPosition.z)).y;
        currentPosition.x = Camera.main.ViewportToWorldPoint(new Vector3(1 - viewportPosition.x, viewportPosition.y, viewportPosition.z)).x;
    }

    transform.position = currentPosition;
}
}
