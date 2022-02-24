public class Constants
{
    //Power up types
    public const string Water = "Water";
    public const string Fire = "Fire";
    public const string Electricity = "Electricity";

    //Pickup Types
    public const int PickUpWater = 1;
    public const int PickUpFire = 2;
    public const int PickUpElect = 3;
    public const int PickUpHealth = 4;

    public static readonly int[] AllPickupsTypes = new int[4]
    {
        PickUpWater,
        PickUpFire,
        PickUpElect,
        PickUpHealth
    };
}
