using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mainmenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerSaveManager.DeleteProgess();
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
}
