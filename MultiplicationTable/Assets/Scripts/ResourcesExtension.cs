using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public enum ResourceType {
    AnimalTextures,

}

public class ResourcesExtension {
    private static string MainDirectories = Application.dataPath + "/Resources";
    private static string AndroidMainDirectories = "jar:file://" + Application.dataPath + "!/assets/";
    
    private const string TexturesPath = "Textures";
    private const string AnimalsPath = "Animals";

    public string _subDirectories;

    private List<string> _itemPathes;
    private List<Texture2D> _textures;
    private Color32[] _colors;

    public ResourcesExtension(ResourceType type) {
        if (type == ResourceType.AnimalTextures) {
            _subDirectories = Path.Combine(TexturesPath, AnimalsPath);
        }

        GetItemPathes();
    }

    public string GetRandomItemPathes() {
        if (_itemPathes.Count == 0)
            throw new ArgumentNullException($"ItemPathes is empty");

        int randomIndex = UnityEngine.Random.Range(0, _itemPathes.Count);
        return _itemPathes[randomIndex];
    }

    private void GetItemPathes() {
        _itemPathes = new List<string>();
        string fillPath;

        try {
            if (SystemInfo.deviceType == DeviceType.Handheld) {
                fillPath = _subDirectories;
                _textures = Resources.LoadAll<Texture2D>(fillPath).ToList();
                Logger.Instance.Log($"Textures2D Count: {_textures.Count}");

                foreach (var iTexture in _textures) {
                    string iPath = Path.Combine(Application.streamingAssetsPath, fillPath, iTexture.name + ".png");
                    _itemPathes.Add(iPath);
                }
            }
            else {
                fillPath = Path.Combine(MainDirectories, _subDirectories);
                string[] directories = Directory.GetFiles(fillPath, "*.png", SearchOption.AllDirectories);
                _itemPathes = directories.ToList();
            }
        }
        catch (Exception ex) {
            Logger.Instance.Log($"ResourcesExtension: {ex.Message}");
            throw new ArgumentException($"{ex.Message}");
        }

        Logger.Instance.Log($"FillPath:  {fillPath}");
        Logger.Instance.Log($"ItemPathes Count: {_itemPathes.Count}");
    }

    public Color32[] Test(MonoBehaviour mono) {
        mono.StartCoroutine(nameof(Get));

        Logger.Instance.Log($"IEnumerator Pixels32 Count:  {_colors.Count()}");
        return _colors;
    }

    private IEnumerator Get() {     
        string path = Path.Combine(AndroidMainDirectories, _subDirectories, "Capybara.png");
        Logger.Instance.Log($"IEnumerator Get path:  {path}");

        var loadingRequest = UnityWebRequest.Get(path);
        yield return loadingRequest.SendWebRequest();

        if (!loadingRequest.isDone) {
            byte[] bytes = loadingRequest.downloadHandler.data;

            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(bytes);

            _colors = texture.GetPixels32();
        }
    }
    //public static Object Load(string resourceName, System.Type systemTypeInstance) {
    //    string fillPath = Path.Combine(MainDirectories, _subDirectories);
    //    string[] directories = Directory.GetDirectories(fillPath, "*", SearchOption.AllDirectories);

    //    foreach (var item in directories) {
    //        string itemPath = item.Substring(MainDirectories.Length + 1);
    //        Object result = Resources.Load(itemPath + "\\" + resourceName, systemTypeInstance);
    //        if (result != null)
    //            return result;
    //    }
    //    return null;
    //}
}
