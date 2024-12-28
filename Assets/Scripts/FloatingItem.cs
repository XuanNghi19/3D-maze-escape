using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    public float floatAmplitude = 0.02f; // Biên độ lơ lửng
    public float floatFrequency = 1f; // Tần số lơ lửng
    public float rotationSpeed = 50f; // Tốc độ xoay

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // Lưu vị trí ban đầu của item
    }

    void Update()
    {
        if (gameObject.CompareTag("Heart"))
        {
            // Tạo hiệu ứng lơ lửng
            float newY = startPosition.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }

        // Tạo hiệu ứng xoay
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
