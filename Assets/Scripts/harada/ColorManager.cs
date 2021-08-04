using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    // �e�I�u�W�F�N�g�ɕϐ����쐬���Đݒ�
    public enum Color_Type
    {
        Blue = 0,
        Red,
        Yellow,
        Orange,
        Purple,
        Green,
        c_Max
    };

    // �F�̌������X�g���쐬
    List<ColorObject>[] array_listColors;

    // Start is called before the first frame update
    void Start()
    {
        // ���ꂼ��̐F�̃��X�g��������
        array_listColors = new List<ColorObject>[(int)Color_Type.c_Max];

        for (var i = 0; i < (int)Color_Type.c_Max; ++i)
        {
            array_listColors[i] = new List<ColorObject>();
        }

        // �F�t���̃I�u�W�F�N�g��T���Ċe���X�g�Ɋi�[
        var list_GameObject = new List<ColorObject>();
        list_GameObject.AddRange(GameObject.FindObjectsOfType<ColorObject>());

        for (var i = 0; i < (int)Color_Type.c_Max; ++i)
        {
            for (var j = 0; j < list_GameObject.Count; ++j)
            {
                // �F�̃^�C�v���擾�A��r
                if (i == (int)list_GameObject[j].GetColorType())
                {
                    // ���X�g�ɒǉ�
                    array_listColors[i].Add(list_GameObject[j]);
                }
            }

            Debug.Log("" + array_listColors[i].Count);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    // ����̐F�𔒍��ɕύX���鏈��
    public void TurnMonochrome(Color_Type color)
    {
        // �����ɕύX����F
        var c_num = (int)color;

        // i�͐F�ɑΉ����鐔��
        for (var i = 0; i < (int)Color_Type.c_Max; ++i)
        {
            if (c_num == i)
            {
                for (var j = 0; j < array_listColors[i].Count; ++j)
                {
                    array_listColors[i][j].TurnOffColor();
                }

                break;
            }
        }
    }

    // �F�𕜊������鏈��
    public void RecoverColor(Color_Type color)
    {
        // ����������F
        var c_num = (int)color;

        // i�͐F�ɑΉ����鐔��
        for (var i = 0; i < (int)Color_Type.c_Max; ++i)
        {
            if (c_num == i)
            {
                for (var j = 0; j < array_listColors[i].Count; ++j)
                {
                    array_listColors[i][j].TurnOnColor();
                }

                break;
            }
        }
    }
}
