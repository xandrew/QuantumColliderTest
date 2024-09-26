#pragma warning disable CS8524

using Photon.Deterministic;

namespace Quantum
{
    public partial struct HexPosition : System.IEquatable<HexPosition>
    {
        public static FP HexSideLength = FP._0_10;
        public static FP HexCellHeight = FP._0_02 * 3;
        public static FP HalfHeight = HexSideLength * 866025 / 1000000;

        public HexPosition(int layer, int row, int column) : this()
        {
            Layer = layer;
            Row = row;
            Column = column;
        }

        public bool Equals(HexPosition other)
        {
            return (Layer == other.Layer) && (Row == other.Row) && (Column == other.Column);
        }


        public FPVector3 GetSpatialPosition()
        {
            return new FPVector3(
                Row * FP._1_50 * HexSideLength,
                Layer * HexCellHeight,
                (Row & 1) * HalfHeight + Column * 2 * HalfHeight);
        }

        public override string ToString()
        {
            return $"L{Layer} R{Row} C{Column}";
        }
    }
}
