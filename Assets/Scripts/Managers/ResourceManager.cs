using System.Collections.Generic;
using System.IO;
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
    private Dictionary<string, Sprite> _sprites;
    private Dictionary<string, AudioClip> _audioClips;

    public void Flush()
    {
        if(_prefabs != null) _prefabs.Clear();
        if(_scriptableObjects != null) _scriptableObjects.Clear();
    }

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
    public GameObject GetPrefab(string path, int id)
    {
        return GetPrefab(Path.Combine(path, id.ToString("000")));
    }

    public T GetPrefab<T>(string path) where T : Component
    {
        return GetPrefab(path).TryGetComponent(out T component) ? component : null;
    }

    public T GetPrefab<T>(string path, int id) where T : Component
    {
        return GetPrefab<T>(Path.Combine(path, id.ToString("000")));
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

    public T GetSO<T>(string path, int id) where T : ScriptableObject
    {
        return GetSO<T>(Path.Combine(path, id.ToString("000")));
    }

    public Sprite GetSprite(string path)
    {
        if (_sprites == null) _sprites = new();

        if (!_sprites.TryGetValue(path, out Sprite sprite))
        {
            sprite = Resources.Load<Sprite>(path);
            if (sprite != null)
            {
                _sprites.Add(path, sprite);
            }
        }

        return sprite;
    }

    public AudioClip GetAudioClip(SFX sfxName)
    {
        string path = Path.Combine(Const.Sounds, sfxName.ToString());

        if (_audioClips == null) _audioClips = new();

        if (!_audioClips.TryGetValue(path, out AudioClip clip))
        {
            clip = Resources.Load<AudioClip>(path);
            if (clip != null)
            {
                _audioClips.Add(path, clip);
            }
        }

        return clip;
    }
}
