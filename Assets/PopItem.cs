using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopItem : MonoBehaviour
{
    public Rigidbody rigi;
    private void Start()
    {
        Destroy(gameObject, 3);
        GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
    }

    public void SetForce(Vector3 dir, int f)
    {
        rigi.AddForce(dir * f);
    }

}
