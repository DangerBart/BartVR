using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Assets.Scripts.NPCs
{
    public enum StateType
    {
        Zero, 	//freeze
        One, 	//ragdollstate
        Two		//animState
    }
    /// <summary>
    /// Handles the ragdoll of a npc
    /// </summary>
    public class Ragdoll02 : MonoBehaviour
    {
        public StateType stateType;

		public bool blend;     //Activate Skin > Anim in Slerp (smooth transition)
        public bool IsGettingUp;

        private List<GameObject> _animBones;
        private List<GameObject> _skinBones; //This hierarchy also contains the Colliders the player can hit in prefab 

        // Use this for initialization
        void Start()
        {
            _animBones = new List<GameObject>();
            _skinBones = new List<GameObject>();
            Transform[] Bones = gameObject.GetComponentsInChildren<Transform>();
            SetBones(Bones);


            string[] legs = { "LThighTwist", "RThighTwist" };
        }
        public void SetBones(Transform[] Bones)
        {
            foreach (Transform child in Bones)
            {
                if (child.tag == "AnimBones")
                    _animBones.Add(child.gameObject);
                else if (child.tag == "SkinBones")
                    _skinBones.Add(child.gameObject);

                stateType = StateType.Two;
            }
        }

        public void SetKinimatic(List<GameObject> gameobject)
        {
            foreach (GameObject rb in gameobject)
            {
                Rigidbody rbody = rb.GetComponent<Rigidbody>();

                if (rbody != null)
                {
                    rbody.isKinematic = false;
                }
            }
        }

        IEnumerator WaitBlend()
        {
            yield return new WaitForSeconds(0.8f);
            blend = false;
            stateType = StateType.Two;
        }

        public IEnumerator GetUp()
        {
            stateType = StateType.Zero;

            blend = true;
            bool faceUp = Vector3.Dot(-_skinBones[0].GetComponent<Transform>().right, Vector3.up) > 0f;

            yield return new WaitForSeconds(6);

			blend = false;
            stateType = StateType.Two;
            IsGettingUp = false;
        }

        void LateUpdate()
        {
			if (stateType == StateType.Two)
            {
                _skinBones[0].transform.localPosition = _animBones[0].transform.transform.localPosition;

                for (int i = 0; i < _skinBones.Count; i++)
                {
                    _skinBones[i].transform.localRotation = _animBones[i].transform.localRotation;
                }
            }
			if (blend)
            {
                _skinBones[0].transform.localPosition = Vector3.Lerp(_skinBones[0].transform.localPosition, _animBones[0].transform.localPosition, 0.1f);
                for (int i = 0; i < _skinBones.Count; i++)
                {
                    _skinBones[i].transform.localRotation = Quaternion.Slerp(_skinBones[i].transform.localRotation, _animBones[i].transform.localRotation, 0.1f);
                }
            }
        }
    }
}