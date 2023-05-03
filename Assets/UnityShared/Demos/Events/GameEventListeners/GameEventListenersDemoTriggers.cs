using System;
using UnityEngine;
using UnityEngine.Events;

namespace UnityShared.Demos
{
    /// <summary>
    /// this would be a custom class for the game that thows events whith custom parameters
    /// </summary>
    public class GameEventListenersDemoTriggers : MonoBehaviour
    {
        public TestValues testValues;

        public UnityEvent onRunTest;
        public UnityEvent<int> onRunTestInt;
        public UnityEvent<string> onRunTestString;
        public UnityEvent<long> onRunTestLong;
        public UnityEvent<float> onRunTestFloat;
        public UnityEvent<double> onRunTestDouble;
        public UnityEvent<bool> onRunTestBool;
        public UnityEvent<Vector2> onRunTestVector2;
        public UnityEvent<Vector3> onRunTestVector3;
        public UnityEvent<GameObject> onRunTestGameObject;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                onRunTest.Invoke();

            if (Input.GetKeyDown(KeyCode.W))
                onRunTestString.Invoke(testValues._string);

            if (Input.GetKeyDown(KeyCode.E))
                onRunTestInt.Invoke(testValues._int);

            if (Input.GetKeyDown(KeyCode.R))
                onRunTestLong.Invoke(testValues._long);

            if (Input.GetKeyDown(KeyCode.T))
                onRunTestFloat.Invoke(testValues._float);

            if (Input.GetKeyDown(KeyCode.S))
                onRunTestDouble.Invoke(testValues._double);

            if (Input.GetKeyDown(KeyCode.D))
                onRunTestBool.Invoke(testValues._bool);

            if (Input.GetKeyDown(KeyCode.G))
                onRunTestVector2.Invoke(testValues._vector2);

            if (Input.GetKeyDown(KeyCode.Z))
                onRunTestVector3.Invoke(testValues._Vector3);

            if (Input.GetKeyDown(KeyCode.X))
                onRunTestGameObject.Invoke(this.gameObject);
        }

        [Serializable]
        public class TestValues
        {
            public string _string;
            public int _int;
            public long _long;
            public float _float;
            public double _double;
            public bool _bool;
            public Vector2 _vector2;
            public Vector3 _Vector3;
        }
    }
}