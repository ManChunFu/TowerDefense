using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PauseMenu _pauseMenu = null;

    private void Start()
    {
        if (_pauseMenu != null)
            return;

        _pauseMenu = FindObjectOfType<Canvas>()?.GetComponent<PauseMenu>();
        if (_pauseMenu == null)
            throw new MissingReferenceException("Missing refrence of PauseMenu script.");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            _pauseMenu.Pause();
    }
}
