using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UDP_project;
using TMPro;
using UnityEngine.UI;

public class Receive_udp
{
    private UDP commUDP = new UDP();
    public void setup(int port_snd, int port_to, int port_rcv)
    {
        //commUDP.init(int型の送信用ポート番号, int型の送信先ポート番号, int型の受信用ポート番号);
        commUDP.init(port_snd, port_to, port_rcv);
        commUDP.start_receive();
    }

    public void receive(TextMeshProUGUI logText, Slider valueSlider)
    {
        var b = commUDP.rcv_float_arr;
        Debug.Log(b.Count);
        if (b.Count == 0)
        {
            return;
        }

        for (int i = 0; i < b.Count; i++)
        {
            logText.text = b[i].ToString();
            valueSlider.value = b[i];
        }

    }

    public void end()
    {
        commUDP.end();
    }
}
