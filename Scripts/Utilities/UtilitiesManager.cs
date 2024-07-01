using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PUZ.Sound;

namespace PUZ.Utilities
{
    public class UtilitiesManager : MonoBehaviour
    {
        public static UtilitiesManager Instance { get; private set; }

        protected TerrainSurfaceDetector terrainSurfaceDetector;

        [SerializeField] SoundManager soundManager;

        private void Awake() 
        { 
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this; 
            } 

            terrainSurfaceDetector = new TerrainSurfaceDetector();
        }

        public int GetActiveTerrainTextureIdx(Vector3 position) => terrainSurfaceDetector.GetActiveTerrainTextureIdx(position);

        public void PlayClip(string nam, AudioSource source, bool loop = false) => soundManager.Play(nam, source, loop);

        public void StopClip(string nam, AudioSource source) => soundManager.Stop(nam, source);
    }
}