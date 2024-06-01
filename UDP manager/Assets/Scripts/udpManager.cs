using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UDP_project;
using TMPro;
using UnityEngine.UI;

public class udpManager : MonoBehaviour
{
    private UDP commUDP = new UDP();
    bool IsStop;
    bool sendFlag;
    bool rcvFlag;
    [SerializeField] TMP_InputField remoteHostText;
    [SerializeField] TMP_InputField port_snd;
    [SerializeField] TMP_InputField port_to;
    [SerializeField] TMP_InputField port_rcv;
    int port_snd_int;
    int port_to_int;
    int port_rcv_int;

    [SerializeField] Animator iconAnim;
    [SerializeField] TextMeshProUGUI sendButtonText;
    [SerializeField] TextMeshProUGUI rcvButtonText;
    [SerializeField] Slider valueSlider;
    [SerializeField] TextMeshProUGUI logText;

    [SerializeField] Image changeImg;
    [SerializeField] Sprite sendSpr;
    [SerializeField] Sprite rcvSpr;

    [SerializeField] GameObject sendObj;
    [SerializeField] GameObject rcvObj;

    Receive_udp recv = new Receive_udp();
    Send_udp send = new Send_udp();

    // Start is called before the first frame update
    void Start()
    {
        IsStop = true;
        sendFlag = true;
        rcvFlag = false;

        remoteHostText.text = "192.168.0.124"; //メタクエスト→"192.168.1.119" iphone→"192.168.1.169"
        port_snd.text = "40005";
        port_to.text = "40002";
        port_rcv.text = "40002";

        parseInt();
        send.setup(port_snd_int, port_to_int, port_rcv_int, remoteHostText.text);

        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsStop) return;

        if (rcvFlag)
        {
            recv.receive(logText, valueSlider);
        }

        if (sendFlag) 
        {
            send.send(valueSlider); 
            //Debug.Log("send");
        } 
    }

    void parseInt()
    {
        //int型へ変換
        port_snd_int = int.Parse(port_snd.text);
        port_to_int = int.Parse(port_to.text);
        port_rcv_int = int.Parse(port_rcv.text);
    }

    //void send()
    //{
    //    float send_value = valueSlider.value;
    //    float[] av = { send_value };
    //    commUDP.send_xyz_arr(av);

    //}

    //void receive()
    //{
    //    Debug.Log("aaa");
    //    //var a = commUDP.rcvMsg_xyz;
    //    commUDP.start_receive();
    //    var b = commUDP.rcv_float_arr;

    //    for (int i = 0; i < b.Count; i++)
    //    {
    //        logText.text = b[i].ToString();
    //        valueSlider.value = b[i];
    //    }
    //}

    public void OnButtonClicked_send()
    {
        if (IsStop)
        {
            parseInt();
            send.setup(port_snd_int, port_to_int, port_rcv_int, remoteHostText.text);

            iconAnim.SetBool("flag", true);
            sendButtonText.text = "stop";

            IsStop = false;
        }
        else
        {
            send.end();

            iconAnim.SetBool("flag", false);
            sendButtonText.text = "send";
            
            IsStop = true;
        }
    }

    public void OnButtonClicked_rcv()
    {
        if (IsStop)
        {
            parseInt();
            recv.setup(port_snd_int, port_to_int, port_rcv_int);

            iconAnim.SetBool("flag", true);
            rcvButtonText.text = "stop";

            IsStop = false;
        }
        else
        {
            recv.end();

            iconAnim.SetBool("flag", false);
            rcvButtonText.text = "receive";

            IsStop = true;
        }
    }

    public void changeMode()
    {
        IsStop = true;
        iconAnim.SetBool("flag", false);

        if (sendFlag)
        {
            sendFlag = false;
            rcvFlag = true;
            
            rcvButtonText.text = "receive";
            sendButtonText.text = "stop";
            changeImg.sprite = rcvSpr;
            rcvObj.SetActive(true);
            sendObj.SetActive(false);
        }
        else
        {
            sendFlag = true;
            rcvFlag = false;

            sendButtonText.text = "send";
            rcvButtonText.text = "stop";
            changeImg.sprite = sendSpr;
            rcvObj.SetActive(false);
            sendObj.SetActive(true);
        }
    }

    public void OnFinish()
    {
        recv.end();
        send.end();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }
}
