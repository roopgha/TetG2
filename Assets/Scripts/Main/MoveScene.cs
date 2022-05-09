using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MoveScene : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("2048");
    }
}

