using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShooter : MonoBehaviour
{
    SpriteRenderer fire;

    // Start is called before the first frame update
    void Start()
    {
        fire = transform.Find("FireArea").gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        fire.color = GetComponent<SpriteRenderer>().color;
    }
}
