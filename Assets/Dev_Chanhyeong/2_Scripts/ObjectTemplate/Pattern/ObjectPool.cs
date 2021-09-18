using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace ObjectTemplate{
    [System.Serializable]
    public class ObjectPool<T> where T : MonoBehaviour {
        // FIXME : 객체 지향을 해침, 추후 삭제 예정
        public List<T> Objects => objects;
        [SerializeField]
        private int cashCount;
        [SerializeField]
        private Transform parent;
        [SerializeField]
        private GameObject prefab;
        // FIXME : 클래스의 의미를 해침, 추후 삭제 예정
        public T Data => objects[0];
        // FIXME : 클래스의 의미를 해침, 추후 삭제 예정
        public T Current => objects[listIndex];
        protected List<T> objects = new List<T>();
        protected int listIndex = 0;

        /// <summary>
        /// 오브젝트 풀 생성자
        /// </summary>
        /// <param name="_cashCount">캐싱할 갯수</param>
        /// <param name="_parent">부모 Transform</param>
        /// <param name="_prefab">Instantiate 할 오브젝트</param>
        public ObjectPool(int _cashCount, Transform _parent, GameObject _prefab){
            cashCount = _cashCount;
            parent = _parent;
            prefab = _prefab;
        }

        public void Create(){
            for (int i = 0 ; i < cashCount; i++)
            {
                GameObject obj = GameObject.Instantiate(prefab, parent);
                objects.Add(obj.GetComponent<T>());
                obj.SetActive(false);
            }
        }
        
        // FIXME : Dispatcher 스크립트를 사용해 Create사용 바람, 추후 삭제 예정
        public IEnumerator DelayCreate(float delayTime){
            for (int i = 0 ; i < cashCount; i++)
            {
                GameObject obj = GameObject.Instantiate(prefab, parent);
                objects.Add(obj.GetComponent<T>());
                obj.SetActive(false);

                yield return new WaitForSeconds(delayTime);
            }

            yield return null;
        }

        public void AllDisable()
        {
            for (int i = 0; i < cashCount; i++)
            {
                objects[i].gameObject.SetActive(false);
            }
        }
        
        /// <summary>
        /// 해당 오브젝트 풀의 리스트를 비우고 초기화시킴
        /// </summary>
        public void Clear(){
            objects.Clear();
            objects = null;
        }

        public T Pop(bool instantActive = false){
            if (listIndex == cashCount) listIndex = 0;
            return objects[listIndex++];
        }
    }
}