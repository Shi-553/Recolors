using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �F��؂�ւ����肷��N���X�AUI�Ƃ̂��������܂�
/// </summary>
public class ControllColor : MonoBehaviour
{
    ColorManager color_manager;

    // �L�����o�X
    GameObject canvas;

    // UI�T�[�N���p�̏��
    GameObject[] Circles;
    GameObject[] MonoCircles;
    Vector3[] c_SettingPos;

    Vector3 Vanish_Pos;

    // �I�����̃t�H�[�J�X�摜
    GameObject Focus_Circle;
    Vector3 Pos_modify_focus;

    // �T�[�N���\������
    float Time_circlesShow;
    bool isShowing = false;

    // �F�������Ă��邩�����ĂȂ���
    bool[] isHaving;

    // ���݂̔\�͂̎w�W
    int index_CurPow = -1;

    public void SetInputActions(RecolorsInputAction inp)
    {
        inp.Player.SwitchAbility.started += SwitchAbilityStarted;
    }

    // Start is called before the first frame update
    void Start()
    {
        // �}�l�[�W���[���擾
        color_manager = GameObject.FindObjectOfType<ColorManager>();

        // �u�[��������
        isHaving = new bool[(int)ColorManager.Color_Type.c_Max];

        // �T�[�N��������
        Vanish_Pos = new Vector3(1000f, 1000f, 0);

        Circles = new GameObject[(int)ColorManager.Color_Type.c_Max];
        MonoCircles = new GameObject[(int)ColorManager.Color_Type.c_Max];
        c_SettingPos = new Vector3[(int)ColorManager.Color_Type.c_Max];

        GameObject temp_g = Resources.Load<GameObject>("Sprites/Monochrome_g");

        canvas = GameObject.Find("Canvas");

        for (var i = 0; i < (int)ColorManager.Color_Type.c_Max; ++i)
        {
            isHaving[i] = false;

            ColorManager.Color_Type temp = (ColorManager.Color_Type)i;
            Circles[i] = canvas.transform.Find(temp.ToString() + "Circle").gameObject;

            // �T�[�N��������ꍇ
            if (Circles[i] != null)
            {
                c_SettingPos[i] = new Vector3(Circles[i].transform.position.x - canvas.transform.position.x,
                    Circles[i].transform.position.y, Circles[i].transform.position.z);

                // �ŏ��͔�����z�u����
                MonoCircles[i] = Instantiate<GameObject>(temp_g);
                MonoCircles[i].transform.parent = canvas.transform;

                MonoCircles[i].transform.position = Vanish_Pos;
                Circles[i].transform.position = Vanish_Pos;
            }
            else
            {
                MonoCircles[i] = Instantiate<GameObject>(temp_g, Vanish_Pos, Quaternion.identity);
            }
        }

        Focus_Circle = canvas.transform.Find("Focus").gameObject;

        Pos_modify_focus = new Vector3(Focus_Circle.transform.position.x - canvas.transform.position.x
            - c_SettingPos[0].x, Focus_Circle.transform.position.y - c_SettingPos[0].y,
            Focus_Circle.transform.position.z);

        // �����֔�΂�
        Focus_Circle.transform.position = Vanish_Pos;
    }

    // Update is called once per frame
    void Update()
    {
        VanishCircles();

    }

    private void VanishCircles()
    {
        if (isShowing)
        {
            // �\������������
            Time_circlesShow -= Time.deltaTime;

            if (Time_circlesShow <= 0f)
            {
                isShowing = false;

                Time_circlesShow = 0f;

                // �t�H�[�J�X�T�[�N��������
                Focus_Circle.transform.position = Vanish_Pos;

                // �e�T�[�N��������
                for (var k = 0; k < (int)ColorManager.Color_Type.c_Max; ++k)
                {
                    if (Circles[k] != null)
                    {
                        if (isHaving[k])
                        {
                            Circles[k].transform.position = Vanish_Pos;
                        }
                        else
                        {
                            MonoCircles[k].transform.position = Vanish_Pos;

                        }
                    }
                }
            }
        }
    }


    public void SetColorActiveState(ColorManager.Color_Type col,bool state)
    {
        isHaving[(int)col] = state;
    }
    
    private void SwitchAbilityStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        // �֐����Ă΂�Ă��邩�`�F�b�N
        //Debug.Log("SwtichAbility Working");

        // �\�͂������Ă��邩����
        var fal_num = 0;

        for (var i = 0; i < (int)ColorManager.Color_Type.c_Max; ++i)
        {
            fal_num++;

            if (isHaving[i])
            {
                break;
            }
        }

        // �\�͂���������ĂȂ��ꍇ
        if (fal_num == (int)ColorManager.Color_Type.c_Max)
        {
            return;
        }
        else
        {
            // �T�[�N���̕\������
            isShowing = true;

            Time_circlesShow = 2f;

            for (var k = 0; k < (int)ColorManager.Color_Type.c_Max; ++k)
            {
                if (Circles[k] != null)
                {
                    if (isHaving[k])
                    {
                        Circles[k].transform.position = new Vector3(c_SettingPos[k].x + canvas.transform.position.x,
                            c_SettingPos[k].y, c_SettingPos[k].z);

                        MonoCircles[k].transform.position = Vanish_Pos;
                    }
                    else
                    {
                        Circles[k].transform.position = Vanish_Pos;

                        MonoCircles[k].transform.position = new Vector3(c_SettingPos[k].x + canvas.transform.position.x,
                            c_SettingPos[k].y, c_SettingPos[k].z);
                    }
                }
            }

            // �F�̐؂�ւ�����
            for (var j = 0; ; j++)
            {
                // �\�͂̐؂�ւ�����
                index_CurPow++;

                if (index_CurPow >= (int)ColorManager.Color_Type.c_Max)
                {
                    index_CurPow = 0;
                }

                // �C���f�b�N�X�ɑΉ�����F�������Ă�����u���[�N
                if (isHaving[index_CurPow])
                {
                    GetComponent<Player>().SetPlayerColor((ColorManager.Color_Type)index_CurPow);

                    // �t�H�[�J�X�I�u�W�F�N�g���ړ�������
                    Focus_Circle.transform.position = new Vector3(c_SettingPos[index_CurPow].x + canvas.transform.position.x
                        + Pos_modify_focus.x, c_SettingPos[index_CurPow].y + Pos_modify_focus.y, Pos_modify_focus.z);

                    break;
                }

                // �������[�v�΍�
                if (j > (int)ColorManager.Color_Type.c_Max)
                {
                    break;
                }
            }

        }
    }
}
