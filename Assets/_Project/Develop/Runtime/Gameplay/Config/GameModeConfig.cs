using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Config
{
    [CreateAssetMenu(fileName = "GameModeConfig", menuName = "Configs/GameModeConfig")]
    public class GameModeConfig : ScriptableObject
    {
        [field: SerializeField, Header("Sequence Generation Settings")]
        public string NumbersCharacters { get; private set; } = "0123456789";

        [field: SerializeField]
        public string LettersCharacters { get; private set; } = "abcdefghijklmnopqrstuvwxyz";

        [field: SerializeField]
        public int SequenceLength { get; private set; } = 5;

        public string GetCharactersForMode(GameModeType mode)
        {
            return mode switch
            {
                GameModeType.Numbers => NumbersCharacters,
                GameModeType.Letters => LettersCharacters,
                _ => throw new System.ArgumentOutOfRangeException(nameof(mode), mode, null)
            };
        }
    }
}
