using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

/// <summary>
/// プレイヤー
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundChecker))]
[RequireComponent(typeof(ControllColor))]
public class Player : MonoBehaviour {
    [SerializeField]
    float speed = 10;

    [Tooltip("大きくするとキビキビ動く（大きすぎるとバグる）")]
    [SerializeField]
    float speedFollowing = 30;

    [SerializeField]
    float jump = 5;

    [SerializeField]
    float waterJump = 3;

    [Tooltip("電気の能力で移動する速さ、\n大きくするほど距離が伸びる")]
    [SerializeField]
    float elec_move = 6f;
    bool isMoving_yellow = false;

    // 黄色の時間に関する変数、コルーチンがうまくいかなかったので分けて制御
    float time_flashing = 0.1f;
    float time_sinceStartedFlash;

    RecolorsInputAction inputActions;
    Rigidbody2D rigid;

    GroundChecker groundChecker;

    GameObject foot;

    bool isColor = false;
    ColorManager.Color_Type current;

    ColorManager manager;
    Vector3 respawnPos;

    Animator animator;

    // 服だけの色情報を管理するリスト
    SpriteResolver[] spriteResolvers;

    // 

    GrabedObject grabedObject;

    [SerializeField]
    float abilityDuration = 5;
    [SerializeField]
    float abilityCoolDown = 2;

    Coroutine abilityDurationCo = null;
    Coroutine abilityCoolDownCo = null;

    float yScale;

    bool lastIsGround = false;

    public bool IsFront { private set; get; } = true;

    void Awake() {
        animator = GetComponentInChildren<Animator>();
        yScale = transform.localScale.y;

        respawnPos = transform.position;

        isColor = false;

        foot = transform.GetChild(0).gameObject;

        rigid = GetComponent<Rigidbody2D>();
        groundChecker = GetComponent<GroundChecker>();


        spriteResolvers = GetComponentsInChildren<SpriteResolver>();

        inputActions = new RecolorsInputAction();

        inputActions.Player.Jump.started += JumpStarted;
        inputActions.Player.UseAbility.started += UseAbilityStarted;
        inputActions.Player.UseAbility.canceled += UseAbilityCanceled;
        inputActions.Player.Grab.started += GrabStarted;
        inputActions.Player.Grab.canceled += GrabCanceled; ;
        inputActions.Player.ReturnColor.started += ReturnColorStarted;

        con_color = GetComponent<ControllColor>();
        con_color.SetInputActions(inputActions);

        // メニュー操作をバインド
        GameObject.Find("Canvas")?.GetComponent<OperateMenu>()?.SetInputActions(inputActions);
    }

    private void ReturnColorStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        manager.RecoverColor(current);
        isColor = false;
        // 能力の情報を追加
        con_color.SetColorActiveState(current, false);

        // 服の色を変更
        for (var i = 0; i < spriteResolvers.Length; i++) {
            var cate = spriteResolvers[i].GetCategory();

            spriteResolvers[i].SetCategoryAndLabel(cate ,ColorManager.GetWhiteLabel());
        }
    }

    private void GrabCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        grabedObject?.GrabEnd();
    }

    private void GrabStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        grabedObject?.GrabBegin(GetComponent<Rigidbody2D>());
    }

    private void Start() {
        manager = GameObject.Find("GameMaster").GetComponent<ColorManager>();
    }

    private void UseAbilityStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        if (!isColor || abilityCoolDownCo != null) {
            return;
        }
        switch (current) {
            case ColorManager.Color_Type.Blue:
                var scale = transform.localScale;
                scale.y = yScale / 3;
                transform.localScale = scale;
                break;
            case ColorManager.Color_Type.Red:
                break;
            case ColorManager.Color_Type.Yellow:
                isMoving_yellow = true;
                time_sinceStartedFlash = 0f;

                // 一度停止させる
                rigid.velocity = Vector2.zero;

                var el = IsFront ? elec_move : -elec_move;
                rigid.AddForce(new Vector2(el, 0f), ForceMode2D.Impulse);

                abilityCoolDownCo = StartCoroutine(AbilityCoolDown());
                return;

            case ColorManager.Color_Type.c_Max:
                break;
            default:
                break;
        }
        abilityDurationCo = StartCoroutine(AbilityDuration());
    }
    private void UseAbilityCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        if (!isColor || abilityCoolDownCo != null) {
            return;
        }
        switch (current) {
            case ColorManager.Color_Type.Blue:
                var scale = transform.localScale;
                scale.y = yScale;
                transform.localScale = scale;
                break;
            case ColorManager.Color_Type.Red:
                break;
            case ColorManager.Color_Type.Yellow:

                break;

            case ColorManager.Color_Type.c_Max:
                break;
            default:
                break;
        }
        if (abilityDurationCo != null) {
            StopCoroutine(abilityDurationCo);
            abilityDurationCo = null;
        }

        abilityCoolDownCo = StartCoroutine(AbilityCoolDown());
    }

    IEnumerator AbilityDuration() {
        float startTime = Time.time;
        float currentTime = Time.time;

        while (currentTime - startTime < abilityDuration) {
            currentTime = Time.time;



            yield return null;
        }

        UseAbilityCanceled(new UnityEngine.InputSystem.InputAction.CallbackContext());
        abilityDurationCo = null;
    }
    IEnumerator AbilityCoolDown() {
        float startTime = Time.time;
        float currentTime = Time.time;

        while (currentTime - startTime < abilityCoolDown) {
            currentTime = Time.time;



            yield return null;
        }

        abilityCoolDownCo = null;
    }


    void OnDisable() => inputActions.Disable();
    void OnDestroy() => inputActions.Disable();

    void OnEnable() => inputActions.Enable();


    void Update() {
        //下にすり抜ける用
        var value = inputActions.Player.Move.ReadValue<Vector2>();
        var active = value.y > -0.5f;

        if (foot.activeSelf != active) {
            foot.SetActive(active);
        }

        if (!groundChecker.IsGround && lastIsGround) {
            animator.SetBool("jump", true);
        }
        if (groundChecker.IsGround && !lastIsGround) {
            animator.SetBool("jump", false);
        }


        lastIsGround = groundChecker.IsGround;
    }

    //ジャンプ
    private void JumpStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        if (groundChecker.IsGround) {
            var j = (current == ColorManager.Color_Type.Blue && abilityDurationCo != null) ? waterJump : jump;

            rigid.AddForce(new Vector2(0, j), ForceMode2D.Impulse);

        }
    }


    void FixedUpdate() {
        //横移動
        var value = inputActions.Player.Move.ReadValue<Vector2>();

        animator.SetFloat("speed", Mathf.Abs(value.x));

        if (grabedObject != null && grabedObject.IsGrab) {
            grabedObject.GrabMove(value.x);
            return;
        }
        var move = new Vector2(value.x * speed, 0);

        var moveForce = speedFollowing * (move - rigid.velocity);
        moveForce.y = 0;

        rigid.AddForce(moveForce);

        // 体の向きを買える
        if (value.x > 0.1f) {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            IsFront = true;
        }
        if (value.x < -0.1f) {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            IsFront = false;
        }

        // 黄色の能力を使っているとき
        if (current == ColorManager.Color_Type.Yellow && isMoving_yellow)
        {
            rigid.AddForce(new Vector2(0, 9.81f));

            time_sinceStartedFlash += Time.deltaTime;

            if (time_sinceStartedFlash > time_flashing)
            {
                isMoving_yellow = false;
            }
        }
    }

    public void Death(ColorManager.Color_Type type) {
        if (type == ColorManager.Color_Type.Blue && type == current && abilityDurationCo != null) {
            return;
        }
        if (type == ColorManager.Color_Type.Red && type == current && abilityDurationCo != null) {
            return;
        }
        if(type==ColorManager.Color_Type.Yellow && type == current && abilityDurationCo != null)
        {
            return;
        }

        transform.position = respawnPos;
        var cameraPos = Camera.main.transform.position;
        cameraPos.x = respawnPos.x;
        Camera.main.transform.position = cameraPos;

        current = type;
        manager.TurnMonochrome(current);

        // 能力の情報を追加
        con_color.SetColorActiveState(type, true);

        // 服の色を変更
        SetPlayerColor(current);


        isColor = true;
    }


    private void OnTriggerEnter2D(Collider2D collision) {

        grabedObject ??= collision.GetComponent<GrabedObject>();
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (grabedObject == collision.GetComponent<GrabedObject>()) {
            grabedObject?.GrabEnd();
            grabedObject = null;
        }

        if (collision.CompareTag("RespawnPoint")) {
            respawnPos = collision.transform.position;
        }
    }


    // ControllColor　から呼び出し /////////////////////////////////////
    ControllColor con_color;

    public RecolorsInputAction GetInputAction() {
        return inputActions;
    }

    // 色を設定
    public void SetPlayerColor(ColorManager.Color_Type c_type) {
        current = c_type;

        // 服の色を変更
        for (var i = 0; i < spriteResolvers.Length; i++) {
            var cate = spriteResolvers[i].GetCategory();

            spriteResolvers[i].SetCategoryAndLabel(cate, ColorManager.GetOriginalColorLabel(current));
        }
    }

    public ColorManager.Color_Type GetPlayerColor()
    {
        return current;
    }

    public bool isUsingAbility()
    {
        if (abilityDurationCo != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GetStateofYellow()
    {
        return isMoving_yellow;
    }
}
