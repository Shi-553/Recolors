using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllColor : MonoBehaviour
{
    ColorManager color_manager;

    // �F�������Ă��邩�����ĂȂ���
    bool[] isHaving; 

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
    }

    // Update is called once per frame
    void Update()
    {



    }
}
