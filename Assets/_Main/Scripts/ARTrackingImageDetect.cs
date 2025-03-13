using System.Collections.Generic;
using Unity.XR.CoreUtils.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTrackingImageDetect : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager _imageManager;
    private Dictionary<string, GameObject> _models = new Dictionary<string, GameObject>();

    void OnEnable() => _imageManager.trackablesChanged.AddListener(OnChanged);

    void OnDisable() => _imageManager.trackablesChanged.RemoveListener(OnChanged);
    public ReadOnlyList<ARTrackedImage> updated { get; set; }

    KalmanFilter kfX = new KalmanFilter();
    KalmanFilter kfY = new KalmanFilter();
    KalmanFilter kfZ = new KalmanFilter();

    void OnChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            Debug.Log($"add {newImage.referenceImage.name}");
            var prefab = Resources.Load<GameObject>($"Models/{newImage.referenceImage.name}");
            var go = Instantiate(prefab);
            _models.Add(newImage.referenceImage.name, go);
        }
        updated = eventArgs.updated;
    }

    private void FixedUpdate()
    {
        if (updated == null || updated.Count == 0) return;
        foreach (var updatedImage in updated)
        {
            Debug.Log($"{updatedImage.referenceImage.name} - State: {updatedImage.trackingState}");

            if (_models.TryGetValue(updatedImage.referenceImage.name, out var model))
            {
                bool isTracking = updatedImage.trackingState == TrackingState.Tracking;
                model.SetActive(isTracking);

                if (isTracking)
                {
                    // Làm mượt vị trí với Kalman Filter
                    Vector3 smoothPosition = new Vector3(
                        kfX.Update(updatedImage.transform.position.x),
                        kfY.Update(updatedImage.transform.position.y),
                        kfZ.Update(updatedImage.transform.position.z)
                    );

                    // Làm mượt xoay với Slerp
                    Quaternion smoothRotation = Quaternion.Slerp(
                        model.transform.rotation,
                        updatedImage.transform.rotation,
                        0.1f
                    );

                    if (model.activeSelf)
                    {
                        smoothPosition = updatedImage.transform.position;
                        smoothRotation = updatedImage.transform.rotation;
                    }

                    model.transform.position = smoothPosition;
                    model.transform.rotation = smoothRotation;
                }
            }
        }
    }
}

public class KalmanFilter
{
    private float q = 0.0001f; // Noise process
    private float r = 0.1f;    // Noise sensor
    private float p = 1, x = 0, k = 0;

    public float Update(float measurement)
    {
        p = p + q;
        k = p / (p + r);
        x = x + k * (measurement - x);
        p = (1 - k) * p;
        return x;
    }
}
