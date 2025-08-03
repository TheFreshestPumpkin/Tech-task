using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPanel : MonoBehaviour
{
    [SerializeField] private HelicopterSpawner _helicopterSpawner;
    [SerializeField] private TMP_Text _helicoptersDestroyed;
    [SerializeField] private GameObject _endMissionField;
    [SerializeField] private TMP_Text _endMissionText;
    [SerializeField] private TMP_Text _expCount;
    [SerializeField] private int _expForHeli;

    public TMP_Text HelicoptersDestroyed { get => _helicoptersDestroyed; set => _helicoptersDestroyed = value; }

    public void Start()
    {
        _helicoptersDestroyed.text = "0/4";
    }

    public void RestartMission()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitMission()
    {
        Application.Quit();
    }

    public void EndMission()
    {
        _endMissionField.SetActive(true);
        _helicoptersDestroyed.gameObject.SetActive(false);
        _endMissionText.text = _helicopterSpawner.EnemyDestroyed.ToString().Trim() + "/4 helicopters destroyed";
        _expCount.text = $"{_expForHeli * 4} exp earned";
    }
}
