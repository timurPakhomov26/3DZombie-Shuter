using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Printing : MonoBehaviour
{
    [SerializeField] private Camera cam;

    void Start()
    {
        StartCoroutine(Screenshot());
    }

    IEnumerator Screenshot()
    {
        yield return new WaitForEndOfFrame();
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();

        byte[] bytes = texture.EncodeToPNG();
        //var folder = Directory.CreateDirectory(Application.dataPath + "Мандалы"); // returns a DirectoryInfo object
        string pathName = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
        File.WriteAllBytes(Application.dataPath + pathName, bytes);

        Destroy(texture);
    }
}
