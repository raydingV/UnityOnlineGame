using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    PhotonView pw;
    void Start()
    {
        pw = GetComponent<PhotonView>();
    }
    
    void Update()
    {
        if (pw.IsMine)
        {
            hareket();
        }
    }

    void hareket()
    {
        // WASD tuşlarına göre hareket etme
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Hareket vektörünü oluştur
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * 5f * Time.deltaTime;

        // Küpü hareket ettir
        transform.Translate(movement);
    }
}
