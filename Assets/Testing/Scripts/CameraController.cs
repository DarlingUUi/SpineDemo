using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Public Variable

    public Transform m_trfPlayer;

    #endregion

    #region Original Functions

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = m_trfPlayer.position - Vector3.forward * 10f;
    }

    #endregion
}
