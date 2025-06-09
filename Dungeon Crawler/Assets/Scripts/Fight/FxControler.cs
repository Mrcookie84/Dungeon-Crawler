using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxControler : MonoBehaviour
{
    [Header("Physique")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;

    [Header("Destroy condition")]
    [SerializeField] private float lifeTime;
    private float _timeLeft;

    private void Start()
    {
        _timeLeft = lifeTime;

        Vector2 dir = Vector2.up;
        dir.y *= transform.parent.localScale.y;
        rb.velocity = dir * speed;
    }

    private void Update()
    {
        _timeLeft -= Time.deltaTime;

        if (_timeLeft < 0)
            Destroy(gameObject);
    }
}
