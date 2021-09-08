using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothWobble : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 offset;
    [SerializeField] float smoothSpeed = 0.08f;

    private void Start()
    {
        startPos = transform.position;
    }

    private void LateUpdate()
    {
        Wobble();
    }

    private void Wobble()
    {
        transform.position = new Vector3(Mathf.Cos(offset.x) * smoothSpeed, Mathf.Sin(offset.y) * smoothSpeed, startPos.z);
        offset.x += Time.deltaTime * 0.05f;
        offset.y -= Time.deltaTime;
    }
}
