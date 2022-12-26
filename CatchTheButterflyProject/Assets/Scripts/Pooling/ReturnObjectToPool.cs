using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnObjectToPool : MonoBehaviour
{
    [SerializeField] private ObstaclePool _returnPool;
    [SerializeField] private string _tagToReturn;

    #region MonoBehaviour Methods
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tagToReturn))
        {
            _returnPool.Pool.Release(other.gameObject);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
    #endregion
}
