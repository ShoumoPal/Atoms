using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerService : GenericLazySingleton<PlayerService>
{
    public AtomController _playerPrefab;

    public List<AtomController> _players;

    [SerializeField] private GameObject _pointer;
    [SerializeField] private CinemachineVirtualCamera _camera;

    private void OnEnable()
    {
        _players.Add(Instantiate<AtomController>(_playerPrefab, LevelManagerService.Instance.GetSpawnPointFromLevelName(SceneManager.GetActiveScene().name), Quaternion.identity));
        Instantiate(_pointer, new Vector3(-15f, 0f, 56f), Quaternion.identity);
        CameraFollowPlayer();
    }

    public void AddAtomToList(AtomController _atom)
    {
        _players.Add(_atom);
    }


    public void RemoveAtomFromList(AtomController _atom)
    {
        _players.Remove(_atom);
    }

    public bool ArePlayersPresent()
    {
        return _players.Count > 0;
    }

    public CinemachineVirtualCamera GetCamera()
    {
        return _camera;
    }

    public void CameraFollowPlayer()
    {
        if(_players.Count > 0)
        {
            _camera.Follow = _players[0].transform;
        }
    }
}
