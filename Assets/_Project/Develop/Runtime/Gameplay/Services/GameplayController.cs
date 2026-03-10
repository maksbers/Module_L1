using Assets._Project.Develop.Runtime.Gameplay.Config;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using System.Text;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Services
{
    public class GameplayController
    {
        private readonly ConfigsProviderService _configsProvider;
        private readonly SequenceGeneratorService _sequenceGenerator;
        private readonly DIContainer _container;
        private readonly GameplayInputArgs _inputArgs;

        private GameModeConfig _gameModeConfig;
        private string _targetSequence;
        private StringBuilder _currentInput;

        private bool _isGameOver;

        public GameplayController(
            ConfigsProviderService configsProvider,
            SequenceGeneratorService sequenceGenerator,
            DIContainer container,
            GameplayInputArgs inputArgs)
        {
            _configsProvider = configsProvider;
            _sequenceGenerator = sequenceGenerator;
            _container = container;
            _inputArgs = inputArgs;
        }

        public void Initialize()
        {
            _gameModeConfig = _configsProvider.GetConfig<GameModeConfig>();
            _currentInput = new StringBuilder();
            StartNewGame();
        }

        private void StartNewGame()
        {
            _isGameOver = false;
            _currentInput.Clear();

            GameModeType mode = _inputArgs.SelectedMode;
            string characters = _gameModeConfig.GetCharactersForMode(mode);
            int length = _gameModeConfig.SequenceLength;

            _targetSequence = _sequenceGenerator.Generate(characters, length);

            Debug.Log($"<color=cyan>--- NEW GAME ---</color>");
            Debug.Log($"Mode: {mode}");
            Debug.Log($"Target sequence: <color=yellow><b>{_targetSequence}</b></color>");
            Debug.Log("Please start typing the sequence...");
        }

        public void ProcessInput()
        {
            if (_isGameOver)
                ProcessGameOverInput();
            else
                ProcessGameplayInput();
        }

        private void ProcessGameOverInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_currentInput.ToString() == _targetSequence)
                    ReturnToMainMenu();
                else
                    RestartGameplay();
            }
        }

        private void ProcessGameplayInput()
        {
            if (Input.anyKeyDown)
            {
                string inputString = Input.inputString;

                if (string.IsNullOrEmpty(inputString) == false)
                {
                    char inputChar = inputString[0];

                    if (inputChar == '\b' || inputChar == '\n' || inputChar == '\r')
                        return;

                    _currentInput.Append(inputChar);
                    CheckSequenceMatch();
                }
            }
        }

        private void CheckSequenceMatch()
        {
            string currentStr = _currentInput.ToString();

            if (_targetSequence.StartsWith(currentStr) == false)
            {
                Debug.Log($"<color=red>DEFEAT!</color> You typed: {currentStr}");
                Debug.Log("Press <color=yellow>SPACE</color> to restart the game.");

                _isGameOver = true;
                return;
            }

            if (currentStr.Length == _targetSequence.Length)
            {
                Debug.Log("<color=green>VICTORY!</color> You entered the correct sequence.");
                Debug.Log("Press <color=yellow>SPACE</color> to return to the Main Menu.");

                _isGameOver = true;
            }
        }

        private void ReturnToMainMenu()
        {
            SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
            coroutinesPerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu));
        }

        private void RestartGameplay()
        {
            SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
            coroutinesPerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.Gameplay, _inputArgs));
        }
    }
}
