using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Satellite : MonoBehaviour
{
    public float rotationSpeed = 10;
    public List<GameObject> _gos = new List<GameObject>();
    public GameObject _prefab;
    public Transform veTinh;
    public Transform parent;
    private float _spacetime = .2f;
    private float _lifeTime = 15f;

    public IEnumerator SpawnPoint(float duration)
    {
        GameObject go = _gos.Where(i => !i.activeSelf).FirstOrDefault();
        if (!go)
        {
            go = Instantiate(_prefab, veTinh.position, Quaternion.identity, parent);
            _gos.Add(go);
        }
        else
        {
            go.transform.position = veTinh.position;
            go.SetActive(true);
        }
        StartCoroutine(HideGo(go, this._lifeTime));
        yield return new WaitForSeconds(duration);
        StartCoroutine(SpawnPoint(duration));
    }

    private IEnumerator HideGo(GameObject go, float duration)
    {
        yield return new WaitForSeconds(duration);
        go.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnPoint(this._spacetime));
    }

    private void OnDisable()
    {
        _gos.ForEach(i => i.SetActive(false));
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.localEulerAngles += Vector3.up * rotationSpeed * Time.deltaTime;
    }

#if UNITY_EDITOR
    public bool isShowGizmos;

    private void OnDrawGizmos()
    {
        if (!isShowGizmos) return;
        Rotate();
    }
#endif
}
