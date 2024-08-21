using UnityEngine;

namespace Mechanics.Spawners
{
    public class EnemyCreater : MonoBehaviour
    {
        // Start is called before the first frame update
        void Awake()
        {
            ServiceLocator.Instance.BindData(typeof(EnemyCreater),this);
        }


        public void CreateAndHide(GameObject createdObject, Transform getRandomPoint, Transform parent, EnemyPool pool)
        {
            GameObject obj = Instantiate(createdObject, getRandomPoint);
            //obj.SetActive(false);
            obj.transform.parent = parent;
            pool.AddToPool(createdObject);
        }
    }
}
