using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTrackingImageDetect : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager _imageManager;
    private Dictionary<TrackableId, GameObject> _models = new Dictionary<TrackableId, GameObject>();

    void OnEnable() => _imageManager.trackablesChanged.AddListener(OnChanged);

    void OnDisable() => _imageManager.trackablesChanged.RemoveListener(OnChanged);

    void OnChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            Debug.Log($"add {newImage.referenceImage.name}");
            var prefab = Resources.Load<GameObject>($"Models/{newImage.referenceImage.name}");
            var go = Instantiate(prefab);
            _models.Add(newImage.trackableId, go);
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            Debug.Log($"{updatedImage.referenceImage.name} : {updatedImage.trackingState.ToString()}");
            _models[updatedImage.trackableId].SetActive(updatedImage.trackingState != TrackingState.None);
        }
    }
}
