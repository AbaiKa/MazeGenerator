using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssistant : MonoBehaviour
{
    public static GameAssistant Instance;

    [SerializeField] private GameObject _playerPrefab;

    [SerializeField] private CinemachineFreeLook _c_camera;
    [SerializeField] private Camera _m_camera;

    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject _youWonPanel;

    public bool playerWon = false;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) OpenMainMenu();
    }
    public GameObject SpawnPlayerOnFirstCell(int x, int y)
    {
        GameObject player = Instantiate(_playerPrefab, new Vector3(x, y), Quaternion.identity);

        _c_camera.Follow = player.transform;
        _c_camera.LookAt = player.transform;

        _mainMenuPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        playerWon = false;
        return player;
    }

    public Transform GetCameraTransform() => _m_camera.transform;
    private void OpenMainMenu()
    {
        bool enabled = !_mainMenuPanel.activeInHierarchy;
        Cursor.lockState = enabled ? CursorLockMode.None : CursorLockMode.Locked;

        _mainMenuPanel.SetActive(enabled);
        _youWonPanel.SetActive(false);
    }

    public void EnableYouWinPanel()
    {
        playerWon = true;
        _youWonPanel.SetActive(true);
    }
}
