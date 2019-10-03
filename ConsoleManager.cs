using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleManager : MonoBehaviour
{
    public List<string> commands = new List<string>();
    public bool canDie;
    public float delay;
    public KeyCode consoleKey;
    public GameObject logPrefab;
    public GameObject cmdListPrefab;

    private InputField consoleInput;
    private Transform log;
    private GameObject console;
    private GameObject commandList;
    private Transform commandListContainer;

    //Example
    private ConsoleFunctions functions;

    private void Start()
    {
        console = transform.Find("Console").gameObject;
        consoleInput = transform.Find("Console/Main/ConsoleInput").GetComponent<InputField>();
        log = transform.Find("Console/Main/Log/Viewport/Content");
        commandList = transform.Find("Console/Main/CommandList").gameObject;
        commandListContainer = commandList.transform.Find("Viewport/Content");

        //Example
        functions = GetComponent<ConsoleFunctions>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(consoleKey))
        {
            console.SetActive(!console.activeInHierarchy);
            consoleInput.Select();
            consoleInput.ActivateInputField();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Command(consoleInput.text);
            consoleInput.text = "";
            consoleInput.Select();
            consoleInput.ActivateInputField();
        }
    }

    void Command(string cmd)
    {
        if(cmd == "/setColorRed")
        {
            var entry = Instantiate(logPrefab, log);
            entry.GetComponentInChildren<Text>().text = "Set color to Red";
            if (canDie) Destroy(entry, delay);

            functions.ChangeColor(Color.red);
        }

        if (cmd == "/setColorBlue")
        {
            var entry = Instantiate(logPrefab, log);
            entry.GetComponentInChildren<Text>().text = "Set color to Blue";
            if (canDie) Destroy(entry, delay);

            functions.ChangeColor(Color.blue);
        }

        if (cmd == "/setSize2")
        {
            var entry = Instantiate(logPrefab, log);
            entry.GetComponentInChildren<Text>().text = "Set size to 2";
            if (canDie) Destroy(entry, delay);

            functions.ChangeSize(2);
        }

        if (cmd == "/setSize3")
        {
            var entry = Instantiate(logPrefab, log);
            entry.GetComponentInChildren<Text>().text = "Set size to 3";
            if (canDie) Destroy(entry, delay);

            functions.ChangeSize(3);
        }
    }

    public void CheckSimilarCommands()
    {
        if (commandListContainer.childCount > 0)
        {
            int childs = commandListContainer.childCount;
            for (int i = childs - 1; i >= 0; i--)
            {
                Destroy(commandListContainer.GetChild(i).gameObject);
                if (i == 0)
                {
                    CreateCmdList();
                }
            }
        }
        else
        {
            CreateCmdList();
        }
    }

    void CreateCmdList()
    {
        if (!string.IsNullOrEmpty(consoleInput.text))
        {
            List<string> found = commands.FindAll(w => w.StartsWith(consoleInput.text));
            int childs = found.Count;
            if(found.Count > 0) { commandList.SetActive(true); } else { commandList.SetActive(false); }
            for (int i = childs - 1; i >= 0; i--)
            {
                var cmd = Instantiate(cmdListPrefab, commandListContainer);
                cmd.GetComponent<Text>().text = found[i];
            }
        }
        if (consoleInput.text == "") { commandList.SetActive(false); }
    }
}
