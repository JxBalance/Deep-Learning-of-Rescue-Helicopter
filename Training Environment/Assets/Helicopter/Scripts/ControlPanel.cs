using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.IO;

public class ControlPanel : MonoBehaviour
{

    public AudioSource MusicSound;

    private int leftShift;
    private int space;
    private int w;
    private int s;
    private int a;
    private int d;
    private int q;
    private int e;
    private int noKeyPressed;

    List<List<float>> key = new List<List<float>>();

    //private string m;

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

    }

    void FixedUpdate()
    {
        var pressedKeyCode = new List<PressedKeyCode>();
        for (int index = 0; index < keyCodes.Length; index++)
        {
            var keyCode = keyCodes[index];
            if (Input.GetKey(keyCode))
                pressedKeyCode.Add((PressedKeyCode)index);
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

    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            leftShift = 1;
            Debug.Log("LeftShift key was pressed.");
        }
        else
        {
            leftShift = 0;
            Debug.Log("LeftShift key was released.");
        }

        if (Input.GetKey(KeyCode.Space))
        {
            space = 1;
            Debug.Log("Space key was pressed.");
        }
        else
        {
            space = 0;
            Debug.Log("Space key was released.");
        }

        if (Input.GetKey(KeyCode.W))
        {
            w = 1;
            Debug.Log("W key was pressed.");
        }
        else
        {
            w = 0;
            Debug.Log("W key was released.");
        }

        if (Input.GetKey(KeyCode.S))
        {
            s = 1;
            Debug.Log("S key was pressed.");
        }
        else
        {
            s = 0;
            Debug.Log("S key was released.");
        }

        if (Input.GetKey(KeyCode.A))
        {
            a = 1;
            Debug.Log("A key was pressed.");
        }
        else
        {
            a = 0;
            Debug.Log("A key was released.");
        }

        if (Input.GetKey(KeyCode.D))
        {
            d = 1;
            Debug.Log("D key was pressed.");
        }
        else
        {
            d = 0;
            Debug.Log("D key was released.");
        }

        if (Input.GetKey(KeyCode.Q))
        {
            q = 1;
            Debug.Log("Q key was pressed.");
        }
        else
        {
            q = 0;
            Debug.Log("Q key was released.");
        }

        if (Input.GetKey(KeyCode.E))
        {
            e = 1;
            Debug.Log("E key was pressed.");
        }
        else
        {
            e = 0;
            Debug.Log("E key was released.");
        }

        if (leftShift == 0 && space == 0 && a == 0 && s == 0 && d == 0 && q == 0 && w == 0 && e == 0)
        {
            noKeyPressed = 1;
            Debug.Log("No key was pressed.");
        }
        else
        {
            noKeyPressed = 0;
        }

        List<float> row = new List<float>();

        row.Add(leftShift);
        row.Add(space);
        row.Add(w);
        row.Add(s);
        row.Add(a);
        row.Add(d);
        row.Add(q);
        row.Add(e);
        row.Add(noKeyPressed);

        key.Add(row);

        if (Input.GetKeyDown(KeyCode.O))
        {
            StoreData(key);
        }

        //StoreData_Input(leftShift + "\n");
    }


    public void StoreData(List<List<float>> line)
    {
        FileStream fs = new FileStream("InPut.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
        StreamWriter sw = new StreamWriter(fs);

        for (int i = 0; i < line.Count; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                sw.Write(key[i][j] + " ");
            }
            sw.WriteLine();
        }
        sw.Close();
        Debug.Log("数据保存成功!");

        /*
        public void StoreData_Input(string data)
        {
            FileStream fs = new FileStream("data_Input.csv", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            m = m + data;
            if (m != null)
            {
                sw.WriteLine(m);
            }
            sw.Close();
            Debug.Log("store data ok!");
        }
        */
    }
}
