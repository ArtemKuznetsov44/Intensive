using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Drawing; 

public class CameraChange : MonoBehaviour, IPointerClickHandler
{
    // ���� ������, ���� � Unity:
    public GameObject camera_main;      // ������ �����.
    public GameObject camera_object;    // ������ �� ������ � ��������.
    public GameObject camera_rotative;  // ������ �� ������ ������ (�����������).
    public GameObject emptyObject;      // ������ ������, �� ������� ���������� ������.

    private Quaternion startAngles;     // ������ ��������� ���� ��������.
    private bool flag = false;          // ���� ��� ��������.

    public Text information;
    public Image picture; 

    private static string pathForTxt = @"Assets\Information\";

    public Sprite tower;
    public Sprite guardHouse;
    public Sprite homeBorsheva;
    public Sprite besedka;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(gameObject.tag + ".txt"); 

        information.text = File.ReadAllText(pathForTxt + gameObject.tag + ".txt");

        switch (gameObject.tag)
        {
            case "Tower":
                picture.sprite = tower;
                break;
            case "HomeBorsheva":
                picture.sprite = homeBorsheva;
                break;
            case "GuardHouse":
                picture.sprite = guardHouse;
                break;
            case "Besedka":
                picture.sprite = besedka;
                break;
        }
        
        if (camera_main.activeSelf)
        {
            camera_main.SetActive(false); // ���������� ������ �� �����.
            camera_object.SetActive(true); // ��������� ������ �� ������ � ��������.

            startAngles = transform.rotation;  // ����� ���� �������� �������, �� ������� ������.
            // ���������� ������� �������:
            emptyObject.transform.rotation = startAngles; // ���������� ���������� ���� �� ������ ������.
            // ������ ������� ��� ������� ������� ����� ��, ��� � � ������� �� ������� ������.
            emptyObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            // ������ �� ������ ������ ����� ���������� �������������.

            // ���������� ������ � �������� �� ������ �� ������� ����������� ������, ������� ���������� �� ������ ������.
            camera_object.transform.rotation = camera_rotative.transform.rotation;
            camera_object.transform.position = camera_rotative.transform.position;
            return; 
        }
        if (!flag) 
        { 
            flag = !flag;
            camera_object.SetActive(!camera_object.activeSelf); 
            camera_rotative.SetActive(!camera_rotative.activeSelf);
            Debug.Log("Start Rotation"); 
        }
        else
        {
            flag = !flag;
            camera_object.SetActive(!camera_object.activeSelf);
            camera_rotative.SetActive(!camera_rotative.activeSelf); 
            emptyObject.transform.rotation = startAngles;   
            Debug.Log("Stop rotation!");
        }
    }

    private void Update() { if (flag) emptyObject.transform.Rotate(0, -25 * Time.deltaTime, 0); }

    public void Exit() // ��� ������ �� ������ �����.
    {
        camera_rotative.SetActive(false);  
        camera_object.SetActive(false);
        camera_main.SetActive(true); 
    }
}
