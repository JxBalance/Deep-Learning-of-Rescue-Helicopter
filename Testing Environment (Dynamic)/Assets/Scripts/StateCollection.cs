using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StateCollection : MonoBehaviour {
    public GameObject helicopter;
    public GameObject riverBoat;
    private float helicopter_x;
    private float helicopter_y;
    private float helicopter_z;
    private float helicopter_vx;
    private float helicopter_vy;
    private float helicopter_vz;
    private float helicopter_rotation_x;
    private float helicopter_rotation_y;
    private float helicopter_rotation_z;
    private int time;

    private float riverBoat_x;
    private float riverBoat_y;
    private float riverBoat_z;
    private float riverBoat_rotation_y;

    List<List<float>> line = new List<List<float>>();

    ClientControlSend client = new ClientControlSend();

    // Use this for initialization
    void Start () {
        time = 0;
        client.Connect("192.168.217.1", 12323);
        client.ThreadOfReceive();
    }
	
	// Update is called once per frame
	void Update () {

        helicopter_x = helicopter.transform.position.x;
        helicopter_y = helicopter.transform.position.y;
        helicopter_z = helicopter.transform.position.z;
        helicopter_vx = helicopter.GetComponent<Rigidbody>().velocity.x;
        helicopter_vy = helicopter.GetComponent<Rigidbody>().velocity.y;
        helicopter_vz = helicopter.GetComponent<Rigidbody>().velocity.z;
        helicopter_rotation_x = helicopter.transform.rotation.x;
        helicopter_rotation_y = helicopter.transform.rotation.y;
        helicopter_rotation_z = helicopter.transform.rotation.z;
        time += 1;
        riverBoat_x = riverBoat.transform.position.x;
        riverBoat_y = riverBoat.transform.position.y;
        riverBoat_z = riverBoat.transform.position.z;
        riverBoat_rotation_y = riverBoat.transform.rotation.y;

        client.Send(helicopter_x.ToString() + "|" + helicopter_y.ToString() + "|" 
            + helicopter_z.ToString() + "|" + helicopter_rotation_y.ToString() + "|" 
            + helicopter_rotation_z.ToString() + "|" + time.ToString());

        List<float> row = new List<float>();

        row.Add(helicopter_x);
        row.Add(helicopter_y);
        row.Add(helicopter_z);
        row.Add(helicopter_vx);
        row.Add(helicopter_vy);
        row.Add(helicopter_vz);
        row.Add(helicopter_rotation_x);
        row.Add(helicopter_rotation_y);
        row.Add(helicopter_rotation_z);
        row.Add(time);
        row.Add(riverBoat_x);
        //row.Add(riverBoat_y);
        row.Add(riverBoat_z);
        //row.Add(riverBoat_rotation_y);


        line.Add(row);


        /*
        StoreData_helicopter_x(helicopter_x + "\n");
        */

        if (Input.GetKeyDown (KeyCode.O))
        {
            StoreData(line);
        }

    }
    
    
    public void StoreData(List<List<float>> line)
    {
        FileStream fs = new FileStream("Data.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
        StreamWriter sw = new StreamWriter(fs);

        for (int i = 0; i < line.Count ; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                sw.Write(line[i][j]+" ");
            }
            sw.WriteLine();
        }
        sw.Close();
        Debug.Log("数据保存成功!");
    }

    public void StorePlotData(List<List<float>> line)
    {
        FileStream fs = new FileStream("PlotData.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
        StreamWriter sw = new StreamWriter(fs);

        for (int i = 0; i < line.Count; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                sw.Write(line[i][j] + " ");
            }
            sw.WriteLine();
        }
        sw.Close();
        Debug.Log("数据保存成功!");
    }

    /*
    public void StoreData_helicopter_x(string data)
    {
        FileStream fs_1 = new FileStream("data_helicopter_x.csv", FileMode.OpenOrCreate, FileAccess.ReadWrite);
        StreamWriter sw_1 = new StreamWriter(fs_1);
        m_Data = m_Data + data;
        if (m_Data != null)
        {
            sw_1.WriteLine(m_Data);
        }
        sw_1.Close();
        Debug.Log("store data ok!");
    }

    */
}
