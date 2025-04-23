using UnityEngine;

public class TimeSpeedTesting : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale = 2f;
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Time.timeScale = 3f;
        }
    }
}