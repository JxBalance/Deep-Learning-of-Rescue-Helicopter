using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.IO;

public class ControlPanel : MonoBehaviour
{

    public AudioSource MusicSound;
    public bool Wave;
    public bool Danger;
    public bool Transport;
    public float SP;
    private string height;
    public GameObject Helicopter;
    int number;

    private float spendTime;
    private float realTime;
    private int hour;
    private int minute;
    private int second;

    string path = @"E:\Graduate\Research\Helicopter Rescue\NNT\InputPredicted2.txt";
    private int m;
    private int[,] testInput = new int[1786, 9];
    private int[] A = new int[9];
    private Transform m_Transform;
    private Rigidbody m_Rigidbody;
    private UISlider m_UISlider;
    private UILabel m_UILabel1;
    private UILabel m_UILabel2;
    private bool open = false;

    [SerializeField]
    KeyCode SpeedUp = KeyCode.Space;
    [SerializeField]
    KeyCode SpeedDown = KeyCode.C;
    [SerializeField]
    KeyCode Forward = KeyCode.W;
    [SerializeField]
    KeyCode Back = KeyCode.S;
    [SerializeField]
    KeyCode Left = KeyCode.A;
    [SerializeField]
    KeyCode Right = KeyCode.D;
    [SerializeField]
    KeyCode TurnLeft = KeyCode.Q;
    [SerializeField]
    KeyCode TurnRight = KeyCode.E;
    [SerializeField]
    KeyCode MusicOffOn = KeyCode.M;

    private KeyCode[] keyCodes;

    public Action<PressedKeyCode[]> KeyPressed;
    private void Awake()
    {
        keyCodes = new[] {
                            SpeedUp,
                            SpeedDown,
                            Forward,
                            Back,
                            Left,
                            Right,
                            TurnLeft,
                            TurnRight
                        };

    }

    void Start()
    {
        number = -1;
        m = -1;
        m_Transform = Helicopter.GetComponent<Transform>();
        m_Rigidbody = Helicopter.GetComponent<Rigidbody>();
        m_UISlider = GameObject.Find("CurrentSurplusPower").GetComponent<UISlider>();
        m_UILabel1 = GameObject.Find("Height").GetComponent<UILabel>();
        m_UILabel2 = GameObject.Find("Time").GetComponent<UILabel>();
        testInput = ReadData(path);
        Wave = true;
        Danger = true;
        Transport = true;
        SP = 0.9f;

    }

    void FixedUpdate()
    {
        number += 1;
        var pressedKeyCode = new List<PressedKeyCode>();

        if (number <= 400)
        {
            pressedKeyCode.Add((PressedKeyCode)0);
        }

        if (number <=710&&number >400)
        {
            pressedKeyCode.Add((PressedKeyCode)4);
        }

        if (number>710&&(Wave==true||Danger==true||Transport==true||SP<=0.1f))
        {
            pressedKeyCode.Add((PressedKeyCode)4);
        }
        if (number>710&&Wave==false&&Danger==false&&Transport==false&&SP>0.1f&&m_Transform.localEulerAngles.y >=46&& m_Transform.localEulerAngles.y<340)
        {
            
            pressedKeyCode.Add((PressedKeyCode)4);
        }
        if (number > 710 && m_Rigidbody.angularVelocity.y>-0.1f)
        {
            open = true;
        }
        if (open==true)
        {
            m += 1;
            for (int k = 0; k < A.Length; k++)
            {
                A[k] = testInput[m, k];
            }

            for (int index = 0; index < keyCodes.Length; index++)
            {
                if (A[index] == 1)
                    pressedKeyCode.Add((PressedKeyCode)index);
            }
        }

        if (KeyPressed != null)
            KeyPressed(pressedKeyCode.ToArray());

        // for test
        if (Input.GetKey(MusicOffOn))
        {
            if (MusicSound.volume == 1) return;
            /*            if (MusicSound.isPlaying)
                            MusicSound.Stop();
                        else*/
            MusicSound.volume = 1;
            MusicSound.Play();
        }

        GetHeight();
        GetTime();
    }


    private void Update()
    {
        SPRange();
    }

    private int[,] ReadData(string path)
    {
        string str = File.ReadAllText(path);
        string[] hang = str.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        string[] lie = new string[9];
        int[,] arr = new int[hang.Length, lie.Length];

        for (int i = 0; i < hang.Length; i++)
        {
            lie = hang[i].Split(' ');
            for (int j = 0; j < lie.Length; j++)
            {
                arr[i, j] = (int)Convert.ToDouble(lie[j]);
            }
        }
        Debug.Log("数据导入成功！");
        return arr;
    }

    private void SPRange()
    {
        if (SP <= 0)
        {
            SP = 0;
        }
        if (SP >= 100)
        {
            SP = 100;
        }
    }

    public void SetWave()
    {
        if (Wave==false)
        {
            Wave = true;
        }
        else
        {
            Wave = false;
        }
    }

    public void SetDanger()
    {
        if (Danger == false)
        {
            Danger = true;
        }
        else
        {
            Danger = false;
        }
    }

    public void SetTransport()
    {
        if (Transport == false)
        {
            Transport = true;
        }
        else
        {
            Transport = false;
        }
    }

    public void SetSP()
    {
        SP = m_UISlider.value;
    }

    private void GetHeight()
    {
        height = String.Format("{0:F}", m_Transform.position.y / 73 * 100);
        m_UILabel1.text = "Height:  " + height + "m";
    }

    private void GetTime()
    {
        spendTime += Time.deltaTime;
        realTime = spendTime * 15;
        hour = (int)realTime / 3600;
        minute = (int)(realTime - hour * 3600) / 60;
        second = (int)(realTime - hour * 3600 - minute * 60);

        m_UILabel2.text = "Time:  " + string.Format("{0:D2}:{1:D2}:{2:D2}", hour, minute, second);
    }
}
