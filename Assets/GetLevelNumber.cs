using UnityEngine;
using TMPro;
using System.Drawing;

public enum Screentype
{
    lose,win
}

public class GetLevelNumber : MonoBehaviour
{
    public TextMeshProUGUI leveltext;

    public int levelNum;

    [SerializeField] Screentype screentype;

    void Start()
    {
        if (levelNum == 0)
        {
            // Get the name of the current scene
            string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            // Extract the numeric part from the scene name
            int extractedLevelNum = ExtractLevelNumber(sceneName);

            if (extractedLevelNum > 0)
            {
                switch (screentype)
                {
                    case Screentype.lose:
                            levelNum = extractedLevelNum;
                            // Set the text of the TextMeshProUGUI component
                            leveltext.text = "<size=70%>LEVEL " + levelNum +  "\n<size=100%> FAILED";
                        break;
                    case Screentype.win:
                        levelNum = extractedLevelNum;
                        // Set the text of the TextMeshProUGUI component
                        leveltext.text = "LEVEL " + levelNum + "\nCOMPLETE";
                        break;
                }

            }
        }
    }

    int ExtractLevelNumber(string sceneName)
    {
        // Assuming scene name is in the format "level X"
        string[] parts = sceneName.Split(' ');
        if (parts.Length >= 2 && int.TryParse(parts[1], out int levelNum))
        {
            return levelNum;
        }
        return 0; // Default level number if extraction fails
    }
}
