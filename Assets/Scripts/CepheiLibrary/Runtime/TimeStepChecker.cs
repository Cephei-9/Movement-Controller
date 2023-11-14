using UnityEngine;

namespace SimpleSandbox.NPC
{
    public class TimeStepChecker
    {
        [System.Serializable]
        public class Data
        {
            public float Step = 5;
        }

        private float _lastFixedTime;
        private bool _nonFixedTime = true;

        private Data _data;

        public TimeStepChecker(Data data) => _data = data;

        public bool CheckStep()
        {
            float nowTime = Time.realtimeSinceStartup;
            float timeDifference = nowTime - _lastFixedTime;

            if (_nonFixedTime || timeDifference > _data.Step)
            {
                _nonFixedTime = false;

                _lastFixedTime = nowTime;
                return true;
            }

            return false;
        }
    }
}