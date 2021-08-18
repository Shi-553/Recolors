using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    public void PushButton(int num)
    {
        SceneManager.LoadScene("Level_"+num);
    }
}
