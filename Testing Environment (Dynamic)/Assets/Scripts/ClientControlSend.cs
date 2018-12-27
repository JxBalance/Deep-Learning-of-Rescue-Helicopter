using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class ClientControlSend : MonoBehaviour {

    public static int[] actions = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    void Start () {
		
	}
	
    private Socket clientSocket;

    public ClientControlSend()
    {
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }

    public void Connect(string ip, int port)
    {
        clientSocket.Connect(ip, port);
        Debug.Log("Successfully connect server!");

    }

    
    public void ThreadOfReceive()
    {
        Thread threadReceive = new Thread(Receive);
        threadReceive.IsBackground = true;
        threadReceive.Start();
    }

    public void Receive()
    {
        while (true)
        {
            try
            {
                byte[] msg = new byte[1024];
                int msgLen = clientSocket.Receive(msg);
                string action = Encoding.UTF8.GetString(msg, 0, msgLen);
                action = action.Substring(3, 57);
                //Debug.Log(action);
                
                
                int a = -1;
                for (int i = 0; i < action.Length ; i++)
                {
                    if (i==0||i==7||i==14||i==21||i==28||i==35||i==42||i==49||i==56)
                    {
                        a += 1;
                        actions[a] = Convert.ToInt32(action[i].ToString());
                    }
                } 
            }
            catch (Exception)
            {
                Console.WriteLine("服务器积极拒绝");
                break;
            }
        }
    }

    public void Send(string msg)
    {
        clientSocket.Send(Encoding.UTF8.GetBytes(msg));
    }
    
}
