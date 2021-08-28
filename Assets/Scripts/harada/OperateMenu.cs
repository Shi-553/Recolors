using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperateMenu : MonoBehaviour
{
    bool ToggleOpen = false;


    // 操作するメニュー達
    GameObject Button_retry;
    GameObject Button_titleBack;
    GameObject Image_backGround;

    // 最初の位置を保持しておく
    Vector3 Pos_relative_retry;
    Vector3 Pos_relative_titleBack;
    Vector3 Pos_relative_backGround;
    
    // ボタンなどを吹き飛ばす位置
    Vector3 PosVanish;

    public void SetInputActions(RecolorsInputAction inp)
    {
        // 関数をバインド
        inp.Player.Menu.started += MenuStarted;
    }

    // Start is called before the first frame update
    void Start()
    {
        // オブジェクトを取得 
        Button_retry = transform.Find("Retry").gameObject;
        Button_titleBack = transform.Find("BacktoTitle").gameObject;
        Image_backGround = transform.Find("MenuBG").gameObject;

        // 相対位置を取得
        Pos_relative_retry = Button_retry.transform.position - transform.position;
        Pos_relative_titleBack = Button_titleBack.transform.position - transform.position;
        Pos_relative_backGround = Image_backGround.transform.position - transform.position;

        PosVanish = new Vector3(0f, -10000f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MenuStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        // 関数が呼ばれているかチェック
        Debug.Log("Menu Working");

        // メニュー展開中にメニューを閉じる
        if (ToggleOpen)
        {
            ToggleOpen = false;

            Button_retry.transform.position = transform.position + Pos_relative_retry;
            Button_titleBack.transform.position = transform.position + Pos_relative_titleBack;
            Image_backGround.transform.position = transform.position + Pos_relative_backGround;
        }
        // メニュー非表示中
        else
        {
            ToggleOpen = true;

            Button_retry.transform.position = PosVanish;
            Button_titleBack.transform.position = PosVanish;
            Image_backGround.transform.position = PosVanish;
        }
    }
}
