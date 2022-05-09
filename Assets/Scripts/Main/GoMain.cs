using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GoMain : MonoBehaviour
{
    
    public void SceneChange()
    {
        SceneManager.LoadScene("Main");
    }
}
