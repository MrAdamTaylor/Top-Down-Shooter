using Infrastructure.Services.AssertService.ExtendetAssertService;
using UnityEngine;

internal class EnemyFactory : IEnemyFactory 
{
    private AssertServiceObj<GameObject> _enemySkinsAssert;
    
    public EnemyFactory(AssertServiceObj<GameObject> skinAsser)
    {
        _enemySkinsAssert = skinAsser;
    }

    public void Create()
    {
        Debug.Log("Created Enemy with Random Skin");
    }
}