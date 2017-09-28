using System;
using UnityEngine;
using Kopernicus;
using Kopernicus.Configuration;
using Kopernicus.Configuration.ModLoader;


namespace PQSMod_GroundCube
{
    public class PQSMod_GroundCube : PQSMod
    {
        public bool scaleDeformityByRadius = true;
        public double deformity = 1;
        public double radius = 1;
        public double power = 1;
        public bool moveCorner = false;
        public float angle = 0;
        public Vector3 position = Vector3.up;

        public override void OnVertexBuildHeight(PQS.VertexBuildData data)
        {
            Vector3 reference = moveCorner ? Vector3.one : Vector3.up;
            Vector3 vector = data.directionFromCenter;

            Quaternion rotation = Quaternion.FromToRotation(reference, position) * Quaternion.AngleAxis(angle, reference);
            vector = Quaternion.Inverse(rotation) * vector;

            double x = Math.Abs(vector.x);
            double y = Math.Abs(vector.y);
            double z = Math.Abs(vector.z);
            double max = Math.Max(x, Math.Max(y, z));

            x /= max;
            y /= max;
            z /= max;

            data.vertHeight += sphere.radius * deformity * ((UtilMath.LerpUnclamped(1, Math.Pow(x * x + y * y + z * z, 0.5), power) * radius) - 1);
        }
    }

    [RequireConfigType(ConfigType.Node)]
    public class GroundCube : ModLoader<PQSMod_GroundCube>
    {
        [ParserTarget("radius", optional = true)]
        private NumericParser<double> radius
        {
            get { return mod.radius; }
            set { mod.radius = value; }
        }

        [ParserTarget("power", optional = true)]
        private NumericParser<double> power
        {
            get { return mod.power; }
            set { mod.power = value; }
        }

        [ParserTarget("moveCorner", optional = true)]
        private NumericParser<bool> moveCorner
        {
            get { return mod.moveCorner; }
            set { mod.moveCorner = value; }
        }

        [ParserTarget("angle", optional = true)]
        private NumericParser<float> angle
        {
            get { return mod.angle; }
            set { mod.angle = value; }
        }

        [ParserTarget("position")]
        public Vector3Parser position
        {
            get { return mod.position; }
            set { mod.position = value; }
        }

        [ParserTarget("Position")]
        public PositionParser Position
        {
            set { mod.position = value; }
        }
    }

    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    class Version : MonoBehaviour
    {
        void Awake()
        {
            Debug.Log("[SigmaLog] Version Check:   PQSMod_GroundCube v0.2.0");
        }
    }
}
