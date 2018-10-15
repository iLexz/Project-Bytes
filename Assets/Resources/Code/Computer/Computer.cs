using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Computer : MonoBehaviour {

    [SerializeField] private GameObject computerUiObject;
    [SerializeField] private Text computerUiText;
    [SerializeField] private bool openComputerOnStart;
    [SerializeField] private ComputerBasicInfo computerInfo;
    [SerializeField] private ComputerFileSystem computerFilesys;

    private int selection = -1;
    private string[] directories;

    private bool isBooted;

    private void Start()
    {
        directories = computerFilesys.directories;

        if (openComputerOnStart)
        {
            Boot();
        }
    }

    private void Update()
    {
        if (isBooted)
        {
            computerUiText.text = string.Empty;
            computerUiText.text = "\n" +
                "  " + computerInfo.operatingSystem +
                "\n\n";

            for (uint i = 0; i < directories.Length; i++)
            {
                computerUiText.text += "| " + directories[i];
                if (i == selection)
                {
                    computerUiText.text += " ◄\n";
                }
                else
                {
                    computerUiText.text += "\n";
                }
            }

            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                SelectionDown();
            }
            else if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                SelectionUp();
            }
            else if (Input.GetKeyUp(KeyCode.Return))
            {
                EnterDir(selection);
            }
        }
    }

    public void SelectionUp()
    {
        if (selection > 0)
        {
            selection--;
        }
    }

    public void SelectionDown()
    {
        if (selection < directories.Length - 1)
        {
            selection++;
        }
    }

    public void Boot()
    {
        isBooted = true;
        computerUiObject.SetActive(true);

        selection = 0;
    }

    public void Shutdown()
    {
        isBooted = false;
        computerUiObject.SetActive(false);
    }

    public bool IsBooted()
    {
        return isBooted;
    }

    public void SelectDir(int index)
    {
        if (index >= 0) directories[index].Remove(directories[index].Length - 2);

        selection = index;
        directories[index] += " ◄";
    }

    public void EnterDir(int index)
    {
        if (computerFilesys.objective == index)
        {
            StartHack();
        }
        else
        {
            FailHack();
        }
    }

    public void StartHack()
    {
        Debug.Log("haxored the fakin server m8");
        // Do hacking shit. Yes.
    }

    public void FailHack()
    {
        Debug.Log("failed hard as fuk");
        // shitty haker.
    }

}
