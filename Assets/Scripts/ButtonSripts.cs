using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSripts : MonoBehaviour
{
   public void LoadGame()
   {
      SceneManager.LoadScene("GameScene");
   }

   public void Quit()
   {
      Application.Quit();
   }

}
