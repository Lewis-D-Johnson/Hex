using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	[SerializeField] float CameraSpeed;

    void Start()
    {
        
    }
	
    void Update()
    {
		if ((Input.GetKey(KeyCode.LeftControl)))
		{
			transform.Translate(Vector3.forward * (CameraSpeed * Input.GetAxis("Vertical")));
		}
		else
		{
			transform.position = new Vector3(transform.position.x + (CameraSpeed * Input.GetAxis("Vertical")), transform.position.y, transform.position.z + (CameraSpeed * Input.GetAxis("Vertical")));
		}


		transform.Translate(Vector3.right * (CameraSpeed * Input.GetAxis("Horizontal")));
	}
}
