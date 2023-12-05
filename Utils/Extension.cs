static class Extension
{
    public static string[] ToStringArray<T>(this T[] array)
    {
        string[] arr = new string[array.Length];
        
        for(int i = 0; i < arr.Length; i++){
            arr[i] = array[i]!.ToString()!;
        }

        return arr;
    }
}