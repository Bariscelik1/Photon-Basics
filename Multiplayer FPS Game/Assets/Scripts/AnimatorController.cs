using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace myNamespace {
    public class AnimatorController : MonoBehaviourPun
    {
        [SerializeField]
        private float directionDampTime = 0.25f;

        private Animator animator;
        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            if (!animator)
            {
                Debug.LogError("PlayerAnimatorManager is Missing Animator Component", this);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
            {
                return;
            }

            if (!animator)
            {
                return;
            }
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            if (v < 0)
            {
                v = 0;
            }
        
            animator.SetFloat("Speed", h * h + v * v);

            animator.SetFloat("Direction", h, directionDampTime, Time.deltaTime);
        }

    }
}

