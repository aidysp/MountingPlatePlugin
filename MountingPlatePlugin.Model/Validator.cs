namespace MountingPlatePlugin.Model
{
    /// <summary>
    /// —татический класс дл€ валидации параметров.
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// ѕровер€ет, находитс€ ли значение в допустимом диапазоне параметра.
        /// </summary>
        /// <typeparam name="T">“ип параметра.</typeparam>
        /// <param name="parameter">ѕараметр с границами.</param>
        /// <param name="value">ѕровер€емое значение.</param>
        /// <returns>true если значение валидно, иначе false.</returns>
        public static bool ValidateParameter<T>(Parameter<T> parameter, T value)
            where T : IComparable<T>
        {
            return value.CompareTo(parameter.MinValue) >= 0 &&
                   value.CompareTo(parameter.MaxValue) <= 0;
        }
    }
}