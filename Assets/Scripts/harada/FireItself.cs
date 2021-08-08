using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireItself : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        // プレイヤーが赤を持っていて能力を使っている場合は通れるように
        if (player.GetPlayerColor() == ColorManager.Color_Type.Red && player.isUsingAbility())
        {
            // コライダーを無効化
            GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            GetComponent<Collider2D>().enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().Death(transform.parent.GetComponent<ColorObject>().GetColorType());

            GetComponent<SpriteRenderer>().color = ColorManager.GetOff();
        }
    }
}
