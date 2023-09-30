using Cinemachine;
using UnityEngine;

public class PlayerService : GenericLazySingleton<PlayerService>
{
    public GameObject _playerPrefab;

    [HideInInspector]
    public GameObject _player;

    [SerializeField] private GameObject _pointer;
    [SerializeField] private CinemachineVirtualCamera _camera;

    private void Start()
    {
        _player = Instantiate(_playerPrefab, new Vector3(-15f, 0.5f, 56f), Quaternion.identity);
        Instantiate(_pointer, new Vector3(-15f, 0f, 56f), Quaternion.identity);
        _camera.Follow = _player.transform;
    }
}
