using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOffset : MonoBehaviour
{

    [SerializeField] private float speed;
    private float offset;
    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        offset += speed * Time.deltaTime;

        material.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }
}
