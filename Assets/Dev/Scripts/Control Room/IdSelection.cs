using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdSelection : MonoBehaviour {
    // Needs to set in inspector
    public Image genderImageToChange;
    public NamedGenderImage[] GenderOptions;

    [Serializable]
    public struct NamedGenderImage
    {
        public Genders value;
        public Texture2D image;
    }

    [Serializable]
    public struct NamedColorImage
    {
        public Color value;
        public Texture2D image;
    }

    // Use this for initialization
    void Start () {
     

    }

    public void LeftSelection()
    {

    }

    public void RightSelection()
    {

    }
}
