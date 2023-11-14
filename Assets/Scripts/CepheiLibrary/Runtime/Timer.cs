namespace Cephei
{
    public class Timer
    {
        public float TargetTime;
        public float CurrentTime;

        public float LerpValue => CurrentTime / TargetTime;

        public Timer() { }

        public Timer(float targetTime) => TargetTime = targetTime;

        public bool UpdateTime(float delta)
        {
            CurrentTime += delta;

            if(CurrentTime >= TargetTime)
            {
                CurrentTime -= TargetTime;
                return true;
            }

            return false;
        }

        public void Reset() => CurrentTime = 0;
    }
}