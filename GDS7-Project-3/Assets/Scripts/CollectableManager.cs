using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GDS7.Group1.Project3.Assets.Scripts.Interactable;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    public class CollectableManager : MonoBehaviour
    {
        public static CollectableManager Instance { get; private set; }


        [HideInInspector] public List<CollectableInteract> collectableInteracts = new List<CollectableInteract>();
        [HideInInspector] public int collectedCount = 0;


        private void Awake()
        {
            collectableInteracts.Clear();
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }


        public void UpdateCollectables()
        {
            collectedCount = collectableInteracts.Count(c => c.collected);
            if (collectedCount == collectableInteracts.Count)
            {
                var screens = GameObject.FindGameObjectWithTag("Screens");
                var achievement = screens.transform.Find("Achievement");
                achievement.gameObject.SetActive(true);
            }
        }
    }
}