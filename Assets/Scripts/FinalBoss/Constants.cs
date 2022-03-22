public class Constants
{
    //Power up types
    public const string Dagger = "Dagger";
    public const string Water = "Water";
    public const string Fire = "Fire";
    public const string Electricity = "Electric";

    //Pickup Types
    public const int PickUpDagger = 0;
    public const int PickUpWater = 1;
    public const int PickUpFire = 2;
    public const int PickUpElect = 3;
    public const int PickUpHealth = 4;

    //Allies
    public const int Orry = 0;
    public const int Gaehl = 1;
    public const int Embre = 3;

    public static readonly int[] AllPickupsTypes = new int[4]
    {
        PickUpWater,
        PickUpFire,
        PickUpElect,
        PickUpHealth
    };
}
