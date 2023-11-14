using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BlinkingText : MonoBehaviour
{
    public float interval = 0.3f; 
    private TextMeshProUGUI textComponent;

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        InvokeRepeating("ToggleVisibility", 0, interval);
    }

    void ToggleVisibility()
    {
        textComponent.enabled = !textComponent.enabled;
    }
}
