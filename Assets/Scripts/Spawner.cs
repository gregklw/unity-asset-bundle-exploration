using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefabToSpawn;
    [SerializeField] private string _guid;

    private AssetBundleDownloader _database;

    private void Awake()
    {
        _database = FindObjectOfType<AssetBundleDownloader>();
    }

    public void InstantiatePrefab(AssetBundle bundle)
    {
        GameObject instance =Instantiate(_database.LoadGameObject(_guid, bundle), transform);
        instance.transform.localPosition = Vector3.zero;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_prefabToSpawn != null)
        {
            if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(_prefabToSpawn, out var guid, out long file))
            {
                _guid = guid;
                _prefabToSpawn = null;
            }
        }
    }


#endif
}
