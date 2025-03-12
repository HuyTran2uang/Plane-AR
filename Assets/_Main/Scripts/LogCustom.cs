using TMPro;
using UnityEngine;

public class LogCustom : MonoBehaviour
{
    public static LogCustom Instance { get; private set; }
    [SerializeField] private TMP_Text _logText;

    private void Awake()
    {
        Instance = this;
    }

    public void Log(string message)
    {
        _logText.text += $"{message}\n";
    }
}
