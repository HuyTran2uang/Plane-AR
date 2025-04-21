using DG.Tweening;
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
    [Header("Info")]
    public TMP_Text f_dayTXT;
    public TMP_Text f_canTXT;
    public TMP_Text f_nangTXT;
    public TMP_Text pTXT;
    public TMP_Text v_duoiTXT;
    public TMP_Text v_trenTXT;
    //
    [Header("Button")]
    public Button _arrowBtn;
    public Button _infoBtn;
    public Button _fBtn;
    public Button _vBtn;
    public Button _tBtn;
    [Header("Component")]
    public GameObject _info;
    public List<GameObject> _listF;
    public List<GameObject> _listV;
    public List<GameObject> _listT;
    //arrow
    public RectTransform _listBtns;
    public Image _iconArrow;
    private bool isShowingListBtns = true;
    private bool isSwitching = false;

    private void Awake()
    {
        _arrowBtn.onClick.AddListener(() =>
        {
            DisplayListBtns();
        });
        _infoBtn.onClick.AddListener(() =>
        {
            _info.SetActive(!_info.activeSelf);
        });
        _fBtn.onClick.AddListener(() =>
        {
            _listF.ForEach(i => i.SetActive(!i.activeSelf));
        });
        _vBtn.onClick.AddListener(() =>
        {
            _listV.ForEach(i => i.SetActive(!i.activeSelf));
        });
        _tBtn.onClick.AddListener(() =>
        {
            _listT.ForEach(i => i.SetActive(!i.activeSelf));
        });
    }

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
        f_dayTXT.SetText($"F đẩy: {stats.F_day} N");
        f_canTXT.SetText($"F cản: {stats.F_can} N");
        f_nangTXT.SetText($"F nâng: {stats.F_nang} N");
        pTXT.SetText($"P: {stats.P} N");
        v_duoiTXT.SetText($"V dưới: {stats.V_duoi} km/h");
        v_trenTXT.SetText($"V trên: {stats.V_tren} km/h");
    }

    private void DisplayListBtns()
    {
        if (isSwitching) return;
        isSwitching = true;
        //420
        if (isShowingListBtns)
        {
            _iconArrow.transform.localScale = new Vector3(1, -1, 1);
            _listBtns.DOSizeDelta(new Vector2(100, 0), .5f).OnComplete(() =>
            {
                isSwitching = false;
                isShowingListBtns = false;
            });
        }
        else
        {
            _iconArrow.transform.localScale = new Vector3(1, 1, 1);
            _listBtns.DOSizeDelta(new Vector2(100, 420), .5f).OnComplete(() =>
            {
                isSwitching = false;
                isShowingListBtns = true;
            });
        }
    }
}
