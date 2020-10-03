using UnityEngine;

public class QuitOnEscape : MonoBehaviour
{
#if PLATFORM_STANDALONE_WIN
    private void Update () {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
#endif
}
