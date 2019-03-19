using UnityEngine;

// Enum's
public enum Roles
{
    Civilian,
    Officer,
    Suspect
}

public enum Genders
{
    Male,
    Female
}

public enum Colors
{
    White,
    BLack,
    Grey,
    Yellow,
    Green,
    Pink,
    Purple,
    Blue,
    Brown
}

[System.Serializable]
public class Identification : MonoBehaviour {

    // All the information needed to identify someone.
    public Roles? role;
    public Genders? gender;
    public Colors? topPiece;
    public Colors? bottomPiece;
}
