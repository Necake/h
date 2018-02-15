using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTiling : MonoBehaviour {

    public float parallax = 2f;

    MeshRenderer mr;
    Material mat;
    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        mat = mr.material;
    }

    private void Update()
    {
        Vector2 offset = mat.mainTextureOffset;

        //offset.x += Time.deltaTime; //loops 1 time per sec
        offset.x = transform.position.x / transform.localScale.x / parallax;
        offset.y = transform.position.y / transform.localScale.y / parallax;

        mat.mainTextureOffset = offset;
    }

}
