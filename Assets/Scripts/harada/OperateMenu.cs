using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OperateMenu : MonoBehaviour
{
    bool ToggleOpen = false;


    // ���삷�郁�j���[�B
    GameObject Button_retry;
    GameObject Button_titleBack;
    GameObject Image_backGround;

    // �ŏ��̈ʒu��ێ����Ă���
    Vector3 Pos_relative_retry;
    Vector3 Pos_relative_titleBack;
    Vector3 Pos_relative_backGround;
    
    // �{�^���Ȃǂ𐁂���΂��ʒu
    Vector3 PosVanish;

    public void SetInputActions(RecolorsInputAction inp)
    {
        // �֐����o�C���h
        inp.Player.Menu.started += MenuStarted;
    }

    // Start is called before the first frame update
    void Start()
    {
        // �I�u�W�F�N�g���擾 
        Button_retry = transform.Find("Retry").gameObject;
        Button_titleBack = transform.Find("BacktoTitle").gameObject;
        Image_backGround = transform.Find("MenuBG").gameObject;

        // ���Έʒu���擾
        Pos_relative_retry = Button_retry.transform.position - transform.position;
        Pos_relative_titleBack = Button_titleBack.transform.position - transform.position;
        Pos_relative_backGround = Image_backGround.transform.position - transform.position;

        PosVanish = new Vector3(0f, -10000f, 0f);

        // �ŏ��͔�\���ɂ��Ă���
        Button_retry.transform.position = PosVanish;
        Button_titleBack.transform.position = PosVanish;
        Image_backGround.transform.position = PosVanish;
    }

    private void MenuStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        // �֐����Ă΂�Ă��邩�`�F�b�N
        Debug.Log("Menu Working");

        // ���j���[��\�����Ƀ��j���[���J��
        if (!ToggleOpen)
        {
            ToggleOpen = true;

            Button_retry.transform.position = transform.position + Pos_relative_retry;
            Button_titleBack.transform.position = transform.position + Pos_relative_titleBack;
            Image_backGround.transform.position = transform.position + Pos_relative_backGround;
        }
        // ���j���[�W�J���Ƀ��j���[�����

        else
        {
            ToggleOpen = false;

            Button_retry.transform.position = PosVanish;
            Button_titleBack.transform.position = PosVanish;
            Image_backGround.transform.position = PosVanish;
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BacktoTitle()
    {
        SceneManager.LoadScene("Main");
    }
}
