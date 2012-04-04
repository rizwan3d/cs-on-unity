using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon;

public class PhotonStatsGui : MonoBehaviour
{
    public Rect statsRect = new Rect(0, 0, 200, 50);
    public float WidthWithText = 400;
    public bool statsWindowOn;
    public bool statsOn;
    public bool textOn;
    public bool resetButtonOn;

    void Start()
    {
        float width = statsRect.width;
        if (textOn)
        {
            width = WidthWithText;
        }

        statsRect = new Rect(Screen.width - width, 0, width, statsRect.height);
    }

    void OnGUI()
    {
        if (PhotonNetwork.networkingPeer.TrafficStatsEnabled != statsOn)
        {
            PhotonNetwork.networkingPeer.TrafficStatsEnabled = statsOn;
        }

        if (!statsWindowOn)
        {
            return;
        }

        statsRect = GUILayout.Window(0, statsRect, this.TrafficStatsWindow, "Messages");

        if (GUILayout.Button("Disconnect"))
        {
            PhotonNetwork.Disconnect();
        }
    }

    void TrafficStatsWindow(int windowID)
    {
        TrafficStatsGameLevel gls = PhotonNetwork.networkingPeer.TrafficStatsGameLevel;
        long elapsedMs = PhotonNetwork.networkingPeer.TrafficStatsElapsedMs / 1000;
        if (elapsedMs == 0)
        {
            elapsedMs = 1;
        }

        GUILayout.Label(string.Format("Out|In|Sum:\t{0,4} | {1,4} | {2,4}", gls.TotalOutgoingMessageCount, gls.TotalIncomingMessageCount, gls.TotalMessageCount));
        GUILayout.Label(string.Format("{0}sec average:", elapsedMs));
        GUILayout.Label(string.Format("Out|In|Sum:\t{0,4} | {1,4} | {2,4}", gls.TotalOutgoingMessageCount / elapsedMs, gls.TotalIncomingMessageCount / elapsedMs, gls.TotalMessageCount / elapsedMs));

        if (this.resetButtonOn)
        {
            if (GUILayout.Button("Reset"))
            {
                PhotonNetwork.networkingPeer.TrafficStatsReset();
                PhotonNetwork.networkingPeer.TrafficStatsEnabled = true;
            }
        }

        if (this.textOn)
        {
            GUILayout.Label("Incoming: " + PhotonNetwork.networkingPeer.TrafficStatsIncoming.ToString());
            GUILayout.Label("Outgoing: " + PhotonNetwork.networkingPeer.TrafficStatsOutgoing.ToString());
        }

        GUI.DragWindow();
    }
}
