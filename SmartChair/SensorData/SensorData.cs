using System;

namespace SmartChair.model
{
    [Serializable()]
    public class SensorData
    {
        private float _bottomLeft, _bottomoRight, _topLeft, _topRight, _weightKg;
        private CenterOfGravity _cog;
        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public CenterOfGravity Cog
        {
            get { return _cog; }
            set { _cog = value; }
        }

        public float WeightKg
        {
            get { return _weightKg; }
            set { _weightKg = value; }
        }

        public float TopRight
        {
            get { return _topRight; }
            set { _topRight = value; }
        }

        public float TopLeft
        {
            get { return _topLeft; }
            set { _topLeft = value; }
        }

        public float BottomoRight
        {
            get { return _bottomoRight; }
            set { _bottomoRight = value; }
        }

        public float BottomLeft
        {
            get { return _bottomLeft; }
            set { _bottomLeft = value; }
        }

        public SensorData(float bL, float bR, float tL, float tR, float wKg, CenterOfGravity cog)
        {
            _bottomLeft = bL;
            _bottomoRight = bR;
            _topLeft = tL;
            _topRight = tR;
            _weightKg = wKg;
            _cog = cog;
            _date = DateTime.Now;
        }

        [Serializable()]
        public class CenterOfGravity
        {
            private float _X, _Y;

            public float Y
            {
                get { return _Y; }
                set { _Y = value; }
            }

            public float X
            {
                get { return _X; }
                set { _X = value; }
            }

            public CenterOfGravity(float X, float Y)
            {
                _X = X;
                _Y = Y;
            }
        }
    }
}
