using System;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressablesExample : MonoBehaviour {
    [SerializeField] private string _path = "Assets/Textures/12X12/Capybara.png";
    [SerializeField] private AssetReferenceTexture2D _texture;

    public void Start() {
        //AsyncOperationHandle<GameObject> asyncOperationHandleGO = Addressables.LoadAssetAsync<GameObject>(_path);
        //asyncOperationHandleGO.Completed += CompletedLoadGameObject;

        AsyncOperationHandle<Texture2D> asyncOperationHandleTexture2D = Addressables.LoadAssetAsync<Texture2D>(_path);
        asyncOperationHandleTexture2D.Completed += CompletedLoadTexture2D;
    }

    private void CompletedLoadGameObject(AsyncOperationHandle<GameObject> obj) {
        if(obj.Status != AsyncOperationStatus.Succeeded) {
            Logger.Instance.Log($"AddressablesExample: ResourcesLoad is falled: {_path}");
            Debug.Log($"Не удалось загрузить ресурс: {_path}!");
            return;
        }

        GameObject prefab = obj.Result;
        Instantiate(prefab);
    }


    private void CompletedLoadTexture2D(AsyncOperationHandle<Texture2D> obj) {
        if (obj.Status != AsyncOperationStatus.Succeeded) {
            Logger.Instance.Log($"AddressablesExample: ResourcesLoad is falled: {_texture.Asset.name}");
            Debug.Log($"Не удалось загрузить ресурс: {_texture.Asset.name}!");
            return;
        }

        Texture2D texture = obj.Result;
        byte[] bytes = File.ReadAllBytes(obj.Result.name);
        Texture2D newTexture = new Texture2D(2, 2);
        newTexture.LoadImage(bytes);

        //Texture2D newTexture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
        //newTexture.SetPixels32(texture.GetPixels32());

        var pixels = newTexture.GetPixels32();
    }
}
