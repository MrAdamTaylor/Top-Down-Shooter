using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Baf", menuName = "Baf/PlayerBaf")]
    public class BafConfigs : ScriptableObject
    {
        public float Value;
        public int PerSeconds;
    }
}