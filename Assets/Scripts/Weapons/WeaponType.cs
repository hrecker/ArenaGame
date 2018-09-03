public class WeaponType
{
    public static WeaponBase GetWeapon(WeaponMods mods, string weaponName, bool isPlayerControlled)
    {
        switch(weaponName)
        {
            case "SimpleGun":
                return CreateSimpleGun(mods, isPlayerControlled);
            case "ChargeGun":
                return CreateChargeGun(mods, isPlayerControlled);
            case "GrenadeLauncher":
                return CreateGrenadeLauncher(mods, isPlayerControlled);
            // default to simple gun
            default:
                return CreateSimpleGun(mods, isPlayerControlled);
        }
    }

    private static WeaponBase CreateSimpleGun(WeaponMods mods, bool isPlayerControlled)
    {
        return new SimpleGun(mods, isPlayerControlled);
    }

    private static WeaponBase CreateChargeGun(WeaponMods mods, bool isPlayerControlled)
    {
        return new ChargeGun(mods, isPlayerControlled);
    }

    private static WeaponBase CreateGrenadeLauncher(WeaponMods mods, bool isPlayerControlled)
    {
        return new GrenadeLauncher(mods, isPlayerControlled);
    }
}
