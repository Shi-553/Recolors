using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class stage1_s : MonoBehaviour
{
    public void PushButton()
    {
        SceneManager.LoadScene("Blue");
    }
}
