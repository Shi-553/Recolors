using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllColor : MonoBehaviour
{
    ColorManager color_manager;

    GameObject[] Circles;

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

        for(var i = 0; i < (int)ColorManager.Color_Type.c_Max; ++i)
        {
            isHaving[i] = false;
        }

        // �T�[�N���擾
    }

    // Update is called once per frame
    void Update()
    {



    }

    public void SetColorActive(ColorManager.Color_Type col)
    {
        isHaving[(int)col] = true;
    }

    private void SwitchAbilityStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        // �֐����Ă΂�Ă��邩�`�F�b�N
        Debug.Log("SwtichAbility Working");

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
        if (fal_num == 0)
        {
            return;
        }
        else
        {
            // �F�̐؂�ւ�����
            for (var j = 0; ; j++)
            {
                index_CurPow++;

                if (index_CurPow >= (int)ColorManager.Color_Type.c_Max)
                {
                    index_CurPow = 0;
                }

                // �C���f�b�N�X�ɑΉ�����F�������Ă�����u���[�N
                if (isHaving[index_CurPow])
                {
                    GetComponent<Player>().SetPlayerColor((ColorManager.Color_Type)index_CurPow);

                    break;
                }

                if (j > (int)ColorManager.Color_Type.c_Max)
                {
                    break;
                }
            }

        }
    }
}
