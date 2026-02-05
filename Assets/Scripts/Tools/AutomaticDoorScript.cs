using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoorScript : MonoBehaviour
{
    public Transform LEFT;
    public Transform RIGHT;
    public float moveDistance = 1.3f;
    public float moveDuration = 1f;

    Vector3 leftClosedPos;
    Vector3 rightClosedPos;
    Vector3 leftOpenPos;
    Vector3 rightOpenPos;

    public bool IsOpen = false;
    public float radius = 3f;
    void Start()
    {
        leftClosedPos = LEFT.localPosition;
        rightClosedPos = RIGHT.localPosition;

        leftOpenPos = leftClosedPos + new Vector3(0, 0, -moveDistance);
        rightOpenPos = rightClosedPos + new Vector3(0, 0, moveDistance);
    }

    public void OpenDoor()
    {
        
        LEFT.DOKill();
        RIGHT.DOKill();

        LEFT.DOLocalMove(leftOpenPos, moveDuration).SetEase(Ease.OutQuad);
        RIGHT.DOLocalMove(rightOpenPos, moveDuration).SetEase(Ease.OutQuad);

        IsOpen = true;
    }

    public void CloseDoor()
    {
        LEFT.DOKill();
        RIGHT.DOKill();

        LEFT.DOLocalMove(leftClosedPos, moveDuration).SetEase(Ease.OutQuad);
        RIGHT.DOLocalMove(rightClosedPos, moveDuration).SetEase(Ease.OutQuad);

        IsOpen = false;
    }

    public void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //               OpenDoor();

        //}else if(Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    CloseDoor();
        //}

        CheckPeopleInRadius();

    }

    public void CheckPeopleInRadius()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        int PeopleCount = 0;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player") || hitCollider.CompareTag("Client"))
            {
                PeopleCount++;

            }
        }

        if (PeopleCount > 0)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
