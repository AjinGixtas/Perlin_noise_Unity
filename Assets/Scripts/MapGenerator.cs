using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
    /*
     * In popular implementation of Perlin noise, there's also a parameter called "seed".
     * However, the way System.Random.Next() work and how seed affect the outcome is quite
     * confusing so I've decided to not implement it into my Perlin noise generator.
     *
     * Perlin noise research paper: 
     * -> https://mrl.cs.nyu.edu/~perlin/paper445.pdf
     * Wikipedia article on Perlin noise: 
     * -> https://en.wikipedia.org/wiki/Perlin_noise
     * Code shamelessly stole from Sebastian Lague: 
     * -> https://github.com/SebLague/Procedural-Landmass-Generation
     * Perlin noise series by Sebastian Lague: 
     * -> https://www.youtube.com/watch?v=wbpMiKiSKm8&list=PLFt_AvWsXl0eBW2EiBtl_sxmDtSgZBxB3
     * Explaination of Perlin noise and an implmentation of it in Javascript by Daniel Shiffman 
     * from the Youtube channel "The Coding Train": 
     * -> https://www.youtube.com/watch?v=IKB1hWWedMk
     */
    private readonly GenerateNoiseMap noiseMapGenerator = new();
    public int width, height;
    public float scale;
    public bool autoUpdate;
    public bool debugMode;
    public int octave;
    public float persistance, lacunarity;
    public void GenerateMap() {
        // While some of these variables in the guard clausecan go under or
        // equal to 0 and the code still work fine, this help with
        // consistancies and avoid any potentional confusion
        if (width <= 0 || height <= 0 || scale <= 0 || octave <= 0) return;

        float[,] noiseMap = noiseMapGenerator.GenerateNoise(width, height, scale, octave, persistance, lacunarity);

        RenderTerrain terrainRenderer = FindFirstObjectByType<RenderTerrain>();
        terrainRenderer.DrawNoiseMap(noiseMap);
    }
}
