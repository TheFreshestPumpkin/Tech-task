using UnityEngine;

public class HelicopterSpawner : MonoBehaviour
{
    public static HelicopterSpawner Instance;

    [SerializeField] private GameObject _helicopterPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _hoverTarget;
    [SerializeField] private UIPanel _uiPanel;
    private int _enemyDestroyed = 0;

    public int EnemyDestroyed { get => _enemyDestroyed; set => _enemyDestroyed = value; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SpawnNext();
    }

    public void SpawnNext()
    {
        if (_enemyDestroyed < 4)
        {
            GameObject helicopter = Instantiate(_helicopterPrefab, _spawnPoint.position, Quaternion.identity);
            EnemyHelicopter eh = helicopter.GetComponent<EnemyHelicopter>();
            eh.TargetPoint = _hoverTarget;
            _uiPanel.HelicoptersDestroyed.text = $"{_enemyDestroyed}/4";
            _enemyDestroyed++;
        }
        else
        {
            _uiPanel.EndMission();
            return;
        }
    }
}
