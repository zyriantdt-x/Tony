using Tony.Sdk.Enums;

namespace Tony.Sdk.Extensions;
public static class EntityStatusTypeExtensions {
    private static readonly Dictionary<EntityStatusType, string> status_codes = new()
    {
        { EntityStatusType.Move, "mv" },
        { EntityStatusType.Sit, "sit" },
        { EntityStatusType.Lay, "lay" },
        { EntityStatusType.FlatControl, "flatctrl" },
        { EntityStatusType.Dance, "dance" },
        { EntityStatusType.Swim, "swim" },
        { EntityStatusType.CarryItem, "cri" },
        { EntityStatusType.CarryDrink, "carryd" },
        { EntityStatusType.CarryFood, "carryf" },
        { EntityStatusType.UseItem, "usei" },
        { EntityStatusType.UseFood, "eat" },
        { EntityStatusType.UseDrink, "drink" },
        { EntityStatusType.Wave, "wave" },
        { EntityStatusType.Gesture, "gest" },
        { EntityStatusType.Talk, "talk" },
        { EntityStatusType.AvatarSleep, "Sleep" },
        { EntityStatusType.Trade, "trd" },
        { EntityStatusType.Sign, "sign" },
        { EntityStatusType.Dead, "ded" },
        { EntityStatusType.Jump, "jmp" },
        { EntityStatusType.PetSleep, "slp" },
        { EntityStatusType.Eat, "eat" },
        { EntityStatusType.Smile, "sml" },
        { EntityStatusType.Play, "pla" }
    };

    public static string GetStatusCode( this EntityStatusType status ) => status_codes[ status ];
}