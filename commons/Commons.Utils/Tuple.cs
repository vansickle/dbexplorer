namespace Commons.Utils
{
    public class Tuple<T, T1>
    {
        private T v;
        private T1 v1;

        public Tuple(T v, T1 v1)
        {
            this.v = v;
            this.v1 = v1;
        }

        public T V
        {
            get { return v; }
        }

        public T1 V1
        {
            get { return v1; }
        }
    }

    public class Tuple<T, T1, T2>
    {
        private T v;
        private T1 v1;
        private T2 v2;

        public Tuple(T v, T1 v1, T2 v2)
        {
            this.v = v;
            this.v1 = v1;
            this.v2 = v2;
        }

        public T V
        {
            get { return v; }
        }

        public T1 V1
        {
            get { return v1; }
        }
        public T2 V2
        {
            get { return v2; }
        }
    }
}
