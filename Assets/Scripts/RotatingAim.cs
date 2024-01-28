using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingAim : MonoBehaviour
{
    private GameObject player;
    private float speed = 200f;
    private void Awake()
    {
        player = transform.parent.gameObject;
    }
    void Update()
    {
        transform.RotateAround(player.transform.position, Vector3.forward, speed * Time.deltaTime);
    }
    public Quaternion GetRotation()
    {
        return transform.rotation;
    }
}
