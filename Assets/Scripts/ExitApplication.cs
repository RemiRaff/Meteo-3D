using UnityEngine;

public class ExitApplication : MonoBehaviour
{
    public void ExitGame()
    {

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif

    }
    //End of the "Exit Game" method}
}
