using cakeslice;
using UnityEngine;

public class OutlineOnHover : MonoBehaviour
{
    public Outline outline;

    void Start()
    {
        outline = GetComponent<Outline>();
        if (outline != null)
            outline.enabled = false;
    }

    void OnMouseEnter()
    {
        if (outline != null)
            outline.enabled = true;
    }

    void OnMouseExit()
    {
        if (outline != null)
            outline.enabled = false;
    }
}
