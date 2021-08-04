using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �F�t���I�u�W�F�N�g�ɃA�^�b�`����N���X
/// </summary>

public class ColorObject : MonoBehaviour
{
    [Tooltip("�V�[���z�u���ɐF�����肵�Ă�������\nc_Max�͑I�����Ȃ��ł�������")]
    [SerializeField]
    ColorManager.Color_Type MyColor;

    // �F�������Ă��邩�ǂ����A�O������擾
    bool havingColor;

    // Start is called before the first frame update
    void Start()
    {
        // �^�O��ύX
        tag = "ColorObject";

        havingColor = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public ColorManager.Color_Type GetColorType()
    {
        // �����̎����Ă���F��ԋp
        return MyColor;
    }

    public bool GethavingColor()
    {
        // �������F�������Ă��邩�ǂ����ԋp
        return havingColor;
    }

    public void TurnOnColor()
    {
        havingColor = true;
    }

    public void TurnOffColor()
    {
        havingColor = false;
    }
}
