using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundChecker))]
public class Player : MonoBehaviour {
    [SerializeField]
    float speed = 10;

    [Tooltip("�傫������ƃL�r�L�r�����i�傫������ƃo�O��j")]
    [SerializeField]
    float speedFollowing = 30;

    [SerializeField]
    float jump = 5;


    RecolorsInputAction inputActions;
    Rigidbody2D rigid;

    GroundChecker groundChecker;

    GameObject foot;

    void Awake() {
        foot = transform.GetChild(0).gameObject;

        rigid = GetComponent<Rigidbody2D>();
        groundChecker = GetComponent<GroundChecker>();

        inputActions = new RecolorsInputAction();

        inputActions.Player.Jump.started += JumpStarted;
    }

    void OnDisable() => inputActions.Disable();
    void OnDestroy() => inputActions.Disable();

    void OnEnable() => inputActions.Enable();


    void Update() {
        //���ɂ��蔲����p
        var value = inputActions.Player.Move.ReadValue<Vector2>();
        var active=value.y > -0.8f;

        if (foot.activeSelf != active) {
            foot.SetActive(active);
        }

    }

    //�W�����v
    private void JumpStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        if (groundChecker.IsGround) {
            rigid.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
        }
    }


    void FixedUpdate() {
        //���ړ�
        var value = inputActions.Player.Move.ReadValue<Vector2>();

        var move = new Vector2(value.x * speed , 0);

        var moveForce = speedFollowing * (move - rigid.velocity);
        moveForce.y = 0;

        rigid.AddForce(moveForce);

    }
}
