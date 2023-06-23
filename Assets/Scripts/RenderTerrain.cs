using UnityEngine;

/*
 * Altough this script is generate texture, later on I might make it actually generate terrain
 * like it suppose to. 
 * Should I rename the script to accurately reflect the role of the script? Yes! 
 * But am I actually gonna do it? Nah.
 * (Don't do this in your project though. It's terrible practice.)
 */
public class RenderTerrain : MonoBehaviour {
    [SerializeField] Renderer textureRender;
    public void DrawNoiseMap(float[,] noiseMap) {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Texture2D texture = new(width, height);

        Color[] colourMap = new Color[width * height];

        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                // Lerp documentaion: https://docs.unity3d.com/ScriptReference/Color.Lerp.html
                colourMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
            }
        }
        texture.SetPixels(colourMap);
        texture.Apply(); // This force Unity to update the texture immediately

        textureRender.sharedMaterial.mainTexture = texture;
        // Change the width and height to fit the texture
        textureRender.transform.localScale = new Vector3(width, 1, height);
    }
}