using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeSetFillLevel : MonoBehaviour
{
    public Vector3 initialScale;
    public Vector3 initialPosition;
    public Renderer cylinderRenderer;

    void Start()
    {
        initialScale = transform.localScale;
        initialPosition = transform.position;
        cylinderRenderer = GetComponent<Renderer>();

        SetFillLevel(0f); // Початково порожній
    }

    public void SetFillLevel(float value)
    {
        value = Mathf.Clamp01(value);

        if (value <= 0f)
        {
            cylinderRenderer.enabled = false;
            return;
        }
        else
        {
            cylinderRenderer.enabled = true;
        }

        float newHeight = initialScale.y * value;

        float offsetY = (initialScale.y - newHeight);// / 2f;

        transform.localScale = new Vector3(initialScale.x, newHeight, initialScale.z);

        transform.position = initialPosition - new Vector3(0, offsetY, 0);
    }


}
