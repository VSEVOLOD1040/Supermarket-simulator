using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DoorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Open();

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Close();
        }
    }

    public Transform Door;
    public bool IsOpen = false;
    public float Duration = 1f;

    public float OpenAngle = -90f;



    public void SwitchDoor()
    {
        if (IsOpen)
        {
            Close();
        }
        else
        {
            Open();
        }   
    }

    public void Open()
    {
        Door.DOKill();

        Door.DOLocalRotate(new Vector3(0, OpenAngle, 0), Duration).SetEase(Ease.OutQuad);

        IsOpen=true;
    }
    public void Close()
    {
        Door.DOKill();
        Door.DOLocalRotate(new Vector3(0, 0, 0), Duration).SetEase(Ease.OutQuad);
        IsOpen = false;

    }
}
