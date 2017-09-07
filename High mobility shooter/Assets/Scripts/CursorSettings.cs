using UnityEngine;

public class CursorSettings : MonoBehaviour {

    public CursorLockMode cursorMode = CursorLockMode.None;
    public bool lockCursor;

    private void Update()
    {
        if (lockCursor)
        {
            Cursor.lockState = cursorMode;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
