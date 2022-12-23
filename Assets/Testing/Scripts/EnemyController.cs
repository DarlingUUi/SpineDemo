using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{


    #region Public Variable

    public CharacterController m_characterController;

    #endregion

    #region Local Variables

    bool isGrounded, wasGrounded, isSlope;
    Vector3 v3Velocity;

    #endregion

    #region Original Functions

    // Start is called before the first frame update
    void Start()
    {
        m_characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        v3Velocity.x = 0.2f;
        if (!isGrounded)
        {
            if (wasGrounded)
            {
                v3Velocity.y = 0;
            }
            else
            {
                v3Velocity.y = Physics.gravity.y;
            }
        }
        wasGrounded = isGrounded;
        m_characterController.Move(v3Velocity);
    }
    #endregion

    #region Collision Event

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        isSlope = hit.gameObject.tag == "slope";
    }

    #endregion
}