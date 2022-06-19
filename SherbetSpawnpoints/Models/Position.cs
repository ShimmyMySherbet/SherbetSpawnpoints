using System.Xml.Serialization;
using UnityEngine;

namespace SherbetSpawnpoints.Models.Config
{
    public class Position
    {
        public float Rot;
        public float X;
        public float Y;
        public float Z;

        [XmlIgnore]
        public bool IsValid => X != 0 || Y != 0 || Z != 0;

        public Vector3 Vector3 => new Vector3(X, Y, Z);

        public static Position FromVector3(Vector3 position, float rotation)
        {
            return new Position()
            {
                Rot = rotation,
                X = position.x,
                Y = position.y,
                Z = position.z
            };
        }

        public (Vector3 position, float rot) Deconstruct() => (new Vector3(X, Y, Z), Rot);

        public override string ToString() => $"[{X}, {Y}, {Z}]";
    }
}