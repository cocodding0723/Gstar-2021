using System.Collections.Generic;
using UnityEngine; 

[System.Serializable]
public class CashObjects<T> where T : Component {
    public int cashCount;
    public Transform parent;
    public GameObject prefab;
    protected List<T> objects = new List<T>();
    protected int listIndex = 0;

    public void CreateObject(){
        for (int i = 0 ; i < cashCount; i++)
        {
            GameObject obj = GameObject.Instantiate(prefab, parent);
            objects.Add(obj.GetComponent<T>());
            obj.SetActive(false);
        }
    }

    public void Pop(ref T obj){
        if (listIndex == cashCount) listIndex = 0;
        obj = objects[listIndex++];
    }

    public T Pop(){
        if (listIndex == cashCount) listIndex = 0;
        return objects[listIndex++];
    }

    public void CreateDictionary<U>(ref Dictionary<T, U> dictionary) 
    {
        for (int i = 0; i < cashCount; i++){
            U objValue = objects[i].GetComponent<U>();
            dictionary.Add(objects[i], objValue);
        }
    }

    public void CreateDictionary<U, O>(ref Dictionary<U, O> dictionary) 
    {
        for (int i = 0; i < cashCount; i++){
            U objKey =  objects[i].GetComponent<U>();
            O objValue =  objects[i].GetComponent<O>();
            dictionary.Add(objKey, objValue);
        }
    }

    public List<T> GetObjectList(){
        return objects;
    }
}