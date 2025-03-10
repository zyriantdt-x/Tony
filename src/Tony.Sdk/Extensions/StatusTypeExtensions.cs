using Tony.Sdk.Enums;

namespace Tony.Sdk.Extensions;
public static class StatusTypeExtensions {
    private static readonly Dictionary<StatusType, string> status_codes = new()
    {
        { StatusType.Move, "mv" },
        { StatusType.Sit, "sit" },
        { StatusType.Lay, "lay" },
        { StatusType.FlatControl, "flatctrl" },
        { StatusType.Dance, "dance" },
        { StatusType.Swim, "swim" },
        { StatusType.CarryItem, "cri" },
        { StatusType.CarryDrink, "carryd" },
        { StatusType.CarryFood, "carryf" },
        { StatusType.UseItem, "usei" },
        { StatusType.UseFood, "eat" },
        { StatusType.UseDrink, "drink" },
        { StatusType.Wave, "wave" },
        { StatusType.Gesture, "gest" },
        { StatusType.Talk, "talk" },
        { StatusType.AvatarSleep, "Sleep" },
        { StatusType.Trade, "trd" },
        { StatusType.Sign, "sign" },
        { StatusType.Dead, "ded" },
        { StatusType.Jump, "jmp" },
        { StatusType.PetSleep, "slp" },
        { StatusType.Eat, "eat" },
        { StatusType.Smile, "sml" },
        { StatusType.Play, "pla" }
    };

    public static string GetStatusCode( this StatusType status ) => StatusCodes[ status ];
}