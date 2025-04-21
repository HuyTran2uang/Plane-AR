using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Plane : MonoBehaviour
{
    public List<StatSO> listStats = new List<StatSO>();
    public StatSO stat;
    //view
    public TMP_Dropdown dropdown;
    //info
    public TMP_Text f_dayTXT;
    public TMP_Text f_canTXT;
    public TMP_Text f_nangTXT;
    public TMP_Text pTXT;
    public TMP_Text v_duoiTXT;
    public TMP_Text v_trenTXT;

    private void Start()
    {
        listStats = Resources.LoadAll<StatSO>("Stats").ToList();
        stat = listStats[0];

        dropdown.AddOptions(listStats.Select(x => $"{x.h}Km-{x.F}KN").ToList());
        dropdown.value = 0;
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);

        ShowStats(stat);
    }

    private void OnDropdownValueChanged(int index)
    {
        stat = listStats[index];
        ShowStats(stat);
    }

    public void ShowStats(StatSO stats)
    {
        f_dayTXT.SetText($"F đẩy: {stats.F_day}");
        f_canTXT.SetText($"F cản: {stats.F_can}");
        f_nangTXT.SetText($"F nâng: {stats.F_nang}");
        pTXT.SetText($"P: {stats.P}");
        v_duoiTXT.SetText($"V dưới: {stats.V_duoi}");
        v_trenTXT.SetText($"V trên: {stats.V_tren}");
    }
}
