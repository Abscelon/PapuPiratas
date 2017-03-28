using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class cssBaseStructure : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void OnMouseDrag()
    {
        DeActivate();
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(pos.x, pos.y, 0f);
    }

    private void OnMouseUp()
    {
        col.enabled = true;
    }

    public void Activate()
    {
        rb.WakeUp();
        rb.gravityScale = 1.0f;
        col.enabled = true;
    }

    public void DeActivate()
    {
        rb.Sleep();
        rb.gravityScale = 0f;
        col.enabled = false;
    }
}
