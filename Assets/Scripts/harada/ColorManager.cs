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
        c_Max
    };

    // �F�̌������X�g���쐬
    List<ColorObject>[] array_listColors;

    // ���ꂼ��̐F���
    static Color32[] array_MaterialColors = new Color32[(int)Color_Type.c_Max];
    static bool isInit = false;

    // ���F
    static Color32 white_MaterialColor;

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

    static void ColorInit()
    {
        // ����̐F��ǂݍ���
        for (var i = 0; i < (int)Color_Type.c_Max; ++i)
        {
            Color_Type temp = (Color_Type)i;
            array_MaterialColors[i] = Resources.Load<Material>("Materials/" + temp.ToString()).color;
        }

        white_MaterialColor = Resources.Load<Material>("Materials/White").color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    static public string GetOriginalColorLabel(Color_Type color) {
        switch (color) {
            case Color_Type.Blue:
                return "Entry";
            case Color_Type.Red:
                return "Entry_1";
            case Color_Type.Yellow:
                return "Entry_0";

            default:
                return "Entry_0";
        }
    }
    // ����̐F���擾
    static public Color32 GetOriginalColor(Color_Type color)
    {
        // �������ς݂��`�F�b�N
        if (isInit == false)
        {
            ColorInit();

            isInit=true;
        }

        // �擾�������F
        var c_num = (int)color;

        // i�͐F�ɑΉ����鐔��
        for (var i = 0; i < (int)Color_Type.c_Max; ++i)
        {
            if (c_num == i)
            {
                return array_MaterialColors[i];
            }
        }

        // ���s���ɂ�0�Ԃ�ԋp
        return array_MaterialColors[0];
    }

    static public string GetWhiteLabel()
    {
        return "Entry_0";
    }
    static public Color32 GetWhite()
    {
        return white_MaterialColor;
    }
    static public Color32 GetOff()
    {
        return new Color(0.2f,0.2f,0.2f,1);
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
