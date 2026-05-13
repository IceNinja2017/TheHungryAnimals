using UnityEngine;
using System.IO;

public class Screenshot : MonoBehaviour
{
    public Camera iconCamera;
    public RenderTexture renderTexture;

    private void Start()
    {
        Capture();
    }

    public void Capture()
    {
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = renderTexture;

        iconCamera.Render();

        Texture2D image = new Texture2D(256, 256, TextureFormat.RGB24, false);
        image.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
        image.Apply();

        RenderTexture.active = currentRT;

        byte[] bytes = image.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/icon.png", bytes);

        Debug.Log("Icon saved!");
        Debug.Log("Path: " + Application.dataPath + "/icon.png");
    }

}
