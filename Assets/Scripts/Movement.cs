using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 1;

    // Update is called once per frame
    void Update()
    {
      this.transform.position += Time.deltaTime * moveSpeed * new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
