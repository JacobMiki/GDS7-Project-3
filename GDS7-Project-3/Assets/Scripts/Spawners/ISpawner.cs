using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts.Spawners
{
    public interface ISpawner
    {
        void SpawnAndForget();
        GameObject Spawn();
        Vector3 GetPosition();
    }
}
