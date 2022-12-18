using UnityEngine;
using Pixeltheory.Messaging;


[MessagingInterface]
public interface TestInterfaceVectorParameter
{
    void TestMethodVector2Parameter(Vector2 vector2Param);
    void TestMethodVector3Parameter(Vector3 vector3Param);
    void TestMethodVector4Parameter(Vector4 vector4Param);
    void TestMethodVector2IntParameter(Vector2Int vector2IntParam);
    void TestMethodVector3IntParameter(Vector3Int vector3IntParam);
}