using UnityEngine;

public class DayCycleManager : MonoBehaviour
{
    public Light directionalLight;

    public float timeInSeconds;

    public Vector3 sunriseRotation;
    public Vector3 sunsetRotation;

    public float MaxSecondsInDay;
    private void Start()
    {
        
    }
    void Update()
    {
        timeInSeconds = gameObject.GetComponent<GameManager>().CurrentTime;


        float clampedTime = Mathf.Clamp(timeInSeconds, 0f, MaxSecondsInDay);
        float t = clampedTime / MaxSecondsInDay;
        Vector3 currentRotation = Vector3.Lerp(sunriseRotation, sunsetRotation, t);
        if (directionalLight != null)
        {
            directionalLight.transform.rotation = Quaternion.Euler(currentRotation);
        }
    }
 
}