using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

enum FormatRequestType
{
    json,
    xml
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private string url;
    [SerializeField] private string apiKey;
    [SerializeField] private string from;
    [SerializeField] private string to;

    [SerializeField] private FormatRequestType format;

    [SerializeField] private Text responseTextBox;

    [SerializeField] private Text errorTextBox;
    [SerializeField] private GameObject reconnectButton;
    [TextArea(1,100)]
    [SerializeField] private string connectErrorText;

    private string _path;

    private void Start()
    {
        Connected();
    }
    public void Connected()
    {
        _path = $"{url}apikey={apiKey}&format={format}&from={from}&to={to}";

        errorTextBox.text = "";
        reconnectButton.SetActive(false);

        StartCoroutine(HttpClient.Get<Root>(_path, (root) => Response(root), (type, text) => ErrorHandler(type, text), (s) => { print(s); }));
        StartCoroutine(ShowReconnectButton());

    }

    private void Response(Root root)
    {
        if (root != null)
        {
            errorTextBox.text = "";
            reconnectButton.SetActive(false);

            var firstThread = root.segments[0];
            var text = $"Путь из {firstThread.from.title} в {firstThread.to.title}. Время отправления {firstThread.departure}. Время прибытия {root.segments[0].arrival} ";

            responseTextBox.text = text;
        }

    }

    private void ErrorHandler(ErrorType error, string errorText)
    {
        switch (error)
        {
            case ErrorType.Connection:
                {
                    ErrorBoxController(connectErrorText);
                }
                break;
            case ErrorType.Protocol:
                {
                    ErrorBoxController(errorText);
                }
                break;
        }
    }

    private void ErrorBoxController(string errorText)
    {
        errorTextBox.text = errorText;
        responseTextBox.text = "";
        reconnectButton.SetActive(true);
    }



    // для вывода кнопки через время
    private IEnumerator ShowReconnectButton()
    {
        yield return new WaitForSeconds(2.5f);
        reconnectButton.SetActive(true);
    }

}
