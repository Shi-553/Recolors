using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���Ƃ̐ڒn����N���X
/// </summary>
public class GroundChecker : MonoBehaviour {

    GameObject groundGameObject = null;
    public bool IsGround { get => groundGameObject!=null; }

    [Tooltip("���Ƃ݂Ȃ��p�x")]
    [SerializeField]
    float goundThresholdAngle = 30;


    void OnCollisionStay2D(Collision2D other) {
        if (groundGameObject != null) {
            return;
        }
        foreach (var contact in other.contacts) {
            Vector2 dir = Vector2.down;

            Vector2 contactObjectDown = -contact.normal;


            if (Vector2.Angle(contactObjectDown, dir) < goundThresholdAngle) {
                groundGameObject=other.gameObject;
                break;
            }

        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (groundGameObject==other.gameObject) {
            groundGameObject = null;
        }
    }
}