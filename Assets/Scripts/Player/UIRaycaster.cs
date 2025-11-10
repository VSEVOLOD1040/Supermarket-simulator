using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIRaycaster : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float maxDistance = 10f;


    void Update()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = new Vector2(Screen.width / 2, Screen.height / 2);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        

        if (results.Count > 0)
        {
            foreach (var r in results)
            {
                var button = r.gameObject.GetComponent<Button>();
                if (button != null)
                {

                    if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
                    {
                        button.onClick.Invoke();
                        Debug.Log("Clicked button: " + button.name);
                    }
                    break;
                }
            }
        }
    }
}