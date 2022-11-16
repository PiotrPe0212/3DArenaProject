using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace PiotrPietraszek
{
    //Handle the current state of the game. Turns up and down panels and handle buttons actions.
    public class GameManager : MonoBehaviour
    { 
        public static GameManager Instance;
        public GameState State;
        [SerializeField] private GameObject _menuPlane;
        [SerializeField] private GameObject _pausePlane;
        [SerializeField] private GameObject __mainMenuButton;
        [SerializeField] private GameObject _pauseMenuButton;
        public static event System.Action<GameState> OnGameStateChange;
        public static event System.Action ResetGame;

        private void Awake()
        {
            Instance = this;
        }


        private void Start()
        {
            _menuPlane.SetActive(false);

            _pausePlane.SetActive(false);
            GameStateUpdate(State);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) PauseClicked();
        }
        public void GameStateUpdate(GameState newState)
        {
            State = newState;
            switch (newState)
            {
                case GameState.MainMenu:
                    _menuPlane.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(__mainMenuButton);
                    Time.timeScale = 0;
                    break;
                case GameState.PlayGame:
                    _menuPlane.SetActive(false);
                    _pausePlane.SetActive(false);
                    Time.timeScale = 1;
                    break;
                case GameState.Pause:
                    _pausePlane.SetActive(true);
                    Time.timeScale = 0;
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(_pauseMenuButton);
                    break;
                case GameState.Lose:
                    _menuPlane.SetActive(true);
                    ResetGame?.Invoke();
                    break;
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(newState), newState, "Wrong argument!");
            }
            OnGameStateChange?.Invoke(newState);
        }


        public enum GameState
        {
            MainMenu,
            PlayGame,
            Pause,
            Lose
        }

        //Buttons

        private void PauseClicked()
        {
            GameStateUpdate(GameState.Pause);
        }
        public void PlayButton()
        {
            GameStateUpdate(GameState.PlayGame);
        }

        public void ContinueButton()
        {
            GameStateUpdate(GameState.PlayGame);
        }

        public void ExitButton()
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }
}
