using System;
using Android.Hardware;

namespace fastCSharp.android
{
    /// <summary>
    /// Android.Hardware.Sensor ��չ
    /// </summary>
    public static class sensor
    {
        /// <summary>
        /// Type
        /// </summary>
        /// <param name="sensor"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static SensorType getType(this Sensor sensor)
        {
            return sensor.Type;
        }
    }
}