using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTrackingImageDetect : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager _imageManager;

    void OnEnable() => _imageManager.trackablesChanged.AddListener(OnChanged);

    void OnDisable() => _imageManager.trackablesChanged.RemoveListener(OnChanged);

    void OnChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            // Handle added event
            LogCustom.Instance.Log($"add {newImage.name}");
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            // Handle updated event
        }

        foreach (var removed in eventArgs.removed)
        {
            // Handle removed event
            TrackableId removedImageTrackableId = removed.Key;
            ARTrackedImage removedImage = removed.Value;
            LogCustom.Instance.Log($"remove {removedImage.name}");
        }
    }
}
