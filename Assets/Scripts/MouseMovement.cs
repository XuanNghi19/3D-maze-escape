using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
  PlayerMovement player;
  public float mouseSensitivity = 100f;
  float xRotation = 0f;
  float YRotation = 0f;

  void Start()
  {
    player = GameObject.Find("FemaleCharacterPBR").GetComponent<PlayerMovement>();
    //Locking the cursor to the middle of the screen and making it invisible
    Cursor.lockState = CursorLockMode.Locked;
  }

  void Update()
  {
    float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
    float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

    //control rotation around x axis (Look up and down)
    xRotation -= mouseY;

    //we clamp the rotation so we cant Over-rotate (like in real life)
    xRotation = Mathf.Clamp(xRotation, -90f, 90f);

    //control rotation around y axis (Look up and down)
    YRotation += mouseX;

    //applying both rotations
    transform.localRotation = Quaternion.Euler(0f, YRotation, 0f);
    player.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
  }
}