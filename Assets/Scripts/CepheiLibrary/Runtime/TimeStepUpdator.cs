using System;

namespace Cephei
{
    public class TimeStepUpdator : IUpdatable
    {
        public float TimeStep;
        public Action Action;

        private float _timer;

        public bool IsStop { get; private set; }

        public TimeStepUpdator(Action action, float timeStep)
        {
            Action = action;
            TimeStep = timeStep;
        }

        public void UpdateWork(float delta)
        {
            if (IsStop) return;

            _timer += delta;

            if (_timer > TimeStep)
            {
                _timer -= TimeStep;
                Action?.Invoke();
            }                        
        }

        public void MaximazeTimer() => _timer = TimeStep;

        public void SetStop(bool stopStatus) => IsStop = stopStatus;

        public void Reset() => _timer = 0;
    }
}