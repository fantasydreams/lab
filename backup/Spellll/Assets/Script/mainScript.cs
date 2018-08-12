using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.UI;

public class mainScript : MonoBehaviour {

    private Text prit;
    public GUIText gui;
    //定义基本信息  
    private string portName = "COM3";
    public int baudRate = 9600;
    public Parity parity = Parity.None;
    public int dataBits = 8;
    public StopBits stopBits = StopBits.One;
    public GameObject pic;
    SerialPort sp = null;
    private bool _contiue = false;
    Thread dataReceiveThread;
    Thread controlAnimator;
    private Queue<CMD_TYPE> cmdQueue;
    //发送  
    string message = "";



    //来自arduino板的控制命令，目前支持范围-128-127.
    private enum CMD_TYPE
    {
        plane = (int)'a',
        bulb,
        SP,
    };


    void Start()
    {
        pic = GameObject.Find("pic");
        cmdQueue = new Queue<CMD_TYPE>();
        OpenPort();
    }


    private void commandParse()
    {
        CMD_TYPE command;
        while (_contiue)
        {
            if (cmdQueue.Count > 0)
            {
                lock (cmdQueue)
                {
                    command = cmdQueue.Dequeue();
                }
                Debug.Log("commandParse data =  ");
                Debug.Log(command);
                switch (command)
                {
                    case CMD_TYPE.plane: Plane.Plane_Trigger = true;
                        break;
                    case CMD_TYPE.bulb:bulb.bulbTrigger = true;
                        break;
                    case CMD_TYPE.SP:SpecialEffect.isActivated = true;
                        break;

                    default:break;
                }
            }
            else
            {
                Thread.Sleep(100);
            }
        }
    }


    void Update() //主线程循环更新
    {

    }


    //打开com口
    public bool OpenPort()
    {
        sp = new SerialPort("\\\\.\\" + portName, baudRate, parity, dataBits, stopBits);
        //   sp.ReadTimeout = 400;

        sp.ReadTimeout = 500;//端口读取速度
        try
        {
            sp.Open();
            dataReceiveThread = new Thread(DataReceiveFunction);
            controlAnimator = new Thread(commandParse);
            _contiue = true;
            if (dataReceiveThread.ThreadState != ThreadState.Running)
            {
                dataReceiveThread.Start();
            }
            if(controlAnimator.ThreadState != ThreadState.Running)
            {
                controlAnimator.Start();
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
            if (!_contiue)
            {
                dataReceiveThread.Abort();
                controlAnimator.Abort();
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
        while (_contiue)
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
            catch (TimeoutException)
            {
                continue;
            }
            catch (System.IO.IOException) //热插拔
            {
                Debug.Log("Reconnect the Serial port...");
                ClosePort();
                Thread.Sleep(1000);
                OpenPort();
            }
            catch (System.InvalidOperationException)  //热插拔
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
        _contiue = false;
        ClosePort();
    }
}
