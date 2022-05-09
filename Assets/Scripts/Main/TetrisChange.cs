using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TetrisChange : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("Tetris");
    }
}
