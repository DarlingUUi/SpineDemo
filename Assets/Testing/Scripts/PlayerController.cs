using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity
{
    public enum PlayerState
    {
        aim,
        death,
        hoverboard,
        idle,
        idle_turn,
        jump,
        portal,
        run,
        run_to_idle,
        shoot,
        walk
    }
    public class PlayerController : MonoBehaviour
    {
        #region Public Variable

        public CharacterController m_characterController;
        public PlayerState m_state;
        public SkeletonAnimation m_skeletonAnimation;
        public Renderer m_rdEnemy;

        #endregion

        #region Local Variables

        Dictionary<string, string> m_dicState = new Dictionary<string, string>{
            { "aim", "aim" },
            { "death", "death" },
            { "hoverboard", "hoverboard" },
            { "idle", "idle" },
            { "idle_turn", "idle-turn" },
            { "jump", "jump" },
            { "portal", "portal" },
            { "run", "run" },
            { "run_to_idle", "run-to-idle" },
            { "shoot", "shoot" },
            { "walk", "walk" },
        };
        float fFlipX = 1;
        bool isGrounded, wasGrounded, isSlope;
        Vector3 v3Velocity;

        #endregion

        #region Original Functions

        // Start is called before the first frame update
        void Start()
        {
            m_characterController = GetComponent<CharacterController>();
            m_skeletonAnimation = GetComponent<SkeletonAnimation>();
        }

        // Update is called once per frame
        void Update()
        {
            if (isSlope)
            {
                v3Velocity = new Vector3(0.6f, Physics.gravity.y, 0);
                m_skeletonAnimation.AnimationName = m_dicState[m_state.ToString()];
                m_skeletonAnimation.skeleton.SetColor(Color.red);
                m_rdEnemy.material.SetColor("_Color", Color.red);
            }
            else
            {
                m_rdEnemy.material.SetColor("_Color", Color.white);
                m_skeletonAnimation.skeleton.SetColor(Color.white);
                float _h = Input.GetAxis("Horizontal");
                isGrounded = m_characterController.isGrounded;
                if (Mathf.Abs(_h) > 0 && Mathf.Abs(_h) < 0.5f)
                    m_state = PlayerState.walk;
                else if (Mathf.Abs(_h) >= 0.5f)
                    m_state = PlayerState.run;
                else
                    m_state = PlayerState.idle;
                fFlipX = _h > 0 ? 1 : (_h < 0 ? -1 : fFlipX);
                m_skeletonAnimation.Skeleton.ScaleX = fFlipX;
                v3Velocity.x = _h;
                m_skeletonAnimation.AnimationName = m_dicState[m_state.ToString()];
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
            }
            m_characterController.Move(v3Velocity);
        }
        #endregion

        #region Collision Event

        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            isSlope = hit.gameObject.tag == "slope";
            m_state = PlayerState.hoverboard;
        }

        #endregion
    }
}