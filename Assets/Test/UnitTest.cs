using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTest : MonoBehaviour
{
    public TileVisuals mVisualsPrefabA;
    //public FunctionMesher mVisualsPrefabB;
    [SerializeField]
    public AnimationCurveContainer mBlendCurve;

    /*
    [Header("Test Tile")]
//These are the parameters a factory might generate / load from a file
//Fow now, we're just setting them manually
    [SerializeField]
    private int mPointDensity = 10;
    [SerializeField]
    private float mSize = 1f;
    [SerializeField]
    private HexGrid.TopType mTopType;
    */
    [SerializeField]
    private TileFunctionNoiseParameters mNoiseParameters;
    [SerializeField]
    private TileFunctionTaperParameters mTaperParameters;
    

    private void Start()
    {
        /*
        TileVisuals mA = Instantiate(mVisualsPrefabA);
        mA.Create();

        I2DFunction function = new TileFunction(mNoiseParameters, mTaperParameters);
        TileVisuals mB = Instantiate(mVisualsPrefabA);
        TileVisuals mBImmutable = Instantiate(mVisualsPrefabA);
        mB.Create(function);
        mBImmutable.Create(function);

        mA.name = "A";
        mB.name = "B";
        mBImmutable.name = "BOriginial";
        mBImmutable.transform.position = Vector3.one * 2f;

        Vector3 position = new Vector3(Mathf.Sqrt(3) * 3/4f, 0f, 3/4f);
        mA.transform.position = position;

        Vector3 difference = mB.transform.position - mA.transform.position;

        mB.Mesher.BlendMesh(mA.Mesher.Function, -new Vector2(difference.x, difference.z), mBlendCurve);
        */
    }
}
