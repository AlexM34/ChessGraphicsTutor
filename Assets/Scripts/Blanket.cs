using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blanket : MonoBehaviour
{
    public Sprite[] sprites;
    private int type = 0;

    private void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
    }

    private void Update()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[type];
        Vector3 scale = new Vector3(1f, 2f, 1f);
        transform.localScale = scale;
    }

    public void Change (int t)
    {
        type = t;
    }
}
