using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupSpawner : MonoBehaviour
{
    private Spawner[] _spawners;

    public void SpawnAllChildren(AssetBundle bundle)
    {
        _spawners = GetComponentsInChildren<Spawner>();

        foreach (var spawner in _spawners)
        {
            spawner.InstantiatePrefab(bundle);
        }
    }
}
