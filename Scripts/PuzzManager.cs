using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUZ.Manager
{
    public class PuzzManager : MonoBehaviour
    {
        public static PuzzManager Instance { get; private set; }

        public PuzzManagerConfig configFile;

        GameObject babyPuzPrefab;
        float minRebirthTime;
        float maxRebirthTime;
        [SerializeField]
        private float yPosition;

        private void LoadConfigFile()
        {
            babyPuzPrefab = configFile.BabyPuzPrefab;
            minRebirthTime = configFile.MinRebirthTime;
            maxRebirthTime = configFile.MaxRebirthTime;
        }

        public void CapturePuz(GameObject puz)
        {
            StartCoroutine(RebirthPUZ(puz.transform.position));
        }

        IEnumerator RebirthPUZ(Vector3 collectionPoint) 
        {
            yield return new WaitForSeconds(Random.Range(minRebirthTime, maxRebirthTime));
            Vector3 spawnPosition = new Vector3(collectionPoint.x, yPosition, collectionPoint.z);
            Instantiate(babyPuzPrefab, spawnPosition, Quaternion.identity);
        }
    }
}