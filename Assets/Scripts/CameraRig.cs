using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public PlayerMovement player;
    public Transform twistPivot; // Đặt TwistPivot
    public Transform pitchPivot; // Đặt PitchPivot

    public float twistSpeed = 100f; // Tốc độ xoay ngang
    public float pitchSpeed = 100f; // Tốc độ xoay dọc
    public float minPitch = -30f;   // Giới hạn góc nghiêng thấp nhất
    public float maxPitch = 60f;    // Giới hạn góc nghiêng cao nhất
    private float currentPitch = 0f; // Lưu trạng thái pitch hiện tại
    public Quaternion defaultRotation; // Lưu trạng thái ban đầu của twistPivot

    void Start()
    {
        //Locking the cursor to the middle of the screen and making it invisible
        Cursor.lockState = CursorLockMode.Locked;
        player = GameObject.Find("FemaleCharacterPBR").GetComponent<PlayerMovement>();
        // Lưu lại góc xoay ban đầu của twistPivot
        defaultRotation = twistPivot.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Nhận input từ chuột
        float twistInput = Input.GetAxis("Mouse X") * twistSpeed * Time.deltaTime;
        float pitchInput = -Input.GetAxis("Mouse Y") * pitchSpeed * Time.deltaTime;

        // xoay ngang nhân vật
        player.transform.Rotate(0f, twistInput, 0f);

        // Tính toán và giới hạn pitch
        currentPitch = Mathf.Clamp(currentPitch + pitchInput, minPitch, maxPitch);
        pitchPivot.localRotation = Quaternion.Euler(currentPitch, 0f, 0f);
    }
}
