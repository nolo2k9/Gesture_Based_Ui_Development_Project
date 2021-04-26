using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    //Spped of rotation
    public float spinSpeed = 3600;
    public bool doSpin = false;

    private Rigidbody rb;

    public GameObject playerGraphics;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        // Apply a rotation if the bool is truthy
        if (doSpin)
        {
            playerGraphics.transform.Rotate(new Vector3(0, spinSpeed * Time.deltaTime, 0));
        }
    }
}
