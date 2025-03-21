namespace Library.Utils
{
    public class Enums
    {
        static public bool HasField<TEnum>(string field)
        {
            foreach (var v in Enum.GetValues(typeof(TEnum)))
            {
                if (field == nameof(v))
                {
                    return true;
                }
            }
            return false;
        }

        static public string[] GetFields<TEnum>()
        {
            var buf = new List<string>();
            foreach (var v in Enum.GetValues(typeof(TEnum)))
            {
                buf.Append(nameof(v));
            }
            return buf.ToArray();
        }

        static public string GetFieldsAsString<TEnum>(string separation = ", ")
        {
            return string.Join(separation, GetFields<TEnum>());
        }
    }
}
