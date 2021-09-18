using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace ObjectTemplate{
    [System.Serializable]
    public class GameObjectPool {
        public List<GameObject> Objects => objects;
        [SerializeField]
        private int cashCount;
        [SerializeField]
        private Transform parent;
        [SerializeField]
        private GameObject prefab;
        public GameObject Data => objects[0];
        public GameObject Current => objects[listIndex];
        protected List<GameObject> objects = new List<GameObject>();
        protected int listIndex = 0;

        public GameObjectPool(int _cashCount, Transform _parent, GameObject _prefab) {
            cashCount = _cashCount;
            parent = _parent;
            prefab = _prefab;
        }

        public void Create() {
            for (int i = 0; i < cashCount; i++)
            {
                GameObject obj = GameObject.Instantiate(prefab, parent);
                objects.Add(obj);
                obj.SetActive(false);
            }
        }

        public IEnumerator DelayCreate(float delayTime) {
            for (int i = 0; i < cashCount; i++)
            {
                GameObject obj = GameObject.Instantiate(prefab, parent);
                objects.Add(obj);
                obj.SetActive(false);

                yield return new WaitForSeconds(delayTime);
            }

            yield return null;
        }

        public void AllDisable()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].SetActive(false);
            }
        }

        public void Clear(){
            objects.Clear();
            objects = null;
        }

        public GameObject Pop(){
            if (listIndex == cashCount) listIndex = 0;
            return objects[listIndex++];
        }
    }
}