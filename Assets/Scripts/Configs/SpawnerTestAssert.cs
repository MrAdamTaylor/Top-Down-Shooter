using UnityEngine;

[CreateAssetMenu(fileName = "Spawner", menuName = "Spawner/Test")]
public class SpawnerTestAssert : SpawnerConfigs
{
    public string PathToParticle;
    public string PathToTrail;
    public string PathToLine;
    public string PathToObject;

    public GameObject ObjValue;
    public ParticleSystem ParticleValue;
    public LineRenderer LineRenderer;
    public TrailRenderer TrailRenderer;
}