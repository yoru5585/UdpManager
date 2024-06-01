using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UDP_project;
using TMPro;
using UnityEngine.UI;

public class Send_udp
{
    private UDP commUDP = new UDP();
    public void setup(int port_snd, int port_to, int port_rcv, string remoteHost)
    {
        //commUDP.init(int型の送信用ポート番号, int型の送信先ポート番号, int型の受信用ポート番号);
        commUDP.init(port_snd, port_to, port_rcv);
        //ipアドレスをセット 
        commUDP.set_remoteHost(remoteHost);
    }

    public void send(Slider valueSlider)
    {
        float send_value = valueSlider.value;
        float[] av = { send_value };
        commUDP.send_xyz_arr(av);
    }

    public void end()
    {
        commUDP.end();
    }
}
