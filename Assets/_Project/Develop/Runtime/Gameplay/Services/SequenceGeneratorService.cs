using System.Text;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Services
{
    public class SequenceGeneratorService
    {
        public string Generate(string characterSet, int length)
        {
            if (string.IsNullOrEmpty(characterSet))
                return string.Empty;

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                int index = Random.Range(0, characterSet.Length);
                builder.Append(characterSet[index]);
            }

            return builder.ToString();
        }
    }
}
