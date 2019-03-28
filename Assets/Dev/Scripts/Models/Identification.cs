using UnityEngine;

// Enum's
public enum Roles {
    None,
    Civilian,
    Officer,
    Suspect
}

public enum Genders { 
    None,
    Male,
    Female
}

public enum Colors {
    None,
    White,
    Black,
    Grey,
    Yellow,
    Green,
    Pink,
    Purple,
    Blue,
    Brown,
    Red
}

[System.Serializable]
public class Identification : MonoBehaviour {

    // All the information needed to identify someone.
    public Roles role;
    public Genders gender;
    public Colors topPiece;
    public Colors bottomPiece;

}
