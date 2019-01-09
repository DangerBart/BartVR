using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletDisplay : MonoBehaviour {
    [SerializeField]
    private Material material;

    public static Sprite tabletSprite;

    void Start() {
        material.SetTexture("_MainTex", null);
    }

    public void SetImage(Sprite sprite) {
        material.SetTexture("_MainTex", sprite.texture);  
    }

    Texture GetImage() {
        return material.mainTexture;
    }
}
