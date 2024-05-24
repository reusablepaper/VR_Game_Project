using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private static ResourceManager _instance;
    public static ResourceManager Instance
    {
        get
        {
            if(!_instance)
            {
                _instance = new GameObject("Resource Manager").AddComponent<ResourceManager>();
            }

            return _instance;
        }
    }

    private Dictionary<string, GameObject> _prefabs;
    private Dictionary<string, ScriptableObject> _scriptableObjects;

    public GameObject GetPrefab(string path)
    {
        if (_prefabs == null) _prefabs = new();

        if (!_prefabs.TryGetValue(path, out GameObject prefab))
        {
            prefab = Resources.Load<GameObject>(path);
            if (prefab != null)
            {
                _prefabs.Add(path, prefab);
            }
        }

        return prefab;
    }

    public T GetPrefab<T>(string path) where T : Component
    {
        return GetPrefab(path).TryGetComponent(out T component) ? component : null;
    }

    public T GetSO<T>(string path) where T : ScriptableObject
    {
        if (_scriptableObjects == null) _scriptableObjects = new();

        T T_so = null;

        if (_scriptableObjects.TryGetValue(path, out ScriptableObject so))
        {
            T_so = so as T;
        }

        if (!T_so)
        {
            if (T_so = Resources.Load<T>(path))
            {
                if (so)
                {
                    _scriptableObjects.Remove(path);
                }

                _scriptableObjects.Add(path, T_so);
            }
        }

        return T_so;
    }
}
