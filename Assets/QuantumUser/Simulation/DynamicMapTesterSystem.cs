using System.Collections.Generic;
using Photon.Deterministic;

using Quantum;
using UnityEngine.Scripting;


[Preserve]
public unsafe class DynamicMapTesterSystem : SystemMainThread
{

    public void AddCollider(Frame f, FPVector3 position)
    {
        DynamicMap dynamicMap = f.Map as DynamicMap;

        Log.Error("Adding mesh collider");
        dynamicMap.AddMeshCollider(
                f,
                CellMesh,
                staticData: new StaticColliderData()
                {
                    MutableMode =
                        PhysicsCommon.StaticColliderMutableMode.ToggleableStartOn
                },
                position: position,
                resetPhysics: false);
        Log.Error("Added mesh collider");
    }

    public void DeleteCollider(Frame f, int idx)
    {
        DynamicMap dynamicMap = f.Map as DynamicMap;
        Log.Error("Removing mesh collider");
        dynamicMap.RemoveMeshCollider(f, idx, resetPhysics: false);
        Log.Error("Removed mesh collider");
    }

    public override void OnInit(Frame f)
    {
        var dynamicMapToAdd = DynamicMap.FromStaticMap(f.Map);
        f.AddAsset(dynamicMapToAdd);
        f.Map = dynamicMapToAdd;
    }
    public override void Update(Frame f)
    {
        if (f.Number == 60)
        {
            AddCollider(f, new HexPosition(0, 0, 0).GetSpatialPosition());
            AddCollider(f, new HexPosition(0, 1, 0).GetSpatialPosition());
            AddCollider(f, new HexPosition(0, 2, 0).GetSpatialPosition());
        }
        if (f.Number == 61)
        {
            DeleteCollider(f, 1);
            DeleteCollider(f, 1);
        }
        f.ResetPhysics();
    }

    public static readonly FP TopGap = HexPosition.HexCellHeight / 4;

    public readonly static IList<FPVector3> CellMesh = GenerateCellMesh();


    public static IList<FPVector3> GenerateCellMesh()
    {
        var hl = HexPosition.HexSideLength / FP._2;
        var hh = HexPosition.HalfHeight;
        var lhh = HexPosition.HexCellHeight / FP._2 - TopGap;

        var AH = new FPVector3(-hl, lhh, -hh);
        var BH = new FPVector3(hl, lhh, -hh);
        var CH = new FPVector3(2 * hl, lhh, 0);
        var DH = new FPVector3(hl, lhh, hh);
        var EH = new FPVector3(-hl, lhh, hh);
        var FH = new FPVector3(-2 * hl, lhh, 0);

        var AL = new FPVector3(-hl, -lhh, -hh);
        var BL = new FPVector3(hl, -lhh, -hh);
        var CL = new FPVector3(2 * hl, -lhh, 0);
        var DL = new FPVector3(hl, -lhh, hh);
        var EL = new FPVector3(-hl, -lhh, hh);
        var FL = new FPVector3(-2 * hl, -lhh, 0);

        return new List<FPVector3>(){
                    AH, EH, FH,
                    AH, DH, EH,
                    AH, BH, DH,
                    BH, CH, DH,


                    BH, AH, AL,
                    AL, BL, BH,

                    CH, BH, BL,
                    BL, CL, CH,

                    DH, CH, CL,
                    CL, DL, DH,

                    EH, DH, DL,
                    DL, EL, EH,

                    FH, EH, EL,
                    EL, FL, FH,

                    AH, FH, FL,
                    FL, AL, AH,


                    EL, AL, FL,
                    DL, AL, EL,
                    BL, AL, DL,
                    CL, BL, DL,
            };
    }
}

