using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerService : GenericLazySingleton<PlayerService>
{
    public AtomController _playerPrefab;

    public List<AtomController> _players = new List<AtomController>();

    [SerializeField] private GameObject _pointer;
    [SerializeField] private CinemachineVirtualCamera _camera;

    private void OnEnable()
    {
        Instantiate(_playerPrefab, LevelManagerService.Instance.GetSpawnPointFromLevelName(SceneManager.GetActiveScene().name), Quaternion.identity);
        Instantiate(_pointer, LevelManagerService.Instance.GetSpawnPointFromLevelName(SceneManager.GetActiveScene().name), Quaternion.identity);
        CameraFollowPlayer();
    }

    public void AddAtomToList(AtomController _atom)
    {
        _players.Add(_atom);
    }


    public void RemoveAtomFromList(AtomController _atom)
    {
        _players.Remove(_atom);
        Debug.Log("Removed from list, new count: " + _players.Count);
        if (!ArePlayersPresent())
        {
            Debug.Log("Failed");
        }
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
