using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlowManager : MonoBehaviour
{
    public GameObject settingUI;
    public MobileCam mobileCam;

    void Start()
    {
        settingUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (settingUI.activeSelf) {//关闭暂停面板
                settingUI.SetActive(false);
                mobileCam.froze = false;
                Cursor.visible = !mobileCam.isFreeMove;
                if (mobileCam.isFreeMove) {
                    Cursor.lockState = CursorLockMode.Confined;
                }
            } else {//打开暂停面板
                settingUI.SetActive(true);
                mobileCam.froze = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeCamState()
    {
        settingUI.SetActive(false);
        mobileCam.froze = false;

        mobileCam.isFreeMove = !mobileCam.isFreeMove;
        if (!mobileCam.isFreeMove) {
            mobileCam.ResetCamTransform();
        }

        Cursor.visible = !mobileCam.isFreeMove;
        if (mobileCam.isFreeMove) {
            Cursor.lockState = CursorLockMode.Confined;
        } else {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void Autumn()
    {
        SceneManager.LoadScene("Autumn");
    }
    public void Winter()
    {
        SceneManager.LoadScene("Winter");
    }

}
