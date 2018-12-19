using System;
using UnityEngine;

namespace Assets.Scripts.NPCs
{
    /// <summary>
    /// Handles all animation events of the NPC
    /// </summary>
    class AnimationEvents : MonoBehaviour
    {
        [SerializeField] private AudioClip _footstepAudioClip;

        private AudioSource _audioSource;

        private void Awake()
        {
        }

        public void OnFootstep()
        {
        }
    }
}