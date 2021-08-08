using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    Player player;

    [SerializeField]
    float offset;
    [SerializeField]
    float lerp = 10.0f;


    float minX;
    void Start() {
        player = GameObject.FindObjectOfType<Player>();
        minX = transform.position.x;

    }


    void LateUpdate() {
        var a = transform.position.x;
        var b = player.transform.position.x;
        if (player.IsFront) {
            b += offset;
        }
        else {
            b -= offset;
        }

        b = Mathf.Max(b, minX);


        var pos = transform.position;
        pos.x = Mathf.Lerp(a, b, lerp * Time.deltaTime);

        transform.position = pos;
    }
}
