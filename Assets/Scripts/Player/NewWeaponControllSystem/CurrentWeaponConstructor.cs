namespace Player.NewWeaponControllSystem
{
    public class CurrentWeaponConstructor
    {
        private WeaponController _weaponController;
        
        public CurrentWeaponConstructor(WeaponController weaponController)
        {
            _weaponController = weaponController;
        }

        public void SwitchInput(Weapon.Weapon weapon)
        {
            _weaponController.CleanInputSystem();
            _weaponController.ConnectInputToWeapon(weapon);
        }
    }
}