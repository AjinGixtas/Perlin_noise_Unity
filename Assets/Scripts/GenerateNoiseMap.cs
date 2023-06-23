using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNoiseMap
{
    public float[,] GenerateNoise(int width, int height, float scale, int octave, float persistance, float lacunarity) {
        float[,] result = new float[width, height];
        // This array hold the coordinate on where to sample Perlin noise value for each layer
        Vector2[] octaveOffsets = new Vector2[octave];

        // Generate offset coordinate for each layer
        for (int i = 0; i < octave; i++) {
            // While the number itself don't matter so much, this value range
            // should make the value fit inside 16 bits
            // The code is uglier than it probaly needs to be though
            short offsetX = (short)Random.Range(-32768, 32767);
            short offsetY = (short)Random.Range(-32768, 32767);
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        // Value for normalization purpose
        float maxNoiseValue = float.NegativeInfinity;
        float minNoiseValue = float.PositiveInfinity;

        // Loop through all tile
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {

                // Intialize variable for that tile
                float amplitude = 1, frequency = 1;
                float noiseResult = 0;

                // Loop through all layer
                for (int i = 0; i < octave; i++) {

                    // Calculate coordinate to sample the noise
                    /* 
                     * Frequency will make the sample point further/closer exponentially
                     * Sample point closer will make the noise map generally less noisy
                     * and smoother while further point will make noise less smooth
                     * 
                     * We increment the coordinate by octaveOffsets to make sample point 
                     * between 2 layer further away and therefore make it less likely to
                     * look the same.
                     */
                    float xPos = x / scale * frequency + octaveOffsets[i].x;
                    float yPos = y / scale * frequency + octaveOffsets[i].y;

                    // Multiply by 2 and minus 1 make the value range from (0,1) to (-1,1)
                    float noiseValue = Mathf.PerlinNoise(xPos, yPos) * 2 - 1;



                    /* Amplitude decide how high and low the noise can be
                     * 
                     * We cound multiply the noiseResult by amplitude instead of this, but it's
                     * easier to read to it's here to stay >:)
                     */
                    noiseResult += noiseValue * amplitude;

                    // Increase amplitude and frequency exponentially, this make amplitude
                    // and frequency have bigger effect on subsequent layer
                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                // This if-else block is for normalization latter on
                if (maxNoiseValue < noiseResult) {
                    maxNoiseValue = noiseResult;
                } else if (minNoiseValue > noiseResult) {
                    minNoiseValue = noiseResult;
                }
                result[x, y] = noiseResult;
            }
        }

        // Normalize all value
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                result[x, y] = Mathf.InverseLerp(minNoiseValue, maxNoiseValue, result[x, y]);
            }
        }

        return result;
    }
    // Debugging purpose
    public void Print2DArray(float[,] arr) {
        string result = "";
        for (int i = 0; i < arr.GetLength(0); i++) {
            for (int j = 0; j < arr.GetLength(1); j++) {
                result += $" {arr[i, j]}";
            }
            result += "\n";
        }
        Debug.Log(result);
    }
}
