using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class LaserBeamObjectPool : MonoBehaviour
{
    [SerializeField]
    private LaserBeam _laserBeamPrefab;

    private IObjectPool<LaserBeam> _objectPool;

    public IObjectPool<LaserBeam> ObjectPool {
        get {
            if (_objectPool == null)
                _objectPool = new ObjectPool<LaserBeam>(InstantiateLaserBeam, OnGetLaserBeam, OnReleased, OnDestroyObjectPool, defaultCapacity: 10, maxSize: 10);
            return _objectPool;
        }
    }

    public void ReleaseLaserBeam(LaserBeam obj) {
        ObjectPool.Release(obj);
    }

    private LaserBeam InstantiateLaserBeam() {
        LaserBeam obj = Instantiate(_laserBeamPrefab);
        return obj;
    }

    private void OnGetLaserBeam(LaserBeam obj) {
        obj.gameObject.SetActive(true);
    }

    private void OnReleased(LaserBeam obj) {
        obj.gameObject.SetActive(false);
    }

    private void OnDestroyObjectPool(LaserBeam obj) {
        Destroy(obj.gameObject);
    }
}
