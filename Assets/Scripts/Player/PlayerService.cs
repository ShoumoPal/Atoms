using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class PlayerService : GenericLazySingleton<PlayerService>
{
    public GameObject _playerPrefab;

    public List<GameObject> _players;

    [SerializeField] private GameObject _pointer;
    [SerializeField] private CinemachineVirtualCamera _camera;

    private void OnEnable()
    {
        _players.Add(Instantiate(_playerPrefab, new Vector3(-15f, 0.5f, 56f), Quaternion.identity));
        Instantiate(_pointer, new Vector3(-15f, 0f, 56f), Quaternion.identity);
        CameraFollowPlayer();
    }

    public void AddAtomToList(GameObject _atom)
    {
        _players.Add(_atom);
    }

    public void RemoveAtomFromList(GameObject _atom)
    {
        _players.Remove(_atom);
    }

    public bool ArePlayersPresent()
    {
        return _players.Count > 0;
    }

    public void CameraFollowPlayer()
    {
        if(_players.Count > 0)
        {
            _camera.Follow = _players[0].transform;
        }
    }
}
