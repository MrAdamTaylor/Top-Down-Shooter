using UnityEngine;

namespace Weapon.StaticData
{
    [CreateAssetMenu(fileName = "ShootgunWeaponConfig", menuName = "Configs/Weapon/Shootgun")]
    public class WeaponStaticShootgun: WeaponStaticData
    {
        private const int MIN_FRACTION = 5;
        private const int MAX_FRACTION = 7;
    
        public int AmountOfFraction;
        public float FovAngle;
        public float Distance;
        public float FadeDuration;
    
        public void OnValidate()
        {
            if (AmountOfFraction % 2 == 0)
            {
                AmountOfFraction -= 1;
            }

            if (AmountOfFraction < MIN_FRACTION)
            {
                AmountOfFraction = MIN_FRACTION;
            }

            if (AmountOfFraction > MAX_FRACTION)
            {
                AmountOfFraction = MAX_FRACTION;
            }
        }
    }
}