using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {
    private Renderer rend;
    public GameObject go;
   // public int i;


    void Start()
    {
        rend = go.GetComponent<Renderer>();

    }

    void Update()
    {
        Vector3 center = go.GetComponent<Renderer>().bounds.center;
        float radius = rend.bounds.extents.magnitude;
        //Debug.Log("Bounding Radius: " + radius);

    }
    
}
