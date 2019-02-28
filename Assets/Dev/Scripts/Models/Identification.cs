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

public class Identification : MonoBehaviour {

    // All the information needed to identify someone.
    public Roles Role { get; set; }
    public Genders Gender { get; set; }
    public Colors TopPiece { get; set; }
    public Colors BottomPiece { get; set; }
}
