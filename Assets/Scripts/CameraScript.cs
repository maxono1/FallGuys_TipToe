using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;

    private Vector3 offset;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position - new Vector3(0, -3, 8);
        //hier noch mousex
        //Input.GetAxis("Mouse X");
        //transform.rotation
    }
}
