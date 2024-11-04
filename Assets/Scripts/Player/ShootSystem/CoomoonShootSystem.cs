using Infrastructure.ServiceLocator;
using Infrastructure.Services.AbstractFactory;
using UnityEngine;
using Weapon;
using Weapon.StaticData;

namespace Player.ShootSystem
{
    public class CoomoonShootSystem : MonoBehaviour, IShootSystem, IPlayerSystem
    {
        protected Transform _directionObject;
        protected LayerMask _layerMask;

        public virtual void Construct(WeaponStaticData staticData, WeaponEffectsConteiner conteiner)
        {
        }

        public virtual void Shoot()
        {
        
        }

        protected Vector3 GetDirection()
        {
            Vector3 direction = _directionObject.transform.forward;
            return direction;
        }

        public virtual void UpdateValues(WeaponCharacteristics characteristics)
        {
        
        }

        public void AddSelfBlockList()
        {
            Player player = (Player)ServiceLocator.Instance.GetData(typeof(Player));
            player.AddBlockList((MonoBehaviour)this);
        }
    }
}
