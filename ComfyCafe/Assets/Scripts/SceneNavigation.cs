using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    public void startGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void openShop() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void closeShop() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + -1);
    }

    public void openCustomers() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void closeCustomers() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

}
