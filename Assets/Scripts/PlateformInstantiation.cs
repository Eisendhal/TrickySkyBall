using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformInstantiation : MonoBehaviour {
    
    public GameObject FlatPlateform;
    public GameObject UpPlateform;
    public GameObject Obstacle;

    private GameObject FlatNeutral1, FlatNeutral2, FlatSpeedUp1, FlatSpeedUp2, FlatSpeedDown1, FlatSpeedDown2;
    private GameObject UpNeutral1, UpNeutral2, UpSpeedUp1, UpSpeedUp2, UpSpeedDown1, UpSpeedDown2;
    private GameObject ObstacleLeft, ObstacleRight;

    public Transform Player;

    private List<GameObject> PlateformsAvailable;
    private List<GameObject> AllPlateforms;

    private Vector3 PreviousPosition;
    // Y and Z before instantiating a new plateform
    public float deltaY;
    public float deltaZ;
    private int activePlateforms = 0;

    // Value in Z needed for the ball to reach tthe next plateform
    public float minimalZ;

    System.Random rnd;

    public GameObject GeneralManager;

    public Material[] PlatefomMaterials;
    private int materialCount = 0;
    private int countObstacle = 1;
    private int countMaterialChange = 1;

    public delegate void PlateformInstantiationAction();
    public static event PlateformInstantiationAction OnPlateformInstantiated;

    // Use this for initialization
    void Start () {
        rnd = new System.Random();
        PreviousPosition = Vector3.zero;
        minimalZ = -100.0f;
        PlateformsAvailable = new List<GameObject>();
        AllPlateforms = new List<GameObject>();
        InstantiatePlateforms();
        // Initializing the arrays
        PlateformsAvailable.Add(FlatNeutral1);
        PlateformsAvailable.Add(FlatNeutral2);
        PlateformsAvailable.Add(FlatSpeedUp1);
        PlateformsAvailable.Add(FlatSpeedUp2);
        PlateformsAvailable.Add(FlatSpeedDown1);
        PlateformsAvailable.Add(FlatSpeedDown2);
        PlateformsAvailable.Add(UpNeutral1);
        PlateformsAvailable.Add(UpNeutral2);
        PlateformsAvailable.Add(UpSpeedUp1);
        PlateformsAvailable.Add(UpSpeedUp2);
        PlateformsAvailable.Add(UpSpeedDown1);
        PlateformsAvailable.Add(UpSpeedDown2);

        AllPlateforms.Add(FlatNeutral1);
        AllPlateforms.Add(FlatNeutral2);
        AllPlateforms.Add(FlatSpeedUp1);
        AllPlateforms.Add(FlatSpeedUp2);
        AllPlateforms.Add(FlatSpeedDown1);
        AllPlateforms.Add(FlatSpeedDown2);
        AllPlateforms.Add(UpNeutral1);
        AllPlateforms.Add(UpNeutral2);
        AllPlateforms.Add(UpSpeedUp1);
        AllPlateforms.Add(UpSpeedUp2);
        AllPlateforms.Add(UpSpeedDown1);
        AllPlateforms.Add(UpSpeedDown2);

        // Instantiate first plateforms
        SelectOneAndPositionPlateform(true);
        SelectOneAndPositionPlateform(false);
        SelectOneAndPositionPlateform(false);
        SelectOneAndPositionPlateform(false);
        SelectOneAndPositionPlateform(false);
	}
	
	void Update () {
        if (PreviousPosition.y < -countMaterialChange * 300)
        {
            ChangeMaterial();
            countMaterialChange++;
        }
        //Chose a plateform randomly among the available ones
        foreach (GameObject plateform in AllPlateforms)
        {
            if (!PlateformsAvailable.Contains(plateform))
            {
                if (plateform.transform.position.y > Player.position.y)
                {
                    PlateformsAvailable.Add(plateform);
                    activePlateforms--;
                    minimalZ = plateform.transform.position.z;
                }
            }
        }
        // There must be at least 3 plateforms under the player at any time so he can think his move ahead
        while (activePlateforms < 5)
        {
            SelectOneAndPositionPlateform(false);
        }
    }

    // instantiate the plateforms and the obstacle objects
    private void InstantiatePlateforms()
    {
        FlatNeutral1 = Instantiate(FlatPlateform);
        FlatNeutral2 = Instantiate(FlatPlateform);
        FlatSpeedUp1 = Instantiate(FlatPlateform);
        FlatSpeedUp2 = Instantiate(FlatPlateform);
        FlatSpeedDown1 = Instantiate(FlatPlateform);
        FlatSpeedDown2 = Instantiate(FlatPlateform);

        UpNeutral1 = Instantiate(UpPlateform);
        UpNeutral2 = Instantiate(UpPlateform);
        UpSpeedUp1 = Instantiate(UpPlateform);
        UpSpeedUp2 = Instantiate(UpPlateform);
        UpSpeedDown1 = Instantiate(UpPlateform);
        UpSpeedDown2 = Instantiate(UpPlateform);

        ObstacleLeft = Instantiate(Obstacle);
        ObstacleRight = Instantiate(Obstacle);
        // Add materials to the objects

        if (PlatefomMaterials.Length > 0)
        {
            FlatNeutral1.GetComponent<MeshRenderer>().material = PlatefomMaterials[0];
            FlatNeutral2.GetComponent<MeshRenderer>().material = PlatefomMaterials[0];
            FlatSpeedUp1.GetComponent<MeshRenderer>().material = PlatefomMaterials[0];
            FlatSpeedUp2.GetComponent<MeshRenderer>().material = PlatefomMaterials[0];
            FlatSpeedDown1.GetComponent<MeshRenderer>().material = PlatefomMaterials[0];
            FlatSpeedDown2.GetComponent<MeshRenderer>().material = PlatefomMaterials[0];
            UpNeutral1.GetComponent<MeshRenderer>().material = PlatefomMaterials[0];
            UpNeutral2.GetComponent<MeshRenderer>().material = PlatefomMaterials[0];
            UpSpeedUp1.GetComponent<MeshRenderer>().material = PlatefomMaterials[0];
            UpSpeedUp2.GetComponent<MeshRenderer>().material = PlatefomMaterials[0];
            UpSpeedDown1.GetComponent<MeshRenderer>().material = PlatefomMaterials[0];
            UpSpeedDown2.GetComponent<MeshRenderer>().material = PlatefomMaterials[0];
        }

        // Add component to the arrows for the speed up or down
        FlatNeutral1.transform.GetChild(0).gameObject.GetComponent<Arrows>().ArrowDirection = Arrows.Direction.None;
        FlatNeutral2.transform.GetChild(0).gameObject.GetComponent<Arrows>().ArrowDirection = Arrows.Direction.None;
        FlatSpeedUp1.transform.GetChild(0).gameObject.GetComponent<Arrows>().ArrowDirection = Arrows.Direction.Forward;
        FlatSpeedUp2.transform.GetChild(0).gameObject.GetComponent<Arrows>().ArrowDirection = Arrows.Direction.Forward;
        FlatSpeedDown1.transform.GetChild(0).gameObject.GetComponent<Arrows>().ArrowDirection = Arrows.Direction.Backward;
        FlatSpeedDown2.transform.GetChild(0).gameObject.GetComponent<Arrows>().ArrowDirection = Arrows.Direction.Backward;

        UpNeutral1.transform.GetChild(0).gameObject.GetComponent<Arrows>().ArrowDirection = Arrows.Direction.None;
        UpNeutral2.transform.GetChild(0).gameObject.GetComponent<Arrows>().ArrowDirection = Arrows.Direction.None;
        UpSpeedUp1.transform.GetChild(0).gameObject.AddComponent<Arrows>().ArrowDirection = Arrows.Direction.Forward;
        UpSpeedUp2.transform.GetChild(0).gameObject.AddComponent<Arrows>().ArrowDirection = Arrows.Direction.Forward;
        UpSpeedDown1.transform.GetChild(0).gameObject.AddComponent<Arrows>().ArrowDirection = Arrows.Direction.Backward;
        UpSpeedDown2.transform.GetChild(0).gameObject.AddComponent<Arrows>().ArrowDirection = Arrows.Direction.Backward;

        OnPlateformInstantiated();

        // Move the objects out of the camera at the beginning
        FlatNeutral1.transform.position = new Vector3(100f, 100f, 100f);
        FlatNeutral2.transform.position = new Vector3(110f, 110f, 110f);
        FlatSpeedUp1.transform.position = new Vector3(120f, 120f, 120f);
        FlatSpeedUp2.transform.position = new Vector3(130f, 130f, 130f);
        FlatSpeedDown1.transform.position = new Vector3(140f, 140f, 140f);
        FlatSpeedDown2.transform.position = new Vector3(150f, 150f, 150f);
        UpNeutral1.transform.position = new Vector3(90f, 90f, 90f);
        UpNeutral2.transform.position = new Vector3(80f, 80f, 80f);
        UpSpeedUp1.transform.position = new Vector3(70f, 70f, 70f);
        UpSpeedUp2.transform.position = new Vector3(60f, 60f, 60f);
        UpSpeedDown1.transform.position = new Vector3(50f, 50f, 50f);
        UpSpeedDown2.transform.position = new Vector3(40f, 40f, 40f);
        ObstacleLeft.transform.position = new Vector3(-1.5f, 0, -50f);
        ObstacleRight.transform.position = new Vector3(1.5f, 0, -50f);
    }

    // Select and position a plateform among the available ones
    private void SelectOneAndPositionPlateform(bool IsFirstPlateform)
    {
        if (PlateformsAvailable.Count > 0)
        {
            int selectedIndex = rnd.Next(PlateformsAvailable.Count - 1);
            if (IsFirstPlateform)
            {
                PlateformsAvailable[selectedIndex].transform.position = new Vector3(0, -3.0f, 0.0f);
            }
            else
            {
                PlateformsAvailable[selectedIndex].transform.position = PreviousPosition + new Vector3(rnd.Next(66) / 10.0f - 3.3f, -deltaY, deltaZ);
            }
            PreviousPosition = new Vector3(0, PlateformsAvailable[selectedIndex].transform.position.y, PlateformsAvailable[selectedIndex].transform.position.z);
            if (PreviousPosition.y < -countObstacle * 100 && Player.transform.position.z > ObstacleLeft.transform.position.z)
            {
                ObstacleLeft.transform.position = PlateformsAvailable[selectedIndex].transform.position - new Vector3(3.0f, 0, 0);
                ObstacleRight.transform.position = PlateformsAvailable[selectedIndex].transform.position + new Vector3(3.0f, 0, 0);
                countObstacle++;
            }
            PlateformsAvailable.Remove(PlateformsAvailable[selectedIndex]);
            activePlateforms++;
        }
    }

    // Change the material of the plateforms
    private void ChangeMaterial()
    {
        materialCount++;
        materialCount = materialCount % PlatefomMaterials.Length;
        foreach (GameObject plateform in AllPlateforms)
        {
            plateform.GetComponent<MeshRenderer>().material = PlatefomMaterials[materialCount];
        }
    }
}
