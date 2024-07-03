using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerWithoutBundle : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;

    private void Start()
    {
        var go = Instantiate(_prefab, transform);
        go.transform.localPosition = Vector3.zero;
    }
}
