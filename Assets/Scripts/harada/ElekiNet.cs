using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElekiNet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Player>()?.Death(GetComponent<ColorObject>().GetColorType());
    }
}
