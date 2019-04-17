using System;
using UnityEngine;
using UnityEngine.UI;

public class IdSelection : MonoBehaviour {
    // Needs to set in inspector
    public NamedGenderImage[] genderOptions;
    public Image genderImageToChange;
    public NamedColorImage[] topPieceOptions;
    public Image topPieceImageToChange;
    public NamedColorImage[] bottomPieceOptions;
    public Image bottomPieceImageToChange;
   
    // Private variables needed to keep track of selection
    private int selectedGenderIndex;
    private int selectedTopPieceIndex;
    private int selectedBottomPieceIndex;

    [Serializable]
    public struct NamedGenderImage {
        public Genders value;
        public Sprite image;
    }

    [Serializable]
    public struct NamedColorImage {
        public Colors value;
        public Sprite image;
    }

    // Use this for initialization
    void Start () {
        // Set defaults which is none
        selectedGenderIndex = 0;
        selectedTopPieceIndex = 0;
        selectedBottomPieceIndex = 0;

        // Set default images
        genderImageToChange.sprite = genderOptions[selectedGenderIndex].image;
        topPieceImageToChange.sprite = topPieceOptions[selectedTopPieceIndex].image;
        bottomPieceImageToChange.sprite = bottomPieceOptions[selectedBottomPieceIndex].image;
    }

    // ======== Public functions =========
    public void LeftSelectionGender() {
        if (selectedGenderIndex != 0)
            selectedGenderIndex--;

        genderImageToChange.sprite = genderOptions[selectedGenderIndex].image;
    }

    public void RightSelectionGender() {
        if (selectedGenderIndex < genderOptions.Length - 1)
            selectedGenderIndex++;

        genderImageToChange.sprite = genderOptions[selectedGenderIndex].image;
    }

    public void LeftSelectionTopPiece() {
        ChangeColorValueAndDisplayedImage(topPieceImageToChange, topPieceOptions, ref selectedTopPieceIndex, false);
    }

    public void RightSelectionTopPiece() {
        ChangeColorValueAndDisplayedImage(topPieceImageToChange, topPieceOptions, ref selectedTopPieceIndex, true);
    }

    public void LeftSelectionBottomPiece() {
        ChangeColorValueAndDisplayedImage(bottomPieceImageToChange, bottomPieceOptions, ref selectedBottomPieceIndex, false);
    }

    public void RightSelectionBottomPiece() {
        ChangeColorValueAndDisplayedImage(bottomPieceImageToChange, bottomPieceOptions, ref selectedBottomPieceIndex, true);
    }


    public void SendID() {
        Identification id = new Identification {
            gender = genderOptions[selectedGenderIndex].value,
            topPiece = topPieceOptions[selectedTopPieceIndex].value,
            bottomPiece = bottomPieceOptions[selectedBottomPieceIndex].value,
        };

        Officer.SetId(id);
    }

    // ========= Private functions =========
    private void ChangeColorValueAndDisplayedImage(Image imageToChange, NamedColorImage[] Options, ref int index, bool increment) {

        if (increment) {
            if (index < Options.Length - 1)
                index++;
        } else {
            if (index != 0)
                index--;
        }

        imageToChange.sprite = Options[index].image;
    }
}
