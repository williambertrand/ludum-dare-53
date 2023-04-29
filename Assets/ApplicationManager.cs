using OTBG.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : Singleton<ApplicationManager>
{
    public void ExitGame()
    {
        Application.Quit(); 
    }
}
