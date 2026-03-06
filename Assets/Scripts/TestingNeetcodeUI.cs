using Unity.Netcode;
using UnityEngine;

public class TestingNeetcodeUI : MonoBehaviour
{
    public void HostButton()
    {
        Debug.Log("Host");
        NetworkManager.Singleton.StartHost();
        Hide();
    }

    public void ClientButton()
    {
        Debug.Log("Client");
        NetworkManager.Singleton.StartClient();
        Hide();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
