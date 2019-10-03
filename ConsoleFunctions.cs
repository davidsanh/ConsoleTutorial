using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleFunctions : MonoBehaviour
{
    public GameObject sphere;
    public GameObject cube;

    public void ChangeColor(Color color)
    {
        sphere.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
        cube.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
    }

    public void ChangeSize(int size)
    {
        sphere.transform.localScale = new Vector3(size,size,size);
        cube.transform.localScale = new Vector3(size, size, size);
    }
}
