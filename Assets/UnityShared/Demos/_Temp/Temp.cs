using UnityEngine;
using UnityShared.Commons.PropertyAttributes;
using UnityShared.Commons.Structs;

public class Temp : MonoBehaviour
{
    public Size2 size1;
    public Size2Int size2;
    public RangeNumber<float> range1;
    public RangeNumber<int> range3;
    [RangeFloatSlider(-10, 10)] public RangeNumber<float> range2;
    [TagSelectorAttribute] public string tagTest1;
    [TagSelectorAttribute(UseDefaultTagFieldDrawer = true)] public string tagTest2;
}
