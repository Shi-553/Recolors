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
        var list_GameObject = new List<GameObject>();
        list_GameObject.AddRange(GameObject.FindGameObjectsWithTag("ColorObject"));

        for (var i = 0; i < (int)Color_Type.c_Max - 1; ++i)
        {
            for (var j = 0; j < list_GameObject.Count; ++j)
            {
                var its_color = list_GameObject[j].GetComponent<ColorObject>();

                // �擾�ł������`�F�b�N
                if (its_color != null)
                {
                    // �F�̃^�C�v���擾�A��r
                    if (i == (int)its_color.GetColorType())
                    {
                        // ���X�g�ɒǉ�
                        array_listColors[i].Add(its_color);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // ����̐F�𔒍��ɕύX
    public void TurnMonochrome(Color_Type color)
    {
        // �����ɕύX����F
        var c_num = (int)color;

        // i�͐F�ɑΉ����鐔��
        for (var i = 0; i < (int)Color_Type.c_Max - 1; ++i)
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

    public void RecoverColor(Color_Type color)
    {
        // ����������F
        var c_num = (int)color;

        // i�͐F�ɑΉ����鐔��
        for (var i = 0; i < (int)Color_Type.c_Max - 1; ++i)
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
