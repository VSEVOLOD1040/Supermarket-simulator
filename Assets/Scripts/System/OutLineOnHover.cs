using cakeslice;
using UnityEngine;

public class OutlineOnHover : MonoBehaviour
{
    public Outline outline;

    void Start()
    {
        
        if (outline == null) outline = GetComponent<Outline>();

        if (outline != null)
            outline.enabled = false;
    }

    //void OnMouseEnter()
    //{
    //    if (outline != null && CheckDistance())
    //        outline.enabled = true;
    //}

    private void OnMouseOver()
    {
        if (CheckDistance())
            outline.enabled = true;
        else
        {
            outline.enabled = false;

        }
    }
    void OnMouseExit()
    {

        outline.enabled = false;
    }

    public bool CheckDistance()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float MaxDistanceAllowed = player.GetComponent<PlayerScript>().MaxRaycastDistance;

        if (Vector3.Distance(player.transform.position, transform.position) > MaxDistanceAllowed)
        {
            return false;

        }
        else
        {
            return true;
        }

    }
}
