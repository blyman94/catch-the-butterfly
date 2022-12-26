using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObstaclePool : MonoBehaviour
{
    [SerializeField] private GameObject _obstaclePrefab;
    [SerializeField] private Transform _spawnTransform;
    public ObjectPool<GameObject> Pool;

    #region MonoBehaviour Methods
    private void Awake()
    {
        Pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, 
            OnReturnedToPool, OnDestroyPoolObject, true, 10, 10);
    }
    #endregion

    private GameObject CreatePooledItem()
    {
        return Instantiate(_obstaclePrefab);
    }

    private void OnTakeFromPool(GameObject poolObject)
    {
        poolObject.SetActive(true);
        poolObject.transform.parent = null;
    }

    private void OnReturnedToPool(GameObject poolObject)
    {
        poolObject.SetActive(false);
        poolObject.transform.parent = transform;
    }

    private void OnDestroyPoolObject(GameObject poolObject)
    {
        Destroy(poolObject);
    }
}
