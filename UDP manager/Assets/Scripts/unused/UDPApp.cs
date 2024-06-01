using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;

public class UDPApp : MonoBehaviour
{
    private void Start()
    {
       
        var port = 40002;
        var client = new UdpClient(port);
        var remoteEP = new IPEndPoint(IPAddress.Any, port);

        try
        {
            
            while (true)
            {
                Debug.Log("��M�ҋ@���c");
                var bytes = client.Receive(ref remoteEP);

                Debug.Log($"��M! [{remoteEP}] :");
                foreach (var bytevalue in bytes)
                {
                    Debug.Log($" {bytevalue}");
                }
            }
        }
        catch (SocketException ex)
        {
            Debug.Log(ex);
        }
        finally
        {
            client.Close();
        }
    }
}
