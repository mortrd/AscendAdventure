using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] float cameraLag = 1.4f;

    private void Start()
    {

    }
    private void Update()
    {
        if(transform.position.y < Player.transform.position.y + cameraLag)
        {
            Vector3 newpos = new Vector3 (transform.position.x, Player.transform.position.y + cameraLag, transform.position.z);
            transform.position = newpos;
        }
    }
}
