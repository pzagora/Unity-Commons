namespace Common
{
    public static class Hash
    {
        public static int Compute(params object[] objects)
        {
            unchecked
            {
                var result = 0;
                for (int i = 0; i < objects.Length; ++i)
                {
                    result = (result * 397) ^ objects[i].GetHashCode();
                }
                return result;
            }
        }
    }
}
