using UnityEngine;
using Pixeltheory.Messaging;


[MessagingInterface]
public interface TestInterfaceVectorParameter
{
    [MessagingTargetAll] public void TestMethodVector2Parameter(Vector2 vector2Param);
    [MessagingTargetAll] public void TestMethodVector3Parameter(Vector3 vector3Param);
    [MessagingTargetAll] public void TestMethodVector4Parameter(Vector4 vector4Param);
    [MessagingTargetAll] public void TestMethodVector2IntParameter(Vector2Int vector2IntParam);
    [MessagingTargetAll] public void TestMethodVector3IntParameter(Vector3Int vector3IntParam);
}