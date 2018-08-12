using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.UI;





public class test : MonoBehaviour
{

    private Text prit;
    public GUIText gui;
    //定义基本信息  
    private string portName = "COM3";
    public int baudRate = 9600;
    public Parity parity = Parity.None;
    public int dataBits = 8;
    public StopBits stopBits = StopBits.One;
    public GameObject pic;
    private string xx = "接收的内容";
    SerialPort sp = null;
    private bool RecState = false;
    Thread dataReceiveThread;
    private Queue<CMD_TYPE> cmdQueue;
    //发送  
    string message = "";

    

    //来自arduino板的控制命令，目前支持范围-128-127.
    private enum CMD_TYPE
    {
        DOG = 'a',
        Cat
    };


    void Start()
    {
        pic = GameObject.Find("pic");
        dataReceiveThread = new Thread(DataReceiveFunction);
        cmdQueue = new Queue<CMD_TYPE>();
        OpenPort();
    }



    void Update() //主线程循环更新
    {
        if(cmdQueue.Count > 0)
        {
            CMD_TYPE cmd;
            lock (cmdQueue)
            {
                 cmd = cmdQueue.Dequeue();
            }
            if (cmd == (CMD_TYPE)'a')
            {
                pic.SetActive(!pic.active);
            }
        }
    }


    //打开com口
    public bool OpenPort()
    {
        sp = new SerialPort("\\\\.\\" + portName, baudRate, parity, dataBits, stopBits);
        sp.DataBits = 8;
        //   sp.ReadTimeout = 400;

        sp.ReadTimeout = 500;//端口读取速度
        try
        {
            sp.Open();
            RecState = true;
            if (dataReceiveThread.ThreadState != ThreadState.Running)
            {
                dataReceiveThread.Start();
            }
        }
        catch (System.IO.IOException)
        {
            return false;
        }

        return true;
    }
    //关闭com口和线程
    public void ClosePort()
    {
        try
        {
            if (!RecState)
            {
                dataReceiveThread.Abort();
            }
            sp.Close();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    public void DataReceiveFunction()
    {
        Debug.Log("Recive data...");
        while (RecState)
        {
            try
            {
                CMD_TYPE cmd = (CMD_TYPE)sp.ReadChar();
                Debug.Log(cmd);

                lock (cmdQueue)
                {
                    cmdQueue.Enqueue(cmd);
                }
            }
            catch(TimeoutException)
            {
                continue;
            }
            catch(System.IO.IOException) //热插拔
            {
                Debug.Log("Reconnect the Serial port...");
                ClosePort();
                Thread.Sleep(1000);
                OpenPort();
            }
            catch(System.InvalidOperationException)  //热插拔
            {
                Debug.Log("Reconnect the Serial port...");
                ClosePort();
                Thread.Sleep(1000);
                OpenPort();
            }
        }
    }
    //发送数据
    public void WriteData(string dataStr)
    {
        if (sp.IsOpen)
        {
            sp.Write(dataStr);
        }

    }

    //退出时关闭com口
    void OnApplicationQuit()
    {
        RecState = false;
        ClosePort();
    }


    void OnGUI()
    {
        message = GUILayout.TextField(message);
        if (GUILayout.Button("Send Message"))
        {
            WriteData(message);
        }
        //  portName = GUILayout.TextField(portName);

        if (GUILayout.Button("connect COM"))
        {
            OpenPort();
        }

    }

}