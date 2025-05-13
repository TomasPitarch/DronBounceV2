using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotateComponent : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotationSpeed;
    
    [SerializeField]
    private RectTransform rectTransform;
   
    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.rotation = Quaternion.Euler((transform.rotation.eulerAngles) + (rotationSpeed * Time.deltaTime));
    }
}
