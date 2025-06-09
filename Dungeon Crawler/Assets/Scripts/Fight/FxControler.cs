using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxControler : MonoBehaviour
{
    [Header("Physique")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 dir = Vector2.up;
    [SerializeField] private float speed;

    [Header("Destroy condition")]
    [SerializeField] private float lifeTime;
    private float _timeLeft;
    [SerializeField] private bool hasToReachPoint;
    private Vector3 _pointToReach;

    private void Start()
    {
        _timeLeft = lifeTime;

        Vector2 dir = this.dir;
        dir.y *= transform.parent.localScale.y;
        rb.velocity = dir * speed;
    }

    private void Update()
    {
        if (hasToReachPoint)
        {
            if (transform.position.x >= _pointToReach.x)
            {
                Destroy(gameObject);
            }

            return;
        }


        _timeLeft -= Time.deltaTime;

        if (_timeLeft < 0)
            Destroy(gameObject);
    }

    public void SetPointToReach(Vector3 point)
    {
        _pointToReach = point;
    }
}
