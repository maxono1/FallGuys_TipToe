using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private float verticalOffset;
    [SerializeField] private Vector3 distance;
    [SerializeField] private float rotationSpeed = 100.0f;
    [SerializeField] [Range(0.01f, 1.0f)] private float smoothness = 0.5f;
    [SerializeField] private float minFov = 40.0f;
    [SerializeField] private float maxFov = 60.0f;


    private void Start()
    {
        cameraOffset = this.target.position + distance;
    }

    private void Update()
    {
       
    }


    private void LateUpdate()
    {
        this.ZoomCamera();
        this.OrbitCamera();
    }


    private void ZoomCamera()
    {
        // Jeden Frame checken ob der Nutzer zoomt
        float camFov = GetComponent<Camera>().fieldOfView;

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && camFov > minFov)
        {
            this.GetComponent<Camera>().fieldOfView--;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && camFov < maxFov)
        {
            this.GetComponent<Camera>().fieldOfView++;
        }
    }

    private void OrbitCamera()
    {
        if (Input.GetMouseButton(0))
        {
            Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime, Vector3.up);
            cameraOffset = camTurnAngle * cameraOffset;
        }

        Vector3 newPos = this.target.position + cameraOffset;
        // Sphere Interpolation damit Kamera nicen übergang hat und nicht ruckartig wie ne missgeburt stoppt. 0.01 = nice smooth, 1.00 = missgeburt stop
        this.transform.position = newPos; //Vector3.Slerp(this.transform.position, newPos, smoothness);
        this.transform.LookAt(new Vector3(this.target.position.x, this.target.position.y + verticalOffset, this.target.position.z));
    }
    
   
}
